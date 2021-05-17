using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryptoUtility
{
    // WebRequest—in its HTTP-specific implementation, HttpWebRequest—represents the original way to consume HTTP requests in .NET Framework.
    // WebClient provides a simple but limited wrapper around HttpWebRequest.
    // HttpClient is the new and improved way of doing HTTP requests and posts, having arrived with .NET Framework 4.5
    public static class MyWeb
    {
        public static async Task HTTPReadPageAsync(string url,string userAgent,Action<string> result)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            var content = await client.GetStringAsync(url);

            result.Invoke(content);
        }

        public static string WebClientReadPage(string url, string userAgent)
        {
            using var client = new WebClient();
            client.Headers.Add("User-Agent", userAgent);
            string content = client.DownloadString(url);

            return content;
        }

        public static void WebClientReadPageAsync(string url, string userAgent,Action<string> result)
        {
            using var client = new WebClient();
            client.Headers.Add("User-Agent", userAgent);

            client.DownloadStringCompleted += (sender, e) =>
            {
                result.Invoke(e.Result);
            };

            client.DownloadStringAsync(new Uri(url));
        }

        public static string WebRequestReadPage(string url, string userAgent)
        {
            string html = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = userAgent;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            return html;
        }

    }
}
