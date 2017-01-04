namespace Lab1_Calculator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.screen = new System.Windows.Forms.TextBox();
            this.Btn1 = new System.Windows.Forms.Button();
            this.Btn2 = new System.Windows.Forms.Button();
            this.Btn3 = new System.Windows.Forms.Button();
            this.Btn4 = new System.Windows.Forms.Button();
            this.Btn5 = new System.Windows.Forms.Button();
            this.BtnDivide = new System.Windows.Forms.Button();
            this.BtnMultiply = new System.Windows.Forms.Button();
            this.BtnSubstract = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnEquals = new System.Windows.Forms.Button();
            this.Btn6 = new System.Windows.Forms.Button();
            this.Btn7 = new System.Windows.Forms.Button();
            this.Btn8 = new System.Windows.Forms.Button();
            this.Btn9 = new System.Windows.Forms.Button();
            this.Btn0 = new System.Windows.Forms.Button();
            this.BtnBracketL = new System.Windows.Forms.Button();
            this.BtnBracketR = new System.Windows.Forms.Button();
            this.BtnDot = new System.Windows.Forms.Button();
            this.status = new System.Windows.Forms.Label();
            this.BtnNeg = new System.Windows.Forms.Button();
            this.BtnDel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // screen
            // 
            this.screen.Enabled = false;
            this.screen.Location = new System.Drawing.Point(12, 12);
            this.screen.Name = "screen";
            this.screen.Size = new System.Drawing.Size(260, 20);
            this.screen.TabIndex = 0;
            // 
            // Btn1
            // 
            this.Btn1.Location = new System.Drawing.Point(12, 38);
            this.Btn1.Name = "Btn1";
            this.Btn1.Size = new System.Drawing.Size(28, 23);
            this.Btn1.TabIndex = 2;
            this.Btn1.TabStop = false;
            this.Btn1.Text = "1";
            this.Btn1.UseVisualStyleBackColor = true;
            this.Btn1.Click += new System.EventHandler(this.Btn1_Click);
            this.Btn1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Btns_KeyDown);
            // 
            // Btn2
            // 
            this.Btn2.Location = new System.Drawing.Point(46, 38);
            this.Btn2.Name = "Btn2";
            this.Btn2.Size = new System.Drawing.Size(28, 23);
            this.Btn2.TabIndex = 3;
            this.Btn2.TabStop = false;
            this.Btn2.Text = "2";
            this.Btn2.UseVisualStyleBackColor = true;
            this.Btn2.Click += new System.EventHandler(this.Btn2_Click);
            this.Btn2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Btns_KeyDown);
            // 
            // Btn3
            // 
            this.Btn3.Location = new System.Drawing.Point(80, 38);
            this.Btn3.Name = "Btn3";
            this.Btn3.Size = new System.Drawing.Size(28, 23);
            this.Btn3.TabIndex = 4;
            this.Btn3.TabStop = false;
            this.Btn3.Text = "3";
            this.Btn3.UseVisualStyleBackColor = true;
            this.Btn3.Click += new System.EventHandler(this.Btn3_Click);
            this.Btn3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Btns_KeyDown);
            // 
            // Btn4
            // 
            this.Btn4.Location = new System.Drawing.Point(114, 38);
            this.Btn4.Name = "Btn4";
            this.Btn4.Size = new System.Drawing.Size(28, 23);
            this.Btn4.TabIndex = 5;
            this.Btn4.TabStop = false;
            this.Btn4.Text = "4";
            this.Btn4.UseVisualStyleBackColor = true;
            this.Btn4.Click += new System.EventHandler(this.Btn4_Click);
            this.Btn4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Btns_KeyDown);
            // 
            // Btn5
            // 
            this.Btn5.Location = new System.Drawing.Point(148, 38);
            this.Btn5.Name = "Btn5";
            this.Btn5.Size = new System.Drawing.Size(28, 23);
            this.Btn5.TabIndex = 6;
            this.Btn5.TabStop = false;
            this.Btn5.Text = "5";
            this.Btn5.UseVisualStyleBackColor = true;
            this.Btn5.Click += new System.EventHandler(this.Btn5_Click);
            this.Btn5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Btns_KeyDown);
            // 
            // BtnDivide
            // 
            this.BtnDivide.Location = new System.Drawing.Point(230, 67);
            this.BtnDivide.Name = "BtnDivide";
            this.BtnDivide.Size = new System.Drawing.Size(42, 23);
            this.BtnDivide.TabIndex = 15;
            this.BtnDivide.TabStop = false;
            this.BtnDivide.Text = "/";
            this.BtnDivide.UseVisualStyleBackColor = true;
            this.BtnDivide.Click += new System.EventHandler(this.BtnDivide_Click);
            this.BtnDivide.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Btns_KeyDown);
            // 
            // BtnMultiply
            // 
            this.BtnMultiply.Location = new System.Drawing.Point(182, 67);
            this.BtnMultiply.Name = "BtnMultiply";
            this.BtnMultiply.Size = new System.Drawing.Size(42, 23);
            this.BtnMultiply.TabIndex = 14;
            this.BtnMultiply.TabStop = false;
            this.BtnMultiply.Text = "*";
            this.BtnMultiply.UseVisualStyleBackColor = true;
            this.BtnMultiply.Click += new System.EventHandler(this.BtnMultiply_Click);
            this.BtnMultiply.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Btns_KeyDown);
            // 
            // BtnSubstract
            // 
            this.BtnSubstract.Location = new System.Drawing.Point(230, 38);
            this.BtnSubstract.Name = "BtnSubstract";
            this.BtnSubstract.Size = new System.Drawing.Size(42, 23);
            this.BtnSubstract.TabIndex = 13;
            this.BtnSubstract.TabStop = false;
            this.BtnSubstract.Text = "-";
            this.BtnSubstract.UseVisualStyleBackColor = true;
            this.BtnSubstract.Click += new System.EventHandler(this.BtnSubstract_Click);
            this.BtnSubstract.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Btns_KeyDown);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(182, 38);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(42, 23);
            this.BtnAdd.TabIndex = 12;
            this.BtnAdd.TabStop = false;
            this.BtnAdd.Text = "+";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            this.BtnAdd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Btns_KeyDown);
            // 
            // BtnEquals
            // 
            this.BtnEquals.Location = new System.Drawing.Point(230, 96);
            this.BtnEquals.Name = "BtnEquals";
            this.BtnEquals.Size = new System.Drawing.Size(41, 52);
            this.BtnEquals.TabIndex = 1;
            this.BtnEquals.TabStop = false;
            this.BtnEquals.Text = "=";
            this.BtnEquals.UseVisualStyleBackColor = true;
            this.BtnEquals.Click += new System.EventHandler(this.BtnEquals_Click);
            this.BtnEquals.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Btns_KeyDown);
            // 
            // Btn6
            // 
            this.Btn6.Location = new System.Drawing.Point(12, 67);
            this.Btn6.Name = "Btn6";
            this.Btn6.Size = new System.Drawing.Size(28, 23);
            this.Btn6.TabIndex = 7;
            this.Btn6.TabStop = false;
            this.Btn6.Text = "6";
            this.Btn6.UseVisualStyleBackColor = true;
            this.Btn6.Click += new System.EventHandler(this.Btn6_Click);
            this.Btn6.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Btns_KeyDown);
            // 
            // Btn7
            // 
            this.Btn7.Location = new System.Drawing.Point(46, 67);
            this.Btn7.Name = "Btn7";
            this.Btn7.Size = new System.Drawing.Size(28, 23);
            this.Btn7.TabIndex = 8;
            this.Btn7.TabStop = false;
            this.Btn7.Text = "7";
            this.Btn7.UseVisualStyleBackColor = true;
            this.Btn7.Click += new System.EventHandler(this.Btn7_Click);
            this.Btn7.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Btns_KeyDown);
            // 
            // Btn8
            // 
            this.Btn8.Location = new System.Drawing.Point(80, 67);
            this.Btn8.Name = "Btn8";
            this.Btn8.Size = new System.Drawing.Size(28, 23);
            this.Btn8.TabIndex = 9;
            this.Btn8.TabStop = false;
            this.Btn8.Text = "8";
            this.Btn8.UseVisualStyleBackColor = true;
            this.Btn8.Click += new System.EventHandler(this.Btn8_Click);
            this.Btn8.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Btns_KeyDown);
            // 
            // Btn9
            // 
            this.Btn9.Location = new System.Drawing.Point(114, 67);
            this.Btn9.Name = "Btn9";
            this.Btn9.Size = new System.Drawing.Size(28, 23);
            this.Btn9.TabIndex = 10;
            this.Btn9.TabStop = false;
            this.Btn9.Text = "9";
            this.Btn9.UseVisualStyleBackColor = true;
            this.Btn9.Click += new System.EventHandler(this.Btn9_Click);
            this.Btn9.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Btns_KeyDown);
            // 
            // Btn0
            // 
            this.Btn0.Location = new System.Drawing.Point(148, 67);
            this.Btn0.Name = "Btn0";
            this.Btn0.Size = new System.Drawing.Size(28, 23);
            this.Btn0.TabIndex = 11;
            this.Btn0.TabStop = false;
            this.Btn0.Text = "0";
            this.Btn0.UseVisualStyleBackColor = true;
            this.Btn0.Click += new System.EventHandler(this.Btn0_Click);
            this.Btn0.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Btns_KeyDown);
            // 
            // BtnBracketL
            // 
            this.BtnBracketL.Location = new System.Drawing.Point(12, 96);
            this.BtnBracketL.Name = "BtnBracketL";
            this.BtnBracketL.Size = new System.Drawing.Size(28, 23);
            this.BtnBracketL.TabIndex = 16;
            this.BtnBracketL.TabStop = false;
            this.BtnBracketL.Text = "(";
            this.BtnBracketL.UseVisualStyleBackColor = true;
            this.BtnBracketL.Click += new System.EventHandler(this.BtnBracketL_Click);
            this.BtnBracketL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Btns_KeyDown);
            // 
            // BtnBracketR
            // 
            this.BtnBracketR.Location = new System.Drawing.Point(46, 96);
            this.BtnBracketR.Name = "BtnBracketR";
            this.BtnBracketR.Size = new System.Drawing.Size(28, 23);
            this.BtnBracketR.TabIndex = 17;
            this.BtnBracketR.TabStop = false;
            this.BtnBracketR.Text = ")";
            this.BtnBracketR.UseVisualStyleBackColor = true;
            this.BtnBracketR.Click += new System.EventHandler(this.BtnBracketR_Click);
            this.BtnBracketR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Btns_KeyDown);
            // 
            // BtnDot
            // 
            this.BtnDot.Location = new System.Drawing.Point(80, 96);
            this.BtnDot.Name = "BtnDot";
            this.BtnDot.Size = new System.Drawing.Size(28, 23);
            this.BtnDot.TabIndex = 18;
            this.BtnDot.TabStop = false;
            this.BtnDot.Text = ".";
            this.BtnDot.UseVisualStyleBackColor = true;
            this.BtnDot.Click += new System.EventHandler(this.BtnDot_Click);
            this.BtnDot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Btns_KeyDown);
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(12, 130);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(41, 13);
            this.status.TabIndex = 19;
            this.status.Text = "Ready.";
            // 
            // BtnNeg
            // 
            this.BtnNeg.Location = new System.Drawing.Point(114, 96);
            this.BtnNeg.Name = "BtnNeg";
            this.BtnNeg.Size = new System.Drawing.Size(62, 23);
            this.BtnNeg.TabIndex = 19;
            this.BtnNeg.TabStop = false;
            this.BtnNeg.Text = "NEG";
            this.BtnNeg.UseVisualStyleBackColor = true;
            this.BtnNeg.Click += new System.EventHandler(this.BtnNeg_Click);
            this.BtnNeg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Btns_KeyDown);
            // 
            // BtnDel
            // 
            this.BtnDel.Location = new System.Drawing.Point(182, 96);
            this.BtnDel.Name = "BtnDel";
            this.BtnDel.Size = new System.Drawing.Size(42, 52);
            this.BtnDel.TabIndex = 20;
            this.BtnDel.TabStop = false;
            this.BtnDel.Text = "DEL";
            this.BtnDel.UseVisualStyleBackColor = true;
            this.BtnDel.Click += new System.EventHandler(this.BtnDel_Click);
            this.BtnDel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Btns_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 156);
            this.Controls.Add(this.BtnDel);
            this.Controls.Add(this.BtnNeg);
            this.Controls.Add(this.status);
            this.Controls.Add(this.BtnDot);
            this.Controls.Add(this.BtnBracketR);
            this.Controls.Add(this.BtnBracketL);
            this.Controls.Add(this.Btn0);
            this.Controls.Add(this.Btn9);
            this.Controls.Add(this.Btn8);
            this.Controls.Add(this.Btn7);
            this.Controls.Add(this.Btn6);
            this.Controls.Add(this.BtnEquals);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.BtnSubstract);
            this.Controls.Add(this.BtnMultiply);
            this.Controls.Add(this.BtnDivide);
            this.Controls.Add(this.Btn5);
            this.Controls.Add(this.Btn4);
            this.Controls.Add(this.Btn3);
            this.Controls.Add(this.Btn2);
            this.Controls.Add(this.Btn1);
            this.Controls.Add(this.screen);
            this.Name = "Form1";
            this.Text = "Calculator";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox screen;
        private System.Windows.Forms.Button Btn1;
        private System.Windows.Forms.Button Btn2;
        private System.Windows.Forms.Button Btn3;
        private System.Windows.Forms.Button Btn4;
        private System.Windows.Forms.Button Btn5;
        private System.Windows.Forms.Button BtnDivide;
        private System.Windows.Forms.Button BtnMultiply;
        private System.Windows.Forms.Button BtnSubstract;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnEquals;
        private System.Windows.Forms.Button Btn6;
        private System.Windows.Forms.Button Btn7;
        private System.Windows.Forms.Button Btn8;
        private System.Windows.Forms.Button Btn9;
        private System.Windows.Forms.Button Btn0;
        private System.Windows.Forms.Button BtnBracketL;
        private System.Windows.Forms.Button BtnBracketR;
        private System.Windows.Forms.Button BtnDot;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Button BtnNeg;
        private System.Windows.Forms.Button BtnDel;
    }
}

