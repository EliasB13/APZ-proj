using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APZ_BACKEND.Core.Dtos.Users
{
	public class UpdateBusinessUserRequest
	{
		[Required]
		public string Login { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string CompanyName { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string Phone { get; set; }
		[Required]
		public string Photo { get; set; }
	}
}
