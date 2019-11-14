using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APZ_BACKEND.Presentation.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class BusinessController : Controller
	{
		[HttpGet]
		public async Task<IActionResult> GetAllEmployees()
		{
			return BadRequest("Not implemented");
		}

		[HttpPost("add-employee")]
		public async Task<IActionResult> AddEmployee(int userId)
		{
			return BadRequest("Not implemented");
		}

		[HttpPost("add-employees")]
		public async Task<IActionResult> AddEmployees(IEnumerable<int> userIds)
		{
			return BadRequest("Not implemented");
		}

		[HttpDelete("employee")]
		public async Task<IActionResult> DeleteEmployeeFromBusiness(int userId)
		{
			return BadRequest("Not implemented");
		}
	}
}
