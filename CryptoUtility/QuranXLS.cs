using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace CryptoUtility;
internal class QuranXLS : QInit<string>, IQuran
{
    public List<QuranIndex> quranIndex = new();
    private Excel.Range range;

    private readonly string startupPath;

    private Excel.Application xlApp;
    private Excel.Workbook xlWorkBook;
    private Excel.Worksheet xlWorkSheet;

    public QuranXLS(string StartupPath) : base(StartupPath)
    {
        startupPath = StartupPath;
        OpenQuran();
        ReadQuranIndex();
    }

    public IQuran.QuranTextVersion QuranTextIndex { get; set; }

    public bool OpenQuran()
    {
        try
        {
            QuranTextIndex = IQuran.QuranTextVersion.NewWithDiacritics;
            InitExcel(startupPath);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool CloseQuran()
    {
        try
        {
            CloseExcel();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public string[] GetSoraNames()
    {
        List<string> SoraNames = new();
        //            string sn = "";
        foreach (var aya in quranIndex)
            //                if (aya.soraName != sn)
            //                {
            SoraNames.Add(string.Format("[{0,-3}] ", aya.soraNo) + aya.soraName);
        //SoraNames.Add("[" + aya.soraNo.ToString("d2") + "] " + aya.soraName);
        //                  sn = aya.soraName;
        //                }
        return SoraNames.ToArray();
    }

    public int CurQuranTextIndex()
    {
        return (int)QuranTextIndex;
    }

    public List<IQuran.Aya> GetSora(int index)
    {
        int rCnt;
        // int cCnt;
        // int rw = 0;
        // int cl = 0;         

        // List<Ayat> quranText = new();
        List<IQuran.Aya> sora = new();

        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
        range = xlWorkSheet.UsedRange;

        var rw = range.Rows.Count;
        // cl = range.Columns.Count;

        rCnt = quranIndex[index].serial + 1;
        var n = CurQuranTextIndex() + 1;
        do
        {
            IQuran.Aya aya = new();
            aya.serial = (int)(range.Cells[rCnt, 1] as Excel.Range).Value2;
            aya.soraNo = (byte)(range.Cells[rCnt, 2] as Excel.Range).Value2;
            aya.soraName = (string)(range.Cells[rCnt, 3] as Excel.Range).Value2;
            aya.ayaNo = (int)(range.Cells[rCnt, 4] as Excel.Range).Value2;
            aya.ayaText = (string)(range.Cells[rCnt, n] as Excel.Range).Value2;
            aya.words = (int)(range.Cells[rCnt, 8] as Excel.Range).Value2;
            aya.letters = (int)(range.Cells[rCnt, 9] as Excel.Range).Value2;

            sora.Add(aya);
            //quranText.Add(aya);
            /*                for (cCnt = 1; cCnt <= cl; cCnt++)
                            {
                                str = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
                                MessageBox.Show(str);
                            }
            */
            rCnt++;
        } while (rCnt <= rw && (int)(range.Cells[rCnt, 2] as Excel.Range).Value2 == index + 1);

        return sora;
    }

    public List<string> GetSoraText(int index)
    {
        int rCnt;
        List<string> sora = new();
        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
        range = xlWorkSheet.UsedRange;
        var rw = range.Rows.Count;
        var n = CurQuranTextIndex() + 1;
        rCnt = quranIndex[index].serial + 1;
        do
        {
            sora.Add((string)(range.Cells[rCnt, n] as Excel.Range).Value2 + Environment.NewLine);
            rCnt++;
        } while (rCnt <= rw && (int)(range.Cells[rCnt, 2] as Excel.Range).Value2 == index + 1);

        return sora;
    }

    public DataTable GetSoraTable(int index)
    {
        var dt = new DataTable();
        //In following sample 'szFilePath' is the variable for filePath
        //szConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source = '" + startupPath +\\Quran.XLS"';Extended Properties=\"Excel 8.0;HDR=YES;\"";
        // if the File extension is .XLSX using below connection string
        var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + startupPath +
                               "\\Quran.xlsx';Extended Properties='Excel 12.0;HDR=YES;'";
        var queryString = "SELECT * FROM [Quran$] WHERE [Sora]=" + (index + 1);

        using (OleDbConnection connection = new(connectionString))
        using (OleDbCommand command = new(queryString, connection))
        using (OleDbDataAdapter da = new(command))
        {
            da.Fill(dt);
        }

        return dt;
    }

    public DataTable GetSorasTable(IList<int> soras)
    {
        var dt = new DataTable();

        var list = "";
        foreach (var s in soras) list += s + 1 + ",";
        ;
        list = list[0..^1];

        //In following sample 'szFilePath' is the variable for filePath
        //szConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source = '" + startupPath +\\Quran.XLS"';Extended Properties=\"Excel 8.0;HDR=YES;\"";
        // if the File extension is .XLSX using below connection string
        var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + startupPath +
                               "\\Quran.xlsx';Extended Properties='Excel 12.0;HDR=YES;'";
        var queryString = "SELECT * FROM [Quran$] WHERE [Sora] in (" + list + ")";

        using (OleDbConnection connection = new(connectionString))
        using (OleDbCommand command = new(queryString, connection))
        using (OleDbDataAdapter da = new(command))
        {
            da.Fill(dt);
        }

        return dt;
    }

    private void InitExcel(string StartupPath)
    {
        xlApp = new Excel.Application
        {
            DisplayAlerts = false,
            Visible = false,
            ScreenUpdating = false
        };
        //File.WriteAllBytes(Application.StartupPath + "\\Quran.xlsx", Properties.Resources.Quran);

        if (!File.Exists(StartupPath + "\\Quran.xlsx"))
            File.WriteAllBytes(StartupPath + "\\Quran.xlsx", MyClass.ResourceReadAllBytes("DB\\Quran.xlsx"));
        xlWorkBook = xlApp.Workbooks.Open(StartupPath + "\\Quran.xlsx", 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows,
            "\t", false, false, 0, true, 1, 0);
    }

    private void CloseExcel()
    {
        try
        {
            xlWorkBook.Close(true);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
            //File.Delete(StartupPath + "\\Quran.xlsx");
        }
        catch (Exception ex)
        {
            Debug.Print(ex.Message);
        }
    }

    private void ReadQuranIndex()
    {
        var file = GetQuranIndex().ToArray();
        foreach (var s in file)
            if (!string.IsNullOrEmpty(s))
            {
                var items = s.Split(',');
                QuranIndex q = new();
                q.serial = int.Parse(items[0]);
                q.soraNo = byte.Parse(items[1]);
                q.soraName = items[2];
                quranIndex.Add(q);
            }
    }

    private List<string> GetQuranIndex()
    {
        int rCnt;
        List<string> qIndex = new();

        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(2);
        range = xlWorkSheet.UsedRange;
        var rw = range.Rows.Count;
        rCnt = 1;
        do
        {
            qIndex.Add((int)(range.Cells[rCnt, 1] as Excel.Range).Value2 + "," +
                       ((byte)(range.Cells[rCnt, 2] as Excel.Range).Value2) + "," +
                       (string)(range.Cells[rCnt, 3] as Excel.Range).Value2 +
                       "\r\n");
            rCnt++;
        } while (rCnt <= rw);


        return qIndex;
    }

    public struct QuranIndex
    {
        public int serial;
        public byte soraNo;
        public string soraName;
    }
}