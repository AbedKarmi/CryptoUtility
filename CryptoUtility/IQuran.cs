using System.Collections.Generic;
using System.Data;

namespace CryptoUtility;

public abstract class QInit<T>
{
    public QInit(T parameters)
    {
    }
}

public interface IQuran
{
    public enum QuranTextVersion : short
    {
        FirstOriginal = 7,
        NewNoDiacritics = 5,
        NewWithDiacritics = 4
    }

    public QuranTextVersion QuranTextIndex { get; set; }
    public string[] GetSoraNames();
    public List<Aya> GetSora(int index);
    public List<string> GetSoraText(int index);
    public DataTable GetSoraTable(int index);
    public DataTable GetSorasTable(IList<int> soras);
    public bool OpenQuran();
    public bool CloseQuran();

    public int CurQuranTextIndex();

    public struct Aya
    {
        public int serial;
        public byte soraNo;
        public string soraName;
        public int ayaNo;
        public string ayaText;
        public int words;
        public int letters;
    }
}