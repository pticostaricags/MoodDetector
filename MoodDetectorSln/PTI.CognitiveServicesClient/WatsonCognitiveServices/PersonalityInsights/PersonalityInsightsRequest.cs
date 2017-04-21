using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTI.CognitiveServicesClient.WatsonCognitiveServices.PersonalityInsights
{

    public class PersonalityInsightsRequest
    {
        public Contentitem[] contentItems { get; set; }
    }

    public class Contentitem
    {
        public string content { get; set; }
        public string contenttype { get; set; }
        public long created { get; set; }
        public string id { get; set; }
        public string language { get; set; }
    }

}
