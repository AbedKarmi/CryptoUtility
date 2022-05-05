using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.IO;
using System.Net;
using System.Text;

namespace SystemInformation
{
    public class SysInfo
    {
        private string manufacturer, processor, processorCoreScore, timeOfTest,
                       socket, clockSpeed, fsb, cache, idleLoad, windowsVersion;
        private readonly string webServerUrl;

        private int processorCoreRunningThread;

        private PropertyDataCollection processorInfo, osInfo;

        /// <summary>
        /// SysInfo constructor.
        /// </summary>
        /// <param name="url">The web application url where the test results are submited to</param>
        /// <param name="processorCoreRunningThread">The processor core actually running the test
        /// </param>
        public SysInfo(string webServerUrl, int processorCoreRunningThread)
        {
            this.webServerUrl = webServerUrl;
            this.processorCoreRunningThread = processorCoreRunningThread;
        }

        /// <summary>
        /// Gets information about the system.
        /// </summary>
        public void GetSystemInformation()
        {
            processorInfo = GetProperties("Win32_Processor")[processorCoreRunningThread];
            osInfo = GetProperties("Win32_OperatingSystem")[0];

            manufacturer = CleanString(processorInfo["Manufacturer"].Value.ToString());
            processor = CleanString(processorInfo["Name"].Value.ToString());
            timeOfTest = DateTime.Now.ToUniversalTime().ToString("dd.MM.yyyy HH:mm:ss");

            DetermineProcessorClockSpeed();

            socket = CleanString(processorInfo["SocketDesignation"].Value.ToString());
            fsb = CleanString(processorInfo["ExtClock"].Value.ToString());
            cache = CleanString(processorInfo["L2CacheSize"].Value.ToString());
            idleLoad = CleanString(processorInfo["LoadPercentage"].Value.ToString());

            windowsVersion = String.Format("{0} {1} ({2})",
                                    CleanString(osInfo["Caption"].Value.ToString()),
                                    CleanString(osInfo["CSDVersion"].Value.ToString()),
                                    CleanString(osInfo["Version"].Value.ToString()));
        }

        /// <summary>
        /// Sets the processor score.
        /// </summary>
        public int ProcessorCoreScore
        {
            set
            { 
                processorCoreScore = value.ToString();
            }
        }

        /// <summary>
        /// Gets the current system information string.
        /// </summary>
        public string SystemInformationString
        {
            get
            {
                string info = "Manufacturer: " + manufacturer + "\r\n\r\n" +
                              "Processor: " + processor + "\r\n\r\n" +
                              "Processor Core Score: " + processorCoreScore + "\r\n\r\n" +
                              "Time Of Test (Universal): " + timeOfTest + "\r\n\r\n" +
                              "Socket: " + socket + "\r\n\r\n" +
                              "Clock Speed: " + clockSpeed + " MHz\r\n\r\n" +
                              "FSB Speed: " + fsb + " MHz\r\n\r\n" +
                              "L2 Cache Size: " + cache + " KB\r\n\r\n" +
                              "Idle Load: " + idleLoad + "%\r\n\r\n" +
                              "Windows Version: " + windowsVersion;

                return info;
            }
        }

        /// <summary>
        /// Determines the processor current clock speed.
        /// </summary>
        private void DetermineProcessorClockSpeed()
        {
            int currentClockSpeed = int.Parse(CleanString(processorInfo["CurrentClockSpeed"].Value.ToString()));
            int maxClockSpeed = int.Parse(CleanString(processorInfo["MaxClockSpeed"].Value.ToString()));

            if (currentClockSpeed < maxClockSpeed)
                clockSpeed = maxClockSpeed.ToString();
            else
                clockSpeed = currentClockSpeed.ToString();
        }

        /// <summary>
        /// Gets the list of properties of a given WMI (Windows Management Instrumentation) class.
        /// </summary>
        /// <param name="managementClassName">The name of the WMI class</param>
        /// <returns>The PropertyDataCollection corresponding to the given WMI class</returns>
        private List<PropertyDataCollection> GetProperties(string managementClassName)
        {
            ManagementClass management = new ManagementClass(managementClassName);
            List<PropertyDataCollection> properties = new List<PropertyDataCollection>();

            foreach (ManagementObject obj in management.GetInstances())
                properties.Add(obj.Properties);

            return properties;
        }

        /// <summary>
        /// Trims the string, also removing redundant spaces or tabs inside the string.
        /// </summary>
        /// <param name="current">The string given as an input parameter</param>
        /// <returns>The cleaned string</returns>
        private string CleanString(string current)
        {
            string output = "";
            bool spaceCharEncountered = false;
            int i = 0;

            while (i < current.Length)
            {
                if ((current[i] == ' ') || (current[i] == '\t'))
                    spaceCharEncountered = true;

                else
                    if (spaceCharEncountered == true)
                    {
                        output += " " + current[i];
                        spaceCharEncountered = false;
                    }
                    else
                        output += current[i];

                i++;
            }

            return output.TrimStart();
        }

        /// <summary>
        /// Gets the current system information as a parameters list for a GET request.
        /// </summary>
        private string SystemInformationGETRequestString
        {
            get
            {
                string getRequest = "manufacturer=" + manufacturer + "&" +
                                    "processor=" + processor + "&" +
                                    "processorCoreScore=" + processorCoreScore + "&" +
                                    "timeOfTest=" + timeOfTest;

                return getRequest;
            }
        }

        /// <summary>
        /// Gets the current system information for a POST request.
        /// </summary>
        private string SystemInformationPOSTRequestString
        {
            get
            {
                string postRequest = manufacturer + "~" + processor + "~" + processorCoreScore + "~" +
                                     timeOfTest + "~" + socket + "~" + clockSpeed + " MHz~" +
                                     fsb + " MHz~" + cache + " KB~" + idleLoad + "%~" + windowsVersion;

                return postRequest;
            }
        }

        /// <summary>
        /// Submits a GET request to the web server, through the web browser installed.
        /// </summary>
        public void SubmitGETRequest()
        {
            string submitUrl = webServerUrl + "?" + SystemInformationGETRequestString;
            Process.Start("rundll32.exe" ,"url.dll,FileProtocolHandler " + submitUrl);
        }

        /// <summary>
        /// Submits a POST request to the web server, through the .NET API.
        /// </summary>
        public void SubmitPOSTRequest()
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(webServerUrl);
            myRequest.Method = "POST";

            Stream requestStream = myRequest.GetRequestStream();
            byte[] bytes = Encoding.ASCII.GetBytes(SystemInformationPOSTRequestString);
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Flush();
            requestStream.Close();

            WebResponse myResponse = myRequest.GetResponse();
            myResponse.Close();     
        }
    }
}
