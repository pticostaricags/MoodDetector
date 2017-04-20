using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MoodDetector.Startup;

namespace MoodDetector.Controllers
{
    public class BaseController : Controller
    {
        protected string FacebookAccessToken
        {
            get
            {
                return GlobalSettings.Facebook_AccessToken;
            }
        }
    }
}