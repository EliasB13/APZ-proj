using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APZ_BACKEND.Presentation.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class EmployeesRolesController : Controller
	{
		[HttpGet("business-employees-roles")]
		public async Task<IActionResult> GetBusinessEmployeesRoles()
		{

		}

		[HttpPost("create-employees-role")]
		public async Task<IActionResult> CreateEmployeesRole([FromBody]string employeesRoleRequest)
		{
			return BadRequest("Not implemented");
		}

		[HttpPost("add-employee-to-role")]
		public async Task<IActionResult> AddEmployeeToRole(int userId, int roleId)
		{
			return BadRequest("Not implemented");
		}

		[HttpPost("add-employees-to-role")]
		public async Task<IActionResult> AddEmployeesToRole(IEnumerable<int> userIds, int roleId)
		{
			return BadRequest("Not implemented");
		}

		[HttpPut("employees-role/{id}")]
		public async Task<IActionResult> UpdateEmployeesRole(int roleId, [FromBody]string value)
		{
			return BadRequest("Not implemented");
		}

		[HttpDelete("employees-role/{id}")]
		public async Task<IActionResult> DeleteEmployeesRole(int roleId)
		{
			return BadRequest("Not implemented");
		}

		[HttpDelete("employee-in-role")]
		public async Task<IActionResult> DeleteEmployeeFromRole(int roleId, int userId)
		{
			return BadRequest("Not implemented");
		}
	}
}
