using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APZ_BACKEND.Core.Dtos.Readers
{
	public class ReturnItemRequest
	{
		[Required]
		public string ItemRfid { get; set; }
		[Required]
		public string UserRfid { get; set; }
		[Required]
		public string SecretKey { get; set; }
		[Required]
		public int ReaderId { get; set; }
	}
}
