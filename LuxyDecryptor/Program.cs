using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuxyDecryptor.Components;
using LuxyDecryptor.Components.Algorithms;
using LuxyDecryptor.Components.Utilities;

namespace LuxyDecryptor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write($"Path of Folder want to decrypt. Default [C:\\Users\\{Environment.UserName}]: ");
            var sDir = Console.ReadLine();
            Console.Write("Key: ");
            var KeyAndIv = Console.ReadLine().Split('.');

            List<string> eList = new List<string>();
            eList.Add(Settings.EncryptExtension);
            Settings.EncryptExtensionList = eList.ToArray();

            Settings.EncryptKey = Convert.FromBase64String(KeyAndIv[0]);
            Settings.EncryptIV = Convert.FromBase64String(KeyAndIv[1]);

            if (sDir == "")
            {
                sDir = $"C:\\Users\\{Environment.UserName}";
            }
            var filesPath = Common.FilesTree(sDir);
            try
            {
                foreach (var file in filesPath)
                {
                    var encryptedData = Convert.FromBase64String(File.ReadAllText(file));
                    var decryptedData = Aes256.Decrypt(encryptedData, Settings.EncryptKey, Settings.EncryptIV);
                    File.WriteAllBytes(file, decryptedData);
                    var path = Path.GetDirectoryName(file);
                    var originalFileName = Path.GetFileNameWithoutExtension(file);
                    var originalFilePath = path + "\\" + originalFileName;
                    File.Move(file, originalFilePath);
                    Console.WriteLine($"Found {file} -> Decrypt to {originalFilePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                while (true)
                {

                }
            }

        }
    }
}
