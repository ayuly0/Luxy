using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LuxyDecryptor.Components.Utilities
{
    internal static class Common
    {
        public static string[] FilesTree(string sDir)
        {
            List<string> filesPath = new List<string>();
            try
            {
                foreach (string file in Directory.EnumerateFiles(sDir, "*.*", SearchOption.AllDirectories)
                .Where(s => Settings.EncryptExtensionList.Any(ext => ext == Path.GetExtension(s).Replace(".", ""))))
                {
                    filesPath.Add(file);
                }
            }
            catch (Exception ex)
            {

            }
            string[] lFilesPath = filesPath.ToArray();
            return lFilesPath;
        }
    }
}
