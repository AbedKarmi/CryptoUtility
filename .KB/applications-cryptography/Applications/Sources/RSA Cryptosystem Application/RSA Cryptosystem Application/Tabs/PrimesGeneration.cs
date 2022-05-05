using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using BigIntegerImplementation;
using PrimesGeneratorImplementation;

namespace RSA_Cryptosystem_Application
{
    public partial class MainForm : Form
    {
        private void cbPrimeGen_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPrimeGenLog.Checked == true)
                tbPrimeGenLog.Visible = true;
            else
                tbPrimeGenLog.Visible = false;
        }

        private void tbText1_TextChanged(object sender, EventArgs e)
        {
            if (beginEditText1 == 0)
                beginEditText1 = DateTime.Now.ToBinary();
            else
                endEditText1 = DateTime.Now.ToBinary();
        }

        private void tbText2_TextChanged(object sender, EventArgs e)
        {
            if (beginEditText2 == 0)
                beginEditText2 = DateTime.Now.ToBinary();
            else
                endEditText2 = DateTime.Now.ToBinary();
        }

        private void rbtRandomStrings_CheckedChanged(object sender, EventArgs e)
        {
            lbText1.Visible = true;
            tbText1.Visible = true;
            lbText2.Visible = true;
            tbText2.Visible = true;

            useRandomOrg = false;
        }

        private void rbtRandomOrg_CheckedChanged(object sender, EventArgs e)
        {
            lbText1.Visible = false;
            tbText1.Visible = false;
            lbText2.Visible = false;
            tbText2.Visible = false;

            useRandomOrg = true;
        }

        private void btPrimeGen_Click(object sender, EventArgs e)
        {
            if (btPrimeGen.Text == "Stop Generation")
                if (threadIsRunning == true)
                {
                    activeThread.Abort();
                    btPrimeGen.Text = "Generate Prime";
                    EnableUserItems();
                    return;
                }

            tbPrimeGenLog.Text = "";

            if (((tbText1.Text.ToString() == "") ||
                (tbText2.Text.ToString() == "")) &&
                (rbtRandomStrings.Checked == true))
                MessageBox.Show("Please enter the two texts!", "Input Text Missing", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            else
            {
                text1 = tbText1.Text.ToString();
                timeSpan1 = endEditText1 - beginEditText1;
                text2 = tbText2.Text.ToString();
                timeSpan2 = endEditText2 - beginEditText2;

                if (threadIsRunning == false)
                {
                    Thread primeGenThread = new Thread(new ThreadStart(PrimeNumberGeneration));
                    activeThreadType = "Prime Generation Thread";
                    activeThread = primeGenThread;
                    primeGenThread.Start();

                    tbText1.Text = "";
                    tbText2.Text = "";
                    beginEditText1 = 0;
                    beginEditText2 = 0;
                }
            }
        }

        private void btPrimeSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog savePrimeFile = new SaveFileDialog();
            savePrimeFile.Filter = "RSA Prime Number File (*.RSAPrime)|*.RSAPrime";
            savePrimeFile.Title = "Save An RSA Prime Number File As";
            savePrimeFile.InitialDirectory = currentDir + "\\Primes";
            savePrimeFile.ShowDialog();

            if (savePrimeFile.FileName != "")
            {
                try
                {
                    if (!savePrimeFile.FileName.EndsWith(".RSAPrime"))
                        savePrimeFile.FileName += ".RSAPrime";
                    FileStream fs = (FileStream)savePrimeFile.OpenFile();
                    BinaryWriter bw = new BinaryWriter(fs);
                    generatedPrime.Serialize(bw);
                    bw.Close();
                    fs.Close();
                }

                catch (Exception)
                {
                    MessageBox.Show("Could not write to the file " + savePrimeFile.FileName +
                                    " because it is either a read-only file or is used by another process.",
                                    "Could Not Write To File", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            }

            if ((prime1 != zero) && (prime2 != zero))
                btKeyGen.Enabled = true;
        }

        /// <summary>
        /// Generates a random prime number of corresponding size.
        /// </summary>
        private void PrimeNumberGeneration()
        {
            guiChange = delegate()
            {
                DisableUserItems();
                btPrimeGen.Text = "Stop Generation";
                btPrimeGen.Enabled = true;
            };
            this.Invoke(guiChange);

            BigInteger eight = new BigInteger(numberBase, maxSize, 8);
            BigInteger oneThousand = new BigInteger(numberBase, maxSize, 1000);

            PrimesGenerator generator = new PrimesGenerator(numberBase, maxSize, maxSize / 4);
            BigInteger n = generator.GetRandomNumber(text1, timeSpan1, text2, timeSpan2, useRandomOrg);

            if (n == zero)
            {
                guiChange = delegate()
                {
                    btPrimeGen.Text = "Generate Prime";
                    EnableUserItems();
                };
                this.Invoke(guiChange);

                MessageBox.Show("Could not connect to the Internet !", "Failed Connection Attempt",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            TimeSpan start = DateTime.Now.TimeOfDay, finish;

            if (useRandomOrg)
            {
                guiChange = delegate()
                {
                    tbPrimeGenLog.Text += "Using atmospheric noise sensors from www.random.org to generate " +
                                          "the starting random number...\r\n\r\n";
                };
                this.Invoke(guiChange);
            }
            else
            {
                guiChange = delegate()
                {
                    tbPrimeGenLog.Text += "Using the input strings to generate the starting random number...\r\n\r\n";
                };
                this.Invoke(guiChange);
            }

            guiChange = delegate()
            {
                tbPrimeGenLog.Text += "Starting from the generated random number: " + n.ToString() + "\r\n\r\n";
            };
            this.Invoke(guiChange);

            bool probablyPrime;
            BigInteger number = new BigInteger(n);

            if (number % two == zero)
            {
                number++;

                guiChange = delegate()
                {
                    tbPrimeGenLog.Text += "The generated number is even, adding 1 to it.\r\n";
                };
                this.Invoke(guiChange);
            }

            for (; ; number += two)
            {
                probablyPrime = true;

                if ((probablyPrime) && (number > oneThousand))
                    foreach (BigInteger smallPrime in smallPrimesList)
                        if (number % smallPrime == zero)
                        {
                            probablyPrime = false;

                            guiChange = delegate()
                            {
                                tbPrimeGenLog.Text += "The current number is not prime (divisible by " +
                                                      smallPrime + ").\r\n";
                            };
                            this.Invoke(guiChange);

                            break;
                        }

                if (probablyPrime)
                {
                    probablyPrime = PrimesGenerator.MillerRabinTest(number);
                    if (probablyPrime)
                    {
                        finish = DateTime.Now.TimeOfDay;

                        guiChange = delegate()
                        {
                            tbPrimeGenLog.Text += "\r\nThe generated prime is: " + number.ToString() + "\r\n\r\n";
                            tbPrimeGenLog.Text += "Prime generation required " +
                                                  finish.Subtract(start).TotalSeconds + " seconds.\r\n";
                        };
                        this.Invoke(guiChange);

                        generatedPrime = new BigInteger(number);

                        guiChange = delegate()
                        {
                            btPrimeGen.Text = "Generate Prime";
                            EnableUserItems();
                        };
                        this.Invoke(guiChange);

                        return;
                    }

                    guiChange = delegate()
                    {
                        tbPrimeGenLog.Text += "The current number is not prime (by Miller-Rabin Test).\r\n";
                    };
                    this.Invoke(guiChange);
                }
            }
        }
    }
}
