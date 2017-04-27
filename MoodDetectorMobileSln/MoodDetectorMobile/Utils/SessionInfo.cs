using System;
using Xamarin.Facebook;

namespace MoodDetectorMobile
{
	public class SessionInfo
	{
		
		static SessionInfo()
		{
			MSAccessToken = "REPLACE WITH YOUR OWN";
		}

		public static AccessToken AccessToken { get; set; }
		public static string MSAccessToken { get; set; }
	}
}
