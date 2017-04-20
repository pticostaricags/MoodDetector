using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using MoodDetector.Models;
using Microsoft.Owin.Security.Facebook;
using System.Web;
using System.Threading.Tasks;

namespace MoodDetector
{
    public class GlobalConstants
    {
        public const string FacebookAppApplicationId = "FacebookApp-ApplicationId";
        public const string FacebookAppSecretKey = "FacebookApp-SecretKey";

        public const string MSCSTextAnalyticsKey = "MSCS-TextAnalyticsKey";
    }
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            string fbApplicationId =
                System.Configuration.ConfigurationManager.AppSettings
                [GlobalConstants.FacebookAppApplicationId];
            string fbApplicationSecret =
                System.Configuration.ConfigurationManager.AppSettings
                [GlobalConstants.FacebookAppSecretKey];

            FacebookAuthenticationOptions fbAuthOptions =
                new FacebookAuthenticationOptions()
                {
                    Provider =
                    new FacebookAuthenticationProvider()
                    {
                        OnAuthenticated = (context) =>
                        {
                            // All data from facebook in this object. 
                            var rawUserObjectFromFacebookAsJson = context.User;

                            // Only some of the basic details from facebook 
                            // like id, username, email etc are added as claims.
                            // But you can retrieve any other details from this
                            // raw Json object from facebook and add it as claims here.
                            // Subsequently adding a claim here will also send this claim
                            // as part of the cookie set on the browser so you can retrieve
                            // on every successive request. 
                            //context.Identity.AddClaim(...);
                            GlobalSettings.Facebook_AccessToken = context.AccessToken;
                            //context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:access_token", context.AccessToken, ClaimValueTypes.String, "Facebook"));
                            return Task.FromResult(0);
                        },
                    },
                    AppId = fbApplicationId,
                    AppSecret = fbApplicationSecret
                };
            fbAuthOptions.Scope.Add("user_posts");
            app.UseFacebookAuthentication(
                fbAuthOptions);

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }

        internal class GlobalSettings
        {
            public static string Facebook_AccessToken { get; internal set; }
        }
    }
}