using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Facebook;
using System;
using Android.Runtime;
using Xamarin.Facebook.Login;
using System.Collections.Generic;

namespace MoodDetectorMobile
{
	[Activity(Label = "MoodDetectorMobile", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		ICallbackManager callbackManager;
		FacebookCallback<LoginResult> loginCallback = null;

		protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			callbackManager.OnActivityResult (requestCode, (int)resultCode, data);
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			FacebookSdk.SdkInitialize(this.ApplicationContext);
			callbackManager = CallbackManagerFactory.Create ();
			loginCallback = new FacebookCallback<LoginResult>
			{
				HandleSuccess = loginResult =>
				{
					SessionInfo.AccessToken = loginResult.AccessToken;
					StartActivity(typeof(MyFacebookPostsActivity));
				},
				HandleCancel = () =>
				{
					bool b = true;
				},
				HandleError = loginError =>
				{
					int z = 0;
				}
			};

			LoginManager.Instance.RegisterCallback (callbackManager, loginCallback);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);
			Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
			btnLogin.Click += BtnLogin_Click;
		}

		void BtnLogin_Click(object sender, System.EventArgs e)
		{
			List<string> permissions = new List<string>()
			{
				"user_posts"
			};
			LoginManager.Instance.LogInWithReadPermissions(this, permissions);
		}
	}

	class FacebookCallback<TResult> : Java.Lang.Object, IFacebookCallback where TResult : Java.Lang.Object
	{
		public Action HandleCancel { get; set; }
		public Action<FacebookException> HandleError { get; set; }
		public Action<TResult> HandleSuccess { get; set; }

		public void OnCancel()
		{
			var c = HandleCancel;
			if (c != null)
				c();
		}

		public void OnError(FacebookException error)
		{
			var c = HandleError;
			if (c != null)
				c(error);
		}

		public void OnSuccess(Java.Lang.Object result)
		{
			var c = HandleSuccess;
			if (c != null)
				c(result.JavaCast<TResult>());
		} 
	}

}

