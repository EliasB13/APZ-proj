using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APZ_BACKEND.Core.Entities
{
	public class Employee
	{
		public int Id { get; set; }

		public PrivateUser PrivateUser { get; set; }
		public BusinessUser BusinessUser { get; set; }
		public EmployeesRole EmployeesRole { get; set; }
	}
}
