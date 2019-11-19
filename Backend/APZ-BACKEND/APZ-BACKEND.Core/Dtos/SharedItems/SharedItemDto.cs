using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_BACKEND.Core.Dtos.SharedItems
{
	public class SharedItemDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsTaken { get; set; }
	}
}
