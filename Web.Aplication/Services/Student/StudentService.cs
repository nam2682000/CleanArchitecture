using Web.Aplication.Features.Course.Command.Request;
using Web.Aplication.Interface.Repositories.Student;
using Web.Aplication.Interface.Service.Student;
using Web.Aplication.Modal.Request.Student;
using Web.Domain.Entities;

namespace Web.Aplication.Services.Student
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<bool> AddStudent(CreateStudentCommand request)
        {
            await _studentRepository.Add(new Domain.Entities.Student
            {
                FirstMidName = request.FirstMidName,
                LastName = request.LastName,
            });
            return true;
        }

        public async Task<List<Domain.Entities.Student>> GetAllStudent()
        {
            return await _studentRepository.Get();
        }
    }
}
