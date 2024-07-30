using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Web.Domain.Entities;
using Web.Infrastructure.DbContext;

namespace Web.Persistence.Common
{
    public static class SeedDatabase
    {
        public static async Task Initialize(IServiceProvider services, ILogger logger)
        {
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                context.Database.Migrate();
                context.Database.EnsureCreated();

                await SeedAdminUserAsync(userManager, roleManager);
                // Look for any students.
                if (context.Students.Any())
                {
                    return;   // DB has been seeded
                }

                var students = new Student[]
                {
                    new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
                    new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
                    new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
                    new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
                    new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
                    new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
                    new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
                    new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
                };

                foreach (Student s in students)
                {
                    context.Students.Add(s);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }


        private static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Check if there are any users in the database
            if (!userManager.Users.Any())
            {
                // Create the Admin role if it doesn't exist
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                if (!await roleManager.RoleExistsAsync("User"))
                {
                    await roleManager.CreateAsync(new IdentityRole("User"));
                }


                string[] roles = { "Admin", "Manager", "User" };

                // Create roles if they do not exist
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                // Define users
                var users = new[]
                {
                    new { FirstName = "Duong",LastName ="Nam", Username = "admin", Email = "admin@mywebapp.com", Password = "Admin@123", Role = "Admin" },
                    new { FirstName = "Duong",LastName ="Manager", Username = "manager", Email = "manager@mywebapp.com", Password = "Admin@123", Role = "Manager" },
                    new { FirstName = "Duong",LastName ="User1", Username = "user1", Email = "user1@mywebapp.com", Password = "Admin@123", Role = "User" },
                    new { FirstName = "Duong",LastName ="User1", Username = "user2", Email = "user2@mywebapp.com", Password = "Admin@123", Role = "User" }
                };

                foreach (var user in users)
                {
                    var userApp = new ApplicationUser
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.Username,
                        Email = user.Email,
                        EmailConfirmed = true
                    };
                    var result = await userManager.CreateAsync(userApp, user.Password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(userApp, user.Role);
                    }
                }
            }
        }


    }
}
