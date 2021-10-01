using DemoAzureCosmosDb.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoAzureCosmosDb.Domain.Repositories
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetItemsAsync(string queryString);
        Task<Item> GetItemAsync(string id);
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(string id, Item item);
        Task DeleteItemAsync(string id);
    }
}
