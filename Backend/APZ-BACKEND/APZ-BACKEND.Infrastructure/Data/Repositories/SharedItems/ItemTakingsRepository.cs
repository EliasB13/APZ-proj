using APZ_BACKEND.Core.Entities;
using APZ_BACKEND.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APZ_BACKEND.Infrastructure.Data.Repositories.SharedItems
{
	public class ItemTakingsRepository : GenericEfRepository<ItemTaking>, IItemsTakingsRepository
	{
		public ItemTakingsRepository(ApplicationContext dbContext) : base(dbContext)
		{
		}

		public async Task<bool> IsItemTakenByUser(int privateUserId, int sharedItemId)
		{
			return await dbContext.ItemTakings
				.Include(it => it.ItemTakingLines)
				.Include(it => it.PrivateUser)
				.AnyAsync(it => it.PrivateUser.Id == privateUserId && it.ItemTakingLines.Any(itl => itl.SharedItemId == sharedItemId));
		}

		public async Task<ItemTaking> GetItemTakingByUserAndItem(int privateUserId, int sharedItemId)
		{
			return await dbContext.ItemTakings
				.Include(it => it.ItemTakingLines)
				.Include(it => it.PrivateUser)
				.SingleOrDefaultAsync(it => it.PrivateUser.Id == privateUserId && it.ItemTakingLines.Any(itl => itl.SharedItemId == sharedItemId));
		}
	}
}
