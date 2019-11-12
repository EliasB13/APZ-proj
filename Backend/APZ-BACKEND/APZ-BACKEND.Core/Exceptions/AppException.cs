using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace APZ_BACKEND.Core.Exceptions
{
	public class AppException : Exception
	{
		public AppException() : base() { }

		public AppException(string message) : base(message) { }

		public AppException(string message, params object[] args)
			: base(string.Format(CultureInfo.CurrentCulture, message, args))
		{
		}
	}
}
