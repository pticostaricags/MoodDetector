
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Facebook;
using Newtonsoft.Json;
using System.Linq.Expressions;
using PTI.CognitiveServicesClient.MSCognitiveServices.KeyPhrases;

namespace MoodDetectorMobile
{

	[Activity(Label = "MyFacebookPostsActivity")]
	public class MyFacebookPostsActivity : Activity
	{
		public static object _lock = new object();
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.MyFacebookPosts);
			FacebookClient fbClient = new FacebookClient(SessionInfo.AccessToken.Token);
			var result = 
				fbClient.Get("me/posts?fields=id,application,caption,created_time,description,from,message,name,status_type,story,type");
			var convertedResult =
				JsonConvert.DeserializeObject<FacebookPostsResult>(result.ToString());
			var lstMyFacebookPosts = FindViewById<ListView>(Resource.Id.lstMyFacebookPosts);
			List<FacebookPost> lstPosts = new List<FacebookPost>();
			List<KeyPhrasesRequestDocument> reqDocs = new List<KeyPhrasesRequestDocument>();
			foreach (var singleItem in convertedResult.data)
			{
				lstPosts.Add(new FacebookPost()
				{
					CreatedDate = DateTimeOffset.Parse(singleItem.created_time),
					Message = singleItem.message,
					PostId = singleItem.id
				});
				if (!String.IsNullOrWhiteSpace(singleItem.message))
				{
					reqDocs.Add(
						new KeyPhrasesRequestDocument()
						{
							id = singleItem.id,
							text = singleItem.message
						}
					);
				}
			}
			KeyPhrasesRequest reqDoc = new KeyPhrasesRequest();
			reqDoc.documents = reqDocs.ToArray();
			PTI.CognitiveServicesClient.CognitiveServicesClient client =
				new PTI.CognitiveServicesClient.CognitiveServicesClient(SessionInfo.MSAccessToken);
			var task = client.GetKeyPhrases(reqDoc).ContinueWith((arg) => 
			{
				var resultKP = arg.Result;
				foreach (var singlePost in lstPosts)
				{
					string[] postKPs = 
						resultKP.documents.Where(p => p.id == singlePost.PostId).Select(p => p.keyPhrases).FirstOrDefault();
					if (postKPs != null && postKPs.Count() > 0)
						singlePost.KeyPhrases = String.Join(",", postKPs);
				}
			});
			task.Wait();
			lstMyFacebookPosts.Adapter = new FacebookPostsAdapter(this, lstPosts);
		}
	}
}
