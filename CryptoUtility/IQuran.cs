using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoUtility
{
    public abstract class QInit<T>
    {
        public QInit(T parameters)
        {

        }
    }
    public interface IQuran
    {
        public enum QuranTextVersion : short { FirstOriginal = 7, NewNoDiacritics = 5, NewWithDiacritics = 4 };
        public QuranTextVersion QuranTextIndex { get; set; }
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
        public string[] GetSoraNames();
        public List<Aya> GetSora(int index);
        public List<string> GetSoraText(int index);
        public DataTable GetSoraTable(int index);
        public bool OpenQuran();
        public bool CloseQuran();

        public int CurQuranTextIndex();
    }
}
