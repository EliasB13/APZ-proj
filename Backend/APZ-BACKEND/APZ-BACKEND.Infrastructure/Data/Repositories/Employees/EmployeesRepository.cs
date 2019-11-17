using APZ_BACKEND.Core.Entities;
using APZ_BACKEND.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace APZ_BACKEND.Infrastructure.Data.Repositories.Employees
{
	public class EmployeesRepository : EfRepository<Employee>, IEmployeesRepository
	{
		public EmployeesRepository(ApplicationContext dbContext) : base(dbContext)
		{
		}

		public async Task<IEnumerable<Employee>> GetEmployeesWithUsers(int businessUserId)
		{
			return await dbSet
				//.Include(e => e.BusinessUser)
				.Include(e => e.PrivateUser)
				.Where(e => e.BusinessUser.Id == businessUserId)
				.ToListAsync();
		}
	}
}
