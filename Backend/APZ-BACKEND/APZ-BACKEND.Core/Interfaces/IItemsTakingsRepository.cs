using APZ_BACKEND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APZ_BACKEND.Core.Interfaces
{
	public interface IItemsTakingsRepository : IAsyncRepository<ItemTaking>
	{
		Task<ItemTaking> GetItemTakingByUserAndItem(int privateUserId, int sharedItemId);
	}
}
