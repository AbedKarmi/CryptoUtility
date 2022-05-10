using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Be.Windows.Forms;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Shell32;
using SpectrumAnalyzerLib;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;


using Timer = System.Windows.Forms.Timer;
//using Excel = Microsoft.Office.Interop.Excel;


//using System.Windows.Input;


namespace CryptoUtility;
public partial class FrmMain : Form
{
   

    #region Defs

    private const string factorDbURL = "http://www.factordb.com/index.php?query=";

    private enum WebMethod
    {
        HTTPClient = 0,
        WebClient,
        HttpWebRequest,
        WindowsDefault
    }

    private struct FactorData
    {
        public string number;
        public string code;
        public string digits;
        public int factors;
        public readonly List<string> factorList;

        public FactorData()
        {
            factorList = new List<string>();
            code = "";
            digits = "";
            factors = 0;
            number = "";
        }
    }

    private readonly string[][] fonts = { new[] { "KFGQPC Uthman Taha Naskh", "Uthmani.otf" }, new[] { "DQ7 Quran Koufi A", "DQ7QuranKoufiA.ttf" } };

#pragma warning disable CA1825 // Avoid zero-length array allocations
    private readonly string[] resources =  { }; //{ "bin32\\libiomp5md.dll", "bin32\\mkl_custom.dll", "bin64\\libiomp5md.dll", "bin64\\mkl_custom.dll" };
#pragma warning restore CA1825 // Avoid zero-length array allocations

    private int currentThread = -1;
    private int lockThread = -1;
    private bool needRestart;
    private EncodingOptions EncodingOptions { get; set; }
    private string stackTrace = "";
    private string lastPlayed;
    private Type Renderer { get; set; }
    private string sentSora = "", encoding = "";
    private InputLanguage original;
    private IQuran quran;
    private ISettings AppSettings;

    private Color backColor = Color.Black;
    //       struct Area { public int x; public int y; public int size; }
    //       List<Area> areas = new();

    private struct Pics
    {
        public int x1;
        public int y1;
        public int x2;
        public int y2;
        public int size1;
        public int size2;
    }

    private Pics picSpace;
    private readonly GPUClass gpuClass = new();
    private readonly RSAClass rsaClass = new(1024);
    private readonly DSAClass dsaClass = new();
    private RSAParameters privateKey, publicKey;
    private DSAParameters dsaPrivateKey, dsaPublicKey;
    private readonly string[] dataType = { "Base64", "Text", "Hex" };

    private int entry;
    private int CurNum;
    private string hex;
    private byte[] fileBuffer;
    private BigInteger P;
    private BigInteger Q;
    private BigInteger N;
    private BigInteger R;
    private readonly TextBox[] listTxtCS = new TextBox[44];
    private readonly Label[] listLblCS = new Label[44];
    private readonly Label[] listLblCSS = new Label[44];

   

    private struct Charset
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Data { get; set; }
        public Charset(string name,string description, byte[] data)
        {
            Name = name;
            Description = description;
            Data = data;
        }
    }
    private readonly Charset[] charsets = new Charset[]
    {
        new Charset("Common.Charset","«· ”·”· «·„‘ —ﬂ · — Ì» «·«Õ—› ·ﬂ«›… «· „ÀÌ·«  «·—ﬁ„Ì…",new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44 }),
        new Charset("Abjadi.Charset","«· — Ì» «·«»ÃœÌ",new byte[] { 0,1,1,6,1,10,1,2,5,22,23,3,8,24,4,25,20,7,15,21,18,26,9,27,16,28,17,19,11,12,13,14,5,6,10,10,0,0,0,0,0,0,0,0   }),
        new Charset("Abjadi-Hamza.Charset","«· — Ì» «·«»ÃœÌ „⁄ «⁄ »«— «·Â„“…",new byte[] { 1, 2, 2, 7, 2, 11, 2, 3, 6, 23, 24, 4, 9, 25, 5, 26, 21, 8, 16, 22, 19, 27, 10, 28, 17, 29, 18, 20, 12, 13, 14, 15, 6, 7, 11, 11, 0, 0, 0, 0, 0, 0, 0, 0 }),
        new Charset("Hijaei.Charset","«· — Ì» «·ÂÃ«∆Ì° ÊÂÊ «· — Ì» «·‘«∆⁄ Ê«·„⁄ „œ »‘ﬂ· «”«”Ì",new byte[] { 0, 1, 1, 27, 1, 28, 1, 2, 26, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 28, 0, 0, 0, 0, 0, 0, 0, 0 }),
        new Charset("Hijaei-Hamza.Charset","«· — Ì» «·ÂÃ«∆Ì „⁄ «⁄ »«— «·Â„“…",new byte[] { 1, 2, 2, 28, 2, 29, 2, 3, 27, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 29, 0, 0, 0, 0, 0, 0, 0, 0 }),
        new Charset("Vocal.Charset","«· — Ì» «·’Ê Ì ··Õ—Ê› «·⁄—»Ì… Õ”» «·‰ÿﬁ",new byte[] {  29, 28, 28, 26, 28, 27, 28, 24, 15, 15, 19, 8, 2, 4, 16, 18, 20, 13, 12, 9, 11, 10, 14, 17, 1, 5, 23, 6, 7, 21, 25, 22, 3, 26, 27, 27, 0, 0, 0, 0, 0, 0, 0, 0 }),
        new Charset("UTF8Single.Charset","«· „À»· «·—ﬁ„Ì ··Õ—Ê› «·⁄—»Ì… »«” Œœ«„  —„Ì“ UTF8",new byte[] { 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xAF, 0xB0, 0xB1, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7, 0xB8, 0xB9, 0xBA, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87, 0x88, 0x89, 0x8A,  0x8B, 0x8C, 0x8D, 0x8E, 0x8F, 0x90, 0x91, 0x92 }),
        new Charset("UTF8SingleAncient.Charset","«· „À· «·—ﬁ„Ì ··Õ—Ê› «·⁄—»Ì… «·ﬁœÌ„… »«” Œœ«„ »«Ì  Ê«Õœ… Ê«Â„«· «·»«Ì  «·«Œ—Ï",new byte[] { 0,167,167,136,167,137,167,168,135,170,171,172,173,174,175,176,177,178,179,180,181,182,183,184,185,186,129,130,131,132,133,134,135,136,137,137,0,0,0,0,0,0,0,0 }),
        new Charset("UnicodeSingle.Charset","«· „À· «·—ﬁ„Ì ··Õ—Ê› «·⁄—»Ì… »«” Œœ«„  —„Ì“ Unicode",new byte[] { 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3A, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F, 0x50, 0x51, 0x52 }),
        new Charset("UnicodeSingleAncient.Charset","«· „À· «·—ﬁ„Ì ··Õ—Ê› «·⁄—»Ì… «·ﬁœÌ„… »«” Œœ«„ »«Ì  Ê«Õœ…  —„Ì“ Unicode",new byte[] { 0,39,39,72,39,73,39,40,71,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,65,66,67,68,69,70,71,72,73,73,0,0,0,0,0,0,0,0 }),
        new Charset("SymbolicSingle.Charset","«· „ÀÌ· «·—ﬁ„Ì ··Õ—Ê› «·⁄—»Ì… »«” Œœ«„  —„Ì“ Symbolic",new byte[] { 0xD5, 0x45, 0x43, 0xDA, 0x47, 0xD9, 0x41, 0x4C, 0xD1, 0x50, 0x54, 0x58, 0x60, 0x64, 0x66, 0x67, 0x69, 0x6B, 0x70, 0x74, 0x78, 0x7E, 0xA2, 0xA6, 0xAA, 0xAE, 0xB2, 0xB6, 0xBA, 0xBE, 0xC2, 0xC6, 0xCA, 0xCB, 0xD4, 0xD0, 0xE7, 0xE8, 0xEB, 0xE4, 0xE5, 0xEA, 0xE9, 0xE6 }),
        new Charset("SymbolicSingleAncient.Charset","«· „ÀÌ· «·—ﬁ„Ì ··Õ—Ê› «·⁄—»Ì… «·ﬁœÌ„… »«” Œœ«„ »«Ì  Ê«Õœ…  —„Ì“ Symbolic",new byte[] { 0,65,65,203,65,212,65,76,202,80,84,88,96,100,102,103,105,107,112,116,120,126,162,166,170,174,178,182,186,190,194,198,202,203,212,212,0,0,0,0,0,0,0,0 }),
        new Charset("Ancient-Abjadi.Charset","«· — Ì» «·«»ÃœÌ ··Õ—Ê› «·⁄—»Ì… «·ﬁœÌ„…",new byte[] { 0, 1, 1, 13, 1, 5, 1, 9, 2, 9, 9, 3, 3, 3, 6, 6, 14, 14, 10, 10, 12, 12, 4, 4, 11, 11, 13, 13, 6, 7, 8, 9, 2, 13, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0 }),
        new Charset("Ancient-Hijaei-18Seq.Charset","«· „À· «·—ﬁ„Ì ··Õ—Ê› «·⁄—»Ì… «·ﬁœÌ„… „— »… ÂÃ«∆Ì«° ÊÂÌ 18 Õ—› „—ﬁ„…  ”·”·Ì«",new byte[] { 0, 1, 1, 17, 1, 18, 1, 2, 16, 2, 2, 3, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 18, 0, 0, 0, 0, 0, 0, 0, 0 }),
        new Charset("Ancient-Hijaei-18Alpha.Charset","«· „À· «·—ﬁ„Ì ··Õ—Ê› «·⁄—»Ì… «·ﬁœÌ„… „— »… ÂÃ«∆Ì«° ÊÂÌ 18 Õ—› „—ﬁ„… ÂÃ«∆Ì«",new byte[] {  0,1,1,27,1,28,1,2,26,2,2,6,6,6,8,8,10,10,12,12,14,14,16,16,18,18,20,21,22,23,24,25,26,27,28,28,0,0,0,0,0,0,0,0 }),
        new Charset("Ancient-Hijaei-14Seq.Charset","«· „À· «·—ﬁ„Ì ··Õ—Ê› «·⁄—»Ì… «·ﬁœÌ„… „— »… ÂÃ«∆Ì«° ÊÂÌ 14 Õ—› „—ﬁ„…  ”·”·Ì«° «·√·› Ìﬁ«»·Â« «·√·› ÊÂﬂ–« (»° °À°‰) Ìﬁ«»·Â« (‰) Ê«·Õ—Ê› (Ã°Õ°Œ) Ìﬁ«»·Â« (Õ) Ê«·Õ—Ê› (œ.–°ﬂ) Ìﬁ«»·Â« (ﬂ) ÊÕ—Ê› (—° “) Ìﬁ«»·Â« (—) ÊÕ—Ê› (” °‘) Ìﬁ«»·Â« (”) ÊÕ—Ê› (’°÷) Ìﬁ«»·Â« (’) ÊÕ—Ê› (ÿ°Ÿ) Ìﬁ«»·Â« (ÿ) ÊÕ—Ê› (⁄°€) Ìﬁ«»·Â« (⁄) ÊÕ—Ê› (›°ﬁ°Ê) Ìﬁ«»·Â« (ﬁ) ÊÕ—Ê› (·°„°Â‹°Ï) Ìﬁ«»·Â« ‰›” «·Õ—Ê› Ê»Â–« ÌﬂÊ‰ ⁄œœ Õ—Ê› «··€… «·⁄—»Ì… 28 Õ—›« Ìﬁ«»·Â« 14 Õ—›« ÂÏ «·Õ—Ê› «·„ﬁÿ⁄… «·–Ï ﬂ » »Â« «·ﬁ—¬‰ «·ﬂ—Ì„ Ê ﬁ⁄ Ã„Ì⁄Â« ›Ï ”Ê—… «·›« Õ…",
                    new byte[] {  0,1,1,10,1,14,1,2,13,2,2,3,3,3,4,4,5,5,6,6,7,7,8,8,9,9,10,10,4,11,12,2,13,10,14,14,0,0,0,0,0,0,0,0 }),
        new Charset("Ancient-Hijaei-14Alpha.Charset","«· „À· «·—ﬁ„Ì ··Õ—Ê› «·⁄—»Ì… «·ﬁœÌ„… „— »… ÂÃ«∆Ì«° ÊÂÌ 14 Õ—› „—ﬁ„… ÂÃ«∆Ì«° «·√·› Ìﬁ«»·Â« «·√·› ÊÂﬂ–« (»° °À°‰) Ìﬁ«»·Â« (‰) Ê«·Õ—Ê› (Ã°Õ°Œ) Ìﬁ«»·Â« (Õ) Ê«·Õ—Ê› (œ.–°ﬂ) Ìﬁ«»·Â« (ﬂ) ÊÕ—Ê› (—° “) Ìﬁ«»·Â« (—) ÊÕ—Ê› (” °‘) Ìﬁ«»·Â« (”) ÊÕ—Ê› (’°÷) Ìﬁ«»·Â« (’) ÊÕ—Ê› (ÿ°Ÿ) Ìﬁ«»·Â« (ÿ) ÊÕ—Ê› (⁄°€) Ìﬁ«»·Â« (⁄) ÊÕ—Ê› (›°ﬁ°Ê) Ìﬁ«»·Â« (ﬁ) ÊÕ—Ê› (·°„°Â‹°Ï) Ìﬁ«»·Â« ‰›” «·Õ—Ê› Ê»Â–« ÌﬂÊ‰ ⁄œœ Õ—Ê› «··€… «·⁄—»Ì… 28 Õ—›« Ìﬁ«»·Â« 14 Õ—›« ÂÏ «·Õ—Ê› «·„ﬁÿ⁄… «·–Ï ﬂ » »Â« «·ﬁ—¬‰ «·ﬂ—Ì„ Ê ﬁ⁄ Ã„Ì⁄Â« ›Ï ”Ê—… «·›« Õ…",
                    new byte[] {  0,1,1,21,1,28,1,25,26,25,25,6,6,6,22,22,10,10,12,12,14,14,16,16,18,18,21,21,22,23,24,25,26,21,28,28,0,0,0,0,0,0,0,0 }),
    };

    private readonly byte[] alphabetConnect = new byte[256];
    private readonly char[] alphabet = {'¡', '¬', '√', 'ƒ', '≈', '∆', '«', '»', ' ', 'À', 'Ã', 'Õ', 'Œ', 'œ', '–', '—', '“', '”', '‘', '’', '÷', 'ÿ', 'Ÿ', '⁄', '€', '›', 'ﬁ', 'ﬂ', '·', '„', '‰', 'Â', '…', 'Ê', 'Ï', 'Ì'};
    private readonly byte[] leftConnect = { 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1 };
    private readonly short[] jommalCharset =  {0, 1, 1, 6, 1, 10, 1, 2, 400, 400, 500, 3, 8, 600, 4, 700, 200, 7, 60, 300, 90, 800, 9, 900, 70, 1000, 80, 100, 20, 30, 40, 50, 5, 6, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0};

    private IWebCam wCam;

    private Timer webCamTimer;

    //private readonly BarcodeReader barcodeReader;
    //        MemoryStream audioFile;
    private bool pressed;
    private readonly string quranBin = Application.StartupPath + "Quran.bin";
    private readonly string quranWav = Application.StartupPath + "Quran.wav";
    private readonly string htmlFile = Application.StartupPath + "Quran.html";

    private enum RenderType
    {
        None,
        Wave,
        Spectrum
    }

    private readonly int readCount = 8000 * 50 / 1000; // 50 ms
    private readonly int shift = 8000 * 40 / 1000; // 40 ms
    private int readIdx;
    private bool recording;

    private AudioSensor sensor;
    private readonly List<List<double>> spectrums = new();
    private readonly Pen pen = new(Brushes.Black);
    private PointF[] pts = new PointF[8000];
    private float gain;
    private Bitmap spectrumBmp = new(800, 60, PixelFormat.Format24bppRgb);
    private readonly Bitmap spectrumBmpPrep = new(800, 60, PixelFormat.Format24bppRgb);
    private const int chunkSize = 800;

    private Action<float> setVolumeDelegate;

    private Timer blinkTimer;

    private readonly ToolTip txtCSttp = new();

    private byte GPUSCL;
    readonly ToolTip toolTip1 = new();

    bool lastDescChecked = false;
    TabPage PrevTab, CurTab;

    Thread threadPoly;
    private string lastEncUsed;
   

    Thread gpuThread;
    bool gpuStarted = false;

    #endregion

    #region Functions

    static void SetCheckColor(CheckBox cb)
    {
        if (cb.Checked) cb.BackColor = Color.Green; else cb.BackColor = Color.Red;
    }
    private byte[] GetLigatures(string text)
    {
        // ignore diacritics
        // 0: Separate , 1: Last, 2: First, 3: Middle
        var ligatures = new byte[text.Length];
        byte curLigature;

        byte lastLigature = alphabetConnect[(byte)text[0]];
        for (var i = 1; i < text.Length; i++)
        {
            curLigature = alphabetConnect[(byte)text[i]];
            if (lastLigature != 0)
                if (text[i] != ' ')
                {
                    ligatures[i - 1] += 2;
                    ligatures[i] = 1;
                }

            lastLigature = curLigature;
        }

        return ligatures;
    }


    private void OutputMsg(string msg)
    {
        tabControl2.SelectedTab = tabOutput;
        txtOut.Text += msg + Environment.NewLine;
        txtOut.SelectionStart = txtOut.Text.Length;
        txtOut.ScrollToCaret();
        txtOut.Refresh();
    }

    private void LogMsg(string msg, int threadID = -1)
    {
        if (string.IsNullOrEmpty(stackTrace)) stackTrace = Environment.StackTrace;
        var msgs = msg.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var str in msgs)
            if (lstLog.InvokeRequired)
            {
                lstLog.BeginInvoke(new MethodInvoker(delegate { LogMsg(msg); }));
            }
            else
            {
                lstLog.Items.Add(DateTime.Now + ": T[" +
                                 (threadID < 0
                                     ? Environment.CurrentManagedThreadId.ToString()
                                     : threadID.ToString()) + "] " + str);
                lstLog.SelectedIndex = lstLog.Items.Count - 1;
            }
        if (tabControl2.InvokeRequired)
            tabControl2.BeginInvoke(new MethodInvoker(delegate { tabControl2.SelectedIndex=0; }));
        else
            tabControl2.SelectedIndex = 0;
    }
    private void BtnClearOutput_Click(object sender, EventArgs e)
    {
        if (tabControl2.SelectedTab == tabLog)
            lstLog.Items.Clear();
        else if (tabControl2.SelectedTab == tabOutput)
            txtOut.Text = "";
        else webBrowser.GoHome();
    }

    private static RotateFlipType RFType(bool fx, bool fy)
    {
        if (fx && fy)
            return RotateFlipType.RotateNoneFlipXY;
        if (fx)
            return RotateFlipType.RotateNoneFlipX;
        if (fy)
            return RotateFlipType.RotateNoneFlipY;
        return RotateFlipType.RotateNoneFlipNone;
    }

    private void LogMsg(Exception ex, int threadID = -1)
    {
        var stackTrace = ex.StackTrace;
        this.stackTrace = stackTrace;
        var index = stackTrace.LastIndexOf("\\") + 1;
        stackTrace = stackTrace[index..].Replace("\"", "");
        LogMsg("Error:"+ex.Message + " @ " + stackTrace, threadID);
    }

    private enum ShellOptions : short
    {
        Default_No_options_specified = 0,
        Do_not_display_a_progress_dialog_box = 4,
        Rename_the_target_file_if_a_file_exists_at_the_target_location_with_the_same_name = 8,
        Click_Yes_to_All_in_any_dialog_box_displayed = 16,
        Preserve_undo_information,
        _if_possible = 64,
        Perform_the_operation_only_if_a_wildcard_file_name_Start_Dot_Star_is_specified = 128,
        Display_a_progress_dialog_box_but_do_not_show_the_file_names256,
        Do_not_confirm_the_creation_of_a_new_directory_if_the_operation_requires_one_to_be_created = 512,
        Do_not_display_a_user_interface_if_an_error_occurs = 1024,
        Disable_recursion = 4096,
        Do_not_copy_connected_files_as_a_group_Only_copy_the_specified_files_ = 8192
    }

    private void InstallFont(string fontFile)
    {
        MessageBox.Show("Installing required font (" + fontFile + ")");

        needRestart = true;
        File.WriteAllBytes(Application.StartupPath + fontFile,  MyClass.ResourceReadAllBytes("Fonts\\" + fontFile));

        MyClass.RegisterFont(Application.StartupPath  + fontFile);
/*
        Shell shell = new();
        var fontFolder = shell.NameSpace(0x14); // 0x14:  Destination FonFolder
        // Window Copy File From CopyHere.(Source) to .NameSpace(Destination)
        fontFolder.CopyHere(Application.StartupPath +  fontFile,
                            ShellOptions.Do_not_display_a_progress_dialog_box |
                            ShellOptions.Click_Yes_to_All_in_any_dialog_box_displayed);
*/
        File.Delete(Application.StartupPath + fontFile);
    }

    private void CheckQuranFont()
    {
        FontFamily[] fontFamilies;
        InstalledFontCollection installedFontCollection = new();

        var fontFound = new bool[fonts.Length];

        for (var i = 0; i < fontFound.Length; i++) fontFound[i] = false;

        // Get the array of FontFamily objects.
        fontFamilies = installedFontCollection.Families;
        for (var i = 0; i < fontFamilies.Length; i++)
        for (var j = 0; j < fontFound.Length; j++)
            if (fontFamilies[i].Name == fonts[j][0])
                fontFound[j] = true;

        for (var j = 0; j < fontFound.Length; j++)
            if (!fontFound[j])
                InstallFont(fonts[j][1]);
    }

    public static BitmapImage ConvertToImage(byte[] buffer)
    {
        MemoryStream stream = new(buffer);
        BitmapImage image = new();
        image.BeginInit();
        image.StreamSource = stream;
        image.EndInit();
        return image;
    }


    /*
           foreach (TextBox tb in OfType<TextBox>(this.Controls)) {
                ..
            }
    */

    public static void ClearControls(Control c)
    {
        foreach (Control Ctrl in c.Controls)
            //Console.WriteLine(Ctrl.GetType().ToString());
            //MessageBox.Show ( (Ctrl.GetType().ToString())) ;
            switch (Ctrl.GetType().ToString())

            {
                case "System.Windows.Forms.CheckBox":
                    ((CheckBox)Ctrl).Checked = false;
                    break;

                case "System.Windows.Forms.TextBox":
                    ((TextBox)Ctrl).Text = "";
                    break;

                case "System.Windows.Forms.RichTextBox":
                    ((RichTextBox)Ctrl).Text = "";
                    break;

                case "System.Windows.Forms.ComboBox":
                    ((ComboBox)Ctrl).SelectedIndex = 0;
                    break;

                case "System.Windows.Forms.MaskedTextBox":
                    ((MaskedTextBox)Ctrl).Text = "";
                    break;
                default:
                    if (Ctrl.Controls.Count > 0)
                        ClearControls(Ctrl);
                    break;
            }
    }

    public static void AppendAllBytes(string path, byte[] bytes, bool crlf = false)
    {
        //argument-checking here.

        using var stream = new FileStream(path, FileMode.Append);
        stream.Write(bytes, 0, bytes.Length);
        if (crlf) stream.Write(new byte[] { 0x0d, 0x0a }, 0, 2);
    }

    private static InputLanguage GetArabicLanguage()
    {
        foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            if (lang.LayoutName.Contains("Arabic") || lang.Culture.Name.StartsWith("ar-"))
                return lang;
        return null;
    }

    private void ArabicKeyboard()
    {
        original = InputLanguage.CurrentInputLanguage;
        var lang = GetArabicLanguage();
        if (lang == null) LogMsg("Arabic Language Keyboard not installed.");

        InputLanguage.CurrentInputLanguage = lang;
    }

    private void OriginalKeyboard()
    {
        // in the leave event handler:
        InputLanguage.CurrentInputLanguage = original;
    }

    private static IEnumerable<T> GetAllControls<T>(Control container)
    {
        var controlList = new List<T>();
        foreach (Control c in container.Controls)
        {
            controlList.AddRange(GetAllControls<T>(c));

            if (c is T box)
                controlList.Add(box);
        }

        return controlList;
    }


    private byte[] SafeRead(string file)
    {
        try
        {
            File.Copy(file, file + ".tmp");
            var buffer = File.ReadAllBytes(file + ".tmp");
            File.Delete(file + ".tmp");
            return buffer;
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }

        return Array.Empty<byte>();
    }

    private static void SelectIndex(ComboBox cmb, string text)
    {
        for (var i = 0; i < cmb.Items.Count; i++)
            if (cmb.Items[i].ToString().Equals(text))
            {
                cmb.SelectedIndex = i;
                return;
            }

        ;
        cmb.SelectedIndex = -1;
        cmb.Text = text;
    }

    private static int FindInList(ListBox listBox, string searchString, bool caseSensitive = false)
    {
        var index = -1;
        if (!string.IsNullOrEmpty(searchString))
            for (var i = 0; i < listBox.Items.Count; i++)
                if (listBox.Items[i].ToString().Contains(searchString, caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
                {
                    index = i;
                    break;
                }

        return index;
    }

    private static int FindInCombo(ComboBox comboBox, string searchString, bool caseSensitive = false, bool setIfFound = false)
    {
        var index = -1;
        if (!string.IsNullOrEmpty(searchString))
            for (var i = 0; i < comboBox.Items.Count; i++)
                if (comboBox.Items[i].ToString().Contains(searchString, caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
                {
                    if (setIfFound)
                        comboBox.SelectedIndex = i;
                    index = i;
                    break;
                }

        return index;
    }

    private static void ViewImage(string photo)
    {
        var imageViewerAssoc = FileAssociation.GetExecFileAssociatedToExtension(".jpg", "open");
        var imageViewer = Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%") +
                          "\\Windows Photo Viewer\\PhotoViewer.dll";
        Process proc;

        if (!string.IsNullOrEmpty(imageViewerAssoc))
            proc = Process.Start(imageViewerAssoc, photo);
        else
            proc = Process.Start("rundll32.exe", "\"" + imageViewer + "\", ImageView_Fullscreen " + photo);

        proc.WaitForInputIdle(); // wait for program to start

        if (proc != null && proc.HasExited != true) proc.WaitForExit(); // wait for porgram to finish
    }

    /// <summary>
    ///     Load an embedded resource and write it to the disk in the startup folder
    /// </summary>
    /// <param name="resourceName">Full Path to Resource Name</param>
    private void DumpResource(string resourceName)
    {
        try
        {
            if (File.Exists(resourceName))
                return;

            var file = Application.StartupPath +  resourceName;
            var dir = Path.GetDirectoryName(file);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            var resource = Array.Find(GetType().Assembly.GetManifestResourceNames(),
                element => element.EndsWith(resourceName.Replace("\\", ".")));
            if (!string.IsNullOrEmpty(resource))
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    var assemblyData = new byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    File.WriteAllBytes(file, assemblyData);
                }
            else
                File.Copy(Application.StartupPath + "Lib\\" + resourceName, file);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void ManageResources()
    {
        // run this lambda function when couldn't find a DLL, it will load it from the resource :)
        AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
        {
            try
            {
                var resourceName = new AssemblyName(args.Name).Name + ".dll";
                var resource = Array.Find(GetType().Assembly.GetManifestResourceNames(),
                    element => element.EndsWith(resourceName));
                if (!string.IsNullOrEmpty(resource))
                    using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                    {
                        var assemblyData = new byte[stream.Length];
                        stream.Read(assemblyData, 0, assemblyData.Length);
                        return Assembly.Load(assemblyData);
                    }

                if (File.Exists(Application.StartupPath + "LIB\\" + resourceName))
                    return Assembly.LoadFrom(Application.StartupPath + "LIB\\" + resourceName);
            }
            catch (Exception)
            {
            }

            ;

            return null;
        };

        foreach (var resource in resources)
            DumpResource(resource);
    }

    #endregion

    #region Forms

    // Important ....!
    // File DragDrop will not work if run Application as Adminstrator
    private void TextBox_MouseDown(object sender, MouseEventArgs e)
    {
        var txt = (TextBox)sender;
        if (e.Button.Equals(MouseButtons.Left))
        {
            txt.SelectAll();
            txt.DoDragDrop(txt.Text, DragDropEffects.Copy);
        }
    }

    private void TextBox_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.Text) || e.Data.GetDataPresent(DataFormats.FileDrop))
            e.Effect = DragDropEffects.Copy;
        else
            e.Effect = DragDropEffects.None;
    }

    private void TextBox_DragDrop(object sender, DragEventArgs e)
    {
        var txt = (TextBox)sender;
        if (e.Data.GetDataPresent(DataFormats.Text))
        {
            txt.Text =(string)e.Data.GetData(DataFormats.Text);
        }
        else if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0) txt.Text = File.ReadAllText(files[0]);
        }
    }

    private void TextBox_DragOver(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.Text) || e.Data.GetDataPresent(DataFormats.FileDrop))
            e.Effect = DragDropEffects.Copy;
        else
            e.Effect = DragDropEffects.None;
    }

    private void TxtOut_DoubleClick(object sender, EventArgs e)
    {
        File.WriteAllText(Application.StartupPath + "tmp.txt", txtOut.Text);
        Process.Start(Application.StartupPath + "tmp.txt");
    }

    public FrmMain()
    {
        ManageResources();

        InitializeComponent();
    }

    private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
    {
        try
        {
            quran.CloseQuran();
            pressed = true;
            Thread.Sleep(500);
            CloseSpectrum();
        }
        catch (Exception)
        {
        }

        ;
    }

    private void FrmMain_Load(object sender, EventArgs e)
    {
        //RegisterChilkat();
        try
        {
            if (!File.Exists(quranBin)) File.WriteAllBytes(quranBin,new byte[] {0,0,0});
            //Register All Non-Common codepages to be available in .Net6
            EncodingProvider provider = CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(provider);

            CurTab = tabControl1.SelectedTab;

            Text = "QuranView Utility for Quran Fidelity, Version " + typeof(FrmMain).Assembly.GetName().Version;

            //  InitHexBox();

            AppSettings = new IniFile(Application.ExecutablePath[0..^4] + ".ini");

            for (var i = 0; i < alphabet.Length; i++) alphabetConnect[(byte)alphabet[i]] = leftConnect[i];

            ResetCharsets();
            GetCharsetControls();

            LoadEncodings();

            cmbCam.SelectedIndex = 0;

            wCam = new WebCamAforge { Container = picQuran1 };

            cmbCapDevice.Items.Clear();
            cmbCapDevice.Items.AddRange(wCam.VideoDevices());
            if (cmbCapDevice.Items.Count > 0) cmbCapDevice.SelectedIndex = 0;
            //videocapturedevice = new VideoCaptureDevice();

            quran = new QuranDB(Application.StartupPath);
            // quran = new QuranXLS(Application.StartupPath);

            lblCurCharset.Text = AppSettings.ReadValue("Settings", "CharsetProfile", "Common.Charset");

            gpuClass.ProcessCompleted += GPUClass_ProcessCompleted;
            gpuClass.ProcessProgress += GpuClass_ProcessProgress;

            GPUSCL = 0;
            ulong pwr = GPUClass.ProgressScale;
            while  (pwr > 1) { GPUSCL++; pwr /= 10; }

            picSpace.x1 = picQuran1.Left;
            picSpace.y1 = picQuran1.Top;
            picSpace.size1 = picQuran1.Width;
            picSpace.x2 = picQuran2.Left;
            picSpace.y2 = picQuran2.Top;
            picSpace.size2 = picQuran2.Width;

            cmbHash.SelectedIndex = 0;
            cmbData.SelectedIndex = 1;
            cmbKeyLength.SelectedIndex = 1;
            cmbEncryptionKey.SelectedIndex = 0;
            cmbDSAKeyLen.SelectedIndex = 0;
            cmbCryptoAlgorithm.SelectedIndex = 0;
            cmbRSAKeyLen.SelectedIndex = 0;
            cmbExponentMethod.SelectedIndex = 0;
            cmbMULMethod.SelectedIndex = 0;
            cmbHHash.SelectedIndex = 0;

            CurNum = 2;
            rb16.Checked = true;
            cmbKeyLen.SelectedIndex = 0;

            ToolTip toolTip = new();
            toolTip.SetToolTip(txtInfo, "Double-Click to copy content to clipboard");

            
            cmbAccelerator.Items.Clear();
            cmbAccelerator.Items.Add("Direct");
            try
            {
                if (GPUClass.GetAccelerators().Length > 0)
                    cmbAccelerator.Items.AddRange(GPUClass.GetAccelerators());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ;
            if (cmbAccelerator.Items.Count > 0) cmbAccelerator.SelectedIndex = 0;

            //Quran=ReadQuran();

            CheckQuranFont();

            if (needRestart)
            {
                MessageBox.Show("Application will restart now");
                Process.Start(Application.ExecutablePath);
                Application.Exit();
            }

            cmbCRC.Items.AddRange(MyCRCBI.AllCRCMethods.ToArray());
            CmbCRCViewer.Items.Add("CUSTOM");
            CmbCRCViewer.Items.Add("ALL");
            CmbCRCViewer.Items.AddRange(MyCRCBI.AllCRCMethods.ToArray());
            CmbCRCViewer.SelectedIndex = 0;

            txtQuranText.Font = new Font(fonts[0][0], 12);
            // lbSoras.Font = new Font(QuranFont, 10);

            lblFontSize.Text = AppSettings.ReadValue("Settings", "FontSize", "12");
            txtQuranText.Font = new Font(txtQuranText.Font.FontFamily, int.Parse(lblFontSize.Text));

            lblPointSize.Text = AppSettings.ReadValue("Settings", "Scale", "1");
            lblColorPointSize.Text = AppSettings.ReadValue("Settings", "ColorScale", "1");

            cmbBytesPerLine.Text = AppSettings.ReadValue("Settings", "BytesPerLine", "16");
            cmbSpace.Text = AppSettings.ReadValue("Settings", "SpaceChar", "0x00");
            cmbSizeMode.SelectedIndex = int.Parse(AppSettings.ReadValue("Settings", "SizeMode", "1"));
            cmbCoding.SelectedIndex = int.Parse(AppSettings.ReadValue("Settings", "CharCoding", "0"));
            cmbCRC.SelectedIndex = int.Parse(AppSettings.ReadValue("Settings", "CRC8", "0"));
            cmbColorSizeMode.SelectedIndex = int.Parse(AppSettings.ReadValue("Settings", "ColorSizeMode", "1"));
            chkALLEncodings.Checked = AppSettings.ReadValue("Settings", "AllEncodings", "NO").ToUpper() == "YES";
            chkSendToBuffer.Checked = AppSettings.ReadValue("Settings", "SendToBuffer", "Yes").ToUpper() == "YES";
            chkJommalWord.Checked = AppSettings.ReadValue("Settings", "JommalWORD", "Yes").ToUpper() == "YES";
            chkFixPadding.Checked = AppSettings.ReadValue("Settings", "Padding", "No").ToUpper() == "YES";
            chkFlipX.Checked = AppSettings.ReadValue("Settings", "FlipX", "Yes").ToUpper() == "YES";
            chkFlipY.Checked = AppSettings.ReadValue("Settings", "FlipY", "No").ToUpper() == "YES";
            chkINV.Checked = AppSettings.ReadValue("Settings", "Inversed", "No").ToUpper() == "YES";
            lastPlayed = AppSettings.ReadValue("Settings", "LastPlayed");
            chkColorScaled.Checked = AppSettings.ReadValue("Settings", "ColorScaled", "YES").ToUpper() == "YES";
            chkStrechedLines.Checked = AppSettings.ReadValue("Settings", "StrechedLines", "NO").ToUpper() == "YES";
            chkMultiLine.Checked = AppSettings.ReadValue("Settings", "MultiLine", "NO").ToUpper() == "YES";
            chkPreserveSpace.Checked = AppSettings.ReadValue("Settings", "PreserveSpace", "NO").ToUpper() == "YES";
            chkSplitWords.Checked = AppSettings.ReadValue("Settings", "SplitWords", "NO").ToUpper() == "YES";
            chkShowAsTable.Checked = AppSettings.ReadValue("Settings", "ShowAsTable", "NO").ToUpper() == "YES";
            ChkShowSpace.Checked = AppSettings.ReadValue("Settings", "ShowSpace", "NO").ToUpper() == "YES";
            ChkUseGPU.Checked = AppSettings.ReadValue("Settings", "UseGPU", "NO").ToUpper() == "YES";
            chkBigInteger.Checked = AppSettings.ReadValue("Settings", "BigIntegers", "NO").ToUpper() == "YES";
            lastEncUsed = AppSettings.ReadValue("Settings", "LastEncodingUsed");
            
            ChkPreserveSpace_CheckedChanged(this, EventArgs.Empty);
            rbFirstOriginalDots.Checked = false;
            switch (AppSettings.ReadValue("Settings", "QuranText", "rbFirstOriginalDots"))
            {
                case "rbDiacritics":
                    rbDiacritics.Checked = true;
                    break;
                case "rbNoDiacritics":
                    rbNoDiacritics.Checked = true;
                    break;
                case "rbFirstOriginal":
                    rbFirstOriginal.Checked = true;
                    break;
                case "rbFirstOriginalDots":
                    rbFirstOriginalDots.Checked = true;
                    break;
            }

            chkPlay.Checked = AppSettings.ReadValue("Settings", "PlayWhileRecord", "No").ToUpper() == "YES";
            SelectIndex(cmbSampleRate, AppSettings.ReadValue("Settings", "SampleRate", cmbSampleRate.Items[0].ToString()));
            SelectIndex(cmbBits, AppSettings.ReadValue("Settings", "Bits", cmbBits.Items[0].ToString()));
            SelectIndex(cmbChannels, AppSettings.ReadValue("Settings", "Channels", cmbChannels.Items[0].ToString()));
            SelectIndex(cmbWebClient, AppSettings.ReadValue("Settings", "WebClient", cmbWebClient.Items[0].ToString()));

            InitSpectrumScreen();
            InitSpectrum();

            tabControl1.SelectedTab = null;
            tabControl1.SelectedTab = tabQuran;
            canvas.AllowDrop = true;

            trkDB.Value = 13;

            LoadCharset(lblCurCharset.Text);

            lbSoras.Items.Clear();
            lbSoras.Items.AddRange(quran.GetSoraNames());
            lbSoras.SelectedIndex = 0;
            SelectSora();

            //Adjust Width
            Width = (int)(txtInfo.Left + txtInfo.Width + tabControl1.Left * 2.5);

            // Tooltips
            
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(BtnToP, "Send Hex Data to P in Calculator");
            toolTip1.SetToolTip(BtnToQ, "Send Hex Data to Q in Calculator");
            toolTip1.SetToolTip(BtnToC, "Send Hex Data to CRC in HexViewerr");
            toolTip1.SetToolTip(BtnSHex, "Send Hex Data to Data in HexViewerr");
            toolTip1.SetToolTip(BtnToPoly, "Send Hex Data to Poly in HexViewerr");
            toolTip1.SetToolTip(btnSCrypto, "Send Hex Data to Key in Crypto");
            toolTip1.SetToolTip(btnSImage, "Send Hex Data to Texture/Image");
            toolTip1.SetToolTip(btnSpectrum, "Send Hex Data to Audio/Spectrum");
            toolTip1.SetToolTip(btnColor, "Send Hex Data to Color/Spectrum");
            toolTip1.SetToolTip(ChkMonitor, "Monitor Progress of Kernel Process");
        }
        catch (Exception ex)
        {
            LogMsg(ex);
            tabControl1.Enabled = false;
            try { if (File.Exists(quranBin)) File.Delete(quranBin); } catch { }
        }
    }

    private void BtnStackTrace_Click(object sender, EventArgs e)
    {
        txtInfo.Text = stackTrace;
    }

    private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            PrevTab = CurTab;CurTab = tabControl1.SelectedTab;
            if (tabControl1.SelectedTab == tabEncoding && chkRTL.Checked && !chkHexText.Checked)
                txtInfo.RightToLeft = RightToLeft.Yes;
            else txtInfo.RightToLeft = RightToLeft.No;
            if (tabControl1.SelectedTab == tabRSA)
            {
                txtInfo.Text = RSAClass.Info;
            }
            else if (tabControl1.SelectedTab == tabDSA)
            {
                txtInfo.Text = DSAClass.Info;
            }
            else if (tabControl1.SelectedTab == tabCrypto)
            {
                txtInfo.Text =
                    "QuranView Utility\r\n\r\nDecrypt/Encrypt, Calculate hash, and Sign/Verify data using public/private keys.\r\n\r\nData can be loaded in the textbox or a file buffer.";
            }
            else if (tabControl1.SelectedTab == tabCalculator)
            {
                CmbAccelerator_SelectedIndexChanged(sender, e);
            }
            else if (tabControl1.SelectedTab == tabEncoding)
            {
                if (string.IsNullOrEmpty(hex))
                    txtInfo.Text = @"Quran Text

Discard Characters :[Space, OmittedAlef, Carriage  Return, Line Feed]
End of Aya Char Replacement : [zString]

Encoding method : Arabic Common CharSet Order [ACCO]";
            }
            else if (tabControl1.SelectedTab == tabQuran)
            {
                txtInfo.Text = @"QuranView Utility for Quran Fidelity

Quran was revealed to the last prophet Mohammad peace upon him. 

Nowadays, almost all important data is being verified againest alteration and/or corruption.

Quran, is being protected by God against any change, that was stated clearly in the Quran verse. We believe that, Quran must have a modern way that allows verification of its content.

Text to byte conversion:
We implemented several encoding options to convert Arabic text of the Quran to numeric stream of data. 

This utility can represnt the data in several ways:
Hex, Texture, Audio, Light & Color representations";
            }
            else if (tabControl1.SelectedTab == tabCharset)
            {
                LoadCharset(Application.StartupPath + lblCurCharset.Text);
                txtInfo.Text = @"Arabic Charsets

Define the byte conversion of Arabic alphbet, this will be used in the encode/decode process.

Auto: set the first charset, then will auto complete to the end

Blue   : Siamese Lettters
Green  : Arabic Letters
Red    : Diacritics";
            }
            else if (tabControl1.SelectedTab == tabHexViewer)
            {
                txtInfo.Text = "Simple Hex Byte Editor/Viewer";
            }
            else if (tabControl1.SelectedTab == tabTexture)
            {
                txtInfo.Text =
                    "Texture\r\n\r\nDisplay Binary Image of Text Endoding\r\n\r\nLooking for meaningful image represntation of binary data";
            }
            else if (tabControl1.SelectedTab == tabAudioSpectrum)
            {
                txtInfo.Text =
                    "Audio Spectrum Analyzer\r\n\r\nConvert binary data to frequency using FFT and display in spectrum analyzer\r\nLooking for meaningful voice";
            }
            else if (tabControl1.SelectedTab == tabColor)
            {
                txtInfo.Text =
                    "Light/Color Spectrum\r\n\r\nConvert binary data to colors using RGB and display in spectrum analyzer\r\nLooking for meaningful color";
            }
            
            if (PrevTab == tabCharset)
            {
                lastDescChecked = chkCharSetDesc.Checked;
                chkCharSetDesc.Checked = false;
            } 
            else if (CurTab== tabCharset)
            {
                chkCharSetDesc.Checked = lastDescChecked;
            }
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    #endregion

    #region RSA

    private void BtnGenKeys_Click(object sender, EventArgs e)
    {
        try
        {
            rsaClass.CreateKey(int.Parse(cmbRSAKeyLen.Text));
            privateKey = rsaClass.GetKey(true);
            publicKey = rsaClass.GetKey(false);
            /*
             txtPrimeP.Text = MyClass.BinaryToHexString(privateKey.P);
             txtPrimeQ.Text = MyClass.BinaryToHexString(privateKey.Q);
             txtModulN.Text = MyClass.BinaryToHexString( privateKey.Modulus);
            */
            txtPEMPublicKey.Text = PEMClass.ExportPublicKey(publicKey, true);
            txtPEMPrivateKey.Text = PEMClass.ExportPrivateKey(privateKey, true);

            LogMsg("Keys Generated");
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void BtnCalcD_Click(object sender, EventArgs e)
    {
        var P = BigIntegerHelper.GetBig(txtP.Text);
        var Q = BigIntegerHelper.GetBig(txtQ.Text);
        var E = BigIntegerHelper.GetBig(txtE.Text);

        var phi = (P - 1) * (Q - 1);
        var d = E.Modinv(phi);
        txtN.Text = BigIntegerHelper.Hex(P * Q);
        txtD.Text = BigIntegerHelper.Hex(d);
        txtDP.Text = BigIntegerHelper.Hex(d % (P - 1));
        txtDQ.Text = BigIntegerHelper.Hex(d % (Q - 1));
        txtInverseQ.Text = BigIntegerHelper.Hex(Q.Modinv(P));
    }

    private void BtnRSAtoCALC_Click(object sender, EventArgs e)
    {
        txtPrimeP.Text = txtP.Text;
        txtPrimeQ.Text = txtQ.Text;
        txtModulN.Text = txtN.Text;
        tabControl1.SelectedTab = tabCalculator;
    }

    private void BtnUsePublicKey_Click(object sender, EventArgs e)
    {
        txtPublicKey.Text = txtPEMPublicKey.Text;
    }

    private void BtnUsePrivateKey_Click(object sender, EventArgs e)
    {
        txtPrivateKey.Text = txtPEMPrivateKey.Text;
    }

    private void ClearRSAKeys()
    {
        txtE.Text = "";
        txtN.Text = "";
        txtP.Text = "";
        txtQ.Text = "";
        txtD.Text = "";
        txtDP.Text = "";
        txtDQ.Text = "";
        txtInverseQ.Text = "";
    }

    private void BtnImportPublicKey_Click(object sender, EventArgs e)
    {
        try
        {
            ClearRSAKeys();
            publicKey = PEMClass.ImportPublicKey(txtPEMPublicKey.Text);
            rsaClass.SetKey(publicKey, false);
            var E = 0;
            for (var i = 0; i < publicKey.Exponent.Length; i++) E += publicKey.Exponent[i] << (i * 8);

            txtE.Text = E.ToString("X");
            txtN.Text = MyClass.ByteArrayToHexString(publicKey.Modulus);

            // tabControl.SelectedIndex = 0;

            cmbRSAKeyLen.Text = (publicKey.Modulus.Length * 8).ToString();
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void BtnImportPrivateKey_Click(object sender, EventArgs e)
    {
        try
        {
            ClearRSAKeys();
            privateKey = PEMClass.ImportPrivateKey(txtPEMPrivateKey.Text);
            rsaClass.SetKey(privateKey, true);
            var E = 0;
            for (var i = 0; i < privateKey.Exponent.Length; i++) E += privateKey.Exponent[i] << (i * 8);

            txtE.Text = E.ToString("X");
            txtN.Text = MyClass.ByteArrayToHexString(privateKey.Modulus);

            txtP.Text = MyClass.ByteArrayToHexString(privateKey.P);
            txtQ.Text = MyClass.ByteArrayToHexString(privateKey.Q);
            txtD.Text = MyClass.ByteArrayToHexString(privateKey.D);
            txtDP.Text = MyClass.ByteArrayToHexString(privateKey.DP);
            txtDQ.Text = MyClass.ByteArrayToHexString(privateKey.DQ);
            txtInverseQ.Text = MyClass.ByteArrayToHexString(privateKey.InverseQ);

            // tabControl.SelectedIndex = 0;

            cmbRSAKeyLen.Text = (privateKey.Modulus.Length * 8).ToString();
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void BtnExportPublic_Click(object sender, EventArgs e)
    {
        //    using RSACryptoServiceProvider csp = new();
        RSAParameters rsaParams = new();
        try
        {
            var E = int.Parse(txtE.Text, NumberStyles.HexNumber); // or use  Convert.ToInt32(txtE.Text, 16);
            var i = 0;
            while (E > 0)
            {
                Array.Resize(ref rsaParams.Exponent, i + 1);
                rsaParams.Exponent[i] = (byte)(E & 0xFF);
                i++;
                E >>= 8;
            }

            rsaParams.Modulus = MyClass.HexStringToByteArray(txtN.Text);
            if (chkReverse.Checked) Array.Reverse(rsaParams.Modulus);


            // csp.ImportParameters(rsaParams);
            txtPEMPublicKey.Text = PEMClass.ExportPublicKey(rsaParams, true);
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void BtnExportPrivate_Click(object sender, EventArgs e)
    {
        RSAParameters rsaParams = new();
        try
        {
            var E = int.Parse(txtE.Text, NumberStyles.HexNumber);
            var i = 0;
            while (E > 0)
            {
                Array.Resize(ref rsaParams.Exponent, i + 1);
                rsaParams.Exponent[i] = (byte)(E & 0xFF);
                i++;
                E >>= 8;
            }

            rsaParams.Modulus = MyClass.HexStringToByteArray(txtN.Text);
            rsaParams.P = MyClass.HexStringToByteArray(txtP.Text);
            rsaParams.Q = MyClass.HexStringToByteArray(txtQ.Text);
            rsaParams.D = MyClass.HexStringToByteArray(txtD.Text);
            rsaParams.DP = MyClass.HexStringToByteArray(txtDP.Text);
            rsaParams.DQ = MyClass.HexStringToByteArray(txtDQ.Text);
            rsaParams.InverseQ = MyClass.HexStringToByteArray(txtInverseQ.Text);

            txtPEMPrivateKey.Text = PEMClass.ExportPrivateKey(rsaParams, true);
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    #endregion

    #region DSA

    private void BtnExportDSAPublic_Click(object sender, EventArgs e)
    {
        try
        {
            DSAParameters dsaKey = new();
            dsaKey.P = MyClass.HexStringToByteArray(txtDSA_P.Text);
            dsaKey.Q = MyClass.HexStringToByteArray(txtDSA_Q.Text);
            dsaKey.G = MyClass.HexStringToByteArray(txtDSA_G.Text);
            dsaKey.Y = MyClass.HexStringToByteArray(txtDSA_Y.Text);

            txtDSAPEMPublic.Text = PEMClass.ExportDSAPublicKey(dsaKey);
        }
        catch (Exception ex)
        {
           LogMsg(ex);
        }
    }

    private void BtnGenDSAKeys_Click(object sender, EventArgs e)
    {
        try
        {
            dsaClass.CreateKey(int.Parse(cmbDSAKeyLen.Text));
            dsaPrivateKey = dsaClass.GetKey(true);
            dsaPublicKey = dsaClass.GetKey(false);

            txtDSAPEMPublic.Text = PEMClass.ExportDSAPublicKey(dsaPublicKey);
            txtDSAPEMPrivate.Text = PEMClass.ExportDSAPrivateKey(dsaPrivateKey);

            LogMsg("DSA Keys Generated");
        }
        catch (Exception ex)
        {
           LogMsg(ex);
        }
    }

    private void BtnDSAImportPublic_Click(object sender, EventArgs e)
    {
        try
        {
            ClearKeys();
            var dsaKey = PEMClass.ImportDSAPublicKey(txtDSAPEMPublic.Text);
            cmbDSAKeyLen.Text = (dsaKey.Y.Length * 8).ToString();
            ShowKeys(dsaKey, false);
        }
        catch (Exception ex)
        {
           LogMsg(ex);
        }
    }

    private void TxtDSAPEM_DragOver(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.Text))
            e.Effect = DragDropEffects.Copy;
        else
            e.Effect = DragDropEffects.None;
    }

    private void TxtDSAPEM_DragDrop(object sender, DragEventArgs e)
    {
        txtDSAPEMPublic.Text = (string)e.Data.GetData(DataFormats.Text);
    }

    private void BtnDSAImportPrivate_Click(object sender, EventArgs e)
    {
        try
        {
            ClearKeys();
            var dsaKey = PEMClass.ImportDSAPrivateKey(txtDSAPEMPrivate.Text);
            cmbDSAKeyLen.Text = (dsaKey.Y.Length * 8).ToString();
            ShowKeys(dsaKey, true);
        }
        catch (Exception ex)
        {
           LogMsg(ex);
        }
    }

    private void BtnExportDSA_Click(object sender, EventArgs e)
    {
        try
        {
            DSAParameters dsaKey = new();
            dsaKey.P = MyClass.HexStringToByteArray(txtDSA_P.Text);
            dsaKey.Q = MyClass.HexStringToByteArray(txtDSA_Q.Text);
            dsaKey.G = MyClass.HexStringToByteArray(txtDSA_G.Text);
            dsaKey.Y = MyClass.HexStringToByteArray(txtDSA_Y.Text);

            if (txtDSA_X.Text.Trim() != "")
            {
                dsaKey.X = MyClass.HexStringToByteArray(txtDSA_X.Text);
                txtDSAPEMPrivate.Text = PEMClass.ExportDSAPrivateKey(dsaKey);
            }
            else
            {
                txtDSAPEMPublic.Text = PEMClass.ExportDSAPublicKey(dsaKey);
            }
        }
        catch (Exception ex)
        {
           LogMsg(ex);
        }
    }

    private void ShowKeys(DSAParameters dsaKey, bool priv)
    {
        txtDSA_P.Text = MyClass.ByteArrayToHexString(dsaKey.P);
        txtDSA_Q.Text = MyClass.ByteArrayToHexString(dsaKey.Q);
        txtDSA_G.Text = MyClass.ByteArrayToHexString(dsaKey.G);
        txtDSA_Y.Text = MyClass.ByteArrayToHexString(dsaKey.Y);
        if (priv) txtDSA_X.Text = MyClass.ByteArrayToHexString(dsaKey.X);
    }

    private void ClearKeys()
    {
        txtDSA_P.Text = "";
        txtDSA_Q.Text = "";
        txtDSA_G.Text = "";
        txtDSA_Y.Text = "";
        txtDSA_X.Text = "";
    }

    #endregion

    #region Crypto

    private void BtnUseKeys_Click(object sender, EventArgs e)
    {
        switch (cmbCryptoAlgorithm.SelectedIndex)
        {
            case 0:
                txtPublicKey.Text = txtPEMPublicKey.Text;
                txtPrivateKey.Text = txtPEMPrivateKey.Text;
                break;
            case 1:
                txtPublicKey.Text = txtDSAPEMPublic.Text;
                txtPrivateKey.Text = txtDSAPEMPrivate.Text;
                break;
        }
    }

    private void HashAll()
    {
        foreach (var h in cmbHash.Items)
        {
            var hashText = cmbHash.GetItemText(h);
            var hash = MyClass.GetHash(GetData(), hashText);
            OutputMsg(string.Format("{0,-6} -> {1}", hashText, MyClass.ByteArrayToHexString(hash)));
        }
    }

    private void BtnHashAll_Click(object sender, EventArgs e)
    {
        HashAll();
    }

    private void TxtData_TextChanged(object sender, EventArgs e)
    {
        rbTextBox.Checked = true;
    }

    private void BtnSaveKeys_Click(object sender, EventArgs e)
    {
        try
        {
            File.WriteAllText(Application.StartupPath + "PublicKey." + cmbCryptoAlgorithm.Text + ".pem",
                txtPublicKey.Text);
            File.WriteAllText(Application.StartupPath + "PrivateKey." + cmbCryptoAlgorithm.Text + ".pem",
                txtPrivateKey.Text);
            LogMsg("Keys Saved");
        }
        catch (Exception ex)
        {
           LogMsg(ex);
        }
    }

    private void BtnLoadKeys_Click(object sender, EventArgs e)
    {
        try
        {
            txtPublicKey.Text =
                File.ReadAllText(Application.StartupPath + "PublicKey." + cmbCryptoAlgorithm.Text + ".pem");
            txtPrivateKey.Text =
                File.ReadAllText(Application.StartupPath + "PrivateKey." + cmbCryptoAlgorithm.Text + ".pem");
            LogMsg("Keys Loaded");
        }
        catch (Exception ex)
        {
           LogMsg(ex);
        }
    }

    private void BtnLoadFile_Click(object sender, EventArgs e)
    {
        btnLoadFile.Enabled = false;

        OpenFileDialog dlg = new();
        dlg.Title = "File";
        dlg.DefaultExt = "*";
        dlg.Filter = "Files|*.*";
        dlg.FileName = string.Empty;
        dlg.Multiselect = false;
        try
        {
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fileBuffer = File.ReadAllBytes(dlg.FileName);
                LogMsg("File Loaded (" + dlg.FileName + ") Size is " + fileBuffer.Length);
            }

            rbFileBuffer.Checked = true;
        }
        catch (Exception ex)
        {
           LogMsg(ex);
        }

        btnLoadFile.Enabled = true;
    }

    private void BtnClear_Click(object sender, EventArgs e)
    {
        ClearControls(this);
        LoadCharset(lblCurCharset.Text);
        lbSoras.SelectedIndex = -1;
        lbSoras.SelectedIndex = 0;
    }

    private void BtnHash_Click(object sender, EventArgs e)
    {
        var hash = MyClass.GetHash(GetData(), cmbHash.Text);
        txtHash.Text = MyClass.ByteArrayToHexString(hash);
    }

    private byte[] GetData()
    {
        var data = Array.Empty<byte>();
        try
        {
            if (rbFileBuffer.Checked) return fileBuffer;

            data = cmbData.SelectedIndex switch
            {
                0 => Convert.FromBase64String(txtData.Text),
                2 => MyClass.HexStringToByteArray(txtData.Text),
                _ => Encoding.ASCII.GetBytes(txtData.Text),
            };
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }

        return data;
    }

    private string SetData(byte[] data)
    {
        string sData = cmbData.SelectedIndex switch
        {
            0 => Convert.ToBase64String(data),
            2 => MyClass.ByteArrayToHexString(data),
            _ => Encoding.ASCII.GetString(data),
        };
        return sData;
    }

    private void BtnEncrypt_Click(object sender, EventArgs e)
    {
        try
        {
            switch (cmbCryptoAlgorithm.SelectedIndex)
            {
                case 0:
                    publicKey = PEMClass.ImportPublicKey(txtPublicKey.Text);
                    privateKey = PEMClass.ImportPrivateKey(txtPrivateKey.Text);
                    rsaClass.SetKey(publicKey, false);
                    rsaClass.SetKey(privateKey, true);

                    txtOutput.Text = Convert.ToBase64String(rsaClass.Encrypt(GetData(),
                        cmbEncryptionKey.SelectedIndex == 1, chkPadding.Checked));

                    LogMsg("Encrypted");
                    break;
                case 1:
                    LogMsg("Algorithm not supported");
                    break;
                default:
                    LogMsg("Algorithm not implemented");
                    break;
            }
        }
        catch (Exception ex)
        {
           LogMsg(ex);
        }
    }

    private void BtnDecrypt_Click(object sender, EventArgs e)
    {
        try
        {
            switch (cmbCryptoAlgorithm.SelectedIndex)
            {
                case 0:
                    publicKey = PEMClass.ImportPublicKey(txtPublicKey.Text);
                    privateKey = PEMClass.ImportPrivateKey(txtPrivateKey.Text);
                    rsaClass.SetKey(publicKey, false);
                    rsaClass.SetKey(privateKey, true);

                    txtData.Text = SetData(rsaClass.Decrypt(Convert.FromBase64String(txtOutput.Text),
                        cmbEncryptionKey.SelectedIndex == 0, chkPadding.Checked));

                    LogMsg("Decrypted");
                    break;
                case 1:
                    LogMsg("Algorithm not supported");
                    break;
                default:
                    LogMsg("Algorithm not implemented");
                    break;
            }
        }
        catch (Exception ex)
        {
           LogMsg(ex);
        }
    }

    private void BtnVerify_Click(object sender, EventArgs e)
    {
        try
        {
            byte[] hash;
            switch (cmbCryptoAlgorithm.SelectedIndex)
            {
                case 0:
                    hash = MyClass.GetHash(GetData(), cmbHash.Text);
                    txtHash.Text = MyClass.ByteArrayToHexString(hash);

                    rsaClass.SetKey(PEMClass.ImportPublicKey(txtPublicKey.Text), false);
                    if (!string.IsNullOrEmpty(txtPrivateKey.Text))
                        rsaClass.SetKey(PEMClass.ImportPrivateKey(txtPrivateKey.Text), true);

                    LogMsg(rsaClass.Verify(hash, Convert.FromBase64String(txtOutput.Text), cmbHash.Text)
                        ? "Verification Successful"
                        : "Verification Failed");
                    break;
                case 1:
                    hash = MyClass.GetHash(GetData(), cmbHash.Text);
                    txtHash.Text = MyClass.ByteArrayToHexString(hash);

                    dsaClass.SetKey(PEMClass.ImportDSAPublicKey(txtPublicKey.Text), false);
                    dsaClass.SetKey(PEMClass.ImportDSAPrivateKey(txtPrivateKey.Text), true);

                    LogMsg(dsaClass.VerifySignature(hash, Convert.FromBase64String(txtOutput.Text), cmbHash.Text)
                        ? "Verification Successful"
                        : "Verification Failed");
                    break;
                default:
                    LogMsg("Algorithm not implemented");
                    break;
            }
        }
        catch (Exception ex)
        {
           LogMsg(ex);
        }
    }

    private void BtnSign_Click(object sender, EventArgs e)
    {
        try
        {
            byte[] hash;
            switch (cmbCryptoAlgorithm.SelectedIndex)
            {
                case 0:
                    hash = MyClass.GetHash(GetData(), cmbHash.Text);
                    txtHash.Text = MyClass.ByteArrayToHexString(hash);

                    rsaClass.SetKey(PEMClass.ImportPublicKey(txtPublicKey.Text), false);
                    rsaClass.SetKey(PEMClass.ImportPrivateKey(txtPrivateKey.Text), true);

                    txtOutput.Text = Convert.ToBase64String(rsaClass.Sign(hash, cmbHash.Text));

                    LogMsg("Signed");
                    break;
                case 1:
                    hash = MyClass.GetHash(GetData(), cmbHash.Text);
                    txtHash.Text = MyClass.ByteArrayToHexString(hash);

                    dsaClass.SetKey(PEMClass.ImportDSAPublicKey(txtPublicKey.Text), false);
                    dsaClass.SetKey(PEMClass.ImportDSAPrivateKey(txtPrivateKey.Text), true);

                    txtOutput.Text = Convert.ToBase64String(dsaClass.SignData(hash, cmbHash.Text));

                    LogMsg("Signed");
                    break;
                default:
                    LogMsg("Algorithm not implemented");
                    break;
            }
        }
        catch (Exception ex)
        {
           LogMsg(ex);
        }
    }

    private void CmbData_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblData.Text = "Data (" + dataType[cmbData.SelectedIndex] + ")";
    }

    #endregion

    #region Calculator

    private static string FactorMessage(string code)
    {
        var msg = "";
        switch (code)
        {
            case "C":
                msg = " Composite, no factors known";
                break;
            case "CF":
                msg = "Composite, factors known";
                break;
            case "FF":
                msg = "Composite, fully factored";
                break;
            case "P":
                msg = "Definitely prime";
                break;
            case "PRP":
            case "Prp":
                msg = "Probably prime";
                break;
            case "U":
                msg = "Unknown";
                break;
            case "Unit":
                msg = "Just for \"1\"";
                break;
            case "N":
                msg = "This number is not in database(and was not added due to your settings)";
                break;
            case "*":
                msg = "Added to database during this request";
                break;
        }

        return msg;
    }

    private void BtnToRSA_Click(object sender, EventArgs e)
    {
        rb16.Checked = true;
        GetNumbers();
        if (BigIntegerHelper.IsProbablePrime(P, 100) && BigIntegerHelper.IsProbablePrime(Q, 100))
        {
            txtP.Text = txtPrimeP.Text;
            txtQ.Text = txtPrimeQ.Text;
            txtE.Text = "10001";
            tabControl1.SelectedTab = tabRSA;
            BtnCalcD_Click(sender, e);
            BtnExportPublic_Click(sender, e);
            BtnExportPrivate_Click(sender, e);
        }
        else
        {
            LogMsg("Not Primes !");
        }
    }

    private void TabControl2_Click(object sender, EventArgs e)
    {
        if (tabControl2.SelectedTab == tabWebBrowser)
        {
            var data = File.ReadAllText(htmlFile);
            var p = data.FindString("value=", 0);
            if (p > 0)
            {
                var number = data.ExtractData("\"", ref p, 7);
                webBrowser.Navigate(factorDbURL + number);
            }
            //webBrowser.Navigate(htmlFile);
        }
    }

    private FactorData ExtractFactorData(string data)
    {
        FactorData factorData = new();

        var p = 0;
        try
        {
            p = data.FindString("<table ", p);
            p = data.FindString("<tr>", p + 1, 3);
            p = data.FindString("<td>", p + 1);
            factorData.code = data.ExtractData("<", ref p, 4);
            p = data.FindString("<td>", p + 1);
            factorData.digits = data.ExtractData("<", ref p, 4);
            p = data.FindString("<td>", p + 1);
            p = data.FindString("> = <", p + 1);
            factorData.factors = 0;
            if (p > 0) p += 3;
            while (p > 0 && !data.Substring(p + 1, 5).Equals("</td>"))
            {
                p = data.FindString(">", p + 1, 2);
                p++;
                var str = data.ExtractData("<", ref p, 0).Trim();
                factorData.factorList.Add(str);
                var n = str.IndexOf("^");
                if (n > 0) factorData.factors += int.Parse(str[(n + 1)..]);
                else factorData.factors++;

                p = data.FindString(">", p + 1, 2);
                if (data.Substring(p + 1, 5).Equals("<sub>"))
                {
                    p = data.FindString("</sub>", p);
                    p += 5;
                    //factorData.factors += 100;
                }
            }

            ;
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }

        return factorData;
    }

    private FactorData FactorDB(string prime, WebMethod methodIndex)
    {
        var data = "";
        var url = factorDbURL + prime;
        var userAgent = "QuranViewUtility";
        const int timeOut = 50; // 5 seconds

        FactorData factorData;

        switch ((int)methodIndex)
        {
            case 0:
                _ = MyWeb.HTTPReadPageAsync(url, userAgent, result => data = result);
                var i = 0;
                while (data.Equals("") && i < timeOut)
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                    i++;
                }

                break;
            case 1:
                data = MyWeb.WebClientReadPage(url, userAgent);
                break;
            case 2:
                data = MyWeb.WebRequestReadPage(url, userAgent);
                break;
            case 3:
                Process.Start(url);
                break;
        }

        File.WriteAllText(htmlFile, data);
        factorData = ExtractFactorData(data);
        factorData.number = prime;

        return factorData;
    }

    private void DisplayFactors(FactorData factorData)
    {
        OutputMsg(Environment.NewLine +
                  "Number= " + factorData.number + Environment.NewLine +
                  "Code= " + factorData.code + " , [" + FactorMessage(factorData.code) + "]" + Environment.NewLine +
                  "Digits= " + factorData.digits + Environment.NewLine +
                  "Factors (" + factorData.factors + ") = " + string.Join(",", factorData.factorList) +
                  Environment.NewLine +
                  "Charset = " + GetCharset());
    }

    private void BtnFactorDbP_Click(object sender, EventArgs e)
    {
        rb10.Checked = true;
        DisplayFactors(FactorDB(txtPrimeP.Text, (WebMethod)cmbWebClient.SelectedIndex));
    }

    private void BtnFactorDbQ_Click(object sender, EventArgs e)
    {
        rb10.Checked = true;
        DisplayFactors(FactorDB(txtPrimeQ.Text, (WebMethod)cmbWebClient.SelectedIndex));
    }

    void GetFactors(BigInteger n)
    {
        Stopwatch sw = new();
        txtResultR.Text = "";
        sw.Start();
        List<BigIntegerHelper.Factors> factors = new();

        //var factors = BigIntegerHelper.GetFactors(P);
        var thread = new Thread(o => factors = BigIntegerHelper.Factorize(n));
        thread.Start();
        while (thread.IsAlive)
        {
            Application.DoEvents(); Thread.Sleep(100);
            lblStatus.Text = sw.Elapsed.Hours + " : " + sw.Elapsed.Minutes.ToString() + " : " + sw.Elapsed.Seconds.ToString() + " : " + sw.Elapsed.Milliseconds;
        }
        sw.Stop();
        foreach (var factor in factors) txtResultR.Text += factor.Factor + " ^ " + factor.Count + " * ";
        if (txtResultR.Text.Length > 3) txtResultR.Text = txtResultR.Text[0..^3];
    }
    void GetPrimes(BigInteger n)
    {
        Stopwatch sw = new();
        txtResultR.Text = "";
        sw.Start();
        SortedSet<BigInteger> primes = new();

        //var factors = BigIntegerHelper.GetFactors(P);
        var thread = new Thread(o => primes = BigIntegerHelper.GetPrimes_SieveOfAtkin(n));
        thread.Start();
        while (thread.IsAlive)
        {
            Application.DoEvents(); Thread.Sleep(100);
            lblStatus.Text = sw.Elapsed.Hours + " : " + sw.Elapsed.Minutes.ToString() + " : " + sw.Elapsed.Seconds.ToString() + " : " + sw.Elapsed.Milliseconds;
        }
        sw.Stop();
        foreach (var prime in primes) txtResultR.Text += prime + " , ";
        if (txtResultR.Text.Length > 3) txtResultR.Text = txtResultR.Text[0..^3];
    }

    private void BtnGetPrimes_Click(object sender, EventArgs e)
    {
        btnGetPrimes.Enabled = false;
        GetNumbers();
        GetPrimes(P);
        btnGetPrimes.Enabled = true;
    }

    private void BtnFactorizeP_Click(object sender, EventArgs e)
    {
        btnFactorizeP.Enabled = false;
        btnFactorizeQ.Enabled = false;
        GetNumbers();
        GetFactors(P);
        btnFactorizeP.Enabled = true;
        btnFactorizeQ.Enabled = true;
    }

    private void BtnFactorizeQ_Click(object sender, EventArgs e)
    {
        btnFactorizeP.Enabled = false;
        btnFactorizeQ.Enabled = false;
        GetNumbers();
        GetFactors(Q);
        btnFactorizeP.Enabled = true;
        btnFactorizeQ.Enabled = true;
    }

    private void CmbWebClient_SelectedIndexChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "WebClient", cmbWebClient.Text);
    }

    private void TxtPrimeP_DoubleClick(object sender, EventArgs e)
    {
        File.WriteAllText(Application.StartupPath + "tmp.txt", txtPrimeP.Text);
        Process.Start(Application.StartupPath + "tmp.txt");
    }

    private void TxtPrimeQ_DoubleClick(object sender, EventArgs e)
    {
        File.WriteAllText(Application.StartupPath + "tmp.txt", txtPrimeQ.Text);
        Process.Start(Application.StartupPath + "tmp.txt");
    }

    private void TxtModulN_TextChanged(object sender, EventArgs e)
    {
    }

    private void TxtModulN_DoubleClick(object sender, EventArgs e)
    {
        File.WriteAllText(Application.StartupPath + "tmp.txt", txtModulN.Text);
        Process.Start(Application.StartupPath + "tmp.txt");
    }

    private void TxtResultR_DoubleClick(object sender, EventArgs e)
    {
        File.WriteAllText(Application.StartupPath + "tmp.txt", txtResultR.Text);
        Process.Start(Application.StartupPath + "tmp.txt");
    }

    private int GetNumIndex()
    {
        if (rb10.Checked) return 1;
        if (rb16.Checked) return 2;
        if (rb64b.Checked) return 3;
        return 0; // rb2
    }

    private void RbNumberBaseCheckedChanged(object sender, EventArgs e)
    {
        if ((sender as RadioButton).Checked)
        {
            GetNumbers();
            txtResultR.Text = "";
            CurNum = GetNumIndex();
            ShowNumbers();
            ShowResult();
        }
    }

    private static bool NotZero(string num)
    {
        return num.Length > 0 && !num.Equals("00") && !num.Equals("0");
    }
    

    private void TxtPrimeP_TextChanged(object sender, EventArgs e)
    {
        GetNumbers();
        var n = P.GetActualBitwidth();
        if (NotZero(txtPrimeP.Text))
            lblStatus.Text = "P = " + txtPrimeP.Text.Length + " - " + n + " / " + P.GetBitwidth();
    }

    private void TxtPrimeQ_TextChanged(object sender, EventArgs e)
    {
        GetNumbers();
        var n = Q.GetActualBitwidth();
        if (NotZero(txtPrimeQ.Text))
            lblStatus.Text = "Q = " + txtPrimeQ.Text.Length + " - " + n + " / " + Q.GetBitwidth();
    }

    private void BtnBezout_Click(object sender, EventArgs e)
    {
        GetNumbers();
        var bzt = BigIntegerHelper.GcdWithBezout(P, Q);
        if (NotZero(txtResultR.Text)) txtResultR.Text = "P . " + bzt[1] + " + Q ." + bzt[2] + " = " + bzt[0];
    }

    private void BtnOR_Click(object sender, EventArgs e)
    {
        GetNumbers();
        R = P | Q;
        ShowResult();
    }

    private void BtnXOR_Click(object sender, EventArgs e)
    {
        GetNumbers();
        R = P ^ Q;
        ShowResult();
    }

    private void BtnReverseP_Click(object sender, EventArgs e)
    {
        GetNumbers();
        P = BigIntegerHelper.GetBig(P.ToByteArray(), chkPositives.Checked);
        ShowNumbers();
    }

    private void BtnReverseQ_Click(object sender, EventArgs e)
    {
        GetNumbers();
        Q = BigIntegerHelper.GetBig(Q.ToByteArray(), chkPositives.Checked);
        ShowNumbers();
    }

    private void BtnIsEven_Click(object sender, EventArgs e)
    {
        GetNumbers();
        txtResultR.Text = "P is " + ((P & 1) == 1 ? "Odd" : "Even") + Environment.NewLine +
                          "Q is " + ((Q & 1) == 1 ? "Odd" : "Even");
    }

    private void BtnGenPrime_Click(object sender, EventArgs e)
    {
        //P = BigIntegerHelper.GenPrime(int.Parse(cmbKeyLen.Text));
        using RSACryptoServiceProvider csp = new(int.Parse(cmbKeyLen.Text));
        P = BigIntegerHelper.GetBig(csp.ExportParameters(true).P);
        Q = BigIntegerHelper.GetBig(csp.ExportParameters(true).Q);

        ShowNumbers();
    }

    private void BtnSHL_Click(object sender, EventArgs e)
    {
        GetNumbers();
        P <<= 1;
        ShowNumbers();
    }

    private void BtnSHR_Click(object sender, EventArgs e)
    {
        GetNumbers();
        P >>= 1;
        ShowNumbers();
    }

    private void BtnIsPrime_Click(object sender, EventArgs e)
    {
        GetNumbers();
        txtResultR.Text = "P is " + (BigIntegerHelper.IsProbablePrime(P, 100) ? "Prime" : "NOT Prime") +
                          Environment.NewLine +
                          "Q is " + (BigIntegerHelper.IsProbablePrime(Q, 100) ? "Prime" : "NOT Prime");
    }

    private void GetNumbers()
    {
        if (entry > 0) return;
        txtResultR.Text = "";
        Application.DoEvents();
        entry++;
        try
        {
            P = txtPrimeP.Text.ConvertFrom((BigIntegerHelper.NumberFormat)CurNum, chkPositives.Checked);
            Q = txtPrimeQ.Text.ConvertFrom((BigIntegerHelper.NumberFormat)CurNum, chkPositives.Checked);
            N = txtModulN.Text.ConvertFrom((BigIntegerHelper.NumberFormat)CurNum, chkPositives.Checked);
            R = txtResultR.Text.ConvertFrom((BigIntegerHelper.NumberFormat)CurNum, chkPositives.Checked);
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }

        entry--;
    }


    private void ShowNumbers()
    {
        entry++;
        var format = (BigIntegerHelper.NumberFormat)GetNumIndex();

        txtPrimeP.Text = P.ConvertTo(format);
        txtPrimeQ.Text = Q.ConvertTo(format);
        txtModulN.Text = N.ConvertTo(format);

        if (P == 0) txtPrimeP.Text = "";
        if (Q == 0) txtPrimeQ.Text = "";
        if (N == 0) txtModulN.Text = "";
        entry--;
    }

    private void ShowResult()
    {
        txtResultR.Text = R.ConvertTo((BigIntegerHelper.NumberFormat)CurNum);

        //if (R== 0) txtResultR.Text = "";
        //            byte[] RR = R.ToByteArray();
        //            Array.Reverse(RR);
        //            txtResultR.Text = MyClass.BinaryToHexString(RR);
    }

    private void BtnClearCalc_Click(object sender, EventArgs e)
    {
        txtPrimeP.Text = "";
        txtPrimeQ.Text = "";
        txtModulN.Text = "";
        txtResultR.Text = "";
    }

    private void BtnSqrt_Click(object sender, EventArgs e)
    {
        GetNumbers();
        R = P.Sqrt();
        ShowResult();
    }

    private void CmbAccelerator_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cmbAccelerator.SelectedIndex > 0)
            {
                txtInfo.Text = GPUClass.AcceleratorInfo(cmbAccelerator.SelectedIndex);
                LblThreads.Text = GPUClass.MaxThreads(cmbAccelerator.SelectedIndex).ToString();
            }
            else
            {
                txtInfo.Text = " Direct Calculation";
                LblThreads.Text = "1";
            }
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }
    private void BtnKillThread_Click(object sender, EventArgs e)
    {
        gpuClass.Abort = true;

    }

    // event handler
    public void GPUClass_ProcessCompleted(object sender, GpuEventArgs e)
    {
        LogMsg("Operation Completed with : " + e.Accelerator);
        if (tabControl2.InvokeRequired)
            tabControl2.BeginInvoke(new MethodInvoker(delegate { tabControl2.SelectedTab = tabOutput; }));
        else
            tabControl2.SelectedTab = tabOutput;
        GpuClass_ProcessProgress(sender, e);
    }


    private void GpuClass_ProcessProgress(object sender, GpuEventArgs e)
    {
        string msg;
        if (e.Accelerator.Equals("{Started}"))
        {
            gpuStarted = true;
            msg = "Started";
        }
        else
        if (e.Progress == ulong.MaxValue)
            msg = "Kernel Error";
        else msg = e.Progress.ToString() + " E" + GPUSCL;

        if (lblProgress.InvokeRequired)
            lblProgress.BeginInvoke(new MethodInvoker(delegate { lblProgress.Text = msg; }));
        else
            lblProgress.Text = msg;
    }

    private void BtnGPUFindAllPoly_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtResultR.Text == "") txtResultR.Text = "10";
            if (txtModulN.Text == "") txtModulN.Text = (txtPrimeQ.Text.Length * 4).ToString();
            string sR = txtResultR.Text;
            GetNumbers();
            txtResultR.Text = sR;

            if (txtPrimeP.Text=="" || txtPrimeQ.Text=="")
            {
                LogMsg("Set HexByteArray in P, CRC in Q,crc width in N,and Max Results in R. Click FindPoly button! ");
                return;
            }
            if (cmbAccelerator.SelectedIndex == 0)
            {
                if (cmbSourceEnc.SelectedIndex < 0)
                {
                    SelectEncoding("65001", cmbSourceEnc);
                    SelectEncoding("1252");
                }
                /*
                 IDisposable byteProvider = hexBox.ByteProvider as IDisposable;
                 if (byteProvider != null) byteProvider.Dispose();
                 hexBox.ByteProvider = null;
                */
                string localFile = Application.StartupPath + "tmp.bin";
                tabControl1.SelectedTab = tabHexViewer;
                File.WriteAllBytes(localFile, MyClass.HexStringToByteArray(txtPrimeP.Text));
                SendToHexViewer(localFile);
                File.Delete(localFile);
                TxtCRC.Text = txtPrimeQ.Text;
                TxtPolyWidth.Text = txtModulN.Text;
                LogMsg("Using CPU to find poly, choose Accelerator to search in calculator");
            }
            else
            {
                BtnGPUFindAllPoly.Enabled = false;
                byte width = Convert.ToByte(txtModulN.Text);
                OutputMsg("Searching for Poly using " + cmbAccelerator.Text);
                Stopwatch sw = new();
                ulong[] polyList=Array.Empty<ulong>();

                string acc = cmbAccelerator.Text;
                byte[] data = MyClass.HexStringToByteArray(txtPrimeP.Text);
                int maxThreads = GPUClass.MaxThreads(cmbAccelerator.SelectedIndex);
                //if (!Int32.TryParse(CmbThreads.Text,out int maxThreads)) maxThreads = 100000; 
                if (!Int32.TryParse(txtResultR.Text, out  int maxResults) || maxResults==0) maxResults = 10;

                BtnKillThread.Enabled = true;
                gpuStarted = false;
                lblProgress.Text = "Starting";

                if (ChkMonitor.Checked)
                {
                    if (GPUClass.GetAccelerator(acc).AcceleratorType.ToString().Contains("Cuda"))
                        gpuThread = new Thread(o => polyList = gpuClass.CudaFindAllPolyMonitored(acc, data, width, (ulong)Q, maxThreads, maxResults));
                    else if (GPUClass.GetAccelerator(acc).AcceleratorType.ToString().Contains("OpenCL"))
                        gpuThread = new Thread(o => polyList = gpuClass.OpenCLFindAllPolyMonitored(acc, data, width, (ulong)Q, maxThreads, maxResults));
                }
                else
                    gpuThread = new Thread(o => polyList = gpuClass.GPUFindAllPoly(acc, data, width, (ulong)Q, maxThreads, maxResults));

                gpuThread.Start();

                while (!gpuStarted && !gpuClass.Abort)
                {
                    Application.DoEvents(); Thread.Sleep(50);
                }

                sw.Start();

                while (gpuThread.IsAlive)
                    { 
                        Application.DoEvents(); Thread.Sleep(100);
                        lblStatus.Text = sw.Elapsed.Hours + " : " + sw.Elapsed.Minutes.ToString() + " : " + sw.Elapsed.Seconds.ToString() + " : " + sw.Elapsed.Milliseconds;
                    }

                sw.Stop();
                BtnKillThread.Enabled = false ;
                OutputMsg("Elapsed Time (ms) :" + sw.ElapsedMilliseconds.ToString());
                OutputMsg("Found " + polyList.Length.ToString() + " Polys, crc ( "+txtPrimeQ.Text +" ) ,  width : " + width.ToString());
                foreach (var poly in polyList) OutputMsg(poly.ToString("X" + (width / 8 + (width % 8 != 0 ? 1 : 0))));
                
                BtnGPUFindAllPoly.Enabled = true;
                LogMsg("Check Output Tab for results");
                
            }
           // ShowResult();
            txtResultR.Text = sR;
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }


    private void BtnADD_Click(object sender, EventArgs e)
    {
        try
        {
            GetNumbers();
            if (cmbAccelerator.SelectedIndex == 0)
                R = BigInteger.Add(P, Q);
            else
                R = gpuClass.Calc(cmbAccelerator.Text, P, Q, '+');

            ShowResult();
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void BtnSUB_Click(object sender, EventArgs e)
    {
        try
        {
            GetNumbers();

            if (cmbAccelerator.SelectedIndex == 0)
                R = BigInteger.Subtract(P, Q);
            else
                R = gpuClass.Calc(cmbAccelerator.Text, P, Q, '-');

            ShowResult();
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void BtnMUL_Click(object sender, EventArgs e)
    {
        try
        {
            GetNumbers();

            if (cmbMULMethod.SelectedIndex == 0)
                if (cmbAccelerator.SelectedIndex == 0)
                    R = BigInteger.Multiply(P, Q);
                else
                    R = gpuClass.Calc(cmbAccelerator.Text, P, Q, '*');
            else
                R = BigIntegerHelper.KaratsubaMultiply(P, Q);

            ShowResult();
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void BtnDIV_Click(object sender, EventArgs e)
    {
        try
        {
            GetNumbers();

            if (cmbAccelerator.SelectedIndex == 0)
                R = BigInteger.Divide(P, Q);
            else
                R = gpuClass.Calc(cmbAccelerator.Text, P, Q, '/');

            ShowResult();
        }
        catch (Exception ex)
        {
           LogMsg(ex);
        }
    }

    private void BtnMOD_Click(object sender, EventArgs e)
    {
        try
        {
            GetNumbers();

            if (cmbAccelerator.SelectedIndex == 0)
                R = BigInteger.Remainder(P, Q);
            else
                R = gpuClass.Calc(cmbAccelerator.Text, P, Q, '%');

            ShowResult();
        }
        catch (Exception ex)
        {
           LogMsg(ex);
        }
    }

    private void BtnPOW_Click(object sender, EventArgs e)
    {
        try
        {
            GetNumbers();

            switch (cmbExponentMethod.SelectedIndex)
            {
                case 0:
                    if (cmbAccelerator.SelectedIndex == 0)
                    {
                        int E;
                        R = 1;
                        do
                        {
                            if (Q > int.MaxValue) E = int.MaxValue;
                            else E = (int)Q;
                            Q = BigInteger.Subtract(Q, E);
                            R = BigInteger.Multiply(R, BigInteger.Pow(P, E));
                        } while (Q > 0);
                    }
                    else
                    {
                        R = gpuClass.Calc(cmbAccelerator.Text, P, Q, '^');
                    }

                    break;
                case 1:
                    R = BigIntegerHelper.PowBySquaring(P, Q);
                    break;
                case 2:
                    R = BigIntegerHelper.FastPow(P, Q);
                    break;
            }

            ShowResult();
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void BtnPWM_Click(object sender, EventArgs e)
    {
        try
        {
            GetNumbers();

            R = BigInteger.ModPow(P, Q, N);

            ShowResult();
        }
        catch (Exception ex)
        {
           LogMsg(ex);
        }
    }

    private void BtnRTP_Click(object sender, EventArgs e)
    {
        P = R;
        txtPrimeP.Text = txtResultR.Text;
    }

    private void BtnFCD_Click(object sender, EventArgs e)
    {
        try
        {
            GetNumbers();

            R = BigInteger.GreatestCommonDivisor(P, Q);

            ShowResult();
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void BtnLCM_Click(object sender, EventArgs e)
    {
        try
        {
            GetNumbers();

            R = BigInteger.Divide(BigInteger.Abs(BigInteger.Multiply(P, Q)), BigInteger.GreatestCommonDivisor(P, Q));

            ShowResult();
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    #endregion

    #region Encoding

    /// <summary>
    ///     Can we run the conversion
    /// </summary>
    private void ValidateForRun()
    {
        try
        {
            //enable / disable the run button
            btnRun.Enabled = (_sourceFiles != null && _sourceFiles.Length > 0 || rtxtData.Text.Length > 0)
                             && cmbSourceEnc.SelectedIndex >= 0
                             && cmbDestEnc.SelectedIndex >= 0
                             && cmbSourceEnc.Text != cmbDestEnc.Text &&
                             !cmbSourceEnc.Text.Contains("Jommal");
            //&& !(Converter.IsACCO(Converter.GetEncodingEx(cmbSourceEnc.Text)) && Converter.IsACCO(Converter.GetEncodingEx(cmbDestEnc.Text)));
            if (btnRun.Enabled)
            {
                if (Converter.IsACCO(Converter.GetEncodingEx(cmbDestEnc.Text))) chkHexText.Checked = true;
                if (!Converter.IsACCO(Converter.GetEncodingEx(cmbSourceEnc.Text)))
                {
                    chkDiscardChars.Checked = true;
                    if (chkHexText.Checked && (cmbDestEnc.Text.Contains("Vocal") ||
                                               cmbDestEnc.Text.Contains("Hijaei") ||
                                               cmbDestEnc.Text.Contains("Abjadi")))
                        chkDiacritics.Checked = true;
                    else chkDiacritics.Checked = false;
                }
                else
                {
                    chkDiscardChars.Checked = false;
                }
            }
            else
            {
                chkHexText.Checked = false;
            }

            if (_sourceFiles != null)
                //set the info about the destination directory
                if (_sourceFiles.Length > 0 && cmbDestEnc.SelectedIndex >= 0)
                {
                    //john church 05/10/2008 use directory separator char instead of backslash for linux support
                    destDir =
                        _sourceFiles[0][..(_sourceFiles[0].LastIndexOf(Path.DirectorySeparatorChar) + 1)] +
                        cmbDestEnc.SelectedItem + Path.DirectorySeparatorChar;
                    LogMsg("Converted files will be output to:"
                           + Environment.NewLine + destDir
                           + Environment.NewLine + _sourceFiles.Length
                           + " file(s) selected for conversion" + Environment.NewLine);
                }
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    /// <summary>
    ///     Populate the combos
    /// </summary>
    private void LoadEncodings()
    {
        Encoding enc;
        string s;
        cmbDestEnc.Items.Clear();
        cmbSourceEnc.Items.Clear();
        cmbHEncoding.Items.Clear();

        cmbSourceEnc.Items.Add("Arabic [Jommal] - 65538");
        cmbDestEnc.Items.Add("Arabic [Jommal] - 65538");

        var customCharsets = Directory.GetFiles(Application.StartupPath, "*.Charset");
        foreach (var file in customCharsets)
        {
            cmbSourceEnc.Items.Add("Arabic [" + Path.GetFileNameWithoutExtension(file) + "] - 65537");
            cmbDestEnc.Items.Add("Arabic [" + Path.GetFileNameWithoutExtension(file) + "] - 65537");
            cmbHEncoding.Items.Add("Arabic [" + Path.GetFileNameWithoutExtension(file) + "] - 65537");
        }

        //loop through all the encodings on the system
        foreach (var en in Encoding.GetEncodings())
        {
            enc = en.GetEncoding();
            //build a string containing the name and codepage number
            s = enc.EncodingName + " - " + enc.CodePage;
            //add it to the combos
            if (chkALLEncodings.Checked || s.Contains("Arabic") || s.Contains("UTF") || s.Contains("UNICODE"))
            {
                cmbSourceEnc.Items.Add(s);
                cmbDestEnc.Items.Add(s);
                cmbHEncoding.Items.Add(s);
            }
        }

        //sort them alphabetically
        cmbDestEnc.Sorted = true;
        cmbSourceEnc.Sorted = true;
        cmbHEncoding.Sorted = true;
    }

    private void ChkJommalWORD_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "JommalWORD", chkJommalWord.Checked ? "Yes" : "No");
        if (cmbBytesPerLine.SelectedIndex>0)
            hexBox.BytesPerLine = int.Parse(cmbBytesPerLine.Text);
        else
            hexBox.BytesPerLine = 0;
        hexBox.Refresh();
    }

    private void CmbSpace_SelectedIndexChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "SpaceChar", cmbSpace.Text);
    }

    private void ChkShowSpace_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "ShowSpace", ChkShowSpace.Checked ? "Yes" : "No");
    }

    private void ChkMultiLine_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "MultiLine", chkMultiLine.Checked ? "Yes" : "No");
    }

    private void ChkStrechedLines_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "StrechedLines", chkStrechedLines.Checked ? "Yes" : "No");
    }

    private void CmbCoding_SelectedIndexChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "CharCoding", cmbCoding.SelectedIndex.ToString());
    }
    private void CmbCRC8_SelectedIndexChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "CRC8", cmbCRC.SelectedIndex.ToString());
    }
    private void ChkSplitWords_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "SplitWords", chkSplitWords.Checked ? "Yes" : "No");
    }
    private void ChkUseGPU_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "UseGPU", ChkUseGPU.Checked ? "Yes" : "No");
    }

    private void ChkShowAsTable_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "ShowAsTable", chkShowAsTable.Checked ? "Yes" : "No");
    }

    private void BtnLoadFromClipboard_Click(object sender, EventArgs e)
    {
        if (Clipboard.ContainsText())
        {
            File.WriteAllText(Application.StartupPath + "NewCharset.Charset", ""+ ISettings.NewLineSep+Clipboard.GetText());
            LoadCharset(Application.StartupPath + "NewCharset.Charset");
        }
    }

    private void BtnSImage_Click(object sender, EventArgs e)
    {
        tabControl1.SelectedTab = tabTexture;
        DrawImage();
        CreateBarcode(rtxtData.Text);
    }

    private void BtnSendToCrypto_Click(object sender, EventArgs e)
    {
        try
        {
            fileBuffer = new byte[hexBox.ByteProvider.Length];
            for (var i = 0; i < hexBox.ByteProvider.Length; i++) fileBuffer[i] = hexBox.ByteProvider.ReadByte(i);
            rbFileBuffer.Checked = true;
            tabControl1.SelectedTab = tabCrypto;
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void BtnColor_Click(object sender, EventArgs e)
    {
        tabControl1.SelectedTab = tabColor;
        DrawColors();
    }

    private static void NormalizeBuffer(byte[] buffer)
    {
        int max = buffer.Max();
        int min = buffer.Min();
        var mid = (byte)((max + min) / 2);
        for (var i = 0; i < buffer.Length; i++)
            buffer[i] += (byte)(128 - mid);
    }

    private void BtnSpectrum_Click(object sender, EventArgs e)
    {
        tabControl1.SelectedTab = tabAudioSpectrum;
        InjectAudio();
    }


    private void BtnSCrypto_Click(object sender, EventArgs e)
    {
        rbFileBuffer.Checked = true;
        fileBuffer = SafeRead(quranBin);
        tabControl1.SelectedTab = tabCrypto;
    }

    private void BtnSHex_Click(object sender, EventArgs e)
    {
        SendToHexViewer(quranBin);
        tabControl1.SelectedTab = tabHexViewer;
    }

    private void BtnSendToHex_Click(object sender, EventArgs e)
    {
        if (rbFileBuffer.Checked)
        {
            File.WriteAllBytes(Application.StartupPath + "Buffer.bin", fileBuffer);
            SendToHexViewer(Application.StartupPath + "Buffer.bin");
            tabControl1.SelectedTab = tabHexViewer;
        }
    }

    private void ChkSendToBuffer_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "SendToBuffer", chkSendToBuffer.Checked ? "Yes" : "No");
    }

    private void TabEncoding_Leave(object sender, EventArgs e)
    {
        hex = txtInfo.Text;
    }

    private void TabEncoding_Enter(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(hex)) txtInfo.Text = hex;
    }

    private void BtnToP_Click(object sender, EventArgs e)
    {
        Clipboard.SetText(txtInfo.Text);
        if (tabControl1.SelectedTab == tabEncoding && txtInfo.Text.IsHex())
        {
            rb16.Checked = true;
            txtPrimeP.Text = txtInfo.Text;
            tabControl1.SelectedTab = tabCalculator;
        }
    }
    private void BtnToPoly_Click(object sender, EventArgs e)
    {
        ChkAutoWidth.Checked = true;
        Clipboard.SetText(txtInfo.Text);
        if (tabControl1.SelectedTab == tabEncoding && txtInfo.Text.IsHex())
        {
            TxtPoly.Text = txtInfo.Text;
            tabControl1.SelectedTab = tabHexViewer;
        }
    }


    private void BtnToC_Click(object sender, EventArgs e)
    {
        ChkAutoWidth.Checked = true;
        Clipboard.SetText(txtInfo.Text);
        if (tabControl1.SelectedTab == tabEncoding && txtInfo.Text.IsHex())
        {
            TxtCRC.Text = txtInfo.Text;
            tabControl1.SelectedTab = tabHexViewer;
        }
    }
    private void BtnToQ_Click(object sender, EventArgs e)
    {
        Clipboard.SetText(txtInfo.Text);
        if (tabControl1.SelectedTab == tabEncoding && txtInfo.Text.IsHex())
        {
            rb16.Checked = true;
            txtPrimeQ.Text = txtInfo.Text;
            tabControl1.SelectedTab = tabCalculator;
        }
    }

    private void RtxtData_DoubleClick(object sender, EventArgs e)
    {
        File.WriteAllText(Application.StartupPath + "tb.txt", rtxtData.Text);
        Process.Start(Application.StartupPath + "tb.txt");
    }

    private void Button1_Click(object sender, EventArgs e)
    {
        rtxtData.Text = lbSoras.Text[6..];
        tabControl1.SelectedTab = tabEncoding;
    }

    private void BtnToHex_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(rtxtData.Text))
        {
            CloseHex();

            chkHexText.Checked = true;
            rbText.Checked = true;
            var s = rtxtData.Text;
            s = Converter.FilterData(s, 65001, chkDiscardChars.Checked, chkDiacritics.Checked, chkzStrings.Checked);
            var b = MyClass.GetBytes(s);
            File.WriteAllBytes(quranBin, b);
            txtInfo.Text = MyClass.ByteArrayToHexString(b);
        }
    }

    private void ChkALLEncodings_CheckedChanged(object sender, EventArgs e)
    {
        LoadEncodings();
        ValidateForRun();
        AppSettings.WriteValue("Settings", "AllEncodings", chkALLEncodings.Checked ? "Yes" : "No");
        if (chkALLEncodings.Checked && cmbSourceEnc.SelectedIndex<0) 
        {
            SelectEncoding("65001",cmbSourceEnc);
            SelectEncoding("1252");
        }
    }

    private void TxtInfo_DoubleClick(object sender, EventArgs e)
    {

    }

    private void ChkHexText_CheckedChanged(object sender, EventArgs e)
    {
        txtInfo.RightToLeft = chkRTL.Checked && !chkHexText.Checked ? RightToLeft.Yes : RightToLeft.No;
    }

    private bool SelectEncoding(string contains, ComboBox combo=null)
    {
        var i = 0;
        var found = false;
        if (combo == null) combo = cmbDestEnc;
        while (i < combo.Items.Count && !found)
            if (!combo.Items[i].ToString().Contains(contains)) i++;
            else found = true;
        if (found) combo.SelectedIndex = i;
        return found;
    }

    /// <summary>
    ///     Show some info about the currently selected codepage
    /// </summary>
    /// <param name="s">String - The selected encoding</param>
    /// <param name="t">The textbox to fill</param>
    private static void BuildEncodingInfo(string s, TextBox t)
    {
        //get the codepage number
        var cp = Converter.GetEncodingEx(s);
        if (!Converter.IsACCO(cp))
        {
            //get the encoding
            var enc = Encoding.GetEncoding(cp);
            StringBuilder sb = new();
            if (enc != null)
            {
                //build up some information
                sb.Append("WebName: " + enc.WebName + Environment.NewLine);
                sb.Append("Copdepage: " + enc.CodePage + Environment.NewLine);
                if (enc.IsSingleByte)
                    sb.Append("Single Byte Charset");
                else
                    sb.Append("Multi Byte Charset");
            }

            //output it to the textbox
            t.Text = sb.ToString();
        }
        else
        {
            t.Text = "WebName: Arabic Common Charset Order \r\nCodepage: ACCO-" + cp + "\r\nSingle Byte Charset";
        }
    }

    private void BtnClearFiles_Click(object sender, EventArgs e)
    {
        lsSource.Items.Clear();
    }

    private void RtxtData_TextChanged(object sender, EventArgs e)
    {
        ValidateForRun();
        sentSora = "";
        encoding = "";
        rbText.Checked = true;
    }

    private void RtxtData_DragDrop(object sender, DragEventArgs e)
    {
        var files = (string[])e.Data.GetData(DataFormats.FileDrop);
        if (files != null && files.Length != 0)
            if (cmbSourceEnc.SelectedIndex >= 0)
                rtxtData.Text = Converter.ReadAllText(files[0], cmbSourceEnc.SelectedItem.ToString());
    }

    private void RtxtData_DragOver(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
            e.Effect = DragDropEffects.Copy;
        else
            e.Effect = DragDropEffects.None;
    }

    private void ChkRTL_CheckedChanged(object sender, EventArgs e)
    {
        rtxtData.RightToLeft = chkRTL.Checked ? RightToLeft.Yes : RightToLeft.No;
        txtInfo.RightToLeft = chkRTL.Checked && !chkHexText.Checked ? RightToLeft.Yes : RightToLeft.No;
    }

    private string FixSpace(string str)
    {
        if (!str.Length.IsEven()) str = "0" + str;
        if (!ChkShowSpace.Checked) return str;
        StringBuilder sb = new(str);
        var sc = Convert.ToByte(cmbSpace.Text, 16).ToString("X2");
        for (var i = 0; i < sb.Length; i += 2)
            if (str.Substring(i, 2) == sc)
                sb.Replace(sc, chkSplitWords.Checked ? Environment.NewLine : "  ", i,2);
        return sb.Replace("  ", " ").ToString();
    }

    private string GetText()
    {
        var text = rtxtData.Text;
        if (chkSplitWords.Checked) text = text.Replace(" ", Environment.NewLine);
        return text;
    }
    private void BtnCSV_Click(object sender, EventArgs e)
    {
        Process.Start(Application.StartupPath + "QuranTable.csv", "");
    }

    private void Encode()
    {
        string sourceCP, destCP;
        string s;
        string res;
        var iSuccess = 0;
        string sFile;
        var Jommal = false;
        List<string> blines = new();

        var specialType = Converter.SpecialTypes.None;

        //03/05/2007 tidy up the UI
        var btnStat = btnRun.Enabled;
        btnRun.Enabled = false;
        Cursor = Cursors.WaitCursor;

        try
        {
           // CloseHex();
            Converter.KillSpace = false;
            if (chkPreserveSpace.Checked) Converter.SpaceChar = Convert.ToByte(cmbSpace.Text, 16);
            else  Converter.KillSpace = true;

            //get the conversion codepages
            sourceCP = cmbSourceEnc.SelectedItem.ToString();
            destCP = cmbDestEnc.SelectedItem.ToString();
            if (destCP.Contains("Jommal"))
            {
                destCP = "Arabic [Common] - 65537";
                Jommal = true;
            }

            //check for special options
            if (chkUnicodeAsDecimal.Checked)
                specialType = Converter.SpecialTypes.UnicodeAsDecimal;

            if (_sourceFiles != null && rbFiles.Checked)
            {
                //convert each file
                for (var i = 0; i < _sourceFiles.Length; i++)
                {
                    //display some information about the file
                    lsSource.Items[i] = lsSource.Items[i].ToString().Replace(char.ConvertFromUtf32(9745) + " ", "")
                        .Replace(char.ConvertFromUtf32(9746) + " ", "");
                    s = lsSource.Items[i].ToString();
                    LogMsg("Converting " + (i + 1) + " of " + _sourceFiles.Length + Environment.NewLine);


                    //do the conversion
                    if ((sFile = Converter.ConvertFile(_sourceFiles[i], destDir,
                            sourceCP, destCP, specialType, chkMeta.Checked, chkDiscardChars.Checked,
                            chkDiacritics.Checked, chkzStrings.Checked)) != "")
                    {
                        //success
                        res = char.ConvertFromUtf32(9745);
                        if (Jommal) File.WriteAllBytes(sFile, ToJommal(File.ReadAllBytes(sFile)));

                        if (chkOutText.Checked)
                        {
                            if (chkHexText.Checked)
                            {
                                var file = File.ReadAllBytes(sFile);
                                MyClass.ByteArrayToHexString(file);
                                lblStatus.Text = file.Length.ToString();
                            }
                            else
                            {
                                Converter.ReadAllText(sFile, destCP);
                                lblStatus.Text = txtInfo.Text.Length.ToString();
                            }
                        }

                        iSuccess++;
                        OutputMsg("[" + cmbDestEnc.Text + "]");
                        if (chkSendToBuffer.Checked)
                        {
                            rbFileBuffer.Checked = true;
                            fileBuffer = File.ReadAllBytes(sFile);
                            LogMsg("Buffered");
                            HashAll();
                        }

                        encoding = cmbDestEnc.Text;
                    }
                    else
                    {
                        //error
                        res = char.ConvertFromUtf32(9746);
                    }

                    //output some info
                    lsSource.Items[i] = res + " " + s;
                    //update the progress bar
                    //03/05/2007 cast values as double and convert back to int
                    progFiles.Value = (int)((i + 1) / (double)_sourceFiles.Length * 100);
                }

                //display the final message
                LogMsg(iSuccess + " file(s) converted successfully and output to:" +
                       Environment.NewLine + destDir + Environment.NewLine);
            }
            else if (rtxtData.Text.Length > 0 && rbText.Checked)
            {
                LogMsg("Converting From TextBox");
                var text = GetText();

                var sTemp = "";
                if (chkReplaceNoon.Checked)
                {
                    var lig = GetLigatures(text);
                    for (var i = 0; i < text.Length; i++)
                        if (lig[i] == 1 && text[i] == '‰') sTemp += '»';
                        else sTemp += text[i];
                    text = sTemp;
                }

                sFile = quranBin;
                byte[] sOut = Array.Empty<byte>();
                if (chkMultiLine.Checked)
                {
                    var lines = text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);


                    File.Delete(sFile);
                    foreach (var str in lines)
                    {
                        sOut = Converter.ConvertText(str, sourceCP, destCP, specialType, chkMeta.Checked,
                            chkDiscardChars.Checked, chkDiacritics.Checked, chkzStrings.Checked);
                        if (Jommal) sOut = ToJommal(sOut);
                        MyClass.AppendAllBytes(sFile, sOut);
                        var sText = (chkHexText.Checked
                            ? MyClass.ByteArrayToHexString(sOut, true)
                            : Converter.ReadAllText(str, destCP)) + Environment.NewLine;
                        OutputMsg(FixSpace(sText));
                        blines.Add(sText);
                        if (sText.Length <= 4096 || !chkHexText.Checked) txtInfo.Text = FixSpace(sText);
                    }
                }
                else
                {
                    sOut = Converter.ConvertText(text, sourceCP, destCP, specialType, chkMeta.Checked, chkDiscardChars.Checked, chkDiacritics.Checked, chkzStrings.Checked);
                    if (Jommal) sOut = ToJommal(sOut);
                    File.WriteAllBytes(sFile, sOut);
                }

                //if (Jommal) File.WriteAllBytes(sFile,toJommal(File.ReadAllBytes(sFile))); 

                sOut = File.ReadAllBytes(sFile);

                if (!chkMultiLine.Checked)
                {
                    var sText = chkHexText.Checked
                        ? MyClass.ByteArrayToHexString(sOut)
                        : Converter.ReadAllText(sFile, destCP);
                    OutputMsg(FixSpace(sText));
                    if (sText.Length <= 4096 || !chkHexText.Checked) txtInfo.Text = FixSpace(sText);
                }
                else
                {
                    int maxWidth = 0, minWidth = int.MaxValue;
                    int fill = 0, index = 0;
                    var ss = "";
                    var buffer = File.ReadAllBytes(sFile);
                    File.Delete(sFile);
                    var fs = new FileStream(sFile, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                    //var sw = new StreamWriter(fs);

                    foreach (var str in blines)
                    {
                        ss += str;
                        var fstr = str[0..^2];
                        if (!fstr.Length.IsEven()) fstr = "0" + fstr;
                        if (fstr.Length / 2 > maxWidth) maxWidth = fstr.Length / 2;
                        if (fstr.Length / 2 < minWidth) minWidth = fstr.Length / 2;
                    }

                    if (ss.Length <= short.MaxValue || !chkHexText.Checked) txtInfo.Text = FixSpace(ss);
                    else txtInfo.Text = "";

                    if (blines.Count > maxWidth * 8) maxWidth = blines.Count / 8 + (blines.Count % 8 != 0 ? 1 : 0);

                    var times = maxWidth * 8 / blines.Count;

                    for (var i = 0; i < maxWidth * 8; i++)
                        if (i < blines.Count)
                        {
                            var lineLength = (blines[i].Length + (blines[i].Length.IsEven() ? 0 : 1) - 2) / 2;

                            //byte[] lineBuffer = buffer.SubArray(index, lineLength); //MyClass.HexStringToBinary(blines[i]);

                            fill = maxWidth - lineLength;
                            var fillBuffer = new byte[maxWidth - minWidth];

                            for (var j = 0; j < times; j++)
                            {
                                if (chkStrechedLines.Checked)
                                {
                                    var wtimes = maxWidth / lineLength;
                                    for (var m = 0; m < lineLength; m++)
                                    for (var k = 0; k < wtimes; k++)
                                        fs.Write(buffer, index + m, 1);
                                    fill = maxWidth - wtimes * lineLength;
                                }
                                else
                                {
                                    fs.Write(buffer, index, lineLength);
                                }

                                if (fill > 0) fs.Write(fillBuffer, 0, fill);
                            }

                            index += lineLength;
                        }
                        else if (i >= blines.Count * times)
                        {
                            var fillBuffer = new byte[maxWidth];
                            fs.Write(fillBuffer, 0, maxWidth);
                        }

                    fs.Close();
                    fs.Dispose();
                }

                OutputMsg("[" + cmbDestEnc.Text + "]");

                if (cmbCoding.SelectedIndex == 2)
                {
                    var buffer = File.ReadAllBytes(sFile);
                    var oBuffer = new byte[buffer.Length * 2 + 2];
                    oBuffer.Init((byte)6);
                    oBuffer[0] = 0xFE;
                    oBuffer[1] = 0xFF;
                    for (var i = 0; i < buffer.Length; i++)
                    {
                        if (buffer[i] == 0x20) oBuffer[i * 2 + 2] = 0;
                        oBuffer[i * 2 + 3] = buffer[i];
                    }

                    File.WriteAllBytes(sFile, oBuffer);
                }

                if (cmbCoding.SelectedIndex == 1)
                {
                    var buffer = File.ReadAllBytes(sFile);
                    var oBuffer = new byte[buffer.Length * 2];
                    oBuffer.Init((byte)0xF2);
                    for (var i = 0; i < buffer.Length; i++)
                    {
                        if (buffer[i] == 0x20) oBuffer[i * 2] = 0;
                        oBuffer[i * 2 + 1] = buffer[i];
                    }

                    File.WriteAllBytes(sFile, oBuffer);
                }

                LogMsg("Written to " + sFile);
                if (chkSendToBuffer.Checked)
                {
                    rbFileBuffer.Checked = true;
                    fileBuffer = sOut;
                    LogMsg("Buffered");
                    HashAll();
                }

                lblStatus.Text = sOut.Length.ToString();
                encoding = cmbDestEnc.Text;

                var crc = MyCRCBI.Create(cmbCRC.Text);

                if (chkShowAsTable.Checked)
                {
                    var sData = GetText();

                    var hexList = txtInfo.Text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    var wordList = sData.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    if (hexList.Length != wordList.Length)
                    {
                        LogMsg(crc.CurParam.Names[0] + " : " + ((ulong)crc.CalculateCRC(MyClass.HexStringToByteArray(hexList[0]))).ToString("X8"));
                    }
                    else
                    {
                        var tableList = "";
                        int index = 1, wordLength;
                        ulong[] crcList=new ulong[hexList.Length];
                        
                        //CRC8Calc.CRC8_POLY @params = (CRC8Calc.CRC8_POLY) cmbCRC8.SelectedIndex;
                       

                        for (var i = 0; i < hexList.Length; i++)
                        {
                            byte[] ba = MyClass.HexStringToByteArray(hexList[i]);
                            //var crc = NullFX.CRC.Crc8.ComputeChecksum(ba);

                          //  CRC8Calc crc2 = new();
                          //  crc2.ComputeChecksum(ba, @params);

                            crcList[i] = (ulong)crc.CalculateCRC(ba); 
                            
                            wordLength = wordList[i].Length;
                            tableList += index.ToString() + "," + wordLength.ToString() +  ","+ crcList[i].ToString("X8")+ ",'" +
                                         hexList[i].Replace(" ","") + "," + wordList[i] + "," + "'" +
                                         MyClass.ByteArrayToBinaryString(ba, true)
                                             .Replace('0', ' ') + "" + Environment.NewLine;
                            index += wordLength;
                            
                        }
                        string sCrcList = crc.CurParam.Names[0]+" : ";// @params.ToString()+" : ";
                        for (int i = 0; i < crcList.Length; i++) sCrcList += crcList[i].ToString("X2") + " ";
                        LogMsg(sCrcList);
                        var fName = Application.StartupPath + "QuranTable.csv";
                        File.WriteAllText(fName, tableList);
                        //txtInfo.Text = tableList;
                        LogMsg("Saved to " + fName);
                        
                    }
                }
            }
            else
            {
                LogMsg("Nothing to encode");
            }
        }
        catch (Exception ex)
        {
            //just show the error
            LogMsg(ex);
        }
        finally
        {
            //03/05/2007 tidy up
            btnRun.Enabled = btnStat;
            Cursor = Cursors.Default;
            if (chkHexText.Checked) hex = txtInfo.Text;
        }
    }


    private void LstLog_Click(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(lstLog, lstLog.SelectedItem.ToString());
    }

    private void Btn1TO9_8_Click(object sender, EventArgs e)
    {
        CmbCRCViewer.SelectedIndex = 0;
        chkALLEncodings.Checked = true;
        File.WriteAllBytes(quranBin, new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36,  0x37, 0x38, 0x39 });
        BtnSHex_Click(sender, e);
        TxtPolyWidth.Text = "8";
        TxtCRC.Text = "BC";
        if (ChkUseGPU.Checked) BtnSendHexToCalc_Click(sender, e);
    }
    private void Btn1To9_16_Click(object sender, EventArgs e)
    {
        CmbCRCViewer.SelectedIndex = 0;
        chkALLEncodings.Checked = true;
        File.WriteAllBytes(quranBin, new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 });
        BtnSHex_Click(sender, e);
        TxtPolyWidth.Text = "16";
        TxtCRC.Text = "FEE8";
        if (ChkUseGPU.Checked) BtnSendHexToCalc_Click(sender, e);
    }

    private void Btn1To9_32_Click(object sender, EventArgs e)
    {
        CmbCRCViewer.SelectedIndex = 0;
        chkALLEncodings.Checked = true;
        File.WriteAllBytes(quranBin, new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36,  0x37, 0x38, 0x39 });
        BtnSHex_Click(sender, e);
        TxtPolyWidth.Text = "32";
        TxtCRC.Text = "3010BF7F";
        if (ChkUseGPU.Checked) BtnSendHexToCalc_Click(sender, e);
    }

    private void Btn1To9_64_Click(object sender, EventArgs e)
    {
        CmbCRCViewer.SelectedIndex = 0;
        File.WriteAllBytes(quranBin, new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 });
        BtnSHex_Click(sender, e);
        TxtPolyWidth.Text = "64";
        TxtCRC.Text = "6C40DF5F0B497347";
        if (ChkUseGPU.Checked) BtnSendHexToCalc_Click(sender, e);
    }

    private void BtnCalcJommal_Click(object sender, EventArgs e)
    {

    }

    /// <summary>
    ///     Handler for the run button click event
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    private void BtnRun_Click(object sender, EventArgs e)
    {
       // if (rtxtData.Text == "" && cmbSourceEnc.Text.Contains("65001") && cmbDestEnc.Text.Contains("1252")) rtxtData.Text = "123456789";
        Encode();
        lastEncUsed = cmbDestEnc.Text;
        AppSettings.WriteValue("Settings", "LastEncodingUsed", lastEncUsed);
    }

    private void BtnUnicodes_Click(object sender, EventArgs e)
    {
        OutputMsg("Unicodes used in text:");
        foreach (var str in MyClass.GetUnicodes(rtxtData.Text))
            OutputMsg(str);
    }

    private void BtnAnalyzeText_Click(object sender, EventArgs e)
    {
        var ligatures = GetLigatures(rtxtData.Text);
        txtInfo.Text = "";
        for (var i = 0; i < ligatures.Length; i++) txtInfo.Text = txtInfo.Text + ligatures[i] + ",";
    }

    /// <summary>
    ///     Handle the open file menu click event
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    private void BtnOpenFile_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFile = new();

        //set up the open file dialog
        openFile.Multiselect = true;
        openFile.Filter = "All files (*.*)|*.*";
        openFile.FilterIndex = 0;
        openFile.ShowDialog();
        //get the filenames
        _sourceFiles = openFile.FileNames;
        string filename;
        foreach (var s in _sourceFiles)
        {
            //add the filesnames to the list box
            //john church 05/10/2008 use directory separator char instead of backslash for linux support
            filename = s[(s.LastIndexOf(Path.DirectorySeparatorChar) + 1)..];
            lsSource.Items.Add(filename);
        }

        if (_sourceFiles.Length > 0) rbFiles.Checked = true;
        //display the message
        LogMsg(_sourceFiles.Length + " file(s) selected for conversion" + Environment.NewLine);
        //validate the run button
        ValidateForRun();
    }

    /// <summary>
    ///     Handler for the combo change event
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    private void CmbSourceEnc_SelectedIndexChanged(object sender, EventArgs e)
    {
        ValidateForRun();
        BuildEncodingInfo(cmbSourceEnc.SelectedItem.ToString(), txtSourceEnc);
    }

    /// <summary>
    ///     Handler for the combo change event
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    private void CmbDestEnc_SelectedIndexChanged(object sender, EventArgs e)
    {
        ValidateForRun();
        BuildEncodingInfo(cmbDestEnc.SelectedItem.ToString(), txtDestEnc);

        var charSetFile = Converter.GetCharsetFile(cmbDestEnc.Text);
        LoadCharset(charSetFile + ".Charset");
        btnFindKey.Enabled = !cmbDestEnc.Text.Contains("Auto");
    }


    private byte[] ToJommal(byte[] buffer)
    {
        List<byte> jBuffer = new();
        for (var i = 0; i < buffer.Length; i++)
        {
            var h = (byte)(jommalCharset[buffer[i] - 1] >> 8);
            var l = (byte)(jommalCharset[buffer[i] - 1] & 0xFF);
            if (h > 0 || chkJommalWord.Checked) jBuffer.Add(h);
            jBuffer.Add(l);
        }

        return jBuffer.ToArray();
    }

    //filenames to convert
    private string[] _sourceFiles;

    //ouput directory
    private string destDir;


    /// <summary>
    ///     Main form constructor
    /// </summary>
    private void SendToHexViewer(string sFile)
    {
        string localFile = Application.StartupPath + "hex.bin";

        // Close current file
        if (hexBox.ByteProvider is IDisposable byteProvider) byteProvider.Dispose();
        hexBox.ByteProvider = null;

        File.Copy(sFile,localFile, true);

        hexBox.ByteProvider = new FileByteProvider(localFile);

        lblHexFile.Text = sFile;
        var i = 0;
        while (cmbHEncoding.Items[i].ToString() != cmbSourceEnc.Text) i++;
        cmbHEncoding.SelectedIndex = i;
        hexBox.Refresh();
        CalcHHash();
    }

    private void BtnFindKey_Click(object sender, EventArgs e)
    {
        FactorData factorData;

        btnFindKey.Enabled = false;


        if (string.IsNullOrEmpty(rtxtData.Text)) return;
        lsSource.Items.Clear();

        txtOut.Text = "";

        txtPlus.Text = "1";

        var charSetFile = Converter.GetCharsetFile(cmbDestEnc.Text);
        if (string.IsNullOrEmpty(charSetFile))
        {
            LogMsg("No Charset File!");
            return;
        }

        LoadCharset(charSetFile + ".Charset");
        if (charSetFile.IndexOf("-Auto") < 0) charSetFile += "-Auto";
        SaveCharset(charSetFile + ".Charset");
        FindInCombo(cmbDestEnc, "Arabic [" + charSetFile + "] - 65537", false, true);
        var i = 0;
        bool done;
        do
        {
            Encode();
            rb16.Checked = true;
            txtPrimeP.Text = MyClass.ByteArrayToHexString(SafeRead(quranBin));
            rb10.Checked = true;
            GetNumbers();
            if (!P.IsEven)
            {
                factorData = FactorDB(txtPrimeP.Text, (WebMethod)cmbWebClient.SelectedIndex);
                if (factorData.factors <= 2 &&
                    (chkAllFactors.Checked || factorData.code.Equals("FF") || factorData.code.Equals("P")))
                    DisplayFactors(factorData);
            }

            done = !IncCharset(1);
            SaveCharset(charSetFile + ".Charset");
            lblProgress.Text = i++.ToString();
            Application.DoEvents();
        } while (!done);

        LogMsg("Finished finding key");
        btnFindKey.Enabled = true;
    }

    private void BtnExport_Click(object sender, EventArgs e)
    {
        byte c;
        string s = ValidChar(ValueOf(listTxtCS[0].Text)).ToString();
        for (var i = 1; i < 44; i++)
        {
            c = ValidChar(ValueOf(listTxtCS[i].Text));
            s += "," + c.ToString();
        }

        Clipboard.SetText(s);
    }

    private void BrnClearTxtInfo_Click(object sender, EventArgs e)
    {
        txtInfo.Text = "";
    }

    private void RtxtData_Enter(object sender, EventArgs e)
    {
        ArabicKeyboard();
    }

    private void RtxtData_Leave(object sender, EventArgs e)
    {
        OriginalKeyboard();
    }

    private void BtnShowHex_Click(object sender, EventArgs e)
    {
        var c = new byte[44];
        string s = c[0].ToString();
        for (var i = 1; i < 44; i++)
        {
            c[i] = ValidChar(ValueOf(listTxtCS[i].Text));
            s += "," + c[i].ToString("X2");
        }

        txtInfo.Text = s;
    }

    private void ChkPreserveSpace_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "PreserveSpace", chkPreserveSpace.Checked ? "Yes" : "No");
        if (!chkPreserveSpace.Checked) chkSplitWords.Checked = false;
        cmbSpace.Enabled = chkPreserveSpace.Checked;
        chkSplitWords.Enabled = chkPreserveSpace.Checked;
    }

    #endregion

    #region Quran

    private void RbTextType_CheckedChanged(object sender, EventArgs e)
    {
        if ((sender as RadioButton).Checked)
        {
            AppSettings.WriteValue("Settings", "QuranText", (sender as RadioButton).Name);
            var n = txtQuranText.Font.Size;
            if (rbFirstOriginalDots.Checked)
            {
                quran.QuranTextIndex = IQuran.QuranTextVersion.FirstOriginal;
                txtQuranText.Font = new Font(fonts[0][0], n);
            }

            if (rbFirstOriginal.Checked)
            {
                quran.QuranTextIndex = IQuran.QuranTextVersion.FirstOriginal;
                txtQuranText.Font = new Font(fonts[1][0], n);
            }

            if (rbNoDiacritics.Checked)
            {
                quran.QuranTextIndex = IQuran.QuranTextVersion.NewNoDiacritics;
                txtQuranText.Font = new Font(fonts[0][0], n);
            }

            if (rbDiacritics.Checked)
            {
                quran.QuranTextIndex = IQuran.QuranTextVersion.NewWithDiacritics;
                txtQuranText.Font = new Font(fonts[0][0], n);
            }

            SelectSora();
        }
    }

    private void LblFontSize_Click(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "FontSize", lblFontSize.Text);
    }

    private void TxtSearch_Enter(object sender, EventArgs e)
    {
        ArabicKeyboard();
    }

    private void TxtSearch_Leave(object sender, EventArgs e)
    {
        OriginalKeyboard();
    }

    private void BtnSearch_Click(object sender, EventArgs e)
    {
        var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath +  "Quran.accdb;User Id=;Password=;";
        var queryString = "SELECT * FROM Quran where AyaTextSearch LIKE '%" + txtSearch.Text + "%'";
        DataTable dt = new();
        using OleDbConnection connection = new(connectionString);
        using OleDbCommand command = new(queryString, connection);
        using OleDbDataAdapter da = new(command);
        try
        {
            /*
            dgvQuran.DataSource = null;
            dgvQuran.Rows.Clear();
            dgvQuran.Columns.Add("Serial", "Serial");
            dgvQuran.Columns.Add("SoraNo", "SoraNo");
            dgvQuran.Columns.Add("AyaNo", "AyaNo");
            dgvQuran.Columns.Add("SoraName", "Sora Name");
            dgvQuran.Columns.Add("AyaText", "Aya Text");

            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            string AyaText="";
            int n = quran.CurQuranTextIndex();
            while (reader.Read())
            {
                AyaText = reader[n].ToString();
                string[] row = new string[] { reader[0].ToString(), reader[1].ToString(), reader[3].ToString(), reader[2].ToString(), AyaText };
                dgvQuran.Rows.Add(row);
            }
            reader.Close();
            */
            da.Fill(dt);
            dgvQuran.DataSource = dt;
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void DgvQuran_Click(object sender, EventArgs e)
    {
        txtQuranText.Text = dgvQuran.Rows[dgvQuran.CurrentRow.Index].Cells[quran.CurQuranTextIndex()].Value.ToString();
    }

    private void BtnSendToEncoding_Click(object sender, EventArgs e)
    {
        Cursor = Cursors.WaitCursor;
        btnSendToEncoding.Enabled = false;

        var text = ReadSoras(lbSoras.SelectedIndices.Cast<int>().ToList());
        /*
         *          string text = "";
                    foreach (int sora in lbSoras.SelectedIndices)
                    {
                        text += ReadSora(sora+1);
                    }
        */
        rtxtData.Text = text;
        txtQuranText.Text = ReadSora(lbSoras.SelectedIndices[0] + 1);

        tabControl1.SelectedTab = tabEncoding;
        cmbSourceEnc.SelectedIndex = 0;
        var i = 0;
        var found = false;
        while (i < cmbSourceEnc.Items.Count && !found)
            if (!cmbSourceEnc.Items[i].ToString().Contains("65001")) i++;
            else found = true;
        if (found) cmbSourceEnc.SelectedIndex = i;

        //string s = lblCurCharset.Text.Substring(0, lblCurCharset.Text.IndexOf("."));
        var s = "[Abjadi]";
    //    if (cmbDestEnc.SelectedIndex < 0)
        {
            if (String.IsNullOrEmpty(lastEncUsed))
            {
                if (rbDiacritics.Checked) s = "Common";
                if (rbFirstOriginal.Checked || rbFirstOriginalDots.Checked) s = "Hijaei";
                if (rbNoDiacritics.Checked) s = "Hijaei-Hamza";
                s = "[" + s + "]";
            } else  s = lastEncUsed;        

        //if (!SelectEncoding("["+s+"]")) 
            SelectEncoding(s);
        }

        sentSora = string.Join(",", lbSoras.SelectedItems.Cast<string>().ToList());
        OutputMsg("Sent:" + sentSora);
        chkRTL.Checked = true;
        Cursor = Cursors.Default;
        btnSendToEncoding.Enabled = true;
    }

    private DataTable SoraTable(int SoraNo)
    {
        return quran.GetSoraTable(SoraNo - 1);
    }

    private string ReadSora(int SoraNo)
    {
        return ReadSora(SoraTable(SoraNo));
    }

    private string ReadSoras(IList<int> soras)
    {
        return ReadSora(quran.GetSorasTable(soras));
    }

    private string ReadSora(DataTable sora)
    {
        var n = quran.CurQuranTextIndex();
        var text = "";
        foreach (DataRow row in sora.Rows) text += row[n] + Environment.NewLine;
        return text;
    }

    private void SelectSora()
    {
        var sora = SoraTable(lbSoras.SelectedIndex + 1);
        dgvQuran.Columns.Clear();
        dgvQuran.DataSource = sora;
        
        dgvQuran.Refresh();
        txtQuranText.Text = ReadSora(sora);
        lblStatus.Text = txtQuranText.Text.Replace(" ", "").Replace("\r", "").Replace("\n", "").Length.ToString();
    }

    private void LbSoras_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectSora();
    }

    private void BtnPlus_Click(object sender, EventArgs e)
    {
        var n = txtQuranText.Font.Size;
        if (n < 200) txtQuranText.Font = new Font(txtQuranText.Font.FontFamily, n + 1);
        lblFontSize.Text = n.ToString();
    }

    private void BtnMinus_Click(object sender, EventArgs e)
    {
        var n = txtQuranText.Font.Size;
        if (n > 5) txtQuranText.Font = new Font(txtQuranText.Font.FontFamily, n - 1);
        lblFontSize.Text = n.ToString();
    }

    private void TxtSoraSearch_Enter(object sender, EventArgs e)
    {
        ArabicKeyboard();
    }

    private void TxtSoraSearch_Leave(object sender, EventArgs e)
    {
        OriginalKeyboard();
    }

    private void TxtSoraSearch_TextChanged(object sender, EventArgs e)
    {
        lbSoras.ClearSelected();
        var index = FindInList(lbSoras, txtSoraSearch.Text);
        if (index < 0 && lbSoras.Items.Count > 0) index = 0;
        if (index >= 0)
            lbSoras.SetSelected(index, true);
    }

    #endregion

    #region Charset
    private void TxtDescription_Enter(object sender, EventArgs e)
    {
        if (txtDescription.RightToLeft == RightToLeft.Yes) ArabicKeyboard();
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
        if (txtDescription.RightToLeft == RightToLeft.Yes) OriginalKeyboard();
    }

    private static int Max(TextBox[] list)
    {
        var max = ValueOf(list[0].Text);
        for (var i = 1; i < list.Length; i++)
        {
            var n = ValueOf(list[i].Text);
            if (n > max) max = n;
        }

        return max;
    }

    private static int Min(TextBox[] list)
    {
        var min = ValueOf(list[0].Text);
        for (var i = 1; i < list.Length; i++)
        {
            var n = ValueOf(list[i].Text);
            if (n < min) min = n;
        }

        return min;
    }

    private bool IncCharset(int n)
    {
        var max = Max(listTxtCS);
        var min = Min(listTxtCS);

        if (max + n > 255 || min + n < 0) return false;

        for (var i = 0; i < 44; i++)
        {
            var v = ValueOf(listTxtCS[i].Text);
            if (v != 0) listTxtCS[i].Text = ValidChar(v + n).ToString();
        }

        return true;
    }

    private void LblCurCharset_DragDrop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.Text))
        {
            File.WriteAllText(Application.StartupPath + "NewCharset.Charset",""+ ISettings.NewLineSep+(string)e.Data.GetData(DataFormats.Text));
            LoadCharset(Application.StartupPath + "NewCharset.Charset");
        }
        else if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0) LoadCharset(File.ReadAllText(files[0]));
        }
    }

    private void LblCurCharset_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.Text) || e.Data.GetDataPresent(DataFormats.FileDrop))
            e.Effect = DragDropEffects.Copy;
        else
            e.Effect = DragDropEffects.None;
    }

    private void BtnAutoAdd_Click(object sender, EventArgs e)
    {
        if (!int.TryParse(txtPlus.Text, out int n)) n = 0;

        if (sender is Button b)
            if (b.Name.Contains("Sub"))
                n = -n;

        IncCharset(n);
    }

    private void BtnReset_Click(object sender, EventArgs e)
    {
        ResetCharsets(true);
    }
    private void ChkDescLang_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDescLang.Checked)
        {
            txtDescription.RightToLeft = RightToLeft.Yes;
            
        }
        else
        {
            txtDescription.RightToLeft = RightToLeft.No;
          
        }
        SetCheckColor(chkDescLang);
    }
    private void ChkCharSetDesc_CheckedChanged(object sender, EventArgs e)
    {
        float scalingFactor = MyClass.ScalingFactor()/100;
        
        if (chkCharSetDesc.Checked)
        {
            txtDescription.Location = new Point((int)(1530/scalingFactor),(int) (963/scalingFactor));
            txtDescription.Size = new Size((int)(597/scalingFactor), (int)(205/scalingFactor));
            txtDescription.Visible = true;
        }
        else
        {
            txtDescription.Visible = false;
        }
        SetCheckColor(chkCharSetDesc);
    }

    private class ComparerClass : IComparer
    {
        // Call CaseInsensitiveComparer.Compare with the parameters reversed.
        int IComparer.Compare(object x, object y)
        {
            //return ((new CaseInsensitiveComparer()).Compare(Int32.Parse((x as TextBox).Text), Int32.Parse((y as TextBox).Text)));
            var xs = int.Parse((x as TextBox).Text).ToString("D3") + (x as TextBox).TabIndex.ToString("D2");
            var ys = int.Parse((y as TextBox).Text).ToString("D3") + (y as TextBox).TabIndex.ToString("D2");
            return new CaseInsensitiveComparer().Compare(xs, ys);
        }
    }

    private void ReOrderChars()
    {
        int n, m = 0, j = 0;
        chkHex.Checked = false;
        var nTXT = (TextBox[])listTxtCS.Clone();
        Array.Sort(nTXT, new ComparerClass());


        for (var i = 0; i < 44; i++)
        {
            n = int.Parse(nTXT[i].Text);
            if (n != m && m != 0) j++;
            if (n != 0)
            {
                listLblCSS[j].Text = listLblCS[nTXT[i].TabIndex - 1].Text;
                m = n;
            }
            //if (n > 0 && n < 45) listLblCSS[n - 1].Text = listLblCS[Int32.Parse(listTxtCS[i].Name.Substring(5)) - 1].Text;
        }
    }

    private void BtnOrder_Click(object sender, EventArgs e)
    {
        ReOrderChars();
    }

    private long startTime;

    private long TimeElapsed
    {
        get => DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - startTime;
        set => startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond + value;
    }

    private void LoadCharset(string fileName)
    {
        try
        {
            var ts = "";
            TimeElapsed = 0;
            ClearChars();
            ts += TimeElapsed + ",";
            var data = File.ReadAllText(fileName).Split(ISettings.NewLineSep);
            var c = MyClass.HexStringToByteArray(data[1]);
            ts += TimeElapsed + ",";
            AppSettings.WriteValue("Settings", "CharsetProfile", Path.GetFileName(fileName));
            ts += TimeElapsed + ",";
            for (var i = 0; i < 44; i++)
                listTxtCS[i].Text = (chkHex.Checked ? "0x" : "") + c[i].ToString(chkHex.Checked ? "X2" : "");
            ts += TimeElapsed + ",";
            ReOrderChars();
            ts += TimeElapsed + ",";
            lblCurCharset.Text = Path.GetFileName(fileName);
            LogMsg(ts);
            try
            {
                txtDescription.Text = data[0];

                if (MyClass.IsArabic(txtDescription.Text))
                {
                    chkDescLang.Checked = true;
                }
                else if (chkDescLang.Checked)
                {
                    chkDescLang.Checked = false;
                    OriginalKeyboard();
                }
                
                if (tabControl1.SelectedTab == tabCharset)
                    chkCharSetDesc.Checked = true;
                else lastDescChecked = true;

            } catch { txtDescription.Text = ""; }
            LogMsg("Custom Charset Loaded");
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void lblCSS1_Click(object sender, EventArgs e)
    {
        // var t = sender as Label;
        // MessageBox.Show(chkFlipY.TextAlign.ToString());
    }

    private void BtnLoadCharset_Click(object sender, EventArgs e)
    {
        try
        {
            chkHex.Checked = false;
            OpenFileDialog openFile = new();
            //set up the open file dialog
            openFile.Multiselect = false;
            openFile.Filter = "All files (*.Charset)|*.Charset";
            openFile.FilterIndex = 0;
            openFile.InitialDirectory = Application.StartupPath;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                SelectEncoding("["+Path.GetFileNameWithoutExtension(openFile.FileName)+"]",cmbDestEnc);
                LoadCharset(openFile.FileName);
            }
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private static byte ValidChar(int b)
    {
        return (byte)(b > 255 ? 0 : b);
    }

    private static int ValueOf(string s)
    {
        var hex = false;

        if (s.Length > 2)
            if (s[..2].ToUpper() == "0X")
                hex = true;
        if (hex)
            return Convert.ToInt32(s, 16);
        if (int.TryParse(s, out int n)) return n;
        return 0;
    }

    private void BtnAutoCharset_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtCS1.Text.Trim()))
        {
            LogMsg("Fill the first Charset Value first");
            return;
        }

        for (var i = 1; i < 44; i++)
            listTxtCS[i].Text = (chkHex.Checked ? "0x" : "") + ValidChar(ValueOf(txtCS1.Text) + i).ToString(chkHex.Checked ? "X2" : "");
    }

    private string GetCharset()
    {
        var c = new byte[44];
        for (var i = 0; i < 44; i++) c[i] = ValidChar(ValueOf(listTxtCS[i].Text));
        return MyClass.ByteArrayToHexString(c, true);
    }

    private void SaveCharset(string fileName)
    {
        File.WriteAllText(fileName, txtDescription.Text+ ISettings.NewLineSep + GetCharset());
        lblCurCharset.Text = Path.GetFileName(fileName);
        AppSettings.WriteValue("Settings", "CharsetProfile", lblCurCharset.Text);
        var s1 = cmbSourceEnc.Text;
        var s2 = cmbDestEnc.Text;
        LoadEncodings();
        cmbSourceEnc.Text = s1;
        cmbDestEnc.Text = s2;
        ValidateForRun();
    }

    private void BtnCSHelp_Click(object sender, EventArgs e)
    {
        try
        {
            MyClass.OpenDocument(Application.StartupPath + "DOC\\ALNR.pdf");
        } catch (Exception ex) { LogMsg(ex); }
    }

    private void BtnSaveCharset_Click(object sender, EventArgs e)
    {
        try
        {
            var fileName = Application.StartupPath + lblCurCharset.Text;
            if ((sender as Button).Name != "btnSave")
            {
                SaveFileDialog dlg = new();
                dlg.Title = "Save Charset File";
                dlg.DefaultExt = "Charset";
                dlg.Filter = "Charset File|*.Charset";
                dlg.InitialDirectory = Application.StartupPath;
                dlg.FileName = lblCurCharset.Text;
                if (dlg.ShowDialog() == DialogResult.OK)
                    fileName = dlg.FileName;
                else fileName = "";
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                SaveCharset(fileName);
                LogMsg("Custom Charset Saved");
            }
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void ClearChars()
    {
        foreach (var t in listTxtCS)
        {
            t.Text = "";
        }
        foreach (var t in listLblCSS)
        {
            t.Text = "";
        }
        lblCurCharset.Text = "New.Charset";
    }

    private void BtnClearCS_Click(object sender, EventArgs e)
    {
        ClearChars();
    }

    private void ChkHex_CheckedChanged(object sender, EventArgs e)
    {
        for (var i = 0; i < 44; i++)
            listTxtCS[i].Text = (chkHex.Checked ? "0x" : "") + ValidChar(ValueOf(listTxtCS[i].Text)).ToString(chkHex.Checked ? "X2" : "");
        SetCheckColor(chkHex);
    }

    private void ResetCharsets(bool overwite = false)
    {
        for (var i = 0; i < charsets.Length; i++)
            if (!File.Exists(Application.StartupPath + charsets[i].Name) || overwite)
            {
                File.WriteAllText(Application.StartupPath +  charsets[i].Name, charsets[i].Description+ ISettings.NewLineSep + MyClass.ByteArrayToHexString(charsets[i].Data, true));
            }
    }

    private void TxtCS_MouseLeave(object sender, EventArgs e)
    {
        txtCSttp.Hide(this);
    }

    private void TxtCS_MouseMove(object sender, MouseEventArgs e)
    {
        var t = sender as TextBox;
        try
        {
            txtCSttp.Show(chkHex.Checked ? ValueOf(t.Text).ToString() : ValueOf(t.Text).ToString("X2"), this,
                new Point(Cursor.Position.X - Left + 10, Cursor.Position.Y - Top + 10), int.MaxValue);
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void GetCharsetControls()
    {
        var txtCSI = GetAllControls<TextBox>(tabCharset);
        var lblCSI = GetAllControls<Label>(tabCharset);

        foreach (var t in txtCSI)
            if (t.Name.StartsWith("txtCS"))
            {
                listTxtCS[int.Parse(t.Name[5..]) - 1] = t;
                t.MouseMove += TxtCS_MouseMove;
                t.MouseLeave += TxtCS_MouseLeave;
                t.TextAlign = HorizontalAlignment.Center;
            }

        foreach (var t in lblCSI)
            if (t.Name.StartsWith("lblCSS"))
            {
                listLblCSS[int.Parse(t.Name[6..]) - 1] = t;
                listLblCSS[int.Parse(t.Name[6..]) - 1].TextAlign = ContentAlignment.MiddleCenter;
            }
            else if (t.Name.StartsWith("lblCS"))
            {
                listLblCS[int.Parse(t.Name[5..]) - 1] = t;
                listLblCS[int.Parse(t.Name[5..]) - 1].TextAlign = ContentAlignment.MiddleCenter;
            }
    }

    #endregion

    #region HexViewer

    private void CmbBytesPerLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "BytesPerLine", cmbBytesPerLine.Text);
        if (cmbBytesPerLine.SelectedIndex == 0)
        {
            hexBox.UseFixedBytesPerLine = false;
        }
        else
        {
            hexBox.BytesPerLine = int.Parse(cmbBytesPerLine.Text);
            hexBox.UseFixedBytesPerLine = true;
        }

        hexBox.Refresh();
    }

    private void CmbCRCViewer_SelectedIndexChanged(object sender, EventArgs e)
    {
        CRCGetDefParams();
        if (CmbCRCViewer.SelectedIndex == 0 && ChkAutoWidth.Checked) TxtPoly_TextChanged(sender, e);
    }

    private void CmbHEncoding_SelectedIndexChanged(object sender, EventArgs e)
    {
        hexBox.ByteCharConverter = new ByteCharProvider(cmbHEncoding.Text);
        hexBox.Refresh();
    }

    private void HexBox_DragDrop(object sender, DragEventArgs e)
    {
        var filePath = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
        var source = new FileByteProvider(filePath);
        lblHexFile.Text = filePath;
        hexBox.ByteProvider = source;
        hexBox.Refresh();
    }

    private void HexBox_DragOver(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
            e.Effect = DragDropEffects.Copy;
        else
            e.Effect = DragDropEffects.None;
    }

    private void ApplyChanges()
    {
        if (hexBox.ByteProvider == null)
            return;

        try
        {
            var fileByteProvider = hexBox.ByteProvider as FileByteProvider;
            fileByteProvider.ApplyChanges();
            LogMsg("Changes Applied to Disk");
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void BtnApplyChanges_Click(object sender, EventArgs e)
    {
        ApplyChanges();
    }

    private void BtnSendToCalc_Click(object sender, EventArgs e)
    {
        try
        {
            var buffer = new byte[hexBox.ByteProvider.Length];
            for (var i = 0; i < hexBox.ByteProvider.Length; i++) buffer[i] = hexBox.ByteProvider.ReadByte(i);
            rb16.Checked = true;
            txtPrimeP.Text = MyClass.ByteArrayToHexString(buffer);
            tabControl1.SelectedTab = tabCalculator;
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void CalcHHash()
    {
        try
        {
            if (hexBox.ByteProvider == null) return;
            var buffer = new byte[hexBox.ByteProvider.Length];
            for (var i = 0; i < hexBox.ByteProvider.Length; i++) buffer[i] = hexBox.ByteProvider.ReadByte(i);
            var hash = MyClass.GetHash(buffer, cmbHHash.Text);
            lblHash.Text = MyClass.ByteArrayToHexString(hash);
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void BtnSendHexToCalc_Click(object sender, EventArgs e)
    {
        try
        {
            if (hexBox.ByteProvider == null) return;
            string hex = "";
            for (var i = 0; i < hexBox.ByteProvider.Length; i++) hex+=hexBox.ByteProvider.ReadByte(i).ToString("X2");
            rb16.Checked = true;

            cmbAccelerator.SelectedIndex=cmbAccelerator.Items.Count-1;
            txtPrimeP.Text= hex;
            txtModulN.Text=TxtPolyWidth.Text;
            txtPrimeQ.Text = TxtCRC.Text;
            txtResultR.Text = "10";

            tabControl1.SelectedTab = tabCalculator;
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void ChkBigInteger_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "BigIntegers", chkBigInteger.Checked ? "YES" : "NO");
    }

    // event handler
    public void OnProgress(object sender, CRCEventArgs e)
    {
        LogMsg("Operation Progress : " + e.Percent.ToString() + "%    Poly= "+e.Poly.ToString("X8"));
    }
    public void OnProgressBI(object sender, CRCEventArgsBI e)
    {
        LogMsg("Operation Progress : " + e.Percent.ToString() + "%    Poly= " + e.Poly.ToString("X8"));
    }

    private void BtnKillPoly_Click(object sender, EventArgs e)
    {
        try
        {
            if (threadPoly.IsAlive)
            {
               // threadPoly.Abort();
               threadPoly.Interrupt();
               threadPoly.Join();
            }
        }
        catch { }
    }

    void FindPoly()
    {
        if (hexBox.ByteProvider == null) return;
        var buffer = new byte[hexBox.ByteProvider.Length];
        for (var i = 0; i < hexBox.ByteProvider.Length; i++) buffer[i] = hexBox.ByteProvider.ReadByte(i);

        byte width = byte.Parse(TxtPolyWidth.Text);

        ulong[] polyList = Array.Empty<ulong>();

        var cc = Cursor.Current;
        //     Cursor.Current = Cursors.WaitCursor;
        Stopwatch sw = new();

        var myCRC = new MyCRC(null);
        myCRC.OnProgressHandler += OnProgress;

        BtnKillPoly.Enabled = true;


        if (ChkAllPoly.Checked)
        {
            threadPoly = new Thread(o => polyList = myCRC.FindAllPoly(buffer, width, Convert.ToUInt64(TxtCRC.Text, MyClass.IsHex(TxtCRC.Text) ? 16 : 10), 1));

        }
        else
        {
            threadPoly = new Thread(o => polyList = new ulong[1] { myCRC.FindPoly(buffer, width, Convert.ToUInt64(TxtCRC.Text, MyClass.IsHex(TxtCRC.Text) ? 16 : 10), 1) });
        }

        sw.Start();
        threadPoly.Start();
        while (threadPoly.IsAlive)
        {
            Application.DoEvents();
            Thread.Sleep(100);
            lblStatus.Text = sw.Elapsed.Hours + " : " + sw.Elapsed.Minutes.ToString() + " : " + sw.Elapsed.Seconds.ToString() + " : " + sw.Elapsed.Milliseconds;
        }
        sw.Stop();

        BtnKillPoly.Enabled = false;

        OutputMsg("Found " + polyList.Length.ToString() + " Polys , crc width :" + width.ToString());
        foreach (var poly in polyList) OutputMsg(poly.ToString("X" + (width / 4 + (width % 8 != 0 ? 1 : 0))));


        OutputMsg("Elapsed Time (ms) :" + sw.ElapsedMilliseconds.ToString() + "  ");
        Cursor.Current = cc;
    }

    void FindPolyBI()
    {
        if (hexBox.ByteProvider == null) return;
        var buffer = new byte[hexBox.ByteProvider.Length];
        for (var i = 0; i < hexBox.ByteProvider.Length; i++) buffer[i] = hexBox.ByteProvider.ReadByte(i);

        byte width = byte.Parse(TxtPolyWidth.Text);

        BigInteger[] polyList = Array.Empty<BigInteger>();

        var cc = Cursor.Current;
        //     Cursor.Current = Cursors.WaitCursor;
        Stopwatch sw = new();

        var myCRC = new MyCRCBI(null);
        myCRC.OnProgressHandler += OnProgressBI;

        BtnKillPoly.Enabled = true;


        if (ChkAllPoly.Checked)
        {
            threadPoly = new Thread(o => polyList = myCRC.FindAllPoly(buffer, width, BigInteger.Parse(TxtCRC.Text, NumberStyles.AllowHexSpecifier), 1));

        }
        else
        {
            threadPoly = new Thread(o => polyList = new BigInteger[1] { myCRC.FindPoly(buffer, width, BigInteger.Parse(TxtCRC.Text, NumberStyles.AllowHexSpecifier), 1) });
        }

        sw.Start();
        threadPoly.Start();
        while (threadPoly.IsAlive)
        {
            Application.DoEvents();
            Thread.Sleep(100);
            lblStatus.Text = sw.Elapsed.Hours + " : " + sw.Elapsed.Minutes.ToString() + " : " + sw.Elapsed.Seconds.ToString() + " : " + sw.Elapsed.Milliseconds;
        }
        sw.Stop();

        BtnKillPoly.Enabled = false;

        OutputMsg("Found " + polyList.Length.ToString() + " Polys , crc width :" + width.ToString());
        foreach (var poly in polyList) OutputMsg(poly.ToString("X" + (width / 4 + (width % 8 != 0 ? 1 : 0))));


        OutputMsg("Elapsed Time (ms) :" + sw.ElapsedMilliseconds.ToString() + "  ");
        Cursor.Current = cc;
    }
    private void TxtPoly_MouseEnter(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(TxtPoly, TxtPoly.Text);
    }

    private void TxtPoly_MouseLeave(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(TxtPoly, "");
    }
    private void TxtCRC_MouseEnter(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(TxtCRC, TxtCRC.Text);
    }

    private void TxtCRC_MouseLeave(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(TxtCRC, "");
    }

    private void TxtInit_MouseEnter(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(TxtInit, TxtInit.Text);
    }

    private void TxtInit_MouseLeave(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(TxtInit, "");
    }

    private void TxtXorOut_MouseEnter(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(TxtXorOut, TxtXorOut.Text);
    }

    private void TxtXorOut_MouseLeave(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(TxtXorOut, "");
    }

    private void BtnFindPoly_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkBigInteger.Checked)
                FindPoly();
            else
                FindPolyBI();
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void BtnTestCRC_Click(object sender, EventArgs e)
    {
        if (CmbCRCViewer.Text.Equals("ALL")) return;

        chkALLEncodings.Checked = true;
        SelectEncoding("1252", cmbSourceEnc);

        File.WriteAllBytes(Application.StartupPath + "num.tmp", new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 });
        SendToHexViewer(Application.StartupPath + "num.tmp");
        BtnCalcCRC_Click(sender, e);
        if (BigInteger.Parse("0" + lblHash.Text, NumberStyles.AllowHexSpecifier) != BigInteger.Parse("0" + TxtCRC.Text, NumberStyles.AllowHexSpecifier))
        {
            LogMsg("Failed !");
            lblTest.BackColor = Color.Red;
        }
        else
        {
            LogMsg("Success");
            lblTest.BackColor = Color.LightGreen;
        }
    }

    private void TxtPolyWidth_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int width = int.Parse(TxtPolyWidth.Text);
            if (width > 64) chkBigInteger.Checked = true;
        }
        catch { }
    }

    private void ClearParams()
    {
        TxtPoly.Text = "";
        TxtCRC.Text = "";
        TxtInit.Text = "0";
        TxtXorOut.Text = "0";
        ChkRefOut.Checked = false;
        ChkRefIn.Checked = false;
        TxtPolyWidth.Text = "";
    }
    private void CRCGetDefParams()
    {
        if (CmbCRCViewer.SelectedIndex > 1)
        {
            MyCRCBI.CRCParametersBI param = MyCRCBI.GetParams(CmbCRCViewer.Text);
            if (param != null)
            {
                ChkAutoWidth.Checked = false;
                TxtPolyWidth.Text = param.Width.ToString();
                int width = Convert.ToInt32(TxtPolyWidth.Text);
                TxtPoly.Text = param.Polynom.ToString("X" + (width / 4).ToString());
                TxtCRC.Text = param.CheckValue.ToString("X" + (width / 4).ToString());
                TxtInit.Text = param.Init.ToString("X" + (width / 4).ToString());
                TxtXorOut.Text = param.XOROut.ToString("X" + (width / 4).ToString());
                ChkRefIn.Checked = param.ReflectIn;
                ChkRefOut.Checked = param.ReflectOut;
                if (CmbCRCViewer.SelectedIndex>1) if (param.Width > 64) chkBigInteger.Checked = true;else chkBigInteger.Checked = false;    
            }
            else ClearParams();
        }
    }

    private void BtnClearParams_Click(object sender, EventArgs e)
    {
        ClearParams();
    }

    private void TxtCRC_TextChanged(object sender, EventArgs e)
    {
        BigInteger crc = 0; try { crc = BigInteger.Parse(TxtCRC.Text, NumberStyles.AllowHexSpecifier); } catch { }

        if (ChkAutoWidth.Checked)
            if (ChkNibl.Checked)
                TxtPolyWidth.Text = (TxtCRC.Text.Length * 4).ToString();
            else
                TxtPolyWidth.Text = MyClass.BinarySize(crc).ToString();
    }
    private void TxtPoly_TextChanged(object sender, EventArgs e)
    {
        BigInteger poly = 0; try { poly = BigInteger.Parse(TxtPoly.Text, NumberStyles.AllowHexSpecifier); } catch { }

        if (ChkAutoWidth.Checked)
            if (ChkNibl.Checked)
                TxtPolyWidth.Text = (TxtPoly.Text.Length * 4).ToString();
            else
                TxtPolyWidth.Text = MyClass.BinarySize(poly).ToString();
    }

    private void ChkAutoWidth_CheckedChanged(object sender, EventArgs e)
    {
        if (TxtPolyWidth.Text=="") TxtPoly_TextChanged(sender, e);
    }

    void CalcCRC()
    {
        if (hexBox.ByteProvider == null) return;
        var buffer = new byte[hexBox.ByteProvider.Length];
        for (var i = 0; i < hexBox.ByteProvider.Length; i++) buffer[i] = hexBox.ByteProvider.ReadByte(i);
        if (CmbCRCViewer.Text == "CUSTOM")
        {
            int width = Convert.ToInt32(TxtPolyWidth.Text);
            ulong poly = 0; try { poly = Convert.ToUInt64(TxtPoly.Text, 16); } catch { }
            ulong init = 0; try { init = Convert.ToUInt64(TxtInit.Text, 16); } catch { }
            ulong xorout = 0; try { xorout = Convert.ToUInt64(TxtXorOut.Text, 16); } catch { }
            ulong check = 0; try { check = Convert.ToUInt64(TxtCRC.Text, 16); } catch { }
            if (Width > 0 && poly > 0)
            {
                var crc = MyCRC.Create(new MyCRC.CRCParameters(width, poly, init, ChkRefIn.Checked, ChkRefOut.Checked, xorout, check, (ulong)0, "CUSTOM"));//
                ulong crcValue = (ulong)crc.CalculateCRC(buffer);
                lblHash.Text = crcValue.ToString("X" + (crc.CurParam.Width / 8).ToString());
                OutputMsg("CRC:" + crcValue.ToString() + "  " + crcValue.ToString("X" + (crc.CurParam.Width / 8).ToString()));
            }
            else LogMsg("Bad Input Data!, Fill Poly width (10), and Poly value in hex first");
        }
        else
        if (CmbCRCViewer.Text == "ALL")
        {
            string[] allCRCs = MyCRC.UniqueAllCRCMethods.ToArray();
            foreach (var crcType in allCRCs)
            {
                var crc = MyCRC.Create(crcType);
                ulong crcValue = (ulong)crc.CalculateCRC(buffer);
                int width = ((crc.CurParam.Width / 8 + ((crc.CurParam.Width % 8) != 0 ? 1 : 0))) * 2;
                OutputMsg(string.Format("{0,-35}", crcType) + "\t : " + crcValue.ToString("X" + width.ToString()) + "\t\t " + crcValue.ToString("D"));
            }
        }
        else
        {
            var crc = MyCRC.Create(CmbCRCViewer.Text);
            ulong crcValue = (ulong)crc.CalculateCRC(buffer);
            lblHash.Text = crcValue.ToString("X" + (crc.CurParam.Width / 8).ToString());
            OutputMsg("CRC:" + crcValue.ToString() + "  " + crcValue.ToString("X" + (crc.CurParam.Width / 8).ToString()));
        }
    }

    void BigCalcCRC()
    {
        if (hexBox.ByteProvider == null) return;
        var buffer = new byte[hexBox.ByteProvider.Length];
        for (var i = 0; i < hexBox.ByteProvider.Length; i++) buffer[i] = hexBox.ByteProvider.ReadByte(i);
        if (CmbCRCViewer.Text == "CUSTOM")
        {
            int width = Convert.ToInt32(TxtPolyWidth.Text);
            BigInteger poly = 0; try { poly = BigInteger.Parse("0"+TxtPoly.Text, NumberStyles.AllowHexSpecifier); } catch { }
            BigInteger init = 0; try { init = BigInteger.Parse("0"+TxtInit.Text, NumberStyles.AllowHexSpecifier); } catch { }
            BigInteger xorout = 0; try { xorout = BigInteger.Parse("0"+TxtXorOut.Text, NumberStyles.AllowHexSpecifier); } catch { }
            BigInteger check = 0; try { check = BigInteger.Parse("0"+TxtCRC.Text, NumberStyles.AllowHexSpecifier); } catch { }
            if (Width > 0 && poly > 0)
            {
                var crc = MyCRCBI.Create(new MyCRCBI.CRCParametersBI(width, poly, init, ChkRefIn.Checked, ChkRefOut.Checked, xorout, check, BigInteger.Zero, "CUSTOM"));
                BigInteger crcValue = (BigInteger)crc.CalculateCRC(buffer);
                lblHash.Text = crcValue.ToString("X" + (crc.CurParam.Width / 8).ToString());
                OutputMsg("CRC:" + crcValue.ToString() + "  " + crcValue.ToString("X" + (crc.CurParam.Width / 8).ToString()));
            }
            else LogMsg("Bad Input Data!, Fill Poly width (10), and Poly value in hex first");
        }
        else
        if (CmbCRCViewer.Text == "ALL")
        {
            string[] allCRCs = MyCRCBI.UniqueAllCRCMethods.ToArray();
            foreach (var crcType in allCRCs)
            {
                var crc = MyCRCBI.Create(crcType);
                BigInteger crcValue = (BigInteger)crc.CalculateCRC(buffer);
                int width = ((crc.CurParam.Width / 8 + ((crc.CurParam.Width % 8) != 0 ? 1 : 0))) * 2;
                OutputMsg(string.Format("{0,-35}", crcType) + "\t : " + crcValue.ToString("X" + width.ToString()) + "\t\t " + crcValue.ToString("D"));
            }
        }
        else
        {
            var crc = MyCRCBI.Create(CmbCRCViewer.Text);
            BigInteger crcValue = (BigInteger)crc.CalculateCRC(buffer);
            lblHash.Text = crcValue.ToString("X" + (crc.CurParam.Width / 8).ToString());
            OutputMsg("CRC:" + crcValue.ToString() + "  " + crcValue.ToString("X" + (crc.CurParam.Width / 8).ToString()));
        }
    }
    private void BtnCalcCRC_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkBigInteger.Checked)
                BigCalcCRC();
            else
                CalcCRC();
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void HexBox_TextChanged(object sender, EventArgs e)
    {
        CalcHHash();
    }

    private void CmbHHash_SelectedIndexChanged(object sender, EventArgs e)
    {
        CalcHHash();
    }


    private void BtnCalcHHash_Click(object sender, EventArgs e)
    {
        CalcHHash();
    }



    private void TabHexViewer_DragDrop(object sender, DragEventArgs e)
    {
        var filePath = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
        var source = new FileByteProvider(filePath);
        lblHexFile.Text = filePath;
        hexBox.ByteProvider = source;
        hexBox.Refresh();
    }

    private void TabHexViewer_DragOver(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
            e.Effect = DragDropEffects.Copy;
        else
            e.Effect = DragDropEffects.None;
    }

    private void CloseHex()
    {
        // if (hexBox.ByteProvider is FileByteProvider f) (hexBox.ByteProvider as FileByteProvider).Dispose();
        if (hexBox.ByteProvider is FileByteProvider) (hexBox.ByteProvider as FileByteProvider).Dispose();
        hexBox.ByteProvider = null;
        lblHexFile.Text = "";
        hexBox.Refresh();
        LogMsg("HexView Cleared");
    }

    private void CheckChanges()
    {
        if (hexBox.ByteProvider == null) return;
        if (hexBox.ByteProvider.HasChanges())
            if (MessageBox.Show("Do you want to apply changes?", "Changes found!", MessageBoxButtons.YesNo) ==
                DialogResult.Yes)
                ApplyChanges();
    }

    private void BtnClearHex_Click(object sender, EventArgs e)
    {
        CheckChanges();
        CloseHex();
    }

    private void BtnOpenHexFile_Click(object sender, EventArgs e)
    {
        try
        {
            CheckChanges();
            OpenFileDialog openFile = new();
            //set up the open file dialog
            openFile.Multiselect = false;
            openFile.Filter = "All files (*.*)|*.*";
            openFile.FilterIndex = 0;
            openFile.InitialDirectory = Application.StartupPath;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                hexBox.ByteProvider = new FileByteProvider(openFile.FileName);
                lblHexFile.Text = openFile.FileName;
                hexBox.Refresh();
                LogMsg("File Loaded:" + openFile.FileName);
            }
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    #endregion

    #region Texture

    private void BtnBarcode_Click(object sender, EventArgs e)
    {
        Blink(true);
        //picQuran1.Image = picQuran3.Image;
        ReadBarcode((Bitmap)picQuran3.Image);
        Blink(false);
    }

    private void BtnSaveImage_Click(object sender, EventArgs e)
    {
        SaveFileDialog dlg = new();
        dlg.Title = "Save Image File";
        dlg.DefaultExt = "Image";
        dlg.Filter = "Image File|*.jpg";
        dlg.InitialDirectory = Application.StartupPath;
        dlg.FileName = sentSora != "" ? sentSora + "-" + encoding : ".jpg";

        if (dlg.ShowDialog() == DialogResult.OK)
        {
            picQuran1.Image.Save(dlg.FileName);
            LogMsg("Image Saved :" + dlg.FileName);
        }
    }

    private void BtnScreenShot_Click(object sender, EventArgs e)
    {
        Visible = false;
        Thread.Sleep(1000);

        picQuran1.Image = ScreenCapture.CaptureScreen();
        Visible = true;
        Blink(true);
        picColor1.Invalidate();
        ReadBarcode((Bitmap)picColor1.Image);
        Blink(false);
    }

    private void ChkQR_CheckedChanged(object sender, EventArgs e)
    {
        picQuran1.Visible = chkQR.Checked;
        picQuran2.Visible = chkQR.Checked;
        if (!chkQR.Checked)
        {
            picQuran3.Left = picQuran1.Left;
            picQuran3.Top = picQuran1.Top;
            picQuran3.Height = picQuran1.Height;
            picQuran3.Width = picQuran2.Left - picQuran1.Left + picQuran2.Width;
        }
        else
        {
            picQuran3.Left = picQuran2.Left;
            picQuran3.Top = picQuran2.Top + picQuran2.Height + 6;
            picQuran3.Height = picQuran1.Height - picQuran2.Height - 6;
            picQuran3.Width = picQuran2.Width;
        }
    }

    private void PicQuran1_DoubleClick(object sender, EventArgs e)
    {
        if (!picQuran1.Enabled) return;
        picQuran1.Enabled = false;
        try
        {
            var photo = Application.StartupPath + "~tmp.jpg";
            picQuran1.Image.Save(photo, ImageFormat.Jpeg);

            // this will not return proc, will be null, since it will not start viewer directly, done via dllhost
            //var proc=Process.Start(new ProcessStartInfo(photo) { Verb = "open" });      // you can use "edit" to edit image   

            ViewImage(photo);


            File.Delete(photo);
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }

        picQuran1.Enabled = true;
    }

    private void PicQuran3_DoubleClick(object sender, EventArgs e)
    {
        if (!picQuran3.Enabled) return;
        picQuran3.Enabled = false;
        try
        {
            var photo = Application.StartupPath + "~tmp3.jpg";
            picQuran3.Image.Save(photo, ImageFormat.Jpeg);

            // this will not return proc, will be null, since it will not start viewer directly, done via dllhost
            //var proc=Process.Start(new ProcessStartInfo(photo) { Verb = "open" });      // you can use "edit" to edit image   

            ViewImage(photo);


            File.Delete(photo);
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }

        picQuran3.Enabled = true;
    }

    private void CreateBarcode(string str, BarcodeFormat format = BarcodeFormat.QR_CODE)
    {
        try
        {
            if (string.IsNullOrEmpty(str)) return;
            Renderer = typeof(BitmapRenderer);
            var writer = new BarcodeWriter
            {
                Format = format,
                Options = EncodingOptions ?? new EncodingOptions
                {
                    Height = picSpace.size2,
                    Width = picSpace.size2
                },
                Renderer = (IBarcodeRenderer<Bitmap>)Activator.CreateInstance(Renderer)
            };
            picQuran2.Image = writer.Write(str);
        }
        catch (Exception exc)
        {
            LogMsg(exc);
        }
    }

    private void LblScale_TextChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "Scale", lblPointSize.Text);
    }

    private void CmbSizeMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetSizeMod(picQuran1, picQuran2);
        DrawImage();
        AppSettings.WriteValue("Settings", "SizeMode", cmbSizeMode.SelectedIndex.ToString());
    }

    private void TabImage_Click(object sender, EventArgs e)
    {
    }

    private void BtnResetImage_Click(object sender, EventArgs e)
    {
        picQuran1.Image = null;
        picQuran2.Image = null;
        picQuran3.Image = null;
        picQuran1.BackColor = Color.Black;
        picQuran2.BackColor = Color.Black;
        picQuran3.BackColor = Color.Black;
        picQuran1.Invalidate();
        picQuran2.Invalidate();
        picQuran3.Invalidate();
        Application.DoEvents();
    }

    private void ChkFixSquare_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "Padding", chkFixPadding.Checked ? "YES" : "NO");
        DrawImage();
    }

    private void ChkFlipY_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "FlipY", chkFlipY.Checked ? "YES" : "NO");
        DrawImage();
        picQuran3.Image.RotateFlip(RFType(chkFlipX.Checked, chkFlipY.Checked));
    }

    private void ChkFlipX_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "FlipX", chkFlipX.Checked ? "YES" : "NO");
        DrawImage();
        picQuran3.Image.RotateFlip(RFType(chkFlipX.Checked, chkFlipY.Checked));
    }

    private void ChkINV_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "Inversed", chkINV.Checked ? "YES" : "NO");
        backColor = chkINV.Checked ? Color.White : Color.Black;
        DrawImage();
    }

    private void BtnRotate_Click(object sender, EventArgs e)
    {
        /*
        switch ((chkFlipX.Checked?2:0)+(chkFlipY.Checked?1:0))
        {
            case 0: picQuran1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);break;
            case 1: picQuran1.Image.RotateFlip(RotateFlipType.Rotate90FlipY); break;
            case 2: picQuran1.Image.RotateFlip(RotateFlipType.Rotate90FlipX); break;
            case 3: picQuran1.Image.RotateFlip(RotateFlipType.Rotate90FlipXY); break;
        }
        */
        picQuran1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
        picQuran1.Invalidate();
        picQuran2.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
        picQuran2.Invalidate();
    }

    private void BtnSPlus_Click(object sender, EventArgs e)
    {
        var n = int.Parse(lblPointSize.Text);
        if (n < 100) n++;
        lblPointSize.Text = n.ToString();
        DrawImage();
    }

    private void BtnSMinus_Click(object sender, EventArgs e)
    {
        var n = int.Parse(lblPointSize.Text);
        if (n > 1) n--;
        lblPointSize.Text = n.ToString();
        DrawImage();
    }

    private void LblScale_Click(object sender, EventArgs e)
    {
    }

    private void DrawImage()
    {
        try
        {
            var buffer = SafeRead(quranBin);
            if (buffer.Length == 0) return;
            var padding = 0;
            var n = buffer.Length * 8;
            var bpl = (int)Math.Sqrt(n);

            while (bpl * bpl < n) bpl++;

            if (chkFixPadding.Checked)
                padding = bpl * bpl - n;
            //if (padding < 0) padding *= -1;

            var pointSize = int.Parse(lblPointSize.Text);

            var bin = MyClass.ByteArrayToBinaryString(buffer, true);

            SetSizeMod(picQuran1, picQuran2);

            //var g= picQuran.CreateGraphics();
            picQuran1.Image = new Bitmap(bpl * pointSize, bpl * pointSize);
            picQuran3.Image = new Bitmap(n * pointSize, picQuran3.Height);

            var g = Graphics.FromImage(picQuran1.Image);
            var g2 = Graphics.FromImage(picQuran3.Image);

            SolidBrush brush = new(chkINV.Checked ? Color.Black : Color.White);
            Pen pen = new(chkINV.Checked ? Color.Black : Color.White, pointSize); // float.Parse(lblScale.Text));

            picQuran1.BackColor = backColor;
            picQuran3.BackColor = backColor;

            g.Clear(picQuran1.BackColor);
            picQuran1.Invalidate();

            for (var i = 0; i < bin.Length + padding; i++)
            {
                var y = i / bpl + 1;
                var x = i % bpl + 1;
                if (x == 1) Debug.WriteLine("New Line");
                if (i >= padding)
                    if (bin[i - padding] == '1')
                        DrawPoint(g, brush, pointSize, 1, x, y);
                if (i >= padding)
                    if (bin[i - padding] == '1')
                        g2.DrawLine(pen, i * pointSize, 0, i * pointSize, picQuran3.Height);
            }

            switch ((chkFlipX.Checked ? 2 : 0) + (chkFlipY.Checked ? 1 : 0))
            {
                case 1:
                    picQuran1.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    break;
                case 2:
                    picQuran1.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    break;
                case 3:
                    picQuran1.Image.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                    break;
            }

            picQuran1.Invalidate();
            picQuran3.Invalidate();
            pen.Dispose();
            brush.Dispose();
            g.Dispose();
            g2.Dispose();
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }

        ;
    }

    private void SetSizeMod(PictureBox pic1, PictureBox pic2)
    {
        switch (cmbSizeMode.SelectedIndex)
        {
            case 0:
                pic1.SizeMode = PictureBoxSizeMode.Normal;
                pic1.SizeMode = PictureBoxSizeMode.Normal;
                break;
            case 1:
                pic1.SizeMode = PictureBoxSizeMode.StretchImage;
                pic1.SizeMode = PictureBoxSizeMode.StretchImage;
                break;
            case 2:
                pic1.SizeMode = PictureBoxSizeMode.AutoSize;
                pic1.SizeMode = PictureBoxSizeMode.AutoSize;
                break;
            case 3:
                pic1.SizeMode = PictureBoxSizeMode.CenterImage;
                pic1.SizeMode = PictureBoxSizeMode.CenterImage;
                break;
            case 4:
                pic1.SizeMode = PictureBoxSizeMode.Zoom;
                pic1.SizeMode = PictureBoxSizeMode.Zoom;
                break;
        }

        pic1.Width = picSpace.size1;
        pic1.Height = picSpace.size1;
        pic2.Width = picSpace.size2;
        pic2.Height = (int)(picSpace.size2 / 1.25);
        pic1.Left = picSpace.x1;
        pic1.Top = picSpace.y1;
        pic2.Left = picSpace.x2;
        pic2.Top = picSpace.y2;

        pic1.Invalidate();
        pic2.Invalidate();
    }

    /*
           private bool InArea(int x,int y,int bpl)
           {
               foreach (var area in areas)
               {
                   if (x >= area.x && x < (area.x + area.size) && y >= area.y && y < (area.y + area.size)) return true;
               }
               return false;
           }
    */

    public static void DrawPoint(Graphics g, int pointSize, int borderSize, int x, int y, Color color, bool fill = true)
    {
        DrawPoint(g, new SolidBrush(color), pointSize, borderSize, x, y, fill);
    }

    public static void DrawPoint(Graphics g, SolidBrush brush, int pointSize, int borderSize, int x, int y, bool fill = true)
    {
        x -= 1;
        y -= 1;
        var pen = new Pen(brush, pointSize);
        Point dPoint = new(x * pointSize + (!fill ? pointSize / 2 : 0), y * pointSize + (!fill ? pointSize / 2 : 0));
        Rectangle rect = new(dPoint,
            new Size((borderSize - (fill ? 0 : 1)) * pointSize, (borderSize - (fill ? 0 : 1)) * pointSize));
        if (fill)
            g.FillRectangle(brush, rect);
        else
            g.DrawRectangle(pen, rect);
        pen.Dispose();
    }

    private void BlinkTimer_Tick(object sender, EventArgs e)
    {
        if (lblScan.BackColor == Color.Red)
        {
            lblScan.BackColor = Color.ForestGreen;
            if (blinkTimer.Tag.Equals("end"))
            {
                blinkTimer.Stop();
                blinkTimer.Dispose();
                blinkTimer = null;
            }
        }
        else
        {
            lblScan.BackColor = Color.Red;
        }
    }

    private void Blink(bool enable)
    {
        if (enable && blinkTimer == null)
        {
            blinkTimer = new Timer();
            blinkTimer.Tick += BlinkTimer_Tick;
            blinkTimer.Interval = 500;
            blinkTimer.Tag = "run";
            blinkTimer.Start();
        }
        else if (blinkTimer != null)
        {
            blinkTimer.Tag = "end";
        }
    }

    private void ReadBarcode(Bitmap bmp)
    {
        var reader = new BarcodeReader();
        var result = reader.Decode(bmp);
        if (result != null)
        {
            LogMsg("Read Successfull : " + result.BarcodeFormat + "[ " + result.Text + " ]");
            OutputMsg(result.BarcodeFormat + Environment.NewLine + result.Text);
        }
    }

    private void WebCamTimer_Tick(object sender, EventArgs e)
    {
        var bitmap = (Bitmap)picQuran1.Image; // wCam.GetCurrentImage();
        if (bitmap == null)
            return;
        ReadBarcode(bitmap);
    }

    private void CmbCam_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblCam.Visible = true;
        txtCam.Visible = true;
        txtCam.Text = "";
        switch (cmbCam.SelectedIndex)
        {
            case 0:
                lblCam.Visible = false;
                txtCam.Visible = false;
                break;
            case 1:
            case 2:
                lblCam.Text = "URL,Usr,Pass";
                break;
            case 3:
            case 4:
                lblCam.Text = "IP,Usr,Pass";
                break;
            case 5:
                var sSize = MyClass.PhysicalScreenSize();
                lblCam.Text = sSize.Width + " x " + sSize.Height;
                break;
        }
    }

    private void LblCam_Click(object sender, EventArgs e)
    {
        lblCam.Visible = false;
        txtCam.Focus();
    }

    private void TxtCam_Leave(object sender, EventArgs e)
    {
        if (txtCam.Text.Length == 0) lblCam.Visible = true;
    }


    private void BtnScan_Click(object sender, EventArgs e)
    {
        if (webCamTimer == null)
        {
            chkQR.Checked = true;
            chkQR.Enabled = false;
            btnBarcode.Enabled = false;
            btnScreenShot.Enabled = false;
            Blink(true);

            picQuran2.Visible = false;
            picQuran3.Visible = false;
            picQuran1.Width = picQuran2.Left + picQuran2.Width - picQuran1.Left;

            Application.DoEvents();

            var commas = txtCam.Text.Where(f => f == ',').Count();
            for (var i = 0; i < 2 - commas; i++) txtCam.Text += ",";

            var inp = txtCam.Text.Split(',');

            switch (cmbCam.SelectedIndex)
            {
                case 0:
                    wCam.OpenDevice(cmbCapDevice.SelectedIndex);
                    break;
                case 1:
                    wCam.OpenStream(inp[0], IWebCam.JPEGType.MJpeg, inp[1], inp[2]);
                    break;
                case 2:
                    wCam.OpenStream(inp[0], IWebCam.JPEGType.Jpeg, inp[1], inp[2]);
                    break;
                case 3:
                    wCam.OpenAXIS(inp[0], inp[1], inp[2]);
                    break;
                case 4:
                    wCam.OpenHikVision(inp[0], inp[1], inp[2]);
                    break;
                case 5:
                    wCam.OpenScreen();
                    break;
            }
            //wCam.OpenDevice(cmbCapDevice.SelectedIndex);

            //wCam.OpenAXIS("cam212.jwu.org", "admin", "akra97");

            webCamTimer = new Timer();
            webCamTimer.Tick += WebCamTimer_Tick;
            webCamTimer.Interval = 200;
            webCamTimer.Start();
        }
        else
        {
            btnBarcode.Enabled = true;
            btnScreenShot.Enabled = true;
            Blink(false);
            webCamTimer.Stop();
            webCamTimer = null;

            wCam.CloseConnection();
            //wCam.Dispose();
            //wCam = null;

            picQuran1.Width = picQuran1.Height;
            picQuran2.Visible = true;
            picQuran3.Visible = true;
            chkQR.Enabled = true;
        }
    }

    #endregion

    #region AudioSpectrum

   
    private void InitSpectrumScreen()
    {
        var pens = new Pen[3] { new(Color.LightCyan, 1.5f), new(Color.LightYellow), new(Color.LightCoral) };
        picSpectrum.BackColor = Color.FromArgb(0x95, 0xCF, 0xA0);
        var w = picSpectrum.Width;
        var h = picSpectrum.Height;
        var pen = new Pen(Color.LightGray);
        var bmp = new Bitmap(w, h);
        picSpectrum.Image = bmp;
        var g = Graphics.FromImage(bmp);
        var wLines = w / 30;
        var hLines = h / 20;
        for (int i = 0, j = 0; i < w; i += wLines, j++)
            g.DrawLine(j == 15 ? pens[0] : pen, i, 0, i, h);
        for (int i = 0, j = 0; i < h; i += hLines, j++)
            g.DrawLine(j == 10 ? pens[0] : j == 8 || j == 12 ? pens[1] : j == 6 || j == 14 ? pens[2] : pen, 0, i, w, i);

        for (var i = 0; i < pens.Length; i++) pens[i].Dispose();
        g.Dispose();

        picSpectrum.Invalidate();

        canvas.Parent = picSpectrum;
        canvas.BackColor = Color.Transparent;
        //canvas.BringToFront();
        canvas.Invalidate();
    }

    private void InjectAudio()
    {
        ResetSpectrum(false);

        Thread.Sleep(250);
        Application.DoEvents();

        var buffer = SafeRead(quranBin);
        var play = chkPlay.Checked;
        sensor.Start(quranWav, false, play, CreateSampleProvider);

        if (sensor.Bits == 8) NormalizeBuffer(buffer);
        var count = buffer.Length;

        var threadSSpectrum = new Thread(o =>
        {
            var index = 0;
            var chunk = chunkSize;
            while (count > 0)
            {
                if (count > chunk)
                {
                    count -= chunk;
                }
                else
                {
                    chunk = count;
                    count = 0;
                }

                sensor.AddBytes(buffer.Skip(index).Take(chunk).ToArray(), chunk);
                index += chunk;
                if (play)
                    while (sensor.PlayerPosition < index)
                    {
                        Thread.Sleep(100);
                        Application.DoEvents();
                    }
            }
        });

        threadSSpectrum.Start();
        while (threadSSpectrum.IsAlive)
        {
            Thread.Sleep(100);
            Application.DoEvents();
        }

        sensor.Stop();
    }

    private void InitSpectrum(int sampleRate = 8000, int bits = 16, AudioType aType = AudioType.Monaural)
    {
        //sensor = new AudioSensor(8000, 16, AudioType.Monaural, OnUpdate);
        spectrums.Clear();
        var readCount = sampleRate * 50 / 1000; // 50 ms
        var shift = sampleRate * 40 / 1000; // 40 ms
        pts = new PointF[sampleRate];
        readIdx = 0;
        recording = false;

        if (sensor != null)
        {
            sensor.Dispose();
            sensor = null;
        }

        sensor = new AudioSensor(sampleRate, bits, aType, OnUpdate);
        for (var i = 0; i < sensor.Data.Channels; i++)
            spectrums.Add(new List<double>());
        UpdateGUI();
    }

    private void CloseSpectrum()
    {
        try
        {
            spectrums.Clear();
            sensor.Dispose();
            sensor = null;
        }
        catch (Exception)
        {
        }

        ;
    }

    private void StartButton_Click(object sender, EventArgs e)
    {
        if (sensor != null)
        {
            //                audioFile = new MemoryStream();
            SaveFileDialog dlg = new();
            dlg.Title = "Save .wav File";
            dlg.DefaultExt = "wav";
            dlg.Filter = "wav File|*.wav";
            dlg.InitialDirectory = Application.StartupPath;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ReInitSpectrum();
                recording = true;
                sensor.Start(dlg.FileName, true, chkPlay.Checked, CreateSampleProvider);
                LogMsg("Start Recording");
            }
        }

        UpdateGUI();
    }

    private void StopButton_Click(object sender, EventArgs e)
    {
        recording = false;
        if (sensor != null) sensor.Stop();
        UpdateGUI();
        LogMsg("Stopped");
    }

    private void Canvas_Paint(object sender, PaintEventArgs e)
    {
        var renderType = GetRenderType();

        if (renderType == RenderType.Wave)
        {
            canvas.BackColor = Color.Transparent;
            if (sensor != null)
                DrawWaves(e.Graphics, sensor.Data);
        }

        if (renderType == RenderType.Spectrum)
        {
            canvas.BackColor = Color.Black;
            // Clone to resove cross-thread inuse issue
            if (spectrums != null)
                //   PredrawSpectrums(spectrums, spectrumBmp);
                // Bitmap bmp = (Bitmap)spectrumBmp.Clone();
                DrawSpectrums(e.Graphics, spectrumBmp);
            // bmp.Dispose();
        }
    }

    private void WaveRButton_CheckedChanged(object sender, EventArgs e)
    {
        canvas.Invalidate();
    }

    private void SpectrumRButton_CheckedChanged(object sender, EventArgs e)
    {
        canvas.Invalidate();
    }

    // Data updated
    private void OnUpdate(byte[] bytes, int count)
    {
        try
        {
            //           audioFile.Write(bytes,0,bytes.Length);
            if (spectrumRButton.Checked)
                // Find the spectrum and draw it on a bitmap
                while (true)
                {
                    var succeed = SpectrumConverter.GetSpectrum(sensor.Data.Buffer, readIdx, readCount, spectrums,
                        sensor.Bits);
                    if (!succeed)
                        break;
                    readIdx += shift;

                    PredrawSpectrums(spectrums, spectrumBmpPrep);
                    spectrumBmp = (Bitmap)spectrumBmpPrep.Clone();
                }

            canvas.Invalidate();
        }
        catch (Exception ex)
        {
            LogMsg(ex, Environment.CurrentManagedThreadId);
        }

        ;
    }


    private RenderType GetRenderType()
    {
        if (waveRButton.Checked)
            return RenderType.Wave;
        if (spectrumRButton.Checked)
            return RenderType.Spectrum;
        return RenderType.None;
    }

    private void UpdateGUI()
    {
        if (recording)
        {
            startButton.Enabled = false;
            stopButton.Enabled = true;
        }
        else
        {
            startButton.Enabled = true;
            stopButton.Enabled = false;
        }
    }

    private void Canvas_Click(object sender, EventArgs e)
    {
    }

    private void DrawWaves(Graphics g, AudioSensorData data)
    {
        // g.Clear(Color.White);
        try
        {
            if (pts == null || pts.Length <= 1)
                return;

            if (data == null || data.Buffer == null)
                return;

            if (data.Buffer.Count <= 0)
                return;

            var ratioy = -g.ClipBounds.Height /
                         (2f * data.Channels * (data.bits == 16 ? short.MaxValue : byte.MaxValue));

            var dx = g.ClipBounds.Width / pts.Length;
            var h = g.ClipBounds.Height / data.Channels;

            var i0 = Math.Max(0, data.Buffer[0].Count - pts.Length);

            for (var channel = 0; channel < data.Channels; channel++)
            {
                var oy = h * channel + 0.5f * h + (data.bits == 8 ? gain * (h / 4) : gain - 1);
                for (var i = i0; i < data.Buffer[channel].Count; i++)
                {
                    pts[i - i0].X = dx * (i - i0);
                    pts[i - i0].Y = data.Buffer[channel][i] * gain * ratioy + oy;
                }

                g.DrawLines(pen, pts);
            }
        }
        catch (Exception ex)
        {
            LogMsg(ex);
            trkDB.Value = trkDB.Minimum;
        }
    }

    private unsafe void PredrawSpectrums(List<List<double>> spectrums, Bitmap spectrumBmp)
    {
        const int dx = 10;
        try
        {
            if (currentThread != Environment.CurrentManagedThreadId)
            {
                currentThread = Environment.CurrentManagedThreadId;
                lblStatus.BeginInvoke(delegate() { lblStatus.Text += currentThread + " "; });
            }

            if (spectrums == null || spectrums.Count <= 0) return;
            if (spectrumBmp == null || spectrumBmp.Width <= 0 || spectrumBmp.Height <= 0) return;

            using (var g = Graphics.FromImage(spectrumBmp))
            {
                g.DrawImage(spectrumBmp, -dx, 0);
            }

            var lck = spectrumBmp.LockBits(new Rectangle(Point.Empty, spectrumBmp.Size), ImageLockMode.WriteOnly,
                PixelFormat.Format24bppRgb);

            var data = (byte*)lck.Scan0;
            var stride = lck.Width * 3;
            stride = stride % 4 == 0 ? stride : (stride / 4 + 1) * 4;

            float minIdx = 0;
            float maxIdx = spectrums[0].Count - 1;

            var yto01 = 1f / spectrumBmp.Height * spectrums.Count;
            float invdy = spectrums[0].Count * spectrums.Count / spectrumBmp.Height;

            for (var y = 0; y < spectrumBmp.Height; y++)
            {
                var channel = (int)(y * yto01);

                if (spectrums.Count <= channel)
                    continue;

                float oy = channel * spectrumBmp.Height / spectrums.Count;
                var t = Math.Max(0, Math.Min(1, (y - oy) * yto01));
                var i = (int)(t * maxIdx + (1 - t) * minIdx);
                var val = spectrums[channel][i];
                var c = (byte)Math.Max(0, Math.Min(255, val * 255));

                for (var x = spectrumBmp.Width - dx; x < spectrumBmp.Width; x++)
                {
                    var offset = stride * y + 3 * x;
                    data[offset + 0] = 0;
                    data[offset + 1] = c;
                    data[offset + 2] = 0;
                }
            }

            spectrumBmp.UnlockBits(lck);
        }
        catch (Exception ex)
        {
            LogMsg(ex, Environment.CurrentManagedThreadId);
        }
    }

    private void PlayWaveFile(string file)
    {
        lastPlayed = file;
        lblStatus.Text = "Threads : ";
        AppSettings.WriteValue("Settings", "LastPlayed", lastPlayed);
        PlayWaveFromFile(lastPlayed);
        LogMsg("File Loaded:" + lastPlayed);
    }

    private void BtnPlay_Click(object sender, EventArgs e)
    {
        try
        {
            if ((sender as Button).Name != "btnRePlay" || string.IsNullOrEmpty(lastPlayed))
            {
                OpenFileDialog openFile = new();
                //set up the open file dialog
                openFile.Multiselect = false;
                openFile.Filter = "All files (*.Wav)|*.Wav";
                openFile.FilterIndex = 0;
                openFile.InitialDirectory = Application.StartupPath;
                if (openFile.ShowDialog() == DialogResult.OK)
                    PlayWaveFile(openFile.FileName);
                else
                    return;
            }

            PlayWaveFile(lastPlayed);
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void BtnStop_KeyDown(object sender, KeyEventArgs e)
    {
        pressed = true;
    }

    private void BtnStop_KeyUp(object sender, KeyEventArgs e)
    {
        pressed = false;
    }

    private void BtnStop_KeyDown(object sender, MouseEventArgs e)
    {
        pressed = true;
    }

    private void ReInitSpectrum()
    {
        try
        {
            if (cmbChannels.SelectedIndex >= 0 && !string.IsNullOrEmpty(cmbBits.Text) &&
                !string.IsNullOrEmpty(cmbSampleRate.Text))
                InitSpectrum(int.Parse(cmbSampleRate.Text), int.Parse(cmbBits.Text),
                    cmbChannels.SelectedIndex == 0 ? AudioType.Monaural : AudioType.Stereo);
        }
        catch (Exception ex)
        {
            LogMsg(ex);
        }
    }

    private void CmbSampleRate_SelectedIndexChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "SampleRate", cmbSampleRate.Text);
        ReInitSpectrum();
    }

    private void CmbBits_SelectedIndexChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "Bits", cmbBits.Text);
        ReInitSpectrum();
    }

    private void CmbChannels_SelectedIndexChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "Channels", cmbChannels.Text);
        ReInitSpectrum();
    }

    private void ChkPlay_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "PlayWhileRecord", chkPlay.Checked ? "YES" : "NO");
    }

    private void TabSpectrum_Enter(object sender, EventArgs e)
    {
        splitContainer1.SplitterDistance = (int)(panel7.Width * 1.25);
    }

    private void ResetSpectrum(bool resetSettings = true)
    {
        var g = Graphics.FromImage(spectrumBmp);
        g.Clear(Color.Black);
        if (resetSettings)
        {
            FindInCombo(cmbSampleRate, "8000", false, true);
            FindInCombo(cmbBits, "8", false, true);
            FindInCombo(cmbChannels, "Mono", false, true);
        }

        ReInitSpectrum();
        canvas.Invalidate();
    }

    private void BtnResetSpectrum_Click(object sender, EventArgs e)
    {
        ResetSpectrum();
    }

    private void TrkDB_Scroll(object sender, EventArgs e)
    {
    }

    private void TrkDB_ValueChanged(object sender, EventArgs e)
    {
        if (trkDB.Value <= 13)
            gain = 1;
        else
            gain = (float)(10 * Math.Log10(trkDB.Value / 10f));
        lblDB.Text = (trkDB.Value / 10f).ToString("0.0") + "/" + (gain * 100).ToString("0");
    }

    private void Canvas_DragDrop(object sender, DragEventArgs e)
    {
        var files = (string[])e.Data.GetData(DataFormats.FileDrop);
        if (files != null && files.Length != 0 && Path.GetExtension(files[0]).ToUpper().Equals(".WAV"))
            PlayWaveFile(files[0]);
    }

    private void Canvas_DragOver(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
            e.Effect = DragDropEffects.Copy;
        else
            e.Effect = DragDropEffects.None;
    }

    private void OnPreVolumeMeter(object sender, StreamVolumeEventArgs e)
    {
        waveformPainter1.AddMax(e.MaxSampleValues[0]);
        waveformPainter2.AddMax(e.MaxSampleValues.Length > 1 ? e.MaxSampleValues[1] : 0f);
    }

    private void OnPostVolumeMeter(object sender, StreamVolumeEventArgs e)
    {
        volumeMeter1.Amplitude = e.MaxSampleValues[0];
        volumeMeter2.Amplitude = e.MaxSampleValues.Length > 1 ? e.MaxSampleValues[1] : 0f;
    }

    private ISampleProvider CreateSampleProvider(IWaveProvider waveProvider, int bits = 16)
    {
        var sampleChannel = new SampleChannel(waveProvider, bits == 16);
        sampleChannel.PreVolumeMeter += OnPreVolumeMeter;
        setVolumeDelegate = vol => sampleChannel.Volume = vol;
        var postVolumeMeter = new MeteringSampleProvider(sampleChannel);
        postVolumeMeter.StreamVolume += OnPostVolumeMeter;

        return postVolumeMeter;
    }

    private void VolumeSlider1_VolumeChanged(object sender, EventArgs e)
    {
        setVolumeDelegate?.Invoke(volumeSlider1.Volume);
    }

    private void BtnStop_KeyUp(object sender, MouseEventArgs e)
    {
        pressed = false;
    }

    private void DrawSpectrums(Graphics g, Bitmap bmp)
    {
        try
        {
            g.DrawImage(bmp, g.ClipBounds);
        }
        catch (Exception ex)
        {
            LogMsg(ex, Environment.CurrentManagedThreadId);
        }
    }


    public void PlayWaveFromFile(string file)
    {
        try
        {
            WaveFileReader waveFile = new(file);
            FindInCombo(cmbSampleRate, waveFile.WaveFormat.SampleRate.ToString(), false, true);
            FindInCombo(cmbBits, waveFile.WaveFormat.BitsPerSample.ToString(), false, true);
            FindInCombo(cmbChannels, waveFile.WaveFormat.Channels == 1 ? "Mono" : "Stereo", false, true);
            ReInitSpectrum();
            waveFile.Dispose();
            waveFile.Close();
            waveFile = null;
            //     sensor.Stop();
            //     sensor.Dispose();

            var sampleRate = int.Parse(cmbSampleRate.Text);
            var bits = int.Parse(cmbBits.Text);
            var channnels = cmbChannels.SelectedIndex + 1;

            new Thread(o =>
            {
                //           InitSpectrum(sampleRate,bits,channnels==1?AudioType.Monaural:AudioType.Stereo);
                lockThread = Environment.CurrentManagedThreadId;
                WaveFileReader waveFile = new(file);
                using (WaveStream blockAlignedStream =
                       new BlockAlignReductionStream(WaveFormatConversionStream.CreatePcmStream(waveFile)))
                {
                    using WaveOut player = new(WaveCallbackInfo.FunctionCallback());
                    IWaveProvider waveProvider = new MonitoredWaveProvider(blockAlignedStream, sensor.AddBytes);
                    player.Init(CreateSampleProvider(waveProvider, bits));
                    btnPlay.BeginInvoke(delegate () { btnPlay.Enabled = false; });
                    btnRePlay.BeginInvoke(new MethodInvoker(delegate { btnRePlay.Enabled = false; }));
                    player.Play();
                    while (player.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100);
                        if (pressed) player.Stop();
                    }

                    try
                    {
                        btnPlay.BeginInvoke(delegate () { btnPlay.Enabled = true; });
                        btnRePlay.BeginInvoke(new MethodInvoker(delegate { btnRePlay.Enabled = true; }));
                    }
                    catch (Exception)
                    {
                    }

                        ;
                }

                lockThread = -1;
            }).Start();
        }
        catch (Exception ex)
        {
            LogMsg(ex, Environment.CurrentManagedThreadId);
        }
    }

    #endregion

    #region Color

    private void BtnCPSM_Click(object sender, EventArgs e)
    {
        var n = int.Parse(lblColorPointSize.Text);
        if (n > 1) n--;
        lblColorPointSize.Text = n.ToString();
        DrawColors();
    }

    private void CmbColorSizeMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetSizeMod(picColor1, picColor2);
        DrawColors();
        AppSettings.WriteValue("Settings", "ColorSizeMode", cmbColorSizeMode.SelectedIndex.ToString());
    }

    private void ChkColorScaled_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "ColorScaled", chkColorScaled.Checked ? "YES" : "NO");
    }

    private void LstLog_Leave(object sender, EventArgs e)
    {
        toolTip1.SetToolTip(lstLog, "");
    }

    private void chkDiscardChars_CheckedChanged(object sender, EventArgs e)
    {

    }

 
    private void BtnCPSP_Click(object sender, EventArgs e)
    {
        var n = int.Parse(lblColorPointSize.Text);
        if (n < 100) n++;
        lblColorPointSize.Text = n.ToString();
        DrawColors();
    }

    private void LblColorPointSize_TextChanged(object sender, EventArgs e)
    {
        AppSettings.WriteValue("Settings", "ColorScale", lblColorPointSize.Text);
    }

    private int ColorValue(int n, int min, int max)
    {
        if (chkColorScaled.Checked && max > min)
            return (n - min) * 255 / (max - min);
        return n;
    }

    private void DrawColors()
    {
        if (!int.TryParse(lblColorPointSize.Text, out int pointSize)) pointSize = 1;

        var byteArray = SafeRead(quranBin);
        if (byteArray.Length == 0) return;

        int min = byteArray.Min();
        int max = byteArray.Max();

        var colorArray = new Color[byteArray.Length / 3];
        for (var i = 0; i < byteArray.Length - 3; i += 3)
        {
            var color = Color.FromArgb(ColorValue(byteArray[i + 0], min, max), ColorValue(byteArray[i + 1], min, max),
                ColorValue(byteArray[i + 2], min, max));
            colorArray[i / 3] = color;
        }

        var n = (int)Math.Sqrt(colorArray.Length);
        while (n * n < colorArray.Length) n++;

        var bmp = new Bitmap(n * pointSize, n * pointSize, PixelFormat.Format24bppRgb);
        var bmp2 = new Bitmap(colorArray.Length, picColor3.Height, PixelFormat.Format24bppRgb);
        var g1 = Graphics.FromImage(bmp);
        var g2 = Graphics.FromImage(bmp2);

        for (var i = 0; i < colorArray.Length; i++)
        {
            //bmp.SetPixel(i % n, i / n, colorArray[i]);

            DrawPoint(g1, pointSize, 1, i % n + 1, i / n + 1, colorArray[i]);

            g2.DrawLine(new Pen(colorArray[i]), i, 0, i, bmp2.Height);
            //bmp2.SetPixel(i, 0, colorArray[i]);
        }

        g1.Dispose();
        g2.Dispose();
        picColor1.Image = bmp;
        picColor2.Image = bmp;
        picColor3.Image = bmp2;

        picColor1.Invalidate();
        picColor2.Invalidate();
        picColor3.Invalidate();
    }

    #endregion

    #region CommentedCode

    /*    void temp()
        {
            Be.Windows.Forms.HexBox hb = new Be.Windows.Forms.HexBox() { ByteProvider = new Be.Windows.Forms.FileByteProvider(sourcefile),
                                                                         ByteCharConverter = new Be.Windows.Forms.DefaultByteCharConverter(),
                                                                         BytesPerLine = 16, 
                                                                         UseFixedBytesPerLine = true,
                                                                         StringViewVisible = true,
                                                                         VScrollBarVisible = true };

        }
    */

    /*
     * private void InitHexBox()
         {
             hexBox = new HexBox()
             {
                 Top = ph1.Top,
                 Width = ph1.Width,
                 Height = ph1.Height,
                 Left = ph1.Left,
                 Visible = true,
                 UseFixedBytesPerLine = true,
                 BytesPerLine = 16,
                 ColumnInfoVisible = true,
                 LineInfoVisible = true,
                 StringViewVisible = true,
                 VScrollBarVisible = true
             };
             this.Controls.Add(hexBox);
             this.Controls.Remove(ph1);
             hexBox.Parent = tabHexViewer;
             hexBox.BringToFront();
             hexBox.Visible = true;

         }
    */

    //confilcts with fody plugin
    /*
    private void RegisterChilkat()
    {
        // The Chilkat API can be unlocked for a fully-functional 30-day trial by passing any
        // string to the UnlockBundle method.  A program can unlock once at the start. Once unlocked,
        // all subsequently instantiated objects are created in the unlocked state. 
        // 
        // After licensing Chilkat, replace the "Anything for 30-day trial" with the purchased unlock code.
        // To verify the purchased unlock code was recognized, examine the contents of the LastErrorText
        // property after unlocking.  For example:

        Chilkat.Global glob = new Chilkat.Global();
        bool success = glob.UnlockBundle("key");
        if (success != true)
        {
            logMsg("Chilkat:"+glob.LastErrorText);
            return;
        }

        int status = glob.UnlockStatus;
        if (status == 2)
        {
            logMsg("Chilkat:" + "Unlocked using purchased unlock code.");
        }
        else
        {
            logMsg("Chilkat:" + "Unlocked in trial mode.");
        }

        // The LastErrorText can be examined in the success case to see if it was unlocked in
        // trial more, or with a purchased unlock code.
        logMsg("Chilkat:" + glob.LastErrorText);

    }
    */

    /*      private static void XReadQuranIndex()
            {
                string[] file = MyClass.ResourceReadAllText("QuranIndex.txt").Replace("\r\n","\n").Split('\n');
                foreach (var s in file)
                if (!string.IsNullOrEmpty(s))
                {
                    string[] items = s.Split(',');
                    QuranIndex q = new();
                    q.serial = Int32.Parse(items[0]);
                    q.soraNo = Byte.Parse(items[1]);
                    q.soraName = items[2];
                    quranIndex.Add(q);
                }
                return ;
            }
    */

    #endregion
}