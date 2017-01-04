using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using System.Timers;

namespace PongLibrary
{
    public class PingPong : MarshalByRefObject
    {
        private Timer loop = new Timer(25);

        // Constants
        public static int width = 600;
        public static int height = 400;

        public static int ballWidth = 24;
        public static int ballHeight = 24;

        public static int ballX = width / 2 - (ballWidth / 2);
        public static int ballY = height / 2 - (ballHeight / 2);
        public static int speed = 8;

        public static int ballVelocityX = 0;
        public static int ballVelocityY = 0;

        public static int[,] padsStatus = new int[4, 2];
        public static int[] score = new int[2] { 0, 0 };
        public static int[] timeoutTally = new int[4];

        Random random = new Random();
        List<int> pads = new List<int>() { 0, 1, 2, 3 };

        public override Object InitializeLifetimeService()
        {
            ILease lease = (ILease)base.InitializeLifetimeService();
            if (lease.CurrentState == LeaseState.Initial)
            {
                lease.InitialLeaseTime = TimeSpan.FromMilliseconds(200);
                lease.SponsorshipTimeout = TimeSpan.FromMilliseconds(200);
                lease.RenewOnCallTime = TimeSpan.FromMilliseconds(25);
            }
            return lease;
        }

        public PingPong()
        {
            Console.WriteLine("Client connected.");
            loop.Elapsed += Tick;

            Start();

            loop.Start();
        }
        
        private void Restart()
        {
            score[0] = 0;
            score[1] = 0;
            speed = 8;
            Start();
        }

        private void Start()
        {
            ballX = width / 2 - (ballWidth / 2);
            ballY = height / 2 - (ballHeight / 2);

            switch (random.Next(1, 5))
            {
                case 1: // towards upper-right
                    ballVelocityX = speed;
                    ballVelocityY = -speed;
                    break;
                case 2: // towards upper-left
                    ballVelocityX = -speed;
                    ballVelocityY = -speed;
                    break;
                case 3: // towards lower-left
                    ballVelocityX = -speed;
                    ballVelocityY = speed;
                    break;
                case 4: // towards lower-right
                    ballVelocityX = speed;
                    ballVelocityY = speed;
                    break;
            }
        }

        private void Tick(object sender, ElapsedEventArgs e)
        {
            if (ballX < (ballWidth / 2) || ballX >= width - (ballWidth / 2))
            {
                ballVelocityX *= -1;
            }

            if (ballY < (ballHeight / 2) || ballY >= height - (ballHeight / 2))
            {
                ballVelocityY *= -1;
            }

            ballX += ballVelocityX;
            ballY += ballVelocityY;

            for (int i = 0; i < 4; i++)
            {
                if (timeoutTally[i] > 80)
                {
                    leaving(i);
                }
                else if (!pads.Contains(i))
                {
                    timeoutTally[i]++;
                }
            }
        }

        public int[] getBallPosition()
        {
            int[] ballPosition = new int[2] { ballX, ballY };

            return ballPosition;
        }

        public int[] getBallVelocity()
        {
            int[] ballVelocity = new int[2] { ballVelocityX, ballVelocityY };

            return ballVelocity;
        }

        public void touchedLeft()
        {
            ballVelocityX = speed;
        }

        public void touchedRight()
        {
            ballVelocityX = -speed;
        }

        public int selectPad()
        {
            int toReturn = pads[0];
            pads.RemoveAt(0);
            timeoutTally[0] = 0;
            if (toReturn == 0)
            {
                Restart();
            }
            return toReturn;
        }

        public void leaving(int padNo)
        {
            if (!pads.Contains(padNo))
            {
                pads.Add(padNo);
                pads.Sort();
                padsStatus[padNo, 0] = 0;
                timeoutTally[padNo] = 0;
                Console.WriteLine("Client disconnected.");
            }
        }

        public void updatePadStatus(int padNo, int padX)
        {
            padsStatus[padNo,0] = 1;
            padsStatus[padNo,1] = padX;
        }

        public int[,] getPadsStatus()
        {
            return padsStatus;
        }

        public int[] getScore()
        {
            return score;
        }

        public void scoredLeft()
        {
            score[0]++;
            //if (speed <= 14) speed += 2;
            Start();
        }

        public void scoredRight()
        {
            score[1]++;
            //if (speed <= 14) speed += 2;
            Start();
        }

        public void timeoutCancel(int padNo)
        {
            timeoutTally[padNo] = 0;
        }
    }
}
