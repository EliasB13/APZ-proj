using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APZ_BACKEND.Core.Dtos;
using APZ_BACKEND.Core.Entities;
using APZ_BACKEND.Core.Exceptions;
using APZ_BACKEND.Core.Helpers;
using APZ_BACKEND.Core.Interfaces;

namespace APZ_BACKEND.Core.Services.Users
{
	public class UserService : IUserService
	{
		private readonly IAsyncRepository<PrivateUser> privateUserRepository;
		private readonly IAsyncRepository<BusinessUser> businessUserRepository;

		public UserService(IAsyncRepository<PrivateUser> privateUserRepository, IAsyncRepository<BusinessUser> businessUserRepository)
		{
			this.privateUserRepository = privateUserRepository;
			this.businessUserRepository = businessUserRepository;
		}

		public async Task<PrivateUser> AuthenticatePrivateAsync(string login, string password)
		{
			if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
				return null;

				//user = await businessUserRepository.SingleOrDefaultAsync(u => u.Login == login);
			var user = await privateUserRepository.SingleOrDefaultAsync(u => u.Login == login);

			if (user == null)
				return null;

			if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
				return null;

			return user;
		}

		public async Task<BusinessUser> AuthenticateBusinessAsync(string login, string password)
		{
			if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
				return null;

			var user = await businessUserRepository.SingleOrDefaultAsync(u => u.Login == login);

			if (user == null)
				return null;

			if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
				return null;

			return user;
		}

		public async Task<PrivateUser> GetByIdPrivateAsync(int id)
		{
			return await privateUserRepository.GetByIdAsync(id);
		}

		public async Task<BusinessUser> GetByIdBusinessAsync(int id)
		{
			return await businessUserRepository.GetByIdAsync(id);
		}

		public async Task<PrivateUser> RegisterPrivateAsync(RegisterPrivateRequest userDto)
		{
			if (string.IsNullOrWhiteSpace(userDto.Password))
				throw new AppException("Password is required");

			if (await privateUserRepository.AnyAsync(x => x.Login == userDto.Login))
				throw new AppException("Username \"" + userDto.Login + "\" is already taken");

			byte[] passwordHash, passwordSalt;
			CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

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

		public async Task<BusinessUser> RegisterBusinessAsync(RegisterBusinessRequest userDto)
		{
			if (string.IsNullOrWhiteSpace(userDto.Password))
				throw new AppException("Password is required");

			if (await businessUserRepository.AnyAsync(x => x.Login == userDto.Login))
				throw new AppException("Username \"" + userDto.Login + "\" is already taken");

			byte[] passwordHash, passwordSalt;
			CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

			BusinessUser user = new BusinessUser()
			{
				Login = userDto.Login,
				Email = userDto.Email,
				CompanyName = userDto.CompanyName,
				PasswordHash = passwordHash,
				PasswordSalt = passwordSalt
			};

			await businessUserRepository.AddAsync(user);

			return user;
		}

		private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			if (password == null) throw new ArgumentNullException("password");
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

			using (var hmac = new System.Security.Cryptography.HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}

		private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
		{
			if (password == null) throw new ArgumentNullException("password");
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
			if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
			if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

			using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				for (int i = 0; i < computedHash.Length; i++)
				{
					if (computedHash[i] != storedHash[i]) return false;
				}
			}

			return true;
		}
	}
}
