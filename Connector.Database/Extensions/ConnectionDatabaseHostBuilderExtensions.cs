using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Connector.Database.Extensions
{
    public static class ConnectionDatabaseHostBuilderExtensions
    {
        public static IHostBuilder UseConnectorDatabase(this IHostBuilder builder)
        {
            builder.ConfigureServices((context, collection) =>
            {
                collection.Configure<DataBase>(context.Configuration);
                context.Configuration.Bind(ConfigurationConnectionDatabase.Setting);
            });
            return builder;
        }
        public static void UseConnectorDatabase(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<DataBase>(configuration);
            configuration.Bind(ConfigurationConnectionDatabase.Setting);
        }
    }
}
