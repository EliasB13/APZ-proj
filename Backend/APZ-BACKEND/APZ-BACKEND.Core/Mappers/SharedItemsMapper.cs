using APZ_BACKEND.Core.Dtos.SharedItems;
using APZ_BACKEND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_BACKEND.Core.Mappers
{
	public static class SharedItemsMapper
	{
		public static SharedItem ToSharedItem(this AddSharedItemRequest sharedItemDto, BusinessUser businessUser)
		{
			return new SharedItem
			{
				Name = sharedItemDto.Name,
				Description = sharedItemDto.Description,
				BusinessUser = businessUser
			};
		}

		public static SharedItemDto ToDto(this SharedItem sharedItem, bool isTaken)
		{
			return new SharedItemDto
			{
				Name = sharedItem.Name,
				Description = sharedItem.Description,
				Id = sharedItem.Id,
				IsTaken = isTaken
			};
		}

		public static ReaderItemDto ToDto(this SharedItem sharedItem)
		{
			return new ReaderItemDto
			{
				Description = sharedItem.Description,
				Name = sharedItem.Name,
				Rfid = sharedItem.RfidNumber
			};
		}

		public static void UpdateSharedItemFromDto(this SharedItem item, UpdateSharedItemRequest dto)
		{
			item.Name = dto.Name;
			item.Description = dto.Description;
		}

		public static SharedRoleItemDto ToDto(this SharedItem sharedItem, bool isTaken, int roleItemId, int roleId)
		{
			return new SharedRoleItemDto
			{
				Name = sharedItem.Name,
				Description = sharedItem.Description,
				SharedItemId = sharedItem.Id,
				IsTaken = isTaken,
				RoleId = roleId,
				RoleItemId = roleItemId
			};
		}
	}
}
