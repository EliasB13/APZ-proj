using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APZ_BACKEND.Core.Dtos.EmployeesRoles;
using APZ_BACKEND.Core.Entities;
using APZ_BACKEND.Core.Interfaces;
using APZ_BACKEND.Core.Mappers;
using APZ_BACKEND.Core.Services.Communication;

namespace APZ_BACKEND.Core.Services.EmployeesRoles
{
	public class EmployeesRolesService : IEmployeesRolesService
	{
		private readonly IAsyncRepository<EmployeesRole> employeesRoleRepository;
		private readonly IAsyncRepository<Employee> employeesRepository;

		public EmployeesRolesService(IAsyncRepository<EmployeesRole> employeesRoleRepository,
			IAsyncRepository<Employee> employeesRepository)
		{
			this.employeesRoleRepository = employeesRoleRepository;
			this.employeesRepository = employeesRepository;
		}

		public async Task<GenericServiceResponse<EmployeesRole>> AddEmployeeToRole(int employeeId, int roleId)
		{
			try
			{
				var role = await employeesRoleRepository.SingleOrDefaultAsync(er => er.Id == roleId);
				if (role == null)
					return new GenericServiceResponse<EmployeesRole>($"Role with id: {roleId} wasn't found");

				var employee = await employeesRepository.SingleOrDefaultAsync(e => e.Id == employeeId);
				if (employee == null)
					return new GenericServiceResponse<EmployeesRole>($"Employee with id: {employeeId} wasn't found");

				var employeeInRole = await employeesRepository.AnyAsync(e => e.EmployeesRole.Id == roleId);
				if (employeeInRole)
					return new GenericServiceResponse<EmployeesRole>($"Employee with id: {employeeId} already in role with id: {roleId}");

				employee.EmployeesRole = role;
				await employeesRepository.UpdateAsync(employee);

				return new GenericServiceResponse<EmployeesRole>(role);
			}
			catch (Exception ex)
			{
				return new GenericServiceResponse<EmployeesRole>("Adding employee to role: " + ex.Message);
			}
		}

		public async Task<GenericServiceResponse<EmployeesRole>> Create(int businessUserId, CreateEmployeesRoleDto employeesRoleDto)
		{
			try
			{
				var employeesRole = employeesRoleDto.ToRoleFromDto();
				employeesRole.BusinessUser.Id = businessUserId;

				await employeesRoleRepository.AddAsync(employeesRole);

				return new GenericServiceResponse<EmployeesRole>(employeesRole);
			}
			catch (Exception ex)
			{
				return new GenericServiceResponse<EmployeesRole>("Creating employees role: " + ex.Message);
			}
		}

		public async Task<GenericServiceResponse<EmployeesRole>> Delete(int employeesRoleId)
		{
			try
			{
				var employeesRole = await employeesRoleRepository.SingleOrDefaultAsync(er => er.Id == employeesRoleId);
				if (employeesRole == null)
					return new GenericServiceResponse<EmployeesRole>($"Employees role with id: {employeesRoleId} wasn't found");

				await employeesRoleRepository.DeleteAsync(employeesRole);
				return new GenericServiceResponse<EmployeesRole>(employeesRole);
			}
			catch (Exception ex)
			{
				return new GenericServiceResponse<EmployeesRole>("Deleting employees role: " + ex.Message);
			}
		}

		public async Task<IEnumerable<EmployeesRoleDto>> GetBusinessEmployeesRoles(int businessUserId)
		{
			var roles = await employeesRoleRepository.ListAllAsync();
			var roleDtos = roles.Select(r => r.ToDto());

			return roleDtos;
		}

		public async Task<GenericServiceResponse<EmployeesRole>> RemoveEmployeeFromRole(int employeesRoleId, int employeeId)
		{
			try
			{
				var role = await employeesRoleRepository.SingleOrDefaultAsync(er => er.Id == employeesRoleId);
				if (role == null)
					return new GenericServiceResponse<EmployeesRole>($"Role with id: {employeesRoleId} wasn't found");

				var employee = await employeesRepository.SingleOrDefaultAsync(e => e.Id == employeeId);
				if (employee == null)
					return new GenericServiceResponse<EmployeesRole>($"Employee with id: {employeeId} wasn't found");

				var employeeInRole = await employeesRepository.AnyAsync(e => e.EmployeesRole.Id == employeesRoleId);
				if (!employeeInRole)
					return new GenericServiceResponse<EmployeesRole>($"Employee with id: {employeeId} isn't in role with id: {employeesRoleId}");

				employee.EmployeesRole = null;
				await employeesRepository.UpdateAsync(employee);

				return new GenericServiceResponse<EmployeesRole>(role);
			}
			catch (Exception ex)
			{
				return new GenericServiceResponse<EmployeesRole>("Removing employee from role: " + ex.Message);
			}
		}

		public async Task<GenericServiceResponse<EmployeesRole>> Update(EmployeesRoleDto updateEmployeesRole)
		{
			try
			{
				var employeesRole = await employeesRoleRepository.GetByIdAsync(updateEmployeesRole.Id);
				if (employeesRole == null)
					return new GenericServiceResponse<EmployeesRole>($"EmployeesRole with id: {updateEmployeesRole.Id} wasn't found");

				employeesRole.UpdateRoleFromDto(updateEmployeesRole);

				return new GenericServiceResponse<EmployeesRole>(employeesRole);
			}
			catch (Exception ex)
			{
				return new GenericServiceResponse<EmployeesRole>("Updating employees role: " + ex.Message);
			}
		}
	}
}
