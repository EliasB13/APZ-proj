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
	public class SharedItemsController : Controller
	{
		private readonly ISharedItemsService itemsService;

		public SharedItemsController(ISharedItemsService itemsService)
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

		[HttpGet("employees-role-items/{roleId}")]
		public async Task<IActionResult> GetEmployeesRoleItems(int roleId)
		{
			var contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var items = await itemsService.GetEmployeesRoleItems(roleId, contextUserId);
			return Ok(items);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetItem(int id)
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await itemsService.GetItem(contextUserId, id);
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

		[HttpPost("take-item")]
		public async Task<IActionResult> TakeItem(int itemId)
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);
			return Ok();
		}

		[HttpPost("return-item")]
		public async Task<IActionResult> ReturnItem(int itemId)
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);
			return Ok();
		}

		[HttpPost("add-item-to-employees-role")]
		public async Task<IActionResult> AddItemToRole(AddEmployeeRoleItemRequest dto)
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await itemsService.AddItemToEmployeesRole(contextUserId, dto.SharedItemId, dto.RoleId);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage });

			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateItem(UpdateSharedItemRequest updateSharedItemRequest, int id)
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await itemsService.Update(updateSharedItemRequest, id, contextUserId);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage });

			return Ok();
		}

		[HttpDelete("{itemId}")]
		public async Task<IActionResult> DeleteItem(int itemId)
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await itemsService.Delete(itemId, contextUserId);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage });

			return Ok();
		}

		[HttpDelete("item-in-role/{roleItemId}")]
		public async Task<IActionResult> RemoveItemFromRole(int roleItemId)
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await itemsService.RemoveItemFromEmployeesRole(roleItemId, contextUserId);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage });

			return Ok();
		}
	}
}
