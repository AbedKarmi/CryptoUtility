#define DEBUG

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Runtime.InteropServices;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Drawing.Text;
using System.Data.OleDb;
using Be.Windows.Forms;
using System.Windows.Media.Imaging;
//using System.Windows.Input;


namespace CryptoUtility
{    public partial class frmMain : Form
    {
        string[][] fonts = { new [] { "KFGQPC Uthman Taha Naskh", "Uthmani.otf" }, new[] { "DQ7 Quran Koufi A", "DQ7QuranKoufiA.ttf" } };

        InputLanguage original ;
        IQuran quran;
        private ISettings AppSettings;
        Color backColor = Color.Black;
        struct Area { public int x; public int y; public int size; }
        List<Area> areas = new();

        struct Pics {public int x1; public int y1; public int x2; public int y2; public int size1; public int size2; }
        Pics picSpace;

        GPUClass gpuClass = new();
        RSAClass rsaClass = new();
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
        public frmMain()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string resourceName = new AssemblyName(args.Name).Name + ".dll";
                string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };
            InitializeComponent();
            loadEncodings();
        }


        // event handler
        public void ProcessCompleted(object sender, gpuEventArgs e)
        {
            logMsg("Operation Completed with : "+e.accelerator);
        }
        void logMsg(string msg)
        {
            string[] msgs = msg.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var str in msgs)
            {
                lstLog.Items.Add(DateTime.Now + ": " + str);
                lstLog.SelectedIndex = lstLog.Items.Count - 1;
            }
        }

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
        public BitmapImage ConvertToImage(byte[] buffer)
        {
            MemoryStream stream = new MemoryStream(buffer);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
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

                tabControl1.SelectedTab = null;
                tabControl1.SelectedTab = tabRSA;

                CheckQuranFont();

                txtQuranText.Font = new Font(fonts[0][0], 12);
               // lbSoras.Font = new Font(QuranFont, 10);
                
                lblFontSize.Text = AppSettings.ReadValue("Settings", "FontSize", "12");
                txtQuranText.Font = new Font(txtQuranText.Font.FontFamily, Int32.Parse(lblFontSize.Text));

                lblPointSize.Text = AppSettings.ReadValue("Settings", "Scale", "1");

                cmbBytesPerLine.Text = AppSettings.ReadValue("Settings", "BytesPerLine", "16");
                cmbSizeMode.SelectedIndex = Int32.Parse(AppSettings.ReadValue("Settings", "SizeMode", "0"));
                chkALLEncodings.Checked = (AppSettings.ReadValue("Settings", "AllEncodings", "Yes").ToUpper() == "YES");
                chkSendToBuffer.Checked = (AppSettings.ReadValue("Settings", "SendToBuffer", "Yes").ToUpper() == "YES");
                chkSendToHexViewer.Checked = (AppSettings.ReadValue("Settings", "SendToHexViewer", "Yes").ToUpper() == "YES");
                chkFixPadding.Checked = (AppSettings.ReadValue("Settings", "Padding", "Yes").ToUpper() == "YES");
                chkFlipX.Checked = (AppSettings.ReadValue("Settings", "FlipX", "Yes").ToUpper() == "YES");
                chkFlipY.Checked = (AppSettings.ReadValue("Settings", "FlipY", "No").ToUpper() == "YES");
                chkINV.Checked = (AppSettings.ReadValue("Settings", "Inversed", "No").ToUpper() == "YES");

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


            } catch (Exception ex) { logMsg("Error:" + ex.Message); }
        }

        private void btnExportPublic_Click(object sender, EventArgs e)
        {
        //    using RSACryptoServiceProvider csp = new();
            RSAParameters rsaParams = new();
            try
            {
                int E = Int32.Parse(txtE.Text);
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
            } catch (Exception ex) { logMsg( "Error : "+ ex.Message); }
        }

        private void btnExportPrivate_Click(object sender, EventArgs e)
        {
            RSAParameters rsaParams = new();
            try
            {
                int E = Int32.Parse(txtE.Text);
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
            catch (Exception ex) { logMsg("Error : " + ex.Message); }
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

                        txtOutput.Text = Convert.ToBase64String(rsaClass.SignData(hash, cmbHash.Text, Int32.Parse(cmbKeyLength.Text)));

                        logMsg("Signed");
                        break;
                    case 1:
                        hash = MyClass.GetHash(GetData(), cmbHash.Text);
                        txtHash.Text = MyClass.BinaryToHexString(hash);

                        dsaClass.SetKey(PEMClass.ImportDSAPublicKey(txtPublicKey.Text), false);
                        dsaClass.SetKey(PEMClass.ImportDSAPrivateKey(txtPrivateKey.Text), true);

                        txtOutput.Text = Convert.ToBase64String(dsaClass.SignData(hash, cmbHash.Text));

                        logMsg("Signed");
                        break;
                    default:
                        logMsg("Algorithm not implemented");
                        break;
                }
            }
            catch (Exception ex) { logMsg("Error :" + ex.Message); }
        }

        private void cmbData_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblData.Text = "Data (" + dataType[cmbData.SelectedIndex] + ")";
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

                        logMsg("Encrypted");
                        break;
                    case 1:
                        logMsg("Algorithm not supported");
                        break;
                    default:
                        logMsg("Algorithm not implemented");
                        break;
                }
            }
            catch (Exception ex) { logMsg("Error :" + ex.Message); }

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

                        logMsg("Decrypted");
                        break;
                    case 1:
                        logMsg("Algorithm not supported");
                        break;
                    default:
                        logMsg("Algorithm not implemented");
                        break;
                }
            }
            catch (Exception ex) { logMsg("Error :" + ex.Message); }
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
                        rsaClass.SetKey(PEMClass.ImportPrivateKey(txtPrivateKey.Text), true);

                        logMsg(rsaClass.VerifySignature(hash, Convert.FromBase64String(txtOutput.Text), cmbHash.Text, Int32.Parse(cmbKeyLength.Text)) ? "Verification Successful" : "Verification Failed");
                        break;
                    case 1:
                        hash = MyClass.GetHash(GetData(), cmbHash.Text);
                        txtHash.Text = MyClass.BinaryToHexString(hash);

                        dsaClass.SetKey(PEMClass.ImportDSAPublicKey(txtPublicKey.Text), false);
                        dsaClass.SetKey(PEMClass.ImportDSAPrivateKey(txtPrivateKey.Text), true);

                        logMsg(dsaClass.VerifySignature(hash, Convert.FromBase64String(txtOutput.Text), cmbHash.Text) ? "Verification Successful" : "Verification Failed");
                        break;
                    default:
                        logMsg("Algorithm not implemented");
                        break;
                }
            }
            catch (Exception ex) { logMsg("Error :" + ex.Message); }
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
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls(this);
        }

        private void btnHash_Click(object sender, EventArgs e)
        {
            byte[] hash = MyClass.GetHash(GetData(), cmbHash.Text);
            txtHash.Text = MyClass.BinaryToHexString(hash);
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

                txtE.Text = E.ToString();
                txtN.Text = MyClass.BinaryToHexString(publicKey.Modulus);

                tabControl1.SelectedIndex = 0;

                cmbRSAKeyLen.Text = (publicKey.Modulus.Length * 8).ToString();
            }
            catch (Exception ex) { logMsg("Error :" + ex.Message); }
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

                txtE.Text = E.ToString();
                txtN.Text = MyClass.BinaryToHexString(privateKey.Modulus);

                txtP.Text = MyClass.BinaryToHexString(privateKey.P);
                txtQ.Text = MyClass.BinaryToHexString(privateKey.Q);
                txtD.Text = MyClass.BinaryToHexString(privateKey.D);
                txtDP.Text = MyClass.BinaryToHexString(privateKey.DP);
                txtDQ.Text = MyClass.BinaryToHexString(privateKey.DQ);
                txtInverseQ.Text = MyClass.BinaryToHexString(privateKey.InverseQ);

                tabControl1.SelectedIndex = 0;

                cmbRSAKeyLen.Text = (privateKey.Modulus.Length * 8).ToString();
            }
            catch (Exception ex) { logMsg("Error :" + ex.Message); }
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
                    logMsg("File Loaded (" + dlg.FileName + ") Size is " +fileBuffer.Length.ToString());
                }
                rbFileBuffer.Checked = true;
            }
            catch (Exception ex) { logMsg("Error :" + ex.Message); }

            btnLoadFile.Enabled = true;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveKeys_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(Application.StartupPath + "\\PublicKey."+cmbCryptoAlgorithm.Text+".pem", txtPublicKey.Text);
                File.WriteAllText(Application.StartupPath + "\\PrivateKey."+cmbCryptoAlgorithm.Text +".pem", txtPrivateKey.Text);
                logMsg("Keys Saved");
            }
            catch (Exception ex) { logMsg("Error :" + ex.Message); }
        }

        private void btnLoadKeys_Click(object sender, EventArgs e)
        {
            try
            {
                txtPublicKey.Text=File.ReadAllText(Application.StartupPath + "\\PublicKey." + cmbCryptoAlgorithm.Text + ".pem");
                txtPrivateKey.Text=File.ReadAllText(Application.StartupPath + "\\PrivateKey." + cmbCryptoAlgorithm.Text + ".pem");
                logMsg("Keys Loaded");
            }
            catch (Exception ex) { logMsg("Error :" + ex.Message); }
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
        private void btnLoadQuran_Click(object sender, EventArgs e)
        {
            string[] allfiles = Directory.GetFiles("F:\\Software\\Drive3.Private\\Islam\\QuranSSL\\Quran", "*.txt", SearchOption.TopDirectoryOnly);
            foreach (string file in allfiles)
            {
                
                AppendAllBytes(Path.GetDirectoryName(file)+"\\ALL\\Quran.txt", File.ReadAllBytes(file),true);
               
                /*
                string fName = Path.GetFileName(file);
                string ext = Path.GetExtension(file);
                string ns = fName.Substring(0, fName.Length - ext.Length);
                ns = String.Format("{0:D3}", Int32.Parse(ns));
                File.Move(file, Path.GetDirectoryName(file) + "\\" + ns + ext);
                Debug.WriteLine(Path.GetDirectoryName(file) + "\\" + ns + ext);
                */

            }
        }

        private void txtPublicKey_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPublicKey_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                txtPublicKey.Text = File.ReadAllText(files[0]);
            }
        }

        private void txtPublicKey_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void txtPrivateKey_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                txtPrivateKey.Text = File.ReadAllText(files[0]);
            }
        }

        private void txtPrivateKey_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void btnUsePublicKey_Click(object sender, EventArgs e)
        {
            txtPublicKey.Text = txtPEMPublicKey.Text;
        }

        private void btnUsePrivateKey_Click(object sender, EventArgs e)
        {
            txtPrivateKey.Text = txtPEMPrivateKey.Text;
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
            } catch (Exception ex) { logMsg("Error:" + ex.Message); }
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
                logMsg("Error:" + ex.Message);
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
            catch (Exception ex) { logMsg("Error:" + ex.Message); }
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
            catch (Exception ex) { logMsg("Error :" + ex.Message); }
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
            catch (Exception ex) { logMsg("Error :" + ex.Message); }
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
            catch (Exception ex) { logMsg("Error:" + ex.Message); }
        }

        private void btnPWM_Click(object sender, EventArgs e)
        {
            try
            {
                GetNumbers();

                R = BigInteger.ModPow(P, Q, N);

                ShowResult();
            }
            catch (Exception ex) { logMsg("Error :" + ex.Message); }
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
            catch (Exception ex) { logMsg("Error:" + ex.Message); }
        }

        private void btnLCM_Click(object sender, EventArgs e)
        {
            try
            {
                GetNumbers();

                R = BigInteger.Divide(BigInteger.Abs(BigInteger.Multiply(P, Q)), BigInteger.GreatestCommonDivisor(P, Q));

                ShowResult();
            }
            catch (Exception ex) { logMsg("Error:" + ex.Message); }
        }

        private void txtPrimeP_DragDrop(object sender, DragEventArgs e)
        {
            txtPrimeP.Text = (string)e.Data.GetData(DataFormats.Text);
        }

        private void txtPrimeP_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void txtPrimeQ_DragDrop(object sender, DragEventArgs e)
        {
            txtPrimeQ.Text = (string)e.Data.GetData(DataFormats.Text);
        }

        private void txtPrimeQ_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void txtModulN_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void txtModulN_DragDrop(object sender, DragEventArgs e)
        {
            txtModulN.Text = (string)e.Data.GetData(DataFormats.Text);
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
            }  catch (Exception ex) { logMsg("Error :" + ex.Message); }
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
        private void btnGenDSAKeys_Click(object sender, EventArgs e)
        {
            try 
            {
                dsaClass.CreateKey(Int32.Parse(cmbDSAKeyLen.Text));
                dsaPrivateKey = dsaClass.GetKey(true);
                dsaPublicKey = dsaClass.GetKey(false);

                txtDSAPEMPublic.Text = PEMClass.ExportDSAPublicKey(dsaPublicKey);
                txtDSAPEMPrivate.Text = PEMClass.ExportDSAPrivateKey(dsaPrivateKey);

                logMsg("DSA Keys Generated");
            }
            catch (Exception ex) { logMsg("Error :" + ex.Message); }
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
            catch (Exception ex) { logMsg("Error :" + ex.Message); }
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
            catch (Exception ex) { logMsg("Error :" + ex.Message); }
        }

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
            catch (Exception ex) { logMsg("Error :" + ex.Message); }
        }

        private void btnGenKeys_Click(object sender, EventArgs e)
        {
            try
            {
                rsaClass.CreateKey(Int32.Parse(cmbRSAKeyLen.Text));
                privateKey = rsaClass.GetKey(true);
                publicKey = rsaClass.GetKey(false);

                txtPEMPublicKey.Text = PEMClass.ExportPublicKey(publicKey, true);
                txtPEMPrivateKey.Text = PEMClass.ExportPrivateKey(privateKey, true);

                logMsg("Keys Generated");
            }
            catch (Exception ex) { logMsg("Error :" + ex.Message); }
        }
        
        //
        // Encodings ************************************************************************************************************************************
        //

        #region "Private variables"
        //filenames to convert
        private string[] _sourceFiles = null;
        //ouput directory
        private string destDir = null;
        #endregion

        #region "Constructors"
        /// <summary>
        /// Main form constructor
        /// </summary>
  
        #endregion
        private void SendToHexViewer(string sFile)
        {
 
            hexBox.ByteProvider = new FileByteProvider(sFile);
            lblHexFile.Text = sFile;
            cmbHEncoding.SelectedIndex = cmbDestEnc.SelectedIndex;
            hexBox.Refresh();
            CalcHHash();
        }

        #region "Event Handlers"

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
            logMsg (_sourceFiles.Length + " file(s) selected for conversion" + System.Environment.NewLine);
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
        }
        #endregion



        #region "Run the conversion"
        /// <summary>
        /// Handler for the run button click event
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void btnRun_Click(object sender, EventArgs e)
        {
            string sourceCP, destCP;
            string s;
            string res;
            int iSuccess = 0;
            string sFile;
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
                        logMsg("Converting " + (i + 1).ToString() + " of " + _sourceFiles.Length + System.Environment.NewLine);


                        //do the conversion
                        if ((sFile = Converter.ConvertFile( _sourceFiles[i], destDir,
                                                            sourceCP, destCP, specialType, chkMeta.Checked, chkDiscardChars.Checked, 
                                                            chkDiacritics.Checked, chkzStrings.Checked)) != "")
                        {
                            //success
                            res = char.ConvertFromUtf32(9745);

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
                            if (chkSendToHexViewer.Checked) 
                            {
                                SendToHexViewer(sFile);
                            }
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
                    logMsg(iSuccess.ToString() + " file(s) converted successfully and output to:" +
                                                      System.Environment.NewLine + destDir + System.Environment.NewLine);
                }
                else
                if (rtxtData.Text.Length > 0 && rbText.Checked)
                {
                    logMsg("Converting From TextBox");
                    sFile = Application.StartupPath + "\\enc.txt";
                    byte[] sOut = Converter.ConvertText(rtxtData.Text, sourceCP, destCP, specialType,
                                                        chkMeta.Checked, chkDiscardChars.Checked, chkDiacritics.Checked, chkzStrings.Checked);
                    File.WriteAllBytes(sFile, sOut);
                    logMsg("Written to " + sFile);
                    if (chkSendToBuffer.Checked) { rbFileBuffer.Checked = true; fileBuffer = sOut; }
                    if (chkSendToHexViewer.Checked)
                    {
                        SendToHexViewer(sFile);
                    }
                    txtInfo.Text = chkHexText.Checked ? MyClass.BinaryToHexString(sOut) : Converter.ReadAllText(sFile, destCP);
                    lblStatus.Text = sOut.Length.ToString();
                }
                else logMsg("Nothing to encode");
            }
            catch (Exception ex)
            {
                //just show the error
                logMsg(ex.Message);
            }
            finally
            {
                //03/05/2007 tidy up
                btnRun.Enabled = btnStat;
                this.Cursor = System.Windows.Forms.Cursors.Default;
                if (chkHexText.Checked) hex = txtInfo.Text;
            }
        }
        #endregion

        #region "Private Methods"
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
            rtxtData.RightToLeft = chkRTL.Checked ? RightToLeft.Yes : RightToLeft.No;
            txtInfo.RightToLeft = chkRTL.Checked && !chkHexText.Checked ? RightToLeft.Yes : RightToLeft.No;
        }

 
        private void cmbAccelerator_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbAccelerator.SelectedIndex > 0)
                    txtInfo.Text = gpuClass.AcceleratorInfo(cmbAccelerator.SelectedIndex - 1);
                else
                    txtInfo.Text = " Direct Calculation";
            }
            catch (Exception ex) { logMsg("Error:" + ex.Message); }
        }

        private void SelectEncoding(string contains)
        {
            int i = 0; bool found = false;
            while (i < cmbDestEnc.Items.Count && !found)
            {
                if (!cmbDestEnc.Items[i].ToString().Contains(contains)) i++; else found = true;
            }
           if (found) cmbDestEnc.SelectedIndex = i;

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
            /*
            if (lblCurCharset.Text.Contains("Hijaei"))
            { 
                if (rbFirstOriginal.Checked || rbFirstOriginalDots.Checked) SelectEncoding("[Hijaei]");
                if (rbNoDiacritics.Checked) SelectEncoding("[Hijaei-Hamza]");
            } 
            else if (lblCurCharset.Text.Contains("Abjadi"))
            {
                if (rbFirstOriginal.Checked || rbFirstOriginalDots.Checked) SelectEncoding("[Abjadi]");
                if (rbNoDiacritics.Checked) SelectEncoding("[Abjadi-Hamza]");
            }
           else 
            */
            SelectEncoding("["+s+"]");

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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
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
                    txtInfo.Text = "";
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
                    lbSoras.Items.Clear();
                    lbSoras.Items.AddRange(quran.GetSoraNames());
                    lbSoras.SelectedIndex = 0;
                    txtInfo.Text = @"Crypto Utility for Quran Fidelity

Quran is the greatest book ever found on earth, it was revealed to the last prophet Mohammad peace upon him. 

Nowadays, almost all sensitive data being stored or transferred via any media are protected against loss and change. All aspects of life are affected, and rapidly respond to this urgent necessity.

Regular customers and users become aware of this notion, so every piece of data must be protected and highly trusted, otherwise no more confidence is granted.

Quran, is the utmost book that requires such fidelity, we believe that it must have a proof that is aligned with nowadays modern science, since God is the most knowledgeable and the most expert.

Findings:
The Key:
The first Sora of Quran is named ALFATEHA, which means 'the Opener'. We beleive it contains the KEY for verifying the content of the quran using modern available security options.

Text to byte conversion:
We believe that either Arabic Abjadi or Hijaie is being used. Hamza, is to be tested, since it was not used in the revelead text explicitly.

The Signature:
We are looking for this!, which must be in the Quran text, possibly Kursi verse, first verse of the Sora, last Sora in Quran, last revealed verse, ... .
";
                } else if (tabControl1.SelectedTab==tabCharset)
                {
                    LoadCharset(Application.StartupPath+"\\"+ lblCurCharset.Text);
                    txtInfo.Text = @"Arabic Custom Charset

Define the custom byte order of Arabic alphbet, this order will be used in the encode/decode process.

This can be selected in the combox as 'Arabic CUSTOM order Charset'.

Auto: set the first charset, then will auto complete to the end

Blue   : Siamese Lettters
Green  : Arabic Letters
Red    : Diacritics";
                }
                else if (tabControl1.SelectedTab == tabHexViewer)
                {
                    txtInfo.Text = "Simple Hex Byte Editor/Viewer";
                }
                else if (tabControl1.SelectedTab == tabImage)
                {
                    txtInfo.Text = "X-Ray\r\n\r\nDisplay Binary Image of Text Endoding";
                }
            } catch (Exception ex) { logMsg("Eror :" + ex.Message);}
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                quran.CloseQuran();
            }
            catch (Exception) { };
        }
  
        private void btnIsPrime_Click(object sender, EventArgs e)
        {
            GetNumbers();
            txtResultR.Text= "P is " + (BigIntegerHelper.IsProbablePrime(P,100) ? "Prime" : "NOT Prime") + Environment.NewLine +
                              "Q is " + (BigIntegerHelper.IsProbablePrime(Q,100) ? "Prime" : "NOT Prime");
        }

        private string Hex(BigInteger n)
        {
            string s = n.ToString("X");
            while (s.Length > 1 && s.Substring(0, 1) == "0") s = s.Substring(1);
            return s;
        }
        private void GetNumbers()
        {
            if (entry > 0) return;
            entry++;
            try
            {
                txtPrimeP.Text = txtPrimeP.Text.Replace(":", "").Replace(" ","").Trim();
                txtPrimeQ.Text = txtPrimeQ.Text.Replace(":", "").Replace(" ", "").Trim();
                txtModulN.Text = txtModulN.Text.Replace(":", "").Replace(" ", "").Trim();
                txtResultR.Text = txtResultR.Text.Replace(":", "").Replace(" ", "").Trim();

                switch (CurNum)
                {
                    case 0: 
                        P = BigIntegerHelper.NewBigInteger2(txtPrimeP.Text);
                        Q = BigIntegerHelper.NewBigInteger2(txtPrimeQ.Text);
                        N = BigIntegerHelper.NewBigInteger2(txtModulN.Text);
                        R = BigIntegerHelper.NewBigInteger2(txtResultR.Text);
                        break;
                    case 1:
                        if (!BigInteger.TryParse(txtPrimeP.Text, out P)) P = 0;
                        if (!BigInteger.TryParse(txtPrimeQ.Text, out Q)) Q = 0;
                        if (!BigInteger.TryParse(txtModulN.Text, out N)) N = 0;
                        if (!BigInteger.TryParse(txtResultR.Text, out R)) R = 0;
                        break;
                    case 2:
                        P = BigIntegerHelper.GetBig(txtPrimeP.Text,chkPositives.Checked);
                        Q = BigIntegerHelper.GetBig(txtPrimeQ.Text, chkPositives.Checked);
                        N = BigIntegerHelper.GetBig(txtModulN.Text, chkPositives.Checked);
                        R = BigIntegerHelper.GetBig(txtResultR.Text, chkPositives.Checked);
                        break;
                    case 3:
                        P = BigIntegerHelper.GetBig(Convert.FromBase64String(txtPrimeP.Text), chkPositives.Checked);
                        Q = BigIntegerHelper.GetBig(Convert.FromBase64String(txtPrimeQ.Text), chkPositives.Checked);
                        N = BigIntegerHelper.GetBig(Convert.FromBase64String(txtModulN.Text), chkPositives.Checked);
                        R = BigIntegerHelper.GetBig(Convert.FromBase64String(txtResultR.Text), chkPositives.Checked);
                        break;
                }
            } 
            catch (Exception ex) { logMsg(ex.Message); }   
            entry--;
        }
        private void ShowNumbers()
        {
            entry++;
            switch (GetNumIndex())
            {
                case 0:
                    txtPrimeP.Text = BigIntegerHelper.ToBinaryString(P); 
                    txtPrimeQ.Text = BigIntegerHelper.ToBinaryString(Q);
                    txtModulN.Text = BigIntegerHelper.ToBinaryString(N);
                    break;
                case 1:
                    txtPrimeP.Text = P.ToString();
                    txtPrimeQ.Text = Q.ToString();
                    txtModulN.Text = N.ToString();
                    break;
                case 2:
                    txtPrimeP.Text = Hex(P);
                    txtPrimeQ.Text = Hex(Q);
                    txtModulN.Text = Hex(N);
                    break;
                case 3:
                    txtPrimeP.Text = Convert.ToBase64String(P.ToByteArray());
                    txtPrimeQ.Text = Convert.ToBase64String(Q.ToByteArray());
                    txtModulN.Text = Convert.ToBase64String(N.ToByteArray());
                    break;
            }
            if (P == 0) txtPrimeP.Text = "";
            if (Q == 0) txtPrimeQ.Text = "";
            if (N == 0) txtModulN.Text = "";
            entry--;
        }
        private void ShowResult()
        {
            switch (CurNum)
            {
                case 0:
                    txtResultR.Text = BigIntegerHelper.ToBinaryString(R);
                    break;
                case 1:
                    txtResultR.Text = R.ToString();
                    break;
                case 2:
                    txtResultR.Text = Hex(R);
                    break;
                case 3:
                    txtResultR.Text = Convert.ToBase64String(R.ToByteArray());
                    break;
            }
            if (R== 0) txtResultR.Text = "";
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

        private void btnFactorize_Click(object sender, EventArgs e)
        {

        }

        private void btnSqrt_Click(object sender, EventArgs e)
        {
            GetNumbers();
            R = P.Sqrt();
            ShowResult();
        }

        private void chkHexText_CheckedChanged(object sender, EventArgs e)
        {
            txtInfo.RightToLeft = chkRTL.Checked && !chkHexText.Checked ? RightToLeft.Yes : RightToLeft.No;
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

        private void btnGenPrime_Click(object sender, EventArgs e)
        {
            //P = BigIntegerHelper.GenPrime(int.Parse(cmbKeyLen.Text));
            RSACryptoServiceProvider csp = new(int.Parse(cmbKeyLen.Text));
            P = BigIntegerHelper.GetBig( csp.ExportParameters(true).P);
            Q = BigIntegerHelper.GetBig(csp.ExportParameters(true).Q);
            
            ShowNumbers();
            csp.Dispose();
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

        private void btnBezout_Click(object sender, EventArgs e)
        {
            GetNumbers();
            BigInteger[] bzt= BigIntegerHelper.gcdWithBezout(P, Q);
            txtResultR.Text = "P . " + bzt[1] + " + Q ." + bzt[2] + " = " + bzt[0];
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

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
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            original = InputLanguage.CurrentInputLanguage;
            InputLanguage lang = GetArabicLanguage();
            if (lang == null)
            {
                logMsg("Arabic Language Keyboard not installed.");
            }

            InputLanguage.CurrentInputLanguage = lang;
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            // in the leave event handler:
            InputLanguage.CurrentInputLanguage = original;
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
                   logMsg(ex.Message);
                }
            }
        }

        private void dgvQuran_Click(object sender, EventArgs e)
        {
            txtQuranText.Text = dgvQuran.Rows[dgvQuran.CurrentRow.Index].Cells[quran.CurQuranTextIndex()].Value.ToString();
        }
        private void txtPrimeP_TextChanged(object sender, EventArgs e)
        {
            GetNumbers();
            int n = P.GetActualBitwidth();
            lblStatus.Text = "P = " + txtPrimeP.Text.Length +" - "+ (n).ToString() + " / " + P.GetBitwidth();
        }

        private void txtPrimeQ_TextChanged(object sender, EventArgs e)
        {
            GetNumbers();
            int n = Q.GetActualBitwidth();
            lblStatus.Text = "Q = " + txtPrimeQ.Text.Length + " - " + (n).ToString() + " / " + Q.GetBitwidth();
        }

        private void txtPKeyDBLClick(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
                txtPrimeP.Text = tb.Text;
            tabControl1.SelectedTab = tabCalculator;
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

        private long startTime = 0;
        private long TimeElapsed
        {
            get { return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - startTime; }
            set { startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond + value; }
        }
        private void LoadCharset(string fileName)
        {
            string ts = "";
            TimeElapsed=0;
            ClearChars(); ts += TimeElapsed + ",";
            byte[] c = MyClass.HexStringToBinary(File.ReadAllText(fileName)); ts += TimeElapsed + ",";
            AppSettings.WriteValue("Settings", "CharsetProfile", Path.GetFileName(fileName)); ts += TimeElapsed + ",";
            for (int i=0;i<44;i++)
            {
                  listTxtCS[i].Text = c[i].ToString();
            }
            ts += TimeElapsed + ",";
            ReOrderChars(); ts += TimeElapsed + ",";
            lblCurCharset.Text = Path.GetFileName(fileName);
            logMsg(ts);
            logMsg("Custom Charset Loaded");
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
            catch (Exception ex) { logMsg(ex.Message); }
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
                logMsg("Fill the first Charset Value first");
                return;
            }

            for (int i=1; i < 44; i++)
            {
                  listTxtCS[i].Text = ValidChar(ValueOf(txtCS1.Text) + i).ToString();
            }
        }

        private void btnSaveCharset_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Save Charset File";
                dlg.DefaultExt = "Charset";
                dlg.Filter = "Charset File|*.Charset";
                dlg.InitialDirectory = Application.StartupPath;
                dlg.FileName = lblCurCharset.Text;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    byte[] c = new byte[44];
                    for  (int i=0;i<44;i++)
                    {
                            c[i] = ValidChar(Int32.Parse(listTxtCS[i].Text));
                    }
                    File.WriteAllText(dlg.FileName, MyClass.BinaryToHexString(c));
                    lblCurCharset.Text = Path.GetFileName(dlg.FileName);
                    AppSettings.WriteValue("Settings", "CharsetProfile", lblCurCharset.Text);
                    logMsg("Custom Charset Saved");
                }
                string s1 = cmbSourceEnc.Text;
                string s2 = cmbDestEnc.Text;
                loadEncodings();
                cmbSourceEnc.Text = s1;
                cmbDestEnc.Text = s2;
                validateForRun();
            } catch (Exception ex) { logMsg(ex.Message); }
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

        private void chkALLEncodings_CheckedChanged(object sender, EventArgs e)
        {
            loadEncodings();
            validateForRun();
            AppSettings.WriteValue("Settings", "AllEncodings", chkALLEncodings.Checked?"Yes":"No");
        }

        private void lblFontSize_Click(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "FontSize", lblFontSize.Text.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rb10.Checked = true;
            Process.Start("http://www.factordb.com/index.php?query=" + txtPrimeQ.Text);
        }

        private void btnFactorDB_Click(object sender, EventArgs e)
        {
            rb10.Checked = true;
            Process.Start("http://www.factordb.com/index.php?query=" + txtPrimeP.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnFactorDbP_Click(object sender, EventArgs e)
        {
            rb10.Checked = true;
            Process.Start("http://www.factordb.com/index.php?query=" + txtPrimeP.Text);
        }

        private void btnFactorDbQ_Click(object sender, EventArgs e)
        {
            rb10.Checked = true;
            Process.Start("http://www.factordb.com/index.php?query=" + txtPrimeQ.Text);
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

        private void txtInfo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnToHex_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rtxtData.Text))
            {
                chkHexText.Checked = true;
                rbText.Checked= true;
                string s = rtxtData.Text;
                s = Converter.FilterData(s, 65001, chkDiscardChars.Checked, chkDiacritics.Checked, chkzStrings.Checked);
                byte[] b = MyClass.GetBytes(s);
                txtInfo.Text = MyClass.BinaryToHexString(b);
            }
        }

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

        private void btnAutoAdd_Click(object sender, EventArgs e)
        {
            int n;
            if (!Int32.TryParse(txtPlus.Text, out n)) n = 0;

            if (sender is Button b) if (b.Name.Contains("Sub")) n = -n;

            for (int i=0;i<44;i++)
            {
                   int v = ValueOf(listTxtCS[i].Text);
                   if (v!=0) listTxtCS[i].Text = ValidChar(v + n).ToString();
            }
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
            logMsg("HexView Cleared");
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
                    logMsg("File Loaded:" + openFile.FileName);
                }
            }
            catch (Exception ex) { logMsg(ex.Message); }
        }

        private void chkSendToHexViewer_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "SendToHexViewer", chkSendToHexViewer.Checked?"Yes":"No");
            hexBox.BytesPerLine=Int32.Parse(cmbBytesPerLine.Text);
            hexBox.Refresh();
        }

        private void chkSendToBuffer_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "SendToBuffer", chkSendToBuffer.Checked ? "Yes" : "No");
        }

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
                logMsg("Changes Applied to Disk");
            }
            catch (Exception ex) { logMsg(ex.Message); }
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
            } catch (Exception ex) { logMsg(ex.Message); }
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
            } catch (Exception ex) { logMsg(ex.Message); }
        }
        private void hexBox_TextChanged(object sender, EventArgs e)
        {
            CalcHHash();
        }

        private void cmbHHash_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcHHash();
        }

        private void hexBox_Click(object sender, EventArgs e)
        {

        }

        private void btnCalcHHash_Click(object sender, EventArgs e)
        {
            CalcHHash();
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
            catch (Exception ex) { logMsg(ex.Message); }
        }

        private void btnSCalc_Click(object sender, EventArgs e)
        {
            rb16.Checked = true;
            txtPrimeP.Text = MyClass.BinaryToHexString(SafeRead(Application.StartupPath + "\\enc.txt"));
            tabControl1.SelectedTab = tabCalculator;
          }

        private void btnSCrypto_Click(object sender, EventArgs e)
        {
            rbFileBuffer.Checked = true;
            fileBuffer=SafeRead(Application.StartupPath + "\\enc.txt");
            tabControl1.SelectedTab = tabCrypto;
        }

        private void btnSHex_Click(object sender, EventArgs e)
        {
            SendToHexViewer(Application.StartupPath + "\\enc.txt");
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

        public void drawPoint(Graphics g,SolidBrush brush,int size, int x, int y,bool fill=true)
        {
            x -= 1;y -= 1;
            int pointSize = int.Parse(lblPointSize.Text);
            var pen = new Pen(brush, pointSize);
            Point dPoint = new Point(x*pointSize+(!fill?pointSize/2:0) , y*pointSize + (!fill ? pointSize / 2 : 0));
            Rectangle rect = new Rectangle(dPoint, new Size((size-(fill?0:1))*pointSize, (size-(fill?0:1))*pointSize ));
            if (fill) 
                g.FillRectangle(brush, rect); 
            else 
                g.DrawRectangle(pen, rect);
            pen.Dispose();
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
            File.Copy(file, file+".tmp");
            byte[] buffer = File.ReadAllBytes(file+".tmp");
            File.Delete(file+".tmp");
            return buffer;
        }

        private void SetSizeMod()
        {

            switch (cmbSizeMode.SelectedIndex)
            {
                case 0: picQuran1.SizeMode = PictureBoxSizeMode.Normal; picQuran2.SizeMode = PictureBoxSizeMode.Normal; break;
                case 1: picQuran1.SizeMode = PictureBoxSizeMode.StretchImage; picQuran2.SizeMode = PictureBoxSizeMode.StretchImage; break;
                case 2: picQuran1.SizeMode = PictureBoxSizeMode.AutoSize; picQuran2.SizeMode = PictureBoxSizeMode.AutoSize; break;
                case 3: picQuran1.SizeMode = PictureBoxSizeMode.CenterImage; picQuran2.SizeMode = PictureBoxSizeMode.CenterImage; break;
                case 4: picQuran1.SizeMode = PictureBoxSizeMode.Zoom; picQuran2.SizeMode = PictureBoxSizeMode.Zoom; break;
            }
            picQuran1.Width = picSpace.size1;
            picQuran1.Height = picSpace.size1;
            picQuran2.Width = picSpace.size2;
            picQuran2.Height = picSpace.size2;
            picQuran1.Left = picSpace.x1; picQuran1.Top = picSpace.y1; 
            picQuran2.Left = picSpace.x2; picQuran2.Top = picSpace.y2;

            picQuran1.Invalidate();
            picQuran2.Invalidate();
        }

        private bool InArea(int x,int y,int bpl)
        {
            foreach (var area in areas)
            {
                if (x >= area.x && x < (area.x + area.size) && y >= area.y && y < (area.y + area.size)) return true;
            }
            return false;
        }

        private Area DrawQRBlock(Graphics g,SolidBrush brush,int pos,int bpl,bool flipped = false)
        {
            int x=0, y=0,eSize=7,iSize=3,eaSize=5,iaSize=1;
            Area area=new();
            switch (pos)
            {
                case 0: x = 1;y = 1; area.x = x; area.y = y; area.size = eSize + 1; break;
                case 1: x = bpl - eSize + 1;y = 1; area.x = x-1; area.y = y; area.size = eSize + 1; break;
                case 2: if (!flipped)
                    {
                        x = 1;y = bpl - eSize + 1; ;
                        area.x = x; area.y = y-1; area.size = eSize + 1;
                    }
                    else
                    {
                        x = bpl - eSize+1;y = bpl - eSize+1;
                        area.x = x-1; area.y = y-1; area.size = eSize + 1;
                    }

                    break;
                case 3: 
                    if (!flipped)
                    {
                        x = bpl - eSize - 1;y = bpl - eSize - 1;
                    } else
                    {
                        x = eaSize;y = bpl - eSize - 1;
                    }
                    eSize = eaSize; iSize = iaSize;
                    area.x = x; area.y = y; area.size = eSize;
                    break;
            }
            

            drawPoint(g, brush,  eSize, x, y, false);
            drawPoint(g, brush,  iSize, x+2, y+2);
            return area;
        }
        private void DrawImage()
        {
            tabControl1.SelectedTab = tabImage;
            byte[] buffer = SafeRead(Application.StartupPath + "\\enc.txt");
            if (buffer.Length == 0) return;
            int padding = 0;
            int n = buffer.Length * 8;
            int bpl =  (int)Math.Sqrt(n);
            while (bpl * bpl < n) bpl++;
            if (chkFixPadding.Checked)
            {
                padding = bpl * bpl-n;
                if (padding < 0) padding *= -1;
            }
            
            int pointSize = Int32.Parse(lblPointSize.Text);

            string bin = ToBinaryString(buffer);

            SetSizeMod();
            
            //var g= picQuran.CreateGraphics();
            picQuran1.Image = new Bitmap(bpl*pointSize, bpl*pointSize);
            var g = Graphics.FromImage(picQuran1.Image);

            int n2 = buffer.Length * 8 + 8*8*3+5*5;
            int bpl2 = (int)Math.Sqrt(n2);
            while (bpl2 * bpl2 < n2) bpl2++;
            picQuran2.Image = new Bitmap(bpl2 * pointSize, bpl2 * pointSize);
            var g2 = Graphics.FromImage(picQuran2.Image);

            SolidBrush brush = new SolidBrush(chkINV.Checked?Color.Black:Color.White);
            Pen pen = new Pen(chkINV.Checked ? Color.Black : Color.White, pointSize);// float.Parse(lblScale.Text));

            picQuran1.BackColor = backColor;
            picQuran2.BackColor = backColor;

            g.Clear(picQuran1.BackColor);picQuran1.Invalidate();
            g2.Clear(picQuran2.BackColor); picQuran2.Invalidate();


            for (int i = 0; i < bin.Length+ padding; i++)
            {
                int y = (i / bpl)+1;
                int x = (i % bpl)+1;
                if (i>= padding) if (bin[i- padding] == '1') drawPoint(g, brush, 1 , x   , y  );
            }
            switch ((chkFlipX.Checked ? 2 : 0) + (chkFlipY.Checked ? 1 : 0))
            {
                case 1: picQuran1.Image.RotateFlip(RotateFlipType.RotateNoneFlipY); break;
                case 2: picQuran1.Image.RotateFlip(RotateFlipType.RotateNoneFlipX); break;
                case 3: picQuran1.Image.RotateFlip(RotateFlipType.RotateNoneFlipXY); break;
            }

            areas.Clear();
            areas.Add(DrawQRBlock(g2,brush, 0,bpl2));
            areas.Add(DrawQRBlock(g2,brush, 1,bpl2));
            areas.Add(DrawQRBlock(g2,brush, 2,bpl2,chkFlipX.Checked));
            areas.Add(DrawQRBlock(g2,brush, 3,bpl2,chkFlipX.Checked));
           
            int j = 0;
            for (int i = 0; i < bin.Length ; i++)
            {
                int x,y;
                do
                {
                    y = (j / bpl2)+1;
                    x = (j % bpl2)+1;
                    j++;
                } while (InArea(x, y,bpl2));

                if (bin[i] == '1') drawPoint(g2, brush, 1, x  , y  );
            }
            
            switch ((chkFlipX.Checked ? 2 : 0) + (chkFlipY.Checked ? 1 : 0))
            {
                case 1: picQuran2.Image.RotateFlip(RotateFlipType.RotateNoneFlipY); break;
                case 2: picQuran2.Image.RotateFlip(RotateFlipType.RotateNoneFlipX); break;
                case 3: picQuran2.Image.RotateFlip(RotateFlipType.RotateNoneFlipXY); break;
            }

            picQuran1.Invalidate();
            picQuran2.Invalidate();
            pen.Dispose();
            brush.Dispose();
            g.Dispose();
        }
        private void btnSImage_Click(object sender, EventArgs e)
        {
            DrawImage();
        }

        private void lstLog_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void lblScale_TextChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "Scale", lblPointSize.Text);
        }

        private void cmbSizeMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSizeMod();
            DrawImage();
            AppSettings.WriteValue("Settings", "SizeMode", cmbSizeMode.SelectedIndex.ToString());
        }

        private void tabImage_Click(object sender, EventArgs e)
        {

        }

        private void btnResetImage_Click(object sender, EventArgs e)
        {
            DrawImage();
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
        }

        private void chkFlipX_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "FlipX", chkFlipX.Checked ? "YES" : "NO");
            DrawImage();        }

        private void chkINV_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.WriteValue("Settings", "Inversed", chkINV.Checked ? "YES" : "NO");
            backColor = chkINV.Checked ? Color.White : Color.Black;
            DrawImage();
        }

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
                            && cmbSourceEnc.Text != cmbDestEnc.Text;
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
                        logMsg("Converted files will be output to:"
                                    + System.Environment.NewLine + destDir
                                    + System.Environment.NewLine + _sourceFiles.Length
                                    + " file(s) selected for conversion" + System.Environment.NewLine);
                    }
                }
            }
            catch (Exception ex) { logMsg("Error:" + ex.Message); }
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
/*
            cmbSourceEnc.Items.Add("Arabic Common Charset Order - 65536");
            cmbDestEnc.Items.Add("Arabic Common Charset Order - 65536");

            cmbSourceEnc.Items.Add("Arabic UTF8 (SingleByte) - 65540");
            cmbDestEnc.Items.Add("Arabic UTF8 (SingleByte) - 65540");

            cmbSourceEnc.Items.Add("Arabic UNICODE (SingleByte) - 65537");
            cmbDestEnc.Items.Add("Arabic UNICODE (SingleByte) - 65537");

            cmbSourceEnc.Items.Add("Arabic CUSTOM Order - 65541");
            cmbDestEnc.Items.Add("Arabic CUSTOM Order - 65541");
*/
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
        #endregion
    }
}
