using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace CryptoUtility;

internal static class Program
{
    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
    /*    if (!File.Exists(Application.ExecutablePath + ".config"))
        {
            File.WriteAllBytes(Application.ExecutablePath + ".config", MyClass.ResourceReadAllBytes("App.config"));
            Process.Start(Application.ExecutablePath);
            return;
        }
    */
        Application.EnableVisualStyles();
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmMain());
    }
}
