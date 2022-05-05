using System;
using System.IO;

namespace BigIntegerImplementation
{
    public class BigInteger
    {
        private int numberBase;       // numeration base (from 2 to 65536)
        private int maxSize;          // maximum number of digits of the BigInteger

        private int[] digits;         // digits array (in the prestablished numeration base)

        private int size;             // actual number of digits of the BigInteger
        private bool isNegative;        /* false - the number is positive
                                         * true - the number is negative */

        /// <summary>
        /// Makes the numeration base of the BigInteger public for reading.
        /// </summary>
        public int NumberBase
        {
            get
            {
                return numberBase;
            }
        }

        /// <summary>
        ///  Makes the maximum number of digits of the BigInteger public for reading.
        /// </summary>
        public int MaxSize
        {
            get
            {
                return maxSize;
            }
        }

        /// <summary>
        /// Makes the actual number of digits of the BigInteger public for reading.
        /// </summary>
        public int Size
        {
            get
            {
                return size;
            }
        }

        /// <summary>
        /// Makes the digits of the BigInteger public for reading.
        /// </summary>
        public int this[int i]
        {
            get
            {
                return digits[i];
            }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BigInteger(int numberBase, int maxSize)
        {
            this.numberBase = numberBase;
            this.maxSize = maxSize;

            digits = new int[maxSize];

            size = 1;
            for (int i = 0; i < maxSize; i++)
                digits[i] = 0;
            isNegative = false;
        }

        /// <summary>
        /// Constructor creating a new BigInteger as a conversion of a
        /// regular base-10 int.
        /// </summary>
        public BigInteger(int numberBase, int maxSize, int n)
        {
            this.numberBase = numberBase;
            this.maxSize = maxSize;

            digits = new int[maxSize];

            isNegative = false;
            bool flag = false;
            if (n == 0)
                flag = true;
            else if (n < 0)
            {
                n = -n;
                isNegative = true;
            }

            size = 0;
            while (n > 0)
            {
                digits[size] = n % numberBase;
                n /= numberBase;
                size++;
            }

            for (int i = size; i < maxSize; i++)
                digits[i] = 0;

            if (flag)
                size = 1;
        }

        /// <summary>
        /// Constructor creating a new BigInteger from an array of digits (in any base).
        /// </summary>
        public BigInteger(int numberBase, int maxSize, bool isNegative, int size, int[] digits)
        {
            int i;

            this.numberBase = numberBase;
            this.maxSize = maxSize;

            this.digits = new int[maxSize];

            this.size = size;
            this.isNegative = isNegative;

            for (i = 0; i < size; i++)
                this.digits[i] = digits[i];
            for (i = size; i < maxSize; i++)
                this.digits[i] = 0;
        }

        /// <summary>
        /// Constructor creating a new BigInteger as a copy of an existing BigInteger.
        /// </summary>
        public BigInteger(BigInteger n)
        {
            int i;

            numberBase = n.numberBase;
            maxSize = n.maxSize;

            digits = new int[maxSize];

            isNegative = n.isNegative;
            size = n.size;

            for (i = 0; i < size; i++)
                digits[i] = n.digits[i];
            for (i = size; i < maxSize; i++)
                digits[i] = 0;
        }

        /// <summary>
        /// Constructor creating a new BigInteger out of a binary file stream content.
        /// </summary>
        public BigInteger(int numberBase, int maxSize, bool isNegative, BinaryReader br)
        {
            int i;

            this.numberBase = numberBase;
            this.maxSize = maxSize;
            this.isNegative = isNegative;

            size = (int)br.ReadInt16();
            digits = new int[maxSize];

            for (i = 0; i < size; i++)
                digits[i] = (int)((ushort)br.ReadInt16());
            for (i = size; i < maxSize; i++)
                digits[i] = 0;
        }

        /// <summary>
        /// BigInteger inverse with respect to addition.
        /// </summary>
        public static BigInteger operator -(BigInteger n)
        {
            BigInteger res = new BigInteger(n);
            if (res.isNegative == false)
                res.isNegative = true;
            else
                res.isNegative = false;

            return res;
        }

        /// <summary>
        /// Incremetation operation of a BigInteger.
        /// </summary>
        public static BigInteger operator ++(BigInteger n)
        {
            BigInteger res = n + 1;
            return res;
        }

        /// <summary>
        /// Decremetation operation of a BigInteger.
        /// </summary>
        public static BigInteger operator --(BigInteger n)
        {
            BigInteger res = n - 1;
            return res;
        }

        public override bool Equals(object o)
        {
            return (this == (BigInteger)o);
        }

        public override int GetHashCode()
        {
            long result = 0;

            for (int i = 0; i < size; i++)
                result = result + digits[i];

            return (int)(result % int.MaxValue);
        }

        /// <summary>
        /// Equality test of two BigInteger's.
        /// </summary>
        public static bool operator ==(BigInteger a, BigInteger b)
        {
            if (a.numberBase != b.numberBase)
                return false;
            if (a.isNegative != b.isNegative)
                return false;
            if (a.size != b.size)
                return false;

            for (int i = 0; i < a.size; i++)
                if (a.digits[i] != b.digits[i])
                    return false;

            return true;
        }

        /// <summary>
        /// Equality test of a BigInteger and a base-10 int.
        /// </summary>
        public static bool operator ==(BigInteger a, int n)
        {
            BigInteger b = new BigInteger(a.NumberBase, a.MaxSize, n);
            if (a == b)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Inequality test of two BigInteger's.
        /// </summary>
        public static bool operator !=(BigInteger a, BigInteger b)
        {
            if (a == b)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Inequality test of a BigInteger and a base-10 int.
        /// </summary>
        public static bool operator !=(BigInteger a, int n)
        {
            BigInteger b = new BigInteger(a.NumberBase, a.MaxSize, n);
            if (a != b)
                return true;
            else
                return false;
        }

        /// <summary>
        /// "Greater" test of two BigInteger's.
        /// </summary>
        public static bool operator >(BigInteger a, BigInteger b)
        {
            if ((a.isNegative == true) && (b.isNegative == false))
                return false;
            if ((a.isNegative == false) && (b.isNegative == true))
                return true;
            if (a.isNegative == false)
            {
                if (a.size > b.size)
                    return true;
                if (a.size < b.size)
                    return false;
                for (int i = (a.size) - 1; i >= 0; i--)
                    if (a.digits[i] > b.digits[i])
                        return true;
                    else if (a.digits[i] < b.digits[i])
                        return false;
            }
            else
            {
                if (a.size < b.size)
                    return true;
                if (a.size > b.size)
                    return false;
                for (int i = (a.size) - 1; i >= 0; i--)
                    if (a.digits[i] < b.digits[i])
                        return true;
                    else if (a.digits[i] > b.digits[i])
                        return false;
            }

            return false;
        }

        /// <summary>
        /// "Greater" test of a BigInteger and a base-10 int.
        /// </summary>
        public static bool operator >(BigInteger a, int n)
        {
            BigInteger b = new BigInteger(a.NumberBase, a.MaxSize, n);
            if (a > b)
                return true;
            else return false;
        }

        /// <summary>
        /// "Greater or equal" test of two BigInteger's.
        /// </summary>
        public static bool operator >=(BigInteger a, BigInteger b)
        {
            if ((a > b) || (a == b))
                return true;
            else return false;
        }

        /// <summary>
        /// "Greater or equal" test of a BigInteger and a base-10 int.
        /// </summary>
        public static bool operator >=(BigInteger a, int n)
        {
            BigInteger b = new BigInteger(a.NumberBase, a.MaxSize, n);
            if (a >= b)
                return true;
            else return false;
        }

        /// <summary>
        /// "Smaller" test of two BigInteger's.
        /// </summary>
        public static bool operator <(BigInteger a, BigInteger b)
        {
            if (a >= b)
                return false;
            else return true;
        }

        /// <summary>
        /// "Smaller" test of a BigInteger and a base-10 int.
        /// </summary>
        public static bool operator <(BigInteger a, int n)
        {
            BigInteger b = new BigInteger(a.NumberBase, a.MaxSize, n);
            if (a < b)
                return true;
            else return false;
        }

        /// <summary>
        /// "Smaller or equal" test of two BigInteger's.
        /// </summary>
        public static bool operator <=(BigInteger a, BigInteger b)
        {
            if ((a < b) || (a == b))
                return true;
            else return false;
        }

        /// <summary>
        /// "Smaller or equal" test of one BigInteger and a base-10 int.
        /// </summary>
        public static bool operator <=(BigInteger a, int n)
        {
            BigInteger b = new BigInteger(a.NumberBase, a.MaxSize, n);
            if (a <= b)
                return true;
            else return false;
        }

        /// <summary>
        /// Addition operation of two BigInteger's.
        /// </summary>
        public static BigInteger operator +(BigInteger a, BigInteger b)
        {
            BigInteger res = null;

            if ((a.isNegative == false) && (b.isNegative == false))
                if (a >= b)
                {
                    res = Add(a, b);
                    res.isNegative = false;
                }
                else
                {
                    res = Add(b, a);
                    res.isNegative = false;
                }

            if ((a.isNegative == true) && (b.isNegative == true))
                if (a <= b)
                {
                    res = Add(-a, -b);
                    res.isNegative = true;
                }
                else
                {
                    res = Add(-b, -a);
                    res.isNegative = true;
                }

            if ((a.isNegative == false) && (b.isNegative == true))
                if (a >= (-b))
                {
                    res = Subtract(a, -b);
                    res.isNegative = false;
                }
                else
                {
                    res = Subtract(-b, a);
                    res.isNegative = true;
                }

            if ((a.isNegative == true) && (b.isNegative == false))
                if ((-a) <= b)
                {
                    res = Subtract(b, -a);
                    res.isNegative = false;
                }
                else
                {
                    res = Subtract(-a, b);
                    res.isNegative = true;
                }

            return res;
        }

        /// <summary>
        /// Addition operation of a BigInteger and a base-10 int.
        /// </summary>
        public static BigInteger operator +(BigInteger a, int n)
        {
            BigInteger b = new BigInteger(a.NumberBase, a.MaxSize, n);
            BigInteger res = a + b;
            return res;
        }

        /// <summary>
        /// Addition operation of a base-10 int and one BigInteger.
        /// </summary>
        public static BigInteger operator +(int n, BigInteger a)
        {
            BigInteger b = new BigInteger(a.NumberBase, a.MaxSize, n);
            BigInteger res = a + b;
            return res;
        }

        /// <summary>
        /// Subtraction operation of two BigInteger's.
        /// </summary>
        public static BigInteger operator -(BigInteger a, BigInteger b)
        {
            BigInteger res = null;

            if ((a.isNegative == false) && (b.isNegative == false))
                if (a >= b)
                {
                    res = Subtract(a, b);
                    res.isNegative = false;
                }
                else
                {
                    res = Subtract(b, a);
                    res.isNegative = true;
                }

            if ((a.isNegative == true) && (b.isNegative == true))
                if (a <= b)
                {
                    res = Subtract(-a, -b);
                    res.isNegative = true;
                }
                else
                {
                    res = Subtract(-b, -a);
                    res.isNegative = false;
                }

            if ((a.isNegative == false) && (b.isNegative == true))
                if (a >= (-b))
                {
                    res = Add(a, -b);
                    res.isNegative = false;
                }
                else
                {
                    res = Add(-b, a);
                    res.isNegative = false;
                }

            if ((a.isNegative == true) && (b.isNegative == false))
                if ((-a) >= b)
                {
                    res = Add(-a, b);
                    res.isNegative = true;
                }
                else
                {
                    res = Add(b, -a);
                    res.isNegative = true;
                }

            return res;
        }

        /// <summary>
        /// Subtraction operation of a BigInteger and a base-10 int.
        /// </summary>
        public static BigInteger operator -(BigInteger a, int n)
        {
            BigInteger b = new BigInteger(a.NumberBase, a.MaxSize, n);
            BigInteger res = a - b;
            return res;
        }

        /// <summary>
        /// Subtraction operation of a base-10 int and a BigInteger.
        /// </summary>
        public static BigInteger operator -(int n, BigInteger a)
        {
            BigInteger b = new BigInteger(a.NumberBase, a.MaxSize, n);
            BigInteger res = b - a;
            return res;
        }

        /// <summary>
        /// Multiplication operation of two BigInteger's.
        /// </summary>
        public static BigInteger operator *(BigInteger a, BigInteger b)
        {
            BigInteger zero = new BigInteger(a.NumberBase, a.MaxSize);
            if ((a == zero) || (b == zero))
                return zero;

            BigInteger res = null;
            res = Multiply(Abs(a), Abs(b));
            if (a.isNegative != b.isNegative)
                res.isNegative = true;
            else
                res.isNegative = false;

            return res;
        }

        /// <summary>
        /// Multiplication operation of a BigInteger and a base-10 int.
        /// </summary>
        public static BigInteger operator *(BigInteger a, int n)
        {
            BigInteger b = new BigInteger(a.NumberBase, a.MaxSize, n);
            BigInteger res = a * b;
            return res;
        }

        /// <summary>
        /// Multiplication operation of a base-10 int and a BigInteger.
        /// </summary>
        public static BigInteger operator *(int n, BigInteger a)
        {
            BigInteger b = new BigInteger(a.NumberBase, a.MaxSize, n);
            BigInteger res = a * b;
            return res;
        }

        /// <summary>
        /// Division operation of two BigInteger's a and b, b != 0.
        /// </summary>
        public static BigInteger operator /(BigInteger a, BigInteger b)
        {
            BigInteger zero = new BigInteger(a.NumberBase, a.MaxSize);
            if ((a == zero) || (b == zero))
                return zero;
            if (Abs(a) < Abs(b))
                return zero;

            BigInteger res = null;
            if (b.size == 1)
                res = DivideByOneDigitNumber(Abs(a), b.digits[0]);
            else
                res = DivideByBigNumber(Abs(a), Abs(b));

            if (a.isNegative != b.isNegative)
                res.isNegative = true;
            else
                res.isNegative = false;

            return res;
        }

        /// <summary>
        /// Division operation of a BigInteger a and a base-10 int n, n != 0.
        /// </summary>
        public static BigInteger operator /(BigInteger a, int n)
        {
            BigInteger b = new BigInteger(a.NumberBase, a.MaxSize, n);
            BigInteger res = a / b;
            return res;
        }

        /// <summary>
        /// Modulo operation of two BigInteger's a and b, b != 0.
        /// </summary>
        public static BigInteger operator %(BigInteger a, BigInteger b)
        {
            BigInteger res;
            if (Abs(a) < Abs(b))
            {
                res = new BigInteger(a);
                return res;
            }

            res = a - ((a / b) * b);
            return res;
        }

        /// <summary>
        /// Modulo operation of one BigInteger a and a base-10 int n, n != 0.
        /// </summary>
        public static BigInteger operator %(BigInteger a, int n)
        {
            BigInteger b = new BigInteger(a.NumberBase, a.MaxSize, n);
            BigInteger res = a % b;
            return res;
        }

        /// <summary>
        /// Computes the absolute value of a BigInteger.
        /// </summary>
        private static BigInteger Abs(BigInteger n)
        {
            BigInteger res = new BigInteger(n);
            res.isNegative = false;
            return res;
        }

        /// <summary>
        /// Adds two BigNumbers a and b, where a >= b, a, b non-negative.
        /// </summary>
        private static BigInteger Add(BigInteger a, BigInteger b)
        {
            BigInteger res = new BigInteger(a);
            int trans = 0, temp;
            int i;

            for (i = 0; i < b.size; i++)
            {
                temp = res.digits[i] + b.digits[i] + trans;
                res.digits[i] = temp % res.numberBase;
                trans = temp / res.numberBase;
            }

            for (i = b.size; ((i < a.size) && (trans > 0)); i++)
            {
                temp = res.digits[i] + trans;
                res.digits[i] = temp % res.numberBase;
                trans = temp / res.numberBase;
            }

            if (trans > 0)
            {
                res.digits[res.size] = trans % res.numberBase;
                res.size++;
                trans /= res.numberBase;
            }

            return res;
        }

        /// <summary>
        /// Subtracts the BigInteger b from the BigInteger a, where a >= b, a, b non-negative.
        /// </summary>
        private static BigInteger Subtract(BigInteger a, BigInteger b)
        {
            BigInteger res = new BigInteger(a);
            int i, size, temp, trans = 0;
            bool reducible = true;

            for (i = 0; i < b.size; i++)
            {
                temp = res.digits[i] - b.digits[i] - trans;
                if (temp < 0)
                {
                    trans = 1;
                    temp += res.numberBase;
                }
                else trans = 0;
                res.digits[i] = temp;
            }

            for (i = b.size; ((i < a.size) && (trans > 0)); i++)
            {
                temp = res.digits[i] - trans;
                if (temp < 0)
                {
                    trans = 1;
                    temp += res.numberBase;
                }
                else trans = 0;
                res.digits[i] = temp;
            }

            size = res.size;
            for (i = size - 1; ((i > 0) && (reducible)); i--)
                if (res.digits[i] == 0) res.size--;
                else reducible = false;

            return res;
        }

        /// <summary>
        /// Multiplies two BigIntegers.
        /// </summary>
        private static BigInteger Multiply(BigInteger a, BigInteger b)
        {
            int i, j, tempSize;
            long temp, trans = 0;
            long[] tempDigits = new long[a.maxSize];
            long numberBase = (long)a.numberBase;

            for (i = 0; i < a.maxSize; i++)
                tempDigits[i] = 0;

            for (i = 0; i < a.size; i++)
                if (a.digits[i] != 0)
                    for (j = 0; j < b.size; j++)
                        if (b.digits[j] != 0)
                            tempDigits[i + j] += ((long)a.digits[i]) * ((long)b.digits[j]);

            tempSize = a.size + b.size - 1;

            for (i = 0; i < tempSize; i++)
            {
                temp = tempDigits[i] + trans;
                tempDigits[i] = temp % numberBase;
                trans = temp / numberBase;
            }
            while (trans > 0)
            {
                tempDigits[tempSize] = trans % numberBase;
                tempSize++;
                trans /= numberBase;
            }

            BigInteger res = new BigInteger(a.NumberBase, a.MaxSize);

            res.size = tempSize;
            for (i = 0; i < res.size; i++)
                res.digits[i] = (int)tempDigits[i];

            return res;
        }

        /// <summary>
        /// Divides a BigInteger by a one-digit int.
        /// </summary>
        private static BigInteger DivideByOneDigitNumber(BigInteger a, int b)
        {
            BigInteger res = new BigInteger(a.NumberBase, a.MaxSize);
            int temp, i = a.size - 1;
            res.size = a.size;
            temp = a.digits[i];

            while (i >= 0)
            {
                res.digits[i] = temp / b;
                temp %= b;
                i--;
                if (i >= 0)
                    temp = temp * a.numberBase + a.digits[i];
            }
            if ((res.digits[res.size - 1] == 0) && (res.size != 1))
                res.size--;
            return res;
        }

        /// <summary>
        /// Divides a BigInteger by another BigInteger.
        /// </summary>
        private static BigInteger DivideByBigNumber(BigInteger a, BigInteger b)
        {
            int k, f, qt, n = a.size, m = b.size;
            BigInteger d, dq, q, r;

            f = a.numberBase / (b.digits[m - 1] + 1);
            q = new BigInteger(a.numberBase, a.maxSize);
            r = a * f;
            d = b * f;
            for (k = n - m; k >= 0; k--)
            {
                qt = Trial(r, d, k, m);
                dq = d * qt;
                if (Smaller(r, dq, k, m))
                {
                    qt--;
                    dq = d * qt;
                }
                q.digits[k] = qt;
                Difference(r, dq, k, m);
            }
            q.size = n - m + 1;
            if ((q.size != 1) && (q.digits[q.size - 1] == 0))
                q.size--;
            return q;
        }

        /// <summary>
        /// DivideByBigNumber auxilary method. 
        /// </summary>
        private static bool Smaller(BigInteger r, BigInteger dq, int k, int m)
        {
            int i = m, j = 0;
            while (i != j)
                if (r.digits[i + k] != dq.digits[i])
                    j = i;
                else i--;
            if (r.digits[i + k] < dq.digits[i])
                return true;
            else
                return false;
        }

        /// <summary>
        /// DivideByBigNumber auxilary method.
        /// </summary>
        private static void Difference(BigInteger r, BigInteger dq, int k, int m)
        {
            int borrow = 0, diff, i;
            for (i = 0; i <= m; i++)
            {
                diff = r.digits[i + k] - dq.digits[i] - borrow + r.numberBase;
                r.digits[i + k] = diff % r.numberBase;
                borrow = 1 - diff / r.numberBase;
            }
        }

        /// <summary>
        /// DivideByBigNumber auxilary method.
        /// </summary>
        private static int Trial(BigInteger r, BigInteger d, int k, int m)
        {
            long d2, km = k + m, r3, res;
            r3 = ((long)r.digits[km] * (long)r.numberBase + (long)r.digits[km - 1]) * (long)r.numberBase + (long)r.digits[km - 2];
            d2 = (long)d.digits[m - 1] * (long)r.numberBase + (long)d.digits[m - 2];
            res = r3 / d2;
            if (res < r.numberBase - 1)
                return (int)res;
            else
                return r.numberBase - 1;
        }

        /// <summary>
        /// Returns the power of a base to an exponent (the exponent must be non-negative).
        /// Uses fast exponentiation (right to left binary exponentiation).
        /// </summary>
        public static BigInteger Power(BigInteger number, int exponent)
        {
            BigInteger res = new BigInteger(number.numberBase, number.maxSize, 1);
            if (exponent == 0)
                return res;

            BigInteger factor = new BigInteger(number);
            while (exponent > 0)
            {
                if (exponent % 2 == 1)
                    res *= factor;
                exponent /= 2;
                if (exponent > 0)
                    factor *= factor;
            }

            return res;
        }

        /// <summary>
        /// Serializer writing a BigInteger to a binary file stream.
        /// </summary>
        public void Serialize(BinaryWriter bw)
        {
            bw.Write((short)size);
            for (int i = 0; i < size; i++)
                bw.Write((ushort)digits[i]);
        }

        /// <summary>
        /// String representation of the current BigInteger, converted to its base-10 representation.
        /// </summary>
        public override string ToString()
        {
            string output = "";
            if (isNegative == true)
                output += "-";

            if (numberBase == 10)
                for (int i = size - 1; i >= 0; i--)
                    output += digits[i].ToString();

            else
            {
                int i, newSize = maxSize;

                if (numberBase > 10)
                    newSize *= 6;

                BigInteger res = new BigInteger(10, newSize);
                BigInteger currentPower = Power(new BigInteger(10, newSize, numberBase), 0);

                for (i = 0; i < size; i++)
                {
                    res += digits[i] * currentPower;
                    currentPower *= numberBase;
                }

                for (i = (res.size) - 1; i >= 0; i--)
                    output += res.digits[i].ToString();
            }

            return output;
        }

        /// <summary>
        /// Adds a number of salt digits to a BigInteger.
        /// </summary>
        public static BigInteger AddSalt(BigInteger x, BigInteger n, int saltDigits)
        {
            Random random = new Random();
            int i;

            BigInteger res = new BigInteger(x);
            res.size += saltDigits;

            for (i = (res.size - 1); i >= saltDigits; i--)
                res.digits[i] = res.digits[i - saltDigits];

            for (i = saltDigits - 1; i >= 0; i--)
                res.digits[i] = n.digits[i] ^ random.Next(n.numberBase - 1);

            return res;
        }

        /// <summary>
        /// Removes the salt from a BigInteger.
        /// </summary>
        public static BigInteger RemoveSalt(BigInteger x, int saltDigits)
        {
            BigInteger res = new BigInteger(x);

            res.size -= saltDigits;
            for (int i = 0; i < res.size; i++)
                res.digits[i] = res.digits[i + saltDigits];

            return res;
        }

        /// <summary>
        /// Replicates the last digits of a BigInteger.
        /// </summary>
        public static BigInteger Replicate(BigInteger n, int shiftDigits)
        {
            BigInteger res = new BigInteger(n);
            res.size += shiftDigits;

            for (int i = (res.size - 1); i >= shiftDigits; i--)
                res.digits[i] = res.digits[i - shiftDigits];

            return res;
        }

        /// <summary>
        /// Checks the replication of a BigInteger.
        /// </summary>
        public static bool CheckReplication(BigInteger n, int shiftDigits)
        {
            for (int i = 0; i < shiftDigits; i++)
                if (n.digits[i] != n.digits[i + shiftDigits])
                    return false;

            return true;
        }

        /// <summary>
        /// Removes the replication of a BigInteger.
        /// </summary>
        public static BigInteger Dereplicate(BigInteger n, int shiftDigits)
        {
            BigInteger res = new BigInteger(n);

            res.size -= shiftDigits;
            for (int i = 0; i < res.size; i++)
                res.digits[i] = res.digits[i + shiftDigits];

            return res;
        }

        /// <summary>
        /// Euclidean algorithm for computing the gcd of two natural numbers.
        /// </summary>
        public static BigInteger Gcd(BigInteger a, BigInteger b)
        {
            BigInteger r = null;
            BigInteger zero = new BigInteger(a.NumberBase, a.MaxSize);

            while (b > zero)
            {
                r = a % b;
                a = b;
                b = r;
            }

            return a;
        }

        /// <summary>
        /// Extended Euclidian algorithm, returning the gcd of two BigIntegers.
        /// </summary>
        public static BigInteger ExtendedEuclid(BigInteger a, BigInteger b,
                                                ref BigInteger u, ref BigInteger v)
        {
            BigInteger zero = new BigInteger(a.NumberBase, a.MaxSize);
            BigInteger u1 = new BigInteger(zero);
            BigInteger u2 = new BigInteger(a.NumberBase, a.MaxSize, 1);
            BigInteger v1 = new BigInteger(a.NumberBase, a.MaxSize, 1);
            BigInteger v2 = new BigInteger(zero);
            BigInteger q, r = null;

            while (b > zero)
            {
                q = a / b;
                r = a - q * b;
                u = u2 - q * u1;
                v = v2 - q * v1;

                a = new BigInteger(b);
                b = new BigInteger(r);
                u2 = new BigInteger(u1);
                u1 = new BigInteger(u);
                v2 = new BigInteger(v1);
                v1 = new BigInteger(v);
                u = new BigInteger(u2);
                v = new BigInteger(v2);
            }

            return a;
        }

        /// <summary>
        /// Computes the modular inverse of a given BigInteger
        /// </summary>
        public static BigInteger ModularInverse(BigInteger a, BigInteger n)
        {
            BigInteger zero = new BigInteger(n.numberBase, n.maxSize);
            BigInteger u = null, v = null;

            if (a >= n)
                a = a % n;

            ExtendedEuclid(n, a, ref u, ref v);

            if (v < zero)
                v += n;

            return v;
        }

        /// <summary>
        /// Returns the power of a base to an exponent (modulo n) (the exponent must be non-negative).
        /// Uses fast exponentiation (right to left binary exponentiation) and modulo optimizations.
        /// </summary>
        public static BigInteger PowerRepeatedSquaring(BigInteger number, BigInteger exponent, BigInteger n)
        {
            BigInteger zero = new BigInteger(number.NumberBase, number.MaxSize);
            BigInteger one = new BigInteger(number.NumberBase, number.MaxSize, 1);
            BigInteger two = new BigInteger(number.NumberBase, number.MaxSize, 2);
            BigInteger res = new BigInteger(number.NumberBase, number.MaxSize, 1);
            BigInteger factor = new BigInteger(number);

            if (exponent == zero)
                return res;

            while (exponent > zero)
            {
                if (exponent % two == one)
                    res = (res * factor) % n;
                exponent /= two;
                factor = (factor * factor) % n;
            }

            return res;
        }
    }
}
