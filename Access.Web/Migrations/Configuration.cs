using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Access.Data.DAL;
using Access.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Access.Web.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
	{
		private static readonly List<AccessEntity> DefaultAccesses = new List<AccessEntity>
		{
			new AccessEntity
			{
				Id = 1,
				Note = "Access by rdp",
				AccessTypeId = 1,
				Address = "google.com",
				Login = "login",
				Password = "Password",
				ProjectId = 1
			},
			new AccessEntity
			{
				Id = 2,
				Note = "",
				AccessTypeId = 1,
				Address = "google.com",
				Login = "123",
				Password = "321",
				ProjectId = 1
			},
			new AccessEntity
			{
				Id = 3,
				Note = "access note",
				AccessTypeId = 1,
				Address = "ya.com",
				Login = "444",
				Password = "admin",
				ProjectId = 2
			},
			new AccessEntity
			{
				Id = 4,
				Note = "access note",
				AccessTypeId = 3,
				Address = "asdasd.cu",
				Login = "login",
				Password = "pasdokqw",
				ProjectId = 2
			}
		};

		private static readonly List<ProjectEntity> DefaultProjects = new List<ProjectEntity>
		{
			new ProjectEntity
			{
				Id = 1,
				Title = "Awesome project",
				ClientId = 1
			},
			new ProjectEntity
			{
				Id = 2,
				Title = "BE FREE",
				ClientId = 2
			}
		};

		private static readonly List<ClientEntity> DefaultClients = new List<ClientEntity>
		{
			new ClientEntity
			{
				Id = 1,
				Email = "Email@email.com",
				Note = "some note",
				Phone = "21395912",
				Title = "Awesome company"
			},
			new ClientEntity
			{
				Id = 2,
				Email = "some@email.com",
				Note = "my note",
				Phone = "23152132131",
				Title = "COMPAYNE"
			}
		};

		private static readonly List<AccessTypeEntity> DefaultAccessTypes = new List<AccessTypeEntity>
		{
			new AccessTypeEntity
			{
				Id = 1,
				Title = "RDP"
			},
			new AccessTypeEntity
			{
				Id = 2,
				Title = "SSH"
			},
			new AccessTypeEntity
			{
				Id = 3,
				Title = "FTP"
			}
		};

		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
			ContextKey = "Access.Data.DAL.ApplicationDbContext";
		}

		protected override void Seed(ApplicationDbContext context)
		{
			var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
			var roleAdmin = new IdentityRole {Name = "Administrator"};
			var roleUser = new IdentityRole {Name = "User"};

			roleManager.Create(roleAdmin);
			roleManager.Create(roleUser);

			var password = "admin";
			var user = new ApplicationUser
			{
				Email = "admin@smail.ru",
				UserName = "admin"
			};
			CreateIdentity(userManager, roleAdmin, password, user);

			foreach (var client in DefaultClients)
				context.Set<ClientEntity>().AddOrUpdate(client);

			foreach (var accessType in DefaultAccessTypes)
				context.Set<AccessTypeEntity>().AddOrUpdate(accessType);

			context.SaveChanges();

			foreach (var project in DefaultProjects)
				context.Set<ProjectEntity>().AddOrUpdate(project);

			context.SaveChanges();

			foreach (var access in DefaultAccesses)
				context.Set<AccessEntity>().AddOrUpdate(access);

			context.SaveChanges();

			base.Seed(context);
		}

		private static IdentityResult CreateIdentity(ApplicationUserManager userManager, IdentityRole role1, string password,
			ApplicationUser user)
		{
			var result = userManager.Create(user, password);

			// если создание пользователя прошло успешно
			if (result.Succeeded)
				userManager.AddToRole(user.Id, role1.Name);

			return result;
		}
	}
}