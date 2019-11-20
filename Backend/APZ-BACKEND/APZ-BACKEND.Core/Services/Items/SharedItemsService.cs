using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APZ_BACKEND.Core.Dtos.SharedItems;
using APZ_BACKEND.Core.Entities;
using APZ_BACKEND.Core.Interfaces;
using APZ_BACKEND.Core.Mappers;
using APZ_BACKEND.Core.Services.Communication;

namespace APZ_BACKEND.Core.Services.Items
{
	public class SharedItemsService : ISharedItemsService
	{
		private readonly IAsyncRepository<SharedItem> sharedItemsRepository;
		private readonly IAsyncRepository<BusinessUser> businessUsersRepository;
		private readonly IAsyncRepository<ItemTaking> itemTakingsRepository;
		private readonly IAsyncRepository<ItemTakingLine> itemTakingLinesRepository;

		public SharedItemsService(IAsyncRepository<SharedItem> sharedItemsRepository,
			IAsyncRepository<BusinessUser> businessUsersRepository,
			IAsyncRepository<ItemTaking> itemTakingsRepository,
			IAsyncRepository<ItemTakingLine> itemTakingLinesRepository)
		{
			this.sharedItemsRepository = sharedItemsRepository;
			this.businessUsersRepository = businessUsersRepository;
			this.itemTakingsRepository = itemTakingsRepository;
			this.itemTakingLinesRepository = itemTakingLinesRepository;
		}

		public async Task<GenericServiceResponse<SharedItem>> AddItemToBusiness(int businessUserId, AddSharedItemRequest addItemDto)
		{
			try
			{
				var businessUser = await businessUsersRepository.GetByIdAsync(businessUserId);
				if (businessUser == null)
					return new GenericServiceResponse<SharedItem>($"Business user with id: {businessUserId} wasn't found");

				var item = addItemDto.ToSharedItem(businessUser);

				await sharedItemsRepository.AddAsync(item);
				return new GenericServiceResponse<SharedItem>(item);
			}
			catch (Exception ex)
			{
				return new GenericServiceResponse<SharedItem>("Error | Adding item to business: " + ex.Message);
			}
		}

		public async Task<GenericServiceResponse<SharedItem>> Delete(int itemId, int businessUserId)
		{
			try
			{
				var item = await sharedItemsRepository.SingleOrDefaultWithIncludeAsync(si => si.Id == itemId, si => si.BusinessUser);
				if (item == null)
					return new GenericServiceResponse<SharedItem>($"Shared item with id: {itemId} wasn't found");

				if (item.BusinessUser.Id != businessUserId)
					return new GenericServiceResponse<SharedItem>("You don't have permissions");

				await sharedItemsRepository.DeleteAsync(item);
				return new GenericServiceResponse<SharedItem>(item);
			}
			catch (Exception ex)
			{
				return new GenericServiceResponse<SharedItem>("Error | Deleting shared item: " + ex.Message);
			}
		}

		public async Task<IEnumerable<SharedItemDto>> GetBusinessItems(int businessUserId)
		{
			var items = await sharedItemsRepository.ListAllAsync(si => si.BusinessUser.Id == businessUserId, si => si.BusinessUser);
			var itemTakingLines = await itemTakingLinesRepository.ListAllAsync();
			if (items.Count() > 0)
			{
				var itemDtos = items.Select(i =>
				{
					var isItemTaken = itemTakingLines.Any(itl => itl.SharedItemId == i.Id && itl.IsTaken);
					return i.ToDto(isItemTaken);
				}).ToList();

				return itemDtos;
			}

			return new List<SharedItemDto>();
		}

		public async Task<GenericServiceResponse<SharedItemDto>> GetItem(int businessUserId, int itemId)
		{
			var businessUser = await businessUsersRepository.GetByIdAsync(businessUserId);
			if (businessUser == null)
				return new GenericServiceResponse<SharedItemDto>($"Business user with id: {businessUserId} wasn't found");

			var item = await sharedItemsRepository.GetByIdAsync(itemId);
			if (item == null)
				return new GenericServiceResponse<SharedItemDto>($"Item with id: {itemId} wasn't found");

			var isTaken = await itemTakingLinesRepository.AnyAsync(itl => itl.SharedItemId == item.Id && itl.IsTaken);
			var itemDto = item.ToDto(isTaken);

			return new GenericServiceResponse<SharedItemDto>(itemDto);

		}

		public async Task<GenericServiceResponse<SharedItem>> Update(UpdateSharedItemRequest dto, int id, int businessUserId)
		{
			try
			{
				var item = await sharedItemsRepository.SingleOrDefaultWithIncludeAsync(si => si.Id == id, si => si.BusinessUser);
				if (item == null)
					return new GenericServiceResponse<SharedItem>($"Shared item with id: {id} wasn't found");
				
				if (item.BusinessUser.Id != businessUserId)
					return new GenericServiceResponse<SharedItem>("You don't have permissions");

				item.UpdateSharedItemFromDto(dto);
				await sharedItemsRepository.UpdateAsync(item);

				return new GenericServiceResponse<SharedItem>(item);
			}
			catch (Exception ex)
			{
				return new GenericServiceResponse<SharedItem>("Error | Updating shared item: " + ex.Message);
			}
		}
	}
}
