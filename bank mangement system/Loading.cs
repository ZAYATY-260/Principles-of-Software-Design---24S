﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bank_mangement_system
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }


        int startP = 0; 
        private void Timer1_Tick(object sender, EventArgs e)
        {
            startP += 1;
            progressBar1.Value = startP;
            if (progressBar1.Value == 100)
            {
                progressBar1.Value = 0;
                Timer1.Stop();
                Login Obj = new Login();
                Obj.Show();
                this.Hide();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Timer1.Start();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
