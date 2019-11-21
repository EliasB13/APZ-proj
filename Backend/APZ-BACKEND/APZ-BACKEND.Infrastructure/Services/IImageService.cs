using APZ_BACKEND.Core.Helpers;
using APZ_BACKEND.Core.Services.Communication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APZ_BACKEND.Infrastructure.Services.Users
{
	public interface IImageService
	{
		Task<GenericServiceResponse<object>> UploadProfilePicture(IFormFile file, int userId, UserType userType);
	}
}
