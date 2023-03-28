using System;
using System.IO;
using System.Reflection;
using System.Text;
using Be.Windows.Forms;


public abstract class BInit<T>
{
#pragma warning disable IDE0060 // Remove unused parameter
    public BInit(T parameters)
#pragma warning restore IDE0060 // Remove unused parameter
    {
    }
}

internal class ByteCharProvider : BInit<string>, IByteCharConverter
{
    private readonly byte[] charset;
    private readonly byte[] baseCharset;
    private readonly byte[] uBaseCharset;
    private readonly Encoding curEnc;
    private readonly bool custom;
    private readonly int enc;
    private readonly byte[] invBaseCharset = new byte[256];
    private readonly byte[] invCharset = new byte[256];

    private readonly string U1200 =
        "2122232425262728292A2B2C2D2E2F303132333435363738393A4142434445464748494A4B4C4D4E4F505152";

    private readonly string W1256 =
        "C1C2C3C4C5C6C7C8C9CACBCCCDCECFD0D1D2D3D4D5D6D8D9DADBDDDEDFE1E3E4E5E6ECEDF0F1F2F3F5F6F8FA";

    public ByteCharProvider(string _enc) : base(_enc)
    {
        enc = int.Parse(_enc[(_enc.LastIndexOf(" ") + 1)..]);
        if (enc > 0xFFFF)
        {
            custom = true;
            enc &= 0xFFFF;
        }

        if (!custom)
        {
            curEnc = Encoding.GetEncoding(enc);
        }
        else
        {
            charset = HexStringToBinary(File.ReadAllText(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + GetCharsetFile(_enc) +
                ".Charset"));
            baseCharset = HexStringToBinary(W1256);
            uBaseCharset = HexStringToBinary(U1200);
            for (var i = 0; i < charset.Length; i++) invCharset[charset[i]] = (byte)i;
            for (var i = 0; i < baseCharset.Length; i++) invBaseCharset[uBaseCharset[i]] = (byte)i;
            curEnc = Encoding.GetEncoding(1256);
        }
    }

    /// <summary>
    ///     Returns the EBCDIC character corresponding to the byte passed across.
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public virtual char ToChar(byte b)
    {
        if (custom) b = baseCharset[invCharset[b]];
        var encoded = curEnc.GetString(new[] { b });
        return encoded.Length > 0 ? encoded[0] : '.';
    }

    /// <summary>
    ///     Returns the byte corresponding to the EBCDIC character passed across.
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public virtual byte ToByte(char c)
    {
        if (custom) return charset[invBaseCharset[(byte)c]];

        var decoded = curEnc.GetBytes(new[] { c });
        return decoded.Length > 0 ? decoded[0] : (byte)0;
    }

    private static byte[] HexStringToBinary(string s)
    {
        if (s.Length % 2 != 0) s = "0" + s;
        var binary = new byte[s.Length / 2];
        for (var i = 0; i < s.Length; i += 2) binary[i / 2] = Convert.ToByte(string.Concat("0x", s.AsSpan(i, 2)), 16);

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
    ///     Returns a description of the byte char provider.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return "Encoding (Code Page " + enc + " )";
    }
}