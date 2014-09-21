using System.Web.Http;
using AttributeRouting.Web.Http.WebHost;
using Mev.Events.Web.Api;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Mev.Events.Web.AttributeRoutingHttpConfig), "Start")]

namespace Mev.Events.Web 
{
    public static class AttributeRoutingHttpConfig
	{
		public static void RegisterRoutes(HttpRouteCollection routes) 
		{    
			// See http://github.com/mccalltd/AttributeRouting/wiki for more options.
			// To debug routes locally using the built in ASP.NET development server, go to /routes.axd

            routes.MapHttpAttributeRoutes(cfg => {
                cfg.InMemory = true;
                cfg.AutoGenerateRouteNames = true;
                cfg.AddRoutesFromAssemblyOf<DataController>();
            });
		}

        public static void Start() 
		{
            RegisterRoutes(GlobalConfiguration.Configuration.Routes);
        }
    }
}
