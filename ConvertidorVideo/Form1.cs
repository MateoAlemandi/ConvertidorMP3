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
            progressBarDescargar.Visible = false;
            
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
                
                using (var saveDialog = new SaveFileDialog())
                {

                    var youtube = new YoutubeClient();
                    var video = await youtube.Videos.GetAsync(videoUrl);

                    var streamManifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);
                    var audioStreamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();

                    string audioDownloadUrl = audioStreamInfo.Url;

                    saveDialog.FileName = $"{video.Title}.mp3";
                    saveDialog.Filter = "Archivos MP3 (*.mp3)|*.mp3";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        string savePath = saveDialog.FileName;

                        // Utilizamos Task.Run para realizar la descarga en un hilo separado
                        await Task.Run(() => {

                            using (WebClient webClient = new WebClient())
                            {
                                webClient.DownloadFile(audioDownloadUrl, savePath);
                            }

                        });


                        // Actualizamos la barra de progreso desde el hilo principal
                        progressBarDescargar.Invoke(new Action(() =>
                        {
                            progressBarDescargar.Visible = true;
                            progressBarDescargar.Value = progressBarDescargar.Maximum;

                        }));
                        
                        TodoenCero();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                MessageBox.Show("Descarga completada.");
            }

        }

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int version = 1;
            Info inforFormulario = new Info(version);
            inforFormulario.ShowDialog();
        }

        private void TodoenCero()
        {
            VideoUrlTextBox.Text = "";
            progressBarDescargar.Visible = false;
        }
        
    }

}

