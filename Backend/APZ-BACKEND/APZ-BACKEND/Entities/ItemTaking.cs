using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APZ_BACKEND.Entities
{
	public class ItemTaking
	{
		public int Id { get; set; }
		public DateTime TakingTime { get; set; }

		public PrivateUser PrivateUser { get; set; }
		public IEnumerable<ItemTakingLine> ItemTakingLines { get; set; }
	}
}
