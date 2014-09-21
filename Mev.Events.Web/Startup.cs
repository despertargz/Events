using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mev.Events.Web.Startup))]
namespace Mev.Events.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
