using Web.Domain.Entities;

namespace Web.Aplication.Services
{

    public interface IStudentService
    {
        public Task<List<Student>> GetAllStudent();
    }

    public class StudentService : IStudentService
    {
        public Task<List<Student>> GetAllStudent()
        {
            throw new NotImplementedException();
        }
    }
}
