using System.Web.Http;
using System.Web.Http.Cors;
using OrderBook.WebApi.Common;

namespace OrderBook.WebApi
{
	public partial class Startup
	{
	    public static void ConfigureWebApi(HttpConfiguration config)
	    {
            // Enable CORS
            //config.EnableCors();
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
	    }
	}
}