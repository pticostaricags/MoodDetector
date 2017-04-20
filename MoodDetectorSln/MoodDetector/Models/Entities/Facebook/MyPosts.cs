using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoodDetector.Models.Entities.Facebook
{

    public class MyPosts
    {
        public Datum[] data { get; set; }
        public Paging paging { get; set; }
    }

    public class Paging
    {
        public string previous { get; set; }
        public string next { get; set; }
    }

    public class Datum
    {
        public string id { get; set; }
        public string caption { get; set; }
        public DateTime created_time { get; set; }
        public string description { get; set; }
        public From from { get; set; }
        public string name { get; set; }
        public string status_type { get; set; }
        public string story { get; set; }
        public string type { get; set; }
        public string message { get; set; }
        public Application application { get; set; }
    }

    public class From
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Application
    {
        public string category { get; set; }
        public string link { get; set; }
        public string name { get; set; }
        public string _namespace { get; set; }
        public string id { get; set; }
    }

}