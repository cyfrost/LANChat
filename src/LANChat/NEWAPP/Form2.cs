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
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

using System.Windows.Forms;

namespace NEWAPP
{
    public partial class Form2 : Form
    {
        public Form2()
        {

            InitializeComponent();

            string url = "http://checkip.dyndns.org";
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            string a4 = a3[0];
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                   listBox1.Items.Add("Local IP Address: " + ip.ToString());
            }
            listBox1.Items.Add("Your Public IP Address: " + a4.TrimEnd());
            listBox1.Items.Add("");
            listBox1.Items.Add("Listing All Network Interfaces on: " + Environment.MachineName.ToUpper());
            listBox1.Items.Add("---------------------------------------------------------------------");
            foreach(NetworkInterface x in NetworkInterface.GetAllNetworkInterfaces()) {
                if (x.NetworkInterfaceType == NetworkInterfaceType.Ethernet || x.NetworkInterfaceType == NetworkInterfaceType.Ethernet3Megabit || x.NetworkInterfaceType == NetworkInterfaceType.FastEthernetFx || x.NetworkInterfaceType == NetworkInterfaceType.FastEthernetT || x.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet || x.NetworkInterfaceType == NetworkInterfaceType.Wireless80211) {

                    foreach (UnicastIPAddressInformation ip in x.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            listBox1.Items.Add(x.Description + " - " + ip.Address.ToString() + "  (" + x.Name + ")");
                        }


                    }

               
                }
            }
            listBox1.Items.Add("---------------------------------------------------------------------");







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

    
        private void Form2_Load(object sender, EventArgs e)
        {

            MessageBox.Show("To work properly, the local IP address of either Ethernet or Wi-Fi adapter (which ever is present) is required. other network interfaces maybe virtual adapters and can cause connection issues.\n\n In case you are experiencing problems while connecting, please verify current settings using ncpa.cpl command", "Context", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }
    }
}
