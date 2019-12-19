using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APZ_BACKEND.Core.Dtos.SharedItems
{
	public class AddSharedItemRequest
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		public string Rfid { get; set; }
	}
}
