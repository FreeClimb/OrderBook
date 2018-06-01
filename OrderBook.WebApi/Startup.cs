using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(OrderBook.WebApi.Startup))]

namespace OrderBook.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Configure Bearer Authentication
            //ConfigureAuth(app);

            var config = new HttpConfiguration();

            //Configure AutoFac (http://autofac.org/) for DependencyResolver
            //For more information visit http://www.asp.net/web-api/overview/extensibility/using-the-web-api-dependency-resolver
            ConfigureComposition(config);

            //Configure WebApi
            ConfigureWebApi(config);
            app.UseWebApi(config);
        }
    }
}