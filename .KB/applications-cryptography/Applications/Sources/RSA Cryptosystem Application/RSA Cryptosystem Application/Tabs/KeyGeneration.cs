using System;
using System.IO;
using System.Windows.Forms;
using BigIntegerImplementation;

namespace RSA_Cryptosystem_Application
{
    public partial class MainForm : Form
    {
        private void cbKeyGen_CheckedChanged(object sender, EventArgs e)
        {
            if (cbKeyGenLog.Checked == true)
                tbKeyGenLog.Visible = true;
            else
                tbKeyGenLog.Visible = false;
        }

        private void btLoadPrime1_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadPrime1 = new OpenFileDialog();
            loadPrime1.Filter = "RSA Prime Number File (*.RSAPrime)|*.RSAPrime";
            loadPrime1.Title = "Load An RSA Prime Number File";
            loadPrime1.InitialDirectory = currentDir + "\\Primes";
            loadPrime1.ShowDialog();

            if (loadPrime1.FileName != "")
            {
                try
                {
                    FileStream fs = (FileStream)loadPrime1.OpenFile();
                    BinaryReader br = new BinaryReader(fs);

                    prime1 = new BigInteger(numberBase, maxSize, false, br);

                    br.Close();
                    fs.Close();

                    tbLoadPrime1.Text = loadPrime1.FileName;
                }

                catch (Exception)
                {
                    MessageBox.Show("Could not load prime number. The file " + loadPrime1.FileName +
                                    " is unreadable or corrupted.", "Unredable or corrupted Prime Number File",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if ((prime1 != zero) && (prime2 != zero))
                btKeyGen.Enabled = true;

            btKeySave.Enabled = false;
        }

        private void btLoadPrime2_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadPrime2 = new OpenFileDialog();
            loadPrime2.Filter = "RSA Prime Number File (*.RSAPrime)|*.RSAPrime";
            loadPrime2.Title = "Load An RSA Prime Number File";
            loadPrime2.InitialDirectory = currentDir + "\\Primes";
            loadPrime2.ShowDialog();

            if (loadPrime2.FileName != "")
            {
                try
                {
                    FileStream fs = (FileStream)loadPrime2.OpenFile();
                    BinaryReader br = new BinaryReader(fs);

                    prime2 = new BigInteger(numberBase, maxSize, false, br);

                    br.Close();
                    fs.Close();

                    tbLoadPrime2.Text = loadPrime2.FileName;
                }

                catch (Exception)
                {
                    MessageBox.Show("Could not load prime number. The file " + loadPrime2.FileName +
                                    " is unreadable or corrupted.", "Unredable or corrupted Prime Number File",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if ((prime1 != zero) && (prime2 != zero))
                btKeyGen.Enabled = true;

            btKeySave.Enabled = false;
        }

        private void btKeyGen_Click(object sender, EventArgs e)
        {
            if (prime1 == prime2)
                MessageBox.Show("Please choose two different primes!", "Wrong Primes Selection",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            else
            {
                tbKeyGenLog.Text = "Starting the key generation ...\r\n\r\n";
                tbKeyGenLog.Text += "The 1st prime is " + prime1 + "\r\n\r\n";
                tbKeyGenLog.Text += "The 2nd prime is " + prime2 + "\r\n\r\n";

                publicModulus = prime1 * prime2;

                tbKeyGenLog.Text += "The public key modulus (as the product of the two primes) is " +
                                    publicModulus + "\r\n\r\n";

                BigInteger PhiN = (prime1 - 1) * (prime2 - 1);

                for (publicEncryptionExponent = new BigInteger(startE); ; publicEncryptionExponent += two)
                    if (BigInteger.Gcd(publicEncryptionExponent, PhiN) == one)
                        break;

                tbKeyGenLog.Text += "The public encryption exponent is e = " + publicEncryptionExponent +
                                    "\r\n\r\n";

                privateDecryptionExponent = BigInteger.ModularInverse(publicEncryptionExponent, PhiN);

                tbKeyGenLog.Text += "The private decryption exponent is d = " + privateDecryptionExponent +
                                    "\r\n\r\n";

                btKeySave.Enabled = true;
            }
        }

        private void btKeySave_Click(object sender, EventArgs e)
        {
            SaveFileDialog savePublicKeyFile = new SaveFileDialog();
            savePublicKeyFile.Filter = "RSA Public Key File (*.RSAPublicKey)|*.RSAPublicKey";
            savePublicKeyFile.Title = "Save A RSA Public Key File As";
            savePublicKeyFile.InitialDirectory = currentDir + "\\Keys\\Public";
            savePublicKeyFile.ShowDialog();

            if (savePublicKeyFile.FileName != "")
            {
                try
                {
                    if (!savePublicKeyFile.FileName.EndsWith(".RSAPublicKey"))
                        savePublicKeyFile.FileName += ".RSAPublicKey";
                    FileStream fs = (FileStream)savePublicKeyFile.OpenFile();
                    BinaryWriter bw = new BinaryWriter(fs);

                    publicModulus.Serialize(bw);
                    publicEncryptionExponent.Serialize(bw);

                    bw.Close();
                    fs.Close();
                }

                catch (Exception)
                {
                    MessageBox.Show("Could not write to the file " + savePublicKeyFile.FileName +
                                    " because it is either a read-only file or is used by another process.",
                                    "Could Not Write To File", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            }

            SaveFileDialog savePrivateKeyFile = new SaveFileDialog();
            savePrivateKeyFile.Filter = "RSA Private Key File (*.RSAPrivateKey)|*.RSAPrivateKey";
            savePrivateKeyFile.Title = "Save A RSA Private Key File As";
            savePrivateKeyFile.InitialDirectory = currentDir + "\\Keys\\Private";
            savePrivateKeyFile.ShowDialog();

            if (savePrivateKeyFile.FileName != "")
            {
                if (!savePrivateKeyFile.FileName.EndsWith(".RSAPrivateKey"))
                    savePrivateKeyFile.FileName += ".RSAPrivateKey";
                FileStream fs = (FileStream)savePrivateKeyFile.OpenFile();
                BinaryWriter bw = new BinaryWriter(fs);

                publicModulus.Serialize(bw);
                privateDecryptionExponent.Serialize(bw);

                bw.Close();
                fs.Close();
            }
        }
    }
}
