using System;
using System.IO;
using System.Net;
using BigIntegerImplementation;

namespace PrimesGeneratorImplementation
{
    public class PrimesGenerator
    {
        private const int maxIter = 3;      // maximum number of iterations for the Miller-Rabin test
        private int numberBase;             // numberation base (from 2 to 65536)
        private int maxSize;                // maximum number of digits of the BigInteger
        private int size;                   // actual number of digits of the BigInteger

        private int[] seeds;                // seeds array (in base 10)
        private int[] digits;               // digits array (in the prestablished numeration base)

        /// <summary>
        /// Default constructor for the PrimeGenerator.
        /// </summary>
        public PrimesGenerator(int numberBase, int maxSize, int size)
        {
            this.numberBase = numberBase;
            this.maxSize = maxSize;
            this.size = size;

            digits = new int[maxSize];
            seeds = new int[size];
        }

        /// <summary>
        /// Returns a random integer.
        /// </summary>
        private int FindRandomSeed(int coef, string text, long timeSpan)
        {
            long term1, term2, term3, term4;
            long currentTime, textNumericalRep, numberPower;
            long seedNumber = 1;

            for (int i = 0; i < text.Length; i++)
            {
                term1 = seedNumber * coef;

                currentTime = DateTime.Now.ToBinary();
                term2 = currentTime * coef * (i + 1);

                textNumericalRep = (long)Math.Pow((int)text[i], (i + coef + 1));
                term3 = textNumericalRep * coef;

                numberPower = (long)Math.Pow(timeSpan, (i + coef + 1));
                term4 = numberPower * coef;

                seedNumber += term1 + term2 + term3 + term4;
                currentTime = DateTime.Now.ToBinary();
                seedNumber = seedNumber * currentTime;
            }

            return (int)Math.Abs(seedNumber % int.MaxValue);
        }

        /// <summary>
        /// Computes the array of random seeds.
        /// </summary>
        private void FindSeeds(string text, long timeSpan)
        {
            for (int i = 0; i < size; i++)
            {
                seeds[i] = FindRandomSeed(i, text, timeSpan);
                if (seeds[i] < 0)
                    seeds[i] = -seeds[i];
            }
        }

        /// <summary>
        /// Interchanges two digits of the BigInteger.
        /// </summary>
        private void InterchangeDigits(int i)
        {
            Random random;
            int tempDigit, index;

            random = new Random(seeds[i]);
            index = random.Next() % size;

            tempDigit = digits[i];
            digits[i] = digits[index];
            digits[index] = tempDigit;
        }

        /// <summary>
        /// Generates the BigInteger digit-by-digit, w.r.t the previously determined
        /// random seeds.
        /// </summary>
        private bool BuildNumber(string text, long timeSpan, bool useRandomOrg)
        {
            Random random;

            if (useRandomOrg)
            {
                try
                {
                    RetrieveSeedsFromRandomOrg();
                }

                catch (Exception)
                {
                    return false;
                }
            }

            else
                FindSeeds(text, timeSpan);


            for (int i = 0; i < size; i++)
            {
                random = new Random(seeds[i]);
                digits[i] = random.Next() % numberBase;
            }

            return true;
        }

        /// <summary>
        /// Randomizes the order in which the digits appear in the BigInteger.
        /// </summary>
        private bool RandomizeDigitOrder(string text, long timeSpan, bool useRandomOrg)
        {
            Random random;
            int margin = 1;

            if (useRandomOrg)
            {
                try
                {
                    RetrieveSeedsFromRandomOrg();
                }

                catch (Exception)
                {
                    return false;
                }
            }

            else
                FindSeeds(text, timeSpan);

            for (int i = 0; i < size; i++)
                InterchangeDigits(i);

            while (digits[size - 1] == 0)
            {
                random = new Random(seeds[size - 1] + margin);
                digits[size - 1] = random.Next() % numberBase;
                margin++;
            }

            return true;
        }

        /// <summary>
        /// Retrieves the random seeds using the atmospheric noise sensors from www.random.org.
        /// </summary>
        private void RetrieveSeedsFromRandomOrg()
        {
            string url = "http://www.random.org/integers/?num=" + size +
                         "&min=10000&max=1000000000&col=1&base=10&format=plain&rnd=new";
            string row;

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";

            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);

            for (int i = 0; i < size; i++)
            {
                row = sr.ReadLine();
                seeds[i] = int.Parse(row);
            }

            sr.Close();
            myResponse.Close();
        }

        /// <summary>
        /// Retrieves the random generated BigInteger.
        /// </summary>
        public BigInteger GetRandomNumber(string text1, long timeSpan1, string text2, long timeSpan2,
                                          bool useRandomOrg)
        {
            BigInteger res;

            if (BuildNumber(text1, timeSpan1, useRandomOrg) == false)
                return new BigInteger(numberBase, maxSize, 0);

            if (RandomizeDigitOrder(text2, timeSpan2, useRandomOrg) == false)
                return new BigInteger(numberBase, maxSize, 0);

            res = new BigInteger(numberBase, maxSize, false, size, digits);
            return res;
        }

        /// <summary>
        /// Determines whether the given BigInteger number is probably prime, with a probability
        /// of 1/(4^maxIter), using the Miller-Rabin primality test.
        /// </summary>
        public static bool MillerRabinTest(BigInteger n)
        {
            BigInteger s = new BigInteger(n.NumberBase, n.MaxSize);
            BigInteger t = n - 1;
            BigInteger zero = new BigInteger(n.NumberBase, n.MaxSize);
            BigInteger one = new BigInteger(n.NumberBase, n.MaxSize, 1);
            BigInteger two = new BigInteger(n.NumberBase, n.MaxSize, 2);
            BigInteger three = new BigInteger(n.NumberBase, n.MaxSize, 3);
            BigInteger b = new BigInteger(n.NumberBase, n.MaxSize, 2);
            BigInteger nmin1 = n - 1;
            BigInteger r, j, smin1;

            if (n == one)
                return false;
            if (n == two)
                return true;
            if (n == three)
                return true;

            while (t % two == zero)
            {
                t /= two;
                s++;
            }

            smin1 = s - 1;

            for (int i = 0; i < maxIter; i++)
            {
                r = BigInteger.PowerRepeatedSquaring(b, t, n);

                if ((r != one) && (r != nmin1))
                {
                    j = new BigInteger(one);
                    while ((j <= smin1) && (r != nmin1))
                    {
                        r = (r * r) % n;
                        if (r == one)
                            return false;
                        j++;
                    }
                    if (r != nmin1)
                        return false;
                }

                if (b == two)
                    b++;
                else
                    b += two;
            }

            return true;
        }
    }
}
