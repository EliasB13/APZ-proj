using APZ_BACKEND.Core.Dtos.Employee;
using APZ_BACKEND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_BACKEND.Core.Mappers
{
	public static class EmployeesMapper
	{
		public static EmployeeDto ToEmployeeDto(this Employee employee, string companyName)
		{
			return new EmployeeDto
			{
				Company = companyName,
				EmployeeId = employee.Id,
				FirstName = employee.PrivateUser.FirstName,
				LastName = employee.PrivateUser.LastName,
				Login = employee.PrivateUser.Login,
				UserId = employee.PrivateUser.Id,
				Role = employee.EmployeesRole == null ? null : employee.EmployeesRole.ToDto()
			};
		}
	}
}
