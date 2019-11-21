using APZ_BACKEND.Core.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace APZ_BACKEND.Presentation.Helpers
{
	public static class ContextAuthHelper
	{
		public static int GetUserIdFromClaim(string nameClaim)
		{
			return int.Parse(nameClaim);
		}

		public static bool IsBusinessUser(IEnumerable<Claim> claims)
		{
			return int.Parse(claims
				.Where(c => c.Type == ClaimTypes.Role)
				.FirstOrDefault().Value) == (int)UserType.BusinessUser ? true : false;
		}
	}
}
