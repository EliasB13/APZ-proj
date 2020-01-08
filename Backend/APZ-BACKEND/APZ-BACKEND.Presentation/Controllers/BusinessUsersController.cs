using APZ_BACKEND.Core.Dtos.Auth;
using APZ_BACKEND.Core.Dtos.Users;
using APZ_BACKEND.Core.Exceptions;
using APZ_BACKEND.Core.Helpers;
using APZ_BACKEND.Core.Services.Users;
using APZ_BACKEND.Core.Services.Users.BusinessUsers;
using APZ_BACKEND.Presentation.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APZ_BACKEND.Presentation.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class BusinessUsersController : ControllerBase
	{
		private readonly IBusinessUsersService userService;
		private readonly AppSettings appSettings;
		private readonly ILogger<BusinessUsersController> logger;

		public BusinessUsersController(
			IBusinessUsersService userService,
			IOptions<AppSettings> appSettings,
			ILogger<BusinessUsersController> logger)
		{
			this.userService = userService;
			this.appSettings = appSettings.Value;
			this.logger = logger;
		}

		[AllowAnonymous]
		[HttpGet("public-profile")]
		public async Task<IActionResult> GetPublicProfile(int? id, string login)
		{
			if (id.HasValue && !string.IsNullOrEmpty(login))
			{
				logger.LogError("Provide only 1 parameter");
				return BadRequest(new { message = "Provide only 1 parameter", code = ErrorCode.WRONG_REQUEST_PARAMETERS });
			}
			if (id.HasValue)
			{
				var result = await userService.GetPublicProfile(id.Value);
				if (!result.Success)
				{
					logger.LogError(result.ErrorMessage);
					return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });
				}
				return Ok(result.Item);
			}
			if (!string.IsNullOrEmpty(login))
			{
				var result = await userService.GetPublicProfile(login);
				if (!result.Success)
				{
					logger.LogError(result.ErrorMessage);
					return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });
				}
				return Ok(result.Item);
			}
			return BadRequest(new { message = "You should provide at least 1 parameter", ErrorCode.WRONG_REQUEST_PARAMETERS });
		}

		[HttpGet("account-data")]
		public async Task<IActionResult> GetAccountData()
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
			{
				logger.LogInformation("Current user is not a businessUser");
				return BadRequest(new { message = "Current user is not a businessUser", code = ErrorCode.NOT_BUSINESS_USER });
			}

			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await userService.GetAccountData(contextUserId);
			if (!result.Success)
			{
				logger.LogError(result.ErrorMessage);
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });
			}

			return Ok(result.Item);
		}

		[AllowAnonymous]
		[HttpPost("authenticate-business")]
		public async Task<IActionResult> AuthenticateBusiness([FromBody]AuthenticateRequest dto)
		{
			var user = await userService.AuthenticateBusinessAsync(dto.Login, dto.Password);

			if (user == null)
			{
				logger.LogError("Username or password is incorrect");
				return BadRequest(new { message = "Username or password is incorrect", code = ErrorCode.USERNAME_OR_PASSWORD_INCORRECT });
			}

			var token = GetTokenString(user.Id);

			return Ok(new
			{
				Id = user.Id,
				Login = user.Login,
				Token = token,
				Photo = user.Photo
			});
		}

		[AllowAnonymous]
		[HttpPost("register-business")]
		public async Task<IActionResult> RegisterBusiness([FromBody]RegisterBusinessRequest dto)
		{
			var result = await userService.RegisterBusinessAsync(dto);
			if (!result.Success)
			{
				logger.LogError(result.ErrorMessage);
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });
			}

			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody]UpdateBusinessUserRequest businessUser)
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
			{
				logger.LogError("Current user is not a businessUser");
				return BadRequest(new { message = "Current user is not a businessUser", code = ErrorCode.NOT_BUSINESS_USER });
			}

			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await userService.UpdateBusinessUser(businessUser, contextUserId);
			if (!result.Success)
			{
				logger.LogError(result.ErrorMessage);
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });
			}

			return Ok(result.Item);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete()
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
			{
				logger.LogError("Current user is not a businessUser");
				return BadRequest(new { message = "Current user is not a businessUser", code = ErrorCode.NOT_BUSINESS_USER });
			}

			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await userService.DeleteBusinessUser(contextUserId);
			if (!result.Success)
			{
				logger.LogError(result.ErrorMessage);
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });
			}

			return Ok();
		}

		private string GetTokenString(int userId)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(appSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, userId.ToString()),
					new Claim(ClaimTypes.Role, ((int)UserType.BusinessUser).ToString())
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
