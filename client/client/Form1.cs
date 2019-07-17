using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using InstaSharper;
using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.API.Processors;
using InstaSharper.API.UriCreators;
using InstaSharper.Classes;
using InstaSharper.Converters;
using InstaSharper.Helpers;
using InstaSharper.Logger;
using InstaSharper.Classes.Models;
using System.Windows.Forms;

namespace client
{
    public partial class Form1 : Form
    {
        #region Hidden
        #endregion
        private static UserSessionData user;
        private static IInstaApi api;
        public Form1()
        {
            InitializeComponent();
        }

        public async static void Login()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            string[] text = textBox2.Text.Split(' ');
            string userName = textBox1.Text;
            string password = textBox2.Text;

            const int port = 8888;
            const string address = "192.168.0.111";
            TcpClient client = null;
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();
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
                label3.Text = message;
            }
            catch (Exception ex)
            {
                label3.Text = ex.Message;
            }
            finally
            {
                if (client != null)
                    client.Close();
            }

        }



        private async void button4_Click(object sender, EventArgs e)
        {
            const int port = 8888;
            const string address = "192.168.0.111";
            TcpClient client = null;
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();
                string userExp = textBox6.Text;
                if(userExp != "")
                {
                    

                    label5.Text = "Done";

                }
                else
                {
                    client.Close();
                }
                
            }
            catch (Exception ex)
            {
                label5.Text = ex.Message;
            }
            finally
            {
                if (client != null)
                    client.Close();
            }



        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            const int port = 8888;
            const string address = "192.168.0.111";
            TcpClient client = null;
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();
                string userExp = textBox6.Text;
                byte[] userExpData = Encoding.Unicode.GetBytes(userExp);
                stream.Write(userExpData, 0, userExpData.Length);

                user = new UserSessionData();

                Like(userExp);
                label7.Text = "Done";

            }
            catch (Exception ex)
            {
                label7.Text = ex.Message;
            }
            finally
            {
                if (client != null)
                    client.Close();
            }

            
            }



        public static async void Like(string userExp)
        {
            
            api = InstaApiBuilder.CreateBuilder()
                    .SetUser(user)
                    .UseLogger(new DebugLogger(LogLevel.Exceptions))
                    //.SetRequestDelay(TimeSpan.FromSeconds(1))
                    .Build();

            IResult<InstaMediaList> media = await api.GetUserMediaAsync(userExp, PaginationParameters.MaxPagesToLoad(5));
            List<InstaMedia> mediaList = media.Value.ToList();
            await api.LikeMediaAsync(mediaList[4].InstaIdentifier);

        }
    }

}

