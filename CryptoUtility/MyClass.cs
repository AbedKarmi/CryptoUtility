using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace CryptoUtility;

public static class MyClass
{
    private struct BasicMultilingualPlane
    {
        public string ScriptGroup { get; set; }
        public string Name { get; set; }
        public int UniStart { get; set; }
        public int UniEnd { get; set; }
        public BasicMultilingualPlane(string scriptGroup, string name, int uniStart, int uniEnd)
        {
            ScriptGroup = scriptGroup;
            Name = name;
            UniStart = uniStart;
            UniEnd = uniEnd;
        }
    }

    private readonly static BasicMultilingualPlane[] BMP = new BasicMultilingualPlane[]
    {
        new BasicMultilingualPlane("","Basic Latin (Lower half of ISO/IEC 8859-1: ISO/IEC 646:1991-IRV aka ASCII)",0x0000,0x007F),
        new BasicMultilingualPlane("","Latin-1 Supplement (Upper half of ISO/IEC 8859-1)",0x0080,0x00FF),
        new BasicMultilingualPlane("","Latin Extended-A",0x0100,0x017F),
        new BasicMultilingualPlane("","Latin Extended-B",0x0180,0x024F),
        new BasicMultilingualPlane("","IPA Extensions",0x0250,0x02AF),
        new BasicMultilingualPlane("","Spacing Modifier Letters",0x02B0,0x02FF),
        new BasicMultilingualPlane("","Combining Diacritical Marks",0x0300,0x036F),
        new BasicMultilingualPlane("","Greek and Coptic",0x0370,0x03FF),
        new BasicMultilingualPlane("","Cyrillic",0x0400,0x04FF),
        new BasicMultilingualPlane("","Cyrillic Supplement",0x0500,0x052F),
        new BasicMultilingualPlane("","Armenian",0x0530,0x058F),
        new BasicMultilingualPlane("Aramaic Scripts","Hebrew",0x0590,0x05FF),
        new BasicMultilingualPlane("Aramaic Scripts","Arabic",0x0600,0x06FF),
        new BasicMultilingualPlane("Aramaic Scripts","Syriac",0x0700,0x074F),
        new BasicMultilingualPlane("Aramaic Scripts","Arabic Supplement",0x0750,0x077F),
        new BasicMultilingualPlane("Aramaic Scripts","Thaana",0x0780,0x07BF),
        new BasicMultilingualPlane("Aramaic Scripts","N'Ko",0x07C0,0x07FF),
        new BasicMultilingualPlane("Aramaic Scripts","Samaritan",0x0800,0x083F),
        new BasicMultilingualPlane("Aramaic Scripts","Mandaic",0x0840,0x085F),
        new BasicMultilingualPlane("Aramaic Scripts","Syriac Supplement",0x0860,0x086F),
        new BasicMultilingualPlane("Aramaic Scripts","Arabic Extended-B",0x0870,0x089F),
        new BasicMultilingualPlane("Aramaic Scripts","Arabic Extended-A",0x08A0,0x08FF),
        new BasicMultilingualPlane("Brahmic scripts","Devanagari",0x0900,0x097F),
        new BasicMultilingualPlane("Brahmic scripts","Bengali",0x0980,0x09FF),
        new BasicMultilingualPlane("Brahmic scripts","Gurmukhi",0x0A00,0x0A7F),
        new BasicMultilingualPlane("Brahmic scripts","Gujarati",0x0A80,0x0AFF),
        new BasicMultilingualPlane("Brahmic scripts","Oriya",0x0B00,0x0B7F),
        new BasicMultilingualPlane("Brahmic scripts","Tamil",0x0B80,0x0BFF),
        new BasicMultilingualPlane("Brahmic scripts","Telugu",0x0C00,0x0C7F),
        new BasicMultilingualPlane("Brahmic scripts","Kannada",0x0C80,0x0CFF),
        new BasicMultilingualPlane("Brahmic scripts","Malayalam",0x0D00,0x0D7F),
        new BasicMultilingualPlane("Brahmic scripts","Sinhala",0x0D80,0x0DFF),
        new BasicMultilingualPlane("Brahmic scripts","Thai",0x0E00,0x0E7F),
        new BasicMultilingualPlane("Brahmic scripts","Lao",0x0E80,0x0EFF),
        new BasicMultilingualPlane("Brahmic scripts","Tibetan",0x0F00,0x0FFF),
        new BasicMultilingualPlane("Brahmic scripts","Myanmar",0x1000,0x109F),
        new BasicMultilingualPlane("","Georgian",0x10A0,0x10FF),
        new BasicMultilingualPlane("","Hangul Jamo",0x1100,0x11FF),
        new BasicMultilingualPlane("","Ethiopic",0x1200,0x137F),
        new BasicMultilingualPlane("","Ethiopic Supplement",0x1380,0x139F),
        new BasicMultilingualPlane("","Cherokee",0x13A0,0x13FF),
        new BasicMultilingualPlane("","Unified Canadian Aboriginal Syllabics",0x1400,0x167F),
        new BasicMultilingualPlane("","Ogham",0x1680,0x169F),
        new BasicMultilingualPlane("","Runic",0x16A0,0x16FF),
        new BasicMultilingualPlane("Philippine scripts","Tagalog",0x1700,0x171F),
        new BasicMultilingualPlane("Philippine scripts","Hanunoo",0x1720,0x173F),
        new BasicMultilingualPlane("Philippine scripts","Buhid",0x1740,0x175F),
        new BasicMultilingualPlane("Philippine scripts","Tagbanwa",0x1760,0x177F),
        new BasicMultilingualPlane("","Khmer",0x1780,0x17FF),
        new BasicMultilingualPlane("","Mongolian",0x1800,0x18AF),
        new BasicMultilingualPlane("","Unified Canadian Aboriginal Syllabics Extended",0x18B0,0x18FF),
        new BasicMultilingualPlane("","Limbu",0x1900,0x194F),
        new BasicMultilingualPlane("Tai scripts","Tai Le",0x1950,0x197F),
        new BasicMultilingualPlane("Tai scripts","New Tai Lue",0x1980,0x19DF),
        new BasicMultilingualPlane("Tai scripts","Khmer Symbols",0x19E0,0x19FF),
        new BasicMultilingualPlane("Tai scripts","Buginese",0x1A00,0x1A1F),
        new BasicMultilingualPlane("Tai scripts","Tai Tham",0x1A20,0x1AAF),
        new BasicMultilingualPlane("","Combining Diacritical Marks Extended",0x1AB0,0x1AFF),
        new BasicMultilingualPlane("","Balinese",0x1B00,0x1B7F),
        new BasicMultilingualPlane("","Sundanese",0x1B80,0x1BBF),
        new BasicMultilingualPlane("","Batak",0x1BC0,0x1BFF),
        new BasicMultilingualPlane("","Lepcha",0x1C00,0x1C4F),
        new BasicMultilingualPlane("","Ol Chiki",0x1C50,0x1C7F),
        new BasicMultilingualPlane("","Cyrillic Extended-C",0x1C80,0x1C8F),
        new BasicMultilingualPlane("","Georgian Extended",0x1C90,0x1CBF),
        new BasicMultilingualPlane("","Sundanese Supplement",0x1CC0,0x1CCF),
        new BasicMultilingualPlane("","Vedic Extensions",0x1CD0,0x1CFF),
        new BasicMultilingualPlane("Latin supplements","Phonetic Extensions",0x1D00,0x1D7F),
        new BasicMultilingualPlane("Latin supplements","Phonetic Extensions Supplement",0x1D80,0x1DBF),
        new BasicMultilingualPlane("Latin supplements","Combining Diacritical Marks Supplement",0x1DC0,0x1DFF),
        new BasicMultilingualPlane("Latin supplements","Latin Extended Additional",0x1E00,0x1EFF),
        new BasicMultilingualPlane("","Greek Extended",0x1F00,0x1FFF),
        new BasicMultilingualPlane("Symbols","General Punctuation",0x2000,0x206F),
        new BasicMultilingualPlane("Symbols","Superscripts and Subscripts",0x2070,0x209F),
        new BasicMultilingualPlane("Symbols","Currency Symbols",0x20A0,0x20CF),
        new BasicMultilingualPlane("Symbols","Combining Diacritical Marks for Symbols",0x20D0,0x20FF),
        new BasicMultilingualPlane("Symbols","Letterlike Symbols",0x2100,0x214F),
        new BasicMultilingualPlane("Symbols","Number Forms",0x2150,0x218F),
        new BasicMultilingualPlane("Symbols","Arrows",0x2190,0x21FF),
        new BasicMultilingualPlane("Symbols","Mathematical Operators",0x2200,0x22FF),
        new BasicMultilingualPlane("Symbols","Miscellaneous Technical",0x2300,0x23FF),
        new BasicMultilingualPlane("Symbols","Control Pictures",0x2400,0x243F),
        new BasicMultilingualPlane("Symbols","Optical Character Recognition",0x2440,0x245F),
        new BasicMultilingualPlane("Symbols","Enclosed Alphanumerics",0x2460,0x24FF),
        new BasicMultilingualPlane("Symbols","Box Drawing",0x2500,0x257F),
        new BasicMultilingualPlane("Symbols","Block Elements",0x2580,0x259F),
        new BasicMultilingualPlane("Symbols","Geometric Shapes",0x25A0,0x25FF),
        new BasicMultilingualPlane("Symbols","Miscellaneous Symbols",0x2600,0x26FF),
        new BasicMultilingualPlane("Symbols","Dingbats",0x2700,0x27BF),
        new BasicMultilingualPlane("Symbols","Miscellaneous Mathematical Symbols-A",0x27C0,0x27EF),
        new BasicMultilingualPlane("Symbols","Supplemental Arrows-A",0x27F0,0x27FF),
        new BasicMultilingualPlane("Symbols","Braille Patterns",0x2800,0x28FF),
        new BasicMultilingualPlane("Symbols","Supplemental Arrows-B",0x2900,0x297F),
        new BasicMultilingualPlane("Symbols","Miscellaneous Mathematical Symbols-B",0x2980,0x29FF),
        new BasicMultilingualPlane("Symbols","Supplemental Mathematical Operators",0x2A00,0x2AFF),
        new BasicMultilingualPlane("Symbols","Miscellaneous Symbols and Arrows",0x2B00,0x2BFF),
        new BasicMultilingualPlane("","Glagolitic",0x2C00,0x2C5F),
        new BasicMultilingualPlane("","Latin Extended-C",0x2C60,0x2C7F),
        new BasicMultilingualPlane("","Coptic",0x2C80,0x2CFF),
        new BasicMultilingualPlane("","Georgian Supplement",0x2D00,0x2D2F),
        new BasicMultilingualPlane("","Tifinagh",0x2D30,0x2D7F),
        new BasicMultilingualPlane("","Ethiopic Extended",0x2D80,0x2DDF),
        new BasicMultilingualPlane("","Cyrillic Extended-A",0x2DE0,0x2DFF),
        new BasicMultilingualPlane("","Supplemental Punctuation",0x2E00,0x2E7F),
        new BasicMultilingualPlane("CJK scripts and symbols","CJK Radicals Supplement",0x2E80,0x2EFF),
        new BasicMultilingualPlane("CJK scripts and symbols","Kangxi Radicals",0x2F00,0x2FDF),
        new BasicMultilingualPlane("CJK scripts and symbols","Ideographic Description Characters",0x2FF0,0x2FFF),
        new BasicMultilingualPlane("CJK scripts and symbols","CJK Symbols and Punctuation",0x3000,0x303F),
        new BasicMultilingualPlane("CJK scripts and symbols","Hiragana",0x3040,0x309F),
        new BasicMultilingualPlane("CJK scripts and symbols","Katakana",0x30A0,0x30FF),
        new BasicMultilingualPlane("CJK scripts and symbols","Bopomofo",0x3100,0x312F),
        new BasicMultilingualPlane("CJK scripts and symbols","Hangul Compatibility Jamo",0x3130,0x318F),
        new BasicMultilingualPlane("CJK scripts and symbols","Kanbun",0x3190,0x319F),
        new BasicMultilingualPlane("CJK scripts and symbols","Bopomofo Extended",0x31A0,0x31BF),
        new BasicMultilingualPlane("CJK scripts and symbols","CJK Strokes",0x31C0,0x31EF),
        new BasicMultilingualPlane("CJK scripts and symbols","Katakana Phonetic Extensions",0x31F0,0x31FF),
        new BasicMultilingualPlane("CJK scripts and symbols","Enclosed CJK Letters and Months",0x3200,0x32FF),
        new BasicMultilingualPlane("CJK scripts and symbols","CJK Compatibility",0x3300,0x33FF),
        new BasicMultilingualPlane("CJK scripts and symbols","CJK Unified Ideographs Extension A",0x3400,0x4DBF),
        new BasicMultilingualPlane("CJK scripts and symbols","Yijing Hexagram Symbols",0x4DC0,0x4DFF),
        new BasicMultilingualPlane("CJK scripts and symbols","CJK Unified Ideographs",0x4E00,0x9FFF),
        new BasicMultilingualPlane("","Yi Syllables",0xA000,0xA48F),
        new BasicMultilingualPlane("","Yi Radicals",0xA490,0xA4CF),
        new BasicMultilingualPlane("","Lisu",0xA4D0,0xA4FF),
        new BasicMultilingualPlane("","Vai",0xA500,0xA63F),
        new BasicMultilingualPlane("","Cyrillic Extended-B",0xA640,0xA69F),
        new BasicMultilingualPlane("","Bamum",0xA6A0,0xA6FF),
        new BasicMultilingualPlane("","Modifier Tone Letters",0xA700,0xA71F),
        new BasicMultilingualPlane("","Latin Extended-D",0xA720,0xA7FF),
        new BasicMultilingualPlane("","Syloti Nagri",0xA800,0xA82F),
        new BasicMultilingualPlane("","Common Indic Number Forms",0xA830,0xA83F),
        new BasicMultilingualPlane("","Phags-pa",0xA840,0xA87F),
        new BasicMultilingualPlane("","Saurashtra",0xA880,0xA8DF),
        new BasicMultilingualPlane("","Devanagari Extended",0xA8E0,0xA8FF),
        new BasicMultilingualPlane("","Kayah Li",0xA900,0xA92F),
        new BasicMultilingualPlane("","Rejang",0xA930,0xA95F),
        new BasicMultilingualPlane("","Hangul Jamo Extended-A",0xA960,0xA97F),
        new BasicMultilingualPlane("","Javanese",0xA980,0xA9DF),
        new BasicMultilingualPlane("","Myanmar Extended-B",0xA9E0,0xA9FF),
        new BasicMultilingualPlane("","Cham",0xAA00,0xAA5F),
        new BasicMultilingualPlane("","Myanmar Extended-A",0xAA60,0xAA7F),
        new BasicMultilingualPlane("","Tai Viet",0xAA80,0xAADF),
        new BasicMultilingualPlane("","Meetei Mayek Extensions",0xAAE0,0xAAFF),
        new BasicMultilingualPlane("","Ethiopic Extended-A",0xAB00,0xAB2F),
        new BasicMultilingualPlane("","Latin Extended-E",0xAB30,0xAB6F),
        new BasicMultilingualPlane("","Cherokee Supplement",0xAB70,0xABBF),
        new BasicMultilingualPlane("","Meetei Mayek",0xABC0,0xABFF),
        new BasicMultilingualPlane("","Hangul Syllables",0xAC00,0xD7AF),
        new BasicMultilingualPlane("","Hangul Jamo Extended-B",0xD7B0,0xD7FF),
        new BasicMultilingualPlane("Surrogates","High Surrogates",0xD800,0xDB7F),
        new BasicMultilingualPlane("Surrogates","High Private Use Surrogates",0xDB80,0xDBFF),
        new BasicMultilingualPlane("Surrogates","Low Surrogates",0xDC00,0xDFFF),
        new BasicMultilingualPlane("","Private Use Area",0xE000,0xF8FF),
        new BasicMultilingualPlane("","CJK Compatibility Ideographs",0xF900,0xFAFF),
        new BasicMultilingualPlane("","Alphabetic Presentation Forms",0xFB00,0xFB4F),
        new BasicMultilingualPlane("","Arabic Presentation Forms-A",0xFB50,0xFDFF),
        new BasicMultilingualPlane("","Variation Selectors",0xFE00,0xFE0F),
        new BasicMultilingualPlane("","Vertical Forms",0xFE10,0xFE1F),
        new BasicMultilingualPlane("","Combining Half Marks",0xFE20,0xFE2F),
        new BasicMultilingualPlane("","CJK Compatibility Forms",0xFE30,0xFE4F),
        new BasicMultilingualPlane("","Small Form Variants",0xFE50,0xFE6F),
        new BasicMultilingualPlane("","Arabic Presentation Forms-B",0xFE70,0xFEFF),
        new BasicMultilingualPlane("","Halfwidth and Fullwidth Forms",0xFF00,0xFFEF),
        new BasicMultilingualPlane("","Specials",0xFFF0,0xFFFF)
    };

    // http://pinvoke.net/default.aspx/gdi32/GetDeviceCaps.html
    public enum DeviceCap
    {
        /// <summary>
        ///     Device driver version
        /// </summary>
        DRIVERVERSION = 0,

        /// <summary>
        ///     Device classification
        /// </summary>
        TECHNOLOGY = 2,

        /// <summary>
        ///     Horizontal size in millimeters
        /// </summary>
        HORZSIZE = 4,

        /// <summary>
        ///     Vertical size in millimeters
        /// </summary>
        VERTSIZE = 6,

        /// <summary>
        ///     Horizontal width in pixels
        /// </summary>
        HORZRES = 8,

        /// <summary>
        ///     Vertical height in pixels
        /// </summary>
        VERTRES = 10,

        /// <summary>
        ///     Number of bits per pixel
        /// </summary>
        BITSPIXEL = 12,

        /// <summary>
        ///     Number of planes
        /// </summary>
        PLANES = 14,

        /// <summary>
        ///     Number of brushes the device has
        /// </summary>
        NUMBRUSHES = 16,

        /// <summary>
        ///     Number of pens the device has
        /// </summary>
        NUMPENS = 18,

        /// <summary>
        ///     Number of markers the device has
        /// </summary>
        NUMMARKERS = 20,

        /// <summary>
        ///     Number of fonts the device has
        /// </summary>
        NUMFONTS = 22,

        /// <summary>
        ///     Number of colors the device supports
        /// </summary>
        NUMCOLORS = 24,

        /// <summary>
        ///     Size required for device descriptor
        /// </summary>
        PDEVICESIZE = 26,

        /// <summary>
        ///     Curve capabilities
        /// </summary>
        CURVECAPS = 28,

        /// <summary>
        ///     Line capabilities
        /// </summary>
        LINECAPS = 30,

        /// <summary>
        ///     Polygonal capabilities
        /// </summary>
        POLYGONALCAPS = 32,

        /// <summary>
        ///     Text capabilities
        /// </summary>
        TEXTCAPS = 34,

        /// <summary>
        ///     Clipping capabilities
        /// </summary>
        CLIPCAPS = 36,

        /// <summary>
        ///     Bitblt capabilities
        /// </summary>
        RASTERCAPS = 38,

        /// <summary>
        ///     Length of the X leg
        /// </summary>
        ASPECTX = 40,

        /// <summary>
        ///     Length of the Y leg
        /// </summary>
        ASPECTY = 42,

        /// <summary>
        ///     Length of the hypotenuse
        /// </summary>
        ASPECTXY = 44,

        /// <summary>
        ///     Shading and Blending caps
        /// </summary>
        SHADEBLENDCAPS = 45,

        /// <summary>
        ///     Logical pixels inch in X
        /// </summary>
        LOGPIXELSX = 88,

        /// <summary>
        ///     Logical pixels inch in Y
        /// </summary>
        LOGPIXELSY = 90,

        /// <summary>
        ///     Number of entries in physical palette
        /// </summary>
        SIZEPALETTE = 104,

        /// <summary>
        ///     Number of reserved entries in palette
        /// </summary>
        NUMRESERVED = 106,

        /// <summary>
        ///     Actual color resolution
        /// </summary>
        COLORRES = 108,

        // Printing related DeviceCaps. These replace the appropriate Escapes
        /// <summary>
        ///     Physical Width in device units
        /// </summary>
        PHYSICALWIDTH = 110,

        /// <summary>
        ///     Physical Height in device units
        /// </summary>
        PHYSICALHEIGHT = 111,

        /// <summary>
        ///     Physical Printable Area x margin
        /// </summary>
        PHYSICALOFFSETX = 112,

        /// <summary>
        ///     Physical Printable Area y margin
        /// </summary>
        PHYSICALOFFSETY = 113,

        /// <summary>
        ///     Scaling factor x
        /// </summary>
        SCALINGFACTORX = 114,

        /// <summary>
        ///     Scaling factor y
        /// </summary>
        SCALINGFACTORY = 115,

        /// <summary>
        ///     Current vertical refresh rate of the display device (for displays only) in Hz
        /// </summary>
        VREFRESH = 116,

        /// <summary>
        ///     Vertical height of entire desktop in pixels
        /// </summary>
        DESKTOPVERTRES = 117,

        /// <summary>
        ///     Horizontal width of entire desktop in pixels
        /// </summary>
        DESKTOPHORZRES = 118,

        /// <summary>
        ///     Preferred blt alignment
        /// </summary>
        BLTALIGNMENT = 119
    }

    public const int SW_HIDE = 0;
    public const int SW_SHOWNORMAL = 1;

    [DllImport("User32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
#pragma warning disable CA1401 // P/Invokes should not be visible
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
#pragma warning restore CA1401 // P/Invokes should not be visible

    [DllImport("user32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool EnumThreadWindows(int threadId, EnumWindowsProc callback, IntPtr lParam);

    [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int maxCount);

    [DllImport("gdi32.dll")]
    private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

    public static int ScalingFactor()
    {
        using var g = Graphics.FromHwnd(IntPtr.Zero);
        var desktop = g.GetHdc();
        var logicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
        var physicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);
        var scalingFactor = physicalScreenHeight / (float)logicalScreenHeight;
        return (int)(scalingFactor * 100); // 1.25 = 125%
    }

    public static Size PhysicalScreenSize()
    {
        Size scrSize = new();
        using var g = Graphics.FromHwnd(IntPtr.Zero);
        var desktop = g.GetHdc();

        scrSize.Height = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);
        scrSize.Width = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPHORZRES);
        return scrSize;
    }

    public static string[] GetUnicodes(string str)
    {
        List<string> unicodes = new();
        if (!String.IsNullOrEmpty(str))
        foreach (var bmp in BMP)
        {
            if (Regex.IsMatch(str, "[\\u" + bmp.UniStart.ToString("X4") + "-\\u" + bmp.UniEnd.ToString("X4") + "]+"))
                unicodes.Add((String.IsNullOrEmpty(bmp.ScriptGroup) ? "" : bmp.ScriptGroup + "\\") + bmp.Name);
        }
        return unicodes.ToArray();
    }
    public static bool IsArabic(string str)
    {
        return Regex.IsMatch(str, @"[\u0600-\u06FF]+");
    }
    public static int BinarySize(BigInteger num)
    {
            int size = 0;
            for (; num != 0; num >>= 1)
                size++;
            return size;
    }
    public static T[] SubArray<T>(this T[] data, int index, int length)
    {
        var result = new T[length];
        Array.Copy(data, index, result, 0, length);
        return result;
    }

    public static IEnumerable<string> GetWindowText(Process p)
    {
        List<string> titles = new();
        foreach (ProcessThread t in p.Threads)
            _ = EnumThreadWindows(t.Id, (hWnd, lParam) =>
              {
                  StringBuilder text = new(200);
                  _ = GetWindowText(hWnd, text, 200);
                  titles.Add(text.ToString());
                  return true;
              }, IntPtr.Zero);
        return titles;
    }

    public static void AppendAllBytes(string path, byte[] bytes)
    {
        //argument-checking here.

        using var stream = new FileStream(path, FileMode.Append);
        stream.Write(bytes, 0, bytes.Length);
    }

    public static bool IsHex(this string hex)
    {
        return hex.ContainsOnly("0123456789abcdefABCDEFxX");
    }

    public static bool IsSureHex(this string hex)
    {
        return hex.ContainsOnly("abcdefABCDEFxX");
    }
    public static string FirstUpper(string s)
    {
        return char.ToUpper(s[0]) + s[1..];
    }

    public static byte[] UnicodeArr(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            var b = new byte[2] { 0, 0 };
            return b;
        }

        return Encoding.Convert(Encoding.ASCII, Encoding.Unicode, Encoding.ASCII.GetBytes(s));
    }

    public static string Unicode(string s)
    {
        return Encoding.Unicode.GetString(UnicodeArr(s));
    }

    public static int FindPattern(byte[] data, byte[] pattern, int startPos = 0)
    {
        var idx1 = startPos;

        while (idx1 < data.Length)
        {
            if (data[idx1] == pattern[0])
            {
                var good = true;
                for (var idx2 = 1; idx2 < pattern.Length; ++idx2)
                    if (data[idx1 + idx2] != pattern[idx2])
                    {
                        good = false;
                        break;
                    }

                if (good) return idx1;
            }

            ++idx1;
        }

        return -1;
    }


    public static byte[] GetBytes(string str)
    {
        var bytes = new byte[str.Length * sizeof(char)];
        Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
        return bytes;
    }

    public static string GetString(byte[] bytes)
    {
        var chars = new char[bytes.Length / sizeof(char)];
        Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
        return new string(chars);
    }

    public static int KeyPos(byte[] buffer, string key, int startPos = 0)
    {
        return FindPattern(buffer, UnicodeArr(key), startPos);
    }

    public static int KeyPos(byte[] buffer, byte[] key, int startPos = 0)
    {
        return FindPattern(buffer, key, startPos);
    }

    /// <summary>
    ///     Convert String Of 1's,0's ... to HEX String
    /// </summary>
    /// <param name="binary"></param>
    /// <returns></returns>
    public static string BinaryStringToHexString(string binary)
    {
        if (string.IsNullOrEmpty(binary))
            return binary;

        StringBuilder result = new(binary.Length / 8 + 1);

        // TODO: check all 1's or 0's... throw otherwise

        var mod4Len = binary.Length % 8;
        if (mod4Len != 0)
            // pad to length multiple of 8
            binary = binary.PadLeft((binary.Length / 8 + 1) * 8, '0');

        for (var i = 0; i < binary.Length; i += 8)
        {
            var eightBits = binary.Substring(i, 8);
            result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
        }

        return result.ToString();
    }

    public static string ByteArrayToHexString(byte[] binary, bool fixedLength = false)
    {
        if (binary == null) return "";

        StringBuilder result = new(binary.Length);

        for (var i = 0; i < binary.Length; i++) result.AppendFormat("{0:X2}", binary[i]);
        var s = result.ToString();
        if (!fixedLength)
            while (s.Length > 1 && s[0] == '0')
                s = s[1..];
        if (s.Length == 1) s = "0" + s;
        return s;
    }

    public static string ByteArrayToBinaryString(byte[] bytes, bool fixedLength = false)
    {
        var base2 = new StringBuilder(bytes.Length * 8);
        var binary = fixedLength ? Convert.ToString(bytes[0], 2).PadLeft(8, '0') : Convert.ToString(bytes[0], 2);
        base2.Append(binary);
        for (var index = 1; index < bytes.Length; index++)
            base2.Append(Convert.ToString(bytes[index], 2).PadLeft(8, '0'));
        return base2.ToString();
    }

    public static byte[] HexStringToByteArray(string s)
    {
        s = s.Replace(" ", "");
        if (s.Length % 2 != 0) s = "0" + s;
        var binary = new byte[s.Length / 2];
        for (var i = 0; i < s.Length; i += 2) binary[i / 2] = Convert.ToByte(string.Concat("0x", s.AsSpan(i, 2)), 16);

        return binary;
    }

    public static bool IsNumeric(string s)
    {
        // shorter version of this line
        // var isNumber = s.All(c => Char.IsNumber(c));
        return s.All(char.IsNumber);
    }

    public static bool ContainsNumber(string s)
    {
        // shorter version of this line
        // var containsNumbers = s.Any(c => Char.IsNumber(c));
        return s.Any(char.IsNumber);
    }

    public static int MakeHash(string s)
    {
        var n = 0;
        for (var i = 0; i < s.Length; i++)
            n = n * 2 + s[i];
        return n;
    }

    public static bool IsPrime(int n)
    {
        if (n.Equals(2) || n.Equals(3))
            return true;
        if (n <= 1 || (n % 2).Equals(0) || (n % 3).Equals(0)) return false;

        var i = 5;
        while (Math.Pow(i, 2) <= n)
        {
            if ((n % i).Equals(0) || (n % (i + 2)).Equals(0)) return false;
            i += 6;
        }

        return true;
    }

    public static int GCD(int a, int b)
    {
        int Remainder;

        while (b != 0)
        {
            Remainder = a % b;
            a = b;
            b = Remainder;
        }

        return a;
    }

    public static int LCM(int a, int b)
    {
        return Math.Abs(a * b) / GCD(a, b);
    }

    public static bool Is_Carmichael(int n)
    {
        if (n < 2) return false;
        var k = n;
        for (var i = 2; i <= k / i; ++i)
            if (k % i == 0)
            {
                if (k / i % i == 0) return false;
                if ((n - 1) % (i - 1) != 0) return false;
                k /= i;
                i = 1;
            }

        return k != n && (n - 1) % (k - 1) == 0;
    }


    public static byte[] GetHash(byte[] data, string hashAlgorithm = "SHA256")
    {
        byte[] hash;

        switch (hashAlgorithm)
        {
            case "MD5":
                MD5 md5 =  MD5.Create();
                hash = md5.ComputeHash(data);
                md5.Dispose();
                break;
            case "SHA256":
                SHA256 sh256 = SHA256.Create();
                hash = sh256.ComputeHash(data);
                sh256.Dispose();
                break;
            case "SHA512":
                SHA512 sh512 = SHA512.Create();
                hash = sh512.ComputeHash(data);
                sh512.Dispose();
                break;
            case "SHA384":
                SHA384 sh384 = SHA384.Create();
                hash = sh384.ComputeHash(data);
                sh384.Dispose();
                break;
            default:
                SHA1 sh1 = SHA1.Create();
                hash = sh1.ComputeHash(data);
                sh1.Dispose();
                break;
        }

        return hash;
    }

    public static byte[] GetHash(string data, string hashAlgorithm = "SHA256")
    {
        return GetHash(Encoding.ASCII.GetBytes(data), hashAlgorithm);
    }


    public static IEnumerable<T> OfType<T>(IEnumerable e) where T : class
    {
        foreach (var cur in e)
        {
            if (cur is T val) yield return val;
        }
    }

    public static bool In<T>(this T item, params T[] items)
    {
        if (items == null)
            throw new ArgumentNullException(nameof(items));

        return items.Contains(item);
    }

    public static string ResourceReadAllText(string fileName, Encoding encoding)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();
        var resourceStream =
            currentAssembly.GetManifestResourceStream($"{currentAssembly.GetType().Namespace}.{fileName}");
        using var reader = new StreamReader(resourceStream, encoding);
        return reader.ReadToEnd();
    }

    public static string[] GetResources()
    {
        //var currentAssembly = Assembly.GetExecutingAssembly();
        //From the assembly where this code lives!
        // this.GetType().Assembly.GetManifestResourceNames()
        //or from the entry point to the application - there is a difference!
        return Assembly.GetExecutingAssembly().GetManifestResourceNames();
    }

    public static Stream GetResourceStream(string resName)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();
        return currentAssembly.GetManifestResourceStream(currentAssembly.GetName().Name + "." + resName);
    }

    public static string ResourceReadAllText(string resourceName)
    {
        var file = GetResourceStream(resourceName);
        var all = "";

        using (var reader = new StreamReader(file))
        {
            all = reader.ReadToEnd();
        }

        file.Dispose();
        return all;
    }

    public static byte[] ResourceReadAllBytes(string resourceName)
    {
        var file = GetResourceStream(resourceName.Replace("\\", "."));
        byte[] all;

        using (var reader = new BinaryReader(file))
        {
            all = reader.ReadBytes((int)file.Length);
        }

        file.Dispose();
        return all;
    }


    public static int FindString(this string data, string str, int startIndex, int occurence = 1)
    {
        var p = startIndex;
        for (var i = 0; i < occurence && p >= 0; i++)
        {
            p = data.IndexOf(str, startIndex);
            startIndex = p + 1;
        }

        return p;
    }

    public static string ExtractData(this string data, string str, ref int startIndex, int skip)
    {
        var s = "";
        var n = FindString(data, str, startIndex + skip);
        if (n > 0)
        {
            s = data.Substring(startIndex + skip, n - startIndex - skip).Trim();
            startIndex = n + skip;
        }

        return s;
    }

    public static void Init<T>(this T[] array, T defaultVaue)
    {
        if (array == null)
            return;
        for (var i = 0; i < array.Length; i++) array[i] = defaultVaue;
    }

    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    #region ExecuteCommand Sync and Async

    /// <summary>
    ///     Executes a shell command synchronously.
    /// </summary>
    /// <param name="command">string command</param>
    /// <returns>string, as output of the command.</returns>
    public static void ExecuteCommandSync(object command)
    {
        try
        {
            // create the ProcessStartInfo using "cmd" as the program to be run, and "/c " as the parameters.
            // Incidentally, /c tells cmd that we want it to execute the command that follows, and then exit.
            ProcessStartInfo procStartInfo = new("cmd", "/c " + command);
            // The following commands are needed to redirect the standard output. 
            //This means that it will be redirected to the Process.StandardOutput StreamReader.
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            // Do not create the black window.
            procStartInfo.CreateNoWindow = true;
            // Now we create a process, assign its ProcessStartInfo and start it
            Process proc = new();
            proc.StartInfo = procStartInfo;
            proc.Start();

            // Get the output into a string
            var result = proc.StandardOutput.ReadToEnd();

            // Display the command output.
            Console.WriteLine(result);
        }
        catch (Exception objException)
        {
            // Log the exception
            MessageBox.Show(objException.Message);
        }
    }

    /// <summary>
    ///     Execute the command Asynchronously.
    /// </summary>
    /// <param name="command">string command.</param>
    public static void ExecuteCommandAsync(string command)
    {
        //Asynchronously start the Thread to process the Execute command request.
        Thread objThread = new(ExecuteCommandSync);
        //Make the thread as background thread.
        objThread.IsBackground = true;
        //Set the Priority of the thread.
        objThread.Priority = ThreadPriority.AboveNormal;
        //Start the thread.
        objThread.Start(command);
    }

    public static int RunProcess(string prog, string cmd = "", bool redirectOuput = false, bool hidden = false)
    {
        try
        {
            using Process proc = new();
            proc.StartInfo.FileName = prog; // Specify exe name.
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = redirectOuput;
            proc.StartInfo.RedirectStandardError = redirectOuput;
            proc.StartInfo.Arguments = cmd;
            proc.EnableRaisingEvents = true;
            if (hidden)
            {
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }

            //Redirect Console output to String
            //StringBuilder builder = new StringBuilder();
            //TextWriter writer = new StringWriter(builder);
            //Console.SetOut(writer);

            // Run the external process & wait for it to finish
            proc.Start();

            if (hidden) ShowWindow(proc.MainWindowHandle, SW_HIDE);

            //Read Process output and display to console
            if (redirectOuput)
            {
                var r = proc.StandardOutput.ReadToEnd();
                Console.WriteLine(r);
            }

            proc.WaitForExit();
            return proc.ExitCode;
            // Retrieve the app's exit code
        }
        catch
        {
            return -1;
        }
    }

    public static int RunProg(string prog, string cmd = "")
    {
        try
        {
            // Run the external process & wait for it to finish
            using var proc = Process.Start(prog, cmd);
            proc.WaitForExit();
            // Retrieve the app's exit code
            return proc.ExitCode;
        }
        catch
        {
            return -1;
        }
    }

    /// <summary>
    /// Copy structure to array of byte
    /// </summary>
    /// <param name="str">struct to be copied to an array</param>
    public static unsafe byte[] ConvertStruct<T>(ref T str) where T : struct
    {
        int size = Marshal.SizeOf(str);
        var arr = new byte[size];

        fixed (byte* arrPtr = arr)
        {
            Marshal.StructureToPtr(str, (IntPtr)arrPtr, true);
        }

        return arr;
    }

    [DllImport("gdi32", EntryPoint = "AddFontResource", CharSet = CharSet.Unicode)]
#pragma warning disable CA1401 // P/Invokes should not be visible
    public static extern int AddFontResourceA(string lpFileName);
#pragma warning restore CA1401 // P/Invokes should not be visible
    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    private static extern int AddFontResource(string lpszFilename);
    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    private static extern int CreateScalableFontResource(uint fdwHidden, string  lpszFontRes, string lpszFontFile, string lpszCurrentPath);

    /// <summary>
    /// Installs font on the user's system and adds it to the registry so it's available on the next session
    /// Your font must be included in your project with its build path set to 'Content' and its Copy property
    /// set to 'Copy Always'
    /// </summary>
    /// <param name="contentFontName">Your font to be passed as a resource (i.e. "myfont.tff")</param>
    public static void RegisterFont(string contentFontName)
    {
        string fontFile = Path.GetFileName(contentFontName);
        // Creates the full path where your font will be installed
        var fontDestination = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Fonts), fontFile);

        if (!File.Exists(fontDestination))
        {
            // Copies font to destination
            System.IO.File.Copy(contentFontName, fontDestination);

            // Retrieves font name
            // Makes sure you reference System.Drawing
            PrivateFontCollection fontCol = new();
            fontCol.AddFontFile(fontDestination);
            var actualFontName = fontCol.Families[0].Name;

            //Add font
            _ = AddFontResource(fontDestination);
            //Add registry entry  
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", actualFontName, fontFile, RegistryValueKind.String);
        }
    }

    public static void OpenDocument(string docName)
    {
        Process p = new();
        ProcessStartInfo s = new(docName);
        s.UseShellExecute = true;
        p.StartInfo = s;
        p.Start();
    }

    #endregion
}