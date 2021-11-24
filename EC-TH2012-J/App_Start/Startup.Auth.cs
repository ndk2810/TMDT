using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using EC_TH2012_J.Providers;
using EC_TH2012_J.Models;
using Microsoft.AspNet.Identity.Owin;

namespace EC_TH2012_J
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager, CookieAuthenticationDefaults.AuthenticationType))
                }
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider("self"),
                AuthorizeEndpointPath = new PathString("/api/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions); // khoa api Bearer
            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");
            // Twitter : Create a new application
        // https://dev.twitter.com/apps
        //if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("TwitterConsumerKey")))
        //{
        //    var twitterOptions = new Microsoft.Owin.Security.Twitter.TwitterAuthenticationOptions
        //    {
        //        ConsumerKey = ConfigurationManager.AppSettings.Get("TwitterConsumerKey"),
        //        ConsumerSecret = ConfigurationManager.AppSettings.Get("TwitterConsumerSecret"),
        //        Provider = new Microsoft.Owin.Security.Twitter.TwitterAuthenticationProvider
        //        {
        //            OnAuthenticated = (context) =>
        //            {
        //                context.Identity.AddClaim(new System.Security.Claims.Claim("urn:twitter:access_token", context.AccessToken, XmlSchemaString, "Twitter"));
        //                return Task.FromResult(0);
        //            }
        //        }
        //    };
 
        //    app.UseTwitterAuthentication(twitterOptions);
        //}
 
        //// Facebook : Create New App
        //// https://developers.facebook.com/apps
        //if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("FacebookAppId")))
        //{
        //    var facebookOptions = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions
        //    {
        //        AppId = ConfigurationManager.AppSettings.Get("FacebookAppId"),
        //        AppSecret = ConfigurationManager.AppSettings.Get("FacebookAppSecret"),
        //        Provider = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationProvider
        //        {
        //            OnAuthenticated = (context) =>
        //            {
        //                context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:access_token", context.AccessToken, XmlSchemaString, "Facebook"));
        //                foreach (var x in context.User)
        //                {
        //                    var claimType = string.Format("urn:facebook:{0}", x.Key);
        //                    string claimValue = x.Value.ToString();
        //                    if (!context.Identity.HasClaim(claimType, claimValue))
        //                        context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, XmlSchemaString, "Facebook"));
 
        //                }
        //                return Task.FromResult(0);
        //            }
        //        }
        //    };
        //    facebookOptions.Scope.Add("email");
        //    app.UseFacebookAuthentication(facebookOptions);

            app.UseFacebookAuthentication(
               appId: "1402925696618204",
               appSecret: "33216937f762b324a9b4ec88c55bffa3");

            app.UseGoogleAuthentication(
             clientId: "389419646196-se6iu131uaf16qj5sqmtbd17nthkitvi.apps.googleusercontent.com",
             clientSecret: "d0mT-bSvKjcRC-isA18Aq56A");
        }
    }
}