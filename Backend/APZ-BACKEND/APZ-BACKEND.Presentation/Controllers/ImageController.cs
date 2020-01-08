using APZ_BACKEND.Core.Helpers;
using APZ_BACKEND.Infrastructure.Services.Users;
using APZ_BACKEND.Presentation.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APZ_BACKEND.Presentation.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class ImageController : ControllerBase
	{
		private readonly IImageService imagesService;
		private readonly ILogger<ImageController> logger;

		public ImageController(IImageService imagesService,
			ILogger<ImageController> logger)
		{
			this.imagesService = imagesService;
			this.logger = logger;
		}

		[HttpPost("upload-profile-photo")]
		public async Task<IActionResult> UploadProfilePicture(IFormFile file)
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);
			bool isBusinessUser = ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims);

			var result = await imagesService.UploadProfilePicture(file, contextUserId, isBusinessUser ? UserType.BusinessUser : UserType.PrivateUser);
			if (!result.Success)
			{
				logger.LogError(result.ErrorMessage);
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });
			}

			return Ok();
		}
	}
}
