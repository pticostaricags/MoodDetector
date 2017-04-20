using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTI.CognitiveServicesClient.MSCognitiveServices.Sentiment
{

    public class SentimentResponse
    {
        public SentimentResponseDocument[] documents { get; set; }
        public object[] errors { get; set; }
    }

    public class SentimentResponseDocument
    {
        public float score { get; set; }
        public string id { get; set; }
    }

}
