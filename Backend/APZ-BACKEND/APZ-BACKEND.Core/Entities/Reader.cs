using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_BACKEND.Core.Entities
{
	public class Reader
	{
		public int Id { get; set; }
		public byte[] SecretHash { get; set; }
		public byte[] SecretSalt { get; set; }
		
		public BusinessUser BusinessUser { get; set; }
	}
}
