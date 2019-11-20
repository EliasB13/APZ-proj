using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_BACKEND.Core.Dtos.Users
{
	public class PrivateUserProfile
	{
		public int Id { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Photo { get; set; }
	}
}
