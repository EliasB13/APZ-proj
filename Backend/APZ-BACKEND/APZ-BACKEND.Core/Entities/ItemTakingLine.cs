
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APZ_BACKEND.Core.Entities
{
	public class ItemTakingLine
	{
		public int Id { get; set; }
		public bool IsReturned { get; set; }
		public DateTime ReturningTime { get; set; }

		public ItemTaking ItemTaking { get; set; }
		public SharedItem SharedItem { get; set; }
	}
}
