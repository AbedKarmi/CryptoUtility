using System.IO;
using BigIntegerImplementation;

namespace RSAEncryptionDecryptionImplementation
{
    public class Encryption
    {
        private int numberBase;
        private int maxSize;

        private BigInteger n;
        private BigInteger e;

        private int bufferSize;

        private BinaryReader br;
        private BinaryWriter bw;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Encryption(BigInteger n, BigInteger e, int bufferSize, BinaryReader br, BinaryWriter bw)
        {
            numberBase = n.NumberBase;
            maxSize = n.MaxSize;

            this.n = new BigInteger(n);
            this.e = new BigInteger(e);

            this.bufferSize = bufferSize;

            this.br = br;
            this.bw = bw;
        }

        /// <summary>
        /// Encryption method for an individual BigInteger.
        /// </summary>
        public BigInteger Encrypt(BigInteger number)
        {
            BigInteger res = BigInteger.PowerRepeatedSquaring(number, e, n);

            return res;
        }

        /// <summary>
        /// Writes the encrypted data stream to a file.
        /// </summary>
        public void WriteEncryptedData(BigInteger encryptedNumber, int digitDifference, int intReadCount,
                                       bool lastIntIncomplete)
        {
            bw.Write((byte)digitDifference);
            bw.Write((byte)intReadCount);
            bw.Write(lastIntIncomplete);
            encryptedNumber.Serialize(bw);
        }

        /// <summary>
        /// Reads a plain data stream from of a file. 
        /// </summary>
        public int[] ReadPlainData(out int digitDifference, out int intReadCount, out bool lastIntIncomplete)
        {
            int byteBufferSize = 2 * bufferSize;
            byte[] byteBuffer = new byte[byteBufferSize];
            int byteReadCount = br.Read(byteBuffer, 0, byteBufferSize);

            int i, j, lowByte, numberSize;
            int[] intBuffer;

            if ((byteReadCount % 2) == 0)
            {
                intReadCount = byteReadCount / 2;
                lastIntIncomplete = false;
            }
            else
            {
                intReadCount = byteReadCount / 2 + 1;
                lastIntIncomplete = true;
            }

            intBuffer = new int[bufferSize];

            j = 0;
            for (i = (bufferSize - 1); i >= (bufferSize - intReadCount); i--)
            {
                lowByte = (int)byteBuffer[j];
                j++;

                if ((j == byteReadCount) && (lastIntIncomplete))
                {
                    intBuffer[i] = lowByte;
                    break;
                }

                intBuffer[i] = (int)byteBuffer[j];
                intBuffer[i] <<= 8;

                intBuffer[i] += lowByte;
                j++;
            }

            for (i = (bufferSize - intReadCount - 1); i >= 0; i--)
                intBuffer[i] = 0;

            numberSize = bufferSize;
            digitDifference = 0;

            while (numberSize - 1 > 0)
                if (intBuffer[numberSize - 1] == 0)
                    numberSize--;
                else
                    break;

            if (numberSize != bufferSize)
            {
                if ((numberSize == 1) && (intBuffer[0] == 0))
                    return null;
                digitDifference = bufferSize - numberSize;
                for (i = (bufferSize - 1); i >= digitDifference; i--)
                    intBuffer[i] = intBuffer[i - digitDifference];
                for (i = (digitDifference - 1); i >= 0; i--)
                    intBuffer[i] = 0;
            }

            return intBuffer;
        }
    }
}

