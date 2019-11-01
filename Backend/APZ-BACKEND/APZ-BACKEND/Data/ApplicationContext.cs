﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APZ_BACKEND.Entities;
using Microsoft.EntityFrameworkCore;


namespace APZ_BACKEND.Data
{
	public class ApplicationContext : DbContext
	{
		public DbSet<BusinessUser> BusinessUsers { get; set; }
		public DbSet<PrivateUser> PrivateUsers { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<EmployeeRoleItem> EmployeeRoleItems { get; set; }
		public DbSet<EmployeesRole> EmployeesRoles { get; set; }
		public DbSet<ItemTaking> ItemTakings { get; set; }
		public DbSet<ItemTakingLine> ItemTakingLines { get; set; }
		public DbSet<SharedItem> SharedItems { get; set; }

		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{

		}
	}
}
