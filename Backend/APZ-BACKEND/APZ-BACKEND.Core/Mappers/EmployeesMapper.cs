using APZ_BACKEND.Core.Dtos.Employee;
using APZ_BACKEND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_BACKEND.Core.Mappers
{
	public static class EmployeesMapper
	{
		public static EmployeeDto ToEmployeeDto(this Employee employeeDto, string companyName)
		{
			return new EmployeeDto
			{
				Company = companyName,
				EmployeeId = employeeDto.Id,
				FirstName = employeeDto.PrivateUser.FirstName,
				LastName = employeeDto.PrivateUser.LastName,
				Login = employeeDto.PrivateUser.Login,
				UserId = employeeDto.PrivateUser.Id
			};
		}
	}
}
