using APZ_BACKEND.Core.Dtos.EmployeesRoles;
using APZ_BACKEND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_BACKEND.Core.Mappers
{
	public static class EmployeesRoleMapper
	{
		public static EmployeesRoleDto ToDto(this EmployeesRole role)
		{
			return new EmployeesRoleDto
			{
				Description = role.Description,
				Id = role.Id,
				Name = role.Name
			};
		}

		public static EmployeesRole ToRoleFromDto(this CreateEmployeesRoleDto dto)
		{
			return new EmployeesRole
			{
				Description = dto.Description,
				Name = dto.Name
			};
		}

		public static void UpdateRoleFromDto(this EmployeesRole role, EmployeesRoleDto dto)
		{
			role.Name = dto.Name;
			role.Description = dto.Description;
		}
	}
}
