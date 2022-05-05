using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace CryptoUtility;

internal class WebCamAforge : IWebCam
{
    private FilterInfoCollection filterinfocollection;
    private VideoCaptureDevice videocapturedevice;
    private IVideoSource videoSource;

    //private Image captured_image;
    // private int frames;
    //private bool count_frames = false;
    public PictureBox Container { get; set; }

    public string[] VideoDevices()
    {
        var list = new List<string>();
        filterinfocollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        foreach (FilterInfo filterInfo in filterinfocollection)
            list.Add(filterInfo.Name);
        return list.ToArray();
    }

    public void OpenDevice(int deviceIndex = 0)
    {
        if (filterinfocollection == null)
            filterinfocollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        videocapturedevice = new VideoCaptureDevice(filterinfocollection[deviceIndex].MonikerString);
        OpenConnection(videocapturedevice);
    }

    public void OpenStream(string url, IWebCam.JPEGType jpType, string user = "", string password = "")
    {
        var MJPEGvideoSource = new MJPEGStream(url);
        var JPEGvideoSource = new JPEGStream(url);
        if (jpType == IWebCam.JPEGType.Jpeg)
        {
            JPEGvideoSource.Login = user;
            JPEGvideoSource.Password = password;
            OpenConnection(JPEGvideoSource);
        }
        else
        {
            MJPEGvideoSource.Login = user;
            MJPEGvideoSource.Password = password;
            OpenConnection(MJPEGvideoSource);
        }
    }

    public void OpenAXIS(string IP, string user = "", string password = "")
    {
        OpenStream("http://" + IP + "/axis-cgi/mjpg/video.cgi", IWebCam.JPEGType.MJpeg, user, password);
    }

    public void OpenHikVision(string IP, string user = "", string password = "")
    {
        OpenStream("http://" + IP + "/Streaming/channels/1/httppreview", IWebCam.JPEGType.MJpeg, user, password);
    }

    public void OpenScreen()
    {
        OpenConnection(new ScreenCaptureStream(new Rectangle(Point.Empty, MyClass.PhysicalScreenSize()), 100));
    }

    public Bitmap GetCurrentImage()
    {
        return (Bitmap)Container.Image;
    }

    public void CloseConnection()
    {
        if (videoSource != null)
            if (videoSource.IsRunning)
                videoSource.Stop();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    private void Videocapturedevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
    {
        Container.Image = (Bitmap)eventArgs.Frame.Clone();
    }

    private void OpenConnection(IVideoSource vSource)
    {
        videoSource = vSource;
        videoSource.NewFrame += Videocapturedevice_NewFrame;
        videoSource.Start();
    }

    ~WebCamAforge()
    {
        Dispose(false);
      //  GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        CloseConnection();
    }
}