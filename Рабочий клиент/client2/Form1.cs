using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;



namespace client2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] text = textBox2.Text.Split(' ');
            label3.Text = textBox1.Text.ToString();
            label4.Text = textBox2.Text.ToString();
            string userName = label3.Text;
            String password = label4.Text;

            const int port = 8888;
            const string address = "192.168.0.111";
                TcpClient client = null;
                try
                {
                    client = new TcpClient(address, port);
                    NetworkStream stream = client.GetStream();
                int i = 0;
                    while (i<1)
                    {
                        
                        string message = String.Format("{0}, {1}", userName, password);

                        byte[] userData = Encoding.Unicode.GetBytes(userName);
                        byte[] passwordData = Encoding.Unicode.GetBytes(password);
                        stream.Write(userData, 0, userData.Length);
                        stream.Write(passwordData, 0, passwordData.Length);
                       byte[] data = new byte[64]; 
                        StringBuilder builder = new StringBuilder();
                        int bytes = 0;
                        do
                        {
                            bytes = stream.Read(data, 0, data.Length);
                            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        }
                        while (stream.DataAvailable);

                        message = builder.ToString();
                    i++;
                    }
                }
                catch (Exception ex)
                {
                    
                }
               
            }


    }
}
