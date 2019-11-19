using APZ_BACKEND.Core.Dtos.Auth;
using APZ_BACKEND.Core.Dtos.Users;
using APZ_BACKEND.Core.Entities;
using APZ_BACKEND.Core.Exceptions;
using APZ_BACKEND.Core.Interfaces;
using APZ_BACKEND.Core.Mappers;
using APZ_BACKEND.Core.Services.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APZ_BACKEND.Core.Services.Users.BusinessUsers
{
	public class BusinessUsersService : IBusinessUsersService
	{
		private readonly IAsyncRepository<BusinessUser> usersRepository;

		public BusinessUsersService(IAsyncRepository<BusinessUser> businessUserRepository)
		{
			this.usersRepository = businessUserRepository;
		}

		public async Task<BusinessUser> AuthenticateBusinessAsync(string login, string password)
		{
			if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
				return null;

			var user = await usersRepository.SingleOrDefaultAsync(u => u.Login == login);

			if (user == null)
				return null;

			if (!UsersExtensions.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
				return null;

			return user;
		}

		public async Task<BusinessUser> GetByIdAsync(int id)
		{
			return await usersRepository.GetByIdAsync(id);
		}

		public async Task<BusinessUser> RegisterBusinessAsync(RegisterBusinessRequest userDto)
		{
			if (string.IsNullOrWhiteSpace(userDto.Password))
				throw new AppException("Password is required");

			if (await usersRepository.AnyAsync(x => x.Login == userDto.Login))
				throw new AppException("Username \"" + userDto.Login + "\" is already taken");

			if (await usersRepository.AnyAsync(x => x.Email == userDto.Email))
				throw new AppException("Email \"" + userDto.Email + "\" is already taken");

			byte[] passwordHash, passwordSalt;
			UsersExtensions.CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

			BusinessUser user = new BusinessUser()
			{
				Login = userDto.Login,
				Email = userDto.Email,
				CompanyName = userDto.CompanyName,
				PasswordHash = passwordHash,
				PasswordSalt = passwordSalt
			};

			await usersRepository.AddAsync(user);

			return user;
		}

		public async Task<GenericServiceResponse<BusinessUserProfile>> GetPublicProfile(int id)
		{
			var user = await usersRepository.GetByIdAsync(id);
			if (user == null)
				return new GenericServiceResponse<BusinessUserProfile>("User with specified id wasn't found.");

			return new GenericServiceResponse<BusinessUserProfile>(user.ToUserProfile());
		}

		public async Task<GenericServiceResponse<BusinessUserProfile>> GetPublicProfile(string login)
		{
			var user = await usersRepository.SingleOrDefaultAsync(bu => bu.Login == login);
			if (user == null)
				return new GenericServiceResponse<BusinessUserProfile>("User with specified id wasn't found.");

			return new GenericServiceResponse<BusinessUserProfile>(user.ToUserProfile());
		}

		public async Task<GenericServiceResponse<BusinessUserAccountData>> GetAccountData(int id)
		{
			var user = await usersRepository.GetByIdAsync(id);
			if (user == null)
				return new GenericServiceResponse<BusinessUserAccountData>("User with specified id wasn't found");

			return new GenericServiceResponse<BusinessUserAccountData>(user.ToAccountData());
		}

		public async Task<GenericServiceResponse<BusinessUser>> UpdateBusinessUser(UpdateBusinessUserRequest editData, int businessUserId)
		{
			var dbUser = await usersRepository.GetByIdAsync(businessUserId);
			if (dbUser == null)
				return new GenericServiceResponse<BusinessUser>("User with specified id wasn't found.");

			dbUser.UpdateUserFromDto(editData);
			await usersRepository.UpdateAsync(dbUser);

			return new GenericServiceResponse<BusinessUser>(dbUser);
		}

		public async Task<GenericServiceResponse<BusinessUser>> DeleteBusinessUser(int businessUserId)
		{
			var dbUser = await usersRepository.GetByIdAsync(businessUserId);
			if (dbUser == null)
				return new GenericServiceResponse<BusinessUser>("User with specified id wasn't found");

			await usersRepository.DeleteAsync(dbUser);

			return new GenericServiceResponse<BusinessUser>(dbUser);
		}
	}
}
