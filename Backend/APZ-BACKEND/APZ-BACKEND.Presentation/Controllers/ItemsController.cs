using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APZ_BACKEND.Core.Dtos.SharedItems;
using APZ_BACKEND.Core.Services.Items;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APZ_BACKEND.Presentation.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class ItemsController : Controller
	{
		private readonly IItemsService itemsService;

		public ItemsController(IItemsService itemsService)
		{
			this.itemsService = itemsService;
		}

		[HttpGet]
		public async Task<IActionResult> GetBusinessItems()
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var items = await itemsService.GetBusinessItems(contextUserId);
			return Ok(items);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetItem(int itemId)
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await itemsService.GetItem(contextUserId, itemId);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage });

			return Ok(result.Item);
		}

		[HttpPost]
		public async Task<IActionResult> AddItemToBusiness(AddSharedItemRequest addSharedItemRequest)
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await itemsService.AddItemToBusiness(contextUserId, addSharedItemRequest);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage });

			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> EditItem(UpdateSharedItemRequest updateSharedItemRequest)
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);
			// TODO: check whether owner editing item

			var result = await itemsService.Update(updateSharedItemRequest);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage });

			return Ok();
		}

		[HttpDelete("{itemId}")]
		public async Task<IActionResult> DeleteItem(int itemId)
		{
			var result = await itemsService.Delete(itemId);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage });

			return Ok();
		}
	}
}
