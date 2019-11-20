using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APZ_BACKEND.Core.Entities
{
	public class EmployeeRoleItem
	{
		public int Id { get; set; }
		public int SharedItemId { get; set; }
		public int EmployeesRoleId { get; set; }

		public SharedItem SharedItem { get; set; }
		public EmployeesRole EmployeesRole { get; set; }
	}
}
