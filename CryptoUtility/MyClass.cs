using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Reflection;
using System.Collections;

namespace CryptoUtility
{
    public static class MyClass
    {
        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;

        [DllImport("User32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private extern static bool EnumThreadWindows(int threadId, EnumWindowsProc callback, IntPtr lParam);

        [DllImport("user32", SetLastError = true, CharSet = CharSet.Auto)]
        private extern static int GetWindowText(IntPtr hWnd, StringBuilder text, int maxCount);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);


        public static IEnumerable<string> GetWindowText(Process p)
        {
            List<string> titles = new List<string>();
            foreach (ProcessThread t in p.Threads)
            {
                EnumThreadWindows(t.Id, (hWnd, lParam) =>
                {
                    StringBuilder text = new StringBuilder(200);
                    GetWindowText(hWnd, text, 200);
                    titles.Add(text.ToString());
                    return true;
                }, IntPtr.Zero);
            }
            return titles;
        }

        public static void AppendAllBytes(string path, byte[] bytes)
        {
            //argument-checking here.

            using (var stream = new FileStream(path, FileMode.Append))
            {
                stream.Write(bytes, 0, bytes.Length);
            }
        }
        public static bool isHex(this string hex)
        {
            return hex.ContainsOnly("0123456789abcdefABCDEFxX");
        }
        public static string FirstUpper(string s)
        {
            return char.ToUpper(s[0]) + s.Substring(1);
        }
        public static byte[] UnicodeArr(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                byte[] b = new byte[2] { 0, 0 };
                return b;
            }
            else return Encoding.Convert(Encoding.ASCII, Encoding.Unicode, Encoding.ASCII.GetBytes(s));
        }
        public static string Unicode(string s)
        {
            return Encoding.Unicode.GetString(UnicodeArr(s));
        }
        public static int FindPattern(byte[] data, byte[] pattern, int startPos = 0)
        {
            int idx1 = startPos;

            while (idx1 < data.Length)
            {
                if (data[idx1] == pattern[0])
                {
                    bool good = true;
                    for (int idx2 = 1; idx2 < pattern.Length; ++idx2)
                        if (data[idx1 + idx2] != pattern[idx2]) { good = false; break; }

                    if (good) return idx1;
                }

                ++idx1;
            }

            return -1;
        }

   
        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static int KeyPos(byte[] buffer, string key, int startPos = 0)
        {
            return FindPattern(buffer, UnicodeArr(key), startPos);
        }

        public static int KeyPos(byte[] buffer, byte[] key, int startPos = 0)
        {
            return FindPattern(buffer, key, startPos);
        }

        /// <summary>
        /// Convert String Of 1's,0's ... to HEX String
        /// </summary>
        /// <param name="binary"></param>
        /// <returns></returns>
        public static string BinaryStringToHexString(string binary)
        {
            if (string.IsNullOrEmpty(binary))
                return binary;

            StringBuilder result = new StringBuilder(binary.Length / 8 + 1);

            // TODO: check all 1's or 0's... throw otherwise

            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {
                // pad to length multiple of 8
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();
        }
        public static string BinaryToHexString(byte[] binary)
        {
            if (binary == null) return "";

            StringBuilder result = new StringBuilder(binary.Length);

            for (int i = 0; i < binary.Length; i++)
            {
                result.AppendFormat("{0:X2}", binary[i]);
            }
            string s = result.ToString();
            while (s.Length > 1 && s[0] == '0') s = s.Substring(1);
            return s;
        }
        public static byte[] HexStringToBinary(String s)
        {
            if ((s.Length % 2) != 0) s = "0" + s;
            byte[] binary = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
            {
                binary[i / 2] = Convert.ToByte("0x" + s.Substring(i, 2), 16);
            }

            return binary;
        }
        #region ExecuteCommand Sync and Async
        /// <summary>
        /// Executes a shell command synchronously.
        /// </summary>
        /// <param name="command">string command</param>
        /// <returns>string, as output of the command.</returns>
        public static void ExecuteCommandSync(object command)
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run, and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows, and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);
                // The following commands are needed to redirect the standard output. 
                //This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();

                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();

                // Display the command output.
                Console.WriteLine(result);
            }
            catch (Exception objException)
            {
                // Log the exception
                System.Windows.Forms.MessageBox.Show(objException.Message);
            }
        }

        /// <summary>
        /// Execute the command Asynchronously.
        /// </summary>
        /// <param name="command">string command.</param>
        public static void ExecuteCommandAsync(string command)
        {
            try
            {
                //Asynchronously start the Thread to process the Execute command request.
                Thread objThread = new Thread(new ParameterizedThreadStart(ExecuteCommandSync));
                //Make the thread as background thread.
                objThread.IsBackground = true;
                //Set the Priority of the thread.
                objThread.Priority = ThreadPriority.AboveNormal;
                //Start the thread.
                objThread.Start(command);
            }
            /*    catch (ThreadStartException objException)
                {
                    // Log the exception
                }
                catch (ThreadAbortException objException)
                {
                    // Log the exception
                }
                catch (Exception objException)
                {
                    // Log the exception
                }
            */
            finally { }
        }

        public static int RunProcess(string prog, string cmd = "", bool redirectOuput = false, bool hidden = false)
        {
            try
            {
                using (Process proc = new Process())
                {
                    proc.StartInfo.FileName = prog; // Specify exe name.
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardOutput = redirectOuput;
                    proc.StartInfo.RedirectStandardError = redirectOuput;
                    proc.StartInfo.Arguments = cmd;
                    proc.EnableRaisingEvents = true;
                    if (hidden)
                    {
                        proc.StartInfo.CreateNoWindow = true;
                        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    }

                    //Redirect Console output to String
                    //StringBuilder builder = new StringBuilder();
                    //TextWriter writer = new StringWriter(builder);
                    //Console.SetOut(writer);

                    // Run the external process & wait for it to finish
                    proc.Start();

                    if (hidden) ShowWindow(proc.MainWindowHandle, SW_HIDE);

                    //Read Process output and display to console
                    if (redirectOuput)
                    {
                        string r = proc.StandardOutput.ReadToEnd();
                        Console.WriteLine(r);
                    }

                    proc.WaitForExit();
                    return proc.ExitCode;
                }
                // Retrieve the app's exit code
            }
            catch { return -1; }
        }

        public static int RunProg(string prog, string cmd = "")
        {
            try
            {

                // Run the external process & wait for it to finish
                using (Process proc = Process.Start(prog, cmd))
                {
                    proc.WaitForExit();
                    // Retrieve the app's exit code
                    return proc.ExitCode;
                }
            }
            catch { return -1; }
        }
        #endregion
        public static bool isNumeric(string s)
        {
            // shorter version of this line
            // var isNumber = s.All(c => Char.IsNumber(c));
            return s.All(Char.IsNumber);
        }
        public static bool ContainsNumber(string s)
        {
            // shorter version of this line
            // var containsNumbers = s.Any(c => Char.IsNumber(c));
            return s.Any(Char.IsNumber);
        }

        public static int MakeHash(string s)
        {
            int n = 0;
            for (int i = 0; i < s.Length; i++)
                n = n * 2 + s[i];
            return n;
        }
        public static bool IsPrime(int n)
        {
            if (n.Equals(2) || n.Equals(3)) { return true; }
            else if (n <= 1 || (n % 2).Equals(0) || (n % 3).Equals(0)) { return false; }

            int i = 5;
            while (Math.Pow(i, 2) <= n)
            {
                if ((n % i).Equals(0) || (n % (i + 2)).Equals(0)) { return false; }
                i += 6;
            }

            return true;
        }

        public static int GCD(int a, int b)
        {
            int Remainder;

            while (b != 0)
            {
                Remainder = a % b;
                a = b;
                b = Remainder;
            }

            return a;
        }

        public static int LCM(int a, int b)
        {
            return Math.Abs(a * b) / GCD(a, b);
        }
        public static bool Is_Carmichael(int n)
        {
            if (n < 2) { return false; }
            int k = n;
            for (int i = 2; i <= k / i; ++i)
            {
                if (k % i == 0)
                {
                    if ((k / i) % i == 0) { return false; }
                    if ((n - 1) % (i - 1) != 0) { return false; }
                    k /= i;
                    i = 1;
                }
            }
            return k != n && (n - 1) % (k - 1) == 0;
        }

   

        public static byte[] GetHash(byte[] data, String hashAlgorithm = "SHA256")
        {
            byte[] hash;

            switch (hashAlgorithm)
            {
                case "MD5": MD5 md5 = new MD5CryptoServiceProvider(); hash = md5.ComputeHash(data); md5.Dispose(); break;
                case "SHA256": SHA256 sh256 = new SHA256CryptoServiceProvider(); hash = sh256.ComputeHash(data); sh256.Dispose(); break;
                case "SHA512": SHA512 sh512 = new SHA512CryptoServiceProvider(); hash = sh512.ComputeHash(data); sh512.Dispose(); break;
                case "SHA384": SHA384 sh384 = new SHA384CryptoServiceProvider(); hash = sh384.ComputeHash(data); sh384.Dispose(); break;
                default: SHA1 sh1 = new SHA1CryptoServiceProvider(); hash = sh1.ComputeHash(data); sh1.Dispose(); break;
            }

            return hash;
        }
        public static byte[] GetHash(String data, String hashAlgorithm = "SHA256")
        {
            return GetHash(Encoding.ASCII.GetBytes(data), hashAlgorithm);
        }


        public static IEnumerable<T> OfType<T>(IEnumerable e) where T : class
        {
            foreach (object cur in e)
            {
                T val = cur as T;
                if (val != null)
                {
                    yield return val;
                }
            }
        }
        public static bool In<T>(this T item, params T[] items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            return items.Contains(item);
        }

        public static string ResourceReadAllText(string fileName, Encoding encoding)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var resourceStream = currentAssembly.GetManifestResourceStream($"{currentAssembly.GetType().Namespace}.{fileName}");
            using (var reader = new StreamReader(resourceStream, encoding))
            {
                return reader.ReadToEnd();
            }
        }

        public static string[] GetResources()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            //From the assembly where this code lives!
            // this.GetType().Assembly.GetManifestResourceNames()
            //or from the entry point to the application - there is a difference!
            return Assembly.GetExecutingAssembly().GetManifestResourceNames();
        }
        public static Stream GetResourceStream(string resName)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            return currentAssembly.GetManifestResourceStream(currentAssembly.GetName().Name + "." + resName);
        }

        public static string ResourceReadAllText(string resourceName)
        {
            var file = GetResourceStream(resourceName);
            string all = "";

            using (var reader = new StreamReader(file))
            {
                all = reader.ReadToEnd();
            }
            file.Dispose();
            return all;
        }

        public static byte[] ResourceReadAllBytes(string resourceName)
        {
            var file = GetResourceStream(resourceName.Replace("\\","."));
            byte[] all;

            using (var reader = new BinaryReader(file))
            {
                all = reader.ReadBytes((int)file.Length);
            }
            file.Dispose();
            return all;
        }


        public static int  FindString(this string data, string str, int startIndex, int occurence = 1)
        {
            int p = startIndex;
            for (int i = 0; i < occurence && p >= 0; i++)
            {
                p = data.IndexOf(str, startIndex);
                startIndex = p + 1;
            }
            return p;
        }

        public static string ExtractData(this string data, string str, ref int startIndex, int skip)
        {
            string s = "";
            int n = FindString(data, str, startIndex + skip);
            if (n > 0)
            {
                s = data.Substring(startIndex + skip, n - startIndex - skip).Trim();
                startIndex = n + skip;
            }
            return s;
        }
    }
}
