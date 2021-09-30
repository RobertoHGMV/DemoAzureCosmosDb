using DemoAzureCosmosDb.Domain.Configurations;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DemoAzureCosmosDb.Infra.Contexts
{
    public class CosmosDbClient
    {
        private readonly CosmosConfig _config;
        public readonly Container Container;

        public CosmosDbClient(IOptions<CosmosConfig> option)
        {
            _config = option.Value;
            var dbClient = new CosmosClient(_config.Account, _config.Key);
            Container = dbClient.GetContainer(_config.DatabaseName, _config.ContainerName);
            //InitializeCosmosClientInstanceAsync(dbClient);
        }

        private async Task InitializeCosmosClientInstanceAsync(CosmosClient dbClient)
        {
            var database = await dbClient.CreateDatabaseIfNotExistsAsync(_config.DatabaseName);
            await database.Database.CreateContainerIfNotExistsAsync(_config.ContainerName, "/id");
        }
    }
}
