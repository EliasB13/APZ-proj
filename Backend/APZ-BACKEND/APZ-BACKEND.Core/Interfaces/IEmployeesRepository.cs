using APZ_BACKEND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APZ_BACKEND.Core.Interfaces
{
	public interface IEmployeesRepository : IAsyncRepository<Employee>
	{
		Task<IEnumerable<Employee>> GetEmployeesWithUsers(int businessUserId);
	}
}
