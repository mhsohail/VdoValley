using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VdoValley.Infrastructure;

namespace VdoValley.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            // enable web api in mvc application.
            // also add routeTemplate: "api/{controller}/{action}/{id}" to WebApiConfig.cs
            GlobalConfiguration.Configure(WebApiConfig.Register);
            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //protected void Application_Error(Object sender, EventArgs e)
        //{
        //    var exception = Server.GetLastError();
        //    if (exception is HttpUnhandledException)
        //    {
        //        Server.Transfer("~/Error");
        //    }
        //    if (exception != null)
        //    {
        //        Server.Transfer("~/Error");
        //    }
        //    try
        //    {
        //        // This is to stop a problem where we were seeing "gibberish" in the
        //        // chrome and firefox browsers
        //        HttpApplication app = sender as HttpApplication;
        //        app.Response.Filter = null;
        //    }
        //    catch
        //    {
        //    }
        //}
    }
}
