﻿using APZ_BACKEND.Core.Dtos.SharedItems;
using APZ_BACKEND.Core.Entities;
using APZ_BACKEND.Core.Services.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APZ_BACKEND.Core.Services.Items
{
	public interface IItemsService
	{
		Task<IEnumerable<SharedItemDto>> GetBusinessItems(int businessUserId);
		Task<GenericServiceResponse<SharedItemDto>> GetItem(int businessUserId, int itemId);
		Task<GenericServiceResponse<SharedItem>> AddItemToBusiness(int businessUserId, AddSharedItemRequest addItemDto);
		Task<GenericServiceResponse<SharedItem>> Update(UpdateSharedItemRequest sharedItemDto);
		Task<GenericServiceResponse<SharedItem>> Delete(int itemId);
	}
}
