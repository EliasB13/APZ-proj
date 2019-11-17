using APZ_BACKEND.Core.Dtos.Auth;
using APZ_BACKEND.Core.Entities;
using System.Threading.Tasks;

namespace APZ_BACKEND.Core.Services.Users.PrivateUsers
{
	public interface IPrivateUsersService
	{
		Task<PrivateUser> AuthenticatePrivateAsync(string login, string password);
		Task<PrivateUser> GetByIdAsync(int id);
		Task<PrivateUser> RegisterPrivateAsync(RegisterPrivateRequest userDto);
	}
}
