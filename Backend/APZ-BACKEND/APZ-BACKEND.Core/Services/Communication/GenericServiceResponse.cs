using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_BACKEND.Core.Services.Communication
{
	public class GenericServiceResponse<T> : BaseServiceResponse where T : class
	{
		public T Item { get; set; }

		private GenericServiceResponse(T item, bool success, string message) : base(success, message)
		{
			Item = item;
		}

		public GenericServiceResponse(T item) : this(item, true, string.Empty)
		{ }

		public GenericServiceResponse(string errorMessage) : this(null, false, errorMessage)
		{ }
	}
}
