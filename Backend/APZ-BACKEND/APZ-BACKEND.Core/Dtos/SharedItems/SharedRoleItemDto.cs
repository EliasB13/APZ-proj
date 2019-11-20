using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_BACKEND.Core.Dtos.SharedItems
{
	public class SharedRoleItemDto
	{
		public int SharedItemId { get; set; }
		public int RoleItemId { get; set; }
		public int RoleId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsTaken { get; set; }
	}
}
