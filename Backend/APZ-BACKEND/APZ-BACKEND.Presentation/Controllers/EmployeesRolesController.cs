using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APZ_BACKEND.Core.Dtos.EmployeesRoles;
using APZ_BACKEND.Core.Services.EmployeesRoles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APZ_BACKEND.Presentation.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class EmployeesRolesController : Controller
	{
		private readonly IEmployeesRolesService employeesRolesService;

		public EmployeesRolesController(IEmployeesRolesService employeesRolesService)
		{
			this.employeesRolesService = employeesRolesService;
		}

		[HttpGet("business-roles")]
		public async Task<IActionResult> GetBusinessEmployeesRoles()
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var employeesRoles = await employeesRolesService.GetBusinessEmployeesRoles(contextUserId);
			return Ok(employeesRoles);
		}

		[HttpPost("create-employees-role")]
		public async Task<IActionResult> CreateEmployeesRole(CreateEmployeesRoleDto createDto)
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await employeesRolesService.Create(contextUserId, createDto);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage });

			return Ok();
		}

		[HttpPost("add-employee-to-role")]
		public async Task<IActionResult> AddEmployeeToRole(AddEmployeeToRole addEmployeeToRoleDto)
		{
			var result = await employeesRolesService.AddEmployeeToRole(addEmployeeToRoleDto.EmployeeId, addEmployeeToRoleDto.RoleId);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage });

			return Ok();
		}

		[HttpPut("employees-role")]
		public async Task<IActionResult> UpdateEmployeesRole(EmployeesRoleDto roleDto)
		{
			var result = await employeesRolesService.Update(roleDto);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage });

			return Ok();
		}

		[HttpDelete("employees-role/{roleId}")]
		public async Task<IActionResult> DeleteEmployeesRole(int roleId)
		{
			var result = await employeesRolesService.Delete(roleId);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage });

			return Ok();
		}

		[HttpDelete("employee-in-role/{roleId}/{employeeId}")]
		public async Task<IActionResult> RemoveEmployeeFromRole(int roleId, int employeeId)
		{
			var result = await employeesRolesService.RemoveEmployeeFromRole(roleId, employeeId);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage });

			return Ok();
		}
	}
}
