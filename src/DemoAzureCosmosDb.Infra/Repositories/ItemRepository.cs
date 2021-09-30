using DemoAzureCosmosDb.Domain.Configurations;
using DemoAzureCosmosDb.Domain.Models;
using DemoAzureCosmosDb.Domain.Repositories;
using DemoAzureCosmosDb.Infra.Contexts;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAzureCosmosDb.Infra.Repositories
{
    public class ItemRepository : IItemRepository
    {
        public readonly Container _container;

        public ItemRepository(CosmosDbClient cosmosDbClient)
        {
            _container = cosmosDbClient.Container;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<Item> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task AddItemAsync(Item item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.Id));
        }

        public async Task UpdateItemAsync(string id, Item item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
