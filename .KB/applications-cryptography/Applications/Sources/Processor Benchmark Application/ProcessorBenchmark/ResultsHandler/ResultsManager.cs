using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SystemInformation;

namespace ResultsHandler
{
    public class ResultsManager
    {
        private const string urlFile = "Files\\urlFile.txt";
        private readonly string webServerUrl;

        private SysInfo sysInfo;

        public ResultsManager()
        {
            webServerUrl = ReadWebServerUrl();
        }

        public ResultsManager(int processorCoreRunningThread)
        {
            webServerUrl = ReadWebServerUrl();
            sysInfo = new SysInfo(webServerUrl, processorCoreRunningThread);
        }

        /// <summary>
        /// Sets the processor score.
        /// </summary>
        public int ProcessorScore
        {
            set
            {
                if (sysInfo != null)
                    sysInfo.ProcessorCoreScore = value;
            }
        }

        /// <summary>
        /// Sets the processor core running the current thread.
        /// </summary>
        public void GenerateSystemInformation()
        {
            if (sysInfo != null)
                sysInfo.GetSystemInformation();
        }

        /// <summary>
        /// Views all the test results on the web server.
        /// </summary>
        public void ViewTestResultsFromWebServer()
        {
            if (sysInfo != null)
                sysInfo.SubmitGETRequest();
            else
                Process.Start("rundll32.exe" ,"url.dll,FileProtocolHandler " + webServerUrl);
        }

        /// <summary>
        /// Submits the current test data to the web server.
        /// </summary>
        public void SubmitTestDataToWebServer()
        {
            if (sysInfo == null)
                return;
            
            try
            {
                sysInfo.SubmitPOSTRequest();
            }

            catch (Exception)
            {
                MessageBox.Show("Could not connect to the web server!", "Web Server Unreachable",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Gets the current system information string.
        /// </summary>
        public string SystemInformationString
        {
            get
            {
                if (sysInfo == null)
                    return "";

                return sysInfo.SystemInformationString;
            }
        }

        /// <summary>
        /// Reads the url of the web server from a text file.
        /// </summary>
        /// <returns>The url of the web server.</returns>
        private string ReadWebServerUrl()
        {
            TextReader reader = new StreamReader(urlFile);
            string url = reader.ReadLine();
            reader.Close();

            return url;
        }
    }
}
