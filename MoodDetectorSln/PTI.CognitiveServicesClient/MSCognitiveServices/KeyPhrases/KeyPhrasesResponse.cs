using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTI.CognitiveServicesClient.MSCognitiveServices.KeyPhrases
{

    public class KeyPhrasesResponse
    {
        public KeyPhrasesResponseDocument[] documents { get; set; }
        public object[] errors { get; set; }
    }

    public class KeyPhrasesResponseDocument
    {
        public string[] keyPhrases { get; set; }
        public string id { get; set; }
    }

}
