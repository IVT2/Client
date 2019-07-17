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
        private static UserSessionData user;
        private static IInstaApi api;
        public Form1()
        {
            InitializeComponent();
        }
      
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text;
            string password = textBox2.Text;

            const int port = 8888;
            const string address = "127.0.0.1";
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

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void button4_Click(object sender, EventArgs e)
        {
            //PullUserPosts("ivt22222");
            //async void PullUserPosts(string userToScrape)
            //{
            //    IResult<InstaUser> userSearch = await api.GetUserAsync(userToScrape);
            //    IResult<InstaMediaList> media = await api.GetUserMediaAsync(userToScrape, PaginationParameters.MaxPagesToLoad(5));
            //    List<InstaMedia> mediaList = media.Value.ToList();
            //    for (int i = 0; i < mediaList.Count; i++)
            //    {
            //        InstaMedia m = mediaList[i];
            //        if (m != null && m.Caption != null)
            //        {
            //            string captionText = m.Caption.Text;
            //            if (captionText != null)
            //            {
            //                if (m.MediaType == InstaMediaType.Image)
            //                {
            //                    for (int x = 0; x < m.Images.Count; x++)
            //                    {
            //                        if (m.Images[x] != null && m.Images[x].URI != null)
            //                        {
            //                           listBox1.Text = captionText;
            //                            string uri = m.Images[x].URI;
            //                            listBox1.Text = uri;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            const int port = 8888;
            const string address = "127.0.0.1";
            TcpClient client = null;

            client = new TcpClient(address, port);
            NetworkStream stream = client.GetStream();
            string expUser = textBox6.Text;

            byte[] expUserData = Encoding.Unicode.GetBytes(expUser);
            stream.Write(expUserData, 0, expUserData.Length);



            //IResult<InstaMediaList> media = await api.GetUserMediaAsync(textBox6.Text, PaginationParameters.MaxPagesToLoad(5));
            //List<InstaMedia> mediaList = media.Value.ToList();
            //await api.LikeMediaAsync(mediaList[4].InstaIdentifier);
        }
    }
}
