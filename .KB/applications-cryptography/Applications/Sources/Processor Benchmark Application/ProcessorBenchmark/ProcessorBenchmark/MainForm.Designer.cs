namespace ProcessorBenchmark
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
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.btStartBenchmark = new System.Windows.Forms.Button();
            this.btViewResults = new System.Windows.Forms.Button();
            this.gbButtons = new System.Windows.Forms.GroupBox();
            this.tbResults = new System.Windows.Forms.TextBox();
            this.pbBenchmark = new System.Windows.Forms.ProgressBar();
            this.msMain.SuspendLayout();
            this.gbButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miExit,
            this.miAbout});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(558, 24);
            this.msMain.TabIndex = 0;
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(37, 20);
            this.miExit.Text = "E&xit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miAbout
            // 
            this.miAbout.Name = "miAbout";
            this.miAbout.Size = new System.Drawing.Size(51, 20);
            this.miAbout.Text = "&About";
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // btStartBenchmark
            // 
            this.btStartBenchmark.Location = new System.Drawing.Point(27, 28);
            this.btStartBenchmark.Name = "btStartBenchmark";
            this.btStartBenchmark.Size = new System.Drawing.Size(135, 23);
            this.btStartBenchmark.TabIndex = 1;
            this.btStartBenchmark.Text = "Start benchmark";
            this.btStartBenchmark.UseVisualStyleBackColor = true;
            this.btStartBenchmark.Click += new System.EventHandler(this.btStartBenchmark_Click);
            // 
            // btViewResults
            // 
            this.btViewResults.Location = new System.Drawing.Point(27, 97);
            this.btViewResults.Name = "btViewResults";
            this.btViewResults.Size = new System.Drawing.Size(135, 23);
            this.btViewResults.TabIndex = 2;
            this.btViewResults.Text = "View results on the web";
            this.btViewResults.UseVisualStyleBackColor = true;
            this.btViewResults.Click += new System.EventHandler(this.btViewResults_Click);
            // 
            // gbButtons
            // 
            this.gbButtons.Controls.Add(this.btStartBenchmark);
            this.gbButtons.Controls.Add(this.btViewResults);
            this.gbButtons.Location = new System.Drawing.Point(12, 101);
            this.gbButtons.Name = "gbButtons";
            this.gbButtons.Size = new System.Drawing.Size(187, 146);
            this.gbButtons.TabIndex = 3;
            this.gbButtons.TabStop = false;
            // 
            // tbResults
            // 
            this.tbResults.Location = new System.Drawing.Point(220, 39);
            this.tbResults.Multiline = true;
            this.tbResults.Name = "tbResults";
            this.tbResults.ReadOnly = true;
            this.tbResults.Size = new System.Drawing.Size(327, 271);
            this.tbResults.TabIndex = 4;
            // 
            // pbBenchmark
            // 
            this.pbBenchmark.Location = new System.Drawing.Point(12, 325);
            this.pbBenchmark.Name = "pbBenchmark";
            this.pbBenchmark.Size = new System.Drawing.Size(535, 23);
            this.pbBenchmark.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 358);
            this.Controls.Add(this.pbBenchmark);
            this.Controls.Add(this.tbResults);
            this.Controls.Add(this.gbButtons);
            this.Controls.Add(this.msMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.msMain;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Processor Core Benchmark";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.gbButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem miAbout;
        private System.Windows.Forms.Button btStartBenchmark;
        private System.Windows.Forms.Button btViewResults;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.GroupBox gbButtons;
        private System.Windows.Forms.TextBox tbResults;
        private System.Windows.Forms.ProgressBar pbBenchmark;

    }
}

