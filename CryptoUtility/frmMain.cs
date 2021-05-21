//#define _DEBUG

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Numerics;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Drawing.Text;
using System.Data.OleDb;
using Be.Windows.Forms;
using System.Windows.Media.Imaging;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;
using SpectrumAnalyzerLib;
using System.Drawing.Imaging;
using NAudio.Wave;
using System.Threading;
using System.Net;
using Timer = System.Windows.Forms.Timer;
using NAudio.Wave.SampleProviders;
using NAudio.Gui;
using System.Windows.Resources;
using App = System.Windows.Application;

using NumberFormat = CryptoUtility.BigIntegerHelper.NumberFormat;

//using System.Windows.Input;


namespace CryptoUtility
{
    public partial class frmMain : Form
    {
    #region Defs
        const string factorDbURL = "http://www.factordb.com/index.php?query=";
        enum WebMethod { HTTPClient = 0, WebClient, HttpWebRequest, WindowsDefault }
        struct FactorData 
        {
            public string number;
            public string code; 
            public string digits;
            public int factors; 
            public List<string> factorList;

            public FactorData(bool init)
            {
                factorList = new();
                code = "";
                digits = "";
                factors = 0;
                number = "";
            }
        }

        string[][] fonts = { new [] { "KFGQPC Uthman Taha Naskh", "Uthmani.otf" }, new[] { "DQ7 Quran Koufi A", "DQ7QuranKoufiA.ttf" } };
        string[] resources = { }; //{ "bin32\\libiomp5md.dll", "bin32\\mkl_custom.dll", "bin64\\libiomp5md.dll", "bin64\\mkl_custom.dll" };
        int currentThread = -1;
        int lockThread = -1;
        bool needRestart = false;
        private EncodingOptions EncodingOptions { get; set; }
        private string stackTrace = "";
        private string lastPlayed;
        private Type Renderer { get; set; }
        string sentSora="", encoding="";
        InputLanguage original ;
        IQuran quran;
        private ISettings AppSettings;
        Color backColor = Color.Black;
 //       struct Area { public int x; public int y; public int size; }
 //       List<Area> areas = new();

        struct Pics {public int x1; public int y1; public int x2; public int y2; public int size1; public int size2; }
        Pics picSpace;

        GPUClass gpuClass = new();
        RSAClass rsaClass = new(1024);
        DSAClass dsaClass = new();
        RSAParameters privateKey, publicKey;
        DSAParameters dsaPrivateKey, dsaPublicKey;
        readonly string[] dataType = { "Base64", "Text", "Hex" };

        int entry = 0;
        int CurNum;
        string hex;
        byte[] fileBuffer;
        BigInteger P;
        BigInteger Q;
        BigInteger N;
        BigInteger R;
        

        TextBox[] listTxtCS = new TextBox[44];
        Label[] listLblCS = new Label[44];
        Label[] listLblCSS = new Label[44];

        private string[] charsetName = { "Common.Charset","Abjadi.Charset", "Abjadi-Hamza.Charset", "Hijaei.Charset", "Hijaei-Hamza.Charset", "Vocal.Charset","UTF8Single.Charset", "UnicodeSingle.Charset"};
        private byte[][] Charsets = {
                                       new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44 },
                                       new byte[] {0,1,1,6,1,10,1,2,22,22,23,3,8,24,4,25,20,7,15,21,18,26,9,27,16,28,17,19,11,12,13,14,5,6,10,10,0,0,0,0,0,0,0,0 },
                                       new byte[] {1,2,2,7,2,11,2,3,23,23,24,4,9,25,5,26,21,8,16,22,19,27,10,28,17,29,18,20,12,13,14,15,6,7,11,11,0,0,0,0,0,0,0,0},
                                       new byte[] { 0, 1, 1, 27, 1, 28, 1, 2, 3, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 28, 0,0,0,0,0,0,0,0 },
                                       new byte[] {1,2,2,28,2,29,2,3,4,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,29,0,0,0,0,0,0,0,0 },
                                       new byte[] {29,28,28,26,28,27,28,24,15,15,19,8,2,4,16,18,20,13,12,9,11,10,14,17,1,5,23,6,7,21,25,22,3,26,27,27,0,0,0,0,0,0,0,0},
                                       new byte[] {0xA1,0xA2,0xA3,0xA4,0xA5,0xA6,0xA7,0xA8,0xA9,0xAA,0xAB,0xAC,0xAD,0xAE,0xAF,0xB0,0xB1,0xB2,0xB3,0xB4,0xB5,0xB6,0xB7,0xB8,0xB9,0xBA,0x81,0x82,0x83,0x84,0x85,0x86,0x87,0x88,0x89,0x8A,0x8B,0x8C,0x8D,0x8E,0x8F,0x90,0x91,0x92},
                                       new byte[] {0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3A, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F, 0x50, 0x51, 0x52 }
                                    };
        private Int16[] jommalCharset = { 0, 1, 1, 6, 1, 10, 1, 2, 400, 400, 500, 3, 8, 600, 4, 700, 200, 7, 60, 300, 90, 800, 9, 900, 70, 1000, 80, 100, 20, 30, 40, 50, 5, 6, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0 };

        private WebCam wCam;
        private Timer webCamTimer;
        //private readonly BarcodeReader barcodeReader;
        //        MemoryStream audioFile;
        bool pressed;
        private readonly string quranBin = Application.StartupPath + "\\Quran.bin";
        private readonly string quranWav = Application.StartupPath + "\\Quran.wav";
        private readonly string htmlFile = Application.StartupPath + "\\Quran.html";
        enum RenderType
        {
            None,
            Wave,
            Spectrum,
        }

        int readCount = 8000 * 50 / 1000; // 50 ms
        int shift = 8000 * 40 / 1000; // 40 ms
        int readIdx = 0;
        bool recording = false;

        AudioSensor sensor;
        List<List<double>> spectrums = new();

        Pen pen = new Pen(Brushes.Black);
        PointF[] pts = new PointF[8000];
        float gain;
        Bitmap spectrumBmp = new Bitmap(800, 60, PixelFormat.Format24bppRgb);
        Bitmap spectrumBmpPrep = new Bitmap(800, 60, PixelFormat.Format24bppRgb);
        const int chunkSize = 800;

        private Action<float> setVolumeDelegate;

        Timer blinkTimer;

        #endregion

    #region Functions

        // event handler
        public void ProcessCompleted(object sender, gpuEventArgs e)
        {
            LogMsg("Operation Completed with : "+e.accelerator);
        }
		
		void OutputMsg(string msg)
        {
            txtOut.Text += msg + Environment.NewLine;
            txtOut.SelectionStart = txtOut.Text.Length;
            txtOut.ScrollToCaret();
            txtOut.Refresh();
            tabControl2.SelectedTab = tabOutput;
        }
        void LogMsg(string msg,int threadID=-1)
        {
            if (string.IsNullOrEmpty(stackTrace)) stackTrace = Environment.StackTrace;
            string[] msgs = msg.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var str in msgs)
            {
                if (lstLog.InvokeRequired)
                    lstLog.BeginInvoke(new MethodInvoker(delegate  {LogMsg(msg);}) );
                else
                {
                    lstLog.Items.Add(DateTime.Now + ": T["+(threadID<0?Thread.CurrentThread.ManagedThreadId.ToString():threadID.ToString())+"] " + str);
                    lstLog.SelectedIndex = lstLog.Items.Count - 1;
                }
            }
        }
        RotateFlipType RFType(bool fx, bool fy)
        {
            if (fx && fy)
                return RotateFlipType.RotateNoneFlipXY;
            else if (fx)
                return RotateFlipType.RotateNoneFlipX;
            else if (fy)
                return RotateFlipType.RotateNoneFlipY;
            return RotateFlipType.RotateNoneFlipNone;
        }
        void LogMsg(Exception ex,int threadID=-1)
        {
            string stackTrace = ex.StackTrace;
            this.stackTrace = stackTrace;
            int index = stackTrace.LastIndexOf("\\") + 1;
            stackTrace = stackTrace.Substring(index).Replace("\"","");
            LogMsg(ex.Message+" @ "+stackTrace,threadID);
        }

        enum ShellOptions : Int16 
        {
            Default_No_options_specified = 0,
            Do_not_display_a_progress_dialog_box = 4,
            Rename_the_target_file_if_a_file_exists_at_the_target_location_with_the_same_name = 8,
            Click_Yes_to_All_in_any_dialog_box_displayed = 16,
            Preserve_undo_information, _if_possible = 64,
            Perform_the_operation_only_if_a_wildcard_file_name_Start_Dot_Star_is_specified = 128,
            Display_a_progress_dialog_box_but_do_not_show_the_file_names256,
            Do_not_confirm_the_creation_of_a_new_directory_if_the_operation_requires_one_to_be_created = 512,
            Do_not_display_a_user_interface_if_an_error_occurs = 1024,
            Disable_recursion = 4096,
            Do_not_copy_connected_files_as_a_group_Only_copy_the_specified_files_= 8192 
        }

        void InstallFont(string fontFile)
        {
            needRestart = true;
            File.WriteAllBytes(Application.StartupPath + "\\"+fontFile, MyClass.ResourceReadAllBytes("Fonts\\"+fontFile));

            Shell32.Shell shell = new Shell32.Shell();
            Shell32.Folder fontFolder = shell.NameSpace(0x14);              // 0x14:  Destination FonFolder
            // Window Copy File From CopyHere.(Source) to .NameSpace(Destination)
            fontFolder.CopyHere(Application.StartupPath + "\\"+fontFile, ShellOptions.Do_not_display_a_progress_dialog_box |
                                                                           ShellOptions.Click_Yes_to_All_in_any_dialog_box_displayed);
            File.Delete(Application.StartupPath + "\\"+fontFile);

        }
		
        void CheckQuranFont()
        {
            FontFamily[] fontFamilies;
            InstalledFontCollection installedFontCollection = new InstalledFontCollection();

            bool[] fontFound=new bool[fonts.Length / 2];
            for (int i = 0; i < fontFound.Length; i++) fontFound[i] = false;

            // Get the array of FontFamily objects.
            fontFamilies = installedFontCollection.Families;
            for (int i = 0; i < fontFamilies.Length; i++)
            {
                for (int j = 0; j < fontFound.Length; j++)
                {
                    if (fontFamilies[i].Name == fonts[j][0]) fontFound[j] = true;
                }
            }
            for (int j = 0; j < fontFound.Length; j++)
                if (!fontFound[j]) InstallFont(fonts[j][1]);
        }

        public BitmapImage ConvertToImage(byte[] buffer)
        {
            MemoryStream stream = new MemoryStream(buffer);
            BitmapImage image = new BitmapImage();
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
            {
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
        }

        public static void AppendAllBytes(string path, byte[] bytes,bool crlf=false)
        {
            //argument-checking here.

            using (var stream = new FileStream(path, FileMode.Append))
            {
                stream.Write(bytes, 0, bytes.Length);
                if (crlf) stream.Write(new byte[] { 0x0d, 0x0a }, 0, 2);
            }
        }
		
        private InputLanguage GetArabicLanguage()
        {
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                if (lang.LayoutName.Contains("Arabic") || lang.Culture.Name.StartsWith("ar-"))
                    return lang;
            }
            return null;
        }
 
        private void ArabicKeyboard()
        {
            original = InputLanguage.CurrentInputLanguage;
            InputLanguage lang = GetArabicLanguage();
            if (lang == null)
            {
                LogMsg("Arabic Language Keyboard not installed.");
            }

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

        private static string ToBinaryString(byte[] bytes)
        {
            var base2 = new StringBuilder(bytes.Length * 8);
            var binary = Convert.ToString(bytes[0], 2);
            base2.Append(binary);
            for (int index = 1; index < bytes.Length; index++)
                base2.Append(Convert.ToString(bytes[index], 2).PadLeft(8, '0'));
            return base2.ToString();
        }

        private byte[] SafeRead(string file)
        {
            try
            {
                File.Copy(file, file + ".tmp");
                byte[] buffer = File.ReadAllBytes(file + ".tmp");
                File.Delete(file + ".tmp");
                return buffer;
            } catch (Exception ex) { LogMsg(ex); }
            return new byte[0];
        }

        private void SelectIndex(ComboBox cmb, string text)
        {
            for (int i = 0; i < cmb.Items.Count; i++)
                if (cmb.Items[i].ToString().Equals(text)) { cmb.SelectedIndex = i; return; };
            cmb.SelectedIndex = -1;
            cmb.Text = text;
        }

        private int FindInList(ListBox listBox, string searchString, bool caseSensitive = false)
        {
            int index = -1;
            if (!string.IsNullOrEmpty(searchString))
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                if (listBox.Items[i].ToString().IndexOf(searchString, (caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase)) >= 0)
                {
                    index = i;break;
                }
            }
            return index;
        }
        private int FindInCombo(ComboBox comboBox, string searchString, bool caseSensitive = false,bool setIfFound=false)
        {
            int index = -1;
            if (!string.IsNullOrEmpty(searchString))
                for (int i = 0; i < comboBox.Items.Count; i++)
                {
                    if (comboBox.Items[i].ToString().IndexOf(searchString, (caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase)) >= 0)
                    {
                        if (setIfFound) 
                            comboBox.SelectedIndex = i;
                        index = i;break;
                    }
                }
            return index;
        }

        private void ViewImage(string photo)
        {
            string imageViewerAssoc = FileAssociation.GetExecFileAssociatedToExtension(".jpg", "open");
            string imageViewer = Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%") + "\\Windows Photo Viewer\\PhotoViewer.dll";
            Process proc;

            if (!string.IsNullOrEmpty(imageViewerAssoc))
                proc = Process.Start(imageViewerAssoc, photo);
            else
                proc = Process.Start("rundll32.exe", "\"" + imageViewer + "\", ImageView_Fullscreen " + photo);

            proc.WaitForInputIdle();                        // wait for program to start

            if (proc != null && proc.HasExited != true)
            {
                proc.WaitForExit();                         // wait for porgram to finish
            }
            else
            {
                // Process is null or have exited
            }
        }

        /// <summary>
        /// Load an embedded resource and write it to the disk in the startup folder
        /// </summary>
        /// <param name="resourceName">Full Path to Resource Name</param>
        private void DumpResource(string resourceName)
        {
            try
            {
                if (File.Exists(resourceName))
                    return;

                string file = Application.StartupPath + "\\" + resourceName;
                string dir = Path.GetDirectoryName(file);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName.Replace("\\", ".")));
                if (!string.IsNullOrEmpty(resource))
                    using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                    {
                        Byte[] assemblyData = new Byte[stream.Length];
                        stream.Read(assemblyData, 0, assemblyData.Length);
                        File.WriteAllBytes(file, assemblyData);
                    }
                else
                {
                    File.Copy(Application.StartupPath + "\\Lib\\" + resourceName, file);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        
        private void ManageResources()
        {
            // run this lambda function when couldn't find a DLL, it will load it from the resource :)
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                try
                {
                    string resourceName = new AssemblyName(args.Name).Name + ".dll";
                    string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));
                    if (!string.IsNullOrEmpty(resource))
                    {
                        using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                        {
                            Byte[] assemblyData = new Byte[stream.Length];
                            stream.Read(assemblyData, 0, assemblyData.Length);
                            return Assembly.Load(assemblyData);
                        }
                    }
                    else
                    if (File.Exists(Application.StartupPath + "\\LIB\\" + resourceName))
                    {
                        return Assembly.LoadFrom(Application.StartupPath + "\\LIB\\" + resourceName);
                    }
                } catch (Exception) { };

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
            TextBox txt = (TextBox)sender;
            if (e.Button.Equals(MouseButtons.Left))
            {
                txt.SelectAll();
                txt.DoDragDrop(txt.Text, DragDropEffects.Copy);
            }
        }

        private void TextBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text) || e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void TextBox_DragDrop(object sender, DragEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (e.Data.GetDataPresent(DataFormats.Text))
                txt.Text = (string)e.Data.GetData(DataFormats.Text);
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files != null && files.Length != 0)
                {
                    txt.Text = File.ReadAllText(files[0]);
                }
                
            }
        }
        private void TextBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text) || e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void txtOut_DoubleClick(object sender, EventArgs e)
        {
            File.WriteAllText(Application.StartupPath + "\\tmp.txt", txtOut.Text);
            Process.Start(Application.StartupPath + "\\tmp.txt");
        }
        public frmMain()
        {
            ManageResources();

            InitializeComponent();
            
        }
	      private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                quran.CloseQuran();
                pressed = true;
                Thread.Sleep(500);
                CloseSpectrum();
            }
            catch (Exception) { };
        }
		
		   private void frmMain_Load(object sender, EventArgs e)
        {
            //RegisterChilkat();
            try
            {
                this.Text = "Crypto Utility for Quran Fidelity, Version " + typeof(frmMain).Assembly.GetName().Version;
                
              //  InitHexBox();

                AppSettings = new IniFile(Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 4) + ".ini");

                ResetCharsets();
                GetCharsetControls();

                loadEncodings();

                quran = new QuranDB(Application.StartupPath);
                // quran = new QuranXLS(Application.StartupPath);
                
                lblCurCharset.Text = AppSettings.ReadValue("Settings", "CharsetProfile", "Common.Charset");

                gpuClass.ProcessCompleted += ProcessCompleted;

                picSpace.x1 = picQuran1.Left;picSpace.y1 = picQuran1.Top;picSpace.size1 = picQuran1.Width;
                picSpace.x2 = picQuran2.Left; picSpace.y2 = picQuran2.Top; picSpace.size2 = picQuran2.Width;

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

                CurNum = 2; rb16.Checked = true;
                cmbKeyLen.SelectedIndex = 0;

                ToolTip toolTip = new();
                toolTip.SetToolTip(txtInfo, "Double-Click to copy content to clipboard");

                cmbAccelerator.Items.Clear();
                cmbAccelerator.Items.Add("Direct");
                try
                {
                    if (gpuClass.GetAccelerators().Length>0)
                        cmbAccelerator.Items.AddRange(gpuClass.GetAccelerators());
                }
                catch (Exception) { } ;
                if (cmbAccelerator.Items.Count > 0) cmbAccelerator.SelectedIndex = 0;

                //Quran=ReadQuran();

                CheckQuranFont();

                if (needRestart)
                {
                    Process.Start(Application.ExecutablePath);
                    Application.Exit();
                }

                txtQuranText.Font = new Font(fonts[0][0], 12);
               // lbSoras.Font = new Font(QuranFont, 10);
                
                lblFontSize.Text = AppSettings.ReadValue("Settings", "FontSize", "12");
                txtQuranText.Font = new Font(txtQuranText.Font.FontFamily, Int32.Parse(lblFontSize.Text));

                lblPointSize.Text = AppSettings.ReadValue("Settings", "Scale", "1");
                lblColorPointSize.Text = AppSettings.ReadValue("Settings", "ColorScale", "1");

                cmbBytesPerLine.Text = AppSettings.ReadValue("Settings", "BytesPerLine", "16");
                cmbSizeMode.SelectedIndex = Int32.Parse(AppSettings.ReadValue("Settings", "SizeMode", "1"));
                cmbColorSizeMode.SelectedIndex = Int32.Parse(AppSettings.ReadValue("Settings", "ColorSizeMode", "1"));
                chkALLEncodings.Checked = (AppSettings.ReadValue("Settings", "AllEncodings", "Yes").ToUpper() == "NO");
                chkSendToBuffer.Checked = (AppSettings.ReadValue("Settings", "SendToBuffer", "Yes").ToUpper() == "YES");
                chkJommalWord.Checked = (AppSettings.ReadValue("Settings", "JommalWORD", "Yes").ToUpper() == "YES");
                chkFixPadding.Checked = (AppSettings.ReadValue("Settings", "Padding", "No").ToUpper() == "YES");
                chkFlipX.Checked = (AppSettings.ReadValue("Settings", "FlipX", "Yes").ToUpper() == "YES");
                chkFlipY.Checked = (AppSettings.ReadValue("Settings", "FlipY", "No").ToUpper() == "YES");
                chkINV.Checked = (AppSettings.ReadValue("Settings", "Inversed", "No").ToUpper() == "YES");
                lastPlayed = AppSettings.ReadValue("Settings", "LastPlayed", "");
                chkColorScaled.Checked = (AppSettings.ReadValue("Settings", "ColorScaled", "YES").ToUpper() == "YES");
                chkMultiLine.Checked = (AppSettings.ReadValue("Settings", "MultiLine", "NO").ToUpper() == "YES");

                switch (AppSettings.ReadValue("Settings", "QuranText", "rbDiacritics"))
                {
                    case "rbDiacritics": rbDiacritics.Checked = true;
                        break;
                    case "rbNoDiacritics": rbNoDiacritics.Checked = true;
                        break;
                    case "rbFirstOriginal":rbFirstOriginal.Checked = true;
                        break;
                    case "rbFirstOriginalDots":rbFirstOriginalDots.Checked = true;
                        break;
                }

                chkPlay.Checked = (AppSettings.ReadValue("Settings", "PlayWhileRecord", "No").ToUpper() == "YES");
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
                this.Width =(int) (txtInfo.Left + txtInfo.Width + tabControl1.Left*2.5); 

            } catch (Exception ex) { LogMsg("Error:" + ex.Message); }
        }

        private void btnStackTrace_Click(object sender, EventArgs e)
        {
            txtInfo.Text = stackTrace;
        }
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
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
                    txtInfo.Text = "Crypto Utility\r\n\r\nDecrypt/Encrypt, Calculate hash, and Sign/Verify data using public/private keys.\r\n\r\nData can be loaded in the textbox or a file buffer.";
                }
                else if (tabControl1.SelectedTab == tabCalculator)
                {
                    cmbAccelerator_SelectedIndexChanged(sender, e);
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
                    txtInfo.Text = @"Crypto Utility for Quran Fidelity

Quran is the greatest book ever found on earth, it was revealed to the last prophet Mohammad peace upon him. 

Nowadays, almost all sensitive data being stored or transferred via any media are protected against loss and change.

Every piece of data must be protected and highly trusted, otherwise no confidence is granted.

Quran, is the utmost book that requires such fidelity, we believe that, Quran must have a proof that is aligned with nowadays modern technologies, since God is the most knowledgeable and the most expert.

Findings:
The Key:
The first Sora of Quran is named ALFATEHA, which means 'the Opener'. We beleive it contains the public KEY for verifying the content of the quran.

Text to byte conversion (code page):
We implemented several encoding options to convert text to numbers. Hamza, is to be tested, since it was not used in the revelead text explicitly.

Other possible options:
Hex, Texture, Audio, Light & Color representations are implemented for testing on binary data of Quran";
                } 
                else if (tabControl1.SelectedTab==tabCharset)
                {
                    LoadCharset(Application.StartupPath+"\\"+ lblCurCharset.Text);
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
                    txtInfo.Text = "Texture\r\n\r\nDisplay Binary Image of Text Endoding\r\n\r\nLooking for meaningful image represntation of binary data";
                }
                else if (tabControl1.SelectedTab == tabAudioSpectrum)
                {
                    txtInfo.Text = "Audio Spectrum Analyzer\r\n\r\nConvert binary data to frequency using FFT and display in spectrum analyzer\r\nLooking for meaningful voice";
                }
                else if (tabControl1.SelectedTab == tabColor)
                {
                    txtInfo.Text = "Light/Color Spectrum\r\n\r\nConvert binary data to colors using RGB and display in spectrum analyzer\r\nLooking for meaningful color";
                }
            } catch (Exception ex) { LogMsg("Error :" + ex.Message); }
        }


	#endregion
	
	#region RSA
	        private void btnGenKeys_Click(object sender, EventArgs e)
        {
            try
            {
                rsaClass.CreateKey(Int32.Parse(cmbRSAKeyLen.Text));
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
            catch (Exception ex) { LogMsg("Error :" + ex.Message); }
        }
        private void btnCalcD_Click(object sender, EventArgs e)
        {
            BigInteger P = BigIntegerHelper.GetBig(txtP.Text);
            BigInteger Q = BigIntegerHelper.GetBig(txtQ.Text);
            BigInteger E = BigIntegerHelper.GetBig(txtE.Text);

            BigInteger phi = (P - 1) * (Q - 1);
            BigInteger d = E.modinv(phi);
            txtN.Text = BigIntegerHelper.Hex(P * Q);
            txtD.Text = BigIntegerHelper.Hex(d);
            txtDP.Text = BigIntegerHelper.Hex(d % (P - 1));
            txtDQ.Text = BigIntegerHelper.Hex(d % (Q - 1));
            txtInverseQ.Text = BigIntegerHelper.Hex(Q.modinv(P));
        }
        private void btnRSAtoCALC_Click(object sender, EventArgs e)
        {
            txtPrimeP.Text = txtP.Text;
            txtPrimeQ.Text = txtQ.Text;
            txtModulN.Text = txtN.Text;
            tabControl1.SelectedTab = tabCalculator;
        }

        private void btnUsePublicKey_Click(object sender, EventArgs e)
        {
            txtPublicKey.Text = txtPEMPublicKey.Text;
        }

        private void btnUsePrivateKey_Click(object sender, EventArgs e)
        {
            txtPrivateKey.Text = txtPEMPrivateKey.Text;
        }
        private void ClearRSAKeys()
        {
            txtE.Text ="";
            txtN.Text = "";
            txtP.Text = "";
            txtQ.Text = "";
            txtD.Text = "";
            txtDP.Text = "";
            txtDQ.Text = "";
            txtInverseQ.Text = "";
        }
        private void btnImportPublicKey_Click(object sender, EventArgs e)
        {
            try
            {
                ClearRSAKeys();
                publicKey = PEMClass.ImportPublicKey(txtPEMPublicKey.Text);
                rsaClass.SetKey(publicKey, false);
                int E = 0;
                for (int i = 0; i < publicKey.Exponent.Length; i++) E += publicKey.Exponent[i] << (i * 8);

                txtE.Text = E.ToString("X");
                txtN.Text = MyClass.BinaryToHexString(publicKey.Modulus);

               // tabControl.SelectedIndex = 0;

                cmbRSAKeyLen.Text = (publicKey.Modulus.Length * 8).ToString();
            }
            catch (Exception ex) { LogMsg("Error :" + ex.Message); }
        }

        private void btnImportPrivateKey_Click(object sender, EventArgs e)
        {
            try
            {
                ClearRSAKeys();
                privateKey = PEMClass.ImportPrivateKey(txtPEMPrivateKey.Text);
                rsaClass.SetKey(privateKey, true);
                int E = 0;
                for (int i = 0; i < privateKey.Exponent.Length; i++) E += privateKey.Exponent[i] << (i * 8);

                txtE.Text = E.ToString("X");
                txtN.Text = MyClass.BinaryToHexString(privateKey.Modulus);

                txtP.Text = MyClass.BinaryToHexString(privateKey.P);
                txtQ.Text = MyClass.BinaryToHexString(privateKey.Q);
                txtD.Text = MyClass.BinaryToHexString(privateKey.D);
                txtDP.Text = MyClass.BinaryToHexString(privateKey.DP);
                txtDQ.Text = MyClass.BinaryToHexString(privateKey.DQ);
                txtInverseQ.Text = MyClass.BinaryToHexString(privateKey.InverseQ);

               // tabControl.SelectedIndex = 0;

                cmbRSAKeyLen.Text = (privateKey.Modulus.Length * 8).ToString();
            }
            catch (Exception ex) { LogMsg("Error :" + ex.Message); }
        }
		
		      private void btnExportPublic_Click(object sender, EventArgs e)
        {
        //    using RSACryptoServiceProvider csp = new();
            RSAParameters rsaParams = new();
            try
            {
                int E = Int32.Parse(txtE.Text, System.Globalization.NumberStyles.HexNumber); // or use  Convert.ToInt32(txtE.Text, 16);
                int i = 0;
                while (E > 0)
                {
                    Array.Resize(ref rsaParams.Exponent, i + 1);
                    rsaParams.Exponent[i] = (byte)(E & 0xFF);
                    i++;
                    E >>= 8;
                }

                rsaParams.Modulus = MyClass.HexStringToBinary(txtN.Text);
                if (chkReverse.Checked) Array.Reverse(rsaParams.Modulus);


                // csp.ImportParameters(rsaParams);
                txtPEMPublicKey.Text = PEMClass.ExportPublicKey(rsaParams, true);
            } catch (Exception ex) { LogMsg( "Error : "+ ex.Message); }
        }

        private void btnExportPrivate_Click(object sender, EventArgs e)
        {
            RSAParameters rsaParams = new();
            try
            {
                int E = Int32.Parse(txtE.Text, System.Globalization.NumberStyles.HexNumber);
                int i = 0;
                while (E > 0)
                {
                    Array.Resize(ref rsaParams.Exponent, i + 1);
                    rsaParams.Exponent[i] = (byte)(E & 0xFF);
                    i++;
                    E >>= 8;
                }

                rsaParams.Modulus = MyClass.HexStringToBinary(txtN.Text);
                rsaParams.P = MyClass.HexStringToBinary(txtP.Text);
                rsaParams.Q = MyClass.HexStringToBinary(txtQ.Text);
                rsaParams.D = MyClass.HexStringToBinary(txtD.Text);
                rsaParams.DP = MyClass.HexStringToBinary(txtDP.Text);
                rsaParams.DQ = MyClass.HexStringToBinary(txtDQ.Text);
                rsaParams.InverseQ = MyClass.HexStringToBinary(txtInverseQ.Text);

                txtPEMPrivateKey.Text = PEMClass.ExportPrivateKey(rsaParams, true);
            }
            catch (Exception ex) { LogMsg("Error : " + ex.Message); }
        }

	#endregion
	
	#region DSA
	        private void btnExportDSAPublic_Click(object sender, EventArgs e)
        {
            try
            {
                DSAParameters dsaKey = new();
                dsaKey.P = MyClass.HexStringToBinary(txtDSA_P.Text);
                dsaKey.Q = MyClass.HexStringToBinary(txtDSA_Q.Text);
                dsaKey.G = MyClass.HexStringToBinary(txtDSA_G.Text);
                dsaKey.Y = MyClass.HexStringToBinary(txtDSA_Y.Text);

                txtDSAPEMPublic.Text = PEMClass.ExportDSAPublicKey(dsaKey);
            }
            catch (Exception ex) { LogMsg("Error :" + ex.Message); }
        }

        private void btnGenDSAKeys_Click(object sender, EventArgs e)
        {
            try 
            {
                dsaClass.CreateKey(Int32.Parse(cmbDSAKeyLen.Text));
                dsaPrivateKey = dsaClass.GetKey(true);
                dsaPublicKey = dsaClass.GetKey(false);

                txtDSAPEMPublic.Text = PEMClass.ExportDSAPublicKey(dsaPublicKey);
                txtDSAPEMPrivate.Text = PEMClass.ExportDSAPrivateKey(dsaPrivateKey);

                LogMsg("DSA Keys Generated");
            }
            catch (Exception ex) { LogMsg("Error :" + ex.Message); }
        }

        private void btnDSAImportPublic_Click(object sender, EventArgs e)
        {
            try
            {
                ClearKeys();
                DSAParameters dsaKey = PEMClass.ImportDSAPublicKey(txtDSAPEMPublic.Text);
                cmbDSAKeyLen.Text = (dsaKey.Y.Length * 8).ToString();
                ShowKeys(dsaKey, false);
            }
            catch (Exception ex) { LogMsg("Error :" + ex.Message); }
        }

        private void txtDSAPEM_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void txtDSAPEM_DragDrop(object sender, DragEventArgs e)
        {
            txtDSAPEMPublic.Text = (string)e.Data.GetData(DataFormats.Text);
        }

        private void btnDSAImportPrivate_Click(object sender, EventArgs e)
        {
            try
            {
                ClearKeys();
                DSAParameters dsaKey = PEMClass.ImportDSAPrivateKey(txtDSAPEMPrivate.Text);
                cmbDSAKeyLen.Text = (dsaKey.Y.Length * 8).ToString();
                ShowKeys(dsaKey, true);
            }
            catch (Exception ex) { LogMsg("Error :" + ex.Message); }
        }

        private void btnExportDSA_Click(object sender, EventArgs e)
        {
            try
            {
                DSAParameters dsaKey = new();
                dsaKey.P = MyClass.HexStringToBinary(txtDSA_P.Text);
                dsaKey.Q = MyClass.HexStringToBinary(txtDSA_Q.Text);
                dsaKey.G = MyClass.HexStringToBinary(txtDSA_G.Text);
                dsaKey.Y = MyClass.HexStringToBinary(txtDSA_Y.Text);

                if (txtDSA_X.Text.Trim() != "")
                {
                    dsaKey.X = MyClass.HexStringToBinary(txtDSA_X.Text);
                    txtDSAPEMPrivate.Text = PEMClass.ExportDSAPrivateKey(dsaKey);
                }
                else
                    txtDSAPEMPublic.Text = PEMClass.ExportDSAPublicKey(dsaKey);
            }  catch (Exception ex) { LogMsg("Error :" + ex.Message); }
        }

        private void ShowKeys(DSAParameters dsaKey,bool priv)
        {
            txtDSA_P.Text = MyClass.BinaryToHexString(dsaKey.P);
            txtDSA_Q.Text = MyClass.BinaryToHexString(dsaKey.Q);
            txtDSA_G.Text = MyClass.BinaryToHexString(dsaKey.G);
            txtDSA_Y.Text = MyClass.BinaryToHexString(dsaKey.Y);
            if (priv) txtDSA_X.Text = MyClass.BinaryToHexString(dsaKey.X);
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
	        private void btnUseKeys_Click(object sender, EventArgs e)
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
                default:
                    break;
            }
        }

     
       private void btnSaveKeys_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(Application.StartupPath + "\\PublicKey."+cmbCryptoAlgorithm.Text+".pem", txtPublicKey.Text);
                File.WriteAllText(Application.StartupPath + "\\PrivateKey."+cmbCryptoAlgorithm.Text +".pem", txtPrivateKey.Text);
                LogMsg("Keys Saved");
            }
            catch (Exception ex) { LogMsg("Error :" + ex.Message); }
        }

        private void btnLoadKeys_Click(object sender, EventArgs e)
        {
            try
            {
                txtPublicKey.Text=File.ReadAllText(Application.StartupPath + "\\PublicKey." + cmbCryptoAlgorithm.Text + ".pem");
                txtPrivateKey.Text=File.ReadAllText(Application.StartupPath + "\\PrivateKey." + cmbCryptoAlgorithm.Text + ".pem");
                LogMsg("Keys Loaded");
            }
            catch (Exception ex) { LogMsg("Error :" + ex.Message); }
        }
		
        private void btnLoadFile_Click(object sender, EventArgs e)
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
                    LogMsg("File Loaded (" + dlg.FileName + ") Size is " +fileBuffer.Length.ToString());
                }
                rbFileBuffer.Checked = true;
            }
            catch (Exception ex) { LogMsg("Error :" + ex.Message); }

            btnLoadFile.Enabled = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls(this);
        }

        private void btnHash_Click(object sender, EventArgs e)
        {
            byte[] hash = MyClass.GetHash(GetData(), cmbHash.Text);
            txtHash.Text = MyClass.BinaryToHexString(hash);
        }

      private byte[] GetData()
        {
            byte[] data;
            
            if (rbFileBuffer.Checked) return fileBuffer;

            switch (cmbData.SelectedIndex)
            {
                case 0: data = Convert.FromBase64String(txtData.Text); break;
                case 2: data = MyClass.HexStringToBinary(txtData.Text); break;
                default: data = Encoding.ASCII.GetBytes(txtData.Text); break;
            }
            return data;
        }

        private string SetData(byte[] data)
        {
            string sData;
            switch (cmbData.SelectedIndex)
            {
                case 0: sData = Convert.ToBase64String(data); break;
                case 2: sData = MyClass.BinaryToHexString(data); break;
                default: sData = Encoding.ASCII.GetString(data); break;
            }
            return sData;
        }
        private void btnEncrypt_Click(object sender, EventArgs e)
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

                        txtOutput.Text = Convert.ToBase64String(rsaClass.Encrypt(GetData(), cmbEncryptionKey.SelectedIndex == 1,chkPadding.Checked));

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
            catch (Exception ex) { LogMsg("Error :" + ex.Message); }

        }

        private void btnDecrypt_Click(object sender, EventArgs e)
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

                        txtData.Text = SetData(rsaClass.Decrypt(Convert.FromBase64String(txtOutput.Text), cmbEncryptionKey.SelectedIndex == 0, chkPadding.Checked));

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
            catch (Exception ex) { LogMsg("Error :" + ex.Message); }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] hash;
                switch (cmbCryptoAlgorithm.SelectedIndex)
                {
                    case 0:
                        hash = MyClass.GetHash(GetData(), cmbHash.Text);
                        txtHash.Text = MyClass.BinaryToHexString(hash);

                        rsaClass.SetKey(PEMClass.ImportPublicKey(txtPublicKey.Text), false);
                        if (!string.IsNullOrEmpty(txtPrivateKey.Text)) rsaClass.SetKey(PEMClass.ImportPrivateKey(txtPrivateKey.Text), true);

                        LogMsg(rsaClass.Verify(hash, Convert.FromBase64String(txtOutput.Text), cmbHash.Text) ? "Verification Successful" : "Verification Failed");
                        break;
                    case 1:
                        hash = MyClass.GetHash(GetData(), cmbHash.Text);
                        txtHash.Text = MyClass.BinaryToHexString(hash);

                        dsaClass.SetKey(PEMClass.ImportDSAPublicKey(txtPublicKey.Text), false);
                        dsaClass.SetKey(PEMClass.ImportDSAPrivateKey(txtPrivateKey.Text), true);

                        LogMsg(dsaClass.VerifySignature(hash, Convert.FromBase64String(txtOutput.Text), cmbHash.Text) ? "Verification Successful" : "Verification Failed");
                        break;
                    default:
                        LogMsg("Algorithm not implemented");
                        break;
                }
            }
            catch (Exception ex) { LogMsg("Error :" + ex.Message); }
        }
		
		private void btnSign_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] hash;
                switch (cmbCryptoAlgorithm.SelectedIndex)
                {
                    case 0:
                        hash = MyClass.GetHash(GetData(), cmbHash.Text);
                        txtHash.Text = MyClass.BinaryToHexString(hash);

                        rsaClass.SetKey(PEMClass.ImportPublicKey(txtPublicKey.Text), false);
                        rsaClass.SetKey(PEMClass.ImportPrivateKey(txtPrivateKey.Text), true);

                        txtOutput.Text = Convert.ToBase64String(rsaClass.Sign(hash, cmbHash.Text));

                        LogMsg("Signed");
                        break;
                    case 1:
                        hash = MyClass.GetHash(GetData(), cmbHash.Text);
                        txtHash.Text = MyClass.BinaryToHexString(hash);

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
            catch (Exception ex) { LogMsg("Error :" + ex.Message); }
        }

        private void cmbData_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblData.Text = "Data (" + dataType[cmbData.SelectedIndex] + ")";
        }

	#endregion
	
	#region Calculator

        string FactorMessage(string code)
        {
            string msg = "";
            switch (code)
            {
                case "C": msg = " Composite, no factors known"; break;
                case "CF": msg = "Composite, factors known"; break;
                case "FF": msg = "Composite, fully factored"; break;
                case "P": msg = "Definitely prime"; break;
                case "PRP":
                case "Prp": msg = "Probably prime"; break;
                case "U": msg = "Unknown"; break;
                case "Unit": msg = "Just for \"1\""; break;
                case "N": msg = "This number is not in database(and was not added due to your settings)"; break;
                case "*": msg = "Added to database during this request"; break;
            }
            return msg;
        }

        private void btnToRSA_Click(object sender, EventArgs e)
        {
            rb16.Checked = true;
            GetNumbers();
            if (BigIntegerHelper.IsProbablePrime(P, 100) && BigIntegerHelper.IsProbablePrime(Q, 100))
            {
                txtP.Text = txtPrimeP.Text;
                txtQ.Text = txtPrimeQ.Text;
                txtE.Text = "10001";
                tabControl1.SelectedTab = tabRSA;
                btnCalcD_Click(sender, e);
                btnExportPublic_Click(sender, e);
                btnExportPrivate_Click(sender, e);
            }
            else LogMsg("Not Primes !");
        }

        private void tabControl2_Click(object sender, EventArgs e)
        {
            if (tabControl2.SelectedTab == tabWebBrowser)
            {
                string data = File.ReadAllText(htmlFile);
                int p = data.FindString("value=", 0);
                if (p > 0)
                {
                    string number = data.ExtractData("\"", ref p, 7);
                    webBrowser.Navigate(factorDbURL + number);
                }
                //webBrowser.Navigate(htmlFile);
            }
        }
        FactorData ExtractFactorData(string data)
        {
            FactorData factorData = new(true);

            int p = 0;
            try
            {
                p = data.FindString("<table ", p);
                p = data.FindString("<tr>", p+1, 3);
                p = data.FindString("<td>", p+1);
                factorData.code = data.ExtractData("<",ref p,4);
                p = data.FindString("<td>", p+1);
                factorData.digits = data.ExtractData("<", ref p, 4);
                p = data.FindString("<td>", p+1);
                p = data.FindString("> = <", p + 1);
                factorData.factors = 0;
                if (p > 0) p += 3;
                while (p > 0 && !data.Substring(p + 1, 5).Equals("</td>"))
                {
                    p = data.FindString(">", p + 1, 2);
                    p++;
                    string str = data.ExtractData("<", ref p, 0).Trim();
                    factorData.factorList.Add(str);
                    int n = str.IndexOf("^"); if (n > 0) factorData.factors += int.Parse(str.Substring(n + 1)); else factorData.factors++;

                    p = data.FindString(">", p + 1, 2);
                    if (data.Substring(p + 1, 5).Equals("<sub>"))
                    {
                        p = data.FindString("</sub>", p);
                        p += 5;
                        //factorData.factors += 100;
                    }
                } ;

            } catch (Exception ex) { LogMsg(ex); }

            return factorData;
        }

        private FactorData FactorDB(string prime, WebMethod methodIndex)
        {
            string data = "";
            string url = factorDbURL + prime;
            string userAgent = "CryptoUtility";
            FactorData factorData;
            
            switch ((int) methodIndex)
            {
                case 0: _ = MyWeb.HTTPReadPageAsync(url, userAgent, result => data = result); break;
                case 1: data = MyWeb.WebClientReadPage(url, userAgent); break;
                case 2: data = MyWeb.WebRequestReadPage(url, userAgent); break;
                case 3: Process.Start(url); break;
            }
            File.WriteAllText(htmlFile, data);
            factorData = ExtractFactorData(data);
            factorData.number = prime;

            return factorData;
        }

        void DisplayFactors(FactorData factorData)
        {
            OutputMsg(Environment.NewLine+
                      "Number= " + factorData.number + Environment.NewLine +
                      "Code= " + factorData.code + " , [" + FactorMessage(factorData.code) + "]" + Environment.NewLine +
                      "Digits= " + factorData.digits + Environment.NewLine +
                      "Factors (" + factorData.factors + ") = " + string.Join(",", factorData.factorList) + Environment.NewLine +
                      "Charset = " + GetCharset());
        }
        private void btnFactorDbP_Click(object sender, EventArgs e)
        {
            rb10.Checked = true;
            DisplayFactors(FactorDB(txtPrimeP.Text,(WebMethod)  cmbWebClient.SelectedIndex));
        }
        private  void btnFactorDbQ_Click(object sender, EventArgs e)
        {
            rb10.Checked = true;
            DisplayFactors(FactorDB(txtPrimeQ.Text,(WebMethod) cmbWebClient.SelectedIndex));
        }

        private void btnFactorizeP_Click(object sender, EventArgs e)
        {
            GetNumbers();
            txtResultR.Text = "";
            IList<BigInteger> factors = BigIntegerHelper.GetFactors(P);
            foreach (var factor in factors)
            {
                txtResultR.Text += factor.ToString() + Environment.NewLine;
            }
        }

        private void btnFactorizeQ_Click(object sender, EventArgs e)
        {
            GetNumbers();
            txtResultR.Text = "";
            IList<BigInteger> factors = BigIntegerHelper.GetFactors(Q);
            foreach (var factor in factors)
            {
                txtResultR.Text += factor.ToString() + Environment.NewLine;
            }
        }

        private void cmbWebClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "WebClient", cmbWebClient.Text);
        }

        private void txtPrimeP_DoubleClick(object sender, EventArgs e)
        {
            File.WriteAllText(Application.StartupPath + "\\tmp.txt", txtPrimeP.Text);
            Process.Start(Application.StartupPath + "\\tmp.txt");
        }

        private void txtPrimeQ_DoubleClick(object sender, EventArgs e)
        {
            File.WriteAllText(Application.StartupPath + "\\tmp.txt", txtPrimeQ.Text);
            Process.Start(Application.StartupPath + "\\tmp.txt");
        }

        private void txtModulN_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtModulN_DoubleClick(object sender, EventArgs e)
        {
            File.WriteAllText(Application.StartupPath + "\\tmp.txt", txtModulN.Text);
            Process.Start(Application.StartupPath + "\\tmp.txt");
        }

        private void txtResultR_DoubleClick(object sender, EventArgs e)
        {
            File.WriteAllText(Application.StartupPath + "\\tmp.txt", txtResultR.Text);
            Process.Start(Application.StartupPath + "\\tmp.txt");
        }

        private int GetNumIndex()
        {
            if (rb10.Checked) return 1;
            if (rb16.Checked) return 2;
            if (rb64b.Checked) return 3;
            return 0; // rb2
        }
        private void rbNumberBaseCheckedChanged(object sender, EventArgs e)
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

        bool NotZero(string num)
        {
            return (num.Length > 0 && !num.Equals("00") && !num.Equals("0"));
        }
        private void txtPrimeP_TextChanged(object sender, EventArgs e)
        {
            GetNumbers();
            int n = P.GetActualBitwidth();
            if (NotZero(txtPrimeP.Text)) lblStatus.Text = "P = " + txtPrimeP.Text.Length +" - "+ (n).ToString() + " / " + P.GetBitwidth();
        }

        private void txtPrimeQ_TextChanged(object sender, EventArgs e)
        {
            GetNumbers();
            int n = Q.GetActualBitwidth();
            if (NotZero(txtPrimeQ.Text)) 
                lblStatus.Text = "Q = " + txtPrimeQ.Text.Length + " - " + (n).ToString() + " / " + Q.GetBitwidth();
        }
		
		private void btnBezout_Click(object sender, EventArgs e)
        {
            GetNumbers();
            BigInteger[] bzt= BigIntegerHelper.gcdWithBezout(P, Q);
            if (NotZero(txtResultR.Text)) txtResultR.Text = "P . " + bzt[1] + " + Q ." + bzt[2] + " = " + bzt[0];
        }

        private void btnOR_Click(object sender, EventArgs e)
        {
            GetNumbers();
            R = P | Q;
            ShowResult();
        }

        private void btnXOR_Click(object sender, EventArgs e)
        {
            GetNumbers();
            R = P ^ Q;
            ShowResult();
        }

        private void btnReverseP_Click(object sender, EventArgs e)
        {
            GetNumbers();
            P = BigIntegerHelper.GetBig(P.ToByteArray(), chkPositives.Checked);
            ShowNumbers();
        }

        private void btnReverseQ_Click(object sender, EventArgs e)
        {
            GetNumbers();
            Q = BigIntegerHelper.GetBig(Q.ToByteArray(), chkPositives.Checked);
            ShowNumbers();
        }

        private void btnIsEven_Click(object sender, EventArgs e)
        {
            GetNumbers();
            txtResultR.Text= "P is " + ((P & 1)==1 ? "Odd" : "Even") + Environment.NewLine +
                              "Q is " + ((Q & 1)==1 ? "Odd" : "Even");
        }
		
        private void btnGenPrime_Click(object sender, EventArgs e)
        {
            //P = BigIntegerHelper.GenPrime(int.Parse(cmbKeyLen.Text));
            using (RSACryptoServiceProvider csp = new(int.Parse(cmbKeyLen.Text)))
            {
                P = BigIntegerHelper.GetBig(csp.ExportParameters(true).P);
                Q = BigIntegerHelper.GetBig(csp.ExportParameters(true).Q);

                ShowNumbers();
            }
        }

        private void btnSHL_Click(object sender, EventArgs e)
        {
            GetNumbers();
            P <<= 1;
            ShowNumbers();
        }

        private void btnSHR_Click(object sender, EventArgs e)
        {
            GetNumbers();
            P >>= 1;
            ShowNumbers();
        }
		
	    private void btnIsPrime_Click(object sender, EventArgs e)
        {
            GetNumbers();
            txtResultR.Text= "P is " + (BigIntegerHelper.IsProbablePrime(P,100) ? "Prime" : "NOT Prime") + Environment.NewLine +
                              "Q is " + (BigIntegerHelper.IsProbablePrime(Q,100) ? "Prime" : "NOT Prime");
        }

        private void GetNumbers()
        {
            if (entry > 0) return;
            txtResultR.Text = "";Application.DoEvents();
            entry++;
            try
            {
                P = txtPrimeP.Text.ConvertFrom((NumberFormat)CurNum,chkPositives.Checked);
                Q = txtPrimeQ.Text.ConvertFrom((NumberFormat)CurNum, chkPositives.Checked);
                N = txtModulN.Text.ConvertFrom((NumberFormat)CurNum, chkPositives.Checked);
                R = txtResultR.Text.ConvertFrom((NumberFormat)CurNum, chkPositives.Checked);
            } 
            catch (Exception ex) { LogMsg(ex); }   
            entry--;
        }


        private void ShowNumbers()
        {
            entry++;
            NumberFormat format = (NumberFormat)GetNumIndex();
  
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
            txtResultR.Text = R.ConvertTo((NumberFormat)CurNum);

            //if (R== 0) txtResultR.Text = "";
            //            byte[] RR = R.ToByteArray();
            //            Array.Reverse(RR);
            //            txtResultR.Text = MyClass.BinaryToHexString(RR);
        }
        private void btnClearCalc_Click(object sender, EventArgs e)
        {
            txtPrimeP.Text = "";
            txtPrimeQ.Text = "";
            txtModulN.Text = "";
            txtResultR.Text = "";
        }

        private void btnSqrt_Click(object sender, EventArgs e)
        {
            GetNumbers();
            R = P.Sqrt();
            ShowResult();
        }
        private void cmbAccelerator_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbAccelerator.SelectedIndex > 0)
                    txtInfo.Text=gpuClass.AcceleratorInfo(cmbAccelerator.SelectedIndex - 1);
                else
                    txtInfo.Text=" Direct Calculation";
            }
            catch (Exception ex) { LogMsg("Error:" + ex.Message); }
        }
		
		       private void btnADD_Click(object sender, EventArgs e)
        {
            try
            {
                GetNumbers();
                if (cmbAccelerator.SelectedIndex == 0)
                    R = BigInteger.Add(P, Q);
                else
                    R = gpuClass.Calc(cmbAccelerator.Text, P, Q, '+');

                ShowResult();
            } catch (Exception ex) { LogMsg("Error:" + ex.Message); }
        }

        private void btnSUB_Click(object sender, EventArgs e)
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
                LogMsg("Error:" + ex.Message);
            }
}

        private void btnMUL_Click(object sender, EventArgs e)
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
            catch (Exception ex) { LogMsg("Error:" + ex.Message); }
        }

        private void btnDIV_Click(object sender, EventArgs e)
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
            catch (Exception ex) { LogMsg("Error :" + ex.Message); }
        }

        private void btnMOD_Click(object sender, EventArgs e)
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
            catch (Exception ex) { LogMsg("Error :" + ex.Message); }
        }

        private void btnPOW_Click(object sender, EventArgs e)
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
                                if (Q > Int32.MaxValue) E = Int32.MaxValue; else E = (int)Q;
                                Q = BigInteger.Subtract(Q, E);
                                R = BigInteger.Multiply(R, BigInteger.Pow(P, E));
                            } while (Q > 0);
                        }
                        else
                            R = gpuClass.Calc(cmbAccelerator.Text, P, Q, '^');
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
            catch (Exception ex) { LogMsg("Error:" + ex.Message); }
        }

        private void btnPWM_Click(object sender, EventArgs e)
        {
            try
            {
                GetNumbers();

                R = BigInteger.ModPow(P, Q, N);

                ShowResult();
            }
            catch (Exception ex) { LogMsg("Error :" + ex.Message); }
        }

        private void btnRTP_Click(object sender, EventArgs e)
        {
            P = R;
            txtPrimeP.Text = txtResultR.Text;
        }

        private void btnFCD_Click(object sender, EventArgs e)
        {
            try
            {
                GetNumbers();

                R = BigInteger.GreatestCommonDivisor(P, Q);

                ShowResult();
            }
            catch (Exception ex) { LogMsg("Error:" + ex.Message); }
        }

        private void btnLCM_Click(object sender, EventArgs e)
        {
            try
            {
                GetNumbers();

                R = BigInteger.Divide(BigInteger.Abs(BigInteger.Multiply(P, Q)), BigInteger.GreatestCommonDivisor(P, Q));

                ShowResult();
            }
            catch (Exception ex) { LogMsg("Error:" + ex.Message); }
        }


	#endregion
	
	#region Encoding
	
        /// <summary>
        /// Can we run the conversion
        /// </summary>
        private void validateForRun()
        {
            try
            {
                //enable / disable the run button
                btnRun.Enabled = ((_sourceFiles != null && _sourceFiles.Length > 0) || rtxtData.Text.Length > 0)
                            && cmbSourceEnc.SelectedIndex >= 0
                            && cmbDestEnc.SelectedIndex >= 0
                            && cmbSourceEnc.Text != cmbDestEnc.Text &&
                            !cmbSourceEnc.Text.Contains("Jommal");
                //&& !(Converter.IsACCO(Converter.GetEncodingEx(cmbSourceEnc.Text)) && Converter.IsACCO(Converter.GetEncodingEx(cmbDestEnc.Text)));
                if (btnRun.Enabled)
                {
                    if (Converter.IsACCO(Converter.GetEncodingEx(cmbDestEnc.Text)))  chkHexText.Checked = true;
                    if (!Converter.IsACCO(Converter.GetEncodingEx(cmbSourceEnc.Text)))
                    {
                        chkDiscardChars.Checked = true;
                        if (chkHexText.Checked && (cmbDestEnc.Text.Contains("Vocal") ||
                                                   cmbDestEnc.Text.Contains("Hijaei") ||
                                                   cmbDestEnc.Text.Contains("Abjadi"))) 
                             chkDiacritics.Checked = true;
                        else chkDiacritics.Checked = false;
                    }
                    else chkDiscardChars.Checked = false;
                }
                else chkHexText.Checked = false;
                if (_sourceFiles != null)
                {
                    //set the info about the destination directory
                    if (_sourceFiles.Length > 0 && cmbDestEnc.SelectedIndex >= 0)
                    {
                        //john church 05/10/2008 use directory separator char instead of backslash for linux support
                        destDir = _sourceFiles[0].Substring(0, _sourceFiles[0].LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1) + cmbDestEnc.SelectedItem.ToString() + System.IO.Path.DirectorySeparatorChar;
                        LogMsg("Converted files will be output to:"
                                    + System.Environment.NewLine + destDir
                                    + System.Environment.NewLine + _sourceFiles.Length
                                    + " file(s) selected for conversion" + System.Environment.NewLine);
                    }
                }
            }
            catch (Exception ex) { LogMsg("Error:" + ex.Message); }
        }

        /// <summary>
        /// Populate the combos
        /// </summary>
        private void loadEncodings()
        {
            Encoding enc;
            string s;
            cmbDestEnc.Items.Clear();
            cmbSourceEnc.Items.Clear();
            cmbHEncoding.Items.Clear();

            cmbSourceEnc.Items.Add("Arabic [Jommal] - 65538");
            cmbDestEnc.Items.Add("Arabic [Jommal] - 65538");

            string[] customCharsets = Directory.GetFiles(Application.StartupPath, "*.Charset");
            foreach (var file in customCharsets)
            {
                cmbSourceEnc.Items.Add("Arabic ["+Path.GetFileNameWithoutExtension(file)+"] - 65537");
                cmbDestEnc.Items.Add("Arabic [" + Path.GetFileNameWithoutExtension(file) + "] - 65537");
                cmbHEncoding.Items.Add("Arabic [" + Path.GetFileNameWithoutExtension(file) + "] - 65537");
            }
            //loop through all the encodings on the system
            foreach (EncodingInfo en in Encoding.GetEncodings())
            {
                enc = en.GetEncoding();
                //build a string containing the name and codepage number
                s = enc.EncodingName + " - " + enc.CodePage.ToString();
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
		
		private void chkJommalWORD_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "JommalWORD", chkJommalWord.Checked ? "Yes" : "No");
            hexBox.BytesPerLine = Int32.Parse(cmbBytesPerLine.Text);
            hexBox.Refresh();
        }

        private void chkMultiLine_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "MultiLine", chkMultiLine.Checked ? "Yes" : "No");
        }

        private void btnLoadFromClipboard_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                File.WriteAllText(Application.StartupPath + "\\NewCharset.Charset", Clipboard.GetText());
                LoadCharset(Application.StartupPath + "\\NewCharset.Charset");
            }
        }
        private void btnSImage_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabTexture;
            DrawImage();
            CreateBarcode(rtxtData.Text,BarcodeFormat.QR_CODE);
        }
		
		      private void btnSendToCrypto_Click(object sender, EventArgs e)
        {
            try
            {

                fileBuffer = new byte[hexBox.ByteProvider.Length];
                for (int i = 0; i < hexBox.ByteProvider.Length; i++) fileBuffer[i] = hexBox.ByteProvider.ReadByte(i);
                rbFileBuffer.Checked = true;
                tabControl1.SelectedTab = tabCrypto;
            }
            catch (Exception ex) { LogMsg(ex); }
        }
        private void btnColor_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabColor;
            DrawColors();
        }

        private void NormalizeBuffer(byte[] buffer)
        {
            int max=buffer.Max();
            int min = buffer.Min();
            byte mid = (byte) ((max + min) / 2);
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] += (byte) (128 - mid) ;
        }
        private void btnSpectrum_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabAudioSpectrum;
            InjectAudio();
        }
        private void btnSCalc_Click(object sender, EventArgs e)
        {
            rb16.Checked = true;
            txtPrimeP.Text = MyClass.BinaryToHexString(SafeRead(quranBin));
            tabControl1.SelectedTab = tabCalculator;
        }

        private void btnSCrypto_Click(object sender, EventArgs e)
        {
            rbFileBuffer.Checked = true;
            fileBuffer=SafeRead(quranBin);
            tabControl1.SelectedTab = tabCrypto;
        }

        private void btnSHex_Click(object sender, EventArgs e)
        {
            SendToHexViewer(quranBin);
            tabControl1.SelectedTab = tabHexViewer;
        }

        private void btnSendToHex_Click(object sender, EventArgs e)
        {
            if (rbFileBuffer.Checked)
            {
                File.WriteAllBytes(Application.StartupPath+"\\Buffer.bin", fileBuffer);
                SendToHexViewer(Application.StartupPath + "\\Buffer.bin");
                tabControl1.SelectedTab = tabHexViewer;
            }
        }

      private void chkSendToBuffer_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "SendToBuffer", chkSendToBuffer.Checked ? "Yes" : "No");
        }

         private void tabEncoding_Leave(object sender, EventArgs e)
        {
           hex = txtInfo.Text;
        }

        private void tabEncoding_Enter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hex)) txtInfo.Text = hex;
        }

        private void rtxtData_DoubleClick(object sender, EventArgs e)
        {
            File.WriteAllText(Application.StartupPath + "\\tb.txt", rtxtData.Text);
            Process.Start(Application.StartupPath + "\\tb.txt");
            
        }

    
        private void btnToHex_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rtxtData.Text))
            {
                CloseHex();

                chkHexText.Checked = true;
                rbText.Checked= true;
                string s = rtxtData.Text;
                s = Converter.FilterData(s, 65001, chkDiscardChars.Checked, chkDiacritics.Checked, chkzStrings.Checked);
                byte[] b = MyClass.GetBytes(s);
                File.WriteAllBytes(quranBin, b);
                txtInfo.Text= MyClass.BinaryToHexString(b);
            }
        }

         private void chkALLEncodings_CheckedChanged(object sender, EventArgs e)
        {
            loadEncodings();
            validateForRun();
            AppSettings.WriteValue("Settings", "AllEncodings", chkALLEncodings.Checked?"Yes":"No");
        }
        private void txtInfo_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(txtInfo.Text);
            if (tabControl1.SelectedTab == tabEncoding && txtInfo.Text.isHex())
            {
                rb16.Checked = true;
                txtPrimeP.Text = txtInfo.Text;
                tabControl1.SelectedTab = tabCalculator;
            }
        }

        private void chkHexText_CheckedChanged(object sender, EventArgs e)
        {
            txtInfo.RightToLeft = ((chkRTL.Checked && !chkHexText.Checked) ? RightToLeft.Yes : RightToLeft.No);
        }
        private bool SelectEncoding(string contains)
        {
            int i = 0; bool found = false;
            while (i < cmbDestEnc.Items.Count && !found)
            {
                if (!cmbDestEnc.Items[i].ToString().Contains(contains)) i++; else found = true;
            }
           if (found) cmbDestEnc.SelectedIndex = i;
            return found;
        }

        /// <summary>
        /// Show some info about the currently selected codepage
        /// </summary>
        /// <param name="s">String - The selected encoding</param>
        /// <param name="t">The textbox to fill</param>
        private void buildEncodingInfo(string s, TextBox t)
        {
            //get the codepage number
            int cp = Converter.GetEncodingEx(s);
            if (!Converter.IsACCO(cp))
            {
                //get the encoding
                Encoding enc = Encoding.GetEncoding(cp);
                System.Text.StringBuilder sb = new();
                if (enc != null)
                {
                    //build up some information
                    sb.Append("WebName: " + enc.WebName + Environment.NewLine);
                    sb.Append("Copdepage: " + enc.CodePage.ToString() + Environment.NewLine);
                    if (enc.IsSingleByte == true)
                        sb.Append("Single Byte Charset");
                    else
                        sb.Append("Multi Byte Charset");
                }
                //output it to the textbox
                t.Text = sb.ToString();
            }
            else t.Text = "WebName: Arabic Common Charset Order \r\nCodepage: ACCO-"+cp.ToString()+"\r\nSingle Byte Charset";
        }

        private void btnClearFiles_Click(object sender, EventArgs e)
        {
           lsSource.Items.Clear();
        }

        private void rtxtData_TextChanged(object sender, EventArgs e)
        {
            validateForRun();
            sentSora = "";encoding = "";
            rbText.Checked = true;
        }

        private void rtxtData_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                if (cmbSourceEnc.SelectedIndex>=0)
                rtxtData.Text = Converter.ReadAllText(files[0],cmbSourceEnc.SelectedItem.ToString());
            }
        }

        private void rtxtData_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void chkRTL_CheckedChanged(object sender, EventArgs e)
        {
            rtxtData.RightToLeft = (chkRTL.Checked ? RightToLeft.Yes : RightToLeft.No);
            txtInfo.RightToLeft = ((chkRTL.Checked && !chkHexText.Checked) ? RightToLeft.Yes : RightToLeft.No);
        }


        private void Encode() 
        {
            string sourceCP, destCP;
            string s;
            string res;
            int iSuccess = 0;
            string sFile;
            bool Jommal = false;

            Converter.SpecialTypes specialType = Converter.SpecialTypes.None;

            //03/05/2007 tidy up the UI
            bool btnStat = btnRun.Enabled;
            btnRun.Enabled = false;
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            try
            {
                CloseHex();

                //get the conversion codepages
                sourceCP = cmbSourceEnc.SelectedItem.ToString();
                destCP = cmbDestEnc.SelectedItem.ToString();
                if (destCP.Contains("Jommal")) { destCP = "Arabic [Common] - 65537"; Jommal = true; }

                //check for special options
                if (chkUnicodeAsDecimal.Checked == true)
                    specialType = Converter.SpecialTypes.UnicodeAsDecimal;

                if (_sourceFiles != null && rbFiles.Checked)
                {
                    //convert each file
                    for (int i = 0; i < _sourceFiles.Length; i++)
                    {
                        //display some information about the file
                        lsSource.Items[i] = lsSource.Items[i].ToString().Replace(char.ConvertFromUtf32(9745) + " ", "").Replace(char.ConvertFromUtf32(9746) + " ", "");
                        s = lsSource.Items[i].ToString();
                        LogMsg("Converting " + (i + 1).ToString() + " of " + _sourceFiles.Length + System.Environment.NewLine);


                        //do the conversion
                        if ((sFile = Converter.ConvertFile(_sourceFiles[i], destDir,
                                                            sourceCP, destCP, specialType, chkMeta.Checked, chkDiscardChars.Checked,
                                                            chkDiacritics.Checked, chkzStrings.Checked)) != "")
                        {
                            //success
                            res = char.ConvertFromUtf32(9745);
                            if (Jommal) File.WriteAllBytes(sFile, toJommal(File.ReadAllBytes(sFile)));

                            if (chkOutText.Checked)
                            {

                                if (chkHexText.Checked)
                                {
                                    byte[] file = File.ReadAllBytes(sFile);
                                    MyClass.BinaryToHexString(file);
                                    lblStatus.Text = file.Length.ToString();
                                }
                                else
                                {
                                    Converter.ReadAllText(sFile, destCP);
                                    lblStatus.Text = txtInfo.Text.Length.ToString();
                                }
                            }

                            iSuccess++;
                            if (chkSendToBuffer.Checked) { rbFileBuffer.Checked = true; fileBuffer = File.ReadAllBytes(sFile); }
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
                        progFiles.Value = (int)((double)(i + 1) / (double)_sourceFiles.Length * 100);
                    }
                    //display the final message
                    LogMsg(iSuccess.ToString() + " file(s) converted successfully and output to:" +
                                                      System.Environment.NewLine + destDir + System.Environment.NewLine);
                }
                else
                if (rtxtData.Text.Length > 0 && rbText.Checked)
                {
                    LogMsg("Converting From TextBox");
                    sFile = quranBin;
                    byte[] sOut = { };
                    if (chkMultiLine.Checked)
                    {
                        string[] lines = rtxtData.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        File.Delete(sFile);
                        foreach (var str in lines)
                        {
                            sOut = Converter.ConvertText(str, sourceCP, destCP, specialType, chkMeta.Checked, chkDiscardChars.Checked, chkDiacritics.Checked, chkzStrings.Checked);
                            if (Jommal) sOut = toJommal(sOut);
                            MyClass.AppendAllBytes(sFile, sOut);
                            txtInfo.Text=(chkHexText.Checked ? MyClass.BinaryToHexString(sOut) : Converter.ReadAllText(str, destCP)) + Environment.NewLine;
                        }
                    }
                    else
                    {
                        sOut = Converter.ConvertText(rtxtData.Text, sourceCP, destCP, specialType, chkMeta.Checked, chkDiscardChars.Checked, chkDiacritics.Checked, chkzStrings.Checked);
                        if (Jommal) sOut = toJommal(sOut);
                        File.WriteAllBytes(sFile, sOut);
                    }

                    //if (Jommal) File.WriteAllBytes(sFile,toJommal(File.ReadAllBytes(sFile))); 

                    sOut = File.ReadAllBytes(sFile);

                    LogMsg("Written to " + sFile);
                    if (chkSendToBuffer.Checked) { rbFileBuffer.Checked = true; fileBuffer = sOut; }
                    if (!chkMultiLine.Checked) txtInfo.Text=(chkHexText.Checked ? MyClass.BinaryToHexString(sOut) : Converter.ReadAllText(sFile, destCP));
                    lblStatus.Text = sOut.Length.ToString();
                    encoding = cmbDestEnc.Text;
                }
                else LogMsg("Nothing to encode");
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
                this.Cursor = System.Windows.Forms.Cursors.Default;
                if (chkHexText.Checked) hex = txtInfo.Text;
            }
        }
       /// <summary>
        /// Handler for the run button click event
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void btnRun_Click(object sender, EventArgs e)
        {
            Encode();
        }
		
		
        /// <summary>
        /// Handle the open file menu click event
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        /// 
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile=new();

            //set up the open file dialog
            openFile.Multiselect = true;
            openFile.Filter = "All files (*.*)|*.*";
            openFile.FilterIndex = 0;
            openFile.ShowDialog();
            //get the filenames
            _sourceFiles = openFile.FileNames;
            string filename;
            foreach (string s in _sourceFiles)
            {
                //add the filesnames to the list box
                //john church 05/10/2008 use directory separator char instead of backslash for linux support
                filename = s.Substring(s.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
                lsSource.Items.Add(filename);
            }
            if (_sourceFiles.Length > 0) rbFiles.Checked = true;
            //display the message
            LogMsg (_sourceFiles.Length + " file(s) selected for conversion" + System.Environment.NewLine);
            //validate the run button
            validateForRun();
        }

        /// <summary>
        /// Handler for the combo change event
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void cmbSourceEnc_SelectedIndexChanged(object sender, EventArgs e)
        {
            validateForRun();
            buildEncodingInfo(cmbSourceEnc.SelectedItem.ToString(), txtSourceEnc);
        }

        /// <summary>
        /// Handler for the combo change event
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void cmbDestEnc_SelectedIndexChanged(object sender, EventArgs e)
        {
            validateForRun();
            buildEncodingInfo(cmbDestEnc.SelectedItem.ToString(), txtDestEnc);

            string charSetFile = Converter.GetCharsetFile(cmbDestEnc.Text);
            LoadCharset(charSetFile + ".Charset");
            btnFindKey.Enabled = !cmbDestEnc.Text.Contains("Auto");
        }


        private byte[] toJommal(byte[] buffer)
        {
            List<byte> jBuffer = new();
            for (int i=0;i<buffer.Length;i++)
            {
                byte h = (byte) (jommalCharset[buffer[i]-1] >> 8);
                byte l = (byte) (jommalCharset[buffer[i]-1] & 0xFF);
                if (h > 0 || chkJommalWord.Checked) jBuffer.Add(h);
                jBuffer.Add(l);
            }
            return jBuffer.ToArray();
        }

       //filenames to convert
        private string[] _sourceFiles = null;
        //ouput directory
        private string destDir = null;



        /// <summary>
        /// Main form constructor
        /// </summary>
  

        private void SendToHexViewer(string sFile)
        {
 
            hexBox.ByteProvider = new FileByteProvider(sFile);
            lblHexFile.Text = sFile;
            int i= 0;
            while (cmbHEncoding.Items[i].ToString()!=cmbSourceEnc.Text) i++;
            cmbHEncoding.SelectedIndex = i;
            hexBox.Refresh();
            CalcHHash();
        }

        private void btnFindKey_Click(object sender, EventArgs e)
        {
            bool done = false;
            FactorData factorData;

            btnFindKey.Enabled = false;
            
           
            if (string.IsNullOrEmpty(rtxtData.Text)) return;
            lsSource.Items.Clear();
 
            txtOut.Text = "";
            
            txtPlus.Text = "1";

            string charSetFile = Converter.GetCharsetFile(cmbDestEnc.Text);
            if (string.IsNullOrEmpty(charSetFile)) { LogMsg("No Charset File!"); return; }

            LoadCharset(charSetFile + ".Charset");
            if (charSetFile.IndexOf("-Auto") < 0) charSetFile += "-Auto";
            SaveCharset(charSetFile + ".Charset");
            FindInCombo(cmbDestEnc, "Arabic [" + charSetFile + "] - 65537", false, true);
            int i = 0;
            do
            {
                Encode();
                rb16.Checked = true;
                txtPrimeP.Text = MyClass.BinaryToHexString(SafeRead(quranBin));
                rb10.Checked = true;
                GetNumbers();
                if (!P.IsEven)
                {
                    factorData = FactorDB(txtPrimeP.Text, (WebMethod)cmbWebClient.SelectedIndex);
                    if (factorData.factors <= 2 && (chkAllFactors.Checked || factorData.code.Equals("FF") || factorData.code.Equals("P")))
                    {
                        DisplayFactors(factorData);
                    }
                    
                }

                done = !IncCharset(1);
                SaveCharset(charSetFile + ".Charset");
                lblProgress.Text = i++.ToString();Application.DoEvents();
            } while (!done);

            LogMsg("Finished finding key");
            btnFindKey.Enabled = true;
        }

    #endregion

    #region Quran
        private void rbTextType_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                AppSettings.WriteValue("Settings", "QuranText", (sender as RadioButton).Name.ToString());
                float n = txtQuranText.Font.Size;
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

        private void lblFontSize_Click(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "FontSize", lblFontSize.Text.ToString());
        }
       private void txtSearch_Enter(object sender, EventArgs e)
        {
            ArabicKeyboard();
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            OriginalKeyboard();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\Quran.accdb;User Id=;Password=;";
            string queryString = "SELECT * FROM Quran where AyaTextSearch LIKE '%" + txtSearch.Text + "%'";
            DataTable dt=new();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (OleDbCommand command = new OleDbCommand(queryString, connection))
            using (OleDbDataAdapter da = new OleDbDataAdapter(command))
            {
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
        }

        private void dgvQuran_Click(object sender, EventArgs e)
        {
            txtQuranText.Text = dgvQuran.Rows[dgvQuran.CurrentRow.Index].Cells[quran.CurQuranTextIndex()].Value.ToString();
        }
 
		private void btnSendToEncoding_Click(object sender, EventArgs e)
        {
            
            rtxtData.Text = txtQuranText.Text;
            tabControl1.SelectedTab = tabEncoding;
            cmbSourceEnc.SelectedIndex = 0;
            int i = 0; bool found = false;
            while (i < cmbSourceEnc.Items.Count && !found)
            {
                if (!cmbSourceEnc.Items[i].ToString().Contains("65001")) i++; else found = true;
            }
            if (found) cmbSourceEnc.SelectedIndex = i;

            string s = lblCurCharset.Text.Substring(0, lblCurCharset.Text.IndexOf("."));

            if (rbFirstOriginal.Checked || rbFirstOriginalDots.Checked) s="Hijaei";
            if (rbNoDiacritics.Checked) s="Hijaei-Hamza";

            //if (!SelectEncoding("["+s+"]")) 
                SelectEncoding("[Common]");
            sentSora = lbSoras.Text;
            chkRTL.Checked = true;
        }

        private void SelectSora()
        {

            DataTable sora = quran.GetSoraTable(lbSoras.SelectedIndex);
            txtQuranText.Text = "";
            dgvQuran.Columns.Clear();
            dgvQuran.DataSource = sora;
            dgvQuran.Refresh();
            string s = "";
            int n = quran.CurQuranTextIndex();
           // foreach (DataColumn col in sora.Columns)
           //     Console.WriteLine(col.ColumnName);
            foreach (DataRow row in sora.Rows)
            {
                
                s += row[n].ToString() + Environment.NewLine;
            }
            txtQuranText.Text = s;
            s=s.Replace(" ", "").Replace("\r","").Replace("\n","");
            lblStatus.Text = s.Length.ToString();
        }

        private void lbSoras_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectSora();
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            float n = txtQuranText.Font.Size;
            if (n < 200) txtQuranText.Font = new Font(txtQuranText.Font.FontFamily, n + 1);
            lblFontSize.Text = n.ToString();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            float n = txtQuranText.Font.Size;
            if (n > 5) txtQuranText.Font = new Font(txtQuranText.Font.FontFamily, n - 1);
            lblFontSize.Text = n.ToString();
        }

        private void txtSoraSearch_Enter(object sender, EventArgs e)
        {
            ArabicKeyboard();
        }

        private void txtSoraSearch_Leave(object sender, EventArgs e)
        {
            OriginalKeyboard();
        }
        private void txtSoraSearch_TextChanged(object sender, EventArgs e)
        {
            lbSoras.ClearSelected();
            int index = FindInList(lbSoras, txtSoraSearch.Text);
            if (index < 0 && lbSoras.Items.Count > 0) index = 0;
            if (index >= 0)
                lbSoras.SetSelected(index, true);
        }

    #endregion

    #region Charset

        int Max(TextBox[] list)
        {
            int max = ValueOf(list[0].Text);
            for (int i=1;i<list.Length;i++)
            {
                int n = ValueOf(list[i].Text);
                if (n > max) max = n;
            }
            return max;
        }
        int Min(TextBox[] list)
        {
            int min = ValueOf(list[0].Text);
            for (int i = 1; i < list.Length; i++)
            {
                int n = ValueOf(list[i].Text);
                if (n < min) min = n;
            }
            return min;
        }
        private bool IncCharset(int n)
        {
            int max = Max(listTxtCS);
            int min = Min(listTxtCS);

            if ((max + n) > 255 || (min + n) < 0) return false;

            for (int i = 0; i < 44; i++)
            {
                int v = ValueOf(listTxtCS[i].Text);
                if (v != 0) listTxtCS[i].Text = ValidChar(v + n).ToString();
            }
            return true;
        }

        private void lblCurCharset_DragDrop(object sender, DragEventArgs e)
        {
            Label lbl = (Label)sender;
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                File.WriteAllText(Application.StartupPath+"\\NewCharset.Charset", (string)e.Data.GetData(DataFormats.Text));
                LoadCharset(Application.StartupPath + "\\NewCharset.Charset");
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files != null && files.Length != 0)
                {
                    LoadCharset(File.ReadAllText(files[0]));
                }

            }
        }

        private void lblCurCharset_DragEnter(object sender, DragEventArgs e)
        {
                if (e.Data.GetDataPresent(DataFormats.Text) || e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
        }

        private void btnAutoAdd_Click(object sender, EventArgs e)
        {
            int n;
            if (!Int32.TryParse(txtPlus.Text, out n)) n = 0;

            if (sender is Button b) if (b.Name.Contains("Sub")) n = -n;

            IncCharset(n);
        }

          private void btnReset_Click(object sender, EventArgs e)
        {
            ResetCharsets(true);
        }

        private class ComparerClass : IComparer
        {
            // Call CaseInsensitiveComparer.Compare with the parameters reversed.
            int IComparer.Compare(Object x, Object y)
            {
                //return ((new CaseInsensitiveComparer()).Compare(Int32.Parse((x as TextBox).Text), Int32.Parse((y as TextBox).Text)));
                string xs = Int32.Parse((x as TextBox).Text).ToString("D3")+(x as TextBox).TabIndex.ToString("D2");
                string ys = Int32.Parse((y as TextBox).Text).ToString("D3") + (y as TextBox).TabIndex.ToString("D2");
                return new CaseInsensitiveComparer().Compare(xs, ys);
            }
        }
        private void ReOrderChars()
        {
            int n,m=0,j=0;
            TextBox[] nTXT = (TextBox[]) listTxtCS.Clone();
            Array.Sort(nTXT, new ComparerClass());


            for (int i = 0; i < 44; i++)
            {
                n = Int32.Parse(nTXT[i].Text);
                if (n != m && m!=0) j++;
                if (n!=0)
                {
                    listLblCSS[j].Text = listLblCS[nTXT[i].TabIndex-1].Text;
                    m = n;
                }
                //if (n > 0 && n < 45) listLblCSS[n - 1].Text = listLblCS[Int32.Parse(listTxtCS[i].Name.Substring(5)) - 1].Text;
            }
        }
        private void btnOrder_Click(object sender, EventArgs e)
        {
            ReOrderChars();
        }

         private long startTime = 0;
        private long TimeElapsed
        {
            get { return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - startTime; }
            set { startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond + value; }
        }
        private void LoadCharset(string fileName)
        {
            try
            {
                string ts = "";
                TimeElapsed = 0;
                ClearChars(); ts += TimeElapsed + ",";
                byte[] c = MyClass.HexStringToBinary(File.ReadAllText(fileName)); ts += TimeElapsed + ",";
                AppSettings.WriteValue("Settings", "CharsetProfile", Path.GetFileName(fileName)); ts += TimeElapsed + ",";
                for (int i = 0; i < 44; i++)
                {
                    listTxtCS[i].Text = c[i].ToString();
                }
                ts += TimeElapsed + ",";
                ReOrderChars(); ts += TimeElapsed + ",";
                lblCurCharset.Text = Path.GetFileName(fileName);
                LogMsg(ts);
                LogMsg("Custom Charset Loaded");
            } catch (Exception ex) { LogMsg(ex); }
        }
        private void btnLoadCharset_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFile = new();
                //set up the open file dialog
                openFile.Multiselect = false;
                openFile.Filter = "All files (*.Charset)|*.Charset";
                openFile.FilterIndex = 0;
                openFile.InitialDirectory = Application.StartupPath;
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    LoadCharset(openFile.FileName);
                }
            }
            catch (Exception ex) { LogMsg(ex); }
        }

        private byte ValidChar(int b)
        {
            int n = b;
            return (byte)(b > 255 ? 0 : b);
        }

        private int ValueOf(string s)
        {
            int n;
            if (Int32.TryParse(s,out n)) return n; else return 0;
        }
        private void btnAutoCharset_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCS1.Text.Trim()))
            {
                LogMsg("Fill the first Charset Value first");
                return;
            }

            for (int i=1; i < 44; i++)
            {
                  listTxtCS[i].Text = ValidChar(ValueOf(txtCS1.Text) + i).ToString();
            }
        }

        private string GetCharset()
        {
            byte[] c = new byte[44];
            for (int i = 0; i < 44; i++)
            {
                c[i] = ValidChar(ValueOf(listTxtCS[i].Text));
            }
            return MyClass.BinaryToHexString(c);
        }
        private void SaveCharset(string fileName)
        {

            File.WriteAllText(fileName, GetCharset()); ;
            lblCurCharset.Text = Path.GetFileName(fileName);
            AppSettings.WriteValue("Settings", "CharsetProfile", lblCurCharset.Text);
            string s1 = cmbSourceEnc.Text;
            string s2 = cmbDestEnc.Text;
            loadEncodings();
            cmbSourceEnc.Text = s1;
            cmbDestEnc.Text = s2;
            validateForRun();
        }
        private void btnSaveCharset_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = Application.StartupPath+"\\"+ lblCurCharset.Text;
                if ((sender as Button).Name != "btnSave")
                {
                    SaveFileDialog dlg = new SaveFileDialog();
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

            } catch (Exception ex) { LogMsg(ex); }
        }

        private void ClearChars()
        {
            foreach (var t in listTxtCS)
                t.Text = "";
            foreach (var t in listLblCSS)
                t.Text = "";
            lblCurCharset.Text = "New.Charset";
        }
        private void btnClearCS_Click(object sender, EventArgs e)
        {
            ClearChars();
        }

 
        private void ResetCharsets(bool overwite=false)
        {
            for (int i = 0; i < charsetName.Length; i++)
                if (!File.Exists(Application.StartupPath + "\\" + charsetName[i]) || overwite)
                    File.WriteAllText(Application.StartupPath + "\\" + charsetName[i], MyClass.BinaryToHexString(Charsets[i]));
        }

        private void GetCharsetControls()
        {
            IEnumerable<TextBox> txtCSI = GetAllControls<TextBox>(tabCharset);
            IEnumerable<Label> lblCSI = GetAllControls<Label>(tabCharset);

            foreach (var t in txtCSI)
              if (t.Name.StartsWith("txtCS")) listTxtCS[Int32.Parse(t.Name.Substring(5)) - 1] = t;

            foreach (var t in lblCSI)
                if (t.Name.StartsWith("lblCSS"))
                    listLblCSS[Int32.Parse(t.Name.Substring(6)) - 1] = t;
                else
                    if (t.Name.StartsWith("lblCS")) listLblCS[Int32.Parse(t.Name.Substring(5)) - 1] = t;
        }
	#endregion
	
	#region HexViewer
	      private void cmbBytesPerLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "BytesPerLine", cmbBytesPerLine.Text);
            hexBox.BytesPerLine = Int32.Parse(cmbBytesPerLine.Text);
            hexBox.Refresh();
        }

        private void cmbHEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            hexBox.ByteCharConverter = new ByteCharProvider(cmbHEncoding.Text);
            hexBox.Refresh();
        }

        private void hexBox_DragDrop(object sender, DragEventArgs e)
        {
            var filePath = ((string[])(e.Data.GetData(DataFormats.FileDrop)))[0];
            var source = new FileByteProvider(filePath);
            lblHexFile.Text = filePath;
            hexBox.ByteProvider = source;
            hexBox.Refresh();
        }

        private void hexBox_DragOver(object sender, DragEventArgs e)
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
                FileByteProvider fileByteProvider = hexBox.ByteProvider as FileByteProvider;
                fileByteProvider.ApplyChanges();
                LogMsg("Changes Applied to Disk");
            }
            catch (Exception ex) { LogMsg(ex); }
        }

        private void btnApplyChanges_Click(object sender, EventArgs e)
        {
            ApplyChanges();
        }

        private void btnSendToCalc_Click(object sender, EventArgs e)
        {
            try
            {

                byte[] buffer = new byte[hexBox.ByteProvider.Length];
                for (int i = 0; i < hexBox.ByteProvider.Length; i++) buffer[i] = hexBox.ByteProvider.ReadByte(i);
                rb16.Checked = true; 
                txtPrimeP.Text = MyClass.BinaryToHexString(buffer);
                tabControl1.SelectedTab = tabCalculator;
            } catch (Exception ex) { LogMsg(ex); }
        }

        private void CalcHHash()
        {
            try
            {
                if (hexBox.ByteProvider == null) return;
                byte[] buffer = new byte[hexBox.ByteProvider.Length];
                for (int i = 0; i < hexBox.ByteProvider.Length; i++) buffer[i] = hexBox.ByteProvider.ReadByte(i);
                byte[] hash = MyClass.GetHash(buffer, cmbHHash.Text);
                lblHash.Text = MyClass.BinaryToHexString(hash);
            } catch (Exception ex) { LogMsg(ex); }
        }
        private void hexBox_TextChanged(object sender, EventArgs e)
        {
            CalcHHash();
        }

        private void cmbHHash_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcHHash();
        }


        private void btnCalcHHash_Click(object sender, EventArgs e)
        {
            CalcHHash();
        }
       private void tabHexViewer_DragDrop(object sender, DragEventArgs e)
        {
            var filePath = ((string[])(e.Data.GetData(DataFormats.FileDrop)))[0];
            var source = new FileByteProvider(filePath);
            lblHexFile.Text = filePath;
            hexBox.ByteProvider = source;
            hexBox.Refresh();
        }

        private void tabHexViewer_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void CloseHex()
        {
            if (hexBox.ByteProvider is FileByteProvider f)  (hexBox.ByteProvider as FileByteProvider).Dispose();
            hexBox.ByteProvider = null;
            lblHexFile.Text = "";
            hexBox.Refresh();
            LogMsg("HexView Cleared");
        }
        private void CheckChanges()
        {
            if (hexBox.ByteProvider == null) return;
            if (hexBox.ByteProvider.HasChanges())
            {
                if (MessageBox.Show("Do you want to apply changes?", "Changes found!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    ApplyChanges();
            }
        }
        private void btnClearHex_Click(object sender, EventArgs e)
        {
            CheckChanges();
            CloseHex();
        }

        private void btnOpenHexFile_Click(object sender, EventArgs e)
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
                    hexBox.ByteProvider =new FileByteProvider( openFile.FileName );
                    lblHexFile.Text = openFile.FileName;
                    hexBox.Refresh();
                    LogMsg("File Loaded:" + openFile.FileName);
                }
            }
            catch (Exception ex) { LogMsg(ex); }
        }

        #endregion

    #region Texture
        private void btnBarcode_Click(object sender, EventArgs e)
        {
            Blink(true);
            //picQuran1.Image = picQuran3.Image;
            ReadBarcode((Bitmap)picQuran3.Image);
            Blink(false);
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Save Image File";
            dlg.DefaultExt = "Image";
            dlg.Filter = "Image File|*.jpg";
            dlg.InitialDirectory = Application.StartupPath;
            dlg.FileName = sentSora!=""?sentSora+"-"+encoding:".jpg";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                picQuran1.Image.Save(dlg.FileName);
                LogMsg("Image Saved :" + dlg.FileName);
            }
        }
        private void btnScreenShot_Click(object sender, EventArgs e)
        {
            Visible = false;
            Thread.Sleep(1000);
            picQuran1.Image = ScreenCapture.CaptureScreen();
            Visible = true;
            Blink(true);
            picColor1.Invalidate();
            ReadBarcode((Bitmap) picColor1.Image);
            Blink(false);
        }
        private void chkQR_CheckedChanged(object sender, EventArgs e)
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
                picQuran3.Top = picQuran2.Top+picQuran2.Height+6;
                picQuran3.Height = picQuran1.Height-picQuran2.Height-6;
                picQuran3.Width = picQuran2.Width;
            }
        }

        private void picQuran1_DoubleClick(object sender, EventArgs e)
        {
            if (!picQuran1.Enabled) return;
            picQuran1.Enabled = false;
            try
            {
                string photo = Application.StartupPath + "\\~tmp.jpg";
                picQuran1.Image.Save(photo, System.Drawing.Imaging.ImageFormat.Jpeg);

                // this will not return proc, will be null, since it will not start viewer directly, done via dllhost
                //var proc=Process.Start(new ProcessStartInfo(photo) { Verb = "open" });      // you can use "edit" to edit image   

                ViewImage(photo);
              

                File.Delete(photo);
            }
            catch (Exception ex) { LogMsg(ex); }

            picQuran1.Enabled = true;
        }
        private void picQuran3_DoubleClick(object sender, EventArgs e)
        {
            if (!picQuran3.Enabled) return;
            picQuran3.Enabled = false;
            try
            {
                string photo = Application.StartupPath + "\\~tmp3.jpg";
                picQuran3.Image.Save(photo, System.Drawing.Imaging.ImageFormat.Jpeg);

                // this will not return proc, will be null, since it will not start viewer directly, done via dllhost
                //var proc=Process.Start(new ProcessStartInfo(photo) { Verb = "open" });      // you can use "edit" to edit image   

                ViewImage(photo);


                File.Delete(photo);
            }
            catch (Exception ex) { LogMsg(ex); }

            picQuran3.Enabled = true;
        }

        private void CreateBarcode(string str,BarcodeFormat format= BarcodeFormat.QR_CODE)
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
               LogMsg(exc.Message);
            }
        }
        private void lblScale_TextChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "Scale", lblPointSize.Text);
        }

        private void cmbSizeMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSizeMod(picQuran1,picQuran2);
            DrawImage();
            AppSettings.WriteValue("Settings", "SizeMode", cmbSizeMode.SelectedIndex.ToString());
        }

        private void tabImage_Click(object sender, EventArgs e)
        {

        }

        private void btnResetImage_Click(object sender, EventArgs e)
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

        private void chkFixSquare_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "Padding", chkFixPadding.Checked ? "YES": "NO");
            DrawImage();
        }

        private void chkFlipY_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "FlipY", chkFlipY.Checked ? "YES" : "NO");
            DrawImage();
            picQuran3.Image.RotateFlip(RFType(chkFlipX.Checked,chkFlipY.Checked));
        }

        private void chkFlipX_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "FlipX", chkFlipX.Checked ? "YES" : "NO");
            DrawImage();
            picQuran3.Image.RotateFlip(RFType(chkFlipX.Checked,chkFlipY.Checked));
        }

        private void chkINV_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "Inversed", chkINV.Checked ? "YES" : "NO");
            backColor = (chkINV.Checked ? Color.White : Color.Black);
            DrawImage();
        }
		     private void btnRotate_Click(object sender, EventArgs e)
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

        private void btnSPlus_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(lblPointSize.Text);
            if (n<100) n++;
            lblPointSize.Text = n.ToString();
            DrawImage();
        }

        private void btnSMinus_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(lblPointSize.Text);
            if (n >1) n--;
            lblPointSize.Text = n.ToString();
            DrawImage();
        }

        private void lblScale_Click(object sender, EventArgs e)
        {

        }

        private void DrawImage()
        {
            try
            {
                if (!File.Exists(quranBin)) return;
                byte[] buffer = SafeRead(quranBin);
                if (buffer.Length == 0) return;
                int padding = 0;
                int n = buffer.Length * 8;
                int bpl = (int)Math.Sqrt(n);
                
                while ((bpl * bpl) < n) bpl++;
            
                if (chkFixPadding.Checked)
                {
                    padding = bpl * bpl - n;
                    //if (padding < 0) padding *= -1;
                }

                int pointSize = Int32.Parse(lblPointSize.Text);

                string bin = ToBinaryString(buffer);

                SetSizeMod(picQuran1,picQuran2);

                //var g= picQuran.CreateGraphics();
                picQuran1.Image = new Bitmap(bpl * pointSize, bpl * pointSize);
                picQuran3.Image = new Bitmap(n * pointSize, picQuran3.Height);

                var g = Graphics.FromImage(picQuran1.Image);
                var g2 = Graphics.FromImage(picQuran3.Image);

                SolidBrush brush = new(chkINV.Checked ? Color.Black : Color.White);
                Pen pen = new(chkINV.Checked ? Color.Black : Color.White, pointSize);// float.Parse(lblScale.Text));

                picQuran1.BackColor = backColor;
                picQuran3.BackColor = backColor;

                g.Clear(picQuran1.BackColor); picQuran1.Invalidate();

                for (int i = 0; i < bin.Length + padding; i++)
                {
                    int y = (i / bpl) + 1;
                    int x = (i % bpl) + 1;
                    if (i>=padding) if (bin[i - padding] == '1') DrawPoint(g, brush, pointSize,1, x, y);
                    if (i>=padding) if (bin[i - padding] == '1') g2.DrawLine(pen, i * pointSize, 0, i * pointSize, picQuran3.Height);
                }
                switch ((chkFlipX.Checked ? 2 : 0) + (chkFlipY.Checked ? 1 : 0))
                {
                    case 1: picQuran1.Image.RotateFlip(RotateFlipType.RotateNoneFlipY); break;
                    case 2: picQuran1.Image.RotateFlip(RotateFlipType.RotateNoneFlipX); break;
                    case 3: picQuran1.Image.RotateFlip(RotateFlipType.RotateNoneFlipXY); break;
                }

                picQuran1.Invalidate();
                picQuran3.Invalidate();
                pen.Dispose();
                brush.Dispose();
                g.Dispose();
                g2.Dispose();
            }
            catch (Exception ex) { LogMsg(ex); };
        }
         private void SetSizeMod(PictureBox pic1,PictureBox pic2)
        {

            switch (cmbSizeMode.SelectedIndex)
            {
                case 0: pic1.SizeMode = PictureBoxSizeMode.Normal; pic1.SizeMode = PictureBoxSizeMode.Normal; break;
                case 1: pic1.SizeMode = PictureBoxSizeMode.StretchImage; pic1.SizeMode = PictureBoxSizeMode.StretchImage; break;
                case 2: pic1.SizeMode = PictureBoxSizeMode.AutoSize; pic1.SizeMode = PictureBoxSizeMode.AutoSize; break;
                case 3: pic1.SizeMode = PictureBoxSizeMode.CenterImage; pic1.SizeMode = PictureBoxSizeMode.CenterImage; break;
                case 4: pic1.SizeMode = PictureBoxSizeMode.Zoom; pic1.SizeMode = PictureBoxSizeMode.Zoom; break;
            }
            pic1.Width = picSpace.size1;
            pic1.Height = picSpace.size1;
            pic2.Width = picSpace.size2;
            pic2.Height = (int)(picSpace.size2/1.25);
            pic1.Left = picSpace.x1; pic1.Top = picSpace.y1; 
            pic2.Left = picSpace.x2; pic2.Top = picSpace.y2;

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

        public void DrawPoint(Graphics g,int pointSize,int borderSize,int x,int y, Color color, bool fill=true )
        {
            DrawPoint(g, new SolidBrush(color), pointSize,borderSize, x, y, fill);
        }
		
        public void DrawPoint(Graphics g,SolidBrush brush,int pointSize,int borderSize, int x, int y,bool fill=true)
        {
            x -= 1;y -= 1;
            var pen= new Pen(brush, pointSize);
            Point dPoint = new Point(x*pointSize+(!fill?pointSize/2:0) , y*pointSize + (!fill ? pointSize / 2 : 0));
            Rectangle rect = new Rectangle(dPoint, new Size((borderSize-(fill?0:1))*pointSize, (borderSize-(fill?0:1))*pointSize ));
            if (fill) 
                g.FillRectangle(brush, rect); 
            else 
                g.DrawRectangle(pen, rect);
            pen.Dispose();
        }

        void BlinkTimer_Tick(object sender,EventArgs e)
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
        void Blink(bool enable)
        {
            if (enable && blinkTimer==null)
            {
                blinkTimer = new Timer();
                blinkTimer.Tick += BlinkTimer_Tick;
                blinkTimer.Interval = 500;
                blinkTimer.Tag = "run";
                blinkTimer.Start();
            }
            else if (blinkTimer!=null) blinkTimer.Tag = "end";
        }
        void ReadBarcode(Bitmap bmp)
        {
            var reader = new BarcodeReader();
            var result = reader.Decode(bmp);
            if (result != null)
            {
                LogMsg("Read Successfull : " + result.BarcodeFormat.ToString() + "[ " + result.Text + " ]");
                OutputMsg(result.BarcodeFormat.ToString() + Environment.NewLine + result.Text);
            }
        }
        void webCamTimer_Tick(object sender, EventArgs e)
        {
            var bitmap = wCam.GetCurrentImage();
            if (bitmap == null)
                return;
            ReadBarcode(bitmap);
        }
        private void btnScan_Click(object sender, EventArgs e)
        {
            if (wCam == null)
            {
                chkQR.Checked = true;
                chkQR.Enabled = false;
                btnBarcode.Enabled = false;
                btnScreenShot.Enabled = false;
                Blink(true);

                picQuran2.Visible = false;
                picQuran3.Visible = false;
                picQuran1.Width = picQuran2.Left + picQuran2.Width-picQuran1.Left;
                
                Application.DoEvents();

                wCam = new WebCam { Container = picQuran1 };
                wCam.OpenConnection();


                webCamTimer = new Timer();
                webCamTimer.Tick += webCamTimer_Tick;
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
                wCam.Dispose();
                wCam = null;
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
            Pen[] pens = new Pen[3] { new Pen(Color.LightCyan, 1.5f), new Pen(Color.LightYellow), new Pen(Color.LightCoral) };
            picSpectrum.BackColor = Color.FromArgb(0x95, 0xCF, 0xA0);
            int w = picSpectrum.Width;
            int h = picSpectrum.Height;
            var pen = new Pen(Color.LightGray);
            var bmp = new Bitmap(w, h);
            picSpectrum.Image = bmp;
            Graphics g = Graphics.FromImage(bmp);
            int wLines = w / 30;
            int hLines = h / 20;
            for (int i = 0, j = 0; i < w; i += wLines, j++)
                g.DrawLine(j == 15 ? pens[0] : pen, i, 0, i, h);
            for (int i = 0, j = 0; i < h; i += hLines, j++)
                g.DrawLine(j == 10 ? pens[0] : j == 8 || j == 12 ? pens[1] : j == 6 || j == 14 ? pens[2] : pen, 0, i, w, i);

            for (int i = 0; i < pens.Length; i++) pens[i].Dispose();
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

            Thread.Sleep(250); Application.DoEvents();

            byte[] buffer = SafeRead(quranBin);
            bool play = chkPlay.Checked;
            sensor.Start(quranWav, false, play, CreateSampleProvider);

            if (sensor.Bits == 8) NormalizeBuffer(buffer);
            int count = buffer.Length;

            var threadSSpectrum = new Thread(o =>
            {
                int index = 0;
                int chunk = chunkSize;
                while (count > 0)
                {
                    if (count > chunk) count -= chunk; else { chunk = count; count = 0; }
                    sensor.AddBytes(buffer.Skip(index).Take(chunk).ToArray(), chunk);
                    index += chunk;
                    if (play)
                        while (sensor.PlayerPosition < index)
                        {
                            Thread.Sleep(100); Application.DoEvents();
                        }
                }
            });

            threadSSpectrum.Start();
            while (threadSSpectrum.IsAlive)
            {
                Thread.Sleep(100); Application.DoEvents();
            }
            sensor.Stop();
        }
        private void InitSpectrum(int sampleRate=8000,int bits=16,AudioType aType=AudioType.Monaural)
        {
            //sensor = new AudioSensor(8000, 16, AudioType.Monaural, OnUpdate);
            spectrums.Clear();
            int readCount = sampleRate * 50 / 1000; // 50 ms
            int shift = sampleRate * 40 / 1000; // 40 ms
            pts = new PointF[sampleRate];
            readIdx = 0;
            recording = false;

            if (sensor!=null)
            {
                sensor.Dispose();
                sensor = null;
            }
            sensor = new AudioSensor(sampleRate, bits, aType, OnUpdate);
            for (int i = 0; i < sensor.Data.Channels; i++)
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
            catch (Exception) { };
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (sensor != null)
            {
//                audioFile = new MemoryStream();
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Save .wav File";
                dlg.DefaultExt = "wav";
                dlg.Filter = "wav File|*.wav";
                dlg.InitialDirectory = Application.StartupPath;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ReInitSpectrum();
                    recording = true;
                    sensor.Start(dlg.FileName,true,chkPlay.Checked,CreateSampleProvider);
                    LogMsg("Start Recording");
                }
            }
            UpdateGUI();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            recording = false;
            if (sensor != null)
            {
                sensor.Stop();
            }
            UpdateGUI();
            LogMsg("Stopped");
        }

        void canvas_Paint(object sender, PaintEventArgs e)
        {
            RenderType renderType = GetRenderType();
            
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
                {
                 //   PredrawSpectrums(spectrums, spectrumBmp);
                    // Bitmap bmp = (Bitmap)spectrumBmp.Clone();
                    DrawSpectrums(e.Graphics, spectrumBmp);
                   // bmp.Dispose();
                }
            }
        }

        private void waveRButton_CheckedChanged(object sender, EventArgs e)
        {
            canvas.Invalidate();
        }

        private void spectrumRButton_CheckedChanged(object sender, EventArgs e)
        {
            canvas.Invalidate();
        }

        // Data updated
        void OnUpdate(byte[] bytes, int count)
        {
            try
            {
                //           audioFile.Write(bytes,0,bytes.Length);
                if (spectrumRButton.Checked)
                {
                    // Find the spectrum and draw it on a bitmap
                    while (true)
                    {
                        bool succeed = SpectrumConverter.GetSpectrum(sensor.Data.Buffer, readIdx, readCount, spectrums,sensor.Bits);
                        if (!succeed)
                            break;
                        readIdx += shift;
                        
                        PredrawSpectrums(spectrums,spectrumBmpPrep);
                        spectrumBmp =(Bitmap) spectrumBmpPrep.Clone();
                    }
                }
                canvas.Invalidate();
            }
            catch (Exception ex) { LogMsg(ex,Thread.CurrentThread.ManagedThreadId); };
        }


        RenderType GetRenderType()
        {
            if (waveRButton.Checked)
                return RenderType.Wave;
            if (spectrumRButton.Checked)
                return RenderType.Spectrum;
            return RenderType.None;
        }

        void UpdateGUI()
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

        private void canvas_Click(object sender, EventArgs e)
        {

        }

        void DrawWaves(Graphics g, AudioSensorData data)
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

                float ratioy = -g.ClipBounds.Height / (2f * data.Channels * (data.bits == 16 ? short.MaxValue : byte.MaxValue));

                float dx = (float)g.ClipBounds.Width / pts.Length;
                float h = g.ClipBounds.Height / data.Channels;

                int i0 = Math.Max(0, data.Buffer[0].Count - pts.Length);

                for (int channel = 0; channel < data.Channels; channel++)
                {
                    float oy = h * channel + 0.5f * h + (data.bits == 8 ? gain * (h / 4) : gain - 1);
                    for (int i = i0; i < data.Buffer[channel].Count; i++)
                    {
                        pts[i - i0].X = dx * (i - i0);
                        pts[i - i0].Y = data.Buffer[channel][i] * gain * ratioy + oy;
                    }

                    g.DrawLines(pen, pts);
                }
            } catch (Exception ex) { LogMsg(ex); trkDB.Value = trkDB.Minimum; }
        }

        unsafe void PredrawSpectrums(List<List<double>> spectrums, Bitmap spectrumBmp)
        {
            const int dx = 10;
            try
            {
                if (currentThread != Thread.CurrentThread.ManagedThreadId)
                {
                    currentThread = Thread.CurrentThread.ManagedThreadId;
                    lblStatus.BeginInvoke((Action)delegate () { lblStatus.Text +=currentThread.ToString()+" "; });
                }

                if (spectrums == null || spectrums.Count <= 0)
                {
                     return;
                }
                if (spectrumBmp == null || spectrumBmp.Width <= 0 || spectrumBmp.Height <= 0)
                {
                     return;
                }

                using (Graphics g = Graphics.FromImage(spectrumBmp))
                    g.DrawImage(spectrumBmp, -dx, 0);

                BitmapData lck = spectrumBmp.LockBits(new Rectangle(Point.Empty, spectrumBmp.Size), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

                byte* data = (byte*)lck.Scan0;
                int stride = lck.Width * 3;
                stride = ((stride % 4) == 0 ? stride : (stride / 4 + 1) * 4);

                float minIdx = 0;
                float maxIdx = spectrums[0].Count - 1;

                float yto01 = 1f / spectrumBmp.Height * spectrums.Count;
                float invdy = spectrums[0].Count * spectrums.Count / spectrumBmp.Height;

                for (int y = 0; y < spectrumBmp.Height; y++)
                {
                    int channel = (int)(y * yto01);

                    if (spectrums.Count <= channel)
                        continue;

                    float oy = channel * spectrumBmp.Height / spectrums.Count;
                    float t = Math.Max(0, Math.Min(1, (y - oy) * yto01));
                    int i = (int)(t * maxIdx + (1 - t) * minIdx);
                    double val = spectrums[channel][i];
                    byte c = (byte)Math.Max(0, Math.Min(255, val * 255));

                    for (int x = spectrumBmp.Width - dx; x < spectrumBmp.Width; x++)
                    {
                        int offset = stride * y + 3 * x;
                        data[offset + 0] = 0;
                        data[offset + 1] = c;
                        data[offset + 2] = 0;
                    }
                }

                spectrumBmp.UnlockBits(lck);
            }
            catch (Exception ex) { LogMsg( ex,Thread.CurrentThread.ManagedThreadId); }
        }

        private void PlayWaveFile(string file)
        {
            lastPlayed = file;
            lblStatus.Text = "Threads : ";
            AppSettings.WriteValue("Settings", "LastPlayed", lastPlayed);
            PlayWaveFromFile(lastPlayed);
            LogMsg("File Loaded:" + lastPlayed);
        }
        private void btnPlay_Click(object sender, EventArgs e)
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
            catch (Exception ex) { LogMsg(ex); }
        }

        private void btnStop_KeyDown(object sender, KeyEventArgs e)
        {
            pressed = true;
        }

        private void btnStop_KeyUp(object sender, KeyEventArgs e)
        {
            pressed = false;
        }

        private void btnStop_KeyDown(object sender, MouseEventArgs e)
        {
            pressed = true;
        }
        private void ReInitSpectrum()
        {
            try
            {
                if (cmbChannels.SelectedIndex>=0 && !string.IsNullOrEmpty(cmbBits.Text) && !string.IsNullOrEmpty(cmbSampleRate.Text))
                    InitSpectrum(int.Parse(cmbSampleRate.Text), int.Parse(cmbBits.Text), (cmbChannels.SelectedIndex == 0 ? AudioType.Monaural : AudioType.Stereo));
            }
            catch (Exception ex) { LogMsg(ex); }
        }
        private void cmbSampleRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "SampleRate", cmbSampleRate.Text);
            ReInitSpectrum();
        }

        private void cmbBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "Bits", cmbBits.Text);
            ReInitSpectrum();
        }

        private void cmbChannels_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "Channels", cmbChannels.Text);
            ReInitSpectrum();
        }

        private void chkPlay_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "PlayWhileRecord", chkPlay.Checked?"YES":"NO");
        }

        private void tabSpectrum_Enter(object sender, EventArgs e)
        {
           splitContainer1.SplitterDistance=(int)(panel7.Width*1.25);
        }

        private void ResetSpectrum(bool resetSettings=true)
        {
            Graphics g = Graphics.FromImage(spectrumBmp);
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

        private void btnResetSpectrum_Click(object sender, EventArgs e)
        {
            ResetSpectrum();
        }

        private void trkDB_Scroll(object sender, EventArgs e)
        {

        }

        private void trkDB_ValueChanged(object sender, EventArgs e)
        {
            if (trkDB.Value <= 13)
                gain = 1;
            else
                gain = (float)(10 * Math.Log10(trkDB.Value/10f));
            lblDB.Text = (trkDB.Value/10f).ToString("0.0") + "/" + (gain*100).ToString("0");
        }

        private void canvas_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0 && Path.GetExtension(files[0]).ToUpper().Equals(".WAV"))
            {
                PlayWaveFile(files[0]);
            }
        }

        private void canvas_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
        void OnPreVolumeMeter(object sender, StreamVolumeEventArgs e)
        {
            waveformPainter1.AddMax(e.MaxSampleValues[0]);
            waveformPainter2.AddMax(e.MaxSampleValues.Length > 1?e.MaxSampleValues[1]:0f);

        }

        void OnPostVolumeMeter(object sender, StreamVolumeEventArgs e)
        {
            volumeMeter1.Amplitude = e.MaxSampleValues[0];
            volumeMeter2.Amplitude = e.MaxSampleValues.Length > 1?e.MaxSampleValues[1]:0f;
        }

        private ISampleProvider CreateSampleProvider(IWaveProvider waveProvider, int bits = 16)
        {
            var sampleChannel = new SampleChannel(waveProvider, bits == 16);
            sampleChannel.PreVolumeMeter += OnPreVolumeMeter;
            setVolumeDelegate = (vol) => sampleChannel.Volume = vol;
            var postVolumeMeter = new MeteringSampleProvider(sampleChannel);
            postVolumeMeter.StreamVolume += OnPostVolumeMeter;

            return postVolumeMeter;
        }
        private void volumeSlider1_VolumeChanged(object sender, EventArgs e)
        {
            if (setVolumeDelegate != null)
            {
                setVolumeDelegate(volumeSlider1.Volume);
            }
        }

        private void btnStop_KeyUp(object sender, MouseEventArgs e)
        {
            pressed = false;
        }

        void DrawSpectrums(Graphics g, Bitmap bmp)
        {
            try
            {
                g.DrawImage(bmp, g.ClipBounds);
            }
            catch (Exception ex)
            {
                LogMsg(ex,Thread.CurrentThread.ManagedThreadId);
            }

        }


        public void PlayWaveFromFile(string file)
        {
            try
            {
                WaveFileReader waveFile = new WaveFileReader(file);
                FindInCombo(cmbSampleRate, waveFile.WaveFormat.SampleRate.ToString(),false,true);
                FindInCombo(cmbBits, waveFile.WaveFormat.BitsPerSample.ToString(), false, true);
                FindInCombo(cmbChannels, waveFile.WaveFormat.Channels==1?"Mono":"Stereo", false, true);
                ReInitSpectrum();
                waveFile.Dispose();
                waveFile.Close();
                waveFile = null;
           //     sensor.Stop();
           //     sensor.Dispose();
                
                int sampleRate = int.Parse(cmbSampleRate.Text);
                int bits = int.Parse(cmbBits.Text);
                int channnels = cmbChannels.SelectedIndex + 1;

                new Thread(o =>
                {
         //           InitSpectrum(sampleRate,bits,channnels==1?AudioType.Monaural:AudioType.Stereo);
                    lockThread = Thread.CurrentThread.ManagedThreadId;
                    WaveFileReader waveFile = new WaveFileReader(file);
                    using (WaveStream blockAlignedStream = new BlockAlignReductionStream(WaveFormatConversionStream.CreatePcmStream(waveFile)))
                    {
                        using (WaveOut player = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                        {
                            IWaveProvider waveProvider = new MonitoredWaveProvider(blockAlignedStream, sensor.AddBytes);
                            player.Init(CreateSampleProvider(waveProvider, bits));
                            btnPlay.BeginInvoke((Action)delegate () { btnPlay.Enabled = false; });
                            btnRePlay.BeginInvoke(new MethodInvoker(delegate { btnRePlay.Enabled = false; }));
                            player.Play();
                            while (player.PlaybackState == PlaybackState.Playing)
                            {
                                System.Threading.Thread.Sleep(100);
                                if (pressed) player.Stop();
                            }
                            try { 
                                btnPlay.BeginInvoke((Action)delegate () { btnPlay.Enabled = true; });
                                btnRePlay.BeginInvoke(new MethodInvoker(delegate { btnRePlay.Enabled = true; }));
                            } catch (Exception) { };
                        }
                    }
                    lockThread = -1;
                }).Start();
            } catch (Exception ex) { LogMsg(ex, Thread.CurrentThread.ManagedThreadId); }
        }

        #endregion

    #region Color
        private void btnCPSM_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(lblColorPointSize.Text);
            if (n > 1) n--;
            lblColorPointSize.Text = n.ToString();
            DrawColors();
        }

        private void cmbColorSizeMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSizeMod(picColor1, picColor2);
            DrawColors();
            AppSettings.WriteValue("Settings", "ColorSizeMode", cmbColorSizeMode.SelectedIndex.ToString());
        }
        private void chkColorScaled_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "ColorScaled", chkColorScaled.Checked?"YES":"NO");
        }

        private void btnCPSP_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(lblColorPointSize.Text);
            if (n < 100) n++;
            lblColorPointSize.Text = n.ToString();
            DrawColors();
        }

        private void txtInfo_TextChanged(object sender, EventArgs e)
        {

        }
        private void lblColorPointSize_TextChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "ColorScale", lblColorPointSize.Text);
        }

        int ColorValue(int n,int min,int max)
        {
            if (chkColorScaled.Checked && max>min)
            {
                return (n - min) * 255 / (max - min)  ;
            }
            else return n;
        }
        private void DrawColors()
        {
            int pointSize;
            if (!int.TryParse(lblColorPointSize.Text, out pointSize)) pointSize = 1;
            byte[] byteArray = SafeRead(quranBin);

            int min = byteArray.Min();
            int max = byteArray.Max();

            var colorArray = new Color[byteArray.Length / 3];
            for (var i = 0; i < byteArray.Length - 3; i += 3)
            {
                var color = Color.FromArgb(ColorValue(byteArray[i + 0],min,max), ColorValue(byteArray[i + 1],min,max), ColorValue(byteArray[i + 2],min,max));
                colorArray[i / 3] = color;
            }

            int n = (int)Math.Sqrt(colorArray.Length);
            while ((n * n) < colorArray.Length) n++;

            var bmp = new Bitmap(n * pointSize, n * pointSize, PixelFormat.Format24bppRgb);
            var bmp2 = new Bitmap(colorArray.Length, picColor3.Height, PixelFormat.Format24bppRgb);
            var g1 = Graphics.FromImage(bmp);
            var g2 = Graphics.FromImage(bmp2);

            for (int i = 0; i < colorArray.Length; i++)
            {
                //bmp.SetPixel(i % n, i / n, colorArray[i]);

                DrawPoint(g1, pointSize, 1, (i % n)+1 , (i / n)+1 , colorArray[i]);

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
}
