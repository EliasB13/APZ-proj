using APZ_BACKEND.Core.Dtos;
using APZ_BACKEND.Core.Entities;
using System.Threading.Tasks;

namespace APZ_BACKEND.Core.Services.Users
{
	public interface IUserService
	{
		Task<PrivateUser> AuthenticatePrivateAsync(string login, string password);
		Task<BusinessUser> AuthenticateBusinessAsync(string login, string password);
		Task<PrivateUser> GetByIdPrivateAsync(int id);
		Task<BusinessUser> GetByIdBusinessAsync(int id);
		Task<PrivateUser> RegisterPrivateAsync(RegisterPrivateRequest userDto);
		Task<BusinessUser> RegisterBusinessAsync(RegisterBusinessRequest userDto);
	}
}
