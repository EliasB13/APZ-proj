using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_Iot.Dto
{
	public class ItemDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string RfId { get; set; }
		public string IsTaken { get; set; }
	}
}
