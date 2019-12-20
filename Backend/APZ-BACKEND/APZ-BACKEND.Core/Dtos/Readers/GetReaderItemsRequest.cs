using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_BACKEND.Core.Dtos.Readers
{
	public class GetReaderItemsRequest
	{
		public int ReaderId { get; set; }
		public string Secret { get; set; }
	}
}
