using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Aplication.Features.Course.Command.Request;
using Web.Aplication.Modal.Request.Student;

namespace Web.Aplication.Interface.Service.Student
{
    public interface IStudentService
    {
        Task<List<Domain.Entities.Student>> GetAllStudent();
        Task<bool> AddStudent(CreateStudentCommand request);
    }
}
