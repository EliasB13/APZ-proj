﻿using APZ_BACKEND.Core.Dtos.Users;
using APZ_BACKEND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_BACKEND.Core.Mappers
{
	public static class UsersMapper
	{
		public static BusinessUserProfile ToUserProfile(this BusinessUser user)
		{
			return new BusinessUserProfile
			{
				Address = user.Address,
				CompanyName = user.CompanyName,
				Description = user.Description,
				Email = user.Email,
				Id = user.Id,
				Login = user.Login,
				Phone = user.Phone
			};
		}

		public static BusinessUserAccountData ToAccountData(this BusinessUser user)
		{
			return new BusinessUserAccountData
			{
				Address = user.Address,
				CompanyName = user.CompanyName,
				Description = user.Description,
				Email = user.Email,
				Id = user.Id,
				Login = user.Login,
				Phone = user.Phone
			};
		}
	}
}
