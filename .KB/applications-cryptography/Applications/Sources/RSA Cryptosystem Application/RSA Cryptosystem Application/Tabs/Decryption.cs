using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using BigIntegerImplementation;
using RSAEncryptionDecryptionImplementation;

namespace RSA_Cryptosystem_Application
{
    public partial class MainForm : Form
    {
        private void cbDecryptLog_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDecryptLog.Checked == true)
                tbDecryptLog.Visible = true;
            else
                tbDecryptLog.Visible = false;
        }

        private void btLoadPrivateKey_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadPrivateKey = new OpenFileDialog();
            loadPrivateKey.Filter = "RSA Private Key File (*.RSAPrivateKey)|*.RSAPrivateKey";
            loadPrivateKey.Title = "Load A RSA Private Key File";
            loadPrivateKey.InitialDirectory = currentDir + "\\Keys\\Private";
            loadPrivateKey.ShowDialog();

            if (loadPrivateKey.FileName != "")
            {
                try
                {
                    FileStream fs = (FileStream)loadPrivateKey.OpenFile();
                    BinaryReader br = new BinaryReader(fs);

                    decryptionPublicModulus = new BigInteger(numberBase, maxSize, false, br);
                    decryptionPrivateExponent = new BigInteger(numberBase, maxSize, false, br);

                    br.Close();
                    fs.Close();

                    tbPrivateKey.Text = loadPrivateKey.FileName;
                }

                catch (Exception)
                {
                    MessageBox.Show("Could not load private-key. The file is corrupted.", "Corrupted " +
                                    "Private-key File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (((decryptionSourceFileName != "") && (decryptionTargetFileName != "")) &&
                ((decryptionPublicModulus != zero) &&
                 (decryptionPrivateExponent != zero)))
                btStartDecryption.Enabled = true;
        }

        private void btSelectDecryptionSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectDecryptionSource = new OpenFileDialog();
            selectDecryptionSource.Filter = "RSA Encrypted File (*.RSA)|*.RSA";
            selectDecryptionSource.Title = "Select A RSA Encrypted File";
            selectDecryptionSource.InitialDirectory = decryptionCurrentDir;
            selectDecryptionSource.ShowDialog();

            if (selectDecryptionSource.FileName != "")
            {
                decryptionSourceFileName = selectDecryptionSource.FileName;
                decryptionCurrentDir = Environment.CurrentDirectory;

                tbSelectDecryptionSource.Text = selectDecryptionSource.FileName;

                decryptionTargetFileName = "";
                tbSelectDecryptionTarget.Text = "";
                btSelectDecryptionTarget.Enabled = true;
                btStartDecryption.Enabled = false;

                if (cbAutoDeterminePrivateKey.Checked == true)
                    AutoDeterminePrivateKey();
            }

            if (((decryptionSourceFileName != "") && (decryptionTargetFileName != "")) &&
                ((decryptionPublicModulus != zero) &&
                 (decryptionPrivateExponent != zero)))
                btStartDecryption.Enabled = true;
        }

        private void btSelectDecryptionTarget_Click(object sender, EventArgs e)
        {
            SaveFileDialog selectDecryptionTarget = new SaveFileDialog();
            selectDecryptionTarget.Filter = "All Files (*.*)|*.*";
            selectDecryptionTarget.Title = "Save A Decrypted File As";
            selectDecryptionTarget.InitialDirectory = decryptionCurrentDir;
            selectDecryptionTarget.ShowDialog();

            decryptionTargetFileName = selectDecryptionTarget.FileName;

            if (((decryptionSourceFileName != "") && (decryptionTargetFileName != "")) &&
                (decryptionSourceFileName == decryptionTargetFileName))
            {
                MessageBox.Show("A file cannot be decrypted to itself. Please choose another " +
                                "target file!", "Decryption Target Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            if (selectDecryptionTarget.FileName != "")
            {
                decryptionTargetFileName = selectDecryptionTarget.FileName;
                tbSelectDecryptionTarget.Text = selectDecryptionTarget.FileName;
            }

            if (((decryptionSourceFileName != "") && (decryptionTargetFileName != "")) &&
                ((decryptionPublicModulus != zero) &&
                 (decryptionPrivateExponent != zero)))
                btStartDecryption.Enabled = true;
        }

        private void cbAutoDeterminePrivateKey_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutoDeterminePrivateKey.Checked == true)
            {
                btLoadPrivateKey.Visible = false;
                tbPrivateKey.Visible = false;
                tbPrivateKey.Text = "";

                decryptionPublicModulus = new BigInteger(numberBase, maxSize);
                decryptionPrivateExponent = new BigInteger(numberBase, maxSize);

                if (decryptionSourceFileName != "")
                    AutoDeterminePrivateKey();

                btStartDecryption.Enabled = false;
            }

            else
            {
                btLoadPrivateKey.Visible = true;
                tbPrivateKey.Visible = true;

                decryptionPublicModulus = new BigInteger(numberBase, maxSize);
                decryptionPrivateExponent = new BigInteger(numberBase, maxSize);

                btStartDecryption.Enabled = false;
            }

            if (((decryptionSourceFileName != "") && (decryptionTargetFileName != "")) &&
               ((decryptionPublicModulus != zero) &&
                (decryptionPrivateExponent != zero)))
                btStartDecryption.Enabled = true;
        }

        private void btStartDecryption_Click(object sender, EventArgs e)
        {
            BigInteger publicKeyTest = new BigInteger(numberBase, maxSize);

            if (btStartDecryption.Text == "Stop Decryption")
                if (threadIsRunning == true)
                {
                    activeThread.Abort();
                    br.Close();
                    fsr.Close();

                    bw.Close();
                    fsw.Close();

                    btStartDecryption.Text = "Start Decryption";
                    EnableUserItems();

                    return;
                }

            try
            {
                fsr = File.OpenRead(decryptionSourceFileName);
                br = new BinaryReader(fsr);
            }

            catch (Exception)
            {
                btStartDecryption.Text = "Start Decryption";
                EnableUserItems();

                MessageBox.Show("Could not read from the file " + decryptionSourceFileName + " because it is " +
                                "either inexistent or is used by another process.", "Could Not Read " +
                                "From File", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (fsr.Length == 0)
            {
                btStartDecryption.Text = "Start Decryption";
                EnableUserItems();

                MessageBox.Show("Could not read from the file " + decryptionSourceFileName + " because " +
                                "it's length is 0 bytes.", "Could Not Read From File",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                decryptionTargetFileName = "";
                tbSelectDecryptionTarget.Text = "";
                btSelectDecryptionTarget.Enabled = false;
                btStartDecryption.Enabled = false;

                return;
            }

            try
            {
                publicKeyTest = Decryption.ReadPublicKey(numberBase, maxSize, decryptionSourceFileName);
            }

            catch (Exception)
            {
                btStartDecryption.Text = "Start Decryption";
                EnableUserItems();

                MessageBox.Show("Could not read from the file " + decryptionSourceFileName + " because " +
                                "it is an invalid encrypted file.", "Could Not Read From File",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                decryptionTargetFileName = "";
                tbSelectDecryptionTarget.Text = "";
                btSelectDecryptionTarget.Enabled = false;
                btStartDecryption.Enabled = false;

                return;
            }

            if (publicKeyTest != decryptionPublicModulus)
            {
                br.Close();
                fsr.Close();

                btStartDecryption.Text = "Start Decryption";
                EnableUserItems();

                MessageBox.Show("You are using a non-corresponding private key. As such, " +
                                "the decryption is unreliable.", "Inappropriate Private-key Used",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            try
            {
                fsw = File.Create(decryptionTargetFileName);
                bw = new BinaryWriter(fsw);
            }

            catch (Exception)
            {
                br.Close();
                fsr.Close();

                btStartDecryption.Text = "Start Decryption";
                EnableUserItems();

                MessageBox.Show("Could not write to the file " + decryptionTargetFileName + " because it is " +
                                "either a read-only file or is used by another process.", "Could Not Write " +
                                "To File", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            pbDecryption.Maximum = (int)br.BaseStream.Length - sizeOfSerializedPublicKey;
            pbDecryption.Value = 0;

            decryption = new Decryption(decryptionPublicModulus, decryptionPrivateExponent, bufferSize, br, bw);

            if (threadIsRunning == false)
            {
                Thread decryptionThread = new Thread(new ThreadStart(DecryptData));
                activeThreadType = "Decryption Thread";
                activeThread = decryptionThread;
                decryptionThread.Start();
            }
        }

        /// <summary>
        /// Determines the corresponding private key by analyzing all the possible
        /// private keys in the "Private Keys" program folder.
        /// </summary>
        private void AutoDeterminePrivateKey()
        {
            bool matchingPrivateKeyFound = false;
            BigInteger publicKeyFromFile = null;

            try
            {
                publicKeyFromFile = Decryption.ReadPublicKey(numberBase, maxSize, decryptionSourceFileName);
            }

            catch (Exception)
            {
                MessageBox.Show("Could not read from the file " + decryptionSourceFileName + " because " +
                                "it is either an invalid encrypted file or is used by another process.",
                                "Could Not Read From File", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            DirectoryInfo di = new DirectoryInfo(currentDir + "\\Keys\\Private");
            FileInfo[] privateKeyFiles = di.GetFiles("*.RSAPrivateKey");

            foreach (FileInfo fi in privateKeyFiles)
            {
                try
                {
                    FileStream fs = fi.OpenRead();
                    BinaryReader br = new BinaryReader(fs);

                    BigInteger numberTest = new BigInteger(numberBase, maxSize, false, br);
                    if (numberTest == publicKeyFromFile)
                    {
                        decryptionPublicModulus = new BigInteger(numberTest);
                        decryptionPrivateExponent = new BigInteger(numberBase, maxSize, false, br);
                        matchingPrivateKeyFound = true;
                        break;
                    }
                }

                catch (Exception) { }
            }

            if (matchingPrivateKeyFound == false)
                MessageBox.Show("No matching private key found. Search for the corresponding " +
                                "private-key file manually.", "No Matching Private-key Found",
                                MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        /// <summary>
        /// Decrypts the content of the current file.
        /// </summary>
        private void DecryptData()
        {
            int digitDifference, intReadSize;
            bool lastIntIncomplete;

            guiChange = delegate()
            {
                DisableUserItems();
                btStartDecryption.Text = "Stop Decryption";
                btStartDecryption.Enabled = true;
            };
            this.Invoke(guiChange);

            TimeSpan start = DateTime.Now.TimeOfDay, finish;

            guiChange = delegate()
            {
                tbDecryptLog.Text = "";
                tbDecryptLog.Text += "Starting the decryption process ...\r\n\r\n";
            };
            this.Invoke(guiChange);

            new BigInteger(numberBase, maxSize, false, br);

            for (; ; )
            {
                try
                {
                    digitDifference = (int)br.ReadByte();
                }

                catch (Exception)
                {
                    break;
                }

                intReadSize = (int)br.ReadByte();
                lastIntIncomplete = br.ReadBoolean();

                BigInteger encryptedNumber = new BigInteger(numberBase, maxSize, false, br);

                if (encryptedNumber == zero)
                {
                    guiChange = delegate()
                    {
                        tbDecryptLog.Text += "\r\nPlain number 0, obtained without decryption.\r\n\r\n";
                        pbDecryption.Value = (int)br.BaseStream.Position - sizeOfSerializedPublicKey;
                    };
                    this.Invoke(guiChange);

                    for (int i = 0; i < intReadSize; i++)
                        if (((i + 1) == intReadSize) && (lastIntIncomplete))
                            bw.Write((byte)0);
                        else
                            bw.Write((ushort)0);
                    continue;
                }

                guiChange = delegate()
                {
                    tbDecryptLog.Text += "\r\nEncrypted number " + encryptedNumber + "\r\n";
                };
                this.Invoke(guiChange);

                BigInteger decryptedNumber = decryption.Decrypt(encryptedNumber);
                if (decryptedNumber == minusOne)
                {
                    MessageBox.Show("Redundancy check failed! The encrypted file has been either damaged " +
                                    "or forged. As such, the decryption is unreliable.",
                                    "Damaged Or Forged Encrypted File", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    break;
                }

                guiChange = delegate()
                {
                    pbDecryption.Value = (int)br.BaseStream.Position - sizeOfSerializedPublicKey;
                    tbDecryptLog.Text += "\r\nDecrypted to number " + decryptedNumber + "\r\n\r\n";
                };
                this.Invoke(guiChange);

                decryption.WriteDecryptedData(decryptedNumber, digitDifference, intReadSize, lastIntIncomplete);
            }

            br.Close();
            fsr.Close();

            bw.Close();
            fsw.Close();

            finish = DateTime.Now.TimeOfDay;

            guiChange = delegate()
            {
                tbDecryptLog.Text += "\r\nThe decryption process required " + finish.Subtract(start).TotalSeconds +
                                     " seconds.\r\n";
                btStartDecryption.Text = "Start Decryption";
                EnableUserItems();
            };
            this.Invoke(guiChange);
        }
    }
}
