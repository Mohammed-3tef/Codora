using System;
using System.Drawing;
using System.Windows.Forms;

public class ImageDownloader
{
    private PictureBox pictureBox;

    public ImageDownloader(PictureBox pictureBox)
    {
        this.pictureBox = pictureBox ?? throw new ArgumentNullException(nameof(pictureBox));
    }

    /// <summary>
    /// يفتح حوار الحفظ ويحفظ الصورة الموجودة في PictureBox
    /// </summary>
    public void SaveImage()
    {
        if (pictureBox.Image == null)
        {
            ArabicMessageDisplay.ShowWarning("لا توجد صورة لحفظها!");
            return;
        }

        using (SaveFileDialog saveDialog = new SaveFileDialog())
        {
            saveDialog.Title = "اختر مكان الحفظ";
            saveDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
            saveDialog.DefaultExt = "png";
            saveDialog.AddExtension = true;

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox.Image.Save(saveDialog.FileName);
                    ArabicMessageDisplay.ShowSuccess("تم الحفظ بنجاح");
                }
                catch (Exception ex)
                {
                    ArabicMessageDisplay.ShowError($"خطأ أثناء الحفظ:\n{ex.Message}");
                }
            }
        }
    }
}