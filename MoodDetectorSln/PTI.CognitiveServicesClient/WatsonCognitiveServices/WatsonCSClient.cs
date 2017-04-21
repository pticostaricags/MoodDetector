using Newtonsoft.Json;
using PTI.CognitiveServicesClient.WatsonCognitiveServices.PersonalityInsights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PTI.CognitiveServicesClient.WatsonCognitiveServices
{
    public class WatsonCSClient
    {
        private string Username { get; set; } = string.Empty;
        private string Password { get; set; } = string.Empty;
        public WatsonCSClient(string pUsername, string pPssword )
        {
            this.Username = pUsername;
            this.Password = pPssword;
        }

        public async Task<PersonalityInsightsResponse> GetProfile(PersonalityInsightsRequest request)
        {
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            string getProfileUrl = "https://gateway.watsonplatform.net/personality-insights/api/v3/profile?version=" + currentDate;
            string token = await this.GetTokenForPersonalityInsights();
            string jsonRequest = JsonConvert.SerializeObject(request);
            HttpClient objHttpClient = new HttpClient();
            StringContent objStrContent =
                new StringContent(jsonRequest);
            objStrContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            objStrContent.Headers.Add("X-Watson-Authorization-Token", token);
            var response = await objHttpClient.PostAsync(getProfileUrl, objStrContent);
            string jsonResult = await response.Content.ReadAsStringAsync();
            PersonalityInsightsResponse objResult =
                JsonConvert.DeserializeObject<PersonalityInsightsResponse>(jsonResult);
            return objResult;
        }
        public async Task<string> GetTokenForPersonalityInsights()
        {
            System.Net.Http.HttpClientHandler objHttpClientHandler =
                new System.Net.Http.HttpClientHandler();
            objHttpClientHandler.Credentials =
                new System.Net.NetworkCredential(
                    userName: this.Username,
                    password: this.Password);
            //Check https://www.ibm.com/watson/developercloud/doc/common/getting-started-tokens.html
            string authUrl = "https://gateway.watsonplatform.net/authorization/api/v1/token";
            authUrl += "?url=https://gateway.watsonplatform.net/personality-insights/api";
            System.Net.Http.HttpClient objClient =
                new System.Net.Http.HttpClient(objHttpClientHandler);
            var response = await objClient.GetAsync(authUrl);
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
