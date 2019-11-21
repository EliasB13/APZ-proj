using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APZ_BACKEND.Core.Dtos.Employee;
using APZ_BACKEND.Core.Services.Employees;
using APZ_BACKEND.Presentation.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APZ_BACKEND.Presentation.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	public class EmployeesController : Controller
	{
		private readonly IEmployeesService employeesService;

		public EmployeesController(IEmployeesService employeesService)
		{
			this.employeesService = employeesService;
		}

		[HttpGet("businessEmployees")]
		public async Task<IActionResult> GetAllBusinessEmployees()
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
				return BadRequest(new { message = "Current user is not a businessUser" });

			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var employees = await employeesService.GetBusinessEmployees(contextUserId);
			return Ok(employees);
		}

		[HttpPost("add-employee")]
		public async Task<IActionResult> AddEmployee([FromBody]AddEmployeeDto addEmployeeDto)
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
				return BadRequest(new { message = "Current user is not a businessUser" });

			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await employeesService.AddEmployee(contextUserId, addEmployeeDto.Login);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage });

			return Ok();
		}
		
		[HttpDelete("employee")]
		public async Task<IActionResult> DeleteEmployeeFromBusiness(int employeeId)
		{
			if (!ContextAuthHelper.IsBusinessUser(HttpContext.User.Claims))
				return BadRequest(new { message = "Current user is not a businessUser" });

			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await employeesService.DeleteEmployee(employeeId);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage });

			return Ok();
		}
	}
}
