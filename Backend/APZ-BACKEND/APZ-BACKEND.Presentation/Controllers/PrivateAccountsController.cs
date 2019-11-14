using APZ_BACKEND.Core.Dtos.Auth;
using APZ_BACKEND.Core.Exceptions;
using APZ_BACKEND.Core.Helpers;
using APZ_BACKEND.Core.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APZ_BACKEND.Presentation.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class PrivateAccountsController : ControllerBase
	{
		private IUserService _userService;
		private readonly AppSettings _appSettings;

		public PrivateAccountsController(
			IUserService userService,
			IOptions<AppSettings> appSettings)
		{
			_userService = userService;
			_appSettings = appSettings.Value;
		}

		[AllowAnonymous]
		[HttpPost("authenticate-private")]
		public async Task<IActionResult> AuthenticatePrivate([FromBody]AuthenticateRequest model)
		{
			var user = await _userService.AuthenticatePrivateAsync(model.Login, model.Password);

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
				await _userService.RegisterPrivateAsync(dto);
				return Ok();
			}
			catch (AppException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpGet("public-profile")]
		public async Task<IActionResult> GetPublicProfile(int? id, string? login)
		{
			return BadRequest("Not implemented");
		}

		[HttpGet("account-data")]
		public async Task<IActionResult> GetAccountData()
		{
			return BadRequest("Not implemented");
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody]string businessUser)
		{
			return BadRequest("Not implemented");
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			return BadRequest("Not implemented");
		}

		[HttpGet("availiable-services")]
		public async Task<IActionResult> GetAvailiableServices()
		{
			return BadRequest("Not implemented");
		}

		[HttpGet("availiable-services/{serviceId}")]
		public async Task<IActionResult> GetAvailiableService(int serviceId)
		{
			return BadRequest("Not implemented");
		}

		private string GetTokenString(int userId)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
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
