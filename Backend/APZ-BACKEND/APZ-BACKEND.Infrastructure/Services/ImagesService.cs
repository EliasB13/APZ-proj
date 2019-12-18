using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using APZ_BACKEND.Core.Helpers;
using APZ_BACKEND.Core.Services.Communication;
using APZ_BACKEND.Core.Services.Users.BusinessUsers;
using APZ_BACKEND.Core.Services.Users.PrivateUsers;
using Microsoft.AspNetCore.Http;

namespace APZ_BACKEND.Infrastructure.Services.Users
{
	public class ImagesService : IImageService
	{
		private readonly IPrivateUsersService privateUsersService;
		private readonly IBusinessUsersService businessUsersService;

		public ImagesService(IPrivateUsersService privateUsersService,
			IBusinessUsersService businessUsersService)
		{
			this.privateUsersService = privateUsersService;
			this.businessUsersService = businessUsersService;
		}

		public async Task<GenericServiceResponse<object>> UploadProfilePicture(IFormFile file, int userId, UserType userType)
		{
			if (file == null || file.Length == 0)
				return new GenericServiceResponse<object>("File was not found", ErrorCode.FILE_NOT_FOUND);

			var folderName = Path.Combine("Resources", "ProfilePics");
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

			if (!Directory.Exists(filePath))
			{
				Directory.CreateDirectory(filePath);
			}

			string userTypeString = userType == UserType.BusinessUser ? "businessUser" : "privateUser";
			var uniqueFileName = $"{userTypeString}_{userId}_profilepic.png";
			var dbPath = Path.Combine(folderName, uniqueFileName);

			if (userType == UserType.BusinessUser)
			{
				var result = await businessUsersService.UpdatePhotoPath(dbPath, userId);
				if (!result.Success)
					return new GenericServiceResponse<object>(result.ErrorMessage, result.ErrorCode);
			} 
			else
			{
				var result = await privateUsersService.UpdatePhotoPath(dbPath, userId);
				if (!result.Success)
					return new GenericServiceResponse<object>(result.ErrorMessage, result.ErrorCode);
			}

			using (var fileStream = new FileStream(Path.Combine(filePath, uniqueFileName), FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
			}

			return new GenericServiceResponse<object>(new object());
		}
	}
}
