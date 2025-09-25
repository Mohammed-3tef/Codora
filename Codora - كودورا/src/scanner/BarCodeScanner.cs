using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Drawing;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;

public class BarCodeScanner
{
    private FilterInfoCollection videoDevices;
    private VideoCaptureDevice videoSource;

    private event Action<string> BarCodeDetected;  // للـ text
    private event Action<Bitmap> FrameArrived;    // للفريمات للعرض

    private PictureBox pictureBox;
    private TextBox resultBox;

    /// <summary>
    /// Creates a new instance of <see cref="BarCodeScanner"/>.
    /// Initializes the default video device and attaches the frame event handler.
    /// </summary>
    public BarCodeScanner()
    {
        videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        if (videoDevices.Count == 0)
            throw new Exception("لا يوجد كاميرا متصلة!");
        videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
        videoSource.NewFrame += VideoSource_NewFrame;
    }

    /// <summary>
    /// Initializes the scanner UI bindings.
    /// Binds a PictureBox for video frames and a TextBox for detected barcode text.
    /// </summary>
    public void Initialize(PictureBox pictureBox, TextBox resultBox)
    {
        this.pictureBox = pictureBox;
        this.resultBox = resultBox;

        BarCodeDetected += (text) =>
        {
            if (this.resultBox != null)
            {
                if (this.resultBox.InvokeRequired)
                    this.resultBox.Invoke(new Action(() => this.resultBox.Text = text));
                else
                    this.resultBox.Text = text;
            }
        };

        FrameArrived += (frame) =>
        {
            if (this.pictureBox != null)
            {
                if (this.pictureBox.InvokeRequired)
                {
                    this.pictureBox.Invoke(new Action(() =>
                    {
                        this.pictureBox.Image?.Dispose();
                        this.pictureBox.Image = frame;
                    }));
                }
                else
                {
                    this.pictureBox.Image?.Dispose();
                    this.pictureBox.Image = frame;
                }
            }
        };
    }

    /// <summary>
    /// Starts the camera if not running.
    /// </summary>
    public void Start()
    {
        if (!videoSource.IsRunning)
            videoSource.Start();
    }

    /// <summary>
    /// Stops the camera if running.
    /// </summary>
    public void Stop()
    {
        if (videoSource.IsRunning)
            videoSource.SignalToStop();
    }

    /// <summary>
    /// Handles new video frames, tries to decode barcode.
    /// </summary>
    private void VideoSource_NewFrame(object sender, NewFrameEventArgs e)
    {
        try
        {
            Bitmap bitmap = (Bitmap)e.Frame.Clone();

            // ارسال نسخة للعرض
            FrameArrived?.Invoke((Bitmap)bitmap.Clone());

            // قراءة BarCode (CODE128, EAN13, QR وغيرها)
            BarcodeReader reader = new BarcodeReader
            {
                AutoRotate = true,
                Options = new DecodingOptions { TryHarder = true }
            };
            var result = reader.Decode(bitmap);
            if (result != null)
            {
                BarCodeDetected?.Invoke(result.Text);
            }

            bitmap.Dispose();
        }
        catch { }
    }

    /// <summary>
    /// Generates a 1D BarCode image from a given text string.
    /// Default format: CODE_128
    /// </summary>
    /// <param name="text">Text to encode as barcode</param>
    /// <param name="width">Image width</param>
    /// <param name="height">Image height</param>
    /// <param name="format">Barcode format (default CODE_128)</param>
    /// <returns>Bitmap of the barcode</returns>
    public static Bitmap GenerateBarCode(string text, int width = 400, int height = 150, BarcodeFormat format = BarcodeFormat.CODE_128)
    {
        var writer = new BarcodeWriterPixelData
        {
            Format = format,
            Options = new EncodingOptions
            {
                Width = width,
                Height = height,
                Margin = 10,
                PureBarcode = true
            }
        };

        var pixelData = writer.Write(text);

        using (var bmp = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
        {
            var bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                System.Drawing.Imaging.ImageLockMode.WriteOnly, bmp.PixelFormat);

            try
            {
                System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
            }
            finally
            {
                bmp.UnlockBits(bitmapData);
            }

            return new Bitmap(bmp);
        }
    }
}
