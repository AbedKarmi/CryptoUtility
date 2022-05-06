//Copyright 2007 - 2011 John Church
//
//This file is part of CpConverter.
//CpConverter is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//CpConverter is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with CpConverter.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CryptoUtility;

/// <summary>
///     Class to do the conversion
/// </summary>
public class Converter
{
    #region "Enums"

    /// <summary>
    ///     Special cases
    /// </summary>
    public enum SpecialTypes
    {
        None = 0,
        UnicodeAsDecimal = 1
    }

    #endregion


    private const int UTF8CP = 65001;
    private const int UNICODECP = 1200;
    private const int WIN1256CP = 1256;
    private const int CustomCharset = 1;

    private static int destIndex;

    #region "Private Methods"

    /// <summary>
    ///     Do some special processing for special number encoded data
    /// </summary>
    /// <param name="sData">original filedata</param>
    /// <returns></returns>
    private static string ConvertDecimals(string sData)
    {
        //search for &# + at least 2 digits + ;
        //27/03/2007 Changed the regular expression to look for 2 or more digits
        //previously it was only looking for 4 digit groups
        Regex re = new("&#(\\d){2,};");
        Match match;
        StringBuilder sReturnData = new();

        while (sData.Length > 0)
        {
            match = re.Match(sData);
            if (match.Length == 0)
            {
                //no match so just add the rest of the data
                sReturnData.Append(sData);
                sData = "";
            }
            else
            {
                //we got a match so put the first bit into the return string
                sReturnData.Append(sData.AsSpan(0, match.Index));
                //get rid of the bit we already searc5hed
                sData = sData[(match.Index + match.Length)..];
                //get the char val
                //27/03/2007 length parameter in substring is not fixed any more
                //but it will always be the length of the match - 3 (ie &#;)
                int iVal = int.Parse(match.Value.Substring(2, match.Length - 3));
                //output it
                sReturnData.Append(char.ConvertFromUtf32(iVal));
            }
        }

        return sReturnData.ToString();
    }

    #endregion

    #region "Public Methods"

    private enum ArabicEncodings : ushort
    {
        WIN1256 = 0,
        CustomCharset = 1
    }

    private static readonly string[] ACCO =
    {
        "C1C2C3C4C5C6C7C8C9CACBCCCDCECFD0D1D2D3D4D5D6D8D9DADBDDDEDFE1E3E4E5E6ECEDF0F1F2F3F5F6F8FA",
        "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"
    };

    private static readonly byte[,] diacriticsUTF8 =
    {
        { 0xD9, 0x8B }, { 0xD9, 0x8C }, { 0xD9, 0x8D }, { 0xD9, 0x8E }, { 0xD9, 0x8F }, { 0xD9, 0x90 }, { 0xD9, 0x91 },
        { 0xD9, 0x92 }
    };

    private static readonly byte[,] diacriticsUnicodeBE =
    {
        { 0x06, 0x4B }, { 0x06, 0x4C }, { 0x06, 0x4D }, { 0x06, 0x4E }, { 0x06, 0x4F }, { 0x06, 0x50 }, { 0x06, 0x51 },
        { 0x06, 0x52 }
    };

    private static readonly byte[,] diacriticsUnicodeLE =
    {
        { 0x4B, 0x06 }, { 0x4C, 0x06 }, { 0x4D, 0x06 }, { 0x4E, 0x06 }, { 0x4F, 0x06 }, { 0x50, 0x06 }, { 0x51, 0x06 },
        { 0x52, 0x06 }
    };

    private static readonly byte[] alefBE = { 0x06, 0x70 };
    private static readonly byte[] alefLE = { 0x70, 0x06 };
    private static readonly byte[] alefUTF8 = { 0xD9, 0xB0 };

    private static byte[] fromACCO, toACCO, baseFromACCO, baseToACCO;
    private static readonly byte[] discardList = { 0x3F, 0x20, 0x0D, 0x0A };

    private const byte eolChar = 0x0A,
        eolCharReplace = 0x0;

    public static byte SpaceChar { get; set; }
    public static bool KillSpace { get; set; }

    public static bool IsACCO(int cp)
    {
        return cp > 0xFFFF;
    }

    public static byte[] Inverse(int destIndex)
    {
        var bin = new byte[256];

        if (destIndex > 0)
            for (var i = 0; i < fromACCO.Length; i++)
                bin[fromACCO[i]] = (byte)i;
        else
            for (var i = 0; i < fromACCO.Length; i++)
                bin[fromACCO[i]] = (byte)(i + 1);

        return bin;
    }

    public static byte[] BaseInverse()
    {
        var bin = new byte[256];

        for (var i = 0; i < baseFromACCO.Length; i++)
            bin[baseFromACCO[i]] = (byte)(i + 1);

        return bin;
    }

    public static int GetEncoding(string encoding)
    {
        var s = encoding;
        return int.Parse(s[(s.LastIndexOf(" ") + 1)..]) & 0xFFFF;
    }

    public static int GetEncodingEx(string encoding)
    {
        var s = encoding;
        return int.Parse(s[(s.LastIndexOf(" ") + 1)..]);
    }

    public static string GetCharsetFile(string encoding)
    {
        int p0, p1;
        p0 = encoding.LastIndexOf("[");
        p1 = encoding.LastIndexOf("]");
        if (p0 > 0 && p1 > p0) return encoding.Substring(p0 + 1, p1 - p0 - 1);
        return "";
    }

    private static void InitACCO(string SCP)
    {
        int cp;
        var CP = GetEncodingEx(SCP);
        var charset = GetCharsetFile(SCP);
        destIndex = 0;

        if (IsACCO(CP)) cp = CP;
        else cp = 0;
        if (cp > 0)
        {
            destIndex = 1;
            try
            {
                var startupPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var s = File.ReadAllText(startupPath + "\\" + charset + ".Charset").Split(ISettings.NewLineSep); 
                if (s[1].Length == 88) ACCO[CustomCharset] = s[1];
            }
            catch (Exception)
            {
            }

            ;
        }

        fromACCO = MyClass.HexStringToByteArray(ACCO[destIndex]);
        toACCO = Inverse(destIndex);
        baseFromACCO = MyClass.HexStringToByteArray(ACCO[0]);
        baseToACCO = BaseInverse();
    }

    public static string FromACCO(byte[] bin)
    {
        var data = new byte[bin.Length];
        for (var i = 0; i < bin.Length; i++)
            data[i] += destIndex > 0 ? baseFromACCO[toACCO[bin[i]]] : baseFromACCO[bin[i] - 1];
        return MyClass.GetString(data);
    }

    public static string FromACCO(string bin)
    {
        return FromACCO(MyClass.GetBytes(bin));
    }

    public static byte[] ToACCO(byte[] data)
    {
        var j = 0;
        byte c;
        var bin = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            try
            {
                c = destIndex > 0 ? fromACCO[baseToACCO[data[i]] - 1] : baseToACCO[data[i]];
                if (c > 0) bin[j++] = c;
            }
            catch (Exception)
            {
                if (data[i].In<byte>(10, 13, 32)) bin[j++] = SpaceChar;
            } // data[i]; }
        if (bin[j - 1] == SpaceChar) j--;
        Array.Resize(ref bin, j);
        return bin;
    }

    public static byte[] ToACCO(string data)
    {
        return ToACCO(MyClass.GetBytes(data));
    }

    public static string ReadAllText(string file, string encoding)
    {
        var cp = GetEncodingEx(encoding);
        if (!IsACCO(cp))
        {
            return File.ReadAllText(file, Encoding.GetEncoding(cp));
        }

        var bin = File.ReadAllBytes(file);
        return FromACCO(bin);
    }

    public static byte[] FilterBuffer(byte[] data, bool discardChars = false, bool zStrings = false)
    {
        var buffer = new byte[data.Length];
        var j = 0;

        if (!discardChars && !zStrings) return data;
        for (var i = 0; i < data.Length; i++)
        {
            if (zStrings && data[i] == eolChar)
                buffer[j++] = eolCharReplace;
            else if (!discardChars || !discardList.Contains(data[i])) buffer[j++] = data[i];
            Array.Resize(ref buffer, j);
        }

        return buffer;
    }

    public static string FilterData(string sData, int sourceCP, bool discardChars = false,
        bool discardDiacritics = false, bool zStrings = false)
    {
        var singleByte = Encoding.GetEncoding(sourceCP & 0xFFFF).IsSingleByte;
        var BE = sData[0] == (char)0xFE;
        if (!IsACCO(sourceCP))
        {
            if (zStrings)
                if (singleByte)
                    sData = sData.Replace("\r\n", "\0");
                else if (sourceCP == UTF8CP)
                    sData = sData.Replace("\r\n", Encoding.UTF8.GetString(new byte[] { 0x0 }));
                else if (sourceCP == UNICODECP)
                    sData = sData.Replace("\r\n", Encoding.Unicode.GetString(new byte[] { 0x0 }));
            if (discardChars)
            {
                if (KillSpace)
                {
                    sData = sData.Replace(" ", "");
                    sData = sData.Replace("\r\n", "");
                }
                else
                {
                    sData = sData.Replace("\r\n", " ");
                }

                if (singleByte)
                    sData = sData.Replace("\x3F", "");
                else if (sourceCP == UTF8CP)
                    sData = sData.Replace(Encoding.UTF8.GetString(alefUTF8), "");
                else if (sourceCP == UNICODECP)
                    if (BE) sData = sData.Replace(Encoding.Unicode.GetString(alefBE), "");
                    else sData = sData.Replace(Encoding.Unicode.GetString(alefLE), "");
            }

            if (discardDiacritics)
            {
                if (singleByte)
                    for (byte i = 0xF0; i <= 0xFA; i++)
                        sData = sData.Replace(Encoding.ASCII.GetString(new[] { i }), "");
                else if (sourceCP == UTF8CP)
                    for (byte i = 0; i < diacriticsUTF8.Length / 2; i++)
                        sData = sData.Replace(
                            Encoding.UTF8.GetString(new[] { diacriticsUTF8[i, 0], diacriticsUTF8[i, 1] }), "");
                else if (sourceCP == UNICODECP)
                    if (BE)
                        for (byte i = 0; i < diacriticsUnicodeBE.Length / 2; i++)
                            sData = sData.Replace(
                                Encoding.Unicode.GetString(new[]
                                    { diacriticsUnicodeBE[i, 0], diacriticsUnicodeBE[i, 1] }), "");
                    else
                        for (byte i = 0; i < diacriticsUnicodeLE.Length / 2; i++)
                            sData = sData.Replace(
                                Encoding.Unicode.GetString(new[]
                                    { diacriticsUnicodeLE[i, 0], diacriticsUnicodeLE[i, 1] }), "");
            }
        }

        return sData;
    }

    private static Encoding GetEncoding(int cp)
    {
        return Encoding.GetEncoding(cp & 0xFFFF);
    }

    /// <summary>
    ///     Convert a file from source codepage to destination codepage
    /// </summary>
    /// <param name="filename">filename</param>
    /// <param name="destDir">output directory</param>
    /// <param name="sourceCP">source codepage</param>
    /// <param name="destCP">destination codepage</param>
    /// <param name="specialType">SpecialTypes</param>
    /// <param name="outputMetaTag">True=output meta tag</param>
    /// <returns></returns>
    public static string ConvertFile(string filename, string destDir, string sourceSCP, string destSCP,
        SpecialTypes specialType, bool outputMetaTag,
        bool discardChars = false, bool discardDiacritics = false, bool zStrings = false)
    {
        //source file data
        var fileData = "";
        //get the encodings
        var sourceCP = GetEncodingEx(sourceSCP);
        var destCP = GetEncodingEx(destSCP);
        FileStream fw = null;

        //get the output filename
        //john church 05/10/2008 use directory separator char instead of backslash for linux support
        var outputFilename = filename[(filename.LastIndexOf(Path.DirectorySeparatorChar) + 1)..];
        //check if the file exists
        if (!File.Exists(filename)) throw new FileNotFoundException(filename);

        try
        {
            InitACCO(sourceSCP);

            //check or create the output directory
            if (!Directory.Exists(destDir)) Directory.CreateDirectory(destDir);
            //check if we need to output meta tags

            //check we've got a backslash at the end of the pathname
            //john church 05/10/2008 use directory separator char instead of backslash for linux support
            if (destDir.EndsWith(Path.DirectorySeparatorChar.ToString()) == false)
                destDir += Path.DirectorySeparatorChar;

            byte[] bDest;
            //write out the file
            fw = new FileStream(destDir + outputFilename, FileMode.Create);

            //read in the source file
            //john church 09/02/2011 add source encoding parameter to ReadAllText call
            if (!IsACCO(sourceCP))
            {
                var sData = ReadAllText(filename, sourceSCP);

                var sourceEnc = GetEncoding(sourceCP);

                sData = FilterData(sData, sourceCP, discardChars, discardDiacritics, zStrings);
                if (outputMetaTag && !IsACCO(destCP))
                    fileData = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=" +
                               (!IsACCO(destCP) ? GetEncoding(destCP).WebName : "ACCO") + "\" />";
                fileData += sData;
                //check for any special cases
                switch (specialType)
                {
                    case SpecialTypes.UnicodeAsDecimal:
                        fileData = ConvertDecimals(fileData);
                        break;
                    case SpecialTypes.None:
                        //do nothing
                        break;
                }

                //put the data into an array
                var bSource = sourceEnc.GetBytes(fileData);
                if (!IsACCO(destCP))
                {
                    var destEnc = GetEncoding(destCP);
                    bDest = Encoding.Convert(sourceEnc, destEnc, bSource);
                    //02/05/2007 need to write first to bytes when saving as unicode
                    if (destEnc.CodePage == UNICODECP)
                    {
                        fw.WriteByte(0xFF);
                        fw.WriteByte(0xFE);
                    }
                }
                else
                {
                    InitACCO(destSCP);
                    bDest = ToACCO(Encoding.Convert(sourceEnc, Encoding.GetEncoding(WIN1256CP), bSource));
                }
            }
            else
            {
                var bSource = MyClass.GetBytes(FromACCO(File.ReadAllBytes(filename)));
                if (!IsACCO(destCP))
                {
                    var destEnc = GetEncoding(destCP);
                    bDest = Encoding.Convert(Encoding.GetEncoding(WIN1256CP), destEnc, bSource);
                }
                else
                {
                    InitACCO(destSCP);
                    bDest = ToACCO(bSource);
                }
            }
            //do the conversion

            fw.Write(bDest, 0, bDest.Length);
            fw.Flush();

            return destDir + outputFilename;
        }
        catch 
        {
            //just throw the exception back up
            throw;
        }
        finally
        {
            //clean up the stream
            if (fw != null) fw.Close();
            fw.Dispose();
        }
    }

    public static byte[] ConvertText(string data, string sourceSCP, string destSCP, SpecialTypes specialType,
        bool outputMetaTag,
        bool discardChars = false, bool discardDiacritics = false, bool zStrings = false)
    {
        //get the encodings
        var sourceCP = GetEncodingEx(sourceSCP);
        var destCP = GetEncodingEx(destSCP);

        MemoryStream ms = new();
        BinaryWriter bw = new(ms);
        StreamReader sr = new(ms);

        try
        {
            InitACCO(sourceSCP);

            byte[] bDest;
            if (!IsACCO(sourceCP))
            {
                var sourceEnc = GetEncoding(sourceCP);

                data = FilterData(data, sourceCP, discardChars, discardDiacritics, zStrings);

                //check if we need to output meta tags
                if (outputMetaTag && !IsACCO(destCP))
                    data = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=" +
                           GetEncoding(destCP).WebName + "\" />" + data;

                //check for any special cases
                switch (specialType)
                {
                    case SpecialTypes.UnicodeAsDecimal:
                        data = ConvertDecimals(data);
                        break;
                    case SpecialTypes.None:
                        //do nothing
                        break;
                }

                //put the data into an array
                var bSource = sourceEnc.GetBytes(data);
                //File.WriteAllBytes("f:\\downloads.x.txt", bSource);

                if (!IsACCO(destCP))
                {
                    var destEnc = GetEncoding(destCP);
                    bDest = Encoding.Convert(sourceEnc, destEnc, bSource);
                    //02/05/2007 need to write first to bytes when saving as unicode
                    if (destEnc.CodePage == UNICODECP)
                    {
                        bw.Write(0xFF);
                        bw.Write(0xFE);
                    }
                }
                else
                {
                    InitACCO(destSCP);
                    bDest = ToACCO(Encoding.Convert(sourceEnc, Encoding.GetEncoding(WIN1256CP), bSource));
                }
            }
            else
            {
                var bSource = MyClass.GetBytes(FromACCO(data));
                if (!IsACCO(destCP))
                {
                    var destEnc = GetEncoding(destCP);
                    bDest = Encoding.Convert(Encoding.GetEncoding(WIN1256CP), destEnc, bSource);
                }
                else
                {
                    InitACCO(destSCP);
                    bDest = ToACCO(bSource);
                }
            }

            bw.Write(bDest, 0, bDest.Length);
            bw.Flush();

            return ms.ToArray();
        }
        catch 
        {
            //just throw the exception back up
            throw ;
        }
        finally
        {
            //clean up the stream
            ms.Dispose();
            bw.Dispose();
            sr.Dispose();
        }
    }

    #endregion
}