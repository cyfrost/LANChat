using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NEWAPP;


namespace LAN_Chat
{
    public partial class About : Form
    {

        Form1 x = new Form1();



        public About()
        {
            InitializeComponent();
            appname.Text = x.appname;
            build.Text = "version " + x.version;
            author.Text = x.copyright;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "About";
         //   this.Size = new System.Drawing.Size(310, 135);
        }

        private void About_Load(object sender, EventArgs e)
        {

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
