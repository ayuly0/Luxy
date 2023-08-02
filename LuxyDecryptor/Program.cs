using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LuxyDecryptor.Components;
using LuxyDecryptor.Components.Algorithms;
using LuxyDecryptor.Components.Utilities;

namespace LuxyDecryptor
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var Banner = @"
▓█████▄ ▓█████  ▄████▄   ██▀███ ▓██   ██▓ ██▓███  ▄▄▄█████▓ ▒█████   ██▀███  
▒██▀ ██▌▓█   ▀ ▒██▀ ▀█  ▓██ ▒ ██▒▒██  ██▒▓██░  ██▒▓  ██▒ ▓▒▒██▒  ██▒▓██ ▒ ██▒
░██   █▌▒███   ▒▓█    ▄ ▓██ ░▄█ ▒ ▒██ ██░▓██░ ██▓▒▒ ▓██░ ▒░▒██░  ██▒▓██ ░▄█ ▒
░▓█▄   ▌▒▓█  ▄ ▒▓▓▄ ▄██▒▒██▀▀█▄   ░ ▐██▓░▒██▄█▓▒ ▒░ ▓██▓ ░ ▒██   ██░▒██▀▀█▄  
░▒████▓ ░▒████▒▒ ▓███▀ ░░██▓ ▒██▒ ░ ██▒▓░▒██▒ ░  ░  ▒██▒ ░ ░ ████▓▒░░██▓ ▒██▒
 ▒▒▓  ▒ ░░ ▒░ ░░ ░▒ ▒  ░░ ▒▓ ░▒▓░  ██▒▒▒ ▒▓▒░ ░  ░  ▒ ░░   ░ ▒░▒░▒░ ░ ▒▓ ░▒▓░
 ░ ▒  ▒  ░ ░  ░  ░  ▒     ░▒ ░ ▒░▓██ ░▒░ ░▒ ░         ░      ░ ▒ ▒░   ░▒ ░ ▒░
 ░ ░  ░    ░   ░          ░░   ░ ▒ ▒ ░░  ░░         ░      ░ ░ ░ ▒    ░░   ░ 
   ░       ░  ░░ ░         ░     ░ ░                           ░ ░     ░     
 ░             ░                 ░ ░                                         
";


            Console.WriteLine(Banner);

            Console.Write($"[i] Path of Folder want to decrypt. Default [C:\\Users]: ");
            var sDir = Console.ReadLine();
            Console.Write("[i] Key: ");
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
            Console.WriteLine("[+] Scanning encrypted files...");

            Common.ParsePath(sDir);
            var filesPath = Common.AllFiles.ToArray();
            try
            {
                Console.WriteLine("[+] Decrypting files...");
                foreach (var file in filesPath)
                {
                    await Task.WhenAll(DecryptFile(file));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine($"[-] Error: {ex}");
            }

            Console.WriteLine("[+] Decrypt Success!");
            Console.ReadKey();
        }

        private static async Task DecryptFile(string file)
        {
            var encryptedData = Convert.FromBase64String(File.ReadAllText(file));
            var decryptedData = Aes256.Decrypt(encryptedData, Settings.EncryptKey, Settings.EncryptIV);
            File.WriteAllBytes(file, decryptedData);
            var path = Path.GetDirectoryName(file);
            var originalFileName = Path.GetFileNameWithoutExtension(file);
            var originalFilePath = path + "\\" + originalFileName;
            File.Move(file, originalFilePath);
            Console.WriteLine($"[+] {file} -> {originalFilePath}");
        }
    }
}
