using APZ_BACKEND.Core.Dtos.SharedItems;
using APZ_BACKEND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_BACKEND.Core.Mappers
{
	public static class SharedItemsMapper
	{
		public static SharedItem ToSharedItem(this AddSharedItemRequest sharedItemDto)
		{
			return new SharedItem
			{
				Name = sharedItemDto.Name,
				Description = sharedItemDto.Description
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

		public static void UpdateSharedItemFromDto(this SharedItem item, UpdateSharedItemRequest dto)
		{
			item.Name = dto.Name;
			item.Description = dto.Description;
		}
	}
}
