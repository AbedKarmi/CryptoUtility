using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoUtility;

// WebRequest—in its HTTP-specific implementation, HttpWebRequest—represents the original way to consume HTTP requests in .NET Framework.
// WebClient provides a simple but limited wrapper around HttpWebRequest.
// HttpClient is the new and improved way of doing HTTP requests and posts, having arrived with .NET Framework 4.5
public static class MyWeb
{
    public static async Task HTTPReadPageAsync(string url, string userAgent, Action<string> result)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", userAgent);
        var content = await client.GetStringAsync(url);

        result.Invoke(content);
    }

    public static string WebClientReadPage(string url, string userAgent)
    {
#pragma warning disable SYSLIB0014 // Type or member is obsolete
        using var client = new WebClient();
#pragma warning restore SYSLIB0014 // Type or member is obsolete
        client.Headers.Add("User-Agent", userAgent);
        var content = client.DownloadString(url);

        return content;
    }

    public static void WebClientReadPageAsync(string url, string userAgent, Action<string> result)
    {
#pragma warning disable SYSLIB0014 // Type or member is obsolete
        using WebClient client = new();
#pragma warning restore SYSLIB0014 // Type or member is obsolete
        client.Headers.Add("User-Agent", userAgent);

        client.DownloadStringCompleted += (sender, e) => { result.Invoke(e.Result); };

        client.DownloadStringAsync(new Uri(url));
    }

    public static string WebRequestReadPage(string url, string userAgent)
    {
        var html = string.Empty;

#pragma warning disable SYSLIB0014 // Type or member is obsolete
        var request = (HttpWebRequest)WebRequest.Create(url);
#pragma warning restore SYSLIB0014 // Type or member is obsolete
        request.UserAgent = userAgent;

        using (var response = (HttpWebResponse)request.GetResponse())
        using (var stream = response.GetResponseStream())
        using (var reader = new StreamReader(stream))
        {
            html = reader.ReadToEnd();
        }

        return html;
    }
}