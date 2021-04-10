using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Be.Windows.Forms;
using System.IO;

namespace CryptoUtility
{
    public abstract class BInit<T>
    {
        public BInit(T parameters)
        {

        }
    }
    class ByteCharProvider : BInit<string>, IByteCharConverter
    {
        private Encoding curEnc;
        private int enc;
        private bool custom = false;
        private byte[] charset, baseCharset,uBaseCharset;
        byte[] invCharset = new byte[256];
        byte[] invBaseCharset = new byte[256];
        private string W1256 = "C1C2C3C4C5C6C7C8C9CACBCCCDCECFD0D1D2D3D4D5D6D8D9DADBDDDEDFE1E3E4E5E6ECEDF0F1F2F3F5F6F8FA";
        private string U1200 = "2122232425262728292A2B2C2D2E2F303132333435363738393A4142434445464748494A4B4C4D4E4F505152";
        public ByteCharProvider(string _enc) : base(_enc)
        {
            enc= int.Parse(_enc.Substring(_enc.LastIndexOf(" ") + 1));
            if (enc>0xFFFF) { custom = true;enc &= 0xFFFF; }
            if (!custom)
                curEnc = Encoding.GetEncoding(enc);
            else
            {

                charset = HexStringToBinary(File.ReadAllText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)+"\\"+ GetCharsetFile(_enc) + ".Charset"));
                baseCharset= HexStringToBinary(W1256);
                uBaseCharset = HexStringToBinary(U1200);
                for (int i = 0; i < charset.Length; i++) invCharset[charset[i]] = (byte)i;
                for (int i = 0; i < baseCharset.Length; i++) invBaseCharset[uBaseCharset[i]] = (byte)i;
                curEnc = Encoding.GetEncoding(1256);
            }
        }

        private static byte[] HexStringToBinary(String s)
        {
            if ((s.Length % 2) != 0) s = "0" + s;
            byte[] binary = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
            {
                binary[i / 2] = Convert.ToByte("0x" + s.Substring(i, 2), 16);
            }

            return binary;
        }
        private static string GetCharsetFile(string encoding)
        {
            int p0, p1;
            p0 = encoding.LastIndexOf("[");
            p1 = encoding.LastIndexOf("]");
            if (p0 > 0 && p1 > p0) return encoding.Substring(p0 + 1, p1 - p0 - 1);
            return "";
        }

        /// <summary>
        /// Returns the EBCDIC character corresponding to the byte passed across.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public virtual char ToChar(byte b)
        {
            if (custom) b = baseCharset[invCharset[b]];
            string encoded = curEnc.GetString(new byte[] { b });
            return encoded.Length > 0 ? encoded[0] : '.';
        }

        /// <summary>
        /// Returns the byte corresponding to the EBCDIC character passed across.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public virtual byte ToByte(char c)
        {
            if (custom) return charset[invBaseCharset[(byte)c]];

            byte[] decoded = curEnc.GetBytes(new char[] { c });
            return decoded.Length > 0 ? decoded[0] : (byte)0;
        }

        /// <summary>
        /// Returns a description of the byte char provider.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Encoding (Code Page "+enc.ToString()+" )";
        }
    }
}
