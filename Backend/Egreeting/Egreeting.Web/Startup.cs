using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Egreeting.Web.Startup))]
namespace Egreeting.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
