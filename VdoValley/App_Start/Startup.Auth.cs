using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using VdoValley.Models;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.MicrosoftAccount;
using Owin.Security.Providers.Yahoo;
using Owin.Security.Providers.LinkedIn;

namespace VdoValley
{
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
            app.UseMicrosoftAccountAuthentication(new MicrosoftAccountAuthenticationOptions()
            {
                ClientId = "000000004C151C4E",
                ClientSecret = "crOaHJkumg-g0J56nh95GB0QVn6YonED",
                Scope = { "wl.basic", "wl.emails" } // without defining this property, the email in ExternalLoginCallback is null
            });

            app.UseTwitterAuthentication(
               consumerKey: "v6tRKRLiFVVTH7ct71YNkcgvE",
               consumerSecret: "8aQnR6R5WraxYxnoDUNhXBCzGNFCQczkQ5ijmsnt3V2N5X31dk");

            app.UseFacebookAuthentication(new FacebookAuthenticationOptions
            {
                AppId = "1576894379240839",
                AppSecret = "a8819f2ce74b92e436b1e96607ef0058",
                Scope = { "email", "public_profile" } // without defining this property, the email in ExternalLoginCallback is null
            });
            
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "406059334990-cm8so11jaleocbnkt04j1ekqgnpou50d.apps.googleusercontent.com",
                ClientSecret = "-YWFyprz2g07h-2QUfnBXRLt"
            });

            var options = new YahooAuthenticationOptions
            {
                ConsumerKey = "dj0yJmk9YU1WdGhtaFhueXhLJmQ9WVdrOVlWUXplRlUzTldVbWNHbzlNQS0tJnM9Y29uc3VtZXJzZWNyZXQmeD1hMw--",
                ConsumerSecret = "4739386db9b11509feadeeb178ee46af10077c2d",
                Provider = new YahooAuthenticationProvider
                {
                    OnAuthenticated = async context =>
                    {
                        // Retrieve the OAuth access token to store for subsequent API calls
                        string accessToken = context.AccessToken;
                        string accessTokenSecret = context.AccessTokenSecret;

                        // Retrieve the user ID
                        string yahooUserName = context.UserId;

                        // You can even retrieve the full JSON-serialized user
                        var serializedUser = context.User;
                        var serializedEmail = context.Email;
                    }
                }
            };

            app.UseYahooAuthentication(options);

            app.UseLinkedInAuthentication(
                clientId: "77b6gb2j6qzwer",
                clientSecret: "bll2q33FHVjDS1uZ");
            }
    }
}