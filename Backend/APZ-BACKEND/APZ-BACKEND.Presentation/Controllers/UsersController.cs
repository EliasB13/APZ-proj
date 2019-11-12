using APZ_BACKEND.Core.Dtos;
using APZ_BACKEND.Core.Entities;
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
	public class UsersController : ControllerBase
	{
		private IUserService _userService;
		private readonly AppSettings _appSettings;

		public UsersController(
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

		[AllowAnonymous]
		[HttpPost("register-business")]
		public IActionResult RegisterPrivate([FromBody]RegisterBusinessRequest dto)
		{
			try
			{
				_userService.RegisterBusinessAsync(dto);
				return Ok();
			}
			catch (AppException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		//[HttpGet("{id}")]
		//public IActionResult GetById(int id)
		//{
		//	var user = _userService.GetById(id);
		//	var model = _mapper.Map<UserModel>(user);
		//	return Ok(model);
		//}

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

		//[HttpDelete("{id}")]
		//public IActionResult Delete(int id)
		//{
		//	_userService.Delete(id);
		//	return Ok();
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
