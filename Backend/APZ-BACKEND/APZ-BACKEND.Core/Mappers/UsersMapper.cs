using APZ_BACKEND.Core.Dtos.Users;
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
				Phone = user.Phone,
				Photo = user.Photo
			};
		}

		public static PrivateUserProfile ToUserProfile(this PrivateUser user)
		{
			return new PrivateUserProfile
			{
				Email = user.Email,
				FirstName = user.FirstName,
				Id = user.Id,
				LastName = user.LastName,
				Login = user.Login,
				Phone = user.Phone,
				Photo = user.Photo
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

		public static PrivateUserAccountData ToAccountData(this PrivateUser user)
		{
			return new PrivateUserAccountData
			{
				Email = user.Email,
				FirstName = user.FirstName,
				Id = user.Id,
				LastName = user.LastName,
				Login = user.Login,
				Phone = user.Phone,
				Photo = user.Photo,
				Rfid = user.RfidNumber
			};
		}

		public static void UpdateUserFromDto(this BusinessUser user, UpdateBusinessUserRequest userData)
		{
			user.Address = userData.Address;
			user.CompanyName = userData.CompanyName;
			user.Description = userData.Description;
			user.Email = userData.Email;
			user.Login = userData.Login;
			user.Phone = userData.Phone;
		}

		public static void UpdateUserFromDto(this PrivateUser user, UpdatePrivateUserRequest userData)
		{
			user.Email = userData.Email;
			user.FirstName = userData.FirstName;
			user.LastName = userData.LastName;
			user.Login = userData.Login;
			user.Phone = userData.Phone;
		}
	}
}
