using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APZ_BACKEND.Core.Dtos.SharedItems;
using APZ_BACKEND.Core.Entities;
using APZ_BACKEND.Core.Helpers;
using APZ_BACKEND.Core.Interfaces;
using APZ_BACKEND.Core.Services.Items;
using APZ_BACKEND.Core.Services.Users.BusinessUsers;
using APZ_BACKEND.Core.Services.Users.PrivateUsers;
using APZ_BACKEND.Presentation.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APZ_BACKEND.Presentation.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class SharedItemsController : ControllerBase
	{
		private readonly ISharedItemsService itemsService;
		private readonly IAsyncRepository<PrivateUser> privateUsersRepository;

		public SharedItemsController(ISharedItemsService itemsService,
			IAsyncRepository<PrivateUser> privateUsersRepository)
		{
			this.itemsService = itemsService;
			this.privateUsersRepository = privateUsersRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetBusinessItems(int? businessUserId)
		{
			bool isBusinessUser = ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims);
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			if (!isBusinessUser && !businessUserId.HasValue)
				return BadRequest("You should provide businessUserId");
			else if (!isBusinessUser && businessUserId.HasValue)
			{
				var items = await itemsService.GetBusinessItems(businessUserId.Value, contextUserId);
				return Ok(items);
			}
			else
			{
				var items = await itemsService.GetBusinessItems(contextUserId);
				return Ok(items);
			}
		}

		[HttpGet("employees-role-items/{roleId}")]
		public async Task<IActionResult> GetEmployeesRoleItems(int roleId)
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
				return BadRequest(new { message = "Current user is not a businessUser", code = ErrorCode.NOT_BUSINESS_USER });

			var contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var items = await itemsService.GetEmployeesRoleItems(roleId, contextUserId);
			return Ok(items);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetItem(int id)
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);
			bool isBusinessUser = ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims);

			if (isBusinessUser)
			{
				var result = await itemsService.GetItem(contextUserId, id);
				if (!result.Success)
					return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });

				return Ok(result.Item);
			}
			else
			{
				var result = await itemsService.GetItemPrivateUser(contextUserId, id);
				if (!result.Success)
					return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });

				return Ok(result.Item);
			}
		}

		[HttpPost]
		public async Task<IActionResult> AddItemToBusiness(AddSharedItemRequest addSharedItemRequest)
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
				return BadRequest(new { message = "Current user is not a businessUser", code = ErrorCode.NOT_BUSINESS_USER });

			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await itemsService.AddItemToBusiness(contextUserId, addSharedItemRequest);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });

			return Ok();
		}

		[AllowAnonymous]
		[HttpPost("take-item")]
		public async Task<IActionResult> TakeItem(TakeItemRequest takeItemDto)
		{
			var result = await itemsService.TakeItem(takeItemDto);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });

			return Ok();
		}

		[AllowAnonymous]
		[HttpPost("return-item")]
		public async Task<IActionResult> ReturnItem(ReturnItemRequest returnItemRequest)
		{
			var result = await itemsService.ReturnItem(returnItemRequest);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });
			
			return Ok();
		}

		[HttpPost("add-item-to-employees-role")]
		public async Task<IActionResult> AddItemToRole(AddEmployeeRoleItemRequest dto)
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
				return BadRequest(new { message = "Current user is not a businessUser", code = ErrorCode.NOT_BUSINESS_USER });

			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await itemsService.AddItemToEmployeesRole(contextUserId, dto.SharedItemId, dto.RoleId);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });

			return Ok(result.Item);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateItem(UpdateSharedItemRequest updateSharedItemRequest, int id)
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
				return BadRequest(new { message = "Current user is not a businessUser", ErrorCode.NOT_BUSINESS_USER });

			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await itemsService.Update(updateSharedItemRequest, id, contextUserId);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });

			return Ok();
		}

		[HttpDelete("{itemId}")]
		public async Task<IActionResult> DeleteItem(int itemId)
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
				return BadRequest(new { message = "Current user is not a businessUser", code = ErrorCode.NOT_BUSINESS_USER });

			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await itemsService.Delete(itemId, contextUserId);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });

			return Ok();
		}

		[HttpDelete("item-in-role/{roleItemId}")]
		public async Task<IActionResult> RemoveItemFromRole(int roleItemId)
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
				return BadRequest(new { message = "Current user is not a businessUser", code = ErrorCode.NOT_BUSINESS_USER });

			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await itemsService.RemoveItemFromEmployeesRole(roleItemId, contextUserId);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });

			return Ok();
		}
	}
}
