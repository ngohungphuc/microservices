using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;

namespace ApiGatewayMock
{
    public class LoyalProgramClient
    {
        private string hostName;
        private static Policy exponentialRetryPolicy = Policy.Handle<Exception>().WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt)));
        private static Policy circuitBreaker = Policy.Handle<Exception>().CircuitBreaker(5, TimeSpan.FromMinutes(3));

        public async Task<LoyaltyProgramUser> QueryUser(int userId)
        {
            var userResource = $"/users/{userId}";
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"http://{this.hostName}");
                var response = await httpClient.GetAsync(userResource);

                ThrowOnTransientFailure(response);
                return JsonConvert.DeserializeObject<LoyaltyProgramUser>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<HttpResponseMessage> RegisterUser(LoyaltyProgramUser newUser)
        {
            return await exponentialRetryPolicy.ExecuteAsync(() => DoRegisterUser(newUser));
        }

        private async Task<HttpResponseMessage> DoRegisterUser(LoyaltyProgramUser newUser)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"http://{this.hostName}");
                var response = await httpClient.PostAsync("/users/", new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json"));

                ThrowOnTransientFailure(response);
                return response;
            }
        }

        public async Task<LoyaltyProgramUser> UpdateUser(LoyaltyProgramUser user)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"http://{this.hostName}");
                var response = await httpClient.PutAsync($"/users/{user.Id}",
                    new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

                ThrowOnTransientFailure(response);
                return JsonConvert.DeserializeObject<LoyaltyProgramUser>(await response.Content.ReadAsStringAsync());
            }
        }

        private static void ThrowOnTransientFailure(HttpResponseMessage response)
        {
            if (((int)response.StatusCode) < 200 || ((int)response.StatusCode) > 499) throw new Exception(response.StatusCode.ToString());
        }
    }

    //public class LoyalProgramClient
    //{
    //    private string hostName;
    //    private static Policy exponentialRetryPolicy = Policy.Handle<Exception>().WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt)));

    //    public async Task<LoyaltyProgramUser> QueryUser(int userId)
    //    {
    //        var userResource = $"/users/{userId}";
    //        using (var httpClient = new HttpClient())
    //        {
    //            httpClient.BaseAddress = new Uri($"http://{this.hostName}");
    //            var response = await httpClient.GetAsync(userResource);

    //            ThrowOnTransientFailure(response);
    //            return JsonConvert.DeserializeObject<LoyaltyProgramUser>(await response.Content.ReadAsStringAsync());
    //        }
    //    }

    //    public async Task<LoyaltyProgramUser> RegisterUser(LoyaltyProgramUser newUser)
    //    {
    //        using (var httpClient = new HttpClient())
    //        {
    //            httpClient.BaseAddress = new Uri($"http://{this.hostName}");
    //            var response = await httpClient.PostAsync("/users/",
    //                new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json"));

    //            ThrowOnTransientFailure(response);
    //            return JsonConvert.DeserializeObject<LoyaltyProgramUser>(await response.Content.ReadAsStringAsync());
    //        }
    //    }

    //    public async Task<LoyaltyProgramUser> UpdateUser(LoyaltyProgramUser user)
    //    {
    //        using (var httpClient = new HttpClient())
    //        {
    //            httpClient.BaseAddress = new Uri($"http://{this.hostName}");
    //            var response = await httpClient.PutAsync($"/users/{user.Id}",
    //                new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

    //            ThrowOnTransientFailure(response);
    //            return JsonConvert.DeserializeObject<LoyaltyProgramUser>(await response.Content.ReadAsStringAsync());
    //        }
    //    }

    //    private static void ThrowOnTransientFailure(HttpResponseMessage response)
    //    {
    //        if (((int)response.StatusCode) < 200 || ((int)response.StatusCode) > 499) throw new Exception(response.StatusCode.ToString());
    //    }
    //}
}
