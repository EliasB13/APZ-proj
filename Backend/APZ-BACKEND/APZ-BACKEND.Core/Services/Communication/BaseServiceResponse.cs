using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_BACKEND.Core.Services.Communication
{
	public class BaseServiceResponse
	{
		public bool Success { get; protected set; }
		public string ErrorMessage { get; protected set; }

		public BaseServiceResponse(bool success, string message)
		{
			Success = success;
			ErrorMessage = message;
		}
	}
}
