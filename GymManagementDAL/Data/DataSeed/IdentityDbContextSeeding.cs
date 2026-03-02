using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.DataSeed
{
	public static class IdentityDbContextSeeding
	{
		public static bool SeedData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
		{
			try
			{
				var HasUser = userManager.Users.Any();
				var HasRole = roleManager.Roles.Any();
				if (HasRole && HasUser) return false;
				if (!HasRole)
				{
					var Roles = new List<IdentityRole>()
					{
						new(){ Name = "SuperAdmin"},
						new(){ Name = "Admin"}
					};
					foreach (var role in Roles)
					{
						if (!roleManager.RoleExistsAsync(role.Name!).Result)
							roleManager.CreateAsync(role).Wait();
					}
				}

				if (!HasUser)
				{
					var MainAdmin = new ApplicationUser()
					{
						FirstName = "Basil",
						LastName = "Kasim",
						UserName = "BasilKasim",
						Email = "BasilKasim@gmail.com",
						PhoneNumber = "1234567890"
					};
					userManager.CreateAsync(MainAdmin, "P@ssw0rd").Wait();
					userManager.AddToRoleAsync(MainAdmin, "SuperAdmin").Wait();

					var Admin = new ApplicationUser()
					{
						FirstName = "Ahdab",
						LastName = "Kasim",
						UserName = "AhdabKasim",
						Email = "AhdabKasim@gmail.com",
						PhoneNumber = "1234567891"
					};
					userManager.CreateAsync(Admin, "P@ssw0rd").Wait();
					userManager.AddToRoleAsync(Admin, "Admin").Wait();
				}

				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error seeding data: {ex}");
				return false;
			}
		}
	}
}
