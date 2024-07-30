using Web.Infrastructure.DbContext;

namespace Web.Persistence.Common
{
    public static class MigrateDatabase
    {
        public static void CheckMigrateDatabase(ApplicationDbContext context)
        {
            try
            {
                // Check if there is any data in the database
                if (!context.Students.Any())
                {
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
