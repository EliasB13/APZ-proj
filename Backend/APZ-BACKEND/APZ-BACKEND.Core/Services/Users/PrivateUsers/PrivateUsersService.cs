using System;
using System.Threading.Tasks;
using APZ_BACKEND.Core.Dtos.Auth;
using APZ_BACKEND.Core.Entities;
using APZ_BACKEND.Core.Exceptions;
using APZ_BACKEND.Core.Interfaces;

namespace APZ_BACKEND.Core.Services.Users.PrivateUsers
{
	public class PrivateUsersService : IPrivateUsersService
	{
		private readonly IAsyncRepository<PrivateUser> privateUserRepository;

		public PrivateUsersService(IAsyncRepository<PrivateUser> privateUserRepository)
		{
			this.privateUserRepository = privateUserRepository;
		}

		public async Task<PrivateUser> AuthenticatePrivateAsync(string login, string password)
		{
			if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
				return null;

				//user = await businessUserRepository.SingleOrDefaultAsync(u => u.Login == login);
			var user = await privateUserRepository.SingleOrDefaultAsync(u => u.Login == login);

			if (user == null)
				return null;

			if (!UsersExtensions.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
				return null;

			return user;
		}
		
		public async Task<PrivateUser> GetByIdAsync(int id)
		{
			return await privateUserRepository.GetByIdAsync(id);
		}
		
		public async Task<PrivateUser> RegisterPrivateAsync(RegisterPrivateRequest userDto)
		{
			if (string.IsNullOrWhiteSpace(userDto.Password))
				throw new AppException("Password is required");

			if (await privateUserRepository.AnyAsync(x => x.Login == userDto.Login))
				throw new AppException("Username \"" + userDto.Login + "\" is already taken");

			if (await privateUserRepository.AnyAsync(x => x.Email == userDto.Email))
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

			await privateUserRepository.AddAsync(user);

			return user;
		}
	}
}
