using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Videos;
using System.Web;


namespace ConvertidorVideo
{
    public partial class Form1 : Form
    {
        private readonly YoutubeClient youtubeClient;

        public Form1()
        {
            InitializeComponent();
            youtubeClient = new YoutubeClient();
            
        }

        private async void btnDescargar_Click(object sender, EventArgs e)
        {
            string videoUrl = VideoUrlTextBox.Text.Trim();

            if (string.IsNullOrEmpty(videoUrl))
            {
                MessageBox.Show("Ingrese un enlace de video válido.");
                return;
            }

            try
            {
                var youtube = new YoutubeClient();
                var video = await youtube.Videos.GetAsync(videoUrl);

                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);
                var audioStreamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();

                string audioDownloadUrl = audioStreamInfo.Url;

                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.FileName = $"{video.Title}.mp3";
                    saveDialog.Filter = "Archivos MP3 (*.mp3)|*.mp3";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        string savePath = saveDialog.FileName;

                        using (WebClient webClient = new WebClient())
                        {
                            webClient.DownloadFile(audioDownloadUrl, savePath);
                        }

                        MessageBox.Show("Descarga completada.");
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int version = 1;
            Info inforFormulario = new Info(version);
            inforFormulario.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }

}

