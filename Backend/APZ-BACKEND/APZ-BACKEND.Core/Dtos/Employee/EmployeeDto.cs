using APZ_BACKEND.Core.Dtos.EmployeesRoles;
using APZ_BACKEND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_BACKEND.Core.Dtos.Employee
{
	public class EmployeeDto
	{
		public int UserId { get; set; }
		public int EmployeeId { get; set; }
		public string Login { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Company { get; set; }
		public EmployeesRoleDto Role { get; set; }
	}
}
