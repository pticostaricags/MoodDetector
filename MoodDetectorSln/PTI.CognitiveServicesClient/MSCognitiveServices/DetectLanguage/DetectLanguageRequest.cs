using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTI.CognitiveServicesClient.MSCognitiveServices.DetectLanguage
{

    public class DetectLanguageRequest
    {
        public DetectLanguageRequestDocument[] documents { get; set; }
    }

    public class DetectLanguageRequestDocument
    {
        public string id { get; set; }
        public string text { get; set; }
    }

}
