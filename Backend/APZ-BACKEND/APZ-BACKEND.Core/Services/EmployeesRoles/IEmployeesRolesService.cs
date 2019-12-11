using APZ_BACKEND.Core.Dtos.EmployeesRoles;
using APZ_BACKEND.Core.Entities;
using APZ_BACKEND.Core.Services.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APZ_BACKEND.Core.Services.EmployeesRoles
{
	public interface IEmployeesRolesService
	{
		Task<IEnumerable<EmployeesRoleDto>> GetBusinessEmployeesRoles(int businessUserId);
		Task<GenericServiceResponse<EmployeesRoleDto>> Create(int businessUserId, CreateEmployeesRoleDto employeesRoleDto);
		Task<GenericServiceResponse<EmployeesRole>> AddEmployeeToRole(int employeeId, int roleId);
		Task<GenericServiceResponse<EmployeesRole>> Update(EmployeesRoleDto updateEmployeesRole);
		Task<GenericServiceResponse<EmployeesRole>> Delete(int employeesRoleId);
		Task<GenericServiceResponse<EmployeesRole>> RemoveEmployeeFromRole(int employeesRoleId, int employeeId);
	}
}
