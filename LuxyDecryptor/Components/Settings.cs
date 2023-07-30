using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LuxyDecryptor.Components.Algorithms;

namespace LuxyDecryptor.Components
{
    internal static class Settings
    {
        internal static byte[] EncryptKey { get; set; }
        internal static byte[] EncryptIV { get; set; }
        internal static string EncryptExtension { get; set; }
        internal static string[] EncryptExtensionList { get; set; }

        static Settings()
        {
            var key = "";
            var iv = "";

            var encryptedEncryptExtension = "";

            // ---------------- Initialise variables ----------------

            var conv_key = Convert.FromBase64String(key);
            var conv_iv = Convert.FromBase64String(iv);

            EncryptExtension = Decrypt(Convert.FromBase64String(encryptedEncryptExtension), conv_key, conv_iv);
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
