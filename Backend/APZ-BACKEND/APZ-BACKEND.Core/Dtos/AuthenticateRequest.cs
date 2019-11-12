﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APZ_BACKEND.Core.Dtos
{
	public class AuthenticateRequest
	{
		[Required]
		public string Login { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
