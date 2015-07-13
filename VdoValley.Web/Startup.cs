using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VdoValley.Web.Startup))]
namespace VdoValley.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
