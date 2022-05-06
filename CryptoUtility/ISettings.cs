namespace CryptoUtility;

internal interface ISettings
{
    public const string NewLineSep = "{NL}";
    public void WriteValue(string section, string key, string value);
    public string ReadValue(string section, string key, string defaultValue = "");
}