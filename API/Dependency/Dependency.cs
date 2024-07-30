using MediatR;
using Web.Aplication.Interface.Repositories.Student;
using Web.Aplication.Interface.Service.Student;
using Web.Aplication.Services.Student;
using Web.Infrastructure.Authentication;
using Web.Infrastructure.Repositories.Student;

namespace MyApp.Dependency
{
    public static class Dependency
    {
        public static void DependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();
        }
    }
}
