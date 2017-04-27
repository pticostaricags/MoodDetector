using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTI.CognitiveServicesClient.MSCognitiveServices.Topics
{

    public class TopicsRequest
    {
        public string[] stopWords { get; set; }
        public string[] topicsToExclude { get; set; }
        public TopicsRequestDocument[] documents { get; set; }
    }

    public class TopicsRequestDocument
    {
        public string id { get; set; }
        public string text { get; set; }
    }

}
