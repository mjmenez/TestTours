using PruebaIngreso.Contract;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PruebaIngreso.Models
{
    public class ClientProvider : IClientProvider
    {
        public string GetTour(string code)
        {
            return getTours(code).Result;
        }

        private async Task<string> getTours(string code)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // string code = "E-E10-PF2SHOW 500"; //"E-U10-DSCVCOVE 404"; //"E-U10-UNILATIN 204";
            string apiUrl = "https://refactored-pancake.free.beeceptor.com/margin/";
            string fullUrl = apiUrl + code;

            if (!string.IsNullOrEmpty(code))
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(fullUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            return responseBody;
                        }
                        else
                        {
                            return "{ margin: 0.0 }";
                        }
                    }
                    catch (HttpRequestException e)
                    {

                        return "{ margin: 0.0 }";
                    }
                }

            return "{ margin: 0.0 }";

        }

    }

    
}