﻿using APZ_BACKEND.Core.Entities;
using APZ_BACKEND.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APZ_BACKEND.Infrastructure.Data.Repositories.SharedItems
{
	public class SharedItemsRepository : GenericEfRepository<SharedItem>, ISharedItemsRepository
	{
		public SharedItemsRepository(ApplicationContext dbContext) : base(dbContext)
		{
		}

		public async Task<bool> IsItemAvailableForUser(int itemId, int privateUserId)
		{
			return await dbContext.Employees
				.Include(e => e.EmployeesRole)
				.ThenInclude(e => e.EmployeeRoleItems)
				.Where(e => e.PrivateUserId == privateUserId)
				.AnyAsync(e => e.EmployeesRole.EmployeeRoleItems.Any(eri => eri.SharedItemId == itemId));
		}
	}
}
