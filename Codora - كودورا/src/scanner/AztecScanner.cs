using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Drawing;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;

public class AztecScanner
{
    private FilterInfoCollection videoDevices;
    private VideoCaptureDevice videoSource;

    private event Action<string> AztecDetected;   // النص المستخرج
    private event Action<Bitmap> FrameArrived;   // الفريمات للعرض

    private PictureBox pictureBox;
    private TextBox resultBox;

    /// <summary>
    /// Creates a new instance of <see cref="AztecScanner"/>.
    /// Initializes the default video device and attaches the frame event handler.
    /// </summary>
    public AztecScanner()
    {
        videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        if (videoDevices.Count == 0)
            throw new Exception("لا يوجد كاميرا متصلة!");
        videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
        videoSource.NewFrame += VideoSource_NewFrame;
    }

    /// <summary>
    /// Initializes the scanner UI bindings.
    /// Binds a <see cref="PictureBox"/> for displaying video frames
    /// and a <see cref="TextBox"/> for showing detected Aztec text.
    /// </summary>
    public void Initialize(PictureBox pictureBox, TextBox resultBox)
    {
        this.pictureBox = pictureBox;
        this.resultBox = resultBox;

        // ربط الأحداث
        AztecDetected += (text) =>
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
    /// Starts the camera if it is not already running.
    /// </summary>
    public void Start()
    {
        if (!videoSource.IsRunning)
            videoSource.Start();
    }

    /// <summary>
    /// Stops the camera if it is currently running.
    /// </summary>
    public void Stop()
    {
        if (videoSource.IsRunning)
            videoSource.SignalToStop();
    }

    /// <summary>
    /// Event handler for new video frames.
    /// Sends a cloned frame to <see cref="FrameArrived"/> 
    /// and attempts to decode Aztec codes from the frame.
    /// If decoded, invokes <see cref="AztecDetected"/>.
    /// </summary>
    private void VideoSource_NewFrame(object sender, NewFrameEventArgs e)
    {
        try
        {
            Bitmap bitmap = (Bitmap)e.Frame.Clone();

            // ارسال نسخة للعرض
            FrameArrived?.Invoke((Bitmap)bitmap.Clone());

            // قراءة Aztec
            BarcodeReader reader = new BarcodeReader
            {
                Options = new DecodingOptions
                {
                    PossibleFormats = new[] { BarcodeFormat.AZTEC }
                }
            };

            var result = reader.Decode(bitmap);
            if (result != null)
            {
                AztecDetected?.Invoke(result.Text);
            }

            bitmap.Dispose();
        }
        catch { }
    }

    /// <summary>
    /// Generates an Aztec code image from a given text string.
    /// </summary>
    public static Bitmap GenerateAztec(string text, int width = 600)
    {
        var writer = new BarcodeWriterPixelData
        {
            Format = BarcodeFormat.AZTEC,
            Options = new EncodingOptions
            {
                Width = width,
                Height = width,
                Margin = 2
            }
        };

        var pixelData = writer.Write(text);

        using (var bmp = new Bitmap(pixelData.Width, pixelData.Height,
                   System.Drawing.Imaging.PixelFormat.Format32bppArgb))
        {
            var bitmapData = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                System.Drawing.Imaging.ImageLockMode.WriteOnly,
                bmp.PixelFormat);

            try
            {
                System.Runtime.InteropServices.Marshal.Copy(
                    pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
            }
            finally
            {
                bmp.UnlockBits(bitmapData);
            }

            return new Bitmap(bmp);
        }
    }
}
