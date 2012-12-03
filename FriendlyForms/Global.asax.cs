using System;
using System.Data.Entity;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using DataLayerContext.Migrations;
using FriendlyForms.Authentication;


namespace FriendlyForms
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        private IFormsAuthentication _formsAuthentication;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataLayerContext.SplitContext, Configuration>());

        }

        //Authentication logic
        public override void Init()
        {
            _formsAuthentication = (IFormsAuthentication)DependencyResolver.Current.GetService(typeof(IFormsAuthentication));
            this.AuthenticateRequest += AuthenticateRequestFriendlyForm;
            this.PostAuthenticateRequest += PostAuthenticateRequestFriendlyForm;
            base.Init();
        }
        static void AuthenticateRequestFriendlyForm(object sender, EventArgs e)
        {
        }
        private void PostAuthenticateRequestFriendlyForm(object sender, EventArgs e)
        {
            var authCookie = this.Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (IsValidAuthCookie(authCookie))
            {
                var ticket = _formsAuthentication.Decrypt(authCookie.Value);
                var mashId = new FriendlyIdentity(ticket);
                this.Context.User = new GenericPrincipal(mashId, null);

                // Reset cookie for a sliding expiration.
                _formsAuthentication.SetAuthCookie(this.Context, ticket);
            }
        }

        private static bool IsValidAuthCookie(HttpCookie authCookie)
        {
            return authCookie != null && !String.IsNullOrEmpty(authCookie.Value);
        }
    }
}