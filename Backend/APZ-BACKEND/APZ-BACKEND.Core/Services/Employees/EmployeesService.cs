using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APZ_BACKEND.Core.Dtos.Employee;
using APZ_BACKEND.Core.Entities;
using APZ_BACKEND.Core.Interfaces;
using APZ_BACKEND.Core.Mappers;
using APZ_BACKEND.Core.Services.Communication;

namespace APZ_BACKEND.Core.Services.Employees
{
	public class EmployeesService : IEmployeesService
	{
		private readonly IEmployeesRepository employeesRepository;
		private readonly IAsyncRepository<BusinessUser> businessUsersRepository;
		private readonly IAsyncRepository<PrivateUser> privateUsersRepository;
		private readonly IAsyncRepository<EmployeesRole> rolesRepository;

		public EmployeesService(IEmployeesRepository employeesRepository,
			IAsyncRepository<BusinessUser> businessUsersRepository,
			IAsyncRepository<PrivateUser> privateUsersRepository,
			IAsyncRepository<EmployeesRole> rolesRepository)
		{
			this.employeesRepository = employeesRepository;
			this.businessUsersRepository = businessUsersRepository;
			this.privateUsersRepository = privateUsersRepository;
			this.rolesRepository = rolesRepository;
		}

		public async Task<IEnumerable<EmployeeDto>> GetBusinessEmployees(int businessUserId)
		{
			var employees = await employeesRepository.GetEmployeesWithUsersAndRoles(businessUserId);
			if (employees.Count() > 0)
			{
				var businessUser = await businessUsersRepository.SingleOrDefaultAsync(bu => bu.Id == businessUserId);
				var employeesDto = employees.Select(e => e.ToEmployeeDto(businessUser.CompanyName));

				return employeesDto;
			}
			return new List<EmployeeDto>();
		}

		public async Task<GenericServiceResponse<Employee>> AddEmployee(int businessUserId, string login)
		{
			var privateUser = await privateUsersRepository.SingleOrDefaultAsync(pu => pu.Login == login);
			if (privateUser == null)
				return new GenericServiceResponse<Employee>("User \"" + login + "\" was not found.");

			var businessUser = await businessUsersRepository.SingleOrDefaultAsync(bu => bu.Id == businessUserId);
			if (businessUser == null)
				return new GenericServiceResponse<Employee>("BusinessUser was not found.");

			var isEmployeeExits = await employeesRepository.AnyAsync(e => e.PrivateUser.Id == privateUser.Id && e.BusinessUser.Id == businessUser.Id);
			if (isEmployeeExits)
				return new GenericServiceResponse<Employee>("Such employee already exists");

			var employee = new Employee
			{
				BusinessUser = businessUser,
				EmployeesRole = null,
				PrivateUser = privateUser
			};

			await employeesRepository.AddAsync(employee);
			return new GenericServiceResponse<Employee>(employee);
		}

		public async Task<GenericServiceResponse<Employee>> DeleteEmployee(int employeeId)
		{
			var employee = await employeesRepository.GetByIdAsync(employeeId);
			if (employee == null)
				return new GenericServiceResponse<Employee>("Employee with specified id wasn't found");

			await employeesRepository.DeleteAsync(employee);

			return new GenericServiceResponse<Employee>(employee);
		}
	}
}
