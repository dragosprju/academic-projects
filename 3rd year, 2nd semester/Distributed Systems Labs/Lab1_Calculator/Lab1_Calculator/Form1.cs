using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1_Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            screen.Text += "1";
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            screen.Text += "2";
        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            screen.Text += "3";
        }

        private void Btn4_Click(object sender, EventArgs e)
        {
            screen.Text += "4";
        }

        private void Btn5_Click(object sender, EventArgs e)
        {
            screen.Text += "5";
        }

        private void Btn6_Click(object sender, EventArgs e)
        {
            screen.Text += "6";
        }

        private void Btn7_Click(object sender, EventArgs e)
        {
            screen.Text += "7";
        }

        private void Btn8_Click(object sender, EventArgs e)
        {
            screen.Text += "8";
        }

        private void Btn9_Click(object sender, EventArgs e)
        {
            screen.Text += "9";
        }

        private void Btn0_Click(object sender, EventArgs e)
        {
            screen.Text += "0";
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            screen.Text += " + ";
        }

        private void BtnSubstract_Click(object sender, EventArgs e)
        {
            screen.Text += " - ";
        }

        private void BtnMultiply_Click(object sender, EventArgs e)
        {
            screen.Text += " * ";
        }

        private void BtnDivide_Click(object sender, EventArgs e)
        {
            screen.Text += " / ";
        }

        private void BtnBracketL_Click(object sender, EventArgs e)
        {
            screen.Text += " ( ";
        }

        private void BtnBracketR_Click(object sender, EventArgs e)
        {
            screen.Text += " ) ";
        }

        private void BtnNeg_Click(object sender, EventArgs e)
        {
            screen.Text += '-';
        }

        private void BtnDot_Click(object sender, EventArgs e)
        {
            screen.Text += ".";
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (screen.Text.Length > 0)
            {
                if (screen.Text[screen.Text.Length - 1] == ' ')
                {
                    screen.Text = screen.Text.Remove(screen.Text.Length - 3);
                }
                else
                {
                    screen.Text = screen.Text.Remove(screen.Text.Length - 1);
                }
            }
        }

        private void BtnEquals_Click(object sender, EventArgs e)
        {
            try
            {
                string parsed = Postfix.Parse(screen.Text);
                status.Text = parsed;
                screen.Text = Postfix.Calculate(parsed);
            }
            catch(Exception)
            {
                screen.Text = "";
                status.Text = "Error. Please try again.";
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Btns_KeyDown(sender, e);
        }

        private void Btns_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad0 || (e.KeyCode == Keys.D0 && !(e.KeyData == (Keys.Shift | Keys.D0))))
            {
                Btn0_Click(null, null);
            }
            else if (e.KeyCode == Keys.NumPad1 || e.KeyCode == Keys.D1)
            {
                Btn1_Click(null, null);
            }
            else if (e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.D2)
            {
                Btn2_Click(null, null);
            }
            else if (e.KeyCode == Keys.NumPad3 || e.KeyCode == Keys.D3)
            {
                Btn3_Click(null, null);
            }
            else if (e.KeyCode == Keys.NumPad4 || e.KeyCode == Keys.D4)
            {
                Btn4_Click(null, null);
            }
            else if (e.KeyCode == Keys.NumPad5 || e.KeyCode == Keys.D5)
            {
                Btn5_Click(null, null);
            }
            else if (e.KeyCode == Keys.NumPad6 || e.KeyCode == Keys.D6)
            {
                Btn6_Click(null, null);
            }
            else if (e.KeyCode == Keys.NumPad7 || e.KeyCode == Keys.D7)
            {
                Btn7_Click(null, null);
            }
            else if (e.KeyCode == Keys.NumPad8 || (e.KeyCode == Keys.D8 && !(e.KeyData == (Keys.Shift | Keys.D8))))
            {
                Btn8_Click(null, null);
            }
            else if (e.KeyCode == Keys.NumPad9 || (e.KeyCode == Keys.D9 && !(e.KeyData == (Keys.Shift | Keys.D9))))
            {
                Btn9_Click(null, null);
            }
            else if (e.KeyData == (Keys.Shift | Keys.D9))
            {
                BtnBracketL_Click(null, null);
            }
            else if (e.KeyData == (Keys.Shift | Keys.D0))
            {
                BtnBracketR_Click(null, null);
            }
            else if (e.KeyCode == Keys.Add || e.KeyData == (Keys.Oemplus | Keys.Shift))
            {
                BtnAdd_Click(null, null);
            }
            else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
            {
                BtnSubstract_Click(null, null);
            }
            else if (e.KeyCode == Keys.Multiply || e.KeyData == (Keys.Shift | Keys.D8))
            {
                BtnMultiply_Click(null, null);
            }
            else if (e.KeyCode == Keys.Divide || e.KeyCode == Keys.OemBackslash)
            {
                BtnDivide_Click(null, null);
            }
            else if (e.KeyCode == Keys.OemPeriod || e.KeyCode == Keys.Decimal)
            {
                BtnDot_Click(null, null);
            }
            else if (e.KeyCode == Keys.Back)
            {
                BtnDel_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter || (e.KeyData == Keys.Oemplus && !(e.KeyData == (Keys.Oemplus | Keys.Shift))))
            {
                BtnEquals_Click(null, null);
            }
        }
    }
}
