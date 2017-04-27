using PTI.CognitiveServicesClient.MSCognitiveServices.Sentiment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PTI.CognitiveServicesClient.MSCognitiveServices.Topics;
using PTI.CognitiveServicesClient.MSCognitiveServices.KeyPhrases;
using PTI.CognitiveServicesClient.MSCognitiveServices.DetectLanguage;
using Newtonsoft.Json;

namespace PTI.CognitiveServicesClient
{
    public class CognitiveServicesClient
    {
        public CognitiveServicesClient(string pAccessToken)
        {
            AccessToken = pAccessToken;
        }
        internal static string AccessToken { get; set; }
        public async Task<SentimentResponse> GetSentiment(MSCognitiveServices.Sentiment.SentimentRequest request)
        {
            //Check https://westus.dev.cognitive.microsoft.com/docs/services/TextAnalytics.V2.0/operations/56f30ceeeda5650db055a3c9
            //Check https://docs.microsoft.com/en-us/azure/cognitive-services/cognitive-services-text-analytics-api-migration
            string getSentimentUrl = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment";
            System.Net.Http.HttpClient objHttpClient =
                new System.Net.Http.HttpClient();
            //Check http://stackoverflow.com/questions/14627399/setting-authorization-header-of-httpclient
            string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            System.Net.Http.StringContent objStrcontent =
                new StringContent(jsonRequest);
            objStrcontent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            objStrcontent.Headers.Add("Ocp-Apim-Subscription-Key", AccessToken);
            var resultTsk = objHttpClient.PostAsync(getSentimentUrl, objStrcontent);
			resultTsk.Wait();
            var resultStringTsk = resultTsk.Result.Content.ReadAsStringAsync();
			resultStringTsk.Wait();
			var resultString = resultStringTsk.Result;
            SentimentResponse objSentimentResponse =
                Newtonsoft.Json.JsonConvert.DeserializeObject<SentimentResponse>(resultString);
            return objSentimentResponse;
        }

        public async Task<KeyPhrasesResponse> GetKeyPhrases(KeyPhrasesRequest request)
        {
            string getKeyPhrasesUrl = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/keyPhrases";
            HttpClient objHttpClient =
                new HttpClient();
            string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            StringContent objStrContent = new StringContent(jsonRequest);
            objStrContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            objStrContent.Headers.Add("Ocp-Apim-Subscription-Key", AccessToken);
            var resultTsk = objHttpClient.PostAsync(getKeyPhrasesUrl, objStrContent);
			resultTsk.Wait();
			var resultStringTsk = resultTsk.Result.Content.ReadAsStringAsync();
			resultStringTsk.Wait();
			var resultString = resultStringTsk.Result;
            KeyPhrasesResponse objKeyPhrasesResponse =
                Newtonsoft.Json.JsonConvert.DeserializeObject<KeyPhrasesResponse>(resultString);
            return objKeyPhrasesResponse;
        }
    }
}
