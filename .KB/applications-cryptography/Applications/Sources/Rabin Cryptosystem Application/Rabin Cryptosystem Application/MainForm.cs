using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using BigIntegerImplementation;
using PrimesGeneratorImplementation;
using RabinEncryptionDecryptionImplementation;

namespace Rabin_Cryptosystem_Application
{
    public partial class MainForm : Form
    {
        private const int numberBase = 65536;
        private const int maxSize = 193;
        private const int bufferSize = 85;
        private const int shiftDigits = 5;
        private const int saltDigits = 5;
        private const int mediumBoundaryFileSize = 20480;
        private const int highestBoundaryFileSize = 102400;
        private const int sizeOfSerializedPublicKey = 194;
        private long beginEditText1 = 0;
        private long endEditText1;
        private long beginEditText2 = 0;
        private long endEditText2;
        private string text1, text2;
        private long timeSpan1, timeSpan2;
        private bool threadIsRunning = false;
        private Thread activeThread = null;
        private delegate void GUIDelegate();
        private GUIDelegate guiChange;
        private string activeThreadType = "";
        private List<BigInteger> smallPrimesList;
        private BigInteger minusOne = new BigInteger(numberBase, maxSize, -1);
        private BigInteger zero = new BigInteger(numberBase, maxSize);
        private BigInteger one = new BigInteger(numberBase, maxSize, 1);
        private BigInteger two = new BigInteger(numberBase, maxSize, 2);
        private BigInteger generatedPrime, prime1, prime2, publicModulus,
                           encryptionPublicModulus, decryptionPublicModulus,
                           decryptionPrivatePrime1, decryptionPrivatePrime2;
        private bool useRandomOrg = true;
        private string encryptionSourceFileName = "", encryptionTargetFileName = "",
                       decryptionSourceFileName = "", decryptionTargetFileName = "";
        private string currentDir, encryptionCurrentDir = "C:\\", decryptionCurrentDir = "C:\\";
        private BinaryReader br;
        private BinaryWriter bw;
        private FileStream fsr, fsw;
        private Encryption encryption;
        private Decryption decryption;
        
        public MainForm()
        { 
            InitializeComponent();

            currentDir = Environment.CurrentDirectory;

            BuildPrimesList();
        }

        private void MainForm_Closing(object sender, CancelEventArgs e)
        {
            if (activeThread != null)
            {
                DialogResult result = MessageBox.Show("An operation is still pending. Quitting might " +
                                      "corrupt the data being processed. Are you sure you want to exit " +
                                      "the application?", "Task Not Yet Completed", MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (activeThread != null)
                    {
                        activeThread.Abort();
                        
                        if ((activeThreadType == "Encryption Thread") ||
                            (activeThreadType == "Decryption Thread"))
                        {
                            br.Close();
                            fsr.Close();
                            bw.Close();
                            fsw.Close();
                        }

                        Application.Exit();
                    }
                }

                else
                    e.Cancel = true;
            }

            else
                Application.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (activeThread != null)
            {
                DialogResult result = MessageBox.Show("An operation is still pending. Quitting might " +
                                      "corrupt the data being processed. Are you sure you want to exit " +
                                      "the application?", "Task not yet completed", MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (activeThread != null)
                    {
                        activeThread.Abort();
                        
                        if ((activeThreadType == "Encryption Thread") ||
                            (activeThreadType == "Decryption Thread"))
                        {
                            br.Close();
                            fsr.Close();
                            bw.Close();
                            fsw.Close();
                        }

                        Application.Exit();
                    }
                }
            }

            else
                Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Rabin Cryptosystem Application\r\n" +
                            "Uses secure 1536-bit encryption\r\n" +
                            "Version 1.60\r\n" +
                            "Copyright (C) 2008 Mihnea Rădulescu\r\n" +
                            "All rights reserved.", "About Rabin Cryptosystem Application", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        /// <summary>
        /// Enables the user interface items that were disabled when a processor-demanding application
        /// thread execution commenced.
        /// </summary>
        private void EnableUserItems()
        {
            btPrimeGen.Enabled = true;
            threadIsRunning = false;
            activeThreadType = "";
            activeThread = null;
            btPrimeGen.Enabled = true;
            if (generatedPrime != zero)
                btPrimeSave.Enabled = true;

            btLoadPrime1.Enabled = true;
            btLoadPrime2.Enabled = true;
            btKeyGen.Enabled = true;
            if ((prime1 != zero) && (prime2 != zero))
                btKeySave.Enabled = true;

            btLoadPublicKey.Enabled = true;
            btSelectEncryptionSource.Enabled = true;
            if (encryptionSourceFileName != "")
                btSelectEncryptionTarget.Enabled = true;
            if ((encryptionPublicModulus != zero) &&
                ((encryptionSourceFileName != "") && (encryptionTargetFileName != "")))
                btStartEncryption.Enabled = true;

            btLoadPrivateKey.Enabled = true;
            btSelectDecryptionSource.Enabled = true;
            if (decryptionSourceFileName != "")
                btSelectDecryptionTarget.Enabled = true;
            cbAutoDeterminePrivateKey.Enabled = true;
            if (((decryptionPublicModulus != zero) &&
               ((decryptionPrivatePrime1 != zero) && (decryptionPrivatePrime2 != zero))) &&
               ((decryptionSourceFileName != "") && (decryptionTargetFileName != "")))
                btStartDecryption.Enabled = true;
        }

        /// <summary>
        /// Disables the user interface items when a processor-demanding application thread execution
        /// commences.
        /// </summary>
        private void DisableUserItems()
        {
            btPrimeGen.Enabled = false;
            threadIsRunning = true;
            btPrimeGen.Enabled = false;
            btPrimeSave.Enabled = false;

            btLoadPrime1.Enabled = false;
            btLoadPrime2.Enabled = false;
            btKeyGen.Enabled = false;
            btKeySave.Enabled = false;

            btLoadPublicKey.Enabled = false;
            btSelectEncryptionSource.Enabled = false;
            btSelectEncryptionTarget.Enabled = false;
            btStartEncryption.Enabled = false;

            btLoadPrivateKey.Enabled = false;
            btSelectDecryptionSource.Enabled = false;
            btSelectDecryptionTarget.Enabled = false;
            cbAutoDeterminePrivateKey.Enabled = false;
            btStartDecryption.Enabled = false;
        }

        /// <summary>
        /// Generates the list of all the primes less than 1000.
        /// </summary>
        private void BuildPrimesList()
        {
            int[] smallPrimes = new int[] {3, 5, 7, 11, 13, 17, 19, 23, 29, 31,
                                         37, 41, 43, 47, 53, 59, 61, 67, 71,
                                         73, 79, 83, 89, 97, 101, 103, 107, 109,
                                         113, 127, 131, 137, 139, 149, 151, 157,
                                         163, 167, 173, 179, 181, 191, 193, 197,
                                         199, 211, 223, 227, 229,
                                         233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 
                                         283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 
                                         353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 
                                         419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 
                                         467, 479, 487, 491, 499, 503, 509, 521, 523, 541, 
                                         547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 
                                         607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 
                                         661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 
                                         739, 743, 751, 757, 761, 769, 773, 787, 797, 809, 
                                         811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 
                                         877, 881, 883, 887, 907, 911, 919, 929, 937, 941, 
                                         947, 953, 967, 971, 977, 983, 991, 997};

            smallPrimesList = new List<BigInteger>();
            foreach (int prime in smallPrimes)
                smallPrimesList.Add(new BigInteger(numberBase, maxSize, prime));

            generatedPrime = new BigInteger(numberBase, maxSize);
            prime1 = new BigInteger(numberBase, maxSize);
            prime2 = new BigInteger(numberBase, maxSize);
            publicModulus = new BigInteger(numberBase, maxSize);

            encryptionPublicModulus = new BigInteger(numberBase, maxSize);
            decryptionPublicModulus = new BigInteger(numberBase, maxSize);
            decryptionPrivatePrime1 = new BigInteger(numberBase, maxSize);
            decryptionPrivatePrime2 = new BigInteger(numberBase, maxSize);
        }
    }
}