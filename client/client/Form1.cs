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

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text;
            string password = textBox2.Text;
            string[] text = textBox2.Text.Split(' ');
            

            const int port = 8888;
            const string address = "192.168.0.107";
            string userExp = textBox6.Text;
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
            string userName = textBox1.Text;
            string password = textBox2.Text;
            const int port = 8888;
            const string address = "192.168.0.107";
            TcpClient client = null;
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();
                string userExp = textBox6.Text;
                if(userExp != "")
                {
                    user = new UserSessionData();
                    user.UserName = userName;
                    user.Password = password;
                    api = InstaApiBuilder.CreateBuilder()
                            .SetUser(user)
                            .UseLogger(new DebugLogger(LogLevel.Exceptions))
                            //.SetRequestDelay(TimeSpan.FromSeconds(1))
                            .Build();
                    var loginRequest = await api.LoginAsync();

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

        private void button3_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text;
            string password = textBox2.Text;
            const int port = 8888;
            const string address = "192.168.0.107";
            TcpClient client = null;
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();
                string userExp = textBox6.Text;
                byte[] userExpData = Encoding.Unicode.GetBytes(userExp);
                stream.Write(userExpData, 0, userExpData.Length);

                if(userExp != "")
                {
                    Like(userExp, userName, password);
                    label7.Text = "Done";
                }
                else
                {
                    label7.Text = "Enter username"; 
                }
                

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



        public static async void Like(string userExp, string userName, string password)
        {
            user = new UserSessionData();
            user.UserName = userName;
            user.Password = password;

            api = InstaApiBuilder.CreateBuilder()
                    .SetUser(user)
                    .UseLogger(new DebugLogger(LogLevel.Exceptions))
                    //.SetRequestDelay(TimeSpan.FromSeconds(1))
                    .Build();
            var loginRequest = await api.LoginAsync();

            IResult<InstaUser> userSearch = await api.GetUserAsync(userExp);
            IResult<InstaMediaList> media = await api.GetUserMediaAsync(userExp, PaginationParameters.MaxPagesToLoad(5));
            var mediaList = media.Value;
            int count_mediaList = mediaList.ToArray().Length;
            for (int i = 0; i < count_mediaList; i++)
            {
                var res = await api.LikeMediaAsync(mediaList[i].InstaIdentifier);
                string result = res.Succeeded.ToString();
            }


        }

        public static async void Info(string userExp, string userName, string password)
        {
            
        }

        public static async void Subs(string userExp, string userName, string password)
        {
            user = new UserSessionData();
            user.UserName = userName;
            user.Password = password;

            api = InstaApiBuilder.CreateBuilder()
                    .SetUser(user)
                    .UseLogger(new DebugLogger(LogLevel.Exceptions))
                    //.SetRequestDelay(TimeSpan.FromSeconds(1))
                    .Build();
            var loginRequest = await api.LoginAsync();

            IResult<InstaUser> userSearch = await api.GetUserAsync(userExp);

            IResult<InstaUserShortList> followers = await api.GetUserFollowersAsync(userSearch.Value.UserName, PaginationParameters.MaxPagesToLoad(5));
            var followlist = followers.Value;
            int count_followlist = followlist.ToArray().Length;
            for(int i = 0; i < count_followlist; i++)
            {
                var res = await api.FollowUserAsync(followlist[i].Pk);
                string result = res.Succeeded.ToString();
            }

            
                
            
            

        }


        private void button5_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text;
            string password = textBox2.Text;
            const int port = 8888;
            const string address = "192.168.0.107";
            TcpClient client = null;
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();
                string userExp = textBox6.Text;
                byte[] userExpData = Encoding.Unicode.GetBytes(userExp);
                stream.Write(userExpData, 0, userExpData.Length);

                if (userExp != "")
                {
                    Subs(userExp, userName, password);
                    label6.Text = "Done";
                }
                else
                {
                    label6.Text = "Enter username";
                }

            }
            catch (Exception ex)
            {
                label6.Text = ex.Message;
            }
            finally
            {
                if (client != null)
                    client.Close();
            }

        }
    }

}

