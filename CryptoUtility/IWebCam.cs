using System.Drawing;
using System.Windows.Forms;

namespace CryptoUtility;

public interface IWebCam
{
    public enum JPEGType
    {
        Jpeg,
        MJpeg
    }

    public PictureBox Container { get; set; }
    public string[] VideoDevices();
    public void OpenDevice(int deviceIndex = 0);
    public void OpenStream(string url, JPEGType jpType, string user = "", string password = "");
    public void OpenAXIS(string IP, string user = "", string password = "");
    public void OpenHikVision(string IP, string user = "", string password = "");
    public void OpenScreen();
    public Bitmap GetCurrentImage();
    public void CloseConnection();
    public void Dispose();
}