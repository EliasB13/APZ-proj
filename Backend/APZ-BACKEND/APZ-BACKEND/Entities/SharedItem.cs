using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APZ_BACKEND.Entities
{
	public class SharedItem
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public byte[] RfidNumber { get; set; }

		public BusinessUser BusinessUser { get; set; }
		public IEnumerable<ItemTakingLine> ItemTakingLines { get; set; }
		public IEnumerable<EmployeeRoleItem> EmployeeRoleItems { get; set; }
	}
}
