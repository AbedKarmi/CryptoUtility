using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoUtility
{
    class QuranDB : QInit<string>, IQuran
    {

        public QuranDB(string StartupPath) : base(StartupPath)
        {
            startupPath = StartupPath;
            OpenQuran();
            ReadQuranIndex();
        }
        ~QuranDB()
        {   // Do not use close connection in destructor , it will not work
            //CloseQuran();
        }
        public struct QuranIndex
        {
            public int serial;
            public byte soraNo;
            public string soraName;
        }

        public IQuran.QuranTextVersion QuranTextIndex { get; set; }
            
        public List<QuranIndex> quranIndex = new();

        string startupPath;
        private OleDbConnection connection;
   
        public bool OpenQuran()
        {
            try
            {
                QuranTextIndex = IQuran.QuranTextVersion.NewWithDiacritics;
                string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + startupPath + "\\Quran.accdb;User Id=;Password=;";
                if (!File.Exists(startupPath + "\\Quran.accdb"))
                    File.WriteAllBytes(startupPath + "\\Quran.accdb", MyClass.ResourceReadAllBytes("DB\\Quran.accdb"));
                connection = new OleDbConnection(connectionString);
                connection.Open();
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }

        public bool CloseQuran()
        {
            try
            {
                GC.Collect();
                connection.Close();
                connection.Dispose();
                /*
                int i = 0;
                do
                {
                    File.Delete(startupPath + "\\Quran.accdb");
                    Thread.Sleep(250);
                } while (File.Exists(startupPath + "\\Quran.accdb") & i++<15);
                */
                return true;
            }
            catch (Exception ex) {Console.WriteLine(ex.Message); return false; }
        }
        public string[] GetSoraNames()
        {
            List<string> SoraNames = new();
            foreach (var aya in quranIndex)
            {
                
                SoraNames.Add(string.Format("[{0,-3}] ", aya.soraNo) + aya.soraName );
            }
            return SoraNames.ToArray();
        }

        private void ReadQuranIndex()
        {
            string queryString = "SELECT * FROM [QuranIndex]";
            using (OleDbCommand command = new OleDbCommand(queryString, connection))
            {
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    QuranIndex q = new();
                    
                    q.serial = Convert.ToInt32(reader[0]);
                    q.soraNo = Convert.ToByte(reader[1]);
                    q.soraName = reader.GetString(2);
                    quranIndex.Add(q);
                }
                reader.Close();
            }
        }


        public  int CurQuranTextIndex()
        {
            return (int) QuranTextIndex ;
        }
        public List<IQuran.Aya> GetSora(int index)
        {
            List<IQuran.Aya> sora = new();

            string queryString = "SELECT * FROM [Quran] WHERE [Sora]="+(index+1).ToString();
            using (OleDbCommand command = new OleDbCommand(queryString, connection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                int n = CurQuranTextIndex();
                while (reader.Read())
                {
                    IQuran.Aya aya = new();
                    aya.serial = Convert.ToInt32(reader[0]);
                    aya.soraNo = Convert.ToByte(reader[1]);
                    aya.soraName = reader[2].ToString();
                    aya.ayaNo = Convert.ToInt32(reader[3]);
                    aya.ayaText = reader[n].ToString(); 
                    aya.words = Convert.ToInt32(reader[8]);
                    aya.letters = Convert.ToInt32(reader[9]);

                    sora.Add(aya);
                }
                reader.Close();
            }
            return sora;
        }

        public DataTable GetSoraTable(int index)
        {
            DataTable dt = new DataTable();
            string queryString = "SELECT * FROM [Quran] WHERE [Sora]=" + (index + 1).ToString();
            using (OleDbCommand command = new OleDbCommand(queryString, connection))
            using (OleDbDataAdapter da = new OleDbDataAdapter(command))
            {
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetSorasTable(IList<int> soras)
        {
            DataTable dt = new DataTable();
            
            string list = "";
            foreach (var s in soras) { list +=(s+1).ToString() + ","; };
            list = list.Substring(0, list.Length - 1);

            string queryString = "SELECT * FROM [Quran] WHERE [Sora] in (" +list+")";
            using (OleDbCommand command = new OleDbCommand(queryString, connection))
            using (OleDbDataAdapter da = new OleDbDataAdapter(command))
            {
                da.Fill(dt);
            }
            return dt;
        }
        public List<string> GetSoraText(int index)
        {
            List<string> sora = new();

            string queryString = "SELECT * FROM [Quran] WHERE [Sora]=" + (index + 1).ToString();
            using (OleDbCommand command = new OleDbCommand(queryString, connection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                int n = CurQuranTextIndex();
                while (reader.Read())
                {
                        sora.Add(reader[n].ToString() + Environment.NewLine); 
                }
                reader.Close();
            }
            return sora;
        }
    }
}
