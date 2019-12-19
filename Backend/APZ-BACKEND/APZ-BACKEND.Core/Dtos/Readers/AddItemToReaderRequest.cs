using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_BACKEND.Core.Dtos.Readers
{
	public class AddItemToReaderRequest
	{
		public int SharedItemId { get; set; }
		public int ReaderId { get; set; }
	}
}
