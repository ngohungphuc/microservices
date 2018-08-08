using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApiGatewayMock
{
    public class LoyalProgramClient
    {
        private string hostName;

        public async Task<LoyaltyProgramUser> RegisterUser(LoyaltyProgramUser newUser)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"http://{this.hostName}");
                var response = await httpClient.PostAsync("/users/",
                    new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json"));

                ThrowOnTransientFailure(response);
                return JsonConvert.DeserializeObject<LoyaltyProgramUser>(await response.Content.ReadAsStringAsync());
            }
        }
        private static void ThrowOnTransientFailure(HttpResponseMessage response)
        {
            if (((int)response.StatusCode) < 200 || ((int)response.StatusCode) > 499) throw new Exception(response.StatusCode.ToString());
        }
    }
}
