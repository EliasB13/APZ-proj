﻿using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_BACKEND.Core.Helpers
{
	public enum ErrorCode
	{
		USER_NOT_FOUND = 100,
		CONTEXT_USER_NOT_FOUND = 101,

		EMPLOYEE_ALREADY_EXISTS = 102,
		EMPLOYEE_NOT_FOUND = 103,

		ROLE_ALREADY_EXISTS = 104,
		ROLE_NOT_FOUND = 105,
		EMPLOYEE_ALREADY_IN_ROLE = 106,
		EMPLOYEE_NOT_FOUND_IN_ROLE = 107,
		ITEM_ALREADY_IN_ROLE = 108,

		ITEM_NOT_FOUND = 109,
		ITEM_NOT_IN_BUSINESS = 110,
		ITEM_ALREADY_TAKEN = 111,
		ITEM_NOT_TAKEN_BY_CURRENT_USER = 112,

		NOT_BUSINESS_USER = 113,
		NOT_PRIVATE_USER = 114,

		WRONG_REQUEST_PARAMETERS = 115,

		USERNAME_OR_PASSWORD_INCORRECT = 116,

		FILE_NOT_FOUND = 117,

		NO_ACCESS = 900,
		COMMON_ERROR = 999
	}
}
