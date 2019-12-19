using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APZ_BACKEND.Core.Entities
{
	public class SharedItem
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		[MaxLength(20)]
		public string RfidNumber { get; set; }

		public BusinessUser BusinessUser { get; set; }
		public Reader Reader { get; set; }
		public IEnumerable<ItemTakingLine> ItemTakingLines { get; set; }
		public IEnumerable<EmployeeRoleItem> EmployeeRoleItems { get; set; }
	}
}
