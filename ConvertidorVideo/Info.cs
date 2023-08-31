using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertidorVideo
{
    public partial class Info : Form
    {
        int Infor;
        public Info(int InfoR)
        {
            InitializeComponent();
            this.Infor = InfoR;
            //this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Info_Load(object sender, EventArgs e)
        {
            string version = Application.ProductVersion;
            labelVersion.Text = "Versión: " + version;
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.instagram.com/mateo_alemandi/");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.linkedin.com/in/mateo-alemandi/");
        }
    }
}
