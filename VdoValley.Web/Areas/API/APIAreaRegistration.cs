using System.Web.Mvc;

namespace VdoValley.Web.Areas.API
{
    public class APIAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "API";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "API_default",
                "API/api/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}