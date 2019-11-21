using APZ_BACKEND.Core.Dtos.Auth;
using APZ_BACKEND.Core.Dtos.Users;
using APZ_BACKEND.Core.Entities;
using APZ_BACKEND.Core.Services.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APZ_BACKEND.Core.Services.Users.PrivateUsers
{
	public interface IPrivateUsersService
	{
		Task<PrivateUser> AuthenticatePrivateAsync(string login, string password);
		Task<PrivateUser> GetByIdAsync(int id);
		Task<PrivateUser> RegisterPrivateAsync(RegisterPrivateRequest userDto);
		Task<GenericServiceResponse<PrivateUserProfile>> GetPublicProfile(int id);
		Task<GenericServiceResponse<PrivateUserProfile>> GetPublicProfile(string login);
		Task<GenericServiceResponse<PrivateUserAccountData>> GetAccountData(int id);
		Task<GenericServiceResponse<PrivateUser>> UpdatePrivateUser(UpdatePrivateUserRequest editBusinessUserDto, int businessUserId);
		Task<GenericServiceResponse<PrivateUser>> DeletePrivateUser(int privateUserId);
		Task<IEnumerable<BusinessUserProfile>> GetAvailableServices(int privateUserId);
		Task<GenericServiceResponse<PrivateUser>> UpdatePhotoPath(string path, int userId);
	}
}
