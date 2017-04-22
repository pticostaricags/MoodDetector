using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTI.CognitiveServicesClient.MSCognitiveServices.DetectLanguage
{

    public class DetectLanguageResponse
    {
        public DetectLanguageResponseDocument[] documents { get; set; }
        public DetectLanguageResponseError[] errors { get; set; }
    }

    public class DetectLanguageResponseDocument
    {
        public string id { get; set; }
        public Detectedlanguage[] detectedLanguages { get; set; }
    }

    public class Detectedlanguage
    {
        public string name { get; set; }
        public string iso6391Name { get; set; }
        public float score { get; set; }
    }

    public class DetectLanguageResponseError
    {
        public string id { get; set; }
        public string message { get; set; }
    }

}
