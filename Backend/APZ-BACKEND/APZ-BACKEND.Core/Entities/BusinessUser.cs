using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APZ_BACKEND.Core.Entities
{
	public class BusinessUser
	{
		public int Id { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
		public string CompanyName { get; set; }
		public string Description { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public bool IsEmailConfirmed { get; set; }
		public string Photo { get; set; }

		public IEnumerable<SharedItem> SharedItems { get; set; }
		public IEnumerable<Employee> Employees { get; set; }
		public IEnumerable<EmployeesRole> EmployeesRoles { get; set; }
	}
}
