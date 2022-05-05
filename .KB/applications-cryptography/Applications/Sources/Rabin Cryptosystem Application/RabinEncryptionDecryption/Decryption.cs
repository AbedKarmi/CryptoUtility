using System.IO;
using BigIntegerImplementation;

namespace RabinEncryptionDecryptionImplementation
{
    public class Decryption
    {
        private int numberBase;
        private int maxSize;

        private BigInteger n, p, q, pinv, qinv;

        private int bufferSize;
        private int shiftDigits;
        private int saltDigits;

        private BinaryReader br;
        private BinaryWriter bw;

        private BigInteger minusOne, one, two, three, four, five, eight;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Decryption(BigInteger n, BigInteger p, BigInteger q,
                          int bufferSize, int shiftDigits, int saltDigits, BinaryReader br, BinaryWriter bw)
        {
            numberBase = p.NumberBase;
            maxSize = p.MaxSize;

            this.n = new BigInteger(n);
            this.p = new BigInteger(p);
            this.q = new BigInteger(q);

            pinv = BigInteger.ModularInverse(p, q); // pinv = p^(-1) mod q
            qinv = BigInteger.ModularInverse(q, p); // qinv = q^(-1) mod p

            this.bufferSize = bufferSize;
            this.shiftDigits = shiftDigits;
            this.saltDigits = saltDigits;

            this.br = br;
            this.bw = bw;

            minusOne = new BigInteger(numberBase, maxSize, -1);
            one = new BigInteger(numberBase, maxSize, 1);
            two = new BigInteger(numberBase, maxSize, 2);
            three = new BigInteger(numberBase, maxSize, 3);
            four = new BigInteger(numberBase, maxSize, 4);
            five = new BigInteger(numberBase, maxSize, 5);
            eight = new BigInteger(numberBase, maxSize, 8);
        }

        /// <summary>
        /// Finds the solution for x^2 = a (mod p).
        /// </summary>
        private BigInteger FindDecryptionSolution(BigInteger a, BigInteger p)
        {
            BigInteger x = null;

            if ((p % four) == three)
                x = BigInteger.PowerRepeatedSquaring(a, (p + one) / four, p);
            else if ((p % eight) == five)
                if (BigInteger.PowerRepeatedSquaring(a, (p - one) / four, p) == one)
                    x = BigInteger.PowerRepeatedSquaring(a, (p + three) / eight, p);
                else
                    x = (((a * two) % n) *
                        BigInteger.PowerRepeatedSquaring((a * four) % n, (p - five) / eight, p)) % p;

            return x;
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
            BigInteger[] sol = new BigInteger[4]; /* the 4 possible numerical decryptions for each group
                                                   * of l letters */

            BigInteger t1, t2, t3, t4;
            BigInteger x, y;
            BigInteger a, b; // x^2 = a (mod p), y^2 = b (mod q)

            a = number % p;
            b = number % q;

            x = FindDecryptionSolution(a, p);
            y = FindDecryptionSolution(b, q);

            t1 = (((x * q) % n) * qinv) % n;
            t2 = (((y * p) % n) * pinv) % n;
            t3 = (((x * (n - q)) % n) * qinv) % n;
            t4 = (((y * (n - p)) % n) * pinv) % n;

            // sol[i], i=0,3 are the possible 4 numerical solutions
            sol[0] = (t1 + t2) % n;
            sol[1] = (t3 + t2) % n;
            sol[2] = (t1 + t4) % n;
            sol[3] = (t3 + t4) % n;

            for (int i = 0; i < 4; i++)
            {
                sol[i] = BigInteger.RemoveSalt(sol[i], saltDigits);

                if (BigInteger.CheckReplication(sol[i], shiftDigits) == true)
                {
                    BigInteger res = BigInteger.Dereplicate(sol[i], shiftDigits);
                    return res;
                }
            }

            return new BigInteger(minusOne);
        }
    }
}
