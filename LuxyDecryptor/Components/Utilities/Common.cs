using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LuxyDecryptor.Components.Utilities
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
    }
}
