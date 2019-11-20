using APZ_BACKEND.Core.Dtos.Auth;
using APZ_BACKEND.Core.Dtos.Users;
using APZ_BACKEND.Core.Exceptions;
using APZ_BACKEND.Core.Helpers;
using APZ_BACKEND.Core.Services.Users.PrivateUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APZ_BACKEND.Presentation.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class PrivateUsersController : ControllerBase
	{
		private IPrivateUsersService userService;
		private readonly AppSettings appSettings;

		public PrivateUsersController(
			IPrivateUsersService userService,
			IOptions<AppSettings> appSettings)
		{
			this.userService = userService;
			this.appSettings = appSettings.Value;
		}

		[AllowAnonymous]
		[HttpGet("public-profile")]
		public async Task<IActionResult> GetPublicProfile(int? id, string login)
		{
			if (id.HasValue && !string.IsNullOrEmpty(login))
				return BadRequest(new { message = "Provide only 1 parameter" });
			if (id.HasValue)
			{
				var result = await userService.GetPublicProfile(id.Value);
				if (!result.Success)
					return BadRequest(new { message = result.ErrorMessage });
				return Ok(result.Item);
			}
			if (!string.IsNullOrEmpty(login))
			{
				var result = await userService.GetPublicProfile(login);
				if (!result.Success)
					return BadRequest(new { message = result.ErrorMessage });
				return Ok(result.Item);
			}
			return BadRequest(new { message = "You should provide at least 1 parameter" });
		}

		[HttpGet("account-data")]
		public async Task<IActionResult> GetAccountData()
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await userService.GetAccountData(contextUserId);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage });

			return Ok(result.Item);
		}

		[HttpGet("availiable-services")]
		public async Task<IActionResult> GetAvailiableServices()
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var services = await userService.GetAvailableServices(contextUserId);

			return Ok(services);
		}

		[AllowAnonymous]
		[HttpPost("authenticate-private")]
		public async Task<IActionResult> AuthenticatePrivate([FromBody]AuthenticateRequest model)
		{
			var user = await userService.AuthenticatePrivateAsync(model.Login, model.Password);

			if (user == null)
				return BadRequest(new { message = "Username or password is incorrect" });

			var token = GetTokenString(user.Id);

			return Ok(new
			{
				Id = user.Id,
				Login = user.Login,
				Token = token
			});
		}

		[AllowAnonymous]
		[HttpPost("register-private")]
		public async Task<IActionResult> RegisterPrivate([FromBody]RegisterPrivateRequest dto)
		{
			try
			{
				await userService.RegisterPrivateAsync(dto);
				return Ok();
			}
			catch (AppException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpPut]
		public async Task<IActionResult> Update(UpdatePrivateUserRequest privateUser)
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await userService.UpdatePrivateUser(privateUser, contextUserId);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage });

			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete()
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await userService.DeletePrivateUser(contextUserId);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage });

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
					new Claim(ClaimTypes.Name, userId.ToString())
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
