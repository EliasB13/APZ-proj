using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APZ_BACKEND.Core.Dtos.Users
{
	public class UpdatePrivateUserRequest
	{
		[Required]
		[MaxLength(24, ErrorMessage = "Login must be more than 4 symbols"), MinLength(4, ErrorMessage = "Login must be less than 24 symbols")]
		[RegularExpression(@"(?!^\d+$)^[A-Za-z\d]+$", ErrorMessage = "Login should consists of latin and digits symbols, but can't contain only digits")]
		public string Login { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string Phone { get; set; }
	}
}
