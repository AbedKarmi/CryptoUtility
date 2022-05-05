using System.IO;
using BigIntegerImplementation;

namespace RSAEncryptionDecryptionImplementation
{
    public class Decryption
    {
        private int numberBase;
        private int maxSize;

        private BigInteger n;
        private BigInteger d;

        private int bufferSize;

        private BinaryReader br;
        private BinaryWriter bw;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Decryption(BigInteger n, BigInteger d, int bufferSize, BinaryReader br, BinaryWriter bw)
        {
            numberBase = n.NumberBase;
            maxSize = n.MaxSize;

            this.bufferSize = bufferSize;

            this.n = new BigInteger(n);
            this.d = new BigInteger(d);

            this.br = br;
            this.bw = bw;
        }

        /// <summary>
        /// Reads the public key to the data stream.
        /// </summary>
        public static BigInteger ReadPublicKey(int numberBase, int maxSize, string filename)
        {
            FileStream fs = File.OpenRead(filename);
            BinaryReader br = new BinaryReader(fs);

            BigInteger res = new BigInteger(numberBase, maxSize, false, br);

            br.Close();
            fs.Close();

            return res;
        }

        /// <summary>
        /// Writes the decrypted data stream to a file.
        /// </summary>
        public void WriteDecryptedData(BigInteger decryptedNumber, int digitDifference, int intReadSize,
                                       bool lastIntIncomplete)
        {
            int i, lastPosition;

            for (i = 0; i < digitDifference; i++)
                bw.Write((ushort)0);

            lastPosition = bufferSize - (intReadSize - digitDifference);

            for (i = bufferSize - 1; i >= lastPosition; i--)
                if ((i == lastPosition) && (lastIntIncomplete))
                    bw.Write((byte)decryptedNumber[i]);
                else
                    bw.Write((ushort)decryptedNumber[i]);
        }

        /// <summary>
        /// Decryption method for an individual BigInteger.
        /// </summary>
        public BigInteger Decrypt(BigInteger number)
        {
            BigInteger res = BigInteger.PowerRepeatedSquaring(number, d, n);

            return res;
        }
    }
}
