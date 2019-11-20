using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APZ_BACKEND.Core.Dtos.SharedItems
{
	public class AddEmployeeRoleItemRequest
	{
		[Required]
		public int RoleId { get; set; }
		[Required]
		public int SharedItemId { get; set; }
	}
}
