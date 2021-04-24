﻿
namespace CryptoUtility
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tabCrypto = new System.Windows.Forms.TabPage();
            this.chkPadding = new System.Windows.Forms.CheckBox();
            this.rbFileBuffer = new System.Windows.Forms.RadioButton();
            this.rbTextBox = new System.Windows.Forms.RadioButton();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.cmbCryptoAlgorithm = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbKeyLength = new System.Windows.Forms.ComboBox();
            this.cmbData = new System.Windows.Forms.ComboBox();
            this.cmbEncryptionKey = new System.Windows.Forms.ComboBox();
            this.cmbHash = new System.Windows.Forms.ComboBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnHash = new System.Windows.Forms.Button();
            this.btnSaveKeys = new System.Windows.Forms.Button();
            this.btnLoadKeys = new System.Windows.Forms.Button();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.btnUseKeys = new System.Windows.Forms.Button();
            this.btnVerify = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnSendToHex = new System.Windows.Forms.Button();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnSign = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.txtHash = new System.Windows.Forms.TextBox();
            this.txtData = new System.Windows.Forms.TextBox();
            this.txtPrivateKey = new System.Windows.Forms.TextBox();
            this.txtPublicKey = new System.Windows.Forms.TextBox();
            this.tabRSA = new System.Windows.Forms.TabPage();
            this.label32 = new System.Windows.Forms.Label();
            this.cmbRSAKeyLen = new System.Windows.Forms.ComboBox();
            this.btnImportPrivateKey = new System.Windows.Forms.Button();
            this.btnImportPublicKey = new System.Windows.Forms.Button();
            this.btnGenKeys = new System.Windows.Forms.Button();
            this.chkReverse = new System.Windows.Forms.CheckBox();
            this.txtInverseQ = new System.Windows.Forms.TextBox();
            this.txtDQ = new System.Windows.Forms.TextBox();
            this.txtDP = new System.Windows.Forms.TextBox();
            this.txtQ = new System.Windows.Forms.TextBox();
            this.txtP = new System.Windows.Forms.TextBox();
            this.txtN = new System.Windows.Forms.TextBox();
            this.txtD = new System.Windows.Forms.TextBox();
            this.txtE = new System.Windows.Forms.TextBox();
            this.txtPEMPublicKey = new System.Windows.Forms.TextBox();
            this.txtPEMPrivateKey = new System.Windows.Forms.TextBox();
            this.btnUsePrivateKey = new System.Windows.Forms.Button();
            this.btnExportPrivate = new System.Windows.Forms.Button();
            this.btnUsePublicKey = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.btnExportPublic = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabDSA = new System.Windows.Forms.TabPage();
            this.cmbDSAKeyLen = new System.Windows.Forms.ComboBox();
            this.btnDSAImportPrivate = new System.Windows.Forms.Button();
            this.btnDSAImportPublic = new System.Windows.Forms.Button();
            this.btnGenDSAKeys = new System.Windows.Forms.Button();
            this.btnExportDSAPublic = new System.Windows.Forms.Button();
            this.btnExportDSA = new System.Windows.Forms.Button();
            this.txtDSA_X = new System.Windows.Forms.TextBox();
            this.txtDSA_Y = new System.Windows.Forms.TextBox();
            this.txtDSA_G = new System.Windows.Forms.TextBox();
            this.txtDSA_Q = new System.Windows.Forms.TextBox();
            this.txtDSA_P = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.txtDSAPEMPrivate = new System.Windows.Forms.TextBox();
            this.txtDSAPEMPublic = new System.Windows.Forms.TextBox();
            this.tabCalculator = new System.Windows.Forms.TabPage();
            this.chkPositives = new System.Windows.Forms.CheckBox();
            this.rb64b = new System.Windows.Forms.RadioButton();
            this.rb16 = new System.Windows.Forms.RadioButton();
            this.rb2 = new System.Windows.Forms.RadioButton();
            this.rb10 = new System.Windows.Forms.RadioButton();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.cmbKeyLen = new System.Windows.Forms.ComboBox();
            this.label40 = new System.Windows.Forms.Label();
            this.cmbExponentMethod = new System.Windows.Forms.ComboBox();
            this.label39 = new System.Windows.Forms.Label();
            this.cmbMULMethod = new System.Windows.Forms.ComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.cmbAccelerator = new System.Windows.Forms.ComboBox();
            this.btnRTP = new System.Windows.Forms.Button();
            this.btnFCD = new System.Windows.Forms.Button();
            this.btnLCM = new System.Windows.Forms.Button();
            this.btnPWM = new System.Windows.Forms.Button();
            this.btnPOW = new System.Windows.Forms.Button();
            this.btnMOD = new System.Windows.Forms.Button();
            this.btnDIV = new System.Windows.Forms.Button();
            this.btnMUL = new System.Windows.Forms.Button();
            this.btnSUB = new System.Windows.Forms.Button();
            this.btnClearCalc = new System.Windows.Forms.Button();
            this.btnGenPrime = new System.Windows.Forms.Button();
            this.btnXOR = new System.Windows.Forms.Button();
            this.btnOR = new System.Windows.Forms.Button();
            this.btnSHR = new System.Windows.Forms.Button();
            this.btnSHL = new System.Windows.Forms.Button();
            this.btnBezout = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSqrt = new System.Windows.Forms.Button();
            this.btnFactorizeQ = new System.Windows.Forms.Button();
            this.btnFactorizeP = new System.Windows.Forms.Button();
            this.btnFactorDbQ = new System.Windows.Forms.Button();
            this.btnFactorDbP = new System.Windows.Forms.Button();
            this.btnReverseQ = new System.Windows.Forms.Button();
            this.btnReverseP = new System.Windows.Forms.Button();
            this.btnIsEven = new System.Windows.Forms.Button();
            this.btnIsPrime = new System.Windows.Forms.Button();
            this.btnADD = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtResultR = new System.Windows.Forms.TextBox();
            this.txtModulN = new System.Windows.Forms.TextBox();
            this.txtPrimeQ = new System.Windows.Forms.TextBox();
            this.txtPrimeP = new System.Windows.Forms.TextBox();
            this.tabEncoding = new System.Windows.Forms.TabPage();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.chkRTL = new System.Windows.Forms.CheckBox();
            this.chkOutText = new System.Windows.Forms.CheckBox();
            this.chkALLEncodings = new System.Windows.Forms.CheckBox();
            this.chkJommalWord = new System.Windows.Forms.CheckBox();
            this.chkSendToBuffer = new System.Windows.Forms.CheckBox();
            this.chkDiacritics = new System.Windows.Forms.CheckBox();
            this.chkzStrings = new System.Windows.Forms.CheckBox();
            this.chkDiscardChars = new System.Windows.Forms.CheckBox();
            this.chkHexText = new System.Windows.Forms.CheckBox();
            this.chkUnicodeAsDecimal = new System.Windows.Forms.CheckBox();
            this.chkMeta = new System.Windows.Forms.CheckBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnSCrypto = new System.Windows.Forms.Button();
            this.btnSImage = new System.Windows.Forms.Button();
            this.btnSCalc = new System.Windows.Forms.Button();
            this.btnColor = new System.Windows.Forms.Button();
            this.btnSpectrum = new System.Windows.Forms.Button();
            this.btnSHex = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rbFiles = new System.Windows.Forms.RadioButton();
            this.rbText = new System.Windows.Forms.RadioButton();
            this.progFiles = new System.Windows.Forms.ProgressBar();
            this.btnToHex = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnClearFiles = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.grpSource = new System.Windows.Forms.GroupBox();
            this.lsSource = new System.Windows.Forms.ListBox();
            this.grpEncodings = new System.Windows.Forms.GroupBox();
            this.rtxtData = new System.Windows.Forms.TextBox();
            this.txtDestEnc = new System.Windows.Forms.TextBox();
            this.txtSourceEnc = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.labDestEnc = new System.Windows.Forms.Label();
            this.cmbDestEnc = new System.Windows.Forms.ComboBox();
            this.cmbSourceEnc = new System.Windows.Forms.ComboBox();
            this.labSourceEnc = new System.Windows.Forms.Label();
            this.tabQuran = new System.Windows.Forms.TabPage();
            this.rbDiacritics = new System.Windows.Forms.RadioButton();
            this.rbNoDiacritics = new System.Windows.Forms.RadioButton();
            this.rbFirstOriginalDots = new System.Windows.Forms.RadioButton();
            this.rbFirstOriginal = new System.Windows.Forms.RadioButton();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvQuran = new System.Windows.Forms.DataGridView();
            this.Serial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoraNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AyaNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoraName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AyaText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblFontSize = new System.Windows.Forms.Label();
            this.btnMinus = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.lbSoras = new System.Windows.Forms.ListBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSendToEncoding = new System.Windows.Forms.Button();
            this.txtQuranText = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.tabCharset = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblCSS44 = new System.Windows.Forms.Label();
            this.lblCSS43 = new System.Windows.Forms.Label();
            this.lblCSS42 = new System.Windows.Forms.Label();
            this.lblCSS41 = new System.Windows.Forms.Label();
            this.lblCSS40 = new System.Windows.Forms.Label();
            this.lblCSS39 = new System.Windows.Forms.Label();
            this.lblCSS38 = new System.Windows.Forms.Label();
            this.lblCSS37 = new System.Windows.Forms.Label();
            this.lblCSS36 = new System.Windows.Forms.Label();
            this.lblCSS35 = new System.Windows.Forms.Label();
            this.lblCSS34 = new System.Windows.Forms.Label();
            this.lblCSS33 = new System.Windows.Forms.Label();
            this.lblCSS32 = new System.Windows.Forms.Label();
            this.lblCSS31 = new System.Windows.Forms.Label();
            this.lblCSS30 = new System.Windows.Forms.Label();
            this.lblCSS29 = new System.Windows.Forms.Label();
            this.lblCSS28 = new System.Windows.Forms.Label();
            this.lblCSS27 = new System.Windows.Forms.Label();
            this.lblCSS26 = new System.Windows.Forms.Label();
            this.lblCSS25 = new System.Windows.Forms.Label();
            this.lblCSS24 = new System.Windows.Forms.Label();
            this.lblCSS23 = new System.Windows.Forms.Label();
            this.lblCSS22 = new System.Windows.Forms.Label();
            this.lblCSS21 = new System.Windows.Forms.Label();
            this.lblCSS20 = new System.Windows.Forms.Label();
            this.lblCSS19 = new System.Windows.Forms.Label();
            this.lblCSS18 = new System.Windows.Forms.Label();
            this.lblCSS17 = new System.Windows.Forms.Label();
            this.lblCSS16 = new System.Windows.Forms.Label();
            this.lblCSS15 = new System.Windows.Forms.Label();
            this.lblCSS14 = new System.Windows.Forms.Label();
            this.lblCSS13 = new System.Windows.Forms.Label();
            this.lblCSS12 = new System.Windows.Forms.Label();
            this.lblCSS11 = new System.Windows.Forms.Label();
            this.lblCSS10 = new System.Windows.Forms.Label();
            this.lblCSS9 = new System.Windows.Forms.Label();
            this.lblCSS8 = new System.Windows.Forms.Label();
            this.lblCSS7 = new System.Windows.Forms.Label();
            this.lblCSS6 = new System.Windows.Forms.Label();
            this.lblCSS5 = new System.Windows.Forms.Label();
            this.lblCSS4 = new System.Windows.Forms.Label();
            this.lblCSS3 = new System.Windows.Forms.Label();
            this.lblCSS2 = new System.Windows.Forms.Label();
            this.lblCSS1 = new System.Windows.Forms.Label();
            this.txtPlus = new System.Windows.Forms.TextBox();
            this.lblCurCharset = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnClearCS = new System.Windows.Forms.Button();
            this.btnAutoSub = new System.Windows.Forms.Button();
            this.btnAutoAdd = new System.Windows.Forms.Button();
            this.btnAutoCharset = new System.Windows.Forms.Button();
            this.btnLoadCharset = new System.Windows.Forms.Button();
            this.btnOrder = new System.Windows.Forms.Button();
            this.btnSaveCharset = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtCS44 = new System.Windows.Forms.TextBox();
            this.txtCS43 = new System.Windows.Forms.TextBox();
            this.txtCS42 = new System.Windows.Forms.TextBox();
            this.txtCS41 = new System.Windows.Forms.TextBox();
            this.txtCS40 = new System.Windows.Forms.TextBox();
            this.txtCS39 = new System.Windows.Forms.TextBox();
            this.txtCS38 = new System.Windows.Forms.TextBox();
            this.txtCS37 = new System.Windows.Forms.TextBox();
            this.txtCS36 = new System.Windows.Forms.TextBox();
            this.txtCS35 = new System.Windows.Forms.TextBox();
            this.txtCS34 = new System.Windows.Forms.TextBox();
            this.lblCS44 = new System.Windows.Forms.Label();
            this.lblCS43 = new System.Windows.Forms.Label();
            this.lblCS42 = new System.Windows.Forms.Label();
            this.lblCS41 = new System.Windows.Forms.Label();
            this.lblCS40 = new System.Windows.Forms.Label();
            this.lblCS39 = new System.Windows.Forms.Label();
            this.lblCS38 = new System.Windows.Forms.Label();
            this.lblCS37 = new System.Windows.Forms.Label();
            this.lblCS36 = new System.Windows.Forms.Label();
            this.lblCS35 = new System.Windows.Forms.Label();
            this.lblCS34 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtCS33 = new System.Windows.Forms.TextBox();
            this.txtCS32 = new System.Windows.Forms.TextBox();
            this.txtCS31 = new System.Windows.Forms.TextBox();
            this.txtCS30 = new System.Windows.Forms.TextBox();
            this.txtCS29 = new System.Windows.Forms.TextBox();
            this.txtCS28 = new System.Windows.Forms.TextBox();
            this.txtCS27 = new System.Windows.Forms.TextBox();
            this.txtCS26 = new System.Windows.Forms.TextBox();
            this.txtCS25 = new System.Windows.Forms.TextBox();
            this.txtCS24 = new System.Windows.Forms.TextBox();
            this.txtCS23 = new System.Windows.Forms.TextBox();
            this.lblCS33 = new System.Windows.Forms.Label();
            this.lblCS32 = new System.Windows.Forms.Label();
            this.lblCS31 = new System.Windows.Forms.Label();
            this.lblCS30 = new System.Windows.Forms.Label();
            this.lblCS29 = new System.Windows.Forms.Label();
            this.lblCS28 = new System.Windows.Forms.Label();
            this.lblCS27 = new System.Windows.Forms.Label();
            this.lblCS26 = new System.Windows.Forms.Label();
            this.lblCS25 = new System.Windows.Forms.Label();
            this.lblCS24 = new System.Windows.Forms.Label();
            this.lblCS23 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtCS22 = new System.Windows.Forms.TextBox();
            this.txtCS21 = new System.Windows.Forms.TextBox();
            this.txtCS20 = new System.Windows.Forms.TextBox();
            this.txtCS19 = new System.Windows.Forms.TextBox();
            this.txtCS18 = new System.Windows.Forms.TextBox();
            this.txtCS17 = new System.Windows.Forms.TextBox();
            this.txtCS16 = new System.Windows.Forms.TextBox();
            this.txtCS15 = new System.Windows.Forms.TextBox();
            this.txtCS14 = new System.Windows.Forms.TextBox();
            this.txtCS13 = new System.Windows.Forms.TextBox();
            this.txtCS12 = new System.Windows.Forms.TextBox();
            this.lblCS22 = new System.Windows.Forms.Label();
            this.lblCS21 = new System.Windows.Forms.Label();
            this.lblCS20 = new System.Windows.Forms.Label();
            this.lblCS19 = new System.Windows.Forms.Label();
            this.lblCS18 = new System.Windows.Forms.Label();
            this.lblCS17 = new System.Windows.Forms.Label();
            this.lblCS16 = new System.Windows.Forms.Label();
            this.lblCS15 = new System.Windows.Forms.Label();
            this.lblCS14 = new System.Windows.Forms.Label();
            this.lblCS13 = new System.Windows.Forms.Label();
            this.lblCS12 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCS11 = new System.Windows.Forms.TextBox();
            this.txtCS10 = new System.Windows.Forms.TextBox();
            this.txtCS9 = new System.Windows.Forms.TextBox();
            this.txtCS8 = new System.Windows.Forms.TextBox();
            this.txtCS7 = new System.Windows.Forms.TextBox();
            this.txtCS6 = new System.Windows.Forms.TextBox();
            this.txtCS5 = new System.Windows.Forms.TextBox();
            this.txtCS4 = new System.Windows.Forms.TextBox();
            this.txtCS3 = new System.Windows.Forms.TextBox();
            this.txtCS2 = new System.Windows.Forms.TextBox();
            this.txtCS1 = new System.Windows.Forms.TextBox();
            this.lblCS11 = new System.Windows.Forms.Label();
            this.lblCS10 = new System.Windows.Forms.Label();
            this.lblCS9 = new System.Windows.Forms.Label();
            this.lblCS8 = new System.Windows.Forms.Label();
            this.lblCS7 = new System.Windows.Forms.Label();
            this.lblCS6 = new System.Windows.Forms.Label();
            this.lblCS5 = new System.Windows.Forms.Label();
            this.lblCS4 = new System.Windows.Forms.Label();
            this.lblCS3 = new System.Windows.Forms.Label();
            this.lblCS2 = new System.Windows.Forms.Label();
            this.lblCS1 = new System.Windows.Forms.Label();
            this.tabHexViewer = new System.Windows.Forms.TabPage();
            this.hexBox = new Be.Windows.Forms.HexBox();
            this.lblHash = new System.Windows.Forms.TextBox();
            this.cmbHHash = new System.Windows.Forms.ComboBox();
            this.cmbHEncoding = new System.Windows.Forms.ComboBox();
            this.label45 = new System.Windows.Forms.Label();
            this.cmbBytesPerLine = new System.Windows.Forms.ComboBox();
            this.label46 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.lblHexFile = new System.Windows.Forms.Label();
            this.btnClearHex = new System.Windows.Forms.Button();
            this.btnApplyChanges = new System.Windows.Forms.Button();
            this.btnCalcHHash = new System.Windows.Forms.Button();
            this.btnSendToCrypto = new System.Windows.Forms.Button();
            this.btnSendToCalc = new System.Windows.Forms.Button();
            this.btnOpenHexFile = new System.Windows.Forms.Button();
            this.tabXRay = new System.Windows.Forms.TabPage();
            this.chkINV = new System.Windows.Forms.CheckBox();
            this.chkFlipX = new System.Windows.Forms.CheckBox();
            this.chkFlipY = new System.Windows.Forms.CheckBox();
            this.chkFixPadding = new System.Windows.Forms.CheckBox();
            this.lblPointSize = new System.Windows.Forms.Label();
            this.btnSMinus = new System.Windows.Forms.Button();
            this.btnSPlus = new System.Windows.Forms.Button();
            this.label49 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.cmbSizeMode = new System.Windows.Forms.ComboBox();
            this.btnScreenShot = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.btnResetImage = new System.Windows.Forms.Button();
            this.btnRotate = new System.Windows.Forms.Button();
            this.picQuran2 = new System.Windows.Forms.PictureBox();
            this.picQuran1 = new System.Windows.Forms.PictureBox();
            this.tabSpectrum = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.spectrumRButton = new System.Windows.Forms.RadioButton();
            this.waveRButton = new System.Windows.Forms.RadioButton();
            this.label51 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.cmbChannels = new System.Windows.Forms.ComboBox();
            this.cmbBits = new System.Windows.Forms.ComboBox();
            this.cmbSampleRate = new System.Windows.Forms.ComboBox();
            this.chkPlay = new System.Windows.Forms.CheckBox();
            this.stopButton = new System.Windows.Forms.Button();
            this.btnResetSpectrum = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.tabColor = new System.Windows.Forms.TabPage();
            this.texture = new System.Windows.Forms.PictureBox();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.texture2 = new System.Windows.Forms.PictureBox();
            this.tabCrypto.SuspendLayout();
            this.tabRSA.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabDSA.SuspendLayout();
            this.tabCalculator.SuspendLayout();
            this.tabEncoding.SuspendLayout();
            this.grpOptions.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            this.grpSource.SuspendLayout();
            this.grpEncodings.SuspendLayout();
            this.tabQuran.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuran)).BeginInit();
            this.tabCharset.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabHexViewer.SuspendLayout();
            this.tabXRay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQuran2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picQuran1)).BeginInit();
            this.tabSpectrum.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.tabColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.texture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.texture2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabCrypto
            // 
            this.tabCrypto.BackColor = System.Drawing.Color.LightGray;
            this.tabCrypto.Controls.Add(this.chkPadding);
            this.tabCrypto.Controls.Add(this.rbFileBuffer);
            this.tabCrypto.Controls.Add(this.rbTextBox);
            this.tabCrypto.Controls.Add(this.label15);
            this.tabCrypto.Controls.Add(this.label14);
            this.tabCrypto.Controls.Add(this.label13);
            this.tabCrypto.Controls.Add(this.label31);
            this.tabCrypto.Controls.Add(this.cmbCryptoAlgorithm);
            this.tabCrypto.Controls.Add(this.label5);
            this.tabCrypto.Controls.Add(this.cmbKeyLength);
            this.tabCrypto.Controls.Add(this.cmbData);
            this.tabCrypto.Controls.Add(this.cmbEncryptionKey);
            this.tabCrypto.Controls.Add(this.cmbHash);
            this.tabCrypto.Controls.Add(this.btnClear);
            this.tabCrypto.Controls.Add(this.btnHash);
            this.tabCrypto.Controls.Add(this.btnSaveKeys);
            this.tabCrypto.Controls.Add(this.btnLoadKeys);
            this.tabCrypto.Controls.Add(this.btnLoadFile);
            this.tabCrypto.Controls.Add(this.btnUseKeys);
            this.tabCrypto.Controls.Add(this.btnVerify);
            this.tabCrypto.Controls.Add(this.btnDecrypt);
            this.tabCrypto.Controls.Add(this.button6);
            this.tabCrypto.Controls.Add(this.button3);
            this.tabCrypto.Controls.Add(this.btnSendToHex);
            this.tabCrypto.Controls.Add(this.btnEncrypt);
            this.tabCrypto.Controls.Add(this.btnSign);
            this.tabCrypto.Controls.Add(this.label7);
            this.tabCrypto.Controls.Add(this.label6);
            this.tabCrypto.Controls.Add(this.label8);
            this.tabCrypto.Controls.Add(this.lblData);
            this.tabCrypto.Controls.Add(this.label4);
            this.tabCrypto.Controls.Add(this.txtOutput);
            this.tabCrypto.Controls.Add(this.txtHash);
            this.tabCrypto.Controls.Add(this.txtData);
            this.tabCrypto.Controls.Add(this.txtPrivateKey);
            this.tabCrypto.Controls.Add(this.txtPublicKey);
            this.tabCrypto.Location = new System.Drawing.Point(4, 28);
            this.tabCrypto.Name = "tabCrypto";
            this.tabCrypto.Padding = new System.Windows.Forms.Padding(3);
            this.tabCrypto.Size = new System.Drawing.Size(1331, 663);
            this.tabCrypto.TabIndex = 1;
            this.tabCrypto.Text = "Crypto";
            // 
            // chkPadding
            // 
            this.chkPadding.AutoSize = true;
            this.chkPadding.Location = new System.Drawing.Point(833, 525);
            this.chkPadding.Name = "chkPadding";
            this.chkPadding.Size = new System.Drawing.Size(123, 23);
            this.chkPadding.TabIndex = 10;
            this.chkPadding.Text = "Use Padding";
            this.chkPadding.UseVisualStyleBackColor = true;
            // 
            // rbFileBuffer
            // 
            this.rbFileBuffer.AutoSize = true;
            this.rbFileBuffer.Location = new System.Drawing.Point(1164, 523);
            this.rbFileBuffer.Name = "rbFileBuffer";
            this.rbFileBuffer.Size = new System.Drawing.Size(136, 23);
            this.rbFileBuffer.TabIndex = 12;
            this.rbFileBuffer.Text = "Use File Buffer";
            this.rbFileBuffer.UseVisualStyleBackColor = true;
            // 
            // rbTextBox
            // 
            this.rbTextBox.AutoSize = true;
            this.rbTextBox.Checked = true;
            this.rbTextBox.Location = new System.Drawing.Point(1022, 524);
            this.rbTextBox.Name = "rbTextBox";
            this.rbTextBox.Size = new System.Drawing.Size(122, 23);
            this.rbTextBox.TabIndex = 11;
            this.rbTextBox.TabStop = true;
            this.rbTextBox.Text = "Use TextBox";
            this.rbTextBox.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.SystemColors.Info;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(1022, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(278, 33);
            this.label15.TabIndex = 6;
            this.label15.Text = "Encryption Key";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.SystemColors.Info;
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(740, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(276, 33);
            this.label14.TabIndex = 6;
            this.label14.Text = "Hash Algorithm";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.SystemColors.Info;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(528, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(196, 33);
            this.label13.TabIndex = 6;
            this.label13.Text = "Data Format";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.SystemColors.Info;
            this.label31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label31.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(164, 23);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(173, 33);
            this.label31.TabIndex = 6;
            this.label31.Text = "Crypto Algorithm";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbCryptoAlgorithm
            // 
            this.cmbCryptoAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCryptoAlgorithm.FormattingEnabled = true;
            this.cmbCryptoAlgorithm.Items.AddRange(new object[] {
            "RSA",
            "DSA",
            "ecDSA",
            "edDSA",
            "ECC",
            "QuDSA"});
            this.cmbCryptoAlgorithm.Location = new System.Drawing.Point(164, 59);
            this.cmbCryptoAlgorithm.Name = "cmbCryptoAlgorithm";
            this.cmbCryptoAlgorithm.Size = new System.Drawing.Size(173, 27);
            this.cmbCryptoAlgorithm.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Info;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(343, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(179, 33);
            this.label5.TabIndex = 6;
            this.label5.Text = "Key Length";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbKeyLength
            // 
            this.cmbKeyLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKeyLength.FormattingEnabled = true;
            this.cmbKeyLength.Items.AddRange(new object[] {
            "512",
            "1024",
            "2048",
            "4096"});
            this.cmbKeyLength.Location = new System.Drawing.Point(343, 59);
            this.cmbKeyLength.Name = "cmbKeyLength";
            this.cmbKeyLength.Size = new System.Drawing.Size(179, 27);
            this.cmbKeyLength.TabIndex = 1;
            // 
            // cmbData
            // 
            this.cmbData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbData.FormattingEnabled = true;
            this.cmbData.Items.AddRange(new object[] {
            "Base64",
            "Text",
            "Hex"});
            this.cmbData.Location = new System.Drawing.Point(528, 59);
            this.cmbData.Name = "cmbData";
            this.cmbData.Size = new System.Drawing.Size(196, 27);
            this.cmbData.TabIndex = 2;
            this.cmbData.SelectedIndexChanged += new System.EventHandler(this.cmbData_SelectedIndexChanged);
            // 
            // cmbEncryptionKey
            // 
            this.cmbEncryptionKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEncryptionKey.FormattingEnabled = true;
            this.cmbEncryptionKey.Items.AddRange(new object[] {
            "Public Key",
            "Private Key"});
            this.cmbEncryptionKey.Location = new System.Drawing.Point(1022, 59);
            this.cmbEncryptionKey.Name = "cmbEncryptionKey";
            this.cmbEncryptionKey.Size = new System.Drawing.Size(278, 27);
            this.cmbEncryptionKey.TabIndex = 4;
            // 
            // cmbHash
            // 
            this.cmbHash.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHash.FormattingEnabled = true;
            this.cmbHash.Items.AddRange(new object[] {
            "SHA1",
            "SHA256",
            "SHA384",
            "SHA512",
            "MD5"});
            this.cmbHash.Location = new System.Drawing.Point(739, 59);
            this.cmbHash.Name = "cmbHash";
            this.cmbHash.Size = new System.Drawing.Size(277, 27);
            this.cmbHash.TabIndex = 3;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(1145, 553);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(155, 101);
            this.btnClear.TabIndex = 25;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnHash
            // 
            this.btnHash.Location = new System.Drawing.Point(808, 553);
            this.btnHash.Name = "btnHash";
            this.btnHash.Size = new System.Drawing.Size(155, 50);
            this.btnHash.TabIndex = 17;
            this.btnHash.Text = "Hash";
            this.btnHash.UseVisualStyleBackColor = true;
            this.btnHash.Click += new System.EventHandler(this.btnHash_Click);
            // 
            // btnSaveKeys
            // 
            this.btnSaveKeys.Location = new System.Drawing.Point(969, 604);
            this.btnSaveKeys.Name = "btnSaveKeys";
            this.btnSaveKeys.Size = new System.Drawing.Size(155, 50);
            this.btnSaveKeys.TabIndex = 24;
            this.btnSaveKeys.Text = "Save Keys";
            this.btnSaveKeys.UseVisualStyleBackColor = true;
            this.btnSaveKeys.Click += new System.EventHandler(this.btnSaveKeys_Click);
            // 
            // btnLoadKeys
            // 
            this.btnLoadKeys.Location = new System.Drawing.Point(808, 604);
            this.btnLoadKeys.Name = "btnLoadKeys";
            this.btnLoadKeys.Size = new System.Drawing.Size(155, 50);
            this.btnLoadKeys.TabIndex = 23;
            this.btnLoadKeys.Text = "Load Keys";
            this.btnLoadKeys.UseVisualStyleBackColor = true;
            this.btnLoadKeys.Click += new System.EventHandler(this.btnLoadKeys_Click);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(969, 553);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(155, 50);
            this.btnLoadFile.TabIndex = 18;
            this.btnLoadFile.Text = "Load File";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // btnUseKeys
            // 
            this.btnUseKeys.Location = new System.Drawing.Point(647, 604);
            this.btnUseKeys.Name = "btnUseKeys";
            this.btnUseKeys.Size = new System.Drawing.Size(155, 50);
            this.btnUseKeys.TabIndex = 22;
            this.btnUseKeys.Text = "Use Keys";
            this.btnUseKeys.UseVisualStyleBackColor = true;
            this.btnUseKeys.Click += new System.EventHandler(this.btnUseKeys_Click);
            // 
            // btnVerify
            // 
            this.btnVerify.Location = new System.Drawing.Point(647, 553);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(155, 50);
            this.btnVerify.TabIndex = 16;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(325, 553);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(155, 50);
            this.btnDecrypt.TabIndex = 14;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(486, 604);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(155, 50);
            this.button6.TabIndex = 21;
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(325, 604);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(155, 50);
            this.button3.TabIndex = 20;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btnSendToHex
            // 
            this.btnSendToHex.Location = new System.Drawing.Point(164, 604);
            this.btnSendToHex.Name = "btnSendToHex";
            this.btnSendToHex.Size = new System.Drawing.Size(155, 50);
            this.btnSendToHex.TabIndex = 19;
            this.btnSendToHex.Text = "Bufffer > Hex";
            this.btnSendToHex.UseVisualStyleBackColor = true;
            this.btnSendToHex.Click += new System.EventHandler(this.btnSendToHex_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(164, 553);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(155, 50);
            this.btnEncrypt.TabIndex = 13;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnSign
            // 
            this.btnSign.Location = new System.Drawing.Point(486, 553);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(155, 50);
            this.btnSign.TabIndex = 15;
            this.btnSign.Text = "Sign";
            this.btnSign.UseVisualStyleBackColor = true;
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 414);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 19);
            this.label7.TabIndex = 1;
            this.label7.Text = "Output";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 367);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 19);
            this.label6.TabIndex = 1;
            this.label6.Text = "Data Hash";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 19);
            this.label8.TabIndex = 1;
            this.label8.Text = "Options";
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Location = new System.Drawing.Point(23, 230);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(41, 19);
            this.lblData.TabIndex = 1;
            this.lblData.Text = "Data";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 19);
            this.label4.TabIndex = 1;
            this.label4.Text = "Key Public/Private";
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(164, 406);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(1136, 112);
            this.txtOutput.TabIndex = 9;
            // 
            // txtHash
            // 
            this.txtHash.Location = new System.Drawing.Point(164, 353);
            this.txtHash.Multiline = true;
            this.txtHash.Name = "txtHash";
            this.txtHash.Size = new System.Drawing.Size(1136, 47);
            this.txtHash.TabIndex = 8;
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(164, 223);
            this.txtData.Multiline = true;
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(1136, 123);
            this.txtData.TabIndex = 7;
            // 
            // txtPrivateKey
            // 
            this.txtPrivateKey.Location = new System.Drawing.Point(740, 92);
            this.txtPrivateKey.Multiline = true;
            this.txtPrivateKey.Name = "txtPrivateKey";
            this.txtPrivateKey.Size = new System.Drawing.Size(560, 123);
            this.txtPrivateKey.TabIndex = 6;
            this.txtPrivateKey.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtPrivateKey_DragDrop);
            this.txtPrivateKey.DragOver += new System.Windows.Forms.DragEventHandler(this.txtPrivateKey_DragOver);
            // 
            // txtPublicKey
            // 
            this.txtPublicKey.AllowDrop = true;
            this.txtPublicKey.Location = new System.Drawing.Point(164, 92);
            this.txtPublicKey.Multiline = true;
            this.txtPublicKey.Name = "txtPublicKey";
            this.txtPublicKey.Size = new System.Drawing.Size(560, 123);
            this.txtPublicKey.TabIndex = 5;
            this.txtPublicKey.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtPublicKey_DragDrop);
            this.txtPublicKey.DragOver += new System.Windows.Forms.DragEventHandler(this.txtPublicKey_DragOver);
            // 
            // tabRSA
            // 
            this.tabRSA.BackColor = System.Drawing.Color.LightGray;
            this.tabRSA.Controls.Add(this.label32);
            this.tabRSA.Controls.Add(this.cmbRSAKeyLen);
            this.tabRSA.Controls.Add(this.btnImportPrivateKey);
            this.tabRSA.Controls.Add(this.btnImportPublicKey);
            this.tabRSA.Controls.Add(this.btnGenKeys);
            this.tabRSA.Controls.Add(this.chkReverse);
            this.tabRSA.Controls.Add(this.txtInverseQ);
            this.tabRSA.Controls.Add(this.txtDQ);
            this.tabRSA.Controls.Add(this.txtDP);
            this.tabRSA.Controls.Add(this.txtQ);
            this.tabRSA.Controls.Add(this.txtP);
            this.tabRSA.Controls.Add(this.txtN);
            this.tabRSA.Controls.Add(this.txtD);
            this.tabRSA.Controls.Add(this.txtE);
            this.tabRSA.Controls.Add(this.txtPEMPublicKey);
            this.tabRSA.Controls.Add(this.txtPEMPrivateKey);
            this.tabRSA.Controls.Add(this.btnUsePrivateKey);
            this.tabRSA.Controls.Add(this.btnExportPrivate);
            this.tabRSA.Controls.Add(this.btnUsePublicKey);
            this.tabRSA.Controls.Add(this.label18);
            this.tabRSA.Controls.Add(this.btnExportPublic);
            this.tabRSA.Controls.Add(this.label17);
            this.tabRSA.Controls.Add(this.label12);
            this.tabRSA.Controls.Add(this.label16);
            this.tabRSA.Controls.Add(this.label30);
            this.tabRSA.Controls.Add(this.label3);
            this.tabRSA.Controls.Add(this.label2);
            this.tabRSA.Controls.Add(this.label11);
            this.tabRSA.Controls.Add(this.label10);
            this.tabRSA.Controls.Add(this.label9);
            this.tabRSA.Controls.Add(this.label1);
            this.tabRSA.Location = new System.Drawing.Point(4, 28);
            this.tabRSA.Name = "tabRSA";
            this.tabRSA.Padding = new System.Windows.Forms.Padding(3);
            this.tabRSA.Size = new System.Drawing.Size(1331, 663);
            this.tabRSA.TabIndex = 0;
            this.tabRSA.Text = "RSA Keys";
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.SystemColors.Info;
            this.label32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label32.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(926, 172);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(179, 33);
            this.label32.TabIndex = 14;
            this.label32.Text = "Key Length";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbRSAKeyLen
            // 
            this.cmbRSAKeyLen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRSAKeyLen.FormattingEnabled = true;
            this.cmbRSAKeyLen.Items.AddRange(new object[] {
            "512",
            "1024",
            "2048",
            "4096"});
            this.cmbRSAKeyLen.Location = new System.Drawing.Point(1111, 175);
            this.cmbRSAKeyLen.Name = "cmbRSAKeyLen";
            this.cmbRSAKeyLen.Size = new System.Drawing.Size(179, 27);
            this.cmbRSAKeyLen.TabIndex = 4;
            // 
            // btnImportPrivateKey
            // 
            this.btnImportPrivateKey.Location = new System.Drawing.Point(1116, 593);
            this.btnImportPrivateKey.Name = "btnImportPrivateKey";
            this.btnImportPrivateKey.Size = new System.Drawing.Size(175, 50);
            this.btnImportPrivateKey.TabIndex = 18;
            this.btnImportPrivateKey.Text = "Import Private Key";
            this.btnImportPrivateKey.UseVisualStyleBackColor = true;
            this.btnImportPrivateKey.Click += new System.EventHandler(this.btnImportPrivateKey_Click);
            // 
            // btnImportPublicKey
            // 
            this.btnImportPublicKey.Location = new System.Drawing.Point(934, 593);
            this.btnImportPublicKey.Name = "btnImportPublicKey";
            this.btnImportPublicKey.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnImportPublicKey.Size = new System.Drawing.Size(175, 50);
            this.btnImportPublicKey.TabIndex = 17;
            this.btnImportPublicKey.Text = "Import Public Key";
            this.btnImportPublicKey.UseVisualStyleBackColor = true;
            this.btnImportPublicKey.Click += new System.EventHandler(this.btnImportPublicKey_Click);
            // 
            // btnGenKeys
            // 
            this.btnGenKeys.Location = new System.Drawing.Point(151, 593);
            this.btnGenKeys.Name = "btnGenKeys";
            this.btnGenKeys.Size = new System.Drawing.Size(290, 50);
            this.btnGenKeys.TabIndex = 12;
            this.btnGenKeys.Text = "Generate Keys";
            this.btnGenKeys.UseVisualStyleBackColor = true;
            this.btnGenKeys.Click += new System.EventHandler(this.btnGenKeys_Click);
            // 
            // chkReverse
            // 
            this.chkReverse.AutoSize = true;
            this.chkReverse.Location = new System.Drawing.Point(698, 179);
            this.chkReverse.Name = "chkReverse";
            this.chkReverse.Size = new System.Drawing.Size(90, 23);
            this.chkReverse.TabIndex = 3;
            this.chkReverse.Text = "Reverse";
            this.chkReverse.UseVisualStyleBackColor = true;
            // 
            // txtInverseQ
            // 
            this.txtInverseQ.Location = new System.Drawing.Point(151, 536);
            this.txtInverseQ.Multiline = true;
            this.txtInverseQ.Name = "txtInverseQ";
            this.txtInverseQ.Size = new System.Drawing.Size(1140, 45);
            this.txtInverseQ.TabIndex = 11;
            // 
            // txtDQ
            // 
            this.txtDQ.Location = new System.Drawing.Point(151, 486);
            this.txtDQ.Multiline = true;
            this.txtDQ.Name = "txtDQ";
            this.txtDQ.Size = new System.Drawing.Size(1140, 45);
            this.txtDQ.TabIndex = 10;
            // 
            // txtDP
            // 
            this.txtDP.Location = new System.Drawing.Point(151, 436);
            this.txtDP.Multiline = true;
            this.txtDP.Name = "txtDP";
            this.txtDP.Size = new System.Drawing.Size(1140, 45);
            this.txtDP.TabIndex = 9;
            // 
            // txtQ
            // 
            this.txtQ.Location = new System.Drawing.Point(151, 386);
            this.txtQ.Multiline = true;
            this.txtQ.Name = "txtQ";
            this.txtQ.Size = new System.Drawing.Size(1140, 45);
            this.txtQ.TabIndex = 8;
            // 
            // txtP
            // 
            this.txtP.Location = new System.Drawing.Point(151, 336);
            this.txtP.Multiline = true;
            this.txtP.Name = "txtP";
            this.txtP.Size = new System.Drawing.Size(1140, 45);
            this.txtP.TabIndex = 7;
            // 
            // txtN
            // 
            this.txtN.Location = new System.Drawing.Point(151, 261);
            this.txtN.Multiline = true;
            this.txtN.Name = "txtN";
            this.txtN.Size = new System.Drawing.Size(1140, 69);
            this.txtN.TabIndex = 6;
            // 
            // txtD
            // 
            this.txtD.Location = new System.Drawing.Point(151, 208);
            this.txtD.Multiline = true;
            this.txtD.Name = "txtD";
            this.txtD.Size = new System.Drawing.Size(1139, 47);
            this.txtD.TabIndex = 5;
            // 
            // txtE
            // 
            this.txtE.Location = new System.Drawing.Point(151, 175);
            this.txtE.Name = "txtE";
            this.txtE.Size = new System.Drawing.Size(221, 27);
            this.txtE.TabIndex = 2;
            this.txtE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPEMPublicKey
            // 
            this.txtPEMPublicKey.Location = new System.Drawing.Point(151, 26);
            this.txtPEMPublicKey.Multiline = true;
            this.txtPEMPublicKey.Name = "txtPEMPublicKey";
            this.txtPEMPublicKey.Size = new System.Drawing.Size(541, 143);
            this.txtPEMPublicKey.TabIndex = 0;
            // 
            // txtPEMPrivateKey
            // 
            this.txtPEMPrivateKey.Location = new System.Drawing.Point(698, 26);
            this.txtPEMPrivateKey.Multiline = true;
            this.txtPEMPrivateKey.Name = "txtPEMPrivateKey";
            this.txtPEMPrivateKey.Size = new System.Drawing.Size(593, 143);
            this.txtPEMPrivateKey.TabIndex = 1;
            // 
            // btnUsePrivateKey
            // 
            this.btnUsePrivateKey.Location = new System.Drawing.Point(854, 593);
            this.btnUsePrivateKey.Name = "btnUsePrivateKey";
            this.btnUsePrivateKey.Size = new System.Drawing.Size(65, 50);
            this.btnUsePrivateKey.TabIndex = 16;
            this.btnUsePrivateKey.Text = "Use";
            this.btnUsePrivateKey.UseVisualStyleBackColor = true;
            this.btnUsePrivateKey.Click += new System.EventHandler(this.btnUsePrivateKey_Click);
            // 
            // btnExportPrivate
            // 
            this.btnExportPrivate.Location = new System.Drawing.Point(687, 593);
            this.btnExportPrivate.Name = "btnExportPrivate";
            this.btnExportPrivate.Size = new System.Drawing.Size(175, 50);
            this.btnExportPrivate.TabIndex = 15;
            this.btnExportPrivate.Text = "Export Private Key";
            this.btnExportPrivate.UseVisualStyleBackColor = true;
            this.btnExportPrivate.Click += new System.EventHandler(this.btnExportPrivate_Click);
            // 
            // btnUsePublicKey
            // 
            this.btnUsePublicKey.Location = new System.Drawing.Point(618, 593);
            this.btnUsePublicKey.Name = "btnUsePublicKey";
            this.btnUsePublicKey.Size = new System.Drawing.Size(65, 50);
            this.btnUsePublicKey.TabIndex = 14;
            this.btnUsePublicKey.Text = "Use";
            this.btnUsePublicKey.UseVisualStyleBackColor = true;
            this.btnUsePublicKey.Click += new System.EventHandler(this.btnUsePublicKey_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(25, 547);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(78, 19);
            this.label18.TabIndex = 14;
            this.label18.Text = "Inverse Q";
            // 
            // btnExportPublic
            // 
            this.btnExportPublic.Location = new System.Drawing.Point(454, 593);
            this.btnExportPublic.Name = "btnExportPublic";
            this.btnExportPublic.Size = new System.Drawing.Size(175, 50);
            this.btnExportPublic.TabIndex = 13;
            this.btnExportPublic.Text = "Export Public Key";
            this.btnExportPublic.UseVisualStyleBackColor = true;
            this.btnExportPublic.Click += new System.EventHandler(this.btnExportPublic_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(25, 497);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(32, 19);
            this.label17.TabIndex = 13;
            this.label17.Text = "DQ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(398, 180);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 19);
            this.label12.TabIndex = 2;
            this.label12.Text = "( Hex )";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(26, 447);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 19);
            this.label16.TabIndex = 12;
            this.label16.Text = "DP";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(930, 3);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(92, 19);
            this.label30.TabIndex = 2;
            this.label30.Text = "Private PEM";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(355, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Public PEM";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 264);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Modulus (N)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 398);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 19);
            this.label11.TabIndex = 2;
            this.label11.Text = "Prime (Q)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 349);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 19);
            this.label10.TabIndex = 2;
            this.label10.Text = "Prime (P)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 225);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 19);
            this.label9.TabIndex = 2;
            this.label9.Text = "Exponent (D)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Exponent (E)";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabRSA);
            this.tabControl1.Controls.Add(this.tabDSA);
            this.tabControl1.Controls.Add(this.tabCrypto);
            this.tabControl1.Controls.Add(this.tabCalculator);
            this.tabControl1.Controls.Add(this.tabEncoding);
            this.tabControl1.Controls.Add(this.tabQuran);
            this.tabControl1.Controls.Add(this.tabCharset);
            this.tabControl1.Controls.Add(this.tabHexViewer);
            this.tabControl1.Controls.Add(this.tabXRay);
            this.tabControl1.Controls.Add(this.tabSpectrum);
            this.tabControl1.Controls.Add(this.tabColor);
            this.tabControl1.Location = new System.Drawing.Point(19, 31);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1339, 695);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabDSA
            // 
            this.tabDSA.BackColor = System.Drawing.Color.LightGray;
            this.tabDSA.Controls.Add(this.cmbDSAKeyLen);
            this.tabDSA.Controls.Add(this.btnDSAImportPrivate);
            this.tabDSA.Controls.Add(this.btnDSAImportPublic);
            this.tabDSA.Controls.Add(this.btnGenDSAKeys);
            this.tabDSA.Controls.Add(this.btnExportDSAPublic);
            this.tabDSA.Controls.Add(this.btnExportDSA);
            this.tabDSA.Controls.Add(this.txtDSA_X);
            this.tabDSA.Controls.Add(this.txtDSA_Y);
            this.tabDSA.Controls.Add(this.txtDSA_G);
            this.tabDSA.Controls.Add(this.txtDSA_Q);
            this.tabDSA.Controls.Add(this.txtDSA_P);
            this.tabDSA.Controls.Add(this.label23);
            this.tabDSA.Controls.Add(this.label24);
            this.tabDSA.Controls.Add(this.label25);
            this.tabDSA.Controls.Add(this.label26);
            this.tabDSA.Controls.Add(this.label28);
            this.tabDSA.Controls.Add(this.label33);
            this.tabDSA.Controls.Add(this.label29);
            this.tabDSA.Controls.Add(this.label27);
            this.tabDSA.Controls.Add(this.txtDSAPEMPrivate);
            this.tabDSA.Controls.Add(this.txtDSAPEMPublic);
            this.tabDSA.Location = new System.Drawing.Point(4, 28);
            this.tabDSA.Name = "tabDSA";
            this.tabDSA.Size = new System.Drawing.Size(1331, 663);
            this.tabDSA.TabIndex = 3;
            this.tabDSA.Text = "DSA Keys";
            // 
            // cmbDSAKeyLen
            // 
            this.cmbDSAKeyLen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDSAKeyLen.FormattingEnabled = true;
            this.cmbDSAKeyLen.Items.AddRange(new object[] {
            "512",
            "1024",
            "2048",
            "4096"});
            this.cmbDSAKeyLen.Location = new System.Drawing.Point(154, 205);
            this.cmbDSAKeyLen.Name = "cmbDSAKeyLen";
            this.cmbDSAKeyLen.Size = new System.Drawing.Size(531, 27);
            this.cmbDSAKeyLen.TabIndex = 2;
            // 
            // btnDSAImportPrivate
            // 
            this.btnDSAImportPrivate.Location = new System.Drawing.Point(1094, 596);
            this.btnDSAImportPrivate.Name = "btnDSAImportPrivate";
            this.btnDSAImportPrivate.Size = new System.Drawing.Size(200, 56);
            this.btnDSAImportPrivate.TabIndex = 12;
            this.btnDSAImportPrivate.Text = "Import Private";
            this.btnDSAImportPrivate.UseVisualStyleBackColor = true;
            this.btnDSAImportPrivate.Click += new System.EventHandler(this.btnDSAImportPrivate_Click);
            // 
            // btnDSAImportPublic
            // 
            this.btnDSAImportPublic.Location = new System.Drawing.Point(888, 596);
            this.btnDSAImportPublic.Name = "btnDSAImportPublic";
            this.btnDSAImportPublic.Size = new System.Drawing.Size(200, 56);
            this.btnDSAImportPublic.TabIndex = 11;
            this.btnDSAImportPublic.Text = "Import Public";
            this.btnDSAImportPublic.UseVisualStyleBackColor = true;
            this.btnDSAImportPublic.Click += new System.EventHandler(this.btnDSAImportPublic_Click);
            // 
            // btnGenDSAKeys
            // 
            this.btnGenDSAKeys.Location = new System.Drawing.Point(154, 596);
            this.btnGenDSAKeys.Name = "btnGenDSAKeys";
            this.btnGenDSAKeys.Size = new System.Drawing.Size(316, 56);
            this.btnGenDSAKeys.TabIndex = 8;
            this.btnGenDSAKeys.Text = "Generate Keys";
            this.btnGenDSAKeys.UseVisualStyleBackColor = true;
            this.btnGenDSAKeys.Click += new System.EventHandler(this.btnGenDSAKeys_Click);
            // 
            // btnExportDSAPublic
            // 
            this.btnExportDSAPublic.Location = new System.Drawing.Point(682, 596);
            this.btnExportDSAPublic.Name = "btnExportDSAPublic";
            this.btnExportDSAPublic.Size = new System.Drawing.Size(200, 56);
            this.btnExportDSAPublic.TabIndex = 10;
            this.btnExportDSAPublic.Text = "Export Public Key";
            this.btnExportDSAPublic.UseVisualStyleBackColor = true;
            this.btnExportDSAPublic.Click += new System.EventHandler(this.btnExportDSAPublic_Click);
            // 
            // btnExportDSA
            // 
            this.btnExportDSA.Location = new System.Drawing.Point(476, 596);
            this.btnExportDSA.Name = "btnExportDSA";
            this.btnExportDSA.Size = new System.Drawing.Size(200, 56);
            this.btnExportDSA.TabIndex = 9;
            this.btnExportDSA.Text = "Export Private Key";
            this.btnExportDSA.UseVisualStyleBackColor = true;
            this.btnExportDSA.Click += new System.EventHandler(this.btnExportDSA_Click);
            // 
            // txtDSA_X
            // 
            this.txtDSA_X.Location = new System.Drawing.Point(154, 525);
            this.txtDSA_X.Multiline = true;
            this.txtDSA_X.Name = "txtDSA_X";
            this.txtDSA_X.Size = new System.Drawing.Size(1140, 65);
            this.txtDSA_X.TabIndex = 7;
            // 
            // txtDSA_Y
            // 
            this.txtDSA_Y.Location = new System.Drawing.Point(154, 454);
            this.txtDSA_Y.Multiline = true;
            this.txtDSA_Y.Name = "txtDSA_Y";
            this.txtDSA_Y.Size = new System.Drawing.Size(1140, 65);
            this.txtDSA_Y.TabIndex = 6;
            // 
            // txtDSA_G
            // 
            this.txtDSA_G.Location = new System.Drawing.Point(154, 383);
            this.txtDSA_G.Multiline = true;
            this.txtDSA_G.Name = "txtDSA_G";
            this.txtDSA_G.Size = new System.Drawing.Size(1140, 65);
            this.txtDSA_G.TabIndex = 5;
            // 
            // txtDSA_Q
            // 
            this.txtDSA_Q.Location = new System.Drawing.Point(154, 312);
            this.txtDSA_Q.Multiline = true;
            this.txtDSA_Q.Name = "txtDSA_Q";
            this.txtDSA_Q.Size = new System.Drawing.Size(1140, 65);
            this.txtDSA_Q.TabIndex = 4;
            // 
            // txtDSA_P
            // 
            this.txtDSA_P.Location = new System.Drawing.Point(154, 241);
            this.txtDSA_P.Multiline = true;
            this.txtDSA_P.Name = "txtDSA_P";
            this.txtDSA_P.Size = new System.Drawing.Size(1140, 65);
            this.txtDSA_P.TabIndex = 3;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(31, 543);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(108, 19);
            this.label23.TabIndex = 9;
            this.label23.Text = "X (PrivateKey)";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(32, 474);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(103, 19);
            this.label24.TabIndex = 10;
            this.label24.Text = "Y (PublicKey)";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(32, 403);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(20, 19);
            this.label25.TabIndex = 11;
            this.label25.Text = "G";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(32, 326);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(67, 19);
            this.label26.TabIndex = 12;
            this.label26.Text = "Prime.Q";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(28, 51);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(39, 19);
            this.label28.TabIndex = 13;
            this.label28.Text = "PEM";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(31, 208);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(87, 19);
            this.label33.TabIndex = 13;
            this.label33.Text = "Key Length";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(29, 277);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(80, 19);
            this.label29.TabIndex = 13;
            this.label29.Text = "(Modulus)";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(32, 255);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(64, 19);
            this.label27.TabIndex = 13;
            this.label27.Text = "Prime.P";
            // 
            // txtDSAPEMPrivate
            // 
            this.txtDSAPEMPrivate.AllowDrop = true;
            this.txtDSAPEMPrivate.Location = new System.Drawing.Point(712, 37);
            this.txtDSAPEMPrivate.Multiline = true;
            this.txtDSAPEMPrivate.Name = "txtDSAPEMPrivate";
            this.txtDSAPEMPrivate.Size = new System.Drawing.Size(582, 195);
            this.txtDSAPEMPrivate.TabIndex = 1;
            this.txtDSAPEMPrivate.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtDSAPEM_DragDrop);
            this.txtDSAPEMPrivate.DragOver += new System.Windows.Forms.DragEventHandler(this.txtDSAPEM_DragOver);
            // 
            // txtDSAPEMPublic
            // 
            this.txtDSAPEMPublic.AllowDrop = true;
            this.txtDSAPEMPublic.Location = new System.Drawing.Point(154, 37);
            this.txtDSAPEMPublic.Multiline = true;
            this.txtDSAPEMPublic.Name = "txtDSAPEMPublic";
            this.txtDSAPEMPublic.Size = new System.Drawing.Size(531, 159);
            this.txtDSAPEMPublic.TabIndex = 0;
            this.txtDSAPEMPublic.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtDSAPEM_DragDrop);
            this.txtDSAPEMPublic.DragOver += new System.Windows.Forms.DragEventHandler(this.txtDSAPEM_DragOver);
            // 
            // tabCalculator
            // 
            this.tabCalculator.BackColor = System.Drawing.Color.LightGray;
            this.tabCalculator.Controls.Add(this.chkPositives);
            this.tabCalculator.Controls.Add(this.rb64b);
            this.tabCalculator.Controls.Add(this.rb16);
            this.tabCalculator.Controls.Add(this.rb2);
            this.tabCalculator.Controls.Add(this.rb10);
            this.tabCalculator.Controls.Add(this.label43);
            this.tabCalculator.Controls.Add(this.label42);
            this.tabCalculator.Controls.Add(this.label41);
            this.tabCalculator.Controls.Add(this.cmbKeyLen);
            this.tabCalculator.Controls.Add(this.label40);
            this.tabCalculator.Controls.Add(this.cmbExponentMethod);
            this.tabCalculator.Controls.Add(this.label39);
            this.tabCalculator.Controls.Add(this.cmbMULMethod);
            this.tabCalculator.Controls.Add(this.label36);
            this.tabCalculator.Controls.Add(this.cmbAccelerator);
            this.tabCalculator.Controls.Add(this.btnRTP);
            this.tabCalculator.Controls.Add(this.btnFCD);
            this.tabCalculator.Controls.Add(this.btnLCM);
            this.tabCalculator.Controls.Add(this.btnPWM);
            this.tabCalculator.Controls.Add(this.btnPOW);
            this.tabCalculator.Controls.Add(this.btnMOD);
            this.tabCalculator.Controls.Add(this.btnDIV);
            this.tabCalculator.Controls.Add(this.btnMUL);
            this.tabCalculator.Controls.Add(this.btnSUB);
            this.tabCalculator.Controls.Add(this.btnClearCalc);
            this.tabCalculator.Controls.Add(this.btnGenPrime);
            this.tabCalculator.Controls.Add(this.btnXOR);
            this.tabCalculator.Controls.Add(this.btnOR);
            this.tabCalculator.Controls.Add(this.btnSHR);
            this.tabCalculator.Controls.Add(this.btnSHL);
            this.tabCalculator.Controls.Add(this.btnBezout);
            this.tabCalculator.Controls.Add(this.button5);
            this.tabCalculator.Controls.Add(this.button4);
            this.tabCalculator.Controls.Add(this.button2);
            this.tabCalculator.Controls.Add(this.button1);
            this.tabCalculator.Controls.Add(this.btnSqrt);
            this.tabCalculator.Controls.Add(this.btnFactorizeQ);
            this.tabCalculator.Controls.Add(this.btnFactorizeP);
            this.tabCalculator.Controls.Add(this.btnFactorDbQ);
            this.tabCalculator.Controls.Add(this.btnFactorDbP);
            this.tabCalculator.Controls.Add(this.btnReverseQ);
            this.tabCalculator.Controls.Add(this.btnReverseP);
            this.tabCalculator.Controls.Add(this.btnIsEven);
            this.tabCalculator.Controls.Add(this.btnIsPrime);
            this.tabCalculator.Controls.Add(this.btnADD);
            this.tabCalculator.Controls.Add(this.label22);
            this.tabCalculator.Controls.Add(this.label21);
            this.tabCalculator.Controls.Add(this.label20);
            this.tabCalculator.Controls.Add(this.label19);
            this.tabCalculator.Controls.Add(this.txtResultR);
            this.tabCalculator.Controls.Add(this.txtModulN);
            this.tabCalculator.Controls.Add(this.txtPrimeQ);
            this.tabCalculator.Controls.Add(this.txtPrimeP);
            this.tabCalculator.Location = new System.Drawing.Point(4, 28);
            this.tabCalculator.Name = "tabCalculator";
            this.tabCalculator.Size = new System.Drawing.Size(1331, 663);
            this.tabCalculator.TabIndex = 2;
            this.tabCalculator.Text = "Calculator";
            // 
            // chkPositives
            // 
            this.chkPositives.AutoSize = true;
            this.chkPositives.Checked = true;
            this.chkPositives.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPositives.Location = new System.Drawing.Point(1163, 249);
            this.chkPositives.Name = "chkPositives";
            this.chkPositives.Size = new System.Drawing.Size(130, 23);
            this.chkPositives.TabIndex = 10;
            this.chkPositives.Text = "Postives Only";
            this.chkPositives.UseVisualStyleBackColor = true;
            // 
            // rb64b
            // 
            this.rb64b.AutoSize = true;
            this.rb64b.Location = new System.Drawing.Point(1073, 248);
            this.rb64b.Name = "rb64b";
            this.rb64b.Size = new System.Drawing.Size(84, 23);
            this.rb64b.TabIndex = 9;
            this.rb64b.TabStop = true;
            this.rb64b.Text = "Base64";
            this.rb64b.UseVisualStyleBackColor = true;
            this.rb64b.CheckedChanged += new System.EventHandler(this.rbNumberBaseCheckedChanged);
            // 
            // rb16
            // 
            this.rb16.AutoSize = true;
            this.rb16.Location = new System.Drawing.Point(1013, 248);
            this.rb16.Name = "rb16";
            this.rb16.Size = new System.Drawing.Size(52, 23);
            this.rb16.TabIndex = 8;
            this.rb16.TabStop = true;
            this.rb16.Text = "16";
            this.rb16.UseVisualStyleBackColor = true;
            this.rb16.CheckedChanged += new System.EventHandler(this.rbNumberBaseCheckedChanged);
            // 
            // rb2
            // 
            this.rb2.AutoSize = true;
            this.rb2.Location = new System.Drawing.Point(906, 248);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(43, 23);
            this.rb2.TabIndex = 6;
            this.rb2.TabStop = true;
            this.rb2.Text = "2";
            this.rb2.UseVisualStyleBackColor = true;
            this.rb2.CheckedChanged += new System.EventHandler(this.rbNumberBaseCheckedChanged);
            // 
            // rb10
            // 
            this.rb10.AutoSize = true;
            this.rb10.Location = new System.Drawing.Point(954, 248);
            this.rb10.Name = "rb10";
            this.rb10.Size = new System.Drawing.Size(52, 23);
            this.rb10.TabIndex = 7;
            this.rb10.TabStop = true;
            this.rb10.Text = "10";
            this.rb10.UseVisualStyleBackColor = true;
            this.rb10.CheckedChanged += new System.EventHandler(this.rbNumberBaseCheckedChanged);
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.SystemColors.Info;
            this.label43.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label43.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(1163, 212);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(138, 33);
            this.label43.TabIndex = 8;
            this.label43.Text = "Key Length";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.SystemColors.Info;
            this.label42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label42.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(1163, 150);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(138, 33);
            this.label42.TabIndex = 8;
            this.label42.Text = "Key Length";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.SystemColors.Info;
            this.label41.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label41.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(906, 212);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(251, 33);
            this.label41.TabIndex = 8;
            this.label41.Text = "Number Base";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbKeyLen
            // 
            this.cmbKeyLen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKeyLen.FormattingEnabled = true;
            this.cmbKeyLen.Items.AddRange(new object[] {
            "512",
            "1024",
            "2048",
            "4096"});
            this.cmbKeyLen.Location = new System.Drawing.Point(1163, 182);
            this.cmbKeyLen.Name = "cmbKeyLen";
            this.cmbKeyLen.Size = new System.Drawing.Size(138, 27);
            this.cmbKeyLen.TabIndex = 5;
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.SystemColors.Info;
            this.label40.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label40.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(906, 150);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(251, 33);
            this.label40.TabIndex = 8;
            this.label40.Text = "Exponentiation Method";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbExponentMethod
            // 
            this.cmbExponentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExponentMethod.FormattingEnabled = true;
            this.cmbExponentMethod.Items.AddRange(new object[] {
            "Regular",
            "Squaring",
            "Fast"});
            this.cmbExponentMethod.Location = new System.Drawing.Point(906, 182);
            this.cmbExponentMethod.Name = "cmbExponentMethod";
            this.cmbExponentMethod.Size = new System.Drawing.Size(251, 27);
            this.cmbExponentMethod.TabIndex = 4;
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.SystemColors.Info;
            this.label39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label39.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(906, 88);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(395, 33);
            this.label39.TabIndex = 8;
            this.label39.Text = "Multiplaction Method";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbMULMethod
            // 
            this.cmbMULMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMULMethod.FormattingEnabled = true;
            this.cmbMULMethod.Items.AddRange(new object[] {
            "Regular",
            "Karatsuba"});
            this.cmbMULMethod.Location = new System.Drawing.Point(906, 120);
            this.cmbMULMethod.Name = "cmbMULMethod";
            this.cmbMULMethod.Size = new System.Drawing.Size(395, 27);
            this.cmbMULMethod.TabIndex = 3;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.SystemColors.Info;
            this.label36.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label36.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(906, 26);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(395, 33);
            this.label36.TabIndex = 8;
            this.label36.Text = "Accelerator";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbAccelerator
            // 
            this.cmbAccelerator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccelerator.FormattingEnabled = true;
            this.cmbAccelerator.Location = new System.Drawing.Point(906, 58);
            this.cmbAccelerator.Name = "cmbAccelerator";
            this.cmbAccelerator.Size = new System.Drawing.Size(395, 27);
            this.cmbAccelerator.TabIndex = 2;
            this.cmbAccelerator.SelectedIndexChanged += new System.EventHandler(this.cmbAccelerator_SelectedIndexChanged);
            // 
            // btnRTP
            // 
            this.btnRTP.Location = new System.Drawing.Point(1191, 531);
            this.btnRTP.Name = "btnRTP";
            this.btnRTP.Size = new System.Drawing.Size(110, 40);
            this.btnRTP.TabIndex = 22;
            this.btnRTP.Text = "R -> P";
            this.btnRTP.UseVisualStyleBackColor = true;
            this.btnRTP.Click += new System.EventHandler(this.btnRTP_Click);
            // 
            // btnFCD
            // 
            this.btnFCD.Location = new System.Drawing.Point(1075, 531);
            this.btnFCD.Name = "btnFCD";
            this.btnFCD.Size = new System.Drawing.Size(110, 40);
            this.btnFCD.TabIndex = 21;
            this.btnFCD.Text = "GCD (P,Q)";
            this.btnFCD.UseVisualStyleBackColor = true;
            this.btnFCD.Click += new System.EventHandler(this.btnFCD_Click);
            // 
            // btnLCM
            // 
            this.btnLCM.Location = new System.Drawing.Point(959, 531);
            this.btnLCM.Name = "btnLCM";
            this.btnLCM.Size = new System.Drawing.Size(110, 40);
            this.btnLCM.TabIndex = 20;
            this.btnLCM.Text = "LCM (P,Q)";
            this.btnLCM.UseVisualStyleBackColor = true;
            this.btnLCM.Click += new System.EventHandler(this.btnLCM_Click);
            // 
            // btnPWM
            // 
            this.btnPWM.Location = new System.Drawing.Point(843, 531);
            this.btnPWM.Name = "btnPWM";
            this.btnPWM.Size = new System.Drawing.Size(110, 40);
            this.btnPWM.TabIndex = 19;
            this.btnPWM.Text = "P ^ Q % N";
            this.btnPWM.UseVisualStyleBackColor = true;
            this.btnPWM.Click += new System.EventHandler(this.btnPWM_Click);
            // 
            // btnPOW
            // 
            this.btnPOW.Location = new System.Drawing.Point(727, 531);
            this.btnPOW.Name = "btnPOW";
            this.btnPOW.Size = new System.Drawing.Size(110, 40);
            this.btnPOW.TabIndex = 18;
            this.btnPOW.Text = "P ^ Q";
            this.btnPOW.UseVisualStyleBackColor = true;
            this.btnPOW.Click += new System.EventHandler(this.btnPOW_Click);
            // 
            // btnMOD
            // 
            this.btnMOD.Location = new System.Drawing.Point(611, 531);
            this.btnMOD.Name = "btnMOD";
            this.btnMOD.Size = new System.Drawing.Size(110, 40);
            this.btnMOD.TabIndex = 17;
            this.btnMOD.Text = "P % Q";
            this.btnMOD.UseVisualStyleBackColor = true;
            this.btnMOD.Click += new System.EventHandler(this.btnMOD_Click);
            // 
            // btnDIV
            // 
            this.btnDIV.Location = new System.Drawing.Point(495, 531);
            this.btnDIV.Name = "btnDIV";
            this.btnDIV.Size = new System.Drawing.Size(110, 40);
            this.btnDIV.TabIndex = 16;
            this.btnDIV.Text = "P / Q";
            this.btnDIV.UseVisualStyleBackColor = true;
            this.btnDIV.Click += new System.EventHandler(this.btnDIV_Click);
            // 
            // btnMUL
            // 
            this.btnMUL.Location = new System.Drawing.Point(379, 531);
            this.btnMUL.Name = "btnMUL";
            this.btnMUL.Size = new System.Drawing.Size(110, 40);
            this.btnMUL.TabIndex = 15;
            this.btnMUL.Text = "P * Q";
            this.btnMUL.UseVisualStyleBackColor = true;
            this.btnMUL.Click += new System.EventHandler(this.btnMUL_Click);
            // 
            // btnSUB
            // 
            this.btnSUB.Location = new System.Drawing.Point(263, 531);
            this.btnSUB.Name = "btnSUB";
            this.btnSUB.Size = new System.Drawing.Size(110, 40);
            this.btnSUB.TabIndex = 14;
            this.btnSUB.Text = "P - Q";
            this.btnSUB.UseVisualStyleBackColor = true;
            this.btnSUB.Click += new System.EventHandler(this.btnSUB_Click);
            // 
            // btnClearCalc
            // 
            this.btnClearCalc.Location = new System.Drawing.Point(1191, 617);
            this.btnClearCalc.Name = "btnClearCalc";
            this.btnClearCalc.Size = new System.Drawing.Size(110, 40);
            this.btnClearCalc.TabIndex = 42;
            this.btnClearCalc.Text = "Clear";
            this.btnClearCalc.UseVisualStyleBackColor = true;
            this.btnClearCalc.Click += new System.EventHandler(this.btnClearCalc_Click);
            // 
            // btnGenPrime
            // 
            this.btnGenPrime.Location = new System.Drawing.Point(611, 574);
            this.btnGenPrime.Name = "btnGenPrime";
            this.btnGenPrime.Size = new System.Drawing.Size(110, 40);
            this.btnGenPrime.TabIndex = 27;
            this.btnGenPrime.Text = "Gen Primes";
            this.btnGenPrime.UseVisualStyleBackColor = true;
            this.btnGenPrime.Click += new System.EventHandler(this.btnGenPrime_Click);
            // 
            // btnXOR
            // 
            this.btnXOR.Location = new System.Drawing.Point(1191, 574);
            this.btnXOR.Name = "btnXOR";
            this.btnXOR.Size = new System.Drawing.Size(110, 40);
            this.btnXOR.TabIndex = 32;
            this.btnXOR.Text = "P  xor Q";
            this.btnXOR.UseVisualStyleBackColor = true;
            this.btnXOR.Click += new System.EventHandler(this.btnXOR_Click);
            // 
            // btnOR
            // 
            this.btnOR.Location = new System.Drawing.Point(1075, 574);
            this.btnOR.Name = "btnOR";
            this.btnOR.Size = new System.Drawing.Size(110, 40);
            this.btnOR.TabIndex = 31;
            this.btnOR.Text = "P  or Q";
            this.btnOR.UseVisualStyleBackColor = true;
            this.btnOR.Click += new System.EventHandler(this.btnOR_Click);
            // 
            // btnSHR
            // 
            this.btnSHR.Location = new System.Drawing.Point(959, 574);
            this.btnSHR.Name = "btnSHR";
            this.btnSHR.Size = new System.Drawing.Size(110, 40);
            this.btnSHR.TabIndex = 30;
            this.btnSHR.Text = "P  >>";
            this.btnSHR.UseVisualStyleBackColor = true;
            this.btnSHR.Click += new System.EventHandler(this.btnSHR_Click);
            // 
            // btnSHL
            // 
            this.btnSHL.Location = new System.Drawing.Point(843, 574);
            this.btnSHL.Name = "btnSHL";
            this.btnSHL.Size = new System.Drawing.Size(110, 40);
            this.btnSHL.TabIndex = 29;
            this.btnSHL.Text = "<< P";
            this.btnSHL.UseVisualStyleBackColor = true;
            this.btnSHL.Click += new System.EventHandler(this.btnSHL_Click);
            // 
            // btnBezout
            // 
            this.btnBezout.Location = new System.Drawing.Point(727, 574);
            this.btnBezout.Name = "btnBezout";
            this.btnBezout.Size = new System.Drawing.Size(110, 40);
            this.btnBezout.TabIndex = 28;
            this.btnBezout.Text = "Bezout P,Q";
            this.btnBezout.UseVisualStyleBackColor = true;
            this.btnBezout.Click += new System.EventHandler(this.btnBezout_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1075, 617);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(110, 40);
            this.button5.TabIndex = 41;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(959, 617);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(110, 40);
            this.button4.TabIndex = 40;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(843, 617);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 40);
            this.button2.TabIndex = 39;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(727, 617);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 40);
            this.button1.TabIndex = 38;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnSqrt
            // 
            this.btnSqrt.Location = new System.Drawing.Point(611, 617);
            this.btnSqrt.Name = "btnSqrt";
            this.btnSqrt.Size = new System.Drawing.Size(110, 40);
            this.btnSqrt.TabIndex = 37;
            this.btnSqrt.Text = "SQRT P";
            this.btnSqrt.UseVisualStyleBackColor = true;
            this.btnSqrt.Click += new System.EventHandler(this.btnSqrt_Click);
            // 
            // btnFactorizeQ
            // 
            this.btnFactorizeQ.Location = new System.Drawing.Point(495, 574);
            this.btnFactorizeQ.Name = "btnFactorizeQ";
            this.btnFactorizeQ.Size = new System.Drawing.Size(110, 40);
            this.btnFactorizeQ.TabIndex = 26;
            this.btnFactorizeQ.Text = "Factorize Q";
            this.btnFactorizeQ.UseVisualStyleBackColor = true;
            this.btnFactorizeQ.Click += new System.EventHandler(this.btnFactorizeQ_Click);
            // 
            // btnFactorizeP
            // 
            this.btnFactorizeP.Location = new System.Drawing.Point(379, 574);
            this.btnFactorizeP.Name = "btnFactorizeP";
            this.btnFactorizeP.Size = new System.Drawing.Size(110, 40);
            this.btnFactorizeP.TabIndex = 25;
            this.btnFactorizeP.Text = "Factorize P";
            this.btnFactorizeP.UseVisualStyleBackColor = true;
            this.btnFactorizeP.Click += new System.EventHandler(this.btnFactorizeP_Click);
            // 
            // btnFactorDbQ
            // 
            this.btnFactorDbQ.Location = new System.Drawing.Point(495, 617);
            this.btnFactorDbQ.Name = "btnFactorDbQ";
            this.btnFactorDbQ.Size = new System.Drawing.Size(110, 40);
            this.btnFactorDbQ.TabIndex = 36;
            this.btnFactorDbQ.Text = "Factordb Q";
            this.btnFactorDbQ.UseVisualStyleBackColor = true;
            this.btnFactorDbQ.Click += new System.EventHandler(this.btnFactorDbQ_Click);
            // 
            // btnFactorDbP
            // 
            this.btnFactorDbP.Location = new System.Drawing.Point(379, 617);
            this.btnFactorDbP.Name = "btnFactorDbP";
            this.btnFactorDbP.Size = new System.Drawing.Size(110, 40);
            this.btnFactorDbP.TabIndex = 35;
            this.btnFactorDbP.Text = "Factordb P";
            this.btnFactorDbP.UseVisualStyleBackColor = true;
            this.btnFactorDbP.Click += new System.EventHandler(this.btnFactorDbP_Click);
            // 
            // btnReverseQ
            // 
            this.btnReverseQ.Location = new System.Drawing.Point(263, 617);
            this.btnReverseQ.Name = "btnReverseQ";
            this.btnReverseQ.Size = new System.Drawing.Size(110, 40);
            this.btnReverseQ.TabIndex = 34;
            this.btnReverseQ.Text = "Reverse Q";
            this.btnReverseQ.UseVisualStyleBackColor = true;
            this.btnReverseQ.Click += new System.EventHandler(this.btnReverseQ_Click);
            // 
            // btnReverseP
            // 
            this.btnReverseP.Location = new System.Drawing.Point(143, 617);
            this.btnReverseP.Name = "btnReverseP";
            this.btnReverseP.Size = new System.Drawing.Size(110, 40);
            this.btnReverseP.TabIndex = 33;
            this.btnReverseP.Text = "Reverse P";
            this.btnReverseP.UseVisualStyleBackColor = true;
            this.btnReverseP.Click += new System.EventHandler(this.btnReverseP_Click);
            // 
            // btnIsEven
            // 
            this.btnIsEven.Location = new System.Drawing.Point(263, 574);
            this.btnIsEven.Name = "btnIsEven";
            this.btnIsEven.Size = new System.Drawing.Size(110, 40);
            this.btnIsEven.TabIndex = 24;
            this.btnIsEven.Text = "IsEven";
            this.btnIsEven.UseVisualStyleBackColor = true;
            this.btnIsEven.Click += new System.EventHandler(this.btnIsEven_Click);
            // 
            // btnIsPrime
            // 
            this.btnIsPrime.Location = new System.Drawing.Point(143, 574);
            this.btnIsPrime.Name = "btnIsPrime";
            this.btnIsPrime.Size = new System.Drawing.Size(110, 40);
            this.btnIsPrime.TabIndex = 23;
            this.btnIsPrime.Text = "IsPrime";
            this.btnIsPrime.UseVisualStyleBackColor = true;
            this.btnIsPrime.Click += new System.EventHandler(this.btnIsPrime_Click);
            // 
            // btnADD
            // 
            this.btnADD.Location = new System.Drawing.Point(143, 531);
            this.btnADD.Name = "btnADD";
            this.btnADD.Size = new System.Drawing.Size(110, 40);
            this.btnADD.TabIndex = 13;
            this.btnADD.Text = "P + Q";
            this.btnADD.UseVisualStyleBackColor = true;
            this.btnADD.Click += new System.EventHandler(this.btnADD_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(35, 407);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(73, 19);
            this.label22.TabIndex = 1;
            this.label22.Text = "Result R:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(35, 280);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(74, 19);
            this.label21.TabIndex = 1;
            this.label21.Text = "Modul N:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(35, 153);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(73, 19);
            this.label20.TabIndex = 1;
            this.label20.Text = "Prime Q:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(35, 26);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(70, 19);
            this.label19.TabIndex = 1;
            this.label19.Text = "Prime P:";
            // 
            // txtResultR
            // 
            this.txtResultR.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtResultR.ForeColor = System.Drawing.SystemColors.Window;
            this.txtResultR.Location = new System.Drawing.Point(143, 404);
            this.txtResultR.Multiline = true;
            this.txtResultR.Name = "txtResultR";
            this.txtResultR.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultR.Size = new System.Drawing.Size(1158, 121);
            this.txtResultR.TabIndex = 12;
            this.txtResultR.DoubleClick += new System.EventHandler(this.txtResultR_DoubleClick);
            // 
            // txtModulN
            // 
            this.txtModulN.AllowDrop = true;
            this.txtModulN.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtModulN.ForeColor = System.Drawing.SystemColors.Window;
            this.txtModulN.Location = new System.Drawing.Point(143, 277);
            this.txtModulN.Multiline = true;
            this.txtModulN.Name = "txtModulN";
            this.txtModulN.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtModulN.Size = new System.Drawing.Size(1158, 121);
            this.txtModulN.TabIndex = 11;
            this.txtModulN.TextChanged += new System.EventHandler(this.txtModulN_TextChanged);
            this.txtModulN.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtModulN_DragDrop);
            this.txtModulN.DragOver += new System.Windows.Forms.DragEventHandler(this.txtModulN_DragOver);
            this.txtModulN.DoubleClick += new System.EventHandler(this.txtModulN_DoubleClick);
            // 
            // txtPrimeQ
            // 
            this.txtPrimeQ.AllowDrop = true;
            this.txtPrimeQ.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtPrimeQ.ForeColor = System.Drawing.SystemColors.Window;
            this.txtPrimeQ.Location = new System.Drawing.Point(143, 150);
            this.txtPrimeQ.Multiline = true;
            this.txtPrimeQ.Name = "txtPrimeQ";
            this.txtPrimeQ.Size = new System.Drawing.Size(757, 121);
            this.txtPrimeQ.TabIndex = 1;
            this.txtPrimeQ.TextChanged += new System.EventHandler(this.txtPrimeQ_TextChanged);
            this.txtPrimeQ.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtPrimeQ_DragDrop);
            this.txtPrimeQ.DragOver += new System.Windows.Forms.DragEventHandler(this.txtPrimeQ_DragOver);
            this.txtPrimeQ.DoubleClick += new System.EventHandler(this.txtPrimeQ_DoubleClick);
            // 
            // txtPrimeP
            // 
            this.txtPrimeP.AllowDrop = true;
            this.txtPrimeP.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtPrimeP.ForeColor = System.Drawing.SystemColors.Window;
            this.txtPrimeP.Location = new System.Drawing.Point(143, 23);
            this.txtPrimeP.Multiline = true;
            this.txtPrimeP.Name = "txtPrimeP";
            this.txtPrimeP.Size = new System.Drawing.Size(757, 121);
            this.txtPrimeP.TabIndex = 0;
            this.txtPrimeP.TextChanged += new System.EventHandler(this.txtPrimeP_TextChanged);
            this.txtPrimeP.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtPrimeP_DragDrop);
            this.txtPrimeP.DragOver += new System.Windows.Forms.DragEventHandler(this.txtPrimeP_DragOver);
            this.txtPrimeP.DoubleClick += new System.EventHandler(this.txtPrimeP_DoubleClick);
            // 
            // tabEncoding
            // 
            this.tabEncoding.BackColor = System.Drawing.Color.LightGray;
            this.tabEncoding.Controls.Add(this.grpOptions);
            this.tabEncoding.Controls.Add(this.panel7);
            this.tabEncoding.Controls.Add(this.panel5);
            this.tabEncoding.Controls.Add(this.progFiles);
            this.tabEncoding.Controls.Add(this.btnToHex);
            this.tabEncoding.Controls.Add(this.btnRun);
            this.tabEncoding.Controls.Add(this.btnClearFiles);
            this.tabEncoding.Controls.Add(this.btnOpenFile);
            this.tabEncoding.Controls.Add(this.grpSource);
            this.tabEncoding.Controls.Add(this.grpEncodings);
            this.tabEncoding.Location = new System.Drawing.Point(4, 28);
            this.tabEncoding.Name = "tabEncoding";
            this.tabEncoding.Size = new System.Drawing.Size(1331, 663);
            this.tabEncoding.TabIndex = 4;
            this.tabEncoding.Text = "Encoding";
            this.tabEncoding.Enter += new System.EventHandler(this.tabEncoding_Enter);
            this.tabEncoding.Leave += new System.EventHandler(this.tabEncoding_Leave);
            // 
            // grpOptions
            // 
            this.grpOptions.BackColor = System.Drawing.Color.LightGray;
            this.grpOptions.Controls.Add(this.chkRTL);
            this.grpOptions.Controls.Add(this.chkOutText);
            this.grpOptions.Controls.Add(this.chkALLEncodings);
            this.grpOptions.Controls.Add(this.chkJommalWord);
            this.grpOptions.Controls.Add(this.chkSendToBuffer);
            this.grpOptions.Controls.Add(this.chkDiacritics);
            this.grpOptions.Controls.Add(this.chkzStrings);
            this.grpOptions.Controls.Add(this.chkDiscardChars);
            this.grpOptions.Controls.Add(this.chkHexText);
            this.grpOptions.Controls.Add(this.chkUnicodeAsDecimal);
            this.grpOptions.Controls.Add(this.chkMeta);
            this.grpOptions.Location = new System.Drawing.Point(40, 312);
            this.grpOptions.Margin = new System.Windows.Forms.Padding(4);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Padding = new System.Windows.Forms.Padding(4);
            this.grpOptions.Size = new System.Drawing.Size(407, 213);
            this.grpOptions.TabIndex = 7;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Special Options";
            // 
            // chkRTL
            // 
            this.chkRTL.AutoSize = true;
            this.chkRTL.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkRTL.Location = new System.Drawing.Point(211, 28);
            this.chkRTL.Margin = new System.Windows.Forms.Padding(4);
            this.chkRTL.Name = "chkRTL";
            this.chkRTL.Size = new System.Drawing.Size(63, 23);
            this.chkRTL.TabIndex = 1;
            this.chkRTL.Text = "RTL";
            this.chkRTL.UseVisualStyleBackColor = true;
            this.chkRTL.CheckedChanged += new System.EventHandler(this.chkRTL_CheckedChanged);
            // 
            // chkOutText
            // 
            this.chkOutText.AutoSize = true;
            this.chkOutText.Location = new System.Drawing.Point(25, 87);
            this.chkOutText.Margin = new System.Windows.Forms.Padding(4);
            this.chkOutText.Name = "chkOutText";
            this.chkOutText.Size = new System.Drawing.Size(165, 23);
            this.chkOutText.TabIndex = 4;
            this.chkOutText.Text = "Output to Window";
            this.chkOutText.UseVisualStyleBackColor = true;
            // 
            // chkALLEncodings
            // 
            this.chkALLEncodings.AutoSize = true;
            this.chkALLEncodings.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkALLEncodings.Location = new System.Drawing.Point(211, 149);
            this.chkALLEncodings.Margin = new System.Windows.Forms.Padding(4);
            this.chkALLEncodings.Name = "chkALLEncodings";
            this.chkALLEncodings.Size = new System.Drawing.Size(168, 23);
            this.chkALLEncodings.TabIndex = 9;
            this.chkALLEncodings.Text = "List ALL Encodings";
            this.chkALLEncodings.UseVisualStyleBackColor = true;
            this.chkALLEncodings.CheckedChanged += new System.EventHandler(this.chkALLEncodings_CheckedChanged);
            // 
            // chkJommalWord
            // 
            this.chkJommalWord.AutoSize = true;
            this.chkJommalWord.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkJommalWord.Location = new System.Drawing.Point(25, 149);
            this.chkJommalWord.Margin = new System.Windows.Forms.Padding(4);
            this.chkJommalWord.Name = "chkJommalWord";
            this.chkJommalWord.Size = new System.Drawing.Size(143, 23);
            this.chkJommalWord.TabIndex = 8;
            this.chkJommalWord.Text = "Jommal WORD";
            this.chkJommalWord.UseVisualStyleBackColor = true;
            this.chkJommalWord.CheckedChanged += new System.EventHandler(this.chkJommalWORD_CheckedChanged);
            // 
            // chkSendToBuffer
            // 
            this.chkSendToBuffer.AutoSize = true;
            this.chkSendToBuffer.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkSendToBuffer.Location = new System.Drawing.Point(25, 118);
            this.chkSendToBuffer.Margin = new System.Windows.Forms.Padding(4);
            this.chkSendToBuffer.Name = "chkSendToBuffer";
            this.chkSendToBuffer.Size = new System.Drawing.Size(141, 23);
            this.chkSendToBuffer.TabIndex = 6;
            this.chkSendToBuffer.Text = "Send To Buffer";
            this.chkSendToBuffer.UseVisualStyleBackColor = true;
            this.chkSendToBuffer.CheckedChanged += new System.EventHandler(this.chkSendToBuffer_CheckedChanged);
            // 
            // chkDiacritics
            // 
            this.chkDiacritics.AutoSize = true;
            this.chkDiacritics.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkDiacritics.Location = new System.Drawing.Point(211, 118);
            this.chkDiacritics.Margin = new System.Windows.Forms.Padding(4);
            this.chkDiacritics.Name = "chkDiacritics";
            this.chkDiacritics.Size = new System.Drawing.Size(155, 23);
            this.chkDiacritics.TabIndex = 7;
            this.chkDiacritics.Text = "Discard Diacritics";
            this.chkDiacritics.UseVisualStyleBackColor = true;
            // 
            // chkzStrings
            // 
            this.chkzStrings.AutoSize = true;
            this.chkzStrings.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkzStrings.Location = new System.Drawing.Point(211, 87);
            this.chkzStrings.Margin = new System.Windows.Forms.Padding(4);
            this.chkzStrings.Name = "chkzStrings";
            this.chkzStrings.Size = new System.Drawing.Size(91, 23);
            this.chkzStrings.TabIndex = 5;
            this.chkzStrings.Text = "zStrings";
            this.chkzStrings.UseVisualStyleBackColor = true;
            // 
            // chkDiscardChars
            // 
            this.chkDiscardChars.AutoSize = true;
            this.chkDiscardChars.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkDiscardChars.Location = new System.Drawing.Point(211, 59);
            this.chkDiscardChars.Margin = new System.Windows.Forms.Padding(4);
            this.chkDiscardChars.Name = "chkDiscardChars";
            this.chkDiscardChars.Size = new System.Drawing.Size(173, 23);
            this.chkDiscardChars.TabIndex = 3;
            this.chkDiscardChars.Text = "Discard Extra Chars";
            this.chkDiscardChars.UseVisualStyleBackColor = true;
            // 
            // chkHexText
            // 
            this.chkHexText.AutoSize = true;
            this.chkHexText.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkHexText.Location = new System.Drawing.Point(25, 56);
            this.chkHexText.Margin = new System.Windows.Forms.Padding(4);
            this.chkHexText.Name = "chkHexText";
            this.chkHexText.Size = new System.Drawing.Size(154, 23);
            this.chkHexText.TabIndex = 2;
            this.chkHexText.Text = "Output HEX Text";
            this.chkHexText.UseVisualStyleBackColor = true;
            this.chkHexText.CheckedChanged += new System.EventHandler(this.chkHexText_CheckedChanged);
            // 
            // chkUnicodeAsDecimal
            // 
            this.chkUnicodeAsDecimal.AutoSize = true;
            this.chkUnicodeAsDecimal.Location = new System.Drawing.Point(25, 180);
            this.chkUnicodeAsDecimal.Margin = new System.Windows.Forms.Padding(4);
            this.chkUnicodeAsDecimal.Name = "chkUnicodeAsDecimal";
            this.chkUnicodeAsDecimal.Size = new System.Drawing.Size(327, 23);
            this.chkUnicodeAsDecimal.TabIndex = 10;
            this.chkUnicodeAsDecimal.Text = "Source Unicode Decimals (e.g. &&#1075;)";
            this.chkUnicodeAsDecimal.UseVisualStyleBackColor = true;
            // 
            // chkMeta
            // 
            this.chkMeta.AutoSize = true;
            this.chkMeta.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkMeta.Location = new System.Drawing.Point(25, 28);
            this.chkMeta.Margin = new System.Windows.Forms.Padding(4);
            this.chkMeta.Name = "chkMeta";
            this.chkMeta.Size = new System.Drawing.Size(152, 23);
            this.chkMeta.TabIndex = 0;
            this.chkMeta.Text = "Output Metadata";
            this.chkMeta.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.btnSCrypto);
            this.panel7.Controls.Add(this.btnSImage);
            this.panel7.Controls.Add(this.btnSCalc);
            this.panel7.Controls.Add(this.btnColor);
            this.panel7.Controls.Add(this.btnSpectrum);
            this.panel7.Controls.Add(this.btnSHex);
            this.panel7.Location = new System.Drawing.Point(477, 608);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(815, 44);
            this.panel7.TabIndex = 11;
            // 
            // btnSCrypto
            // 
            this.btnSCrypto.Location = new System.Drawing.Point(190, 4);
            this.btnSCrypto.Margin = new System.Windows.Forms.Padding(4);
            this.btnSCrypto.Name = "btnSCrypto";
            this.btnSCrypto.Size = new System.Drawing.Size(85, 35);
            this.btnSCrypto.TabIndex = 5;
            this.btnSCrypto.Text = ">Crypto";
            this.btnSCrypto.UseVisualStyleBackColor = true;
            this.btnSCrypto.Click += new System.EventHandler(this.btnSCrypto_Click);
            // 
            // btnSImage
            // 
            this.btnSImage.Location = new System.Drawing.Point(4, 3);
            this.btnSImage.Margin = new System.Windows.Forms.Padding(4);
            this.btnSImage.Name = "btnSImage";
            this.btnSImage.Size = new System.Drawing.Size(85, 35);
            this.btnSImage.TabIndex = 4;
            this.btnSImage.Text = ">X-Ray";
            this.btnSImage.UseVisualStyleBackColor = true;
            this.btnSImage.Click += new System.EventHandler(this.btnSImage_Click);
            // 
            // btnSCalc
            // 
            this.btnSCalc.Location = new System.Drawing.Point(97, 4);
            this.btnSCalc.Margin = new System.Windows.Forms.Padding(4);
            this.btnSCalc.Name = "btnSCalc";
            this.btnSCalc.Size = new System.Drawing.Size(85, 35);
            this.btnSCalc.TabIndex = 4;
            this.btnSCalc.Text = ">Calc";
            this.btnSCalc.UseVisualStyleBackColor = true;
            this.btnSCalc.Click += new System.EventHandler(this.btnSCalc_Click);
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(466, 4);
            this.btnColor.Margin = new System.Windows.Forms.Padding(4);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(85, 35);
            this.btnColor.TabIndex = 6;
            this.btnColor.Text = ">Color";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnSpectrum
            // 
            this.btnSpectrum.Location = new System.Drawing.Point(373, 4);
            this.btnSpectrum.Margin = new System.Windows.Forms.Padding(4);
            this.btnSpectrum.Name = "btnSpectrum";
            this.btnSpectrum.Size = new System.Drawing.Size(85, 35);
            this.btnSpectrum.TabIndex = 6;
            this.btnSpectrum.Text = ">Spct";
            this.btnSpectrum.UseVisualStyleBackColor = true;
            this.btnSpectrum.Click += new System.EventHandler(this.btnSpectrum_Click);
            // 
            // btnSHex
            // 
            this.btnSHex.Location = new System.Drawing.Point(280, 4);
            this.btnSHex.Margin = new System.Windows.Forms.Padding(4);
            this.btnSHex.Name = "btnSHex";
            this.btnSHex.Size = new System.Drawing.Size(85, 35);
            this.btnSHex.TabIndex = 6;
            this.btnSHex.Text = ">Hex";
            this.btnSHex.UseVisualStyleBackColor = true;
            this.btnSHex.Click += new System.EventHandler(this.btnSHex_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.LightGray;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.rbFiles);
            this.panel5.Controls.Add(this.rbText);
            this.panel5.Location = new System.Drawing.Point(40, 530);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(407, 45);
            this.panel5.TabIndex = 10;
            // 
            // rbFiles
            // 
            this.rbFiles.AutoSize = true;
            this.rbFiles.Checked = true;
            this.rbFiles.Location = new System.Drawing.Point(25, 10);
            this.rbFiles.Name = "rbFiles";
            this.rbFiles.Size = new System.Drawing.Size(162, 23);
            this.rbFiles.TabIndex = 0;
            this.rbFiles.TabStop = true;
            this.rbFiles.Text = "Encode Files (List)";
            this.rbFiles.UseVisualStyleBackColor = true;
            // 
            // rbText
            // 
            this.rbText.AutoSize = true;
            this.rbText.Location = new System.Drawing.Point(210, 10);
            this.rbText.Name = "rbText";
            this.rbText.Size = new System.Drawing.Size(147, 23);
            this.rbText.TabIndex = 1;
            this.rbText.Text = "Encode TextBox";
            this.rbText.UseVisualStyleBackColor = true;
            // 
            // progFiles
            // 
            this.progFiles.Location = new System.Drawing.Point(40, 580);
            this.progFiles.Margin = new System.Windows.Forms.Padding(4);
            this.progFiles.Name = "progFiles";
            this.progFiles.Size = new System.Drawing.Size(407, 24);
            this.progFiles.TabIndex = 4;
            // 
            // btnToHex
            // 
            this.btnToHex.Location = new System.Drawing.Point(244, 607);
            this.btnToHex.Margin = new System.Windows.Forms.Padding(4);
            this.btnToHex.Name = "btnToHex";
            this.btnToHex.Size = new System.Drawing.Size(100, 45);
            this.btnToHex.TabIndex = 2;
            this.btnToHex.Text = "To Hex";
            this.btnToHex.UseVisualStyleBackColor = true;
            this.btnToHex.Click += new System.EventHandler(this.btnToHex_Click);
            // 
            // btnRun
            // 
            this.btnRun.Enabled = false;
            this.btnRun.Location = new System.Drawing.Point(347, 607);
            this.btnRun.Margin = new System.Windows.Forms.Padding(4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(100, 45);
            this.btnRun.TabIndex = 3;
            this.btnRun.Text = "Encode";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnClearFiles
            // 
            this.btnClearFiles.Location = new System.Drawing.Point(142, 607);
            this.btnClearFiles.Name = "btnClearFiles";
            this.btnClearFiles.Size = new System.Drawing.Size(100, 45);
            this.btnClearFiles.TabIndex = 1;
            this.btnClearFiles.Text = "Clear";
            this.btnClearFiles.UseVisualStyleBackColor = true;
            this.btnClearFiles.Click += new System.EventHandler(this.btnClearFiles_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(40, 607);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(100, 45);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "Open";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // grpSource
            // 
            this.grpSource.BackColor = System.Drawing.Color.LightGray;
            this.grpSource.Controls.Add(this.lsSource);
            this.grpSource.Location = new System.Drawing.Point(40, 23);
            this.grpSource.Margin = new System.Windows.Forms.Padding(4);
            this.grpSource.Name = "grpSource";
            this.grpSource.Padding = new System.Windows.Forms.Padding(4);
            this.grpSource.Size = new System.Drawing.Size(407, 289);
            this.grpSource.TabIndex = 8;
            this.grpSource.TabStop = false;
            this.grpSource.Text = "Open Files";
            // 
            // lsSource
            // 
            this.lsSource.FormattingEnabled = true;
            this.lsSource.ItemHeight = 19;
            this.lsSource.Location = new System.Drawing.Point(20, 28);
            this.lsSource.Margin = new System.Windows.Forms.Padding(4);
            this.lsSource.Name = "lsSource";
            this.lsSource.Size = new System.Drawing.Size(364, 251);
            this.lsSource.TabIndex = 0;
            // 
            // grpEncodings
            // 
            this.grpEncodings.BackColor = System.Drawing.Color.LightGray;
            this.grpEncodings.Controls.Add(this.rtxtData);
            this.grpEncodings.Controls.Add(this.txtDestEnc);
            this.grpEncodings.Controls.Add(this.txtSourceEnc);
            this.grpEncodings.Controls.Add(this.label35);
            this.grpEncodings.Controls.Add(this.labDestEnc);
            this.grpEncodings.Controls.Add(this.cmbDestEnc);
            this.grpEncodings.Controls.Add(this.cmbSourceEnc);
            this.grpEncodings.Controls.Add(this.labSourceEnc);
            this.grpEncodings.Location = new System.Drawing.Point(477, 23);
            this.grpEncodings.Margin = new System.Windows.Forms.Padding(4);
            this.grpEncodings.Name = "grpEncodings";
            this.grpEncodings.Padding = new System.Windows.Forms.Padding(4);
            this.grpEncodings.Size = new System.Drawing.Size(815, 569);
            this.grpEncodings.TabIndex = 7;
            this.grpEncodings.TabStop = false;
            this.grpEncodings.Text = "Encodings";
            // 
            // rtxtData
            // 
            this.rtxtData.AllowDrop = true;
            this.rtxtData.Location = new System.Drawing.Point(28, 327);
            this.rtxtData.Multiline = true;
            this.rtxtData.Name = "rtxtData";
            this.rtxtData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.rtxtData.Size = new System.Drawing.Size(762, 219);
            this.rtxtData.TabIndex = 2;
            this.rtxtData.TextChanged += new System.EventHandler(this.rtxtData_TextChanged);
            this.rtxtData.DragDrop += new System.Windows.Forms.DragEventHandler(this.rtxtData_DragDrop);
            this.rtxtData.DragOver += new System.Windows.Forms.DragEventHandler(this.rtxtData_DragOver);
            this.rtxtData.DoubleClick += new System.EventHandler(this.rtxtData_DoubleClick);
            // 
            // txtDestEnc
            // 
            this.txtDestEnc.BackColor = System.Drawing.Color.LightGray;
            this.txtDestEnc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDestEnc.Location = new System.Drawing.Point(124, 219);
            this.txtDestEnc.Margin = new System.Windows.Forms.Padding(4);
            this.txtDestEnc.Multiline = true;
            this.txtDestEnc.Name = "txtDestEnc";
            this.txtDestEnc.ReadOnly = true;
            this.txtDestEnc.Size = new System.Drawing.Size(632, 88);
            this.txtDestEnc.TabIndex = 5;
            // 
            // txtSourceEnc
            // 
            this.txtSourceEnc.BackColor = System.Drawing.Color.LightGray;
            this.txtSourceEnc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSourceEnc.Location = new System.Drawing.Point(124, 83);
            this.txtSourceEnc.Margin = new System.Windows.Forms.Padding(4);
            this.txtSourceEnc.Multiline = true;
            this.txtSourceEnc.Name = "txtSourceEnc";
            this.txtSourceEnc.ReadOnly = true;
            this.txtSourceEnc.Size = new System.Drawing.Size(632, 88);
            this.txtSourceEnc.TabIndex = 4;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(24, 301);
            this.label35.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(93, 19);
            this.label35.TabIndex = 3;
            this.label35.Text = "Source Text";
            // 
            // labDestEnc
            // 
            this.labDestEnc.AutoSize = true;
            this.labDestEnc.Location = new System.Drawing.Point(24, 184);
            this.labDestEnc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labDestEnc.Name = "labDestEnc";
            this.labDestEnc.Size = new System.Drawing.Size(88, 19);
            this.labDestEnc.TabIndex = 3;
            this.labDestEnc.Text = "Destination";
            // 
            // cmbDestEnc
            // 
            this.cmbDestEnc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDestEnc.FormattingEnabled = true;
            this.cmbDestEnc.Location = new System.Drawing.Point(124, 180);
            this.cmbDestEnc.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDestEnc.Name = "cmbDestEnc";
            this.cmbDestEnc.Size = new System.Drawing.Size(666, 27);
            this.cmbDestEnc.TabIndex = 1;
            this.cmbDestEnc.SelectedIndexChanged += new System.EventHandler(this.cmbDestEnc_SelectedIndexChanged);
            // 
            // cmbSourceEnc
            // 
            this.cmbSourceEnc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSourceEnc.FormattingEnabled = true;
            this.cmbSourceEnc.Location = new System.Drawing.Point(124, 44);
            this.cmbSourceEnc.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSourceEnc.Name = "cmbSourceEnc";
            this.cmbSourceEnc.Size = new System.Drawing.Size(666, 27);
            this.cmbSourceEnc.TabIndex = 0;
            this.cmbSourceEnc.SelectedIndexChanged += new System.EventHandler(this.cmbSourceEnc_SelectedIndexChanged);
            // 
            // labSourceEnc
            // 
            this.labSourceEnc.AutoSize = true;
            this.labSourceEnc.Location = new System.Drawing.Point(24, 47);
            this.labSourceEnc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labSourceEnc.Name = "labSourceEnc";
            this.labSourceEnc.Size = new System.Drawing.Size(57, 19);
            this.labSourceEnc.TabIndex = 0;
            this.labSourceEnc.Text = "Source";
            // 
            // tabQuran
            // 
            this.tabQuran.BackColor = System.Drawing.Color.LightGray;
            this.tabQuran.Controls.Add(this.rbDiacritics);
            this.tabQuran.Controls.Add(this.rbNoDiacritics);
            this.tabQuran.Controls.Add(this.rbFirstOriginalDots);
            this.tabQuran.Controls.Add(this.rbFirstOriginal);
            this.tabQuran.Controls.Add(this.txtSearch);
            this.tabQuran.Controls.Add(this.dgvQuran);
            this.tabQuran.Controls.Add(this.lblFontSize);
            this.tabQuran.Controls.Add(this.btnMinus);
            this.tabQuran.Controls.Add(this.btnPlus);
            this.tabQuran.Controls.Add(this.lbSoras);
            this.tabQuran.Controls.Add(this.btnSearch);
            this.tabQuran.Controls.Add(this.btnSendToEncoding);
            this.tabQuran.Controls.Add(this.txtQuranText);
            this.tabQuran.Controls.Add(this.label38);
            this.tabQuran.Controls.Add(this.label37);
            this.tabQuran.Location = new System.Drawing.Point(4, 28);
            this.tabQuran.Name = "tabQuran";
            this.tabQuran.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabQuran.Size = new System.Drawing.Size(1331, 663);
            this.tabQuran.TabIndex = 5;
            this.tabQuran.Text = "Quran Text";
            // 
            // rbDiacritics
            // 
            this.rbDiacritics.AutoSize = true;
            this.rbDiacritics.Checked = true;
            this.rbDiacritics.Location = new System.Drawing.Point(540, 15);
            this.rbDiacritics.Name = "rbDiacritics";
            this.rbDiacritics.Size = new System.Drawing.Size(169, 23);
            this.rbDiacritics.TabIndex = 3;
            this.rbDiacritics.TabStop = true;
            this.rbDiacritics.Text = "New with-Diacritics";
            this.rbDiacritics.UseVisualStyleBackColor = true;
            this.rbDiacritics.CheckedChanged += new System.EventHandler(this.rbTextType_CheckedChanged);
            // 
            // rbNoDiacritics
            // 
            this.rbNoDiacritics.AutoSize = true;
            this.rbNoDiacritics.Location = new System.Drawing.Point(364, 15);
            this.rbNoDiacritics.Name = "rbNoDiacritics";
            this.rbNoDiacritics.Size = new System.Drawing.Size(157, 23);
            this.rbNoDiacritics.TabIndex = 2;
            this.rbNoDiacritics.Text = "New no-Diacritics";
            this.rbNoDiacritics.UseVisualStyleBackColor = true;
            this.rbNoDiacritics.CheckedChanged += new System.EventHandler(this.rbTextType_CheckedChanged);
            // 
            // rbFirstOriginalDots
            // 
            this.rbFirstOriginalDots.AutoSize = true;
            this.rbFirstOriginalDots.Location = new System.Drawing.Point(46, 15);
            this.rbFirstOriginalDots.Name = "rbFirstOriginalDots";
            this.rbFirstOriginalDots.Size = new System.Drawing.Size(162, 23);
            this.rbFirstOriginalDots.TabIndex = 0;
            this.rbFirstOriginalDots.Text = "First Origianl Dots";
            this.rbFirstOriginalDots.UseVisualStyleBackColor = true;
            this.rbFirstOriginalDots.CheckedChanged += new System.EventHandler(this.rbTextType_CheckedChanged);
            // 
            // rbFirstOriginal
            // 
            this.rbFirstOriginal.AutoSize = true;
            this.rbFirstOriginal.Location = new System.Drawing.Point(218, 15);
            this.rbFirstOriginal.Name = "rbFirstOriginal";
            this.rbFirstOriginal.Size = new System.Drawing.Size(125, 23);
            this.rbFirstOriginal.TabIndex = 1;
            this.rbFirstOriginal.Text = "First Origianl";
            this.rbFirstOriginal.UseVisualStyleBackColor = true;
            this.rbFirstOriginal.CheckedChanged += new System.EventHandler(this.rbTextType_CheckedChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(217, 615);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSearch.Size = new System.Drawing.Size(741, 34);
            this.txtSearch.TabIndex = 10;
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // dgvQuran
            // 
            this.dgvQuran.AllowUserToAddRows = false;
            this.dgvQuran.AllowUserToDeleteRows = false;
            this.dgvQuran.AllowUserToOrderColumns = true;
            this.dgvQuran.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvQuran.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dgvQuran.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvQuran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuran.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Serial,
            this.SoraNo,
            this.AyaNo,
            this.SoraName,
            this.AyaText});
            this.dgvQuran.Location = new System.Drawing.Point(43, 301);
            this.dgvQuran.Name = "dgvQuran";
            this.dgvQuran.ReadOnly = true;
            this.dgvQuran.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dgvQuran.RowHeadersWidth = 62;
            this.dgvQuran.RowTemplate.Height = 29;
            this.dgvQuran.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQuran.Size = new System.Drawing.Size(1049, 304);
            this.dgvQuran.TabIndex = 5;
            this.dgvQuran.Click += new System.EventHandler(this.dgvQuran_Click);
            // 
            // Serial
            // 
            this.Serial.HeaderText = "Serial";
            this.Serial.MinimumWidth = 8;
            this.Serial.Name = "Serial";
            this.Serial.ReadOnly = true;
            this.Serial.Width = 84;
            // 
            // SoraNo
            // 
            this.SoraNo.HeaderText = "SoraNo";
            this.SoraNo.MinimumWidth = 8;
            this.SoraNo.Name = "SoraNo";
            this.SoraNo.ReadOnly = true;
            this.SoraNo.Width = 97;
            // 
            // AyaNo
            // 
            this.AyaNo.HeaderText = "AyaNo";
            this.AyaNo.MinimumWidth = 8;
            this.AyaNo.Name = "AyaNo";
            this.AyaNo.ReadOnly = true;
            this.AyaNo.Width = 92;
            // 
            // SoraName
            // 
            this.SoraName.HeaderText = "Sora Name";
            this.SoraName.MinimumWidth = 8;
            this.SoraName.Name = "SoraName";
            this.SoraName.ReadOnly = true;
            this.SoraName.Width = 123;
            // 
            // AyaText
            // 
            this.AyaText.HeaderText = "Aya Text";
            this.AyaText.MinimumWidth = 8;
            this.AyaText.Name = "AyaText";
            this.AyaText.ReadOnly = true;
            this.AyaText.Width = 108;
            // 
            // lblFontSize
            // 
            this.lblFontSize.BackColor = System.Drawing.Color.White;
            this.lblFontSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFontSize.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblFontSize.Location = new System.Drawing.Point(85, 615);
            this.lblFontSize.Name = "lblFontSize";
            this.lblFontSize.Size = new System.Drawing.Size(86, 33);
            this.lblFontSize.TabIndex = 8;
            this.lblFontSize.Text = "12";
            this.lblFontSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFontSize.Click += new System.EventHandler(this.lblFontSize_Click);
            // 
            // btnMinus
            // 
            this.btnMinus.Location = new System.Drawing.Point(49, 614);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(38, 37);
            this.btnMinus.TabIndex = 7;
            this.btnMinus.Text = "-";
            this.btnMinus.UseVisualStyleBackColor = true;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // btnPlus
            // 
            this.btnPlus.Location = new System.Drawing.Point(170, 614);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(38, 37);
            this.btnPlus.TabIndex = 9;
            this.btnPlus.Text = "+";
            this.btnPlus.UseVisualStyleBackColor = true;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // lbSoras
            // 
            this.lbSoras.FormattingEnabled = true;
            this.lbSoras.ItemHeight = 19;
            this.lbSoras.Location = new System.Drawing.Point(1095, 50);
            this.lbSoras.Name = "lbSoras";
            this.lbSoras.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbSoras.Size = new System.Drawing.Size(209, 555);
            this.lbSoras.TabIndex = 6;
            this.lbSoras.SelectedIndexChanged += new System.EventHandler(this.lbSoras_SelectedIndexChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(964, 609);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(128, 47);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSendToEncoding
            // 
            this.btnSendToEncoding.Location = new System.Drawing.Point(1095, 609);
            this.btnSendToEncoding.Name = "btnSendToEncoding";
            this.btnSendToEncoding.Size = new System.Drawing.Size(209, 47);
            this.btnSendToEncoding.TabIndex = 12;
            this.btnSendToEncoding.Text = "Send to Encoding";
            this.btnSendToEncoding.UseVisualStyleBackColor = true;
            this.btnSendToEncoding.Click += new System.EventHandler(this.btnSendToEncoding_Click);
            // 
            // txtQuranText
            // 
            this.txtQuranText.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtQuranText.Location = new System.Drawing.Point(42, 50);
            this.txtQuranText.Multiline = true;
            this.txtQuranText.Name = "txtQuranText";
            this.txtQuranText.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtQuranText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtQuranText.Size = new System.Drawing.Size(1050, 245);
            this.txtQuranText.TabIndex = 4;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(1003, 19);
            this.label38.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(89, 19);
            this.label38.TabIndex = 4;
            this.label38.Text = "Quran Text";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(1214, 19);
            this.label37.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(90, 19);
            this.label37.TabIndex = 4;
            this.label37.Text = "Quran Sora";
            // 
            // tabCharset
            // 
            this.tabCharset.BackColor = System.Drawing.Color.LightGray;
            this.tabCharset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabCharset.Controls.Add(this.panel6);
            this.tabCharset.Controls.Add(this.txtPlus);
            this.tabCharset.Controls.Add(this.lblCurCharset);
            this.tabCharset.Controls.Add(this.btnReset);
            this.tabCharset.Controls.Add(this.btnClearCS);
            this.tabCharset.Controls.Add(this.btnAutoSub);
            this.tabCharset.Controls.Add(this.btnAutoAdd);
            this.tabCharset.Controls.Add(this.btnAutoCharset);
            this.tabCharset.Controls.Add(this.btnLoadCharset);
            this.tabCharset.Controls.Add(this.btnOrder);
            this.tabCharset.Controls.Add(this.btnSaveCharset);
            this.tabCharset.Controls.Add(this.panel4);
            this.tabCharset.Controls.Add(this.panel3);
            this.tabCharset.Controls.Add(this.panel2);
            this.tabCharset.Controls.Add(this.panel1);
            this.tabCharset.Location = new System.Drawing.Point(4, 28);
            this.tabCharset.Name = "tabCharset";
            this.tabCharset.Size = new System.Drawing.Size(1331, 663);
            this.tabCharset.TabIndex = 6;
            this.tabCharset.Text = "CharSet";
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.lblCSS44);
            this.panel6.Controls.Add(this.lblCSS43);
            this.panel6.Controls.Add(this.lblCSS42);
            this.panel6.Controls.Add(this.lblCSS41);
            this.panel6.Controls.Add(this.lblCSS40);
            this.panel6.Controls.Add(this.lblCSS39);
            this.panel6.Controls.Add(this.lblCSS38);
            this.panel6.Controls.Add(this.lblCSS37);
            this.panel6.Controls.Add(this.lblCSS36);
            this.panel6.Controls.Add(this.lblCSS35);
            this.panel6.Controls.Add(this.lblCSS34);
            this.panel6.Controls.Add(this.lblCSS33);
            this.panel6.Controls.Add(this.lblCSS32);
            this.panel6.Controls.Add(this.lblCSS31);
            this.panel6.Controls.Add(this.lblCSS30);
            this.panel6.Controls.Add(this.lblCSS29);
            this.panel6.Controls.Add(this.lblCSS28);
            this.panel6.Controls.Add(this.lblCSS27);
            this.panel6.Controls.Add(this.lblCSS26);
            this.panel6.Controls.Add(this.lblCSS25);
            this.panel6.Controls.Add(this.lblCSS24);
            this.panel6.Controls.Add(this.lblCSS23);
            this.panel6.Controls.Add(this.lblCSS22);
            this.panel6.Controls.Add(this.lblCSS21);
            this.panel6.Controls.Add(this.lblCSS20);
            this.panel6.Controls.Add(this.lblCSS19);
            this.panel6.Controls.Add(this.lblCSS18);
            this.panel6.Controls.Add(this.lblCSS17);
            this.panel6.Controls.Add(this.lblCSS16);
            this.panel6.Controls.Add(this.lblCSS15);
            this.panel6.Controls.Add(this.lblCSS14);
            this.panel6.Controls.Add(this.lblCSS13);
            this.panel6.Controls.Add(this.lblCSS12);
            this.panel6.Controls.Add(this.lblCSS11);
            this.panel6.Controls.Add(this.lblCSS10);
            this.panel6.Controls.Add(this.lblCSS9);
            this.panel6.Controls.Add(this.lblCSS8);
            this.panel6.Controls.Add(this.lblCSS7);
            this.panel6.Controls.Add(this.lblCSS6);
            this.panel6.Controls.Add(this.lblCSS5);
            this.panel6.Controls.Add(this.lblCSS4);
            this.panel6.Controls.Add(this.lblCSS3);
            this.panel6.Controls.Add(this.lblCSS2);
            this.panel6.Controls.Add(this.lblCSS1);
            this.panel6.Location = new System.Drawing.Point(52, 523);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1225, 93);
            this.panel6.TabIndex = 60;
            // 
            // lblCSS44
            // 
            this.lblCSS44.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS44.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS44.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS44.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS44.Location = new System.Drawing.Point(309, 49);
            this.lblCSS44.Name = "lblCSS44";
            this.lblCSS44.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS44.Size = new System.Drawing.Size(35, 40);
            this.lblCSS44.TabIndex = 102;
            this.lblCSS44.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS43
            // 
            this.lblCSS43.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS43.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS43.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS43.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS43.Location = new System.Drawing.Point(350, 49);
            this.lblCSS43.Name = "lblCSS43";
            this.lblCSS43.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS43.Size = new System.Drawing.Size(35, 40);
            this.lblCSS43.TabIndex = 102;
            this.lblCSS43.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS42
            // 
            this.lblCSS42.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS42.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS42.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS42.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS42.Location = new System.Drawing.Point(391, 49);
            this.lblCSS42.Name = "lblCSS42";
            this.lblCSS42.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS42.Size = new System.Drawing.Size(35, 40);
            this.lblCSS42.TabIndex = 102;
            this.lblCSS42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS41
            // 
            this.lblCSS41.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS41.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS41.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS41.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS41.Location = new System.Drawing.Point(432, 49);
            this.lblCSS41.Name = "lblCSS41";
            this.lblCSS41.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS41.Size = new System.Drawing.Size(35, 40);
            this.lblCSS41.TabIndex = 102;
            this.lblCSS41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS40
            // 
            this.lblCSS40.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS40.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS40.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS40.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS40.Location = new System.Drawing.Point(473, 49);
            this.lblCSS40.Name = "lblCSS40";
            this.lblCSS40.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS40.Size = new System.Drawing.Size(35, 40);
            this.lblCSS40.TabIndex = 102;
            this.lblCSS40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS39
            // 
            this.lblCSS39.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS39.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS39.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS39.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS39.Location = new System.Drawing.Point(514, 49);
            this.lblCSS39.Name = "lblCSS39";
            this.lblCSS39.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS39.Size = new System.Drawing.Size(35, 40);
            this.lblCSS39.TabIndex = 102;
            this.lblCSS39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS38
            // 
            this.lblCSS38.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS38.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS38.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS38.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS38.Location = new System.Drawing.Point(555, 49);
            this.lblCSS38.Name = "lblCSS38";
            this.lblCSS38.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS38.Size = new System.Drawing.Size(35, 40);
            this.lblCSS38.TabIndex = 102;
            this.lblCSS38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS37
            // 
            this.lblCSS37.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS37.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS37.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS37.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS37.Location = new System.Drawing.Point(596, 49);
            this.lblCSS37.Name = "lblCSS37";
            this.lblCSS37.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS37.Size = new System.Drawing.Size(35, 40);
            this.lblCSS37.TabIndex = 102;
            this.lblCSS37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS36
            // 
            this.lblCSS36.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS36.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS36.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS36.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS36.Location = new System.Drawing.Point(637, 49);
            this.lblCSS36.Name = "lblCSS36";
            this.lblCSS36.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS36.Size = new System.Drawing.Size(35, 40);
            this.lblCSS36.TabIndex = 102;
            this.lblCSS36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS35
            // 
            this.lblCSS35.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS35.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS35.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS35.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS35.Location = new System.Drawing.Point(678, 49);
            this.lblCSS35.Name = "lblCSS35";
            this.lblCSS35.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS35.Size = new System.Drawing.Size(35, 40);
            this.lblCSS35.TabIndex = 102;
            this.lblCSS35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS34
            // 
            this.lblCSS34.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS34.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS34.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS34.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS34.Location = new System.Drawing.Point(719, 49);
            this.lblCSS34.Name = "lblCSS34";
            this.lblCSS34.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS34.Size = new System.Drawing.Size(35, 40);
            this.lblCSS34.TabIndex = 102;
            this.lblCSS34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS33
            // 
            this.lblCSS33.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS33.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS33.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS33.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS33.Location = new System.Drawing.Point(760, 49);
            this.lblCSS33.Name = "lblCSS33";
            this.lblCSS33.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS33.Size = new System.Drawing.Size(35, 40);
            this.lblCSS33.TabIndex = 102;
            this.lblCSS33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS32
            // 
            this.lblCSS32.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS32.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS32.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS32.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS32.Location = new System.Drawing.Point(801, 49);
            this.lblCSS32.Name = "lblCSS32";
            this.lblCSS32.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS32.Size = new System.Drawing.Size(35, 40);
            this.lblCSS32.TabIndex = 102;
            this.lblCSS32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS31
            // 
            this.lblCSS31.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS31.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS31.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS31.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS31.Location = new System.Drawing.Point(842, 49);
            this.lblCSS31.Name = "lblCSS31";
            this.lblCSS31.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS31.Size = new System.Drawing.Size(35, 40);
            this.lblCSS31.TabIndex = 102;
            this.lblCSS31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS30
            // 
            this.lblCSS30.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS30.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS30.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS30.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS30.Location = new System.Drawing.Point(883, 49);
            this.lblCSS30.Name = "lblCSS30";
            this.lblCSS30.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS30.Size = new System.Drawing.Size(35, 40);
            this.lblCSS30.TabIndex = 102;
            this.lblCSS30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS29
            // 
            this.lblCSS29.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS29.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS29.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS29.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS29.Location = new System.Drawing.Point(22, 9);
            this.lblCSS29.Name = "lblCSS29";
            this.lblCSS29.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS29.Size = new System.Drawing.Size(35, 40);
            this.lblCSS29.TabIndex = 102;
            this.lblCSS29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS28
            // 
            this.lblCSS28.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS28.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS28.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS28.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS28.Location = new System.Drawing.Point(63, 9);
            this.lblCSS28.Name = "lblCSS28";
            this.lblCSS28.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS28.Size = new System.Drawing.Size(35, 40);
            this.lblCSS28.TabIndex = 102;
            this.lblCSS28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS27
            // 
            this.lblCSS27.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS27.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS27.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS27.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS27.Location = new System.Drawing.Point(104, 9);
            this.lblCSS27.Name = "lblCSS27";
            this.lblCSS27.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS27.Size = new System.Drawing.Size(35, 40);
            this.lblCSS27.TabIndex = 102;
            this.lblCSS27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS26
            // 
            this.lblCSS26.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS26.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS26.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS26.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS26.Location = new System.Drawing.Point(145, 9);
            this.lblCSS26.Name = "lblCSS26";
            this.lblCSS26.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS26.Size = new System.Drawing.Size(35, 40);
            this.lblCSS26.TabIndex = 102;
            this.lblCSS26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS25
            // 
            this.lblCSS25.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS25.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS25.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS25.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS25.Location = new System.Drawing.Point(186, 9);
            this.lblCSS25.Name = "lblCSS25";
            this.lblCSS25.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS25.Size = new System.Drawing.Size(35, 40);
            this.lblCSS25.TabIndex = 102;
            this.lblCSS25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS24
            // 
            this.lblCSS24.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS24.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS24.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS24.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS24.Location = new System.Drawing.Point(227, 9);
            this.lblCSS24.Name = "lblCSS24";
            this.lblCSS24.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS24.Size = new System.Drawing.Size(35, 40);
            this.lblCSS24.TabIndex = 102;
            this.lblCSS24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS23
            // 
            this.lblCSS23.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS23.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS23.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS23.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS23.Location = new System.Drawing.Point(268, 9);
            this.lblCSS23.Name = "lblCSS23";
            this.lblCSS23.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS23.Size = new System.Drawing.Size(35, 40);
            this.lblCSS23.TabIndex = 102;
            this.lblCSS23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS22
            // 
            this.lblCSS22.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS22.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS22.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS22.Location = new System.Drawing.Point(309, 9);
            this.lblCSS22.Name = "lblCSS22";
            this.lblCSS22.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS22.Size = new System.Drawing.Size(35, 40);
            this.lblCSS22.TabIndex = 102;
            this.lblCSS22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS21
            // 
            this.lblCSS21.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS21.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS21.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS21.Location = new System.Drawing.Point(350, 9);
            this.lblCSS21.Name = "lblCSS21";
            this.lblCSS21.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS21.Size = new System.Drawing.Size(35, 40);
            this.lblCSS21.TabIndex = 102;
            this.lblCSS21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS20
            // 
            this.lblCSS20.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS20.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS20.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS20.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS20.Location = new System.Drawing.Point(391, 9);
            this.lblCSS20.Name = "lblCSS20";
            this.lblCSS20.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS20.Size = new System.Drawing.Size(35, 40);
            this.lblCSS20.TabIndex = 102;
            this.lblCSS20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS19
            // 
            this.lblCSS19.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS19.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS19.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS19.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS19.Location = new System.Drawing.Point(432, 9);
            this.lblCSS19.Name = "lblCSS19";
            this.lblCSS19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS19.Size = new System.Drawing.Size(35, 40);
            this.lblCSS19.TabIndex = 102;
            this.lblCSS19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS18
            // 
            this.lblCSS18.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS18.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS18.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS18.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS18.Location = new System.Drawing.Point(473, 9);
            this.lblCSS18.Name = "lblCSS18";
            this.lblCSS18.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS18.Size = new System.Drawing.Size(35, 40);
            this.lblCSS18.TabIndex = 102;
            this.lblCSS18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS17
            // 
            this.lblCSS17.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS17.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS17.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS17.Location = new System.Drawing.Point(514, 9);
            this.lblCSS17.Name = "lblCSS17";
            this.lblCSS17.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS17.Size = new System.Drawing.Size(35, 40);
            this.lblCSS17.TabIndex = 102;
            this.lblCSS17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS16
            // 
            this.lblCSS16.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS16.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS16.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS16.Location = new System.Drawing.Point(555, 9);
            this.lblCSS16.Name = "lblCSS16";
            this.lblCSS16.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS16.Size = new System.Drawing.Size(35, 40);
            this.lblCSS16.TabIndex = 102;
            this.lblCSS16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS15
            // 
            this.lblCSS15.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS15.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS15.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS15.Location = new System.Drawing.Point(596, 9);
            this.lblCSS15.Name = "lblCSS15";
            this.lblCSS15.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS15.Size = new System.Drawing.Size(35, 40);
            this.lblCSS15.TabIndex = 102;
            this.lblCSS15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS14
            // 
            this.lblCSS14.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS14.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS14.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS14.Location = new System.Drawing.Point(637, 9);
            this.lblCSS14.Name = "lblCSS14";
            this.lblCSS14.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS14.Size = new System.Drawing.Size(35, 40);
            this.lblCSS14.TabIndex = 102;
            this.lblCSS14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS13
            // 
            this.lblCSS13.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS13.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS13.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS13.Location = new System.Drawing.Point(678, 9);
            this.lblCSS13.Name = "lblCSS13";
            this.lblCSS13.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS13.Size = new System.Drawing.Size(35, 40);
            this.lblCSS13.TabIndex = 102;
            this.lblCSS13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS12
            // 
            this.lblCSS12.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS12.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS12.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS12.Location = new System.Drawing.Point(719, 9);
            this.lblCSS12.Name = "lblCSS12";
            this.lblCSS12.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS12.Size = new System.Drawing.Size(35, 40);
            this.lblCSS12.TabIndex = 102;
            this.lblCSS12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS11
            // 
            this.lblCSS11.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS11.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS11.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS11.Location = new System.Drawing.Point(760, 9);
            this.lblCSS11.Name = "lblCSS11";
            this.lblCSS11.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS11.Size = new System.Drawing.Size(35, 40);
            this.lblCSS11.TabIndex = 102;
            this.lblCSS11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS10
            // 
            this.lblCSS10.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS10.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS10.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS10.Location = new System.Drawing.Point(801, 9);
            this.lblCSS10.Name = "lblCSS10";
            this.lblCSS10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS10.Size = new System.Drawing.Size(35, 40);
            this.lblCSS10.TabIndex = 102;
            this.lblCSS10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS9
            // 
            this.lblCSS9.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS9.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS9.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS9.Location = new System.Drawing.Point(842, 9);
            this.lblCSS9.Name = "lblCSS9";
            this.lblCSS9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS9.Size = new System.Drawing.Size(35, 40);
            this.lblCSS9.TabIndex = 102;
            this.lblCSS9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS8
            // 
            this.lblCSS8.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS8.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS8.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS8.Location = new System.Drawing.Point(883, 9);
            this.lblCSS8.Name = "lblCSS8";
            this.lblCSS8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS8.Size = new System.Drawing.Size(35, 40);
            this.lblCSS8.TabIndex = 102;
            this.lblCSS8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS7
            // 
            this.lblCSS7.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS7.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS7.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS7.Location = new System.Drawing.Point(924, 9);
            this.lblCSS7.Name = "lblCSS7";
            this.lblCSS7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS7.Size = new System.Drawing.Size(35, 40);
            this.lblCSS7.TabIndex = 102;
            this.lblCSS7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS6
            // 
            this.lblCSS6.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS6.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS6.Location = new System.Drawing.Point(965, 9);
            this.lblCSS6.Name = "lblCSS6";
            this.lblCSS6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS6.Size = new System.Drawing.Size(35, 40);
            this.lblCSS6.TabIndex = 102;
            this.lblCSS6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS5
            // 
            this.lblCSS5.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS5.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS5.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS5.Location = new System.Drawing.Point(1006, 9);
            this.lblCSS5.Name = "lblCSS5";
            this.lblCSS5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS5.Size = new System.Drawing.Size(35, 40);
            this.lblCSS5.TabIndex = 102;
            this.lblCSS5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS4
            // 
            this.lblCSS4.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS4.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS4.Location = new System.Drawing.Point(1047, 9);
            this.lblCSS4.Name = "lblCSS4";
            this.lblCSS4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS4.Size = new System.Drawing.Size(35, 40);
            this.lblCSS4.TabIndex = 102;
            this.lblCSS4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS3
            // 
            this.lblCSS3.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS3.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS3.Location = new System.Drawing.Point(1088, 9);
            this.lblCSS3.Name = "lblCSS3";
            this.lblCSS3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS3.Size = new System.Drawing.Size(35, 40);
            this.lblCSS3.TabIndex = 102;
            this.lblCSS3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS2
            // 
            this.lblCSS2.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS2.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS2.Location = new System.Drawing.Point(1129, 9);
            this.lblCSS2.Name = "lblCSS2";
            this.lblCSS2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS2.Size = new System.Drawing.Size(35, 40);
            this.lblCSS2.TabIndex = 102;
            this.lblCSS2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSS1
            // 
            this.lblCSS1.BackColor = System.Drawing.Color.Cornsilk;
            this.lblCSS1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCSS1.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSS1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCSS1.Location = new System.Drawing.Point(1170, 9);
            this.lblCSS1.Name = "lblCSS1";
            this.lblCSS1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCSS1.Size = new System.Drawing.Size(35, 40);
            this.lblCSS1.TabIndex = 102;
            this.lblCSS1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPlus
            // 
            this.txtPlus.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlus.Location = new System.Drawing.Point(189, 622);
            this.txtPlus.Name = "txtPlus";
            this.txtPlus.Size = new System.Drawing.Size(66, 36);
            this.txtPlus.TabIndex = 48;
            this.txtPlus.Text = "1";
            this.txtPlus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCurCharset
            // 
            this.lblCurCharset.BackColor = System.Drawing.SystemColors.Info;
            this.lblCurCharset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurCharset.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurCharset.Location = new System.Drawing.Point(824, 618);
            this.lblCurCharset.Name = "lblCurCharset";
            this.lblCurCharset.Size = new System.Drawing.Size(453, 40);
            this.lblCurCharset.TabIndex = 55;
            this.lblCurCharset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(289, 618);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 40);
            this.btnReset.TabIndex = 59;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnClearCS
            // 
            this.btnClearCS.Location = new System.Drawing.Point(501, 618);
            this.btnClearCS.Name = "btnClearCS";
            this.btnClearCS.Size = new System.Drawing.Size(100, 40);
            this.btnClearCS.TabIndex = 59;
            this.btnClearCS.Text = "Clear";
            this.btnClearCS.UseVisualStyleBackColor = true;
            this.btnClearCS.Click += new System.EventHandler(this.btnClearCS_Click);
            // 
            // btnAutoSub
            // 
            this.btnAutoSub.Location = new System.Drawing.Point(156, 624);
            this.btnAutoSub.Name = "btnAutoSub";
            this.btnAutoSub.Size = new System.Drawing.Size(34, 32);
            this.btnAutoSub.TabIndex = 47;
            this.btnAutoSub.Text = "-";
            this.btnAutoSub.UseVisualStyleBackColor = true;
            this.btnAutoSub.Click += new System.EventHandler(this.btnAutoAdd_Click);
            // 
            // btnAutoAdd
            // 
            this.btnAutoAdd.Location = new System.Drawing.Point(254, 624);
            this.btnAutoAdd.Name = "btnAutoAdd";
            this.btnAutoAdd.Size = new System.Drawing.Size(34, 32);
            this.btnAutoAdd.TabIndex = 47;
            this.btnAutoAdd.Text = "+";
            this.btnAutoAdd.UseVisualStyleBackColor = true;
            this.btnAutoAdd.Click += new System.EventHandler(this.btnAutoAdd_Click);
            // 
            // btnAutoCharset
            // 
            this.btnAutoCharset.Location = new System.Drawing.Point(52, 618);
            this.btnAutoCharset.Name = "btnAutoCharset";
            this.btnAutoCharset.Size = new System.Drawing.Size(100, 40);
            this.btnAutoCharset.TabIndex = 46;
            this.btnAutoCharset.Text = "Auto";
            this.btnAutoCharset.UseVisualStyleBackColor = true;
            this.btnAutoCharset.Click += new System.EventHandler(this.btnAutoCharset_Click);
            // 
            // btnLoadCharset
            // 
            this.btnLoadCharset.Location = new System.Drawing.Point(607, 618);
            this.btnLoadCharset.Name = "btnLoadCharset";
            this.btnLoadCharset.Size = new System.Drawing.Size(100, 40);
            this.btnLoadCharset.TabIndex = 50;
            this.btnLoadCharset.Text = "Load";
            this.btnLoadCharset.UseVisualStyleBackColor = true;
            this.btnLoadCharset.Click += new System.EventHandler(this.btnLoadCharset_Click);
            // 
            // btnOrder
            // 
            this.btnOrder.Location = new System.Drawing.Point(395, 618);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(100, 40);
            this.btnOrder.TabIndex = 51;
            this.btnOrder.Text = "ReOrder";
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // btnSaveCharset
            // 
            this.btnSaveCharset.Location = new System.Drawing.Point(713, 618);
            this.btnSaveCharset.Name = "btnSaveCharset";
            this.btnSaveCharset.Size = new System.Drawing.Size(100, 40);
            this.btnSaveCharset.TabIndex = 51;
            this.btnSaveCharset.Text = "Save";
            this.btnSaveCharset.UseVisualStyleBackColor = true;
            this.btnSaveCharset.Click += new System.EventHandler(this.btnSaveCharset_Click);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.txtCS44);
            this.panel4.Controls.Add(this.txtCS43);
            this.panel4.Controls.Add(this.txtCS42);
            this.panel4.Controls.Add(this.txtCS41);
            this.panel4.Controls.Add(this.txtCS40);
            this.panel4.Controls.Add(this.txtCS39);
            this.panel4.Controls.Add(this.txtCS38);
            this.panel4.Controls.Add(this.txtCS37);
            this.panel4.Controls.Add(this.txtCS36);
            this.panel4.Controls.Add(this.txtCS35);
            this.panel4.Controls.Add(this.txtCS34);
            this.panel4.Controls.Add(this.lblCS44);
            this.panel4.Controls.Add(this.lblCS43);
            this.panel4.Controls.Add(this.lblCS42);
            this.panel4.Controls.Add(this.lblCS41);
            this.panel4.Controls.Add(this.lblCS40);
            this.panel4.Controls.Add(this.lblCS39);
            this.panel4.Controls.Add(this.lblCS38);
            this.panel4.Controls.Add(this.lblCS37);
            this.panel4.Controls.Add(this.lblCS36);
            this.panel4.Controls.Add(this.lblCS35);
            this.panel4.Controls.Add(this.lblCS34);
            this.panel4.Location = new System.Drawing.Point(52, 395);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1225, 130);
            this.panel4.TabIndex = 1;
            // 
            // txtCS44
            // 
            this.txtCS44.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS44.Location = new System.Drawing.Point(26, 68);
            this.txtCS44.Name = "txtCS44";
            this.txtCS44.Size = new System.Drawing.Size(74, 51);
            this.txtCS44.TabIndex = 44;
            this.txtCS44.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS43
            // 
            this.txtCS43.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS43.Location = new System.Drawing.Point(136, 68);
            this.txtCS43.Name = "txtCS43";
            this.txtCS43.Size = new System.Drawing.Size(74, 51);
            this.txtCS43.TabIndex = 43;
            this.txtCS43.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS42
            // 
            this.txtCS42.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS42.Location = new System.Drawing.Point(246, 68);
            this.txtCS42.Name = "txtCS42";
            this.txtCS42.Size = new System.Drawing.Size(74, 51);
            this.txtCS42.TabIndex = 42;
            this.txtCS42.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS41
            // 
            this.txtCS41.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS41.Location = new System.Drawing.Point(356, 68);
            this.txtCS41.Name = "txtCS41";
            this.txtCS41.Size = new System.Drawing.Size(74, 51);
            this.txtCS41.TabIndex = 41;
            this.txtCS41.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS40
            // 
            this.txtCS40.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS40.Location = new System.Drawing.Point(466, 68);
            this.txtCS40.Name = "txtCS40";
            this.txtCS40.Size = new System.Drawing.Size(74, 51);
            this.txtCS40.TabIndex = 40;
            this.txtCS40.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS39
            // 
            this.txtCS39.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS39.Location = new System.Drawing.Point(576, 68);
            this.txtCS39.Name = "txtCS39";
            this.txtCS39.Size = new System.Drawing.Size(74, 51);
            this.txtCS39.TabIndex = 39;
            this.txtCS39.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS38
            // 
            this.txtCS38.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS38.Location = new System.Drawing.Point(686, 68);
            this.txtCS38.Name = "txtCS38";
            this.txtCS38.Size = new System.Drawing.Size(74, 51);
            this.txtCS38.TabIndex = 38;
            this.txtCS38.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS37
            // 
            this.txtCS37.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS37.Location = new System.Drawing.Point(796, 68);
            this.txtCS37.Name = "txtCS37";
            this.txtCS37.Size = new System.Drawing.Size(74, 51);
            this.txtCS37.TabIndex = 37;
            this.txtCS37.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS36
            // 
            this.txtCS36.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS36.Location = new System.Drawing.Point(906, 68);
            this.txtCS36.Name = "txtCS36";
            this.txtCS36.Size = new System.Drawing.Size(74, 51);
            this.txtCS36.TabIndex = 36;
            this.txtCS36.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS35
            // 
            this.txtCS35.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS35.Location = new System.Drawing.Point(1016, 68);
            this.txtCS35.Name = "txtCS35";
            this.txtCS35.Size = new System.Drawing.Size(74, 51);
            this.txtCS35.TabIndex = 35;
            this.txtCS35.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS34
            // 
            this.txtCS34.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS34.Location = new System.Drawing.Point(1126, 68);
            this.txtCS34.Name = "txtCS34";
            this.txtCS34.Size = new System.Drawing.Size(74, 51);
            this.txtCS34.TabIndex = 34;
            this.txtCS34.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCS44
            // 
            this.lblCS44.BackColor = System.Drawing.SystemColors.Info;
            this.lblCS44.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS44.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS44.ForeColor = System.Drawing.Color.Red;
            this.lblCS44.Location = new System.Drawing.Point(26, 12);
            this.lblCS44.Name = "lblCS44";
            this.lblCS44.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS44.Size = new System.Drawing.Size(74, 53);
            this.lblCS44.TabIndex = 144;
            this.lblCS44.Text = "ْ\r\n";
            this.lblCS44.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS43
            // 
            this.lblCS43.BackColor = System.Drawing.SystemColors.Info;
            this.lblCS43.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS43.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS43.ForeColor = System.Drawing.Color.Red;
            this.lblCS43.Location = new System.Drawing.Point(136, 12);
            this.lblCS43.Name = "lblCS43";
            this.lblCS43.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS43.Size = new System.Drawing.Size(74, 53);
            this.lblCS43.TabIndex = 143;
            this.lblCS43.Text = "ّ\r\n";
            this.lblCS43.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS42
            // 
            this.lblCS42.BackColor = System.Drawing.SystemColors.Info;
            this.lblCS42.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS42.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS42.ForeColor = System.Drawing.Color.Red;
            this.lblCS42.Location = new System.Drawing.Point(246, 12);
            this.lblCS42.Name = "lblCS42";
            this.lblCS42.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS42.Size = new System.Drawing.Size(74, 53);
            this.lblCS42.TabIndex = 142;
            this.lblCS42.Text = "ِ\r\n";
            this.lblCS42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS41
            // 
            this.lblCS41.BackColor = System.Drawing.SystemColors.Info;
            this.lblCS41.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS41.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS41.ForeColor = System.Drawing.Color.Red;
            this.lblCS41.Location = new System.Drawing.Point(356, 12);
            this.lblCS41.Name = "lblCS41";
            this.lblCS41.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS41.Size = new System.Drawing.Size(74, 53);
            this.lblCS41.TabIndex = 141;
            this.lblCS41.Text = "ُ\r\n";
            this.lblCS41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS40
            // 
            this.lblCS40.BackColor = System.Drawing.SystemColors.Info;
            this.lblCS40.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS40.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS40.ForeColor = System.Drawing.Color.Red;
            this.lblCS40.Location = new System.Drawing.Point(466, 12);
            this.lblCS40.Name = "lblCS40";
            this.lblCS40.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS40.Size = new System.Drawing.Size(74, 53);
            this.lblCS40.TabIndex = 140;
            this.lblCS40.Text = "َ\r\n";
            this.lblCS40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS39
            // 
            this.lblCS39.BackColor = System.Drawing.SystemColors.Info;
            this.lblCS39.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS39.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS39.ForeColor = System.Drawing.Color.Red;
            this.lblCS39.Location = new System.Drawing.Point(576, 12);
            this.lblCS39.Name = "lblCS39";
            this.lblCS39.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS39.Size = new System.Drawing.Size(74, 53);
            this.lblCS39.TabIndex = 139;
            this.lblCS39.Text = "ٍ";
            this.lblCS39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS38
            // 
            this.lblCS38.BackColor = System.Drawing.SystemColors.Info;
            this.lblCS38.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS38.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS38.ForeColor = System.Drawing.Color.Red;
            this.lblCS38.Location = new System.Drawing.Point(686, 12);
            this.lblCS38.Name = "lblCS38";
            this.lblCS38.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS38.Size = new System.Drawing.Size(74, 53);
            this.lblCS38.TabIndex = 138;
            this.lblCS38.Text = "ٌ\r\n";
            this.lblCS38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS37
            // 
            this.lblCS37.BackColor = System.Drawing.SystemColors.Info;
            this.lblCS37.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS37.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS37.ForeColor = System.Drawing.Color.Red;
            this.lblCS37.Location = new System.Drawing.Point(796, 12);
            this.lblCS37.Name = "lblCS37";
            this.lblCS37.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS37.Size = new System.Drawing.Size(74, 53);
            this.lblCS37.TabIndex = 137;
            this.lblCS37.Text = "ً\r\n";
            this.lblCS37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS36
            // 
            this.lblCS36.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS36.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS36.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS36.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS36.Location = new System.Drawing.Point(906, 12);
            this.lblCS36.Name = "lblCS36";
            this.lblCS36.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS36.Size = new System.Drawing.Size(74, 53);
            this.lblCS36.TabIndex = 136;
            this.lblCS36.Text = "ي\r\n";
            this.lblCS36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS35
            // 
            this.lblCS35.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS35.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS35.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS35.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS35.Location = new System.Drawing.Point(1016, 12);
            this.lblCS35.Name = "lblCS35";
            this.lblCS35.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS35.Size = new System.Drawing.Size(74, 53);
            this.lblCS35.TabIndex = 135;
            this.lblCS35.Text = "ى\r\n";
            this.lblCS35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS34
            // 
            this.lblCS34.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS34.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS34.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS34.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS34.Location = new System.Drawing.Point(1126, 12);
            this.lblCS34.Name = "lblCS34";
            this.lblCS34.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS34.Size = new System.Drawing.Size(74, 53);
            this.lblCS34.TabIndex = 134;
            this.lblCS34.Text = "و\r\n";
            this.lblCS34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtCS33);
            this.panel3.Controls.Add(this.txtCS32);
            this.panel3.Controls.Add(this.txtCS31);
            this.panel3.Controls.Add(this.txtCS30);
            this.panel3.Controls.Add(this.txtCS29);
            this.panel3.Controls.Add(this.txtCS28);
            this.panel3.Controls.Add(this.txtCS27);
            this.panel3.Controls.Add(this.txtCS26);
            this.panel3.Controls.Add(this.txtCS25);
            this.panel3.Controls.Add(this.txtCS24);
            this.panel3.Controls.Add(this.txtCS23);
            this.panel3.Controls.Add(this.lblCS33);
            this.panel3.Controls.Add(this.lblCS32);
            this.panel3.Controls.Add(this.lblCS31);
            this.panel3.Controls.Add(this.lblCS30);
            this.panel3.Controls.Add(this.lblCS29);
            this.panel3.Controls.Add(this.lblCS28);
            this.panel3.Controls.Add(this.lblCS27);
            this.panel3.Controls.Add(this.lblCS26);
            this.panel3.Controls.Add(this.lblCS25);
            this.panel3.Controls.Add(this.lblCS24);
            this.panel3.Controls.Add(this.lblCS23);
            this.panel3.Location = new System.Drawing.Point(52, 267);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1225, 130);
            this.panel3.TabIndex = 1;
            // 
            // txtCS33
            // 
            this.txtCS33.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS33.Location = new System.Drawing.Point(26, 68);
            this.txtCS33.Name = "txtCS33";
            this.txtCS33.Size = new System.Drawing.Size(74, 51);
            this.txtCS33.TabIndex = 33;
            this.txtCS33.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS32
            // 
            this.txtCS32.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS32.Location = new System.Drawing.Point(136, 68);
            this.txtCS32.Name = "txtCS32";
            this.txtCS32.Size = new System.Drawing.Size(74, 51);
            this.txtCS32.TabIndex = 32;
            this.txtCS32.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS31
            // 
            this.txtCS31.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS31.Location = new System.Drawing.Point(246, 68);
            this.txtCS31.Name = "txtCS31";
            this.txtCS31.Size = new System.Drawing.Size(74, 51);
            this.txtCS31.TabIndex = 31;
            this.txtCS31.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS30
            // 
            this.txtCS30.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS30.Location = new System.Drawing.Point(356, 68);
            this.txtCS30.Name = "txtCS30";
            this.txtCS30.Size = new System.Drawing.Size(74, 51);
            this.txtCS30.TabIndex = 30;
            this.txtCS30.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS29
            // 
            this.txtCS29.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS29.Location = new System.Drawing.Point(466, 68);
            this.txtCS29.Name = "txtCS29";
            this.txtCS29.Size = new System.Drawing.Size(74, 51);
            this.txtCS29.TabIndex = 29;
            this.txtCS29.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS28
            // 
            this.txtCS28.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS28.Location = new System.Drawing.Point(576, 68);
            this.txtCS28.Name = "txtCS28";
            this.txtCS28.Size = new System.Drawing.Size(74, 51);
            this.txtCS28.TabIndex = 28;
            this.txtCS28.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS27
            // 
            this.txtCS27.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS27.Location = new System.Drawing.Point(686, 68);
            this.txtCS27.Name = "txtCS27";
            this.txtCS27.Size = new System.Drawing.Size(74, 51);
            this.txtCS27.TabIndex = 27;
            this.txtCS27.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS26
            // 
            this.txtCS26.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS26.Location = new System.Drawing.Point(796, 68);
            this.txtCS26.Name = "txtCS26";
            this.txtCS26.Size = new System.Drawing.Size(74, 51);
            this.txtCS26.TabIndex = 26;
            this.txtCS26.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS25
            // 
            this.txtCS25.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS25.Location = new System.Drawing.Point(906, 68);
            this.txtCS25.Name = "txtCS25";
            this.txtCS25.Size = new System.Drawing.Size(74, 51);
            this.txtCS25.TabIndex = 25;
            this.txtCS25.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS24
            // 
            this.txtCS24.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS24.Location = new System.Drawing.Point(1016, 68);
            this.txtCS24.Name = "txtCS24";
            this.txtCS24.Size = new System.Drawing.Size(74, 51);
            this.txtCS24.TabIndex = 24;
            this.txtCS24.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS23
            // 
            this.txtCS23.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS23.Location = new System.Drawing.Point(1126, 68);
            this.txtCS23.Name = "txtCS23";
            this.txtCS23.Size = new System.Drawing.Size(74, 51);
            this.txtCS23.TabIndex = 23;
            this.txtCS23.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCS33
            // 
            this.lblCS33.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS33.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS33.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS33.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS33.Location = new System.Drawing.Point(26, 12);
            this.lblCS33.Name = "lblCS33";
            this.lblCS33.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS33.Size = new System.Drawing.Size(74, 53);
            this.lblCS33.TabIndex = 133;
            this.lblCS33.Text = "ه\r\n";
            this.lblCS33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS32
            // 
            this.lblCS32.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS32.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS32.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS32.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS32.Location = new System.Drawing.Point(136, 12);
            this.lblCS32.Name = "lblCS32";
            this.lblCS32.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS32.Size = new System.Drawing.Size(74, 53);
            this.lblCS32.TabIndex = 132;
            this.lblCS32.Text = "ن\r\n";
            this.lblCS32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS31
            // 
            this.lblCS31.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS31.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS31.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS31.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS31.Location = new System.Drawing.Point(246, 12);
            this.lblCS31.Name = "lblCS31";
            this.lblCS31.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS31.Size = new System.Drawing.Size(74, 53);
            this.lblCS31.TabIndex = 131;
            this.lblCS31.Text = "م\r\n";
            this.lblCS31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS30
            // 
            this.lblCS30.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS30.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS30.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS30.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS30.Location = new System.Drawing.Point(356, 12);
            this.lblCS30.Name = "lblCS30";
            this.lblCS30.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS30.Size = new System.Drawing.Size(74, 53);
            this.lblCS30.TabIndex = 130;
            this.lblCS30.Text = "ل\r\n";
            this.lblCS30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS29
            // 
            this.lblCS29.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS29.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS29.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS29.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS29.Location = new System.Drawing.Point(466, 12);
            this.lblCS29.Name = "lblCS29";
            this.lblCS29.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS29.Size = new System.Drawing.Size(74, 53);
            this.lblCS29.TabIndex = 129;
            this.lblCS29.Text = "ك\r\n";
            this.lblCS29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS28
            // 
            this.lblCS28.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS28.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS28.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS28.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS28.Location = new System.Drawing.Point(576, 12);
            this.lblCS28.Name = "lblCS28";
            this.lblCS28.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS28.Size = new System.Drawing.Size(74, 53);
            this.lblCS28.TabIndex = 128;
            this.lblCS28.Text = "ق\r\n";
            this.lblCS28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS27
            // 
            this.lblCS27.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS27.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS27.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS27.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS27.Location = new System.Drawing.Point(686, 12);
            this.lblCS27.Name = "lblCS27";
            this.lblCS27.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS27.Size = new System.Drawing.Size(74, 53);
            this.lblCS27.TabIndex = 127;
            this.lblCS27.Text = "ف\r\n";
            this.lblCS27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS26
            // 
            this.lblCS26.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS26.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS26.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS26.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lblCS26.Location = new System.Drawing.Point(796, 12);
            this.lblCS26.Name = "lblCS26";
            this.lblCS26.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS26.Size = new System.Drawing.Size(74, 53);
            this.lblCS26.TabIndex = 126;
            this.lblCS26.Text = "غ\r\n";
            this.lblCS26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS25
            // 
            this.lblCS25.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS25.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS25.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS25.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS25.Location = new System.Drawing.Point(906, 12);
            this.lblCS25.Name = "lblCS25";
            this.lblCS25.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS25.Size = new System.Drawing.Size(74, 53);
            this.lblCS25.TabIndex = 125;
            this.lblCS25.Text = "ع\r\n";
            this.lblCS25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS24
            // 
            this.lblCS24.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS24.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS24.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS24.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lblCS24.Location = new System.Drawing.Point(1016, 12);
            this.lblCS24.Name = "lblCS24";
            this.lblCS24.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS24.Size = new System.Drawing.Size(74, 53);
            this.lblCS24.TabIndex = 124;
            this.lblCS24.Text = "ظ\r\n";
            this.lblCS24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS23
            // 
            this.lblCS23.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS23.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS23.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS23.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS23.Location = new System.Drawing.Point(1126, 12);
            this.lblCS23.Name = "lblCS23";
            this.lblCS23.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS23.Size = new System.Drawing.Size(74, 53);
            this.lblCS23.TabIndex = 123;
            this.lblCS23.Text = "ط\r\n";
            this.lblCS23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtCS22);
            this.panel2.Controls.Add(this.txtCS21);
            this.panel2.Controls.Add(this.txtCS20);
            this.panel2.Controls.Add(this.txtCS19);
            this.panel2.Controls.Add(this.txtCS18);
            this.panel2.Controls.Add(this.txtCS17);
            this.panel2.Controls.Add(this.txtCS16);
            this.panel2.Controls.Add(this.txtCS15);
            this.panel2.Controls.Add(this.txtCS14);
            this.panel2.Controls.Add(this.txtCS13);
            this.panel2.Controls.Add(this.txtCS12);
            this.panel2.Controls.Add(this.lblCS22);
            this.panel2.Controls.Add(this.lblCS21);
            this.panel2.Controls.Add(this.lblCS20);
            this.panel2.Controls.Add(this.lblCS19);
            this.panel2.Controls.Add(this.lblCS18);
            this.panel2.Controls.Add(this.lblCS17);
            this.panel2.Controls.Add(this.lblCS16);
            this.panel2.Controls.Add(this.lblCS15);
            this.panel2.Controls.Add(this.lblCS14);
            this.panel2.Controls.Add(this.lblCS13);
            this.panel2.Controls.Add(this.lblCS12);
            this.panel2.Location = new System.Drawing.Point(52, 138);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1225, 130);
            this.panel2.TabIndex = 1;
            // 
            // txtCS22
            // 
            this.txtCS22.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS22.Location = new System.Drawing.Point(26, 68);
            this.txtCS22.Name = "txtCS22";
            this.txtCS22.Size = new System.Drawing.Size(74, 51);
            this.txtCS22.TabIndex = 22;
            this.txtCS22.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS21
            // 
            this.txtCS21.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS21.Location = new System.Drawing.Point(136, 68);
            this.txtCS21.Name = "txtCS21";
            this.txtCS21.Size = new System.Drawing.Size(74, 51);
            this.txtCS21.TabIndex = 21;
            this.txtCS21.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS20
            // 
            this.txtCS20.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS20.Location = new System.Drawing.Point(246, 68);
            this.txtCS20.Name = "txtCS20";
            this.txtCS20.Size = new System.Drawing.Size(74, 51);
            this.txtCS20.TabIndex = 20;
            this.txtCS20.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS19
            // 
            this.txtCS19.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS19.Location = new System.Drawing.Point(356, 68);
            this.txtCS19.Name = "txtCS19";
            this.txtCS19.Size = new System.Drawing.Size(74, 51);
            this.txtCS19.TabIndex = 19;
            this.txtCS19.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS18
            // 
            this.txtCS18.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS18.Location = new System.Drawing.Point(466, 68);
            this.txtCS18.Name = "txtCS18";
            this.txtCS18.Size = new System.Drawing.Size(74, 51);
            this.txtCS18.TabIndex = 18;
            this.txtCS18.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS17
            // 
            this.txtCS17.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS17.Location = new System.Drawing.Point(576, 68);
            this.txtCS17.Name = "txtCS17";
            this.txtCS17.Size = new System.Drawing.Size(74, 51);
            this.txtCS17.TabIndex = 17;
            this.txtCS17.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS16
            // 
            this.txtCS16.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS16.Location = new System.Drawing.Point(686, 68);
            this.txtCS16.Name = "txtCS16";
            this.txtCS16.Size = new System.Drawing.Size(74, 51);
            this.txtCS16.TabIndex = 16;
            this.txtCS16.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS15
            // 
            this.txtCS15.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS15.Location = new System.Drawing.Point(796, 68);
            this.txtCS15.Name = "txtCS15";
            this.txtCS15.Size = new System.Drawing.Size(74, 51);
            this.txtCS15.TabIndex = 15;
            this.txtCS15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS14
            // 
            this.txtCS14.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS14.Location = new System.Drawing.Point(906, 68);
            this.txtCS14.Name = "txtCS14";
            this.txtCS14.Size = new System.Drawing.Size(74, 51);
            this.txtCS14.TabIndex = 14;
            this.txtCS14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS13
            // 
            this.txtCS13.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS13.Location = new System.Drawing.Point(1016, 68);
            this.txtCS13.Name = "txtCS13";
            this.txtCS13.Size = new System.Drawing.Size(74, 51);
            this.txtCS13.TabIndex = 13;
            this.txtCS13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS12
            // 
            this.txtCS12.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS12.Location = new System.Drawing.Point(1126, 68);
            this.txtCS12.Name = "txtCS12";
            this.txtCS12.Size = new System.Drawing.Size(74, 51);
            this.txtCS12.TabIndex = 12;
            this.txtCS12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCS22
            // 
            this.lblCS22.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS22.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS22.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lblCS22.Location = new System.Drawing.Point(26, 12);
            this.lblCS22.Name = "lblCS22";
            this.lblCS22.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS22.Size = new System.Drawing.Size(74, 53);
            this.lblCS22.TabIndex = 122;
            this.lblCS22.Text = "ض\r\n";
            this.lblCS22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS21
            // 
            this.lblCS21.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS21.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS21.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS21.Location = new System.Drawing.Point(136, 12);
            this.lblCS21.Name = "lblCS21";
            this.lblCS21.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS21.Size = new System.Drawing.Size(74, 53);
            this.lblCS21.TabIndex = 121;
            this.lblCS21.Text = "ص\r\n";
            this.lblCS21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS20
            // 
            this.lblCS20.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS20.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS20.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS20.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS20.Location = new System.Drawing.Point(246, 12);
            this.lblCS20.Name = "lblCS20";
            this.lblCS20.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS20.Size = new System.Drawing.Size(74, 53);
            this.lblCS20.TabIndex = 120;
            this.lblCS20.Text = "ش\r\n";
            this.lblCS20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS19
            // 
            this.lblCS19.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS19.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS19.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS19.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS19.Location = new System.Drawing.Point(356, 12);
            this.lblCS19.Name = "lblCS19";
            this.lblCS19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS19.Size = new System.Drawing.Size(74, 53);
            this.lblCS19.TabIndex = 119;
            this.lblCS19.Text = "س\r\n";
            this.lblCS19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS18
            // 
            this.lblCS18.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS18.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS18.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS18.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS18.Location = new System.Drawing.Point(466, 12);
            this.lblCS18.Name = "lblCS18";
            this.lblCS18.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS18.Size = new System.Drawing.Size(74, 53);
            this.lblCS18.TabIndex = 118;
            this.lblCS18.Text = "ز\r\n";
            this.lblCS18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS17
            // 
            this.lblCS17.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS17.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS17.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS17.Location = new System.Drawing.Point(576, 12);
            this.lblCS17.Name = "lblCS17";
            this.lblCS17.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS17.Size = new System.Drawing.Size(74, 53);
            this.lblCS17.TabIndex = 117;
            this.lblCS17.Text = "ر\r\n";
            this.lblCS17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS16
            // 
            this.lblCS16.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS16.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS16.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lblCS16.Location = new System.Drawing.Point(686, 12);
            this.lblCS16.Name = "lblCS16";
            this.lblCS16.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS16.Size = new System.Drawing.Size(74, 53);
            this.lblCS16.TabIndex = 116;
            this.lblCS16.Text = "ذ\r\n";
            this.lblCS16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS15
            // 
            this.lblCS15.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS15.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS15.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS15.Location = new System.Drawing.Point(796, 12);
            this.lblCS15.Name = "lblCS15";
            this.lblCS15.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS15.Size = new System.Drawing.Size(74, 53);
            this.lblCS15.TabIndex = 115;
            this.lblCS15.Text = "د\r\n";
            this.lblCS15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS14
            // 
            this.lblCS14.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS14.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS14.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lblCS14.Location = new System.Drawing.Point(906, 12);
            this.lblCS14.Name = "lblCS14";
            this.lblCS14.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS14.Size = new System.Drawing.Size(74, 53);
            this.lblCS14.TabIndex = 114;
            this.lblCS14.Text = "خ\r\n";
            this.lblCS14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS13
            // 
            this.lblCS13.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS13.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS13.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS13.Location = new System.Drawing.Point(1016, 12);
            this.lblCS13.Name = "lblCS13";
            this.lblCS13.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS13.Size = new System.Drawing.Size(74, 53);
            this.lblCS13.TabIndex = 113;
            this.lblCS13.Text = "ح\r\n";
            this.lblCS13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS12
            // 
            this.lblCS12.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS12.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS12.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS12.Location = new System.Drawing.Point(1126, 12);
            this.lblCS12.Name = "lblCS12";
            this.lblCS12.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS12.Size = new System.Drawing.Size(74, 53);
            this.lblCS12.TabIndex = 112;
            this.lblCS12.Text = "ج\r\n";
            this.lblCS12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtCS11);
            this.panel1.Controls.Add(this.txtCS10);
            this.panel1.Controls.Add(this.txtCS9);
            this.panel1.Controls.Add(this.txtCS8);
            this.panel1.Controls.Add(this.txtCS7);
            this.panel1.Controls.Add(this.txtCS6);
            this.panel1.Controls.Add(this.txtCS5);
            this.panel1.Controls.Add(this.txtCS4);
            this.panel1.Controls.Add(this.txtCS3);
            this.panel1.Controls.Add(this.txtCS2);
            this.panel1.Controls.Add(this.txtCS1);
            this.panel1.Controls.Add(this.lblCS11);
            this.panel1.Controls.Add(this.lblCS10);
            this.panel1.Controls.Add(this.lblCS9);
            this.panel1.Controls.Add(this.lblCS8);
            this.panel1.Controls.Add(this.lblCS7);
            this.panel1.Controls.Add(this.lblCS6);
            this.panel1.Controls.Add(this.lblCS5);
            this.panel1.Controls.Add(this.lblCS4);
            this.panel1.Controls.Add(this.lblCS3);
            this.panel1.Controls.Add(this.lblCS2);
            this.panel1.Controls.Add(this.lblCS1);
            this.panel1.Location = new System.Drawing.Point(52, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1225, 130);
            this.panel1.TabIndex = 1;
            // 
            // txtCS11
            // 
            this.txtCS11.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS11.Location = new System.Drawing.Point(26, 68);
            this.txtCS11.Name = "txtCS11";
            this.txtCS11.Size = new System.Drawing.Size(74, 51);
            this.txtCS11.TabIndex = 11;
            this.txtCS11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS10
            // 
            this.txtCS10.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS10.Location = new System.Drawing.Point(136, 68);
            this.txtCS10.Name = "txtCS10";
            this.txtCS10.Size = new System.Drawing.Size(74, 51);
            this.txtCS10.TabIndex = 10;
            this.txtCS10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS9
            // 
            this.txtCS9.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS9.Location = new System.Drawing.Point(246, 68);
            this.txtCS9.Name = "txtCS9";
            this.txtCS9.Size = new System.Drawing.Size(74, 51);
            this.txtCS9.TabIndex = 9;
            this.txtCS9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS8
            // 
            this.txtCS8.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS8.Location = new System.Drawing.Point(356, 68);
            this.txtCS8.Name = "txtCS8";
            this.txtCS8.Size = new System.Drawing.Size(74, 51);
            this.txtCS8.TabIndex = 8;
            this.txtCS8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS7
            // 
            this.txtCS7.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS7.Location = new System.Drawing.Point(466, 68);
            this.txtCS7.Name = "txtCS7";
            this.txtCS7.Size = new System.Drawing.Size(74, 51);
            this.txtCS7.TabIndex = 7;
            this.txtCS7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS6
            // 
            this.txtCS6.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS6.Location = new System.Drawing.Point(576, 68);
            this.txtCS6.Name = "txtCS6";
            this.txtCS6.Size = new System.Drawing.Size(74, 51);
            this.txtCS6.TabIndex = 6;
            this.txtCS6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS5
            // 
            this.txtCS5.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS5.Location = new System.Drawing.Point(686, 68);
            this.txtCS5.Name = "txtCS5";
            this.txtCS5.Size = new System.Drawing.Size(74, 51);
            this.txtCS5.TabIndex = 5;
            this.txtCS5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS4
            // 
            this.txtCS4.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS4.Location = new System.Drawing.Point(796, 68);
            this.txtCS4.Name = "txtCS4";
            this.txtCS4.Size = new System.Drawing.Size(74, 51);
            this.txtCS4.TabIndex = 4;
            this.txtCS4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS3
            // 
            this.txtCS3.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS3.Location = new System.Drawing.Point(906, 68);
            this.txtCS3.Name = "txtCS3";
            this.txtCS3.Size = new System.Drawing.Size(74, 51);
            this.txtCS3.TabIndex = 3;
            this.txtCS3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS2
            // 
            this.txtCS2.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS2.Location = new System.Drawing.Point(1016, 68);
            this.txtCS2.Name = "txtCS2";
            this.txtCS2.Size = new System.Drawing.Size(74, 51);
            this.txtCS2.TabIndex = 2;
            this.txtCS2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCS1
            // 
            this.txtCS1.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCS1.Location = new System.Drawing.Point(1126, 68);
            this.txtCS1.Name = "txtCS1";
            this.txtCS1.Size = new System.Drawing.Size(74, 51);
            this.txtCS1.TabIndex = 1;
            this.txtCS1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCS11
            // 
            this.lblCS11.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS11.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS11.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lblCS11.Location = new System.Drawing.Point(26, 12);
            this.lblCS11.Name = "lblCS11";
            this.lblCS11.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS11.Size = new System.Drawing.Size(74, 53);
            this.lblCS11.TabIndex = 111;
            this.lblCS11.Text = "ث\r\n";
            this.lblCS11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS10
            // 
            this.lblCS10.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS10.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS10.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS10.Location = new System.Drawing.Point(136, 12);
            this.lblCS10.Name = "lblCS10";
            this.lblCS10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS10.Size = new System.Drawing.Size(74, 53);
            this.lblCS10.TabIndex = 110;
            this.lblCS10.Text = "ت\r\n";
            this.lblCS10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS9
            // 
            this.lblCS9.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS9.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS9.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS9.Location = new System.Drawing.Point(246, 12);
            this.lblCS9.Name = "lblCS9";
            this.lblCS9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS9.Size = new System.Drawing.Size(74, 53);
            this.lblCS9.TabIndex = 109;
            this.lblCS9.Text = "ة\r\n";
            this.lblCS9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS8
            // 
            this.lblCS8.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS8.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS8.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS8.Location = new System.Drawing.Point(356, 12);
            this.lblCS8.Name = "lblCS8";
            this.lblCS8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS8.Size = new System.Drawing.Size(74, 53);
            this.lblCS8.TabIndex = 108;
            this.lblCS8.Text = "ب\r\n";
            this.lblCS8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS7
            // 
            this.lblCS7.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS7.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS7.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS7.Location = new System.Drawing.Point(466, 12);
            this.lblCS7.Name = "lblCS7";
            this.lblCS7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS7.Size = new System.Drawing.Size(74, 53);
            this.lblCS7.TabIndex = 107;
            this.lblCS7.Text = "ا\r\n";
            this.lblCS7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS6
            // 
            this.lblCS6.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS6.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS6.Location = new System.Drawing.Point(576, 12);
            this.lblCS6.Name = "lblCS6";
            this.lblCS6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS6.Size = new System.Drawing.Size(74, 53);
            this.lblCS6.TabIndex = 106;
            this.lblCS6.Text = "ئ\r\n";
            this.lblCS6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS5
            // 
            this.lblCS5.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS5.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS5.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS5.Location = new System.Drawing.Point(686, 12);
            this.lblCS5.Name = "lblCS5";
            this.lblCS5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS5.Size = new System.Drawing.Size(74, 53);
            this.lblCS5.TabIndex = 105;
            this.lblCS5.Text = "إ\r\n";
            this.lblCS5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS4
            // 
            this.lblCS4.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS4.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS4.Location = new System.Drawing.Point(796, 12);
            this.lblCS4.Name = "lblCS4";
            this.lblCS4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS4.Size = new System.Drawing.Size(74, 53);
            this.lblCS4.TabIndex = 104;
            this.lblCS4.Text = "ؤ\r\n";
            this.lblCS4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS3
            // 
            this.lblCS3.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS3.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS3.Location = new System.Drawing.Point(906, 12);
            this.lblCS3.Name = "lblCS3";
            this.lblCS3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS3.Size = new System.Drawing.Size(74, 53);
            this.lblCS3.TabIndex = 103;
            this.lblCS3.Text = "أ\r\n";
            this.lblCS3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS2
            // 
            this.lblCS2.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS2.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS2.Location = new System.Drawing.Point(1016, 12);
            this.lblCS2.Name = "lblCS2";
            this.lblCS2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS2.Size = new System.Drawing.Size(74, 53);
            this.lblCS2.TabIndex = 102;
            this.lblCS2.Text = "آ";
            this.lblCS2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCS1
            // 
            this.lblCS1.BackColor = System.Drawing.Color.LightYellow;
            this.lblCS1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCS1.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCS1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCS1.Location = new System.Drawing.Point(1126, 12);
            this.lblCS1.Name = "lblCS1";
            this.lblCS1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCS1.Size = new System.Drawing.Size(74, 53);
            this.lblCS1.TabIndex = 101;
            this.lblCS1.Text = "ء";
            this.lblCS1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabHexViewer
            // 
            this.tabHexViewer.AllowDrop = true;
            this.tabHexViewer.BackColor = System.Drawing.Color.LightGray;
            this.tabHexViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabHexViewer.Controls.Add(this.hexBox);
            this.tabHexViewer.Controls.Add(this.lblHash);
            this.tabHexViewer.Controls.Add(this.cmbHHash);
            this.tabHexViewer.Controls.Add(this.cmbHEncoding);
            this.tabHexViewer.Controls.Add(this.label45);
            this.tabHexViewer.Controls.Add(this.cmbBytesPerLine);
            this.tabHexViewer.Controls.Add(this.label46);
            this.tabHexViewer.Controls.Add(this.label44);
            this.tabHexViewer.Controls.Add(this.lblHexFile);
            this.tabHexViewer.Controls.Add(this.btnClearHex);
            this.tabHexViewer.Controls.Add(this.btnApplyChanges);
            this.tabHexViewer.Controls.Add(this.btnCalcHHash);
            this.tabHexViewer.Controls.Add(this.btnSendToCrypto);
            this.tabHexViewer.Controls.Add(this.btnSendToCalc);
            this.tabHexViewer.Controls.Add(this.btnOpenHexFile);
            this.tabHexViewer.Location = new System.Drawing.Point(4, 28);
            this.tabHexViewer.Name = "tabHexViewer";
            this.tabHexViewer.Size = new System.Drawing.Size(1331, 663);
            this.tabHexViewer.TabIndex = 7;
            this.tabHexViewer.Text = "Hex Viewer";
            this.tabHexViewer.DragDrop += new System.Windows.Forms.DragEventHandler(this.tabHexViewer_DragDrop);
            this.tabHexViewer.DragOver += new System.Windows.Forms.DragEventHandler(this.tabHexViewer_DragOver);
            // 
            // hexBox
            // 
            this.hexBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.hexBox.Location = new System.Drawing.Point(34, 68);
            this.hexBox.Name = "hexBox";
            this.hexBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox.Size = new System.Drawing.Size(1268, 520);
            this.hexBox.TabIndex = 59;
            this.hexBox.TextChanged += new System.EventHandler(this.hexBox_TextChanged);
            this.hexBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.hexBox_DragDrop);
            this.hexBox.DragOver += new System.Windows.Forms.DragEventHandler(this.hexBox_DragOver);
            // 
            // lblHash
            // 
            this.lblHash.BackColor = System.Drawing.Color.Ivory;
            this.lblHash.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHash.Location = new System.Drawing.Point(34, 623);
            this.lblHash.Name = "lblHash";
            this.lblHash.ReadOnly = true;
            this.lblHash.Size = new System.Drawing.Size(1269, 29);
            this.lblHash.TabIndex = 10;
            this.lblHash.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmbHHash
            // 
            this.cmbHHash.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHHash.FormattingEnabled = true;
            this.cmbHHash.Items.AddRange(new object[] {
            "SHA1",
            "SHA256",
            "SHA384",
            "SHA512",
            "MD5"});
            this.cmbHHash.Location = new System.Drawing.Point(514, 39);
            this.cmbHHash.Name = "cmbHHash";
            this.cmbHHash.Size = new System.Drawing.Size(137, 27);
            this.cmbHHash.TabIndex = 2;
            this.cmbHHash.SelectedIndexChanged += new System.EventHandler(this.cmbHHash_SelectedIndexChanged);
            // 
            // cmbHEncoding
            // 
            this.cmbHEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHEncoding.FormattingEnabled = true;
            this.cmbHEncoding.Location = new System.Drawing.Point(34, 39);
            this.cmbHEncoding.Name = "cmbHEncoding";
            this.cmbHEncoding.Size = new System.Drawing.Size(365, 27);
            this.cmbHEncoding.TabIndex = 0;
            this.cmbHEncoding.SelectedIndexChanged += new System.EventHandler(this.cmbHEncoding_SelectedIndexChanged);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(192, 17);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(74, 19);
            this.label45.TabIndex = 58;
            this.label45.Text = "Encoding";
            // 
            // cmbBytesPerLine
            // 
            this.cmbBytesPerLine.FormattingEnabled = true;
            this.cmbBytesPerLine.Items.AddRange(new object[] {
            "8",
            "16",
            "24"});
            this.cmbBytesPerLine.Location = new System.Drawing.Point(405, 39);
            this.cmbBytesPerLine.Name = "cmbBytesPerLine";
            this.cmbBytesPerLine.Size = new System.Drawing.Size(103, 27);
            this.cmbBytesPerLine.TabIndex = 1;
            this.cmbBytesPerLine.SelectedIndexChanged += new System.EventHandler(this.cmbBytesPerLine_SelectedIndexChanged);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(521, 17);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(120, 19);
            this.label46.TabIndex = 58;
            this.label46.Text = "Hash Algorithm";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(421, 17);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(81, 19);
            this.label44.TabIndex = 1;
            this.label44.Text = "Bytes/Line";
            // 
            // lblHexFile
            // 
            this.lblHexFile.BackColor = System.Drawing.Color.Ivory;
            this.lblHexFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHexFile.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHexFile.Location = new System.Drawing.Point(34, 588);
            this.lblHexFile.Name = "lblHexFile";
            this.lblHexFile.Size = new System.Drawing.Size(1269, 35);
            this.lblHexFile.TabIndex = 9;
            this.lblHexFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClearHex
            // 
            this.btnClearHex.Location = new System.Drawing.Point(1203, 28);
            this.btnClearHex.Name = "btnClearHex";
            this.btnClearHex.Size = new System.Drawing.Size(100, 40);
            this.btnClearHex.TabIndex = 7;
            this.btnClearHex.Text = "Clear";
            this.btnClearHex.UseVisualStyleBackColor = true;
            this.btnClearHex.Click += new System.EventHandler(this.btnClearHex_Click);
            // 
            // btnApplyChanges
            // 
            this.btnApplyChanges.Location = new System.Drawing.Point(1097, 28);
            this.btnApplyChanges.Name = "btnApplyChanges";
            this.btnApplyChanges.Size = new System.Drawing.Size(100, 40);
            this.btnApplyChanges.TabIndex = 6;
            this.btnApplyChanges.Text = "Apply";
            this.btnApplyChanges.UseVisualStyleBackColor = true;
            this.btnApplyChanges.Click += new System.EventHandler(this.btnApplyChanges_Click);
            // 
            // btnCalcHHash
            // 
            this.btnCalcHHash.Location = new System.Drawing.Point(673, 28);
            this.btnCalcHHash.Name = "btnCalcHHash";
            this.btnCalcHHash.Size = new System.Drawing.Size(100, 40);
            this.btnCalcHHash.TabIndex = 3;
            this.btnCalcHHash.Text = "Calc Hash";
            this.btnCalcHHash.UseVisualStyleBackColor = true;
            this.btnCalcHHash.Click += new System.EventHandler(this.btnCalcHHash_Click);
            // 
            // btnSendToCrypto
            // 
            this.btnSendToCrypto.Location = new System.Drawing.Point(885, 28);
            this.btnSendToCrypto.Name = "btnSendToCrypto";
            this.btnSendToCrypto.Size = new System.Drawing.Size(100, 40);
            this.btnSendToCrypto.TabIndex = 4;
            this.btnSendToCrypto.Text = "> Crypto";
            this.btnSendToCrypto.UseVisualStyleBackColor = true;
            this.btnSendToCrypto.Click += new System.EventHandler(this.btnSendToCrypto_Click);
            // 
            // btnSendToCalc
            // 
            this.btnSendToCalc.Location = new System.Drawing.Point(779, 28);
            this.btnSendToCalc.Name = "btnSendToCalc";
            this.btnSendToCalc.Size = new System.Drawing.Size(100, 40);
            this.btnSendToCalc.TabIndex = 4;
            this.btnSendToCalc.Text = "> Calc";
            this.btnSendToCalc.UseVisualStyleBackColor = true;
            this.btnSendToCalc.Click += new System.EventHandler(this.btnSendToCalc_Click);
            // 
            // btnOpenHexFile
            // 
            this.btnOpenHexFile.Location = new System.Drawing.Point(991, 28);
            this.btnOpenHexFile.Name = "btnOpenHexFile";
            this.btnOpenHexFile.Size = new System.Drawing.Size(100, 40);
            this.btnOpenHexFile.TabIndex = 5;
            this.btnOpenHexFile.Text = "Open File";
            this.btnOpenHexFile.UseVisualStyleBackColor = true;
            this.btnOpenHexFile.Click += new System.EventHandler(this.btnOpenHexFile_Click);
            // 
            // tabXRay
            // 
            this.tabXRay.BackColor = System.Drawing.Color.LightGray;
            this.tabXRay.Controls.Add(this.chkINV);
            this.tabXRay.Controls.Add(this.chkFlipX);
            this.tabXRay.Controls.Add(this.chkFlipY);
            this.tabXRay.Controls.Add(this.chkFixPadding);
            this.tabXRay.Controls.Add(this.lblPointSize);
            this.tabXRay.Controls.Add(this.btnSMinus);
            this.tabXRay.Controls.Add(this.btnSPlus);
            this.tabXRay.Controls.Add(this.label49);
            this.tabXRay.Controls.Add(this.label47);
            this.tabXRay.Controls.Add(this.cmbSizeMode);
            this.tabXRay.Controls.Add(this.btnScreenShot);
            this.tabXRay.Controls.Add(this.btnScan);
            this.tabXRay.Controls.Add(this.btnSaveImage);
            this.tabXRay.Controls.Add(this.btnResetImage);
            this.tabXRay.Controls.Add(this.btnRotate);
            this.tabXRay.Controls.Add(this.picQuran2);
            this.tabXRay.Controls.Add(this.picQuran1);
            this.tabXRay.Location = new System.Drawing.Point(4, 28);
            this.tabXRay.Name = "tabXRay";
            this.tabXRay.Size = new System.Drawing.Size(1331, 663);
            this.tabXRay.TabIndex = 8;
            this.tabXRay.Text = "X-Ray";
            this.tabXRay.Click += new System.EventHandler(this.tabImage_Click);
            // 
            // chkINV
            // 
            this.chkINV.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkINV.Location = new System.Drawing.Point(230, 615);
            this.chkINV.Name = "chkINV";
            this.chkINV.Size = new System.Drawing.Size(55, 40);
            this.chkINV.TabIndex = 14;
            this.chkINV.Text = "INV";
            this.chkINV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkINV.UseVisualStyleBackColor = true;
            this.chkINV.CheckedChanged += new System.EventHandler(this.chkINV_CheckedChanged);
            // 
            // chkFlipX
            // 
            this.chkFlipX.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkFlipX.Location = new System.Drawing.Point(177, 615);
            this.chkFlipX.Name = "chkFlipX";
            this.chkFlipX.Size = new System.Drawing.Size(55, 40);
            this.chkFlipX.TabIndex = 14;
            this.chkFlipX.Text = "FlipX";
            this.chkFlipX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkFlipX.UseVisualStyleBackColor = true;
            this.chkFlipX.CheckedChanged += new System.EventHandler(this.chkFlipX_CheckedChanged);
            // 
            // chkFlipY
            // 
            this.chkFlipY.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkFlipY.Location = new System.Drawing.Point(125, 615);
            this.chkFlipY.Name = "chkFlipY";
            this.chkFlipY.Size = new System.Drawing.Size(55, 40);
            this.chkFlipY.TabIndex = 14;
            this.chkFlipY.Text = "FlipY";
            this.chkFlipY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkFlipY.UseVisualStyleBackColor = true;
            this.chkFlipY.CheckedChanged += new System.EventHandler(this.chkFlipY_CheckedChanged);
            // 
            // chkFixPadding
            // 
            this.chkFixPadding.AutoSize = true;
            this.chkFixPadding.Location = new System.Drawing.Point(976, 625);
            this.chkFixPadding.Name = "chkFixPadding";
            this.chkFixPadding.Size = new System.Drawing.Size(92, 23);
            this.chkFixPadding.TabIndex = 13;
            this.chkFixPadding.Text = "Padding";
            this.chkFixPadding.UseVisualStyleBackColor = true;
            this.chkFixPadding.CheckedChanged += new System.EventHandler(this.chkFixSquare_CheckedChanged);
            // 
            // lblPointSize
            // 
            this.lblPointSize.BackColor = System.Drawing.Color.White;
            this.lblPointSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPointSize.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblPointSize.Location = new System.Drawing.Point(1196, 618);
            this.lblPointSize.Name = "lblPointSize";
            this.lblPointSize.Size = new System.Drawing.Size(58, 33);
            this.lblPointSize.TabIndex = 11;
            this.lblPointSize.Text = "1";
            this.lblPointSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPointSize.TextChanged += new System.EventHandler(this.lblScale_TextChanged);
            this.lblPointSize.Click += new System.EventHandler(this.lblScale_Click);
            // 
            // btnSMinus
            // 
            this.btnSMinus.Location = new System.Drawing.Point(1160, 617);
            this.btnSMinus.Name = "btnSMinus";
            this.btnSMinus.Size = new System.Drawing.Size(38, 37);
            this.btnSMinus.TabIndex = 10;
            this.btnSMinus.Text = "-";
            this.btnSMinus.UseVisualStyleBackColor = true;
            this.btnSMinus.Click += new System.EventHandler(this.btnSMinus_Click);
            // 
            // btnSPlus
            // 
            this.btnSPlus.Location = new System.Drawing.Point(1253, 617);
            this.btnSPlus.Name = "btnSPlus";
            this.btnSPlus.Size = new System.Drawing.Size(38, 37);
            this.btnSPlus.TabIndex = 12;
            this.btnSPlus.Text = "+";
            this.btnSPlus.UseVisualStyleBackColor = true;
            this.btnSPlus.Click += new System.EventHandler(this.btnSPlus_Click);
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(1073, 626);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(84, 19);
            this.label49.TabIndex = 3;
            this.label49.Text = "PointSize :";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(689, 626);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(91, 19);
            this.label47.TabIndex = 3;
            this.label47.Text = "Size Mode :";
            // 
            // cmbSizeMode
            // 
            this.cmbSizeMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSizeMode.FormattingEnabled = true;
            this.cmbSizeMode.Items.AddRange(new object[] {
            "Normal",
            "StrechImage",
            "AutoSize",
            "CenterImage",
            "Zoom"});
            this.cmbSizeMode.Location = new System.Drawing.Point(786, 623);
            this.cmbSizeMode.Name = "cmbSizeMode";
            this.cmbSizeMode.Size = new System.Drawing.Size(184, 27);
            this.cmbSizeMode.TabIndex = 2;
            this.cmbSizeMode.SelectedIndexChanged += new System.EventHandler(this.cmbSizeMode_SelectedIndexChanged);
            // 
            // btnScreenShot
            // 
            this.btnScreenShot.Location = new System.Drawing.Point(549, 615);
            this.btnScreenShot.Name = "btnScreenShot";
            this.btnScreenShot.Size = new System.Drawing.Size(89, 40);
            this.btnScreenShot.TabIndex = 1;
            this.btnScreenShot.Text = "ScrnShot";
            this.btnScreenShot.UseVisualStyleBackColor = true;
            this.btnScreenShot.Click += new System.EventHandler(this.btnScreenShot_Click);
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(463, 615);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(80, 40);
            this.btnScan.TabIndex = 1;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Location = new System.Drawing.Point(377, 615);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(80, 40);
            this.btnSaveImage.TabIndex = 1;
            this.btnSaveImage.Text = "Save";
            this.btnSaveImage.UseVisualStyleBackColor = true;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // btnResetImage
            // 
            this.btnResetImage.Location = new System.Drawing.Point(291, 615);
            this.btnResetImage.Name = "btnResetImage";
            this.btnResetImage.Size = new System.Drawing.Size(80, 40);
            this.btnResetImage.TabIndex = 1;
            this.btnResetImage.Text = "Reset";
            this.btnResetImage.UseVisualStyleBackColor = true;
            this.btnResetImage.Click += new System.EventHandler(this.btnResetImage_Click);
            // 
            // btnRotate
            // 
            this.btnRotate.Location = new System.Drawing.Point(38, 615);
            this.btnRotate.Name = "btnRotate";
            this.btnRotate.Size = new System.Drawing.Size(90, 40);
            this.btnRotate.TabIndex = 1;
            this.btnRotate.Text = "Rotate >";
            this.btnRotate.UseVisualStyleBackColor = true;
            this.btnRotate.Click += new System.EventHandler(this.btnRotate_Click);
            // 
            // picQuran2
            // 
            this.picQuran2.BackColor = System.Drawing.Color.Black;
            this.picQuran2.Location = new System.Drawing.Point(693, 6);
            this.picQuran2.Name = "picQuran2";
            this.picQuran2.Size = new System.Drawing.Size(600, 600);
            this.picQuran2.TabIndex = 0;
            this.picQuran2.TabStop = false;
            // 
            // picQuran1
            // 
            this.picQuran1.BackColor = System.Drawing.Color.Black;
            this.picQuran1.Location = new System.Drawing.Point(38, 6);
            this.picQuran1.Name = "picQuran1";
            this.picQuran1.Size = new System.Drawing.Size(600, 600);
            this.picQuran1.TabIndex = 0;
            this.picQuran1.TabStop = false;
            this.picQuran1.Click += new System.EventHandler(this.picQuran1_Click);
            this.picQuran1.DoubleClick += new System.EventHandler(this.picQuran1_DoubleClick);
            // 
            // tabSpectrum
            // 
            this.tabSpectrum.BackColor = System.Drawing.SystemColors.Control;
            this.tabSpectrum.Controls.Add(this.splitContainer1);
            this.tabSpectrum.Location = new System.Drawing.Point(4, 28);
            this.tabSpectrum.Name = "tabSpectrum";
            this.tabSpectrum.Size = new System.Drawing.Size(1331, 663);
            this.tabSpectrum.TabIndex = 9;
            this.tabSpectrum.Text = "Spectrum";
            this.tabSpectrum.Enter += new System.EventHandler(this.tabSpectrum_Enter);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.LightGray;
            this.splitContainer1.Panel1.Controls.Add(this.spectrumRButton);
            this.splitContainer1.Panel1.Controls.Add(this.waveRButton);
            this.splitContainer1.Panel1.Controls.Add(this.label51);
            this.splitContainer1.Panel1.Controls.Add(this.label50);
            this.splitContainer1.Panel1.Controls.Add(this.label48);
            this.splitContainer1.Panel1.Controls.Add(this.cmbChannels);
            this.splitContainer1.Panel1.Controls.Add(this.cmbBits);
            this.splitContainer1.Panel1.Controls.Add(this.cmbSampleRate);
            this.splitContainer1.Panel1.Controls.Add(this.chkPlay);
            this.splitContainer1.Panel1.Controls.Add(this.stopButton);
            this.splitContainer1.Panel1.Controls.Add(this.btnResetSpectrum);
            this.splitContainer1.Panel1.Controls.Add(this.btnStop);
            this.splitContainer1.Panel1.Controls.Add(this.btnPlay);
            this.splitContainer1.Panel1.Controls.Add(this.startButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.canvas);
            this.splitContainer1.Size = new System.Drawing.Size(1335, 663);
            this.splitContainer1.SplitterDistance = 185;
            this.splitContainer1.TabIndex = 1;
            // 
            // spectrumRButton
            // 
            this.spectrumRButton.AutoSize = true;
            this.spectrumRButton.Checked = true;
            this.spectrumRButton.Location = new System.Drawing.Point(29, 382);
            this.spectrumRButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.spectrumRButton.Name = "spectrumRButton";
            this.spectrumRButton.Size = new System.Drawing.Size(101, 23);
            this.spectrumRButton.TabIndex = 17;
            this.spectrumRButton.TabStop = true;
            this.spectrumRButton.Text = "Spectrum";
            this.spectrumRButton.UseVisualStyleBackColor = true;
            this.spectrumRButton.CheckedChanged += new System.EventHandler(this.spectrumRButton_CheckedChanged);
            // 
            // waveRButton
            // 
            this.waveRButton.AutoSize = true;
            this.waveRButton.Location = new System.Drawing.Point(29, 349);
            this.waveRButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.waveRButton.Name = "waveRButton";
            this.waveRButton.Size = new System.Drawing.Size(101, 23);
            this.waveRButton.TabIndex = 16;
            this.waveRButton.Text = "Raw Data";
            this.waveRButton.UseVisualStyleBackColor = true;
            this.waveRButton.CheckedChanged += new System.EventHandler(this.waveRButton_CheckedChanged);
            // 
            // label51
            // 
            this.label51.Location = new System.Drawing.Point(31, 144);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(109, 25);
            this.label51.TabIndex = 15;
            this.label51.Text = "Channels";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label50
            // 
            this.label50.Location = new System.Drawing.Point(31, 77);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(109, 25);
            this.label50.TabIndex = 15;
            this.label50.Text = "Bits";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label48
            // 
            this.label48.Location = new System.Drawing.Point(32, 17);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(109, 25);
            this.label48.TabIndex = 15;
            this.label48.Text = "Sample Rate";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbChannels
            // 
            this.cmbChannels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChannels.FormattingEnabled = true;
            this.cmbChannels.Items.AddRange(new object[] {
            "Mono",
            "Stereo"});
            this.cmbChannels.Location = new System.Drawing.Point(29, 172);
            this.cmbChannels.Name = "cmbChannels";
            this.cmbChannels.Size = new System.Drawing.Size(112, 27);
            this.cmbChannels.TabIndex = 14;
            this.cmbChannels.SelectedIndexChanged += new System.EventHandler(this.cmbChannels_SelectedIndexChanged);
            // 
            // cmbBits
            // 
            this.cmbBits.FormattingEnabled = true;
            this.cmbBits.Items.AddRange(new object[] {
            "8",
            "16"});
            this.cmbBits.Location = new System.Drawing.Point(29, 105);
            this.cmbBits.Name = "cmbBits";
            this.cmbBits.Size = new System.Drawing.Size(112, 27);
            this.cmbBits.TabIndex = 14;
            this.cmbBits.SelectedIndexChanged += new System.EventHandler(this.cmbBits_SelectedIndexChanged);
            this.cmbBits.TextChanged += new System.EventHandler(this.cmbBits_SelectedIndexChanged);
            // 
            // cmbSampleRate
            // 
            this.cmbSampleRate.FormattingEnabled = true;
            this.cmbSampleRate.Items.AddRange(new object[] {
            "8000",
            "11025",
            "22050",
            "32000",
            "44100",
            "48000"});
            this.cmbSampleRate.Location = new System.Drawing.Point(30, 42);
            this.cmbSampleRate.Name = "cmbSampleRate";
            this.cmbSampleRate.Size = new System.Drawing.Size(112, 27);
            this.cmbSampleRate.TabIndex = 14;
            this.cmbSampleRate.SelectedIndexChanged += new System.EventHandler(this.cmbSampleRate_SelectedIndexChanged);
            this.cmbSampleRate.TextChanged += new System.EventHandler(this.cmbSampleRate_SelectedIndexChanged);
            // 
            // chkPlay
            // 
            this.chkPlay.AutoSize = true;
            this.chkPlay.Location = new System.Drawing.Point(29, 426);
            this.chkPlay.Name = "chkPlay";
            this.chkPlay.Size = new System.Drawing.Size(134, 23);
            this.chkPlay.TabIndex = 13;
            this.chkPlay.Text = "Play && Record";
            this.chkPlay.UseVisualStyleBackColor = true;
            this.chkPlay.CheckedChanged += new System.EventHandler(this.chkPlay_CheckedChanged);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(30, 285);
            this.stopButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(112, 36);
            this.stopButton.TabIndex = 10;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // btnResetSpectrum
            // 
            this.btnResetSpectrum.Location = new System.Drawing.Point(28, 595);
            this.btnResetSpectrum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnResetSpectrum.Name = "btnResetSpectrum";
            this.btnResetSpectrum.Size = new System.Drawing.Size(112, 36);
            this.btnResetSpectrum.TabIndex = 9;
            this.btnResetSpectrum.Text = "Reset";
            this.btnResetSpectrum.UseVisualStyleBackColor = true;
            this.btnResetSpectrum.Click += new System.EventHandler(this.btnResetSpectrum_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(28, 549);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(112, 36);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnStop_KeyDown);
            this.btnStop.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnStop_KeyUp);
            this.btnStop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnStop_KeyDown);
            this.btnStop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnStop_KeyUp);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(28, 503);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(112, 36);
            this.btnPlay.TabIndex = 9;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(30, 239);
            this.startButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(112, 36);
            this.startButton.TabIndex = 9;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.White;
            this.canvas.Location = new System.Drawing.Point(3, 0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(1143, 663);
            this.canvas.TabIndex = 1;
            this.canvas.TabStop = false;
            this.canvas.Click += new System.EventHandler(this.canvas_Click);
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            // 
            // tabColor
            // 
            this.tabColor.Controls.Add(this.texture2);
            this.tabColor.Controls.Add(this.texture);
            this.tabColor.Location = new System.Drawing.Point(4, 28);
            this.tabColor.Name = "tabColor";
            this.tabColor.Size = new System.Drawing.Size(1331, 663);
            this.tabColor.TabIndex = 10;
            this.tabColor.Text = "Color";
            this.tabColor.UseVisualStyleBackColor = true;
            // 
            // texture
            // 
            this.texture.BackColor = System.Drawing.Color.Black;
            this.texture.Location = new System.Drawing.Point(11, 10);
            this.texture.Name = "texture";
            this.texture.Size = new System.Drawing.Size(640, 640);
            this.texture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.texture.TabIndex = 0;
            this.texture.TabStop = false;
            // 
            // lstLog
            // 
            this.lstLog.FormattingEnabled = true;
            this.lstLog.ItemHeight = 19;
            this.lstLog.Location = new System.Drawing.Point(19, 732);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(1339, 137);
            this.lstLog.TabIndex = 0;
            // 
            // txtInfo
            // 
            this.txtInfo.BackColor = System.Drawing.SystemColors.Info;
            this.txtInfo.Location = new System.Drawing.Point(1377, 59);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.Size = new System.Drawing.Size(561, 775);
            this.txtInfo.TabIndex = 1;
            this.txtInfo.TextChanged += new System.EventHandler(this.txtInfo_TextChanged);
            this.txtInfo.DoubleClick += new System.EventHandler(this.txtInfo_DoubleClick);
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label34.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label34.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(1377, 27);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(561, 32);
            this.label34.TabIndex = 7;
            this.label34.Text = "Details";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.FloralWhite;
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatus.Location = new System.Drawing.Point(1377, 837);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(561, 32);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // texture2
            // 
            this.texture2.BackColor = System.Drawing.Color.Black;
            this.texture2.Location = new System.Drawing.Point(679, 10);
            this.texture2.Name = "texture2";
            this.texture2.Size = new System.Drawing.Size(640, 640);
            this.texture2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.texture2.TabIndex = 0;
            this.texture2.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1959, 885);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabCrypto.ResumeLayout(false);
            this.tabCrypto.PerformLayout();
            this.tabRSA.ResumeLayout(false);
            this.tabRSA.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabDSA.ResumeLayout(false);
            this.tabDSA.PerformLayout();
            this.tabCalculator.ResumeLayout(false);
            this.tabCalculator.PerformLayout();
            this.tabEncoding.ResumeLayout(false);
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.grpSource.ResumeLayout(false);
            this.grpEncodings.ResumeLayout(false);
            this.grpEncodings.PerformLayout();
            this.tabQuran.ResumeLayout(false);
            this.tabQuran.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuran)).EndInit();
            this.tabCharset.ResumeLayout(false);
            this.tabCharset.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabHexViewer.ResumeLayout(false);
            this.tabHexViewer.PerformLayout();
            this.tabXRay.ResumeLayout(false);
            this.tabXRay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQuran2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picQuran1)).EndInit();
            this.tabSpectrum.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.tabColor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.texture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.texture2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage tabCrypto;
        private System.Windows.Forms.ComboBox cmbKeyLength;
        private System.Windows.Forms.ComboBox cmbData;
        private System.Windows.Forms.ComboBox cmbEncryptionKey;
        private System.Windows.Forms.ComboBox cmbHash;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnHash;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.TextBox txtHash;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.TextBox txtPrivateKey;
        private System.Windows.Forms.TextBox txtPublicKey;
        private System.Windows.Forms.TabPage tabRSA;
        private System.Windows.Forms.TextBox txtN;
        private System.Windows.Forms.TextBox txtD;
        private System.Windows.Forms.TextBox txtE;
        private System.Windows.Forms.TextBox txtPEMPrivateKey;
        private System.Windows.Forms.Button btnExportPrivate;
        private System.Windows.Forms.Button btnExportPublic;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtInverseQ;
        private System.Windows.Forms.TextBox txtDQ;
        private System.Windows.Forms.TextBox txtDP;
        private System.Windows.Forms.TextBox txtQ;
        private System.Windows.Forms.TextBox txtP;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.RadioButton rbFileBuffer;
        private System.Windows.Forms.RadioButton rbTextBox;
        private System.Windows.Forms.Button btnSaveKeys;
        private System.Windows.Forms.Button btnLoadKeys;
        private System.Windows.Forms.CheckBox chkReverse;
        private System.Windows.Forms.Button btnUsePrivateKey;
        private System.Windows.Forms.Button btnUsePublicKey;
        private System.Windows.Forms.Button btnUseKeys;
        private System.Windows.Forms.TabPage tabCalculator;
        private System.Windows.Forms.Button btnRTP;
        private System.Windows.Forms.Button btnFCD;
        private System.Windows.Forms.Button btnLCM;
        private System.Windows.Forms.Button btnPWM;
        private System.Windows.Forms.Button btnPOW;
        private System.Windows.Forms.Button btnMOD;
        private System.Windows.Forms.Button btnDIV;
        private System.Windows.Forms.Button btnMUL;
        private System.Windows.Forms.Button btnSUB;
        private System.Windows.Forms.Button btnADD;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtResultR;
        private System.Windows.Forms.TextBox txtModulN;
        private System.Windows.Forms.TextBox txtPrimeQ;
        private System.Windows.Forms.TextBox txtPrimeP;
        private System.Windows.Forms.TabPage tabDSA;
        private System.Windows.Forms.Button btnDSAImportPublic;
        private System.Windows.Forms.Button btnExportDSA;
        private System.Windows.Forms.TextBox txtDSA_X;
        private System.Windows.Forms.TextBox txtDSA_Y;
        private System.Windows.Forms.TextBox txtDSA_G;
        private System.Windows.Forms.TextBox txtDSA_Q;
        private System.Windows.Forms.TextBox txtDSA_P;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtDSAPEMPublic;
        private System.Windows.Forms.Button btnGenDSAKeys;
        private System.Windows.Forms.ComboBox cmbDSAKeyLen;
        private System.Windows.Forms.Button btnDSAImportPrivate;
        private System.Windows.Forms.TextBox txtPEMPublicKey;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnImportPrivateKey;
        private System.Windows.Forms.Button btnImportPublicKey;
        private System.Windows.Forms.Button btnGenKeys;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.ComboBox cmbCryptoAlgorithm;
        private System.Windows.Forms.TextBox txtDSAPEMPrivate;
        private System.Windows.Forms.Button btnExportDSAPublic;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.ComboBox cmbRSAKeyLen;
        private System.Windows.Forms.CheckBox chkPadding;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TabPage tabEncoding;
        private System.Windows.Forms.GroupBox grpEncodings;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.CheckBox chkUnicodeAsDecimal;
        private System.Windows.Forms.CheckBox chkMeta;
        private System.Windows.Forms.TextBox txtDestEnc;
        private System.Windows.Forms.TextBox txtSourceEnc;
        private System.Windows.Forms.Label labDestEnc;
        private System.Windows.Forms.ComboBox cmbDestEnc;
        private System.Windows.Forms.ComboBox cmbSourceEnc;
        private System.Windows.Forms.Label labSourceEnc;
        private System.Windows.Forms.GroupBox grpSource;
        private System.Windows.Forms.ListBox lsSource;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.ProgressBar progFiles;
        private System.Windows.Forms.Button btnClearFiles;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.CheckBox chkHexText;
        private System.Windows.Forms.CheckBox chkOutText;
        private System.Windows.Forms.TextBox rtxtData;
        private System.Windows.Forms.CheckBox chkRTL;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.ComboBox cmbAccelerator;
        private System.Windows.Forms.TabPage tabQuran;
        private System.Windows.Forms.Button btnSendToEncoding;
        private System.Windows.Forms.TextBox txtQuranText;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.ComboBox cmbExponentMethod;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.ComboBox cmbMULMethod;
        private System.Windows.Forms.ListBox lbSoras;
        private System.Windows.Forms.CheckBox chkDiscardChars;
        private System.Windows.Forms.CheckBox chkzStrings;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Label lblFontSize;
        private System.Windows.Forms.Button btnIsPrime;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Button btnClearCalc;
        private System.Windows.Forms.Button btnFactorizeP;
        private System.Windows.Forms.Button btnSqrt;
        private System.Windows.Forms.CheckBox chkDiacritics;
        private System.Windows.Forms.CheckBox chkSendToBuffer;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnGenPrime;
        private System.Windows.Forms.Button btnSHR;
        private System.Windows.Forms.Button btnSHL;
        private System.Windows.Forms.Button btnBezout;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.ComboBox cmbKeyLen;
        private System.Windows.Forms.Button btnXOR;
        private System.Windows.Forms.Button btnOR;
        private System.Windows.Forms.Button btnReverseQ;
        private System.Windows.Forms.Button btnReverseP;
        private System.Windows.Forms.Button btnIsEven;
        private System.Windows.Forms.DataGridView dgvQuran;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Serial;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoraNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn AyaNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoraName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AyaText;
        private System.Windows.Forms.Button btnFactorDbP;
        private System.Windows.Forms.TabPage tabCharset;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtCS44;
        private System.Windows.Forms.TextBox txtCS43;
        private System.Windows.Forms.TextBox txtCS42;
        private System.Windows.Forms.TextBox txtCS41;
        private System.Windows.Forms.TextBox txtCS40;
        private System.Windows.Forms.TextBox txtCS39;
        private System.Windows.Forms.TextBox txtCS38;
        private System.Windows.Forms.TextBox txtCS37;
        private System.Windows.Forms.TextBox txtCS36;
        private System.Windows.Forms.TextBox txtCS35;
        private System.Windows.Forms.TextBox txtCS34;
        private System.Windows.Forms.Label lblCS44;
        private System.Windows.Forms.Label lblCS43;
        private System.Windows.Forms.Label lblCS42;
        private System.Windows.Forms.Label lblCS41;
        private System.Windows.Forms.Label lblCS40;
        private System.Windows.Forms.Label lblCS39;
        private System.Windows.Forms.Label lblCS38;
        private System.Windows.Forms.Label lblCS37;
        private System.Windows.Forms.Label lblCS36;
        private System.Windows.Forms.Label lblCS35;
        private System.Windows.Forms.Label lblCS34;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtCS33;
        private System.Windows.Forms.TextBox txtCS32;
        private System.Windows.Forms.TextBox txtCS31;
        private System.Windows.Forms.TextBox txtCS30;
        private System.Windows.Forms.TextBox txtCS29;
        private System.Windows.Forms.TextBox txtCS28;
        private System.Windows.Forms.TextBox txtCS27;
        private System.Windows.Forms.TextBox txtCS26;
        private System.Windows.Forms.TextBox txtCS25;
        private System.Windows.Forms.TextBox txtCS24;
        private System.Windows.Forms.TextBox txtCS23;
        private System.Windows.Forms.Label lblCS33;
        private System.Windows.Forms.Label lblCS32;
        private System.Windows.Forms.Label lblCS31;
        private System.Windows.Forms.Label lblCS30;
        private System.Windows.Forms.Label lblCS29;
        private System.Windows.Forms.Label lblCS28;
        private System.Windows.Forms.Label lblCS27;
        private System.Windows.Forms.Label lblCS26;
        private System.Windows.Forms.Label lblCS25;
        private System.Windows.Forms.Label lblCS24;
        private System.Windows.Forms.Label lblCS23;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtCS22;
        private System.Windows.Forms.TextBox txtCS21;
        private System.Windows.Forms.TextBox txtCS20;
        private System.Windows.Forms.TextBox txtCS19;
        private System.Windows.Forms.TextBox txtCS18;
        private System.Windows.Forms.TextBox txtCS17;
        private System.Windows.Forms.TextBox txtCS16;
        private System.Windows.Forms.TextBox txtCS15;
        private System.Windows.Forms.TextBox txtCS14;
        private System.Windows.Forms.TextBox txtCS13;
        private System.Windows.Forms.TextBox txtCS12;
        private System.Windows.Forms.Label lblCS22;
        private System.Windows.Forms.Label lblCS21;
        private System.Windows.Forms.Label lblCS20;
        private System.Windows.Forms.Label lblCS19;
        private System.Windows.Forms.Label lblCS18;
        private System.Windows.Forms.Label lblCS17;
        private System.Windows.Forms.Label lblCS16;
        private System.Windows.Forms.Label lblCS15;
        private System.Windows.Forms.Label lblCS14;
        private System.Windows.Forms.Label lblCS13;
        private System.Windows.Forms.Label lblCS12;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtCS11;
        private System.Windows.Forms.TextBox txtCS10;
        private System.Windows.Forms.TextBox txtCS9;
        private System.Windows.Forms.TextBox txtCS8;
        private System.Windows.Forms.TextBox txtCS7;
        private System.Windows.Forms.TextBox txtCS6;
        private System.Windows.Forms.TextBox txtCS5;
        private System.Windows.Forms.TextBox txtCS4;
        private System.Windows.Forms.TextBox txtCS3;
        private System.Windows.Forms.TextBox txtCS2;
        private System.Windows.Forms.TextBox txtCS1;
        private System.Windows.Forms.Label lblCS11;
        private System.Windows.Forms.Label lblCS10;
        private System.Windows.Forms.Label lblCS9;
        private System.Windows.Forms.Label lblCS8;
        private System.Windows.Forms.Label lblCS7;
        private System.Windows.Forms.Label lblCS6;
        private System.Windows.Forms.Label lblCS5;
        private System.Windows.Forms.Label lblCS4;
        private System.Windows.Forms.Label lblCS3;
        private System.Windows.Forms.Label lblCS2;
        private System.Windows.Forms.Label lblCS1;
        private System.Windows.Forms.Button btnAutoCharset;
        private System.Windows.Forms.Button btnLoadCharset;
        private System.Windows.Forms.Button btnSaveCharset;
        private System.Windows.Forms.Button btnClearCS;
        private System.Windows.Forms.RadioButton rbText;
        private System.Windows.Forms.RadioButton rbFiles;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton rb64b;
        private System.Windows.Forms.RadioButton rb16;
        private System.Windows.Forms.RadioButton rb10;
        private System.Windows.Forms.Label lblCurCharset;
        private System.Windows.Forms.CheckBox chkALLEncodings;
        private System.Windows.Forms.Button btnFactorizeQ;
        private System.Windows.Forms.Button btnFactorDbQ;
        private System.Windows.Forms.RadioButton rb2;
        private System.Windows.Forms.Button btnToHex;
        private System.Windows.Forms.RadioButton rbDiacritics;
        private System.Windows.Forms.RadioButton rbNoDiacritics;
        private System.Windows.Forms.RadioButton rbFirstOriginal;
        private System.Windows.Forms.RadioButton rbFirstOriginalDots;
        private System.Windows.Forms.TextBox txtPlus;
        private System.Windows.Forms.Button btnAutoAdd;
        private System.Windows.Forms.Button btnAutoSub;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblCSS29;
        private System.Windows.Forms.Label lblCSS28;
        private System.Windows.Forms.Label lblCSS27;
        private System.Windows.Forms.Label lblCSS26;
        private System.Windows.Forms.Label lblCSS25;
        private System.Windows.Forms.Label lblCSS24;
        private System.Windows.Forms.Label lblCSS23;
        private System.Windows.Forms.Label lblCSS22;
        private System.Windows.Forms.Label lblCSS21;
        private System.Windows.Forms.Label lblCSS20;
        private System.Windows.Forms.Label lblCSS19;
        private System.Windows.Forms.Label lblCSS18;
        private System.Windows.Forms.Label lblCSS17;
        private System.Windows.Forms.Label lblCSS16;
        private System.Windows.Forms.Label lblCSS15;
        private System.Windows.Forms.Label lblCSS14;
        private System.Windows.Forms.Label lblCSS13;
        private System.Windows.Forms.Label lblCSS12;
        private System.Windows.Forms.Label lblCSS11;
        private System.Windows.Forms.Label lblCSS10;
        private System.Windows.Forms.Label lblCSS9;
        private System.Windows.Forms.Label lblCSS8;
        private System.Windows.Forms.Label lblCSS7;
        private System.Windows.Forms.Label lblCSS6;
        private System.Windows.Forms.Label lblCSS5;
        private System.Windows.Forms.Label lblCSS4;
        private System.Windows.Forms.Label lblCSS3;
        private System.Windows.Forms.Label lblCSS2;
        private System.Windows.Forms.Label lblCSS1;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.Label lblCSS44;
        private System.Windows.Forms.Label lblCSS43;
        private System.Windows.Forms.Label lblCSS42;
        private System.Windows.Forms.Label lblCSS41;
        private System.Windows.Forms.Label lblCSS40;
        private System.Windows.Forms.Label lblCSS39;
        private System.Windows.Forms.Label lblCSS38;
        private System.Windows.Forms.Label lblCSS37;
        private System.Windows.Forms.Label lblCSS36;
        private System.Windows.Forms.Label lblCSS35;
        private System.Windows.Forms.Label lblCSS34;
        private System.Windows.Forms.Label lblCSS33;
        private System.Windows.Forms.Label lblCSS32;
        private System.Windows.Forms.Label lblCSS31;
        private System.Windows.Forms.Label lblCSS30;
        private System.Windows.Forms.CheckBox chkPositives;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabHexViewer;
        private System.Windows.Forms.Button btnClearHex;
        private System.Windows.Forms.Button btnOpenHexFile;
        private System.Windows.Forms.Label lblHexFile;
        private System.Windows.Forms.CheckBox chkJommalWord;
        private System.Windows.Forms.ComboBox cmbHEncoding;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.ComboBox cmbBytesPerLine;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Button btnApplyChanges;
        private System.Windows.Forms.Button btnSendToCalc;
        private System.Windows.Forms.ComboBox cmbHHash;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox lblHash;
        private System.Windows.Forms.Button btnCalcHHash;
        private System.Windows.Forms.Button btnSendToCrypto;
        private Be.Windows.Forms.HexBox hexBox;
        private System.Windows.Forms.Button btnSHex;
        private System.Windows.Forms.Button btnSCrypto;
        private System.Windows.Forms.Button btnSCalc;
        private System.Windows.Forms.Button btnSendToHex;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabPage tabXRay;
        private System.Windows.Forms.PictureBox picQuran1;
        private System.Windows.Forms.Button btnSImage;
        private System.Windows.Forms.PictureBox picQuran2;
        private System.Windows.Forms.Button btnRotate;
        private System.Windows.Forms.ComboBox cmbSizeMode;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label lblPointSize;
        private System.Windows.Forms.Button btnSMinus;
        private System.Windows.Forms.Button btnSPlus;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Button btnResetImage;
        private System.Windows.Forms.CheckBox chkFixPadding;
        private System.Windows.Forms.CheckBox chkFlipY;
        private System.Windows.Forms.CheckBox chkFlipX;
        private System.Windows.Forms.CheckBox chkINV;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnSaveImage;
        private System.Windows.Forms.TabPage tabSpectrum;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button btnSpectrum;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnScreenShot;
        private System.Windows.Forms.CheckBox chkPlay;
        private System.Windows.Forms.RadioButton spectrumRButton;
        private System.Windows.Forms.RadioButton waveRButton;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.ComboBox cmbChannels;
        private System.Windows.Forms.ComboBox cmbBits;
        private System.Windows.Forms.ComboBox cmbSampleRate;
        private System.Windows.Forms.Button btnResetSpectrum;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.TabPage tabColor;
        private System.Windows.Forms.PictureBox texture;
        private System.Windows.Forms.PictureBox texture2;
    }
}

