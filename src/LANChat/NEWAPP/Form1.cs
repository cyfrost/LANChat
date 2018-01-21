/*

The MIT License (MIT)
---------------------

LAN Chat - Simple LAN Chat program using TCP/IP

Copyright © 2018 cyfrost <cyrus.frost@hotmail.com>

Permission is hereby granted, free of charge, to any person obtaining 
a copy of this software and associated documentation files (the "Software"),
to deal in the Software without restriction, including without limitation
the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies
or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
FOR AND CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
DEALINGS IN THE SOFTWARE.

*/


using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;

namespace NEWAPP
{
    public partial class Form1 : Form
    {
        public string appname = Assembly.GetExecutingAssembly().GetName().Name;
        public string copyright = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false).Cast<AssemblyCopyrightAttribute>().FirstOrDefault().Copyright;
        public string desc = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false).Cast<AssemblyDescriptionAttribute>().FirstOrDefault().Description;
        public string version = Assembly.GetExecutingAssembly().GetName().Version.ToString() + " (build " + DateTime.Now.ToString("Myy") + ")";




        Socket sck;
        string myname;


        EndPoint eplocal, epremote;

        public Form1()
        {
            InitializeComponent();
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            textBox1.KeyPress += new KeyPressEventHandler(checkenterforconnection);
            textBox2.KeyPress += new KeyPressEventHandler(checkenterforconnection);
            textBox3.KeyPress += new KeyPressEventHandler(checkenterforconnection);
            textBox4.KeyPress += new KeyPressEventHandler(checkenterforconnection);
            textBox6.KeyPress += new KeyPressEventHandler(checkenterforconnection);
            textBox5.KeyPress += new KeyPressEventHandler(checkenterforsending);
            textBox1.Text = getIPV4();

            
        }


        public string getListeningIP(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    IPInterfaceProperties adapterProperties = item.GetIPProperties();

                    if (adapterProperties.GatewayAddresses.FirstOrDefault() != null)
                    {
                        foreach (UnicastIPAddressInformation ip in adapterProperties.UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                output = ip.Address.ToString();
                            }
                        }
                    }
                }
            }

            return output;
        }



        public string getIPV4()
        {

            //FIX ME: i can only tell between ethernet and wireless but not loopback etc
            //returns false = adapter is wireless
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet && getListeningIP(NetworkInterfaceType.Ethernet).Length >= 1)
                {


                    return getListeningIP(NetworkInterfaceType.Ethernet);

                }

                else if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 && getListeningIP(NetworkInterfaceType.Wireless80211).Length >= 1)
                {

                    return getListeningIP(NetworkInterfaceType.Wireless80211);
                }



            }

            return null;


        }



        private void checkenterforconnection(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)13)
                button1.PerformClick();

        }
        private void checkenterforsending(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)13)
                button2.PerformClick();

        }

     

        private void closeall(IAsyncResult result)
        {
            sck.EndReceiveFrom(result, ref epremote);
            sck.Close();
        }

        private void MessageCallBack(IAsyncResult aresult)
        {

            try
            {
                int size = sck.EndReceiveFrom(aresult, ref epremote);
                if (size > 0)
                {

                    byte[] receiveddata = new byte[1464];
                    receiveddata = (byte[])aresult.AsyncState;
                    ASCIIEncoding eencoding = new ASCIIEncoding();
                    string receivedmessage = eencoding.GetString(receiveddata);
                    listBox1.Items.Add(receivedmessage);

                }
                byte[] buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epremote, new AsyncCallback(MessageCallBack), buffer);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Runtime Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox6.Text))
                {

                    MessageBox.Show("Please enter a chat name to proceed.", "Runtime Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                myname = textBox6.Text;
                eplocal = new IPEndPoint(IPAddress.Parse(textBox1.Text), Convert.ToInt32(textBox2.Text));
                sck.Bind(eplocal);
                epremote= new IPEndPoint(IPAddress.Parse(textBox3.Text), Convert.ToInt32(textBox4.Text));
                sck.Connect(epremote);
                byte[] buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epremote, new AsyncCallback(MessageCallBack), buffer);
                button1.Text = "Connected.";
                button1.Enabled = false;
                    listBox1.Items.Add("------------------------------------------------------------------------------------");

                    listBox1.Items.Add("###  Connection Started. Listening on " + textBox3.Text + "::" + textBox4.Text + "    ###");
                listBox1.Items.Add("------------------------------------------------------------------------------------");
                               listBox1.Items.Add("");
                    quickchat.Enabled = true;
                    listBox1.BackColor = Color.White;
                    quickconnect.Enabled = false;
                    this.Height = 514;
                    textBox5.Focus();
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Runtime Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                string wholemessage = "(" + DateTime.Now.Hour + ":" +  DateTime.Now.Minute + ":" + DateTime.Now.Second +  ")   " +   myname + " - " + textBox5.Text;
                string mymessage = "(" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ")   " +  "You - " + textBox5.Text;
                byte[] msg = new byte[1500];
                msg = enc.GetBytes(wholemessage);
                sck.Send(msg);
                listBox1.Items.Add(mymessage);
                textBox5.Clear();

 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(),"Runtime Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Text = textBox1.Text;

            textBox6.Text = "Client 2";
            textBox2.Text = "81";
            textBox4.Text = "80";
        }

        private void connectionInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 b = new Form2();
            b.Show();

        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new LAN_Chat.About().ShowDialog();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox3.Text))
                {

                    MessageBox.Show("No Connections Established.", "Runtime Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                   
                    AsyncCallback b =  new AsyncCallback(closeall);

                   
                                                   
                    button1.Text = "CONNECT";
                    button1.Enabled = true;
                    listBox1.Items.Add("------------------------------------------------------------------------------------");
                    listBox1.Items.Add("###  Disconnected from " + textBox3.Text + "::" + textBox4.Text + "    ###");
                    listBox1.Items.Add("------------------------------------------------------------------------------------");
                    listBox1.Items.Add("");
                    quickchat.Enabled = false;
                    listBox1.BackColor = SystemColors.Control;
                    quickconnect.Enabled = true;
                    textBox1.Focus();
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Runtime Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void exportChatToolStripMenuItem_Click(object sender, EventArgs e)
        {

 string spath = Environment.SpecialFolder.Desktop.ToString();
            MessageBox.Show(spath);

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);

        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {

                if (MessageBox.Show("Do you want to exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    Application.Exit();
                    return true;
                }
                
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.BackColor = SystemColors.Control;


            quickchat.Enabled = false;
            textBox6.Text = Environment.UserName;
            textBox3.BringToFront();
            textBox3.Focus();
           




         }
    }
}
