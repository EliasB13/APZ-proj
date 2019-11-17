using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_BACKEND.Core.Dtos.Users
{
	public class BusinessUserAccountData
	{
		public int Id { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }
		public string CompanyName { get; set; }
		public string Description { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
	}
}
