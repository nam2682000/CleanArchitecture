using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Web.Domain.Entities;
using Web.Persistence.DbContext;

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

                var courses = new Course[]
                {
                    new Course{Id=1050,Title="Chemistry",Credits=3},
                    new Course{Id=4022,Title="Microeconomics",Credits=3},
                    new Course{Id=4041,Title="Macroeconomics",Credits=3},
                    new Course{Id=1045,Title="Calculus",Credits=4},
                    new Course{Id=3141,Title="Trigonometry",Credits=4},
                    new Course{Id=2021,Title="Composition",Credits=3},
                    new Course{Id=2042,Title="Literature",Credits=4}
                };

                foreach (Course c in courses)
                {
                    context.Courses.Add(c);
                }
                context.SaveChanges();

                var enrollments = new Enrollment[]
                {
                    new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
                    new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
                    new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
                    new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
                    new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
                    new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
                    new Enrollment{StudentID=3,CourseID=1050},
                    new Enrollment{StudentID=4,CourseID=1050},
                    new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
                    new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
                    new Enrollment{StudentID=6,CourseID=1045},
                    new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
                };

                foreach (Enrollment e in enrollments)
                {
                    context.Enrollments.Add(e);
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
