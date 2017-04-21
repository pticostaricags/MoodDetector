using Facebook;
using PTI.CognitiveServicesClient;
using PTI.CognitiveServicesClient.MSCognitiveServices.KeyPhrases;
using PTI.CognitiveServicesClient.MSCognitiveServices.Topics;
using PTI.CognitiveServicesClient.WatsonCognitiveServices;
using PTI.CognitiveServicesClient.WatsonCognitiveServices.PersonalityInsights;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MoodDetector.Controllers
{
    [Authorize]
    public class FacebookController : BaseController
    {
        private FacebookClient CreateFacebookClient()
        {
            return new FacebookClient(base.FacebookAccessToken);
        }

        public async Task<ActionResult> MyPosts(string pageUrl)
        {
            FacebookClient objClient = CreateFacebookClient();
            if (String.IsNullOrWhiteSpace(pageUrl))
            {
                pageUrl = @"me/posts?fields=id,application,caption,created_time,description,from,message,name,status_type,story,type";
            }
            dynamic myPosts = await objClient.GetTaskAsync(pageUrl);
            string myPostsJson = myPosts.ToString();
            Models.Entities.Facebook.MyPosts objMyPosts =
                Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Entities.Facebook.MyPosts>
                (myPostsJson);
            List<Models.Entities.Facebook.MyPosts> lstMyPosts =
                new List<Models.Entities.Facebook.MyPosts>();
            lstMyPosts.Add(objMyPosts);
            return View(lstMyPosts);
        }

        public async Task<ActionResult> AnalyzeAllMyPosts()
        {
            try
            {
                //await ImportMyPosts();
            }
            catch (Exception ex)
            {

            }
            Models.Entities.Facebook.PostsAnalysis objPostanalysis = null;
            using (DataAccess.MoodDetectorContext ctx = new DataAccess.MoodDetectorContext())
            {
                var userPosts = ctx.FacebookUserPosts
                    .Include("FacebookUserPostSentiments")
                    .Include("FacebookUserPostKeyPhrases")
                    .Include("FacebookProfile")
                    .Include("FacebookProfile.FacebookPersonalityInsights")
                    .Include("FacebookProfile.FacebookPersonalityInsights.FacebookPersonalityInsightsPersonalities")
                    .ToList()
                    //.Where(p=>p.FacebookUserPostKeyPhrases.Count() > 0 && p.FacebookUserPostSentiments.Count() > 0).ToList()
                    ;
                objPostanalysis = new Models.Entities.Facebook.PostsAnalysis(userPosts);
            }
            await Task.Yield();
            return View(objPostanalysis);
        }
        // GET: Facebook
        public async Task ImportMyPosts()
        {
            //Check https://developers.facebook.com/docs/graph-api/reference/v2.8/user/feed
            FacebookClient objClient = CreateFacebookClient();
            dynamic myPosts = await objClient.GetTaskAsync(@"me/posts?fields=id,application,caption,created_time,description,from,message,name,status_type,story,type");
            string myPostsJson = myPosts.ToString();
            Models.Entities.Facebook.MyPosts objMyPosts =
                Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Entities.Facebook.MyPosts>
                (myPostsJson);
            List<Models.Entities.Facebook.MyPosts> lstMyPosts =
                new List<Models.Entities.Facebook.MyPosts>();
            lstMyPosts.Add(objMyPosts);
            string userId = objMyPosts.data.First().from.id;
            int count = 0;
            int totalPosts = 0;
            //while (count++ <= 5)
            while (objMyPosts.paging != null && !String.IsNullOrWhiteSpace(objMyPosts.paging.next))
            {
                myPosts = await objClient.GetTaskAsync(objMyPosts.paging.next);
                myPostsJson = myPosts.ToString();
                objMyPosts =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Entities.Facebook.MyPosts>
                    (myPostsJson);
                lstMyPosts.Add(objMyPosts);
                //totalPosts += objMyPosts.data.Where(p => !string.IsNullOrWhiteSpace(p.message)).Count();
            }
            var allNotEmptyMessages = lstMyPosts.SelectMany(p => p.data.Where(x => !String.IsNullOrWhiteSpace(x.message)));
            totalPosts = allNotEmptyMessages.Count();
            PTI.CognitiveServicesClient.MSCognitiveServices.Sentiment.SentimentRequest
                sentimentReq = new PTI.CognitiveServicesClient.MSCognitiveServices.Sentiment.SentimentRequest();
            sentimentReq.documents =
                new PTI.CognitiveServicesClient.MSCognitiveServices.Sentiment.SentimentRequestDocument[totalPosts];
            TopicsRequest topicsReq = new TopicsRequest();
            topicsReq.documents = new TopicsRequestDocument[totalPosts];
            KeyPhrasesRequest keyPhrasesReq = new KeyPhrasesRequest();
            keyPhrasesReq.documents = new KeyPhrasesRequestDocument[totalPosts];
            PersonalityInsightsRequest personalityInsightsReq =
                new PersonalityInsightsRequest();
            personalityInsightsReq.contentItems =
                new Contentitem[totalPosts];
            int iPos = 0;
            foreach (var singlePost in allNotEmptyMessages)
            {
                if (!string.IsNullOrWhiteSpace(singlePost.message))
                {
                    sentimentReq.documents[iPos] = new PTI.CognitiveServicesClient.MSCognitiveServices.Sentiment.SentimentRequestDocument();
                    sentimentReq.documents[iPos].id = singlePost.id;
                    sentimentReq.documents[iPos].text = singlePost.message;

                    topicsReq.documents[iPos] = new TopicsRequestDocument();
                    topicsReq.stopWords = new string[0];
                    topicsReq.topicsToExclude = new string[0];
                    topicsReq.documents[iPos].id = singlePost.id;
                    topicsReq.documents[iPos].text = singlePost.message;

                    keyPhrasesReq.documents[iPos] = new KeyPhrasesRequestDocument();
                    keyPhrasesReq.documents[iPos].id = singlePost.id;
                    keyPhrasesReq.documents[iPos].text = singlePost.message;

                    personalityInsightsReq.contentItems[iPos] =
                        new Contentitem();
                    personalityInsightsReq.contentItems[iPos].content = singlePost.message;
                    personalityInsightsReq.contentItems[iPos].id = singlePost.id;
                    iPos++;
                }
            }
            string MSCognitiveServicesAccessToken =
                System.Configuration.ConfigurationManager.AppSettings[
                GlobalConstants.MSCSTextAnalyticsKey
                ];
            string WatsonPIUserName =
                ConfigurationManager.AppSettings[GlobalConstants.WatsonPIUserName];
            string WatsonPIPassword =
                ConfigurationManager.AppSettings[GlobalConstants.WatsonPIPassword];
            try
            {
                SaveToDatabase(lstMyPosts);
                //await InsertPostsSentiment(sentimentReq, MSCognitiveServicesAccessToken);
                //await InsertPostTopics(topicsReq, MSCognitiveServicesAccessToken);
                //await InsertKeyPhrases(keyPhrasesReq, MSCognitiveServicesAccessToken);
                await InsertPersonalityInsights(personalityInsightsReq, piUsername:WatsonPIUserName, piPassword:WatsonPIPassword, facebookUserId:userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task InsertPersonalityInsights(PersonalityInsightsRequest personalityInsightsReq,
            string piUsername,
            string piPassword,
            string facebookUserId)
        {
            DataAccess.FacebookProfile userProfile = null;
            WatsonCSClient objWatsonCsClient = new WatsonCSClient(piUsername, piPassword);
            int totalPages = (int)Math.Ceiling((decimal)personalityInsightsReq.contentItems.Count() / (decimal)1000);
            for (int iPage = 0; iPage < totalPages; iPage++)
            {
                var reqBatch = personalityInsightsReq.contentItems.Skip(iPage * 1000).Take(1000);
                PersonalityInsightsRequest tmpReq = new PersonalityInsightsRequest();
                tmpReq.contentItems = reqBatch.ToArray();
                var personalityInsights = await objWatsonCsClient.GetProfile(tmpReq);
                using (DataAccess.MoodDetectorContext ctx = new DataAccess.MoodDetectorContext())
                {
                    if (userProfile == null)
                        userProfile = ctx.FacebookProfiles.Where(p => p.ProfileId == facebookUserId).FirstOrDefault();
                    DataAccess.FacebookPersonalityInsight daPI = new DataAccess.FacebookPersonalityInsight();
                    daPI.ProcessedLanguage = personalityInsights.processed_language;
                    daPI.WordCount = personalityInsights.word_count;
                    daPI.FacebookProfileId = userProfile.FacebookProfileId;
                    ctx.FacebookPersonalityInsights.Add(daPI);
                    ctx.SaveChanges();
                    foreach (var singlePersonalityRecord in personalityInsights.personality)
                    {
                        DataAccess.FacebookPersonalityInsightsPersonality daPersonality =
                            new DataAccess.FacebookPersonalityInsightsPersonality();
                        daPersonality.FacebookPersonalityInsightsId = daPI.FacebookPersonalityInsightsId;
                        daPersonality.Category = singlePersonalityRecord.category;
                        daPersonality.Name = singlePersonalityRecord.name;
                        daPersonality.Percentile = singlePersonalityRecord.percentile;
                        daPersonality.TraitId = singlePersonalityRecord.trait_id;
                        ctx.FacebookPersonalityInsightsPersonalities.Add(daPersonality);
                    }
                    ctx.SaveChanges();
                }
            }
            await Task.Yield();
        }

        private async Task InsertKeyPhrases(KeyPhrasesRequest keyPhrasesReq, string MSCognitiveServicesAccessToken)
        {
            PTI.CognitiveServicesClient.CognitiveServicesClient objCSClient =
                new PTI.CognitiveServicesClient.CognitiveServicesClient(MSCognitiveServicesAccessToken);
            int totalPages = (int)Math.Ceiling((decimal)keyPhrasesReq.documents.Count() / (decimal)1000);
            for (int iPage = 0; iPage < totalPages; iPage++)
            {
                var reqBatch = keyPhrasesReq.documents.Skip(iPage * 1000).Take(1000);
                var tmpreq = new PTI.CognitiveServicesClient.MSCognitiveServices.KeyPhrases.KeyPhrasesRequest();
                tmpreq.documents = reqBatch.ToArray();
                var keyPhrasesResponse = await objCSClient.GetKeyPhrases(tmpreq);
                using (MoodDetector.DataAccess.MoodDetectorContext ctx = new MoodDetector.DataAccess.MoodDetectorContext())
                {
                    ctx.Configuration.AutoDetectChangesEnabled = false;
                    foreach (var singleKeyPhraseRecord in keyPhrasesResponse.documents)
                    {
                        if (singleKeyPhraseRecord.keyPhrases != null && singleKeyPhraseRecord.keyPhrases.Count() > 0)
                        {
                            var userPost = ctx.FacebookUserPosts.Where(p => p.PostId == singleKeyPhraseRecord.id).FirstOrDefault();
                            foreach (var singleKeyPhrase in singleKeyPhraseRecord.keyPhrases)
                            {
                                if (!String.IsNullOrWhiteSpace(singleKeyPhrase))
                                {
                                    var objNewKeyPhraseRecord = new DataAccess.FacebookUserPostKeyPhras();
                                    objNewKeyPhraseRecord.FacebookUserPostId = userPost.FacebookUserPostId;
                                    objNewKeyPhraseRecord.KeyPhrases = singleKeyPhrase;
                                    ctx.FacebookUserPostKeyPhrases.Add(objNewKeyPhraseRecord);
                                }
                                else
                                {
                                    //TODO: Add Log to this cases
                                }
                            }
                        }
                    }
                    ctx.SaveChanges();
                }
            }
        }

        private async Task InsertPostTopics(TopicsRequest topicsReq, string MSCognitiveServicesAccessToken)
        {
            PTI.CognitiveServicesClient.CognitiveServicesClient objCSClient =
                new PTI.CognitiveServicesClient.CognitiveServicesClient(MSCognitiveServicesAccessToken);
            int totalPages = (int)Math.Ceiling((decimal)topicsReq.documents.Count() / (decimal)1000);
            for (int iPage = 0; iPage < totalPages; iPage++)
            {
                var reqBatch = topicsReq.documents.Skip(iPage * 1000).Take(1000);
                var tmpreq = new PTI.CognitiveServicesClient.MSCognitiveServices.Topics.TopicsRequest();
                tmpreq.stopWords = new string[0];
                tmpreq.topicsToExclude = new string[0];
                tmpreq.documents = reqBatch.ToArray();
                await objCSClient.GetTopics(tmpreq);
                //var topicsResponse = await objCSClient.GetTopics(tmpreq);
                //using (MoodDetector.DataAccess.MoodDetectorContext ctx =
                //    new DataAccess.MoodDetectorContext())
                //{
                //    ctx.Configuration.AutoDetectChangesEnabled = false;
                //    foreach (var singleTopicRecord in topicsResponse.documents)
                //    {
                //        var userPost = ctx.FacebookUserPosts.Where(p => p.PostId == singleTopicRecord.id).FirstOrDefault();
                //        var objNewSentimentRecord = new DataAccess.FacebookUserPostSentiment();
                //        objNewSentimentRecord.FacebookUserPostId = userPost.FacebookUserPostId;
                //        objNewSentimentRecord.Score = singleTopicRecord.score;
                //        ctx.FacebookUserPostSentiments.Add(objNewSentimentRecord);
                //    }
                //    ctx.SaveChanges();
                //}
            }
        }

        private static async Task InsertPostsSentiment(PTI.CognitiveServicesClient.MSCognitiveServices.Sentiment.SentimentRequest req, string MSCognitiveServicesAccessToken)
        {
            PTI.CognitiveServicesClient.CognitiveServicesClient objCSClient =
                new PTI.CognitiveServicesClient.CognitiveServicesClient(MSCognitiveServicesAccessToken);
            int totalPages = (int)Math.Ceiling((decimal)req.documents.Count() / (decimal)1000);
            for (int iPage = 0; iPage < totalPages; iPage++)
            {
                var reqBatch = req.documents.Skip(iPage * 1000).Take(1000);
                var tmpreq = new PTI.CognitiveServicesClient.MSCognitiveServices.Sentiment.SentimentRequest();
                tmpreq.documents = reqBatch.ToArray();
                var sentimentResponse = await objCSClient.GetSentiment(tmpreq);
                using (MoodDetector.DataAccess.MoodDetectorContext ctx =
                    new DataAccess.MoodDetectorContext())
                {
                    ctx.Configuration.AutoDetectChangesEnabled = false;
                    foreach (var singleSentimentRecord in sentimentResponse.documents)
                    {
                        var userPost = ctx.FacebookUserPosts.Where(p => p.PostId == singleSentimentRecord.id).FirstOrDefault();
                        var objNewSentimentRecord = new DataAccess.FacebookUserPostSentiment();
                        objNewSentimentRecord.FacebookUserPostId = userPost.FacebookUserPostId;
                        objNewSentimentRecord.Score = singleSentimentRecord.score;
                        ctx.FacebookUserPostSentiments.Add(objNewSentimentRecord);
                    }
                    ctx.SaveChanges();
                }
            }
        }

        private static void SaveToDatabase(List<Models.Entities.Facebook.MyPosts> lstMyPosts)
        {
            using (MoodDetector.DataAccess.MoodDetectorContext ctx = new DataAccess.MoodDetectorContext())
            {
                foreach (var singleUserPostPage in lstMyPosts)
                {
                    foreach (var singleUserPost in singleUserPostPage.data)
                    {
                        MoodDetector.DataAccess.FacebookProfile objProfile =
                            ctx.FacebookProfiles.Where(p => p.ProfileId == singleUserPost.from.id).FirstOrDefault();
                        if (objProfile == null)
                        {
                            objProfile = new DataAccess.FacebookProfile()
                            {
                                DateRecordCreated = DateTime.UtcNow,
                                ProfileId = singleUserPost.from.id,
                                Username = singleUserPost.from.name
                            };
                            ctx.FacebookProfiles.Add(objProfile);
                            ctx.SaveChanges();
                        }
                        MoodDetector.DataAccess.FacebookUserPost objUserPost =
                            ctx.FacebookUserPosts.Where(p => p.PostId == singleUserPost.id).FirstOrDefault();
                        if (objUserPost == null)
                        {
                            objUserPost = new DataAccess.FacebookUserPost();
                            objUserPost.DatePosted = singleUserPost.created_time.ToUniversalTime();
                            objUserPost.Caption = singleUserPost.caption;
                            objUserPost.Description = singleUserPost.description;
                            objUserPost.From = singleUserPost.from.name;
                            objUserPost.Message = singleUserPost.message;
                            objUserPost.Name = singleUserPost.name;
                            objUserPost.PostId = singleUserPost.id;
                            objUserPost.FacebookProfileId = objProfile.FacebookProfileId;
                            objUserPost.Status_Type = singleUserPost.status_type;
                            objUserPost.Story = singleUserPost.story;
                            objUserPost.Type = singleUserPost.type;
                            ctx.FacebookUserPosts.Add(objUserPost);
                        }
                    }
                    ctx.SaveChanges();
                }
            }
        }
    }
}