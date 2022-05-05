using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CryptoUtility;

internal class IniFile : ISettings
{
    private readonly string path;

    /// <summary>
    ///     INIFile Constructor.
    /// </summary>
    /// <PARAM name="INIPath"></PARAM>
    public IniFile(string iniPath)
    {
        path = iniPath;
    }

    /// <summary>
    ///     Write Data to the INI File
    /// </summary>
    /// <PARAM name="Section"></PARAM>
    /// Section name
    /// <PARAM name="Key"></PARAM>
    /// Key Name
    /// <PARAM name="Value"></PARAM>
    /// Value Name
    public void WriteValue(string section, string key, string value)
    {
        WritePrivateProfileString(section, key, value, path);
    }

    /// <summary>
    ///     Read Data Value From the Ini File
    /// </summary>
    /// <PARAM name="Section"></PARAM>
    /// <PARAM name="Key"></PARAM>
    /// <PARAM name="DefaultValue"></PARAM>
    /// <returns></returns>
    public string ReadValue(string section, string key, string defaultValue = "\0")
    {
        var temp = new StringBuilder(255);
        var i = GetPrivateProfileString(section, key, defaultValue, temp, 255, path);
        if (i == 0 && defaultValue == "\0")
            throw new Exception("Value not found");
        return temp.ToString();
    }

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal,
        int size, string filePath);
}