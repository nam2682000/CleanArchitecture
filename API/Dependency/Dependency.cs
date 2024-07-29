using Web.Infrastructure.Authentication;

namespace MyApp.Dependency
{
    public static class Dependency
    {
        public static void DependencyInjection(IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
        }
    }
}
