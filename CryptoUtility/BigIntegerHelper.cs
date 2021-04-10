using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoUtility
{
    static class BigIntegerHelper
    {

        private static readonly BigInteger Ten = new BigInteger(10);
        public static BigInteger ToBigInteger(this ulong ul)
        {
            return new BigInteger(ul);
        }
        public static BigInteger ToBigInteger(this long ul)
        {
            return new BigInteger((ulong)ul);
        }
        public static BigInteger ToBigInteger(this int ul)
        {
            return new BigInteger((ulong)ul);
        }
        public static BigInteger ToBigInteger(this uint ul)
        {
            return new BigInteger((ulong)ul);
        }
        public static BigInteger PowBySquaring(BigInteger x, BigInteger n)
        {
            if (n < 0) return PowBySquaring(BigInteger.Divide(1, x), -n);
            else if (n == 0) return 1;
            else if (n == 1) return x;
            else if (n.IsEven) return PowBySquaring(BigInteger.Multiply(x, x), BigInteger.Divide(n, 2));

            return BigInteger.Multiply(x, PowBySquaring(BigInteger.Multiply(x, x), BigInteger.Divide(BigInteger.Subtract(n, 1), 2)));
        }

        public static BigInteger FastPow(BigInteger x, BigInteger n)
        {
            BigInteger a = 1;
            BigInteger c = x;

            do
            {
                BigInteger r = n % 2;
                if (r == 1) a = KaratsubaMultiply(1, c);
                n >>= 1;
                c = KaratsubaMultiply(c, c);
            } while (n != 0);
            return a;
        }

        public static BigInteger GetBig(string hexNum, bool forcePositive = true)
        {
            //return GetBig(MyClass.HexStringToBinary(hexNum));
            return GetBig(HexToByteArray(hexNum), forcePositive);
        }
        public static BigInteger GetBig(byte[] data,bool forcePositive=true)
        {
            if (data.Length == 0) return new BigInteger(data);
            int n = data.Length;
            if (data[data.Length - 1] == 0 && n>1) n--; // Remove Left Zero Before Reverse, otherwise will be to the right !
            byte[] inArr = new byte[n];
            Array.Copy(data, inArr, n);
            //byte[] inArr = (byte[])data.Clone();
            int m = forcePositive?inArr[n - 1] >> 7:0;  // Force positive
            Array.Reverse(inArr);                       // Reverse the byte order
            byte[] final = new byte[inArr.Length +m];   // Add an empty byte at the end, to simulate unsigned BigInteger (no negatives!)
            Array.Copy(inArr, final, inArr.Length);

            return new BigInteger(final);
        }
        public static BigInteger KaratsubaMultiply(BigInteger x, BigInteger y)
        {
            int size1 = GetSize(x);
            int size2 = GetSize(y);

            //find the max size of two integers
            int N = Math.Max(size1, size2);

            if (N < 2)
                return x * y;

            //Max length divided by two and rounded up
            N = (N / 2) + (N % 2);

            //The mulitplying factor for calculating a,b,c,d
            BigInteger m = BigInteger.Pow(10, N);

            BigInteger b = x % m;
            BigInteger a = x / m;
            BigInteger c = y / m;
            BigInteger d = y % m;

            BigInteger z0 = KaratsubaMultiply(a, c);
            BigInteger z1 = KaratsubaMultiply(b, d);
            BigInteger z2 = KaratsubaMultiply(a + b, c + d);

            return (BigInteger.Pow(10, N * 2) * z0) + z1 + ((z2 - z1 - z0) * m);
        }

        /// <summary>
        /// returns the size of the long integers
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static int GetSize(BigInteger num)
        {
            int len = 0;
            while (num != 0)
            {
                len++;
                num /= 10;
            }
            return len;
        }

        public static bool IsPrime(BigInteger n)
        {
            if (n <= 1) return false;
            else if (n <= 3) return true;
            else if ((n % 2 == 0) || (n % 3 == 0)) return false;
            BigInteger i = 5;
            while (BigInteger.Multiply(i, i) <= n)
            {
                if ((n % i == 0) || (n % (i + 2) == 0)) return false;
                i += 6;
            }
            return true;
        }

        public static BigInteger Sqrt(this BigInteger number)
        {
            BigInteger n = 0, p = 0;
            if (number == BigInteger.Zero)
                return BigInteger.Zero;
            var high = number >> 1;
            var low = BigInteger.Zero;
            while (high > low + 1)
            {
                n = (high + low) >> 1;
                p = n * n;
                if (number < p)
                    high = n;
                else if (number > p)
                    low = n;
                else
                    break;
            }

            return number == p ? n : low;
        }

        /// <summary>
        ///     Creates a new BigInteger from a binary (Base2) string
        /// </summary>
        public static BigInteger NewBigInteger2(this string binaryValue)
        {
            BigInteger res = 0;
            if (binaryValue.Count(b => b == '1') + binaryValue.Count(b => b == '0') != binaryValue.Length) return res;
            foreach (var c in binaryValue)
            {
                res <<= 1;
                res += c == '1' ? 1 : 0;
            }

            return res;
        }
        /// <summary>
        ///     Get the bitwidth of this biginteger n
        /// </summary>
        public static int GetActualBitwidth(this BigInteger n)
        {
            int i = 0;
            while (n>0) { n >>= 1;i++; }
            return i;
        }

        public static int AdjustBits(int n)
        {
            int m = n % 8;
            if (m > 0) n += (8 - m);
            return n;
        }
        public static int GetBitwidth(this BigInteger n)
        {
            byte[] b= n.ToByteArray();
            int i = b.Length;
            if (b[b.Length - 1] == 0) i--;
            return (i << 3);
        }
        public static bool IsEven(this int num)
        {
            return (num & 1) == 0;
        }
        public static bool ContainsOnly(this string str,string digits)
        {
            foreach (var c in str) if (!digits.Contains(c)) return false;
            return true;
        }
        /// <summary>
        ///     Get the Maxvalue for a biginteger of this bitlength
        /// </summary>
        public static BigInteger GetMaxValue(int bitlength)
        {
            var buffer = "";
            if (!bitlength.IsEven())
                buffer = "7f";
            var ByteLength = bitlength >> 3;
            for (var i = 0; i < ByteLength; ++i)
                buffer += "ff";
            return ToBigInteger16(buffer);
        }
        /// <summary>
        ///     Converts a hex number (0xABCDEF or ABCDEF) into a BigInteger
        /// </summary>
        public static BigInteger ToBigInteger16(this string hexNumber)
        {
            if (string.IsNullOrEmpty(hexNumber))
                throw new Exception("Error: hexNumber cannot be either null or have a length of zero.");
            if (!hexNumber.ContainsOnly("0123456789abcdefABCDEFxX"))
                throw new Exception("Error: hexNumber cannot contain characters other than 0-9,a-f,A-F, or xX");
            hexNumber = hexNumber.ToUpper();
            if (hexNumber.IndexOf("0X", StringComparison.OrdinalIgnoreCase) != -1)
                hexNumber = hexNumber.Substring(2);
            var bytes = Enumerable.Range(0, hexNumber.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hexNumber.Substring(x, 2), 16))
                .ToArray();
            return new BigInteger(bytes.Concat(new byte[] { 0 }).ToArray());
        }
        /// <summary>
        ///     Creates a new BigInteger from a BigInteger
        /// </summary>
        public static BigInteger NewBigInteger(this BigInteger value)
        {
            return new BigInteger(value.ToByteArray());
        }
        /// <summary>
        ///     Creates a new BigInteger from a hex (Base16) string
        /// </summary>
        public static BigInteger NewBigInteger16(this string hexValue)
        {
            return new BigInteger(HexToByteArray(hexValue).Concat(new byte[] { 0 }).ToArray());
        }
        /// <summary>
        ///     Creates a new BigInteger from a number (Base10) string
        /// </summary>
        public static BigInteger NewBigInteger10(this string str)
        {
            if (str[0] == '-')
                throw new Exception("Invalid numeric string. Only positive numbers are allowed.");
            var number = new BigInteger();
            int i;
            for (i = 0; i < str.Length; i++)
                if (str[i] >= '0' && str[i] <= '9')
                    number = number * Ten + long.Parse(str[i].ToString());
            return number;
        }
        public static BigInteger ToBigIntegerBase10(this string str)
        {
            if (str[0] == '-')
                throw new Exception("Invalid numeric string. Only positive numbers are allowed.");
            var number = new BigInteger();
            int i;
            for (i = 0; i < str.Length; i++)
                if (str[i] >= '0' && str[i] <= '9')
                    number = number * Ten + long.Parse(str[i].ToString());
            return number;
        }
        /// <summary>
        ///     Return a byte array that represents this hex string
        /// </summary>
        private static byte[] HexToByteArray(string hex)
        {
            byte[] hr;
            try
            {
                if (!hex.Length.IsEven()) hex = "0" + hex;

                hr = Enumerable.Range(0, hex.Length)
                    .Where(x => x % 2 == 0)
                    .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                    .ToArray();
            }
            catch (Exception) { hr = new byte[0] { }; };
            return hr;
        }
        /// <summary>
        ///     Ensures that the BigInteger value will be a positive number. BigInteger is Big-endian
        ///     (the most significant byte is in the [0] position)
        /// </summary>
        public static byte[] EnsurePositiveNumber(this byte[] ba)
        {
            return ba.Concat(new byte[] { 0 }).ToArray();
        }
        /// <summary>
        ///     Converts from a BigInteger to a binary string.
        /// </summary>
        public static string ToBinaryString(this BigInteger bigint) 
        {
            var bytes = bigint.ToByteArray();                                              
            Array.Reverse(bytes);
            var base2 = new StringBuilder(bytes.Length * 8);
            var binary = Convert.ToString(bytes[0], 2);
            if (binary[0] != '0' && bigint.Sign == 1) base2.Append('0');
            base2.Append(binary);
            for (int index=1; index <bytes.Length; index++)
                base2.Append(Convert.ToString(bytes[index], 2).PadLeft(8, '0'));
            return base2.ToString();
        }
        /// <summary>
        ///     Converts from a BigInteger to a hexadecimal string.
        /// </summary>
        public static string ToHexString(this BigInteger bi)
        {
            var bytes = bi.ToByteArray();
            Array.Reverse(bytes);
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                var hex = b.ToString("X2");
                sb.Append(hex);
            }

            return sb.ToString();
        }
        /// <summary>
        ///     Converts from a BigInteger to a octal string.
        /// </summary>
        public static string ToOctalString(this BigInteger bigint)
        {
            var bytes = bigint.ToByteArray();
            Array.Reverse(bytes);
            var index = bytes.Length - 1;
            var base8 = new StringBuilder((bytes.Length / 3 + 1) * 8);
            var rem = bytes.Length % 3;
            if (rem == 0) rem = 3;
            var base0 = 0;
            while (rem != 0)
            {
                base0 <<= 8;
                base0 += bytes[index--];
                rem--;
            }

            var octal = Convert.ToString(base0, 8);
            if (octal[0] != '0' && bigint.Sign == 1) base8.Append('0');
            base8.Append(octal);
            while (index >= 0)
            {
                base0 = (bytes[index] << 16) + (bytes[index - 1] << 8) + bytes[index - 2];
                base8.Append(Convert.ToString(base0, 8).PadLeft(8, '0'));
                index -= 3;
            }

            return base8.ToString();
        }

        /*
    * public void testPrimes()
          {

                      int bitlen = 2048;
                      BigInteger RandomNumber = NextBigInteger(bitlen);

                      if (IsProbablePrime(RandomNumber, 100) == true)
                          Console.WriteLine("\nGenerated Random number is prime!");
                      else
                          Console.WriteLine("\nGenerated Random number is not prime!!");
          }
   */

        public static IList<BigInteger> GetFactors(BigInteger n)
        {
            List<BigInteger> factors = new();
            BigInteger x = 2;
            while (x <= n)
            {
                if (n % x == 0)
                {
                    factors.Add(x);
                    n /= x;
                }
                else
                {
                    x++;
                    if (x * x >= n)
                    {
                        factors.Add(n);
                        break;
                    }
                }
            }
            return factors;
        }
        public static BigInteger[] gcdWithBezout(BigInteger p, BigInteger q)
        {
            if (q == 0)
                return new BigInteger[] { p, 1, 0 };

            BigInteger[] vals = gcdWithBezout(q, p % q);
            BigInteger d = vals[0];
            BigInteger a = vals[2];
            BigInteger b = vals[1] - (p / q) * vals[2];

            return new BigInteger[] { d, a, b };
        }
        public static BigInteger GenPrime(int bitLength)
        {
            BigInteger p;
            do
            {
                p = NextBigInteger(bitLength);
            } while (!IsProbablePrime(p, 100));
            return p;
        }

        public static  BigInteger NextBigInteger(int bitLength)
        {
            if (bitLength < 1) return BigInteger.Zero;

            int bytes = bitLength / 8;
            int bits = bitLength % 8;

            // Generates enough random bytes to cover our bits.
            Random rnd = new();
            byte[] bs = new byte[bytes + 1];
            rnd.NextBytes(bs);

            // Mask out the unnecessary bits.
            byte mask = (byte)(0xFF >> (8 - bits));
            bs[bs.Length - 1] &= mask;

            return new BigInteger(bs);
        }

        // Random Integer Generator within the given range
        public static BigInteger RandomBigInteger(BigInteger start, BigInteger end)
        {
            if (start == end) return start;

            BigInteger res = end;

            // Swap start and end if given in reverse order.
            if (start > end)
            {
                end = start;
                start = res;
                res = end - start;
            }
            else
                // The distance between start and end to generate a random BigIntger between 0 and (end-start) (non-inclusive).
                res -= start;

            byte[] bs = res.ToByteArray();

            // Count the number of bits necessary for res.
            int bits = 8;
            byte mask = 0x7F;
            while ((bs[bs.Length - 1] & mask) == bs[bs.Length - 1])
            {
                bits--;
                mask >>= 1;
            }
            bits += 8 * bs.Length;

            // Generate a random BigInteger that is the first power of 2 larger than res, 
            // then scale the range down to the size of res,
            // finally add start back on to shift back to the desired range and return.
            return ((NextBigInteger(bits + 1) * res) / BigInteger.Pow(2, bits + 1)) + start;
        }



        // Miller-Rabin primality test as an extension method on the BigInteger type.
        // Based on the Ruby implementation on this page.

        public static bool IsProbablePrime(BigInteger source, int certainty)
        {
            if (source == 2 || source == 3)
                return true;
            if (source < 2 || source % 2 == 0)
                return false;

            BigInteger d = source - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }

            // There is no built-in method for generating random BigInteger values.
            // Instead, random BigIntegers are constructed from randomly generated
            // byte arrays of the same length as the source.
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[source.ToByteArray().LongLength];
            BigInteger a;

            for (int i = 0; i < certainty; i++)
            {
                do
                {
                    // This may raise an exception in Mono 2.10.8 and earlier.
                    // http://bugzilla.xamarin.com/show_bug.cgi?id=2761
                    rng.GetBytes(bytes);
                    a = new BigInteger(bytes);
                }
                while (a < 2 || a >= source - 2);

                BigInteger x = BigInteger.ModPow(a, d, source);
                if (x == 1 || x == source - 1)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, source);
                    if (x == 1)
                        return false;
                    if (x == source - 1)
                        break;
                }

                if (x != source - 1)
                    return false;
            }

            return true;
        }
    }
}
