using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTI.CognitiveServicesClient.WatsonCognitiveServices.PersonalityInsights
{

    public class PersonalityInsightsResponse
    {
        public int word_count { get; set; }
        public string processed_language { get; set; }
        public Personality[] personality { get; set; }
        public Need[] needs { get; set; }
        public Value[] values { get; set; }
        public Behavior[] behavior { get; set; }
        public object[] warnings { get; set; }
    }

    public class Personality
    {
        public string trait_id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public float percentile { get; set; }
        public Child[] children { get; set; }
    }

    public class Child
    {
        public string trait_id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public float percentile { get; set; }
    }

    public class Need
    {
        public string trait_id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public float percentile { get; set; }
    }

    public class Value
    {
        public string trait_id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public float percentile { get; set; }
    }

    public class Behavior
    {
        public string trait_id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public float percentage { get; set; }
    }

}
