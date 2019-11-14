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
	public class ItemsController : Controller
	{
		[HttpGet]
		public async Task<IActionResult> GetBusinessItems(int businessId)
		{
			return BadRequest("Not implemented");
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetItem(int itemId)
		{
			return BadRequest("Not implemented");
		}

		[HttpPost]
		public async Task<IActionResult> AddItemToBusiness([FromBody]string value)
		{
			return BadRequest("Not implemented");
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> EditItem(int id, [FromBody]string value)
		{
			return BadRequest("Not implemented");
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteItem(int id)
		{
			return BadRequest("Not implemented");
		}
	}
}
