using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APZ_BACKEND.Core.Dtos.SharedItems
{
	public class TakeItemRequest
	{
		[Required]
		public string ItemRfid { get; set; }
		[Required]
		public string UserRfid { get; set; }
		public string SecretKey { get; set; }
	}
}
