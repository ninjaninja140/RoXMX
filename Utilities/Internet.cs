using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace RoXMX.Utilities
{
    public class Internet
    {
        public static bool CheckForInternetConnection(int timeoutMs = 10000, string url = null)
        {
            try
            {
                url ??= CultureInfo.InstalledUICulture switch
                {
                    { Name: var n } when n.StartsWith("fa") =>
                        "http://www.aparat.com",
                    { Name: var n } when n.StartsWith("zh") =>
                        "http://www.baidu.com",
                    _ =>
                        "http://www.gstatic.com/generate_204",
                };

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;
                request.Timeout = timeoutMs;
                using (var response = (HttpWebResponse)request.GetResponse())
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<string> GetLatestRojoVersion()
        {

            using HttpClient client = new();
            try
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd(
                    // Couldn't get it to work with the default user agent ¯\_(ツ)_/¯
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:133.0) Gecko/20100101 Firefox/133.0"
                );
                HttpResponseMessage response = await client.GetAsync(Configuration.RojoLatest);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(responseBody);
                string maxVersion = json["crate"]?["max_version"]?.ToString();

                return maxVersion;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "unknown";
            }
        }
    }
}
