using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APZ_BACKEND.Entities
{
	public class EmployeesRole
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public IEnumerable<Employee> Employees { get; set; }
		public BusinessUser BusinessUser { get; set; }
	}
}
