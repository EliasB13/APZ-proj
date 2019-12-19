using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APZ_BACKEND.Core.Entities
{
	public class PrivateUser
	{
		public int Id { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public bool IsEmailConfirmed { get; set; }
		[MaxLength(20)]
		public string RfidNumber { get; set; }
		public int SearchId { get; set; }
		public string Photo { get; set; }
		public bool IsAdmin { get; set; }

		public IEnumerable<ItemTaking> ItemTakings { get; set; }
		public IEnumerable<Employee> Employees { get; set; }
	}
}
