using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APZ_BACKEND.Core.Dtos.Auth;
using APZ_BACKEND.Core.Dtos.Users;
using APZ_BACKEND.Core.Entities;
using APZ_BACKEND.Core.Exceptions;
using APZ_BACKEND.Core.Interfaces;
using APZ_BACKEND.Core.Mappers;
using APZ_BACKEND.Core.Services.Communication;

namespace APZ_BACKEND.Core.Services.Users.PrivateUsers
{
	public class PrivateUsersService : IPrivateUsersService
	{
		private readonly IAsyncRepository<PrivateUser> usersRepository;
		private readonly IAsyncRepository<Employee> employeesRepository;
		private readonly IAsyncRepository<BusinessUser> businessUsersRepository;

		public PrivateUsersService(IAsyncRepository<PrivateUser> privateUserRepository,
			IAsyncRepository<Employee> employeesRepository,
			IAsyncRepository<BusinessUser> businessUsersRepository)
		{
			this.usersRepository = privateUserRepository;
			this.employeesRepository = employeesRepository;
			this.businessUsersRepository = businessUsersRepository;
		}

		public async Task<PrivateUser> AuthenticatePrivateAsync(string login, string password)
		{
			if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
				return null;

				//user = await businessUserRepository.SingleOrDefaultAsync(u => u.Login == login);
			var user = await usersRepository.SingleOrDefaultAsync(u => u.Login == login);

			if (user == null)
				return null;

			if (!UsersExtensions.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
				return null;

			return user;
		}

		public async Task<PrivateUser> RegisterPrivateAsync(RegisterPrivateRequest userDto)
		{
			if (string.IsNullOrWhiteSpace(userDto.Password))
				throw new AppException("Password is required");

			if (await usersRepository.AnyAsync(x => x.Login == userDto.Login))
				throw new AppException("Username \"" + userDto.Login + "\" is already taken");

			if (await usersRepository.AnyAsync(x => x.Email == userDto.Email))
				throw new AppException("Email \"" + userDto.Email + "\" is already taken");

			byte[] passwordHash, passwordSalt;
			UsersExtensions.CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

			PrivateUser user = new PrivateUser()
			{
				Login = userDto.Login,
				FirstName = userDto.FirstName,
				LastName = userDto.LastName,
				Email = userDto.Email,
				PasswordHash = passwordHash,
				PasswordSalt = passwordSalt
			};

			await usersRepository.AddAsync(user);

			return user;
		}
		
		public async Task<PrivateUser> GetByIdAsync(int id)
		{
			return await usersRepository.GetByIdAsync(id);
		}

		public async Task<GenericServiceResponse<PrivateUserAccountData>> GetAccountData(int id)
		{
			var user = await usersRepository.GetByIdAsync(id);
			if (user == null)
				return new GenericServiceResponse<PrivateUserAccountData>("User with specified id wasn't found");

			return new GenericServiceResponse<PrivateUserAccountData>(user.ToAccountData());
		}

		public async Task<GenericServiceResponse<PrivateUserProfile>> GetPublicProfile(int id)
		{
			var user = await usersRepository.GetByIdAsync(id);
			if (user == null)
				return new GenericServiceResponse<PrivateUserProfile>("User with specified id wasn't found.");

			return new GenericServiceResponse<PrivateUserProfile>(user.ToUserProfile());
		}

		public async Task<GenericServiceResponse<PrivateUserProfile>> GetPublicProfile(string login)
		{
			var user = await usersRepository.SingleOrDefaultAsync(pu => pu.Login == login);
			if (user == null)
				return new GenericServiceResponse<PrivateUserProfile>("User with specified id wasn't found.");

			return new GenericServiceResponse<PrivateUserProfile>(user.ToUserProfile());
		}

		public async Task<GenericServiceResponse<PrivateUserAccountData>> UpdatePrivateUser(UpdatePrivateUserRequest editData, int businessUserId)
		{
			var dbUser = await usersRepository.GetByIdAsync(businessUserId);
			if (dbUser == null)
				return new GenericServiceResponse<PrivateUserAccountData>("User with specified id wasn't found.");

			dbUser.UpdateUserFromDto(editData);
			await usersRepository.UpdateAsync(dbUser);

			return new GenericServiceResponse<PrivateUserAccountData>(dbUser.ToAccountData());
		}

		public async Task<GenericServiceResponse<PrivateUser>> DeletePrivateUser(int businessUserId)
		{
			var dbUser = await usersRepository.GetByIdAsync(businessUserId);
			if (dbUser == null)
				return new GenericServiceResponse<PrivateUser>("User with specified id wasn't found");

			await usersRepository.DeleteAsync(dbUser);

			return new GenericServiceResponse<PrivateUser>(dbUser);
		}

		public async Task<IEnumerable<BusinessUserProfile>> GetAvailableServices(int privateUserId)
		{
			var userEmployments = await employeesRepository.ListAllAsync(e => e.PrivateUserId == privateUserId);
			var userEmploymentsBusinessIds = userEmployments.Select(ue => ue.BusinessUserId);
			if (userEmployments.Count > 0)
			{
				var businessUsers = await businessUsersRepository
					.ListAllAsync(bu => userEmploymentsBusinessIds.Contains(bu.Id));
				var dtos = businessUsers.Select(bu => bu.ToUserProfile());
				
				return dtos;
			}

			return new List<BusinessUserProfile>();
		}

		public async Task<GenericServiceResponse<PrivateUser>> UpdatePhotoPath(string path, int userId)
		{
			try
			{
				var user = await usersRepository.GetByIdAsync(userId);
				if (user == null)
					return new GenericServiceResponse<PrivateUser>($"Private user with id: {userId} wasn't found");

				user.Photo = path;
				await usersRepository.UpdateAsync(user);
				return new GenericServiceResponse<PrivateUser>(user);
			}
			catch (Exception ex)
			{
				return new GenericServiceResponse<PrivateUser>("Error | Updating photo path private user: " + ex.Message);
			}
		}
	}
}
