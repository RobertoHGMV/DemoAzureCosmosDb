using DemoAzureCosmosDb.Domain.Configurations;
using DemoAzureCosmosDb.Infra.Repositories;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DemoAzureCosmosDb.Infra.Contexts
{
    public class CosmosDbClient
    {
        private readonly CosmosConfig _config;
        public CosmosClient CosmosClient { get; private set; }
        public string DatabaseId { get; private set; }

        public CosmosDbClient(IOptions<CosmosConfig> option)
        {
            _config = option.Value;
            DatabaseId = _config.DatabaseId;

            CosmosClient = new CosmosClient(_config.EndpointUri , _config.PrimaryKey );

            // InitializeCosmosClientInstanceAsync(CosmosClient);
        }

        private async Task InitializeCosmosClientInstanceAsync(CosmosClient dbClient)
        {
            var database = await dbClient.CreateDatabaseIfNotExistsAsync(DatabaseId);
            await database.Database.CreateContainerIfNotExistsAsync(ItemRepository.ContainerName, "/id");
        }
    }
}
