namespace CryptoUtility;

internal interface ISettings
{
    public void WriteValue(string section, string key, string value);
    public string ReadValue(string section, string key, string defaultValue = "");
}