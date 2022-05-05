using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using BigIntegerImplementation;
using RabinEncryptionDecryptionImplementation;

namespace Rabin_Cryptosystem_Application
{
    public partial class MainForm : Form
    {
        private void cbEncryptLog_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEncryptLog.Checked == true)
                tbEncryptLog.Visible = true;
            else
                tbEncryptLog.Visible = false;
        }

        private void btLoadPublicKey_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadPublicKey = new OpenFileDialog();
            loadPublicKey.Filter = "Rabin Public Key File (*.RabinPublicKey)|*.RabinPublicKey";
            loadPublicKey.Title = "Load A Rabin Public Key File";
            loadPublicKey.InitialDirectory = currentDir + "\\Keys\\Public";
            loadPublicKey.ShowDialog();

            if (loadPublicKey.FileName != "")
            {
                try
                {
                    FileStream fs = (FileStream)loadPublicKey.OpenFile();
                    BinaryReader br = new BinaryReader(fs);

                    encryptionPublicModulus = new BigInteger(numberBase, maxSize, false, br);

                    br.Close();
                    fs.Close();

                    tbPublicKey.Text = loadPublicKey.FileName;
                }

                catch (Exception)
                {
                    MessageBox.Show("Could not load public-key. The file is corrupted.", "Corrupted " +
                                    "Public-key File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (((encryptionSourceFileName != "") && (encryptionTargetFileName != "")) &&
                (encryptionPublicModulus != zero))
                btStartEncryption.Enabled = true;
        }

        private void btSelectEncryptionSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectEncryptionSource = new OpenFileDialog();
            selectEncryptionSource.Filter = "All Files (*.*)|*.*";
            selectEncryptionSource.Title = "Select A File";
            selectEncryptionSource.InitialDirectory = encryptionCurrentDir;
            selectEncryptionSource.ShowDialog();

            if (selectEncryptionSource.FileName != "")
            {
                encryptionSourceFileName = selectEncryptionSource.FileName;
                encryptionCurrentDir = Environment.CurrentDirectory;

                tbSelectEncryptionSource.Text = selectEncryptionSource.FileName;

                encryptionTargetFileName = "";
                tbSelectEncryptionTarget.Text = "";
                btSelectEncryptionTarget.Enabled = true;
                btStartEncryption.Enabled = false;
            }

            if (((encryptionSourceFileName != "") && (encryptionTargetFileName != "")) &&
                (encryptionPublicModulus != zero))
                btStartEncryption.Enabled = true;
        }

        private void btSelectEncryptionTarget_Click(object sender, EventArgs e)
        {
            SaveFileDialog selectEncryptionTarget = new SaveFileDialog();
            selectEncryptionTarget.Filter = "Rabin Encrypted File (*.Rabin)|*.Rabin";
            selectEncryptionTarget.Title = "Save A Rabin Encrypted File As";
            selectEncryptionTarget.InitialDirectory = encryptionCurrentDir;
            selectEncryptionTarget.ShowDialog();

            if (selectEncryptionTarget.FileName != "")
            {
                if (!selectEncryptionTarget.FileName.EndsWith(".Rabin"))
                    selectEncryptionTarget.FileName += ".Rabin";
                encryptionTargetFileName = selectEncryptionTarget.FileName;
                tbSelectEncryptionTarget.Text = selectEncryptionTarget.FileName;
            }

            if (((encryptionSourceFileName != "") && (encryptionTargetFileName != "")) &&
                (encryptionSourceFileName == encryptionTargetFileName))
            {
                MessageBox.Show("A file cannot be encrypted to itself. Please choose another " +
                                "target file!", "Encryption Target Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            if (((encryptionSourceFileName != "") && (encryptionTargetFileName != "")) &&
                (encryptionPublicModulus != zero))
                btStartEncryption.Enabled = true;
        }
        
        private void btStartEncryption_Click(object sender, EventArgs e)
        {
            if (btStartEncryption.Text == "Stop Encryption")
                if (threadIsRunning == true)
                {
                    activeThread.Abort();
                    br.Close();
                    fsr.Close();
                    bw.Close();
                    fsw.Close();

                    btStartEncryption.Text = "Start Encryption";
                    EnableUserItems();
                    return;
                }

            try
            {
                fsr = File.OpenRead(encryptionSourceFileName);
            }

            catch (Exception)
            {
                btStartEncryption.Text = "Start Encryption";
                EnableUserItems();

                MessageBox.Show("Could not read from the file " + encryptionSourceFileName + " because it is " +
                                "either inexistent or is used by another process.", "Could Not Read " +
                                "From File", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (fsr.Length == 0)
            {
                btStartEncryption.Text = "Start Encryption";
                EnableUserItems();
                fsr.Close();

                MessageBox.Show("The length of the file " + encryptionSourceFileName +
                                " is 0 bytes. Cannot encrypt such a file!", "Zero-length File",
                                MessageBoxButtons.OK, MessageBoxIcon.Stop);

                encryptionTargetFileName = "";
                tbSelectEncryptionTarget.Text = "";
                btSelectEncryptionTarget.Enabled = false;
                btStartEncryption.Enabled = false;

                return;
            }

            if (fsr.Length > (long)highestBoundaryFileSize)
            {
                btStartEncryption.Text = "Start Encryption";
                EnableUserItems();
                fsr.Close();

                MessageBox.Show("The size of the file to be encrypted is larger " +
                                "than " + highestBoundaryFileSize / 1024 + " KB. The " +
                                "encryption will be relatively fast, but the decryption might " +
                                "take up to tens of hours. Such a file is too large " +
                                "for efficient decrypting!", "File Size Too Large", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                encryptionTargetFileName = "";
                tbSelectEncryptionTarget.Text = "";
                btSelectEncryptionTarget.Enabled = false;
                btStartEncryption.Enabled = false;

                return;
            }

            if (fsr.Length > (long)mediumBoundaryFileSize)
            {
                DialogResult result = MessageBox.Show("The size of the file to be encrypted is larger " +
                                                      "than " + mediumBoundaryFileSize / 1024 + " KB. The " +
                                                      "encryption will be fast, but the decryption might " +
                                                      "take up to a an hour. Are you sure you want to " +
                                                      "continue?", "Large File Size", MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    btStartEncryption.Text = "Start Encryption";
                    EnableUserItems();
                    fsr.Close();

                    encryptionTargetFileName = "";
                    tbSelectEncryptionTarget.Text = "";
                    btSelectEncryptionTarget.Enabled = false;
                    btStartEncryption.Enabled = false;

                    return;
                }
            }

            br = new BinaryReader(fsr);

            try
            {
                fsw = File.Create(encryptionTargetFileName);
                bw = new BinaryWriter(fsw);
            }

            catch (Exception)
            {
                br.Close();
                fsr.Close();

                btStartEncryption.Text = "Start Encryption";
                EnableUserItems();

                MessageBox.Show("Could not write to the file " + encryptionTargetFileName + " because it is " +
                                "either a read-only file or is used by another process.", "Could Not Write " +
                                "To File", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            pbEncryption.Maximum = (int)br.BaseStream.Length;
            pbEncryption.Value = 0;

            encryption = new Encryption(encryptionPublicModulus, bufferSize, shiftDigits, saltDigits, br, bw);

            if (threadIsRunning == false)
            {
                Thread encryptionThread = new Thread(new ThreadStart(EncryptData));
                activeThreadType = "Encryption Thread";
                activeThread = encryptionThread;
                encryptionThread.Start();
            }
        }

        /// <summary>
        /// Encryption method for a whole stream of data.
        /// </summary>
        private void EncryptData()
        {
            int intReadCount, digitDifference;
            bool lastIntIncomplete;

            guiChange = delegate()
            {
                DisableUserItems();
                btStartEncryption.Text = "Stop Encryption";
                btStartEncryption.Enabled = true;
            };
            this.Invoke(guiChange);

            TimeSpan start = DateTime.Now.TimeOfDay, finish;

            guiChange = delegate()
            {
                tbEncryptLog.Text = "";
                tbEncryptLog.Text += "Starting the encryption process ...\r\n";
            };
            this.Invoke(guiChange);

            encryptionPublicModulus.Serialize(bw);

            guiChange = delegate()
            {
                tbEncryptLog.Text += "\r\nWriting the public-key to the encrypted file ...\r\n" +
                                     encryptionPublicModulus + "\r\n\r\n";
            };
            this.Invoke(guiChange);

            for (; ; )
            {
                int[] intReadBuffer;

                try
                {
                    intReadBuffer = encryption.ReadPlainData(out digitDifference, out intReadCount,
                                                             out lastIntIncomplete);
                }

                catch (Exception)
                {
                    break;
                }

                if (intReadBuffer == null)
                {
                    BigInteger numberZero = new BigInteger(numberBase, maxSize);

                    guiChange = delegate()
                    {
                        tbEncryptLog.Text += "\r\nPlain number 0, not encrypting it.\r\n";
                        pbEncryption.Value = (int)br.BaseStream.Position;
                    };
                    this.Invoke(guiChange);

                    encryption.WriteEncryptedData(numberZero, digitDifference, intReadCount, lastIntIncomplete);

                    if (intReadCount < bufferSize)
                        break;
                    else
                        continue;
                }

                BigInteger plainNumber = new BigInteger(numberBase, maxSize, false, bufferSize, intReadBuffer);

                guiChange = delegate()
                {
                    tbEncryptLog.Text += "\r\nPlain number " + plainNumber + "\r\n";
                };
                this.Invoke(guiChange);

                BigInteger encryptedNumber = encryption.Encrypt(plainNumber);
                pbEncryption.Value = (int)br.BaseStream.Position;

                guiChange = delegate()
                {
                    tbEncryptLog.Text += "\r\nEncrypted to number " + encryptedNumber + "\r\n\r\n";
                };
                this.Invoke(guiChange);

                encryption.WriteEncryptedData(encryptedNumber, digitDifference, intReadCount, lastIntIncomplete);

                if (intReadCount < bufferSize)
                    break;
            }

            br.Close();
            fsr.Close();

            bw.Close();
            fsw.Close();

            finish = DateTime.Now.TimeOfDay;

            guiChange = delegate()
            {
                tbEncryptLog.Text += "\r\nThe encryption process required " + finish.Subtract(start).TotalSeconds +
                                     " seconds.\r\n";

                btStartEncryption.Text = "Start Encryption";
                EnableUserItems();
            };
            this.Invoke(guiChange);
        }
    }
}
