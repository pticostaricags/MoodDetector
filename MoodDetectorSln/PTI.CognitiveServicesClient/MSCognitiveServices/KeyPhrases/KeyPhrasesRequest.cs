using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTI.CognitiveServicesClient.MSCognitiveServices.KeyPhrases
{

    public class KeyPhrasesRequest
    {
        public KeyPhrasesRequestDocument[] documents { get; set; }
    }

    public class KeyPhrasesRequestDocument
    {
        public string language { get; set; }
        public string id { get; set; }
        public string text { get; set; }
    }

}
