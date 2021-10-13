using DemoAzureCosmosDb.Domain.Models;
using DemoAzureCosmosDb.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAzureCosmosDb.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ItemController : Controller
    {
        readonly IItemRepository _itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _itemRepository.GetItemsAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> DetailsAsync(string id)
        {
            return Ok(await _itemRepository.GetItemAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] Item item)
        {
            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid().ToString();
                await _itemRepository.AddItemAsync(item);

                return Ok(item);
            }

            return View(item);
        }

        [HttpPut]
        public async Task<IActionResult> EditAsync([FromBody] Item item)
        {
            if (ModelState.IsValid)
            {
                await _itemRepository.UpdateItemAsync(item.Id, item);
                return Ok(item);
            }

            return View(item);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteConfirmedAsync(string id)
        {
            await _itemRepository.DeleteItemAsync(id);
            return NoContent();
        }
    }
}
