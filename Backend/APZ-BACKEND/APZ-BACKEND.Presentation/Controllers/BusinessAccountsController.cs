using APZ_BACKEND.Core.Dtos.Auth;
using APZ_BACKEND.Core.Exceptions;
using APZ_BACKEND.Core.Helpers;
using APZ_BACKEND.Core.Services.Users;
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
	public class BusinessAccountsController : ControllerBase
	{
		private IUserService _userService;
		private readonly AppSettings _appSettings;

		public BusinessAccountsController(
			IUserService userService,
			IOptions<AppSettings> appSettings)
		{
			_userService = userService;
			_appSettings = appSettings.Value;
		}

		[AllowAnonymous]
		[HttpPost("authenticate-business")]
		public async Task<IActionResult> AuthenticateBusiness([FromBody]AuthenticateRequest dto)
		{
			var user = await _userService.AuthenticateBusinessAsync(dto.Login, dto.Password);

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
		[HttpPost("register-business")]
		public async Task<IActionResult> RegisterPrivate([FromBody]RegisterBusinessRequest dto)
		{
			try
			{
				await _userService.RegisterBusinessAsync(dto);
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

		//[HttpPut("{id}")]
		//public IActionResult Update(int id, [FromBody]UpdateModel model)
		//{
		//	// map model to entity and set id
		//	var user = _mapper.Map<User>(model);
		//	user.Id = id;

		//	try
		//	{
		//		// update user 
		//		_userService.Update(user, model.Password);
		//		return Ok();
		//	}
		//	catch (AppException ex)
		//	{
		//		// return error message if there was an exception
		//		return BadRequest(new { message = ex.Message });
		//	}
		//}

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
