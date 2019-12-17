using APZ_BACKEND.Core.Dtos.SharedItems;
using APZ_BACKEND.Core.Entities;
using APZ_BACKEND.Core.Services.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APZ_BACKEND.Core.Services.Items
{
	public interface ISharedItemsService
	{
		Task<IEnumerable<SharedItemDto>> GetBusinessItems(int businessUserId);
		Task<IEnumerable<SharedItemDto>> GetBusinessItems(int businessUserId, int privateUserId);
		Task<IEnumerable<SharedRoleItemDto>> GetEmployeesRoleItems(int roleId, int businessUserId);
		Task<GenericServiceResponse<SharedItemDto>> GetItem(int businessUserId, int itemId);
		Task<GenericServiceResponse<SharedItemDto>> GetItemPrivateUser(int privatUserId, int itemId);
		Task<GenericServiceResponse<SharedItem>> AddItemToBusiness(int businessUserId, AddSharedItemRequest addItemDto);
		Task<GenericServiceResponse<SharedItemDto>> AddItemToEmployeesRole(int businessUserId, int itemId, int roleId);
		Task<GenericServiceResponse<SharedItem>> TakeItem(TakeItemRequest takeItemRequest);
		Task<GenericServiceResponse<SharedItem>> ReturnItem(ReturnItemRequest returnItemRequest);
		Task<GenericServiceResponse<SharedItem>> Update(UpdateSharedItemRequest sharedItemDto, int id, int businessUserId);
		Task<GenericServiceResponse<SharedItem>> Delete(int itemId, int businessUserId);
		Task<GenericServiceResponse<EmployeeRoleItem>> RemoveItemFromEmployeesRole(int roleItemId, int businessUserId);
	}
}
