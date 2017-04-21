using Microsoft.VisualStudio.TestTools.UnitTesting;
using PTI.CognitiveServicesClient.WatsonCognitiveServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodDetector.Tests.APIs
{
    [TestClass]
    public class WatsonTest
    {
        private const string PIUsername = "REPLACE WITH YOUR OWN";
        private const string PIPassword = "REPLACE WITH YOUR OWN";
        [TestMethod]
        public async Task PersonalityInsightsAuth()
        {
            WatsonCSClient
                watsonClient =
                new WatsonCSClient(PIUsername,
                PIPassword);
            var token = await watsonClient.GetTokenForPersonalityInsights();
            await Task.Yield();
        }
    }
}
