using DemoAzureCosmosDb.Domain.Models;
using DemoAzureCosmosDb.Domain.Repositories;
using DemoAzureCosmosDb.Infra.Contexts;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAzureCosmosDb.Infra.Repositories
{
    public class ItemRepository : IItemRepository
    {
        public const string ContainerName = "Item";
        public readonly Container _container;

        public ItemRepository(CosmosDbClient cosmosDbClient)
        {
            _container = cosmosDbClient.CosmosClient.GetContainer(cosmosDbClient.DatabaseId, ContainerName);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            var queryString = "SELECT * FROM Item";
            var query = _container.GetItemQueryIterator<Item>(new QueryDefinition(queryString));
            var results = new List<Item>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<Item> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Item> response = await _container.ReadItemAsync<Item>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task AddItemAsync(Item item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.Id));
        }

        public async Task UpdateItemAsync(string id, Item item)
        {
            await _container.UpsertItemAsync(item, new PartitionKey(id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await _container.DeleteItemAsync<Item>(id, new PartitionKey(id));
        }
    }
}
