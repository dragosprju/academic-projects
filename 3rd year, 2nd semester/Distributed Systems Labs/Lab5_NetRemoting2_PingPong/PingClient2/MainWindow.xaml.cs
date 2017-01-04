using PongLibrary;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PingClient2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PingPong game;
        System.Windows.Threading.DispatcherTimer loop;
        List<Rectangle> pads;
        private List<Rectangle> leftPads = new List<Rectangle>();
        private List<Rectangle> rightPads = new List<Rectangle>();
        private Rectangle pad;
        private int padNo;
        private bool actioned = false;

        public MainWindow()
        {
            InitializeComponent();
            string[] args = Environment.GetCommandLineArgs();

            pads = new List<Rectangle>() { blue, green, violet, yellow };
            leftPads = new List<Rectangle>() { blue, violet };
            rightPads = new List<Rectangle>() { green, yellow };

            HttpChannel channel = new HttpChannel();
            ChannelServices.RegisterChannel(channel, false);
            Object remoteObj;
            if (args.Length == 1)
            {
                remoteObj = Activator.GetObject(typeof(PongLibrary.PingPong), "http://localhost:10000/MyURI.soap");
            }
            else
            {
                remoteObj = Activator.GetObject(typeof(PongLibrary.PingPong), "http://" + args[1] + ":10000/MyURI.soap");
            }
            game = (PingPong)remoteObj;

            selectPad();
            setScore();

            loop = new System.Windows.Threading.DispatcherTimer();
            loop.Tick += tick;
            loop.Interval = TimeSpan.FromMilliseconds(25);
            loop.Start();
        }

        private void tick(object sender, EventArgs e)
        {
            int[] ballPosition = game.getBallPosition();

            int ballX = ballPosition[0];
            int ballY = ballPosition[1];

            int padTop = (int)(Canvas.GetTop(pad));
            int ballTop = (int)(Canvas.GetTop(ball) + (playground.Height / 2));

            bool underPadTop = (ballTop + ball.Height + 2) >= padTop;
            bool overPadBottom = ((ballTop - ball.Height + 2) <= (padTop + pad.Height));

            if (ballX <= 40 && leftPads.Contains(pad))
            {          
                if (!actioned)
                {
                    if (underPadTop && overPadBottom)
                    {
                        game.touchedLeft();
                    }
                    else
                    {
                        game.scoredRight();
                    }
                }
                actioned = true;      
            }
            else if (ballX >= (playground.Width - 40) && rightPads.Contains(pad))
            {
                if (!actioned)
                {
                    if (underPadTop && overPadBottom)
                    {
                        game.touchedRight();
                    }
                    else
                    {
                        game.scoredLeft();
                    }
                } 
                actioned = true;
            }
            else
            {
                actioned = false;
            }

            Canvas.SetLeft(ball, ballX - (playground.Width / 2));
            Canvas.SetTop(ball, ballY - (playground.Height / 2));

            game.updatePadStatus(padNo, padTop);
            game.timeoutCancel(padNo);
            checkAndDrawPads();
            setScore();
        }

        private void selectPad()
        {
            padNo = game.selectPad();
            pad = pads[padNo];
            pad.Visibility = Visibility.Visible;
        }

        private void checkAndDrawPads()
        {
            int[,] padsStatus = game.getPadsStatus();

            for (int i = 0; i < 3; i++)
            {
                if (i != padNo)
                {
                    if (padsStatus[i,0] == 1)
                    {
                        pads[i].Visibility = Visibility.Visible;
                    }
                    else
                    {
                        pads[i].Visibility = Visibility.Hidden;
                    }
                    Canvas.SetTop(pads[i], padsStatus[i, 1]);
                }
            }
        }

        private void setScore()
        {
            int[] score = game.getScore();

            scoreLeft.Content = score[0].ToString();
            scoreRight.Content = score[1].ToString();
        }

        private void mouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Canvas.SetTop(pad, e.GetPosition(playground).Y - (pad.Height / 2));
            
        }

        private void windowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            game.leaving(padNo);
            loop.Stop();
        }
    }
}
