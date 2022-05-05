namespace Rabin_Cryptosystem_Application
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPrimesGen = new System.Windows.Forms.TabPage();
            this.tbPrimeGenLog = new System.Windows.Forms.TextBox();
            this.lbPrimeGenChoice = new System.Windows.Forms.Label();
            this.pnGenerationOptions = new System.Windows.Forms.Panel();
            this.rbtRandomStrings = new System.Windows.Forms.RadioButton();
            this.rbtRandomOrg = new System.Windows.Forms.RadioButton();
            this.btPrimeSave = new System.Windows.Forms.Button();
            this.btPrimeGen = new System.Windows.Forms.Button();
            this.cbPrimeGenLog = new System.Windows.Forms.CheckBox();
            this.tbText2 = new System.Windows.Forms.TextBox();
            this.tbText1 = new System.Windows.Forms.TextBox();
            this.lbText2 = new System.Windows.Forms.Label();
            this.lbText1 = new System.Windows.Forms.Label();
            this.tabKeyGen = new System.Windows.Forms.TabPage();
            this.tbKeyGenLog = new System.Windows.Forms.TextBox();
            this.btKeyGen = new System.Windows.Forms.Button();
            this.btKeySave = new System.Windows.Forms.Button();
            this.lbLoadPrimes = new System.Windows.Forms.Label();
            this.tbLoadPrime1 = new System.Windows.Forms.TextBox();
            this.tbLoadPrime2 = new System.Windows.Forms.TextBox();
            this.cbKeyGenLog = new System.Windows.Forms.CheckBox();
            this.btLoadPrime1 = new System.Windows.Forms.Button();
            this.btLoadPrime2 = new System.Windows.Forms.Button();
            this.tabEncryption = new System.Windows.Forms.TabPage();
            this.pbEncryption = new System.Windows.Forms.ProgressBar();
            this.btStartEncryption = new System.Windows.Forms.Button();
            this.tbPublicKey = new System.Windows.Forms.TextBox();
            this.btLoadPublicKey = new System.Windows.Forms.Button();
            this.lbLoadPublicKey = new System.Windows.Forms.Label();
            this.tbSelectEncryptionTarget = new System.Windows.Forms.TextBox();
            this.tbSelectEncryptionSource = new System.Windows.Forms.TextBox();
            this.btSelectEncryptionTarget = new System.Windows.Forms.Button();
            this.btSelectEncryptionSource = new System.Windows.Forms.Button();
            this.lbSelectEncryptionFiles = new System.Windows.Forms.Label();
            this.cbEncryptLog = new System.Windows.Forms.CheckBox();
            this.tbEncryptLog = new System.Windows.Forms.TextBox();
            this.tabDecryption = new System.Windows.Forms.TabPage();
            this.tbDecryptLog = new System.Windows.Forms.TextBox();
            this.pbDecryption = new System.Windows.Forms.ProgressBar();
            this.cbAutoDeterminePrivateKey = new System.Windows.Forms.CheckBox();
            this.btStartDecryption = new System.Windows.Forms.Button();
            this.tbPrivateKey = new System.Windows.Forms.TextBox();
            this.btLoadPrivateKey = new System.Windows.Forms.Button();
            this.lbLoadPrivateKey = new System.Windows.Forms.Label();
            this.tbSelectDecryptionTarget = new System.Windows.Forms.TextBox();
            this.tbSelectDecryptionSource = new System.Windows.Forms.TextBox();
            this.btSelectDecryptionTarget = new System.Windows.Forms.Button();
            this.btSelectDecryptionSource = new System.Windows.Forms.Button();
            this.lbSelectDecryptionFiles = new System.Windows.Forms.Label();
            this.cbDecryptLog = new System.Windows.Forms.CheckBox();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabMain.SuspendLayout();
            this.tabPrimesGen.SuspendLayout();
            this.pnGenerationOptions.SuspendLayout();
            this.tabKeyGen.SuspendLayout();
            this.tabEncryption.SuspendLayout();
            this.tabDecryption.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPrimesGen);
            this.tabMain.Controls.Add(this.tabKeyGen);
            this.tabMain.Controls.Add(this.tabEncryption);
            this.tabMain.Controls.Add(this.tabDecryption);
            this.tabMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabMain.Location = new System.Drawing.Point(0, 27);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(634, 450);
            this.tabMain.TabIndex = 0;
            // 
            // tabPrimesGen
            // 
            this.tabPrimesGen.Controls.Add(this.tbPrimeGenLog);
            this.tabPrimesGen.Controls.Add(this.lbPrimeGenChoice);
            this.tabPrimesGen.Controls.Add(this.pnGenerationOptions);
            this.tabPrimesGen.Controls.Add(this.btPrimeSave);
            this.tabPrimesGen.Controls.Add(this.btPrimeGen);
            this.tabPrimesGen.Controls.Add(this.cbPrimeGenLog);
            this.tabPrimesGen.Controls.Add(this.tbText2);
            this.tabPrimesGen.Controls.Add(this.tbText1);
            this.tabPrimesGen.Controls.Add(this.lbText2);
            this.tabPrimesGen.Controls.Add(this.lbText1);
            this.tabPrimesGen.Location = new System.Drawing.Point(4, 22);
            this.tabPrimesGen.Name = "tabPrimesGen";
            this.tabPrimesGen.Padding = new System.Windows.Forms.Padding(3);
            this.tabPrimesGen.Size = new System.Drawing.Size(626, 424);
            this.tabPrimesGen.TabIndex = 0;
            this.tabPrimesGen.Text = "Primes Generation";
            this.tabPrimesGen.UseVisualStyleBackColor = true;
            // 
            // tbPrimeGenLog
            // 
            this.tbPrimeGenLog.Location = new System.Drawing.Point(6, 269);
            this.tbPrimeGenLog.Multiline = true;
            this.tbPrimeGenLog.Name = "tbPrimeGenLog";
            this.tbPrimeGenLog.ReadOnly = true;
            this.tbPrimeGenLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbPrimeGenLog.Size = new System.Drawing.Size(612, 149);
            this.tbPrimeGenLog.TabIndex = 16;
            // 
            // lbPrimeGenChoice
            // 
            this.lbPrimeGenChoice.AutoSize = true;
            this.lbPrimeGenChoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrimeGenChoice.Location = new System.Drawing.Point(11, 35);
            this.lbPrimeGenChoice.Name = "lbPrimeGenChoice";
            this.lbPrimeGenChoice.Size = new System.Drawing.Size(289, 15);
            this.lbPrimeGenChoice.TabIndex = 8;
            this.lbPrimeGenChoice.Text = "Choose the means of generating the prime number:";
            // 
            // pnGenerationOptions
            // 
            this.pnGenerationOptions.Controls.Add(this.rbtRandomStrings);
            this.pnGenerationOptions.Controls.Add(this.rbtRandomOrg);
            this.pnGenerationOptions.Location = new System.Drawing.Point(14, 53);
            this.pnGenerationOptions.Name = "pnGenerationOptions";
            this.pnGenerationOptions.Size = new System.Drawing.Size(286, 92);
            this.pnGenerationOptions.TabIndex = 2;
            // 
            // rbtRandomStrings
            // 
            this.rbtRandomStrings.AutoSize = true;
            this.rbtRandomStrings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtRandomStrings.Location = new System.Drawing.Point(14, 51);
            this.rbtRandomStrings.Name = "rbtRandomStrings";
            this.rbtRandomStrings.Size = new System.Drawing.Size(172, 19);
            this.rbtRandomStrings.TabIndex = 1;
            this.rbtRandomStrings.Text = "Using random strings input";
            this.rbtRandomStrings.UseVisualStyleBackColor = true;
            this.rbtRandomStrings.CheckedChanged += new System.EventHandler(this.rbtRandomStrings_CheckedChanged);
            // 
            // rbtRandomOrg
            // 
            this.rbtRandomOrg.AutoSize = true;
            this.rbtRandomOrg.Checked = true;
            this.rbtRandomOrg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtRandomOrg.Location = new System.Drawing.Point(14, 3);
            this.rbtRandomOrg.Name = "rbtRandomOrg";
            this.rbtRandomOrg.Size = new System.Drawing.Size(236, 34);
            this.rbtRandomOrg.TabIndex = 0;
            this.rbtRandomOrg.TabStop = true;
            this.rbtRandomOrg.Text = "Using real-time atmospheric noise\r\n(requires an active Internet connection)";
            this.rbtRandomOrg.UseVisualStyleBackColor = true;
            this.rbtRandomOrg.CheckedChanged += new System.EventHandler(this.rbtRandomOrg_CheckedChanged);
            // 
            // btPrimeSave
            // 
            this.btPrimeSave.Enabled = false;
            this.btPrimeSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btPrimeSave.Location = new System.Drawing.Point(499, 229);
            this.btPrimeSave.Name = "btPrimeSave";
            this.btPrimeSave.Size = new System.Drawing.Size(104, 23);
            this.btPrimeSave.TabIndex = 7;
            this.btPrimeSave.Text = "Save Prime";
            this.btPrimeSave.UseVisualStyleBackColor = true;
            this.btPrimeSave.Click += new System.EventHandler(this.btPrimeSave_Click);
            // 
            // btPrimeGen
            // 
            this.btPrimeGen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btPrimeGen.Location = new System.Drawing.Point(293, 229);
            this.btPrimeGen.Name = "btPrimeGen";
            this.btPrimeGen.Size = new System.Drawing.Size(104, 23);
            this.btPrimeGen.TabIndex = 5;
            this.btPrimeGen.Text = "Generate Prime";
            this.btPrimeGen.UseVisualStyleBackColor = true;
            this.btPrimeGen.Click += new System.EventHandler(this.btPrimeGen_Click);
            // 
            // cbPrimeGenLog
            // 
            this.cbPrimeGenLog.AutoSize = true;
            this.cbPrimeGenLog.Checked = true;
            this.cbPrimeGenLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPrimeGenLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPrimeGenLog.Location = new System.Drawing.Point(28, 232);
            this.cbPrimeGenLog.Name = "cbPrimeGenLog";
            this.cbPrimeGenLog.Size = new System.Drawing.Size(170, 19);
            this.cbPrimeGenLog.TabIndex = 4;
            this.cbPrimeGenLog.Text = "View prime-generation log";
            this.cbPrimeGenLog.UseVisualStyleBackColor = true;
            this.cbPrimeGenLog.CheckedChanged += new System.EventHandler(this.cbPrimeGen_CheckedChanged);
            // 
            // tbText2
            // 
            this.tbText2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbText2.Location = new System.Drawing.Point(318, 110);
            this.tbText2.Multiline = true;
            this.tbText2.Name = "tbText2";
            this.tbText2.Size = new System.Drawing.Size(300, 52);
            this.tbText2.TabIndex = 3;
            this.tbText2.Visible = false;
            // 
            // tbText1
            // 
            this.tbText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbText1.Location = new System.Drawing.Point(318, 35);
            this.tbText1.Multiline = true;
            this.tbText1.Name = "tbText1";
            this.tbText1.Size = new System.Drawing.Size(300, 52);
            this.tbText1.TabIndex = 2;
            this.tbText1.Visible = false;
            // 
            // lbText2
            // 
            this.lbText2.AutoSize = true;
            this.lbText2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbText2.Location = new System.Drawing.Point(315, 92);
            this.lbText2.Name = "lbText2";
            this.lbText2.Size = new System.Drawing.Size(278, 15);
            this.lbText2.TabIndex = 1;
            this.lbText2.Text = "Enter the 2nd random text (at least 50 characters):";
            this.lbText2.Visible = false;
            // 
            // lbText1
            // 
            this.lbText1.AutoSize = true;
            this.lbText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbText1.Location = new System.Drawing.Point(315, 17);
            this.lbText1.Name = "lbText1";
            this.lbText1.Size = new System.Drawing.Size(273, 15);
            this.lbText1.TabIndex = 0;
            this.lbText1.Text = "Enter the 1st random text (at least 50 characters):";
            this.lbText1.Visible = false;
            // 
            // tabKeyGen
            // 
            this.tabKeyGen.Controls.Add(this.tbKeyGenLog);
            this.tabKeyGen.Controls.Add(this.btKeyGen);
            this.tabKeyGen.Controls.Add(this.btKeySave);
            this.tabKeyGen.Controls.Add(this.lbLoadPrimes);
            this.tabKeyGen.Controls.Add(this.tbLoadPrime1);
            this.tabKeyGen.Controls.Add(this.tbLoadPrime2);
            this.tabKeyGen.Controls.Add(this.cbKeyGenLog);
            this.tabKeyGen.Controls.Add(this.btLoadPrime1);
            this.tabKeyGen.Controls.Add(this.btLoadPrime2);
            this.tabKeyGen.Location = new System.Drawing.Point(4, 22);
            this.tabKeyGen.Name = "tabKeyGen";
            this.tabKeyGen.Padding = new System.Windows.Forms.Padding(3);
            this.tabKeyGen.Size = new System.Drawing.Size(626, 424);
            this.tabKeyGen.TabIndex = 1;
            this.tabKeyGen.Text = "Key Generation";
            this.tabKeyGen.UseVisualStyleBackColor = true;
            // 
            // tbKeyGenLog
            // 
            this.tbKeyGenLog.Location = new System.Drawing.Point(7, 266);
            this.tbKeyGenLog.Multiline = true;
            this.tbKeyGenLog.Name = "tbKeyGenLog";
            this.tbKeyGenLog.ReadOnly = true;
            this.tbKeyGenLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbKeyGenLog.Size = new System.Drawing.Size(612, 149);
            this.tbKeyGenLog.TabIndex = 18;
            // 
            // btKeyGen
            // 
            this.btKeyGen.Enabled = false;
            this.btKeyGen.Location = new System.Drawing.Point(283, 223);
            this.btKeyGen.Name = "btKeyGen";
            this.btKeyGen.Size = new System.Drawing.Size(100, 23);
            this.btKeyGen.TabIndex = 17;
            this.btKeyGen.Text = "Generate Key";
            this.btKeyGen.UseVisualStyleBackColor = true;
            this.btKeyGen.Click += new System.EventHandler(this.btKeyGen_Click);
            // 
            // btKeySave
            // 
            this.btKeySave.Enabled = false;
            this.btKeySave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btKeySave.Location = new System.Drawing.Point(502, 223);
            this.btKeySave.Name = "btKeySave";
            this.btKeySave.Size = new System.Drawing.Size(98, 23);
            this.btKeySave.TabIndex = 16;
            this.btKeySave.Text = "Save Key";
            this.btKeySave.UseVisualStyleBackColor = true;
            this.btKeySave.Click += new System.EventHandler(this.btKeySave_Click);
            // 
            // lbLoadPrimes
            // 
            this.lbLoadPrimes.AutoSize = true;
            this.lbLoadPrimes.Location = new System.Drawing.Point(8, 36);
            this.lbLoadPrimes.Name = "lbLoadPrimes";
            this.lbLoadPrimes.Size = new System.Drawing.Size(330, 15);
            this.lbLoadPrimes.TabIndex = 13;
            this.lbLoadPrimes.Text = "Choose which two different primes should generate the key:";
            // 
            // tbLoadPrime1
            // 
            this.tbLoadPrime1.Location = new System.Drawing.Point(121, 75);
            this.tbLoadPrime1.Name = "tbLoadPrime1";
            this.tbLoadPrime1.ReadOnly = true;
            this.tbLoadPrime1.Size = new System.Drawing.Size(490, 21);
            this.tbLoadPrime1.TabIndex = 12;
            // 
            // tbLoadPrime2
            // 
            this.tbLoadPrime2.Location = new System.Drawing.Point(121, 114);
            this.tbLoadPrime2.Name = "tbLoadPrime2";
            this.tbLoadPrime2.ReadOnly = true;
            this.tbLoadPrime2.Size = new System.Drawing.Size(490, 21);
            this.tbLoadPrime2.TabIndex = 11;
            // 
            // cbKeyGenLog
            // 
            this.cbKeyGenLog.AutoSize = true;
            this.cbKeyGenLog.Checked = true;
            this.cbKeyGenLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbKeyGenLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbKeyGenLog.Location = new System.Drawing.Point(25, 226);
            this.cbKeyGenLog.Name = "cbKeyGenLog";
            this.cbKeyGenLog.Size = new System.Drawing.Size(156, 19);
            this.cbKeyGenLog.TabIndex = 10;
            this.cbKeyGenLog.Text = "View key-generation log";
            this.cbKeyGenLog.UseVisualStyleBackColor = true;
            this.cbKeyGenLog.CheckedChanged += new System.EventHandler(this.cbKeyGen_CheckedChanged);
            // 
            // btLoadPrime1
            // 
            this.btLoadPrime1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btLoadPrime1.Location = new System.Drawing.Point(11, 73);
            this.btLoadPrime1.Name = "btLoadPrime1";
            this.btLoadPrime1.Size = new System.Drawing.Size(104, 23);
            this.btLoadPrime1.TabIndex = 8;
            this.btLoadPrime1.Text = "Load 1st prime";
            this.btLoadPrime1.UseVisualStyleBackColor = true;
            this.btLoadPrime1.Click += new System.EventHandler(this.btLoadPrime1_Click);
            // 
            // btLoadPrime2
            // 
            this.btLoadPrime2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btLoadPrime2.Location = new System.Drawing.Point(11, 112);
            this.btLoadPrime2.Name = "btLoadPrime2";
            this.btLoadPrime2.Size = new System.Drawing.Size(104, 23);
            this.btLoadPrime2.TabIndex = 7;
            this.btLoadPrime2.Text = "Load 2nd prime";
            this.btLoadPrime2.UseVisualStyleBackColor = true;
            this.btLoadPrime2.Click += new System.EventHandler(this.btLoadPrime2_Click);
            // 
            // tabEncryption
            // 
            this.tabEncryption.Controls.Add(this.pbEncryption);
            this.tabEncryption.Controls.Add(this.btStartEncryption);
            this.tabEncryption.Controls.Add(this.tbPublicKey);
            this.tabEncryption.Controls.Add(this.btLoadPublicKey);
            this.tabEncryption.Controls.Add(this.lbLoadPublicKey);
            this.tabEncryption.Controls.Add(this.tbSelectEncryptionTarget);
            this.tabEncryption.Controls.Add(this.tbSelectEncryptionSource);
            this.tabEncryption.Controls.Add(this.btSelectEncryptionTarget);
            this.tabEncryption.Controls.Add(this.btSelectEncryptionSource);
            this.tabEncryption.Controls.Add(this.lbSelectEncryptionFiles);
            this.tabEncryption.Controls.Add(this.cbEncryptLog);
            this.tabEncryption.Controls.Add(this.tbEncryptLog);
            this.tabEncryption.Location = new System.Drawing.Point(4, 22);
            this.tabEncryption.Name = "tabEncryption";
            this.tabEncryption.Padding = new System.Windows.Forms.Padding(3);
            this.tabEncryption.Size = new System.Drawing.Size(626, 424);
            this.tabEncryption.TabIndex = 2;
            this.tabEncryption.Text = "Encryption";
            this.tabEncryption.UseVisualStyleBackColor = true;
            // 
            // pbEncryption
            // 
            this.pbEncryption.Location = new System.Drawing.Point(6, 237);
            this.pbEncryption.Name = "pbEncryption";
            this.pbEncryption.Size = new System.Drawing.Size(612, 23);
            this.pbEncryption.Step = 100;
            this.pbEncryption.TabIndex = 26;
            // 
            // btStartEncryption
            // 
            this.btStartEncryption.Enabled = false;
            this.btStartEncryption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btStartEncryption.Location = new System.Drawing.Point(443, 195);
            this.btStartEncryption.Name = "btStartEncryption";
            this.btStartEncryption.Size = new System.Drawing.Size(104, 23);
            this.btStartEncryption.TabIndex = 25;
            this.btStartEncryption.Text = "Start Encryption";
            this.btStartEncryption.UseVisualStyleBackColor = true;
            this.btStartEncryption.Click += new System.EventHandler(this.btStartEncryption_Click);
            // 
            // tbPublicKey
            // 
            this.tbPublicKey.Location = new System.Drawing.Point(133, 45);
            this.tbPublicKey.Name = "tbPublicKey";
            this.tbPublicKey.ReadOnly = true;
            this.tbPublicKey.Size = new System.Drawing.Size(490, 21);
            this.tbPublicKey.TabIndex = 24;
            // 
            // btLoadPublicKey
            // 
            this.btLoadPublicKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btLoadPublicKey.Location = new System.Drawing.Point(11, 43);
            this.btLoadPublicKey.Name = "btLoadPublicKey";
            this.btLoadPublicKey.Size = new System.Drawing.Size(104, 23);
            this.btLoadPublicKey.TabIndex = 23;
            this.btLoadPublicKey.Text = "Load public key";
            this.btLoadPublicKey.UseVisualStyleBackColor = true;
            this.btLoadPublicKey.Click += new System.EventHandler(this.btLoadPublicKey_Click);
            // 
            // lbLoadPublicKey
            // 
            this.lbLoadPublicKey.AutoSize = true;
            this.lbLoadPublicKey.Location = new System.Drawing.Point(8, 14);
            this.lbLoadPublicKey.Name = "lbLoadPublicKey";
            this.lbLoadPublicKey.Size = new System.Drawing.Size(263, 15);
            this.lbLoadPublicKey.TabIndex = 22;
            this.lbLoadPublicKey.Text = "Choose a public key to generate the encryption:";
            // 
            // tbSelectEncryptionTarget
            // 
            this.tbSelectEncryptionTarget.Location = new System.Drawing.Point(133, 158);
            this.tbSelectEncryptionTarget.Name = "tbSelectEncryptionTarget";
            this.tbSelectEncryptionTarget.ReadOnly = true;
            this.tbSelectEncryptionTarget.Size = new System.Drawing.Size(490, 21);
            this.tbSelectEncryptionTarget.TabIndex = 21;
            // 
            // tbSelectEncryptionSource
            // 
            this.tbSelectEncryptionSource.Location = new System.Drawing.Point(133, 120);
            this.tbSelectEncryptionSource.Name = "tbSelectEncryptionSource";
            this.tbSelectEncryptionSource.ReadOnly = true;
            this.tbSelectEncryptionSource.Size = new System.Drawing.Size(490, 21);
            this.tbSelectEncryptionSource.TabIndex = 20;
            // 
            // btSelectEncryptionTarget
            // 
            this.btSelectEncryptionTarget.Enabled = false;
            this.btSelectEncryptionTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSelectEncryptionTarget.Location = new System.Drawing.Point(11, 156);
            this.btSelectEncryptionTarget.Name = "btSelectEncryptionTarget";
            this.btSelectEncryptionTarget.Size = new System.Drawing.Size(104, 23);
            this.btSelectEncryptionTarget.TabIndex = 19;
            this.btSelectEncryptionTarget.Text = "Select target";
            this.btSelectEncryptionTarget.UseVisualStyleBackColor = true;
            this.btSelectEncryptionTarget.Click += new System.EventHandler(this.btSelectEncryptionTarget_Click);
            // 
            // btSelectEncryptionSource
            // 
            this.btSelectEncryptionSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSelectEncryptionSource.Location = new System.Drawing.Point(11, 118);
            this.btSelectEncryptionSource.Name = "btSelectEncryptionSource";
            this.btSelectEncryptionSource.Size = new System.Drawing.Size(104, 23);
            this.btSelectEncryptionSource.TabIndex = 18;
            this.btSelectEncryptionSource.Text = "Select source";
            this.btSelectEncryptionSource.UseVisualStyleBackColor = true;
            this.btSelectEncryptionSource.Click += new System.EventHandler(this.btSelectEncryptionSource_Click);
            // 
            // lbSelectEncryptionFiles
            // 
            this.lbSelectEncryptionFiles.AutoSize = true;
            this.lbSelectEncryptionFiles.Location = new System.Drawing.Point(8, 87);
            this.lbSelectEncryptionFiles.Name = "lbSelectEncryptionFiles";
            this.lbSelectEncryptionFiles.Size = new System.Drawing.Size(303, 15);
            this.lbSelectEncryptionFiles.TabIndex = 17;
            this.lbSelectEncryptionFiles.Text = "Select the source and the target files for the encryption:";
            // 
            // cbEncryptLog
            // 
            this.cbEncryptLog.AutoSize = true;
            this.cbEncryptLog.Checked = true;
            this.cbEncryptLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEncryptLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEncryptLog.Location = new System.Drawing.Point(69, 199);
            this.cbEncryptLog.Name = "cbEncryptLog";
            this.cbEncryptLog.Size = new System.Drawing.Size(131, 19);
            this.cbEncryptLog.TabIndex = 16;
            this.cbEncryptLog.Text = "View encryption log";
            this.cbEncryptLog.UseVisualStyleBackColor = true;
            this.cbEncryptLog.CheckedChanged += new System.EventHandler(this.cbEncryptLog_CheckedChanged);
            // 
            // tbEncryptLog
            // 
            this.tbEncryptLog.Location = new System.Drawing.Point(6, 272);
            this.tbEncryptLog.Multiline = true;
            this.tbEncryptLog.Name = "tbEncryptLog";
            this.tbEncryptLog.ReadOnly = true;
            this.tbEncryptLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbEncryptLog.Size = new System.Drawing.Size(612, 149);
            this.tbEncryptLog.TabIndex = 15;
            // 
            // tabDecryption
            // 
            this.tabDecryption.Controls.Add(this.tbDecryptLog);
            this.tabDecryption.Controls.Add(this.pbDecryption);
            this.tabDecryption.Controls.Add(this.cbAutoDeterminePrivateKey);
            this.tabDecryption.Controls.Add(this.btStartDecryption);
            this.tabDecryption.Controls.Add(this.tbPrivateKey);
            this.tabDecryption.Controls.Add(this.btLoadPrivateKey);
            this.tabDecryption.Controls.Add(this.lbLoadPrivateKey);
            this.tabDecryption.Controls.Add(this.tbSelectDecryptionTarget);
            this.tabDecryption.Controls.Add(this.tbSelectDecryptionSource);
            this.tabDecryption.Controls.Add(this.btSelectDecryptionTarget);
            this.tabDecryption.Controls.Add(this.btSelectDecryptionSource);
            this.tabDecryption.Controls.Add(this.lbSelectDecryptionFiles);
            this.tabDecryption.Controls.Add(this.cbDecryptLog);
            this.tabDecryption.Location = new System.Drawing.Point(4, 22);
            this.tabDecryption.Name = "tabDecryption";
            this.tabDecryption.Padding = new System.Windows.Forms.Padding(3);
            this.tabDecryption.Size = new System.Drawing.Size(626, 424);
            this.tabDecryption.TabIndex = 3;
            this.tabDecryption.Text = "Decryption";
            this.tabDecryption.UseVisualStyleBackColor = true;
            // 
            // tbDecryptLog
            // 
            this.tbDecryptLog.Location = new System.Drawing.Point(5, 269);
            this.tbDecryptLog.Multiline = true;
            this.tbDecryptLog.Name = "tbDecryptLog";
            this.tbDecryptLog.ReadOnly = true;
            this.tbDecryptLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbDecryptLog.Size = new System.Drawing.Size(612, 149);
            this.tbDecryptLog.TabIndex = 39;
            // 
            // pbDecryption
            // 
            this.pbDecryption.Location = new System.Drawing.Point(5, 232);
            this.pbDecryption.Name = "pbDecryption";
            this.pbDecryption.Size = new System.Drawing.Size(612, 23);
            this.pbDecryption.Step = 100;
            this.pbDecryption.TabIndex = 38;
            // 
            // cbAutoDeterminePrivateKey
            // 
            this.cbAutoDeterminePrivateKey.AutoSize = true;
            this.cbAutoDeterminePrivateKey.Checked = true;
            this.cbAutoDeterminePrivateKey.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoDeterminePrivateKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAutoDeterminePrivateKey.Location = new System.Drawing.Point(11, 28);
            this.cbAutoDeterminePrivateKey.Name = "cbAutoDeterminePrivateKey";
            this.cbAutoDeterminePrivateKey.Size = new System.Drawing.Size(382, 19);
            this.cbAutoDeterminePrivateKey.TabIndex = 37;
            this.cbAutoDeterminePrivateKey.Text = "Automatically determine the private key suitable for the decryption ";
            this.cbAutoDeterminePrivateKey.UseVisualStyleBackColor = true;
            this.cbAutoDeterminePrivateKey.CheckedChanged += new System.EventHandler(this.cbAutoDeterminePrivateKey_CheckedChanged);
            // 
            // btStartDecryption
            // 
            this.btStartDecryption.Enabled = false;
            this.btStartDecryption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btStartDecryption.Location = new System.Drawing.Point(442, 191);
            this.btStartDecryption.Name = "btStartDecryption";
            this.btStartDecryption.Size = new System.Drawing.Size(104, 23);
            this.btStartDecryption.TabIndex = 36;
            this.btStartDecryption.Text = "Start Decryption";
            this.btStartDecryption.UseVisualStyleBackColor = true;
            this.btStartDecryption.Click += new System.EventHandler(this.btStartDecryption_Click);
            // 
            // tbPrivateKey
            // 
            this.tbPrivateKey.Location = new System.Drawing.Point(133, 55);
            this.tbPrivateKey.Name = "tbPrivateKey";
            this.tbPrivateKey.ReadOnly = true;
            this.tbPrivateKey.Size = new System.Drawing.Size(490, 21);
            this.tbPrivateKey.TabIndex = 35;
            this.tbPrivateKey.Visible = false;
            // 
            // btLoadPrivateKey
            // 
            this.btLoadPrivateKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btLoadPrivateKey.Location = new System.Drawing.Point(10, 53);
            this.btLoadPrivateKey.Name = "btLoadPrivateKey";
            this.btLoadPrivateKey.Size = new System.Drawing.Size(104, 23);
            this.btLoadPrivateKey.TabIndex = 34;
            this.btLoadPrivateKey.Text = "Load private key";
            this.btLoadPrivateKey.UseVisualStyleBackColor = true;
            this.btLoadPrivateKey.Visible = false;
            this.btLoadPrivateKey.Click += new System.EventHandler(this.btLoadPrivateKey_Click);
            // 
            // lbLoadPrivateKey
            // 
            this.lbLoadPrivateKey.AutoSize = true;
            this.lbLoadPrivateKey.Location = new System.Drawing.Point(7, 10);
            this.lbLoadPrivateKey.Name = "lbLoadPrivateKey";
            this.lbLoadPrivateKey.Size = new System.Drawing.Size(342, 15);
            this.lbLoadPrivateKey.TabIndex = 33;
            this.lbLoadPrivateKey.Text = "Choose the appropriate private key to generate the decryption:";
            // 
            // tbSelectDecryptionTarget
            // 
            this.tbSelectDecryptionTarget.Location = new System.Drawing.Point(132, 158);
            this.tbSelectDecryptionTarget.Name = "tbSelectDecryptionTarget";
            this.tbSelectDecryptionTarget.ReadOnly = true;
            this.tbSelectDecryptionTarget.Size = new System.Drawing.Size(490, 21);
            this.tbSelectDecryptionTarget.TabIndex = 32;
            // 
            // tbSelectDecryptionSource
            // 
            this.tbSelectDecryptionSource.Location = new System.Drawing.Point(132, 125);
            this.tbSelectDecryptionSource.Name = "tbSelectDecryptionSource";
            this.tbSelectDecryptionSource.ReadOnly = true;
            this.tbSelectDecryptionSource.Size = new System.Drawing.Size(490, 21);
            this.tbSelectDecryptionSource.TabIndex = 31;
            // 
            // btSelectDecryptionTarget
            // 
            this.btSelectDecryptionTarget.Enabled = false;
            this.btSelectDecryptionTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSelectDecryptionTarget.Location = new System.Drawing.Point(11, 156);
            this.btSelectDecryptionTarget.Name = "btSelectDecryptionTarget";
            this.btSelectDecryptionTarget.Size = new System.Drawing.Size(104, 23);
            this.btSelectDecryptionTarget.TabIndex = 30;
            this.btSelectDecryptionTarget.Text = "Select target";
            this.btSelectDecryptionTarget.UseVisualStyleBackColor = true;
            this.btSelectDecryptionTarget.Click += new System.EventHandler(this.btSelectDecryptionTarget_Click);
            // 
            // btSelectDecryptionSource
            // 
            this.btSelectDecryptionSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSelectDecryptionSource.Location = new System.Drawing.Point(11, 123);
            this.btSelectDecryptionSource.Name = "btSelectDecryptionSource";
            this.btSelectDecryptionSource.Size = new System.Drawing.Size(104, 23);
            this.btSelectDecryptionSource.TabIndex = 29;
            this.btSelectDecryptionSource.Text = "Select source";
            this.btSelectDecryptionSource.UseVisualStyleBackColor = true;
            this.btSelectDecryptionSource.Click += new System.EventHandler(this.btSelectDecryptionSource_Click);
            // 
            // lbSelectDecryptionFiles
            // 
            this.lbSelectDecryptionFiles.AutoSize = true;
            this.lbSelectDecryptionFiles.Location = new System.Drawing.Point(8, 95);
            this.lbSelectDecryptionFiles.Name = "lbSelectDecryptionFiles";
            this.lbSelectDecryptionFiles.Size = new System.Drawing.Size(303, 15);
            this.lbSelectDecryptionFiles.TabIndex = 28;
            this.lbSelectDecryptionFiles.Text = "Select the source and the target files for the decryption:";
            // 
            // cbDecryptLog
            // 
            this.cbDecryptLog.AutoSize = true;
            this.cbDecryptLog.Checked = true;
            this.cbDecryptLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDecryptLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDecryptLog.Location = new System.Drawing.Point(68, 195);
            this.cbDecryptLog.Name = "cbDecryptLog";
            this.cbDecryptLog.Size = new System.Drawing.Size(131, 19);
            this.cbDecryptLog.TabIndex = 27;
            this.cbDecryptLog.Text = "View decryption log";
            this.cbDecryptLog.UseVisualStyleBackColor = true;
            this.cbDecryptLog.CheckedChanged += new System.EventHandler(this.cbDecryptLog_CheckedChanged);
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(634, 24);
            this.menuMain.TabIndex = 1;
            this.menuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 477);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.menuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rabin Cryptosystem Application";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.tabMain.ResumeLayout(false);
            this.tabPrimesGen.ResumeLayout(false);
            this.tabPrimesGen.PerformLayout();
            this.pnGenerationOptions.ResumeLayout(false);
            this.pnGenerationOptions.PerformLayout();
            this.tabKeyGen.ResumeLayout(false);
            this.tabKeyGen.PerformLayout();
            this.tabEncryption.ResumeLayout(false);
            this.tabEncryption.PerformLayout();
            this.tabDecryption.ResumeLayout(false);
            this.tabDecryption.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPrimesGen;
        private System.Windows.Forms.TabPage tabKeyGen;
        private System.Windows.Forms.Label lbText2;
        private System.Windows.Forms.Label lbText1;
        private System.Windows.Forms.TextBox tbText2;
        private System.Windows.Forms.TextBox tbText1;
        private System.Windows.Forms.Button btPrimeGen;
        private System.Windows.Forms.CheckBox cbPrimeGenLog;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button btPrimeSave;
        private System.Windows.Forms.Panel pnGenerationOptions;
        private System.Windows.Forms.Label lbPrimeGenChoice;
        private System.Windows.Forms.RadioButton rbtRandomStrings;
        private System.Windows.Forms.RadioButton rbtRandomOrg;
        private System.Windows.Forms.Button btLoadPrime2;
        private System.Windows.Forms.Button btLoadPrime1;
        private System.Windows.Forms.CheckBox cbKeyGenLog;
        private System.Windows.Forms.TextBox tbLoadPrime1;
        private System.Windows.Forms.TextBox tbLoadPrime2;
        private System.Windows.Forms.Label lbLoadPrimes;
        private System.Windows.Forms.Button btKeySave;
        private System.Windows.Forms.Button btKeyGen;
        private System.Windows.Forms.TabPage tabDecryption;
        private System.Windows.Forms.TabPage tabEncryption;
        private System.Windows.Forms.TextBox tbSelectEncryptionTarget;
        private System.Windows.Forms.TextBox tbSelectEncryptionSource;
        private System.Windows.Forms.Button btSelectEncryptionTarget;
        private System.Windows.Forms.Button btSelectEncryptionSource;
        private System.Windows.Forms.Label lbSelectEncryptionFiles;
        private System.Windows.Forms.CheckBox cbEncryptLog;
        private System.Windows.Forms.TextBox tbEncryptLog;
        private System.Windows.Forms.Label lbLoadPublicKey;
        private System.Windows.Forms.Button btLoadPublicKey;
        private System.Windows.Forms.TextBox tbPublicKey;
        private System.Windows.Forms.Button btStartEncryption;
        private System.Windows.Forms.Button btStartDecryption;
        private System.Windows.Forms.TextBox tbPrivateKey;
        private System.Windows.Forms.Button btLoadPrivateKey;
        private System.Windows.Forms.Label lbLoadPrivateKey;
        private System.Windows.Forms.TextBox tbSelectDecryptionTarget;
        private System.Windows.Forms.TextBox tbSelectDecryptionSource;
        private System.Windows.Forms.Button btSelectDecryptionTarget;
        private System.Windows.Forms.Button btSelectDecryptionSource;
        private System.Windows.Forms.Label lbSelectDecryptionFiles;
        private System.Windows.Forms.CheckBox cbDecryptLog;
        private System.Windows.Forms.CheckBox cbAutoDeterminePrivateKey;
        private System.Windows.Forms.ProgressBar pbDecryption;
        private System.Windows.Forms.TextBox tbKeyGenLog;
        private System.Windows.Forms.ProgressBar pbEncryption;
        private System.Windows.Forms.TextBox tbDecryptLog;
        private System.Windows.Forms.TextBox tbPrimeGenLog;
    }
}

