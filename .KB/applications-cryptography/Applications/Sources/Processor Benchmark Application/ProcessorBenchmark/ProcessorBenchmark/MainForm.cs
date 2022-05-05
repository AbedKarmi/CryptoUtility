using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BigIntegerImplementation;
using ResultsHandler;

namespace ProcessorBenchmark
{
    public partial class MainForm : Form
    {
        private const string fileNamesDataFile = "Files\\fileNames.txt";
        private const int numberBase = 65536, maxSize = 193;
        private const double divident = 2000000;

        private Thread benchmarkThread;
        private MethodInvoker invoker;
        
        private string privateKeyFileName, fileToDecrypt;
        private int processorScore;
        private IntPtr processorCoresMask = (IntPtr)1;
        private bool threadIsRunning = false;

        private BigInteger decryptionPublicModulus, decryptionPrivateExponent;
        private BigInteger zero = new BigInteger(numberBase, maxSize);
        private List<BigInteger> encryptedNumbers;

        private ResultsManager resultsManager;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ReadFileNameData();
            LoadPrivateKey();
            LoadEncryptedFileContent();

            pbBenchmark.Maximum = encryptedNumbers.Count;
        }

        private void miAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Processor Benchmark Application\r\n" +
                            "Based on RSA 1536-bit decryption\r\n" +
                            "Version 1.30\r\n" +
                            "Copyright (C) 2008 Mihnea Rădulescu\r\n" +
                            "All rights reserved.", "Processor Benchmark Application", 
                            MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btViewResults_Click(object sender, EventArgs e)
        {
            try
            {
                if (resultsManager == null)
                    resultsManager = new ResultsManager();

                resultsManager.ViewTestResultsFromWebServer();
            }

            catch (Exception)
            {
                MessageBox.Show("Could not load web site URL file. The file is inexistent or corrupted.",
                                "Inexistent Or Corrupted Web Site URL File", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Clears the benchmark information obtained through testing.
        /// </summary>
        private void ClearBenchmarkInformation()
        {
            tbResults.Text = "";
            pbBenchmark.Value = 0;
            processorScore = 0;
            resultsManager = null;
        }

        /// <summary>
        /// Gets the currently running thread id.
        /// </summary>
        /// <returns>The id of the running thread</returns>
        [DllImport("kernel32.dll", EntryPoint = "GetCurrentThreadId", ExactSpelling = true)]
        private static extern int GetCurrentWin32ThreadId();

        /// <summary>
        /// Maps the running thread to a given set of processor cores.
        /// </summary>
        private void SetThreadToProcessorCoreMapping()
        {
            int threadId = GetCurrentWin32ThreadId();

            foreach (ProcessThread processThread in Process.GetCurrentProcess().Threads)
                if (processThread.Id == threadId)
                {
                    processThread.ProcessorAffinity = processorCoresMask;
                    processThread.PriorityLevel = ThreadPriorityLevel.Highest;

                    return;
                }
        }

        private void btStartBenchmark_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("You are about to start the processor core benchmark.\r\n" +
                                              "\r\nIn order to obtain reliable test results, please close " +
                                              "all of your other resource-consuming applications " +
                                              "and then click the OK button.", "Processor Core Benchmark " +
                                              "Commencing", MessageBoxButtons.OKCancel,
                                              MessageBoxIcon.Information);

            if (dr == DialogResult.Cancel)
                return;
            
            btStartBenchmark.Enabled = false;
            btViewResults.Enabled = false;

            ClearBenchmarkInformation();

            try
            {
                resultsManager = new ResultsManager(processorCoresMask.ToInt32() - 1);
            }

            catch (Exception)
            {
                MessageBox.Show("Could not load web site URL file. The file is inexistent or corrupted.",
                                "Inexistent Or Corrupted Web Site URL File", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            benchmarkThread = new Thread(new ThreadStart(DecryptNumbers));
            threadIsRunning = true;
            benchmarkThread.Start();
        }
        
        /// <summary>
        /// Increases the benchmark progress value represented visually in the progress bar. 
        /// </summary>
        private void IncreaseProgressBarValue()
        {
            pbBenchmark.Value++;
        }

        /// <summary>
        /// Shows the test results on the screen, updating the UI accordingly.
        /// </summary>
        private void ShowResults()
        {
            tbResults.Text = resultsManager.SystemInformationString;
            btStartBenchmark.Enabled = true;
            btViewResults.Enabled = true;
        } 

        /// <summary>
        /// Decrypts the list of BigInteger numbers.
        /// </summary>
        private void DecryptNumbers()
        {
            Thread.BeginThreadAffinity();
            
            Process currentProcess = Process.GetCurrentProcess();
            currentProcess.PriorityClass = ProcessPriorityClass.High;
            SetThreadToProcessorCoreMapping();

            resultsManager.GenerateSystemInformation();

            DateTime start, finish;

            // Benchmark code (very CPU core-intensive)
            start = DateTime.Now.ToUniversalTime();
            foreach (BigInteger number in encryptedNumbers)
            { 
                BigInteger.PowerRepeatedSquaring(number, decryptionPrivateExponent, decryptionPublicModulus);

                invoker = new MethodInvoker(IncreaseProgressBarValue);
                this.Invoke(invoker);
            }
            finish = DateTime.Now.ToUniversalTime();

            processorScore = (int)(divident / finish.Subtract(start).TotalSeconds);
            resultsManager.ProcessorScore = processorScore;

            try
            {
                resultsManager.SubmitTestDataToWebServer();
            }

            catch (Exception)
            {
                MessageBox.Show("Could not access the web application. The test results have not been " +
                                "exposed online", "Unreachable Web Application", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }

            invoker = new MethodInvoker(ShowResults);
            this.Invoke(invoker);
            
            threadIsRunning = false;
            currentProcess.PriorityClass = ProcessPriorityClass.Normal;

            Thread.EndThreadAffinity();
        }

        /// <summary>
        /// Reads the private key file name and file to decrypt name from a text file.
        /// </summary>
        private void ReadFileNameData()
        {
            try
            {
                TextReader reader = new StreamReader(fileNamesDataFile);
                privateKeyFileName = reader.ReadLine();
                fileToDecrypt = reader.ReadLine();
                reader.Close();
            }

            catch (Exception)
            {
                MessageBox.Show("Could not load file names data. The file is inexistent or corrupted.",
                                "Inexistent Or Corrupted File Names Data File", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                btStartBenchmark.Enabled = false;
            }
        }

        /// <summary>
        /// Loads the private key from a file into the memory.
        /// </summary>
        private void LoadPrivateKey()
        {
            try
            {
                FileStream fs = new FileStream(privateKeyFileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);

                decryptionPublicModulus = new BigInteger(numberBase, maxSize, false, br);
                decryptionPrivateExponent = new BigInteger(numberBase, maxSize, false, br);

                br.Close();
                fs.Close();
            }

            catch (Exception)
            {
                MessageBox.Show("Could not load private-key. The file is inexistent or corrupted.",
                                "Inexistent Or Corrupted Private-key File", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                btStartBenchmark.Enabled = false;
            }
        }

        /// <summary>
        /// Loads the BigInteger numbers to be decrypted from the encrypted file. 
        /// </summary>
        private void LoadEncryptedFileContent()
        {
            encryptedNumbers = new List<BigInteger>();

            try
            {
                FileStream fs = new FileStream(fileToDecrypt, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);

                new BigInteger(numberBase, maxSize, false, br);

                for (; ; )
                {
                    try
                    {
                        br.ReadByte();
                    }

                    catch (Exception)
                    {
                        break;
                    }

                    br.ReadByte();
                    br.ReadBoolean();

                    encryptedNumbers.Add(new BigInteger(numberBase, maxSize, false, br));
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Could not access encrypted file. The file is inexistent or corrupted.",
                                "Inexistent Or Corrupted Encrypted File", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                btStartBenchmark.Enabled = false;
            }
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            if (threadIsRunning == false)
                Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (threadIsRunning == true)
                e.Cancel = true;

            else
                Application.Exit();
        }
    }
}
