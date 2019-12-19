using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APZ_BACKEND.Core.Dtos.Readers;
using APZ_BACKEND.Core.Dtos.SharedItems;
using APZ_BACKEND.Core.Helpers;
using APZ_BACKEND.Core.Services.Readers;
using APZ_BACKEND.Presentation.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APZ_BACKEND.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadersController : ControllerBase
    {
		private readonly IReaderService readerService;

		public ReadersController(IReaderService readerService)
		{
			this.readerService = readerService;
		}

		[HttpGet("reader-items/{readerId}")]
		public async Task<IActionResult> GetReaderItems(int readerId)
		{
			var items = await readerService.GetReaderItems(readerId);
			return Ok(items);
		}

		[Authorize]
		[HttpGet("readers")]
		public async Task<IActionResult> GetReaders()
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
				return BadRequest(new { message = "Current user is not a businessUser", code = ErrorCode.NOT_BUSINESS_USER });

			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var items = await readerService.GetReaders(contextUserId);
			return Ok(items);
		}

		[Authorize]
		[HttpPost("add-item-to-reader")]
		public async Task<IActionResult> AddItemToReader(AddItemToReaderRequest request)
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
				return BadRequest(new { message = "Current user is not a businessUser", code = ErrorCode.NOT_BUSINESS_USER });

			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await readerService.AddItemToReader(contextUserId, request);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });

			return Ok();
		}

		[Authorize]
		[HttpPost("order-card")]
		public async Task<IActionResult> OrderCard()
		{
			if (ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
				return BadRequest(new { message = "Current user is not a privateUser", code = ErrorCode.NOT_PRIVATE_USER });

			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await readerService.OrderCard(contextUserId);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorCode, code = result.ErrorCode });

			return Ok(result.Item);
		}

		[Authorize]
		[HttpPost("order-reader")]
		public async Task<IActionResult> OrderReader()
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
				return BadRequest(new { message = "Current user is not a businessUser", code = ErrorCode.NOT_BUSINESS_USER });

			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await readerService.OrderReader(contextUserId);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorCode, code = result.ErrorCode });

			return Ok(result.Item);
		}

		[AllowAnonymous]
		[HttpPost("take-item")]
		public async Task<IActionResult> TakeItem(TakeItemRequest takeItemDto)
		{
			var result = await readerService.TakeItem(takeItemDto);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });

			return Ok();
		}

		[AllowAnonymous]
		[HttpPost("return-item")]
		public async Task<IActionResult> ReturnItem(ReturnItemRequest returnItemRequest)
		{
			var result = await readerService.ReturnItem(returnItemRequest);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });

			return Ok();
		}
	}
}