using APZ_BACKEND.Core.Helpers;
using APZ_BACKEND.Infrastructure.Services.Users;
using APZ_BACKEND.Presentation.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

		public ImageController(IImageService imagesService)
		{
			this.imagesService = imagesService;
		}

		[HttpPost("upload-profile-photo")]
		public async Task<IActionResult> UploadProfilePicture(IFormFile file, int userId)
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);
			bool isBusinessUser = ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims);

			var result = await imagesService.UploadProfilePicture(file, userId, isBusinessUser ? UserType.BusinessUser : UserType.PrivateUser);
			if (!result.Success)
				return BadRequest(result.ErrorMessage);

			return Ok();
		}
	}
}
