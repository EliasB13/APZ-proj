using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APZ_BACKEND.Core.Dtos.Employee;
using APZ_BACKEND.Core.Services.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var employees = await employeesService.GetBusinessEmployees(contextUserId);
			return Ok(employees);
		}

		[HttpPost("add-employee")]
		public async Task<IActionResult> AddEmployee([FromBody]AddEmployeeDto addEmployeeDto)
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await employeesService.AddEmployee(contextUserId, addEmployeeDto.Login);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage });

			return Ok();
		}

		[HttpPost("add-employees")]
		public async Task<IActionResult> AddEmployees([FromBody]IEnumerable<int> userIds)
		{
			return BadRequest("Not implemented");
		}

		[HttpDelete("employee")]
		public async Task<IActionResult> DeleteEmployeeFromBusiness(int employeeId)
		{
			int contextUserId = int.Parse(HttpContext.User.Identity.Name);

			var result = await employeesService.DeleteEmployee(employeeId);
			if (!result.Success)
				return BadRequest(new { message = result.ErrorMessage });

			return Ok();
		}
	}
}
