using APZ_BACKEND.Core.Dtos.Auth;
using APZ_BACKEND.Core.Dtos.Employee;
using APZ_BACKEND.Core.Dtos.Users;
using APZ_BACKEND.Core.Entities;
using APZ_BACKEND.Core.Services.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APZ_BACKEND.Core.Services.Users.BusinessUsers
{
	public interface IBusinessUsersService
	{
		Task<BusinessUser> RegisterBusinessAsync(RegisterBusinessRequest userDto);
		Task<BusinessUser> AuthenticateBusinessAsync(string login, string password);
		Task<BusinessUser> GetByIdAsync(int id);
		Task<GenericServiceResponse<BusinessUserProfile>> GetPublicProfile(int id);
		Task<GenericServiceResponse<BusinessUserProfile>> GetPublicProfile(string login);
		Task<GenericServiceResponse<BusinessUserAccountData>> GetAccountData(int id);
		Task<GenericServiceResponse<BusinessUserAccountData>> UpdateBusinessUser(UpdateBusinessUserRequest editBusinessUserDto, int businessUserId);
		Task<GenericServiceResponse<BusinessUser>> DeleteBusinessUser(int businessUserId);
		Task<GenericServiceResponse<BusinessUser>> UpdatePhotoPath(string path, int userId);
	}
}
