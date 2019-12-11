using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using APZ_BACKEND.Core.Helpers;
using APZ_BACKEND.Core.Interfaces;
using APZ_BACKEND.Core.Services.Employees;
using APZ_BACKEND.Core.Services.EmployeesRoles;
using APZ_BACKEND.Core.Services.Items;
using APZ_BACKEND.Core.Services.Users;
using APZ_BACKEND.Core.Services.Users.BusinessUsers;
using APZ_BACKEND.Core.Services.Users.PrivateUsers;
using APZ_BACKEND.Infrastructure.Data;
using APZ_BACKEND.Infrastructure.Data.Repositories;
using APZ_BACKEND.Infrastructure.Data.Repositories.Employees;
using APZ_BACKEND.Infrastructure.Data.Repositories.SharedItems;
using APZ_BACKEND.Infrastructure.Services.Users;
using APZ_BACKEND.Presentation.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace APZ_BACKEND.Presentation
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			});
			services.AddDbContext<ApplicationContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DevSqlServerConn")));
			services.AddControllers();

			ConfigureJwtAuth(services);
			ConfigureSwagger(services);

			ConfigureInjection(services);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}


			app.UseRouting();



			app.UseCors("CorsPolicy");

			app.UseAuthentication();
			app.UseAuthorization();
			//app.UseCors("CorsPolicy");

			app.UseHttpsRedirection();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseStaticFiles(new StaticFileOptions()
			{
				FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
				RequestPath = new PathString("/Resources")
			});

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "SCNURE-BACKEND");
				c.RoutePrefix = string.Empty;
			});
		}

		private void ConfigureJwtAuth(IServiceCollection services)
		{
			var appSettingsSection = Configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingsSection);

			var appSettings = appSettingsSection.Get<AppSettings>();
			var key = Encoding.ASCII.GetBytes(appSettings.Secret);
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.Events = new JwtBearerEvents
				{
					OnTokenValidated = async context =>
					{
						var privateUsersService = context.HttpContext.RequestServices.GetRequiredService<IPrivateUsersService>();
						var businessUsersService = context.HttpContext.RequestServices.GetRequiredService<IBusinessUsersService>();

						var userId = ContextAuthHelper.GetUserIdFromClaim(context.Principal.Identity.Name);
						var isBusinessUser = ContextAuthHelper.IsBusinessUser(context.Principal.Claims);

						if (isBusinessUser)
						{
							var user = await businessUsersService.GetByIdAsync(userId);
							if (user == null)
								context.Fail("Unauthorized");
						} else
						{
							var user = await privateUsersService.GetByIdAsync(userId);
							if (user == null)
								context.Fail("Unauthorized");
						}
						await Task.CompletedTask;
					}
				};
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});
		}

		private void ConfigureSwagger(IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo() { Title = "APZ-BACKEND", Version = "v.0.0.1" });


				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
					Name = "Authorization",
					BearerFormat = "JWT",
					Type = SecuritySchemeType.ApiKey,
					In = ParameterLocation.Header,
					Scheme = "Bearer"
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement {
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					new string[] { }
					}
				});
			});
		}

		private void ConfigureInjection(IServiceCollection services)
		{
			services.AddScoped(typeof(IAsyncRepository<>), typeof(GenericEfRepository<>));
			services.AddScoped<IEmployeesRepository, EmployeesRepository>();
			services.AddScoped<ISharedItemsRepository, SharedItemsRepository>();
			services.AddScoped<IItemsTakingsRepository, ItemTakingsRepository>();

			services.AddScoped<IPrivateUsersService, PrivateUsersService>();
			services.AddScoped<IBusinessUsersService, BusinessUsersService>();
			services.AddScoped<IEmployeesService, EmployeesService>();
			services.AddScoped<IEmployeesRolesService, EmployeesRolesService>();
			services.AddScoped<ISharedItemsService, SharedItemsService>();
			services.AddScoped<IImageService, ImagesService>();
		}
	}
}
