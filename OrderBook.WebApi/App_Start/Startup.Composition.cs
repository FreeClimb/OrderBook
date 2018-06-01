using Autofac;
using OrderBook.Domain;
using OrderBook.EF;
using OrderBook.WebApi.Common;
using System.Web.Http;

namespace OrderBook.WebApi
{
    public partial class Startup
    {
        private static IContainer RegisterServices()
        {
            var builder = new ContainerBuilder();

            // repositories
            builder.RegisterType<OrderRepository>()
                   .As<IOrderRepository>();


            // controllers
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                   .Where(t => t.Name.EndsWith("Controller"))
                   .AsSelf();

            return builder.Build();
        } 
        
        public static void ConfigureComposition(HttpConfiguration config)
        {
            IContainer container = RegisterServices();
            config.DependencyResolver = new AutoFacDependencyResolver(container);
        }
    }
}