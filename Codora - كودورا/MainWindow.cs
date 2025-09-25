using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Codora___كودورا
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Text = AppSettings.AppName;
            this.Icon = AppSettings.AppIcon;
            this.FormClosing += AppSettings.FormClosing;
        }

        private void TitleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.TitleTextBox.Text))
            {
                this.ShowCodePictureBox.Image = null;
                return;
            }

            QRcodeCheckBox_CheckedChanged(sender, e);
            DataMatrixCheckBox_CheckedChanged(sender, e);
            BarcodeCheckBox_CheckedChanged(sender, e);
            AztecCheckBox_CheckedChanged(sender, e);
        }

        private void QRcodeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.QRcodeCheckBox.Checked && !String.IsNullOrWhiteSpace(this.TitleTextBox.Text))
                this.ShowCodePictureBox.Image = QRCodeScanner.GenerateQRCode(this.TitleTextBox.Text.Trim(), Math.Min(this.ShowCodePictureBox.Width, this.ShowCodePictureBox.Height));
        }

        private void DataMatrixCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.DataMatrixCheckBox.Checked && !String.IsNullOrWhiteSpace(this.TitleTextBox.Text))
                this.ShowCodePictureBox.Image = DataMatrixScanner.GenerateDataMatrix(this.TitleTextBox.Text.Trim(), Math.Min(this.ShowCodePictureBox.Width, this.ShowCodePictureBox.Height));
        }

        private void BarcodeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.BarcodeCheckBox.Checked && !String.IsNullOrWhiteSpace(this.TitleTextBox.Text))
                this.ShowCodePictureBox.Image = BarCodeScanner.GenerateBarCode(this.TitleTextBox.Text.Trim(), this.ShowCodePictureBox.Width, this.ShowCodePictureBox.Width /2);
        }

        private void AztecCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.AztecCheckBox.Checked && !String.IsNullOrWhiteSpace(this.TitleTextBox.Text))
                this.ShowCodePictureBox.Image = AztecScanner.GenerateAztec(this.TitleTextBox.Text.Trim(), Math.Min(this.ShowCodePictureBox.Width, this.ShowCodePictureBox.Height));
        }

        private void DownloadBtn_Click(object sender, EventArgs e)
        {
            ImageDownloader imageDownloader = new ImageDownloader(this.ShowCodePictureBox);
            imageDownloader.SaveImage();
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            FormSettings.CleanFields(this);
        }

        private void MainWindow_SizeChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.TitleTextBox.Text))
            {
                this.ShowCodePictureBox.Image = null;
                return;
            }

            QRcodeCheckBox_CheckedChanged(sender, e);
            DataMatrixCheckBox_CheckedChanged(sender, e);
            BarcodeCheckBox_CheckedChanged(sender, e);
            AztecCheckBox_CheckedChanged(sender, e);
        }
    }
}
