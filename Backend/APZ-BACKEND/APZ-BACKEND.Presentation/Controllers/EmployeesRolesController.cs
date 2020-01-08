using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APZ_BACKEND.Core.Dtos.EmployeesRoles;
using APZ_BACKEND.Core.Helpers;
using APZ_BACKEND.Core.Services.EmployeesRoles;
using APZ_BACKEND.Presentation.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace APZ_BACKEND.Presentation.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class EmployeesRolesController : Controller
	{
		private readonly IEmployeesRolesService employeesRolesService;
		private readonly ILogger<EmployeesRolesController> logger;

		public EmployeesRolesController(IEmployeesRolesService employeesRolesService,
			ILogger<EmployeesRolesController> logger)
		{
			this.employeesRolesService = employeesRolesService;
			this.logger = logger;
		}

		[HttpGet("business-roles")]
		public async Task<IActionResult> GetBusinessEmployeesRoles()
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
			{
				logger.LogError("Current user is not a businessUser");
				return BadRequest(new { message = "Current user is not a businessUser", code = ErrorCode.NOT_BUSINESS_USER });
			}

			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var employeesRoles = await employeesRolesService.GetBusinessEmployeesRoles(contextUserId);
			return Ok(employeesRoles);
		}

		[HttpGet("role/{roleId}")]
		public async Task<IActionResult> GetRoleById(int roleId)
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
			{
				logger.LogError("Current user is not a businessUser");
				return BadRequest(new { message = "Current user is not a businessUser", code = ErrorCode.NOT_BUSINESS_USER });
			}

			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await employeesRolesService.GetRoleById(roleId, contextUserId);
			if (!result.Success)
			{
				logger.LogError(result.ErrorMessage);
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });
			}

			return Ok(result.Item);
		}

		[HttpGet("employees-in-role/{roleId}")]
		public async Task<IActionResult> GetEmployeesInRole(int roleId)
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
			{
				logger.LogError("Current user is not a businessUser");
				return BadRequest(new { message = "Current user is not a businessUser", code = ErrorCode.NOT_BUSINESS_USER });
			}

			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var employeesRoles = await employeesRolesService.GetEmployeesInRole(contextUserId, roleId);
			return Ok(employeesRoles);
		}

		[HttpPost("create-employees-role")]
		public async Task<IActionResult> CreateEmployeesRole(CreateEmployeesRoleDto createDto)
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
			{
				logger.LogError("Current user is not a businessUser");
				return BadRequest(new { message = "Current user is not a businessUser", code = ErrorCode.NOT_BUSINESS_USER });
			}

			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await employeesRolesService.Create(contextUserId, createDto);
			if (!result.Success)
			{
				logger.LogError(result.ErrorMessage);
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });
			}

			return Ok(result.Item);
		}

		[HttpPost("add-employee-to-role")]
		public async Task<IActionResult> AddEmployeeToRole(AddEmployeeToRole addEmployeeToRoleDto)
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
			{
				logger.LogError("Current user is not a businessUser");
				return BadRequest(new { message = "Current user is not a businessUser", code = ErrorCode.NOT_BUSINESS_USER });
			}

			var result = await employeesRolesService.AddEmployeeToRole(addEmployeeToRoleDto.EmployeeId, addEmployeeToRoleDto.RoleId);
			if (!result.Success)
			{
				logger.LogError(result.ErrorMessage);
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });
			}

			return Ok(result.Item);
		}

		[HttpPut("employees-role")]
		public async Task<IActionResult> UpdateEmployeesRole(EmployeesRoleDto roleDto)
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
			{
				logger.LogError("Current user is not a businessUser");
				return BadRequest(new { message = "Current user is not a businessUser", code = ErrorCode.NOT_BUSINESS_USER });
			}

			var result = await employeesRolesService.Update(roleDto);
			if (!result.Success)
			{
				logger.LogError(result.ErrorMessage);
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });
			}

			return Ok();
		}

		[HttpDelete("employees-role/{roleId}")]
		public async Task<IActionResult> DeleteEmployeesRole(int roleId)
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
			{
				logger.LogError("Current user is not a businessUser");
				return BadRequest(new { message = "Current user is not a businessUser", code = ErrorCode.NOT_BUSINESS_USER });
			}

			var result = await employeesRolesService.Delete(roleId);
			if (!result.Success)
			{
				logger.LogError(result.ErrorMessage);
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });
			}

			return Ok();
		}

		[HttpDelete("employee-in-role/{roleId}/{employeeId}")]
		public async Task<IActionResult> RemoveEmployeeFromRole(int roleId, int employeeId)
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
			{
				logger.LogError("Current user is not a businessUser");
				return BadRequest(new { message = "Current user is not a businessUser", code = ErrorCode.NOT_BUSINESS_USER });
			}

			var result = await employeesRolesService.RemoveEmployeeFromRole(roleId, employeeId);
			if (!result.Success)
			{
				logger.LogError(result.ErrorMessage);
				return BadRequest(new { message = result.ErrorMessage, code = result.ErrorCode });
			}

			return Ok();
		}
	}
}
