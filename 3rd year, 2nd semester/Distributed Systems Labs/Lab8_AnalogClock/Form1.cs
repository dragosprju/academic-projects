using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lab8_AnalogClock
{
    public partial class Form1 : Form
    {
        SystemTime st = new SystemTime();

        Timer t = new Timer();

        int WIDTH = 300, HEIGHT = 300, secHAND = 140, minHAND = 110, hrHAND = 80;

        //center
        int cx, cy;

        Bitmap bmp;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //create bitmap
            bmp = new Bitmap(WIDTH + 1, HEIGHT + 1);

            //center
            cx = WIDTH / 2;
            cy = HEIGHT / 2;

            //backcolor
            this.BackColor = Color.Black;

            //timer
            t.Interval = 1000;      //in millisecond
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
        }

        private void t_Tick(object sender, EventArgs e)
        {
            //create graphics
            g = Graphics.FromImage(bmp);

            //get time
            LibWrap.GetLocalTime(out st);
            int ss = st.second;
            int mm = st.minute;
            int hh = st.hour;

            int[] handCoord = new int[2];

            //clear
            g.Clear(Color.Black);

            //draw circle
            g.DrawEllipse(new Pen(Color.White, 1f), 0, 0, WIDTH, HEIGHT);

            //draw figure
            g.DrawString("12", new Font("Consolas", 12), Brushes.White, new PointF(140, 2));
            g.DrawString("3", new Font("Consolas", 12), Brushes.White, new PointF(286, 140));
            g.DrawString("6", new Font("Consolas", 12), Brushes.White, new PointF(142, 282));
            g.DrawString("9", new Font("Consolas", 12), Brushes.White, new PointF(0, 140));

            //second hand
            handCoord = msCoord(ss, secHAND);
            g.DrawLine(new Pen(Color.Red, 1f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //minute hand
            handCoord = msCoord(mm, minHAND);
            g.DrawLine(new Pen(Color.White, 2f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //hour hand
            handCoord = hrCoord(hh % 12, mm, hrHAND);
            g.DrawLine(new Pen(Color.White, 3f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //load bmp in picturebox1
            pictureBox1.Image = bmp;

            //disp time
            string min = String.Format("{0,2:D2}", st.minute);
            string sec = String.Format("{0,2:D2}", st.second);
            this.Text = "Analog Clock -  " + hh + ":" + min + ":" + sec;

            //dispose
            g.Dispose();
        }

        //coord for minute and second hand
        private int[] msCoord(int val, int hlen)
        {
            int[] coord = new int[2];
            val *= 6;   //each minute and second make 6 degree

            if (val >= 0 && val <= 180)
            { 
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            /*label1.Text = "Sin: " + Math.Sin(Math.PI* val / 180).ToString() + "\r\n"
            + "val: " + val.ToString() + "\r\n"
            + "PI * val: " + (Math.PI* val).ToString() + "\r\n"
            + "PI * val / 180: " + (Math.PI* val / 180).ToString() + "\r\n";*/
            label1.ForeColor = Color.White;
            return coord;
        }

        //coord for hour hand
        private int[] hrCoord(int hval, int mval, int hlen)
        {
            int[] coord = new int[2];

            //each hour makes 30 degree
            //each min makes 0.5 degree
            int val = (int)((hval * 30) + (mval * 0.5));

            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }
    }

    public struct SystemTime
    {
        public ushort year;
        public ushort month;
        public ushort weekday;
        public ushort day;
        public ushort hour;
        public ushort minute;
        public ushort second;
        public ushort millisecond;
    }

    public class LibWrap
    {
        [DllImport("Kernel32.dll")]
        public static extern void GetLocalTime(out SystemTime st);
    }
}