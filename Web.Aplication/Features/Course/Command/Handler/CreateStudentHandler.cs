using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Aplication.Features.Course.Command.Request;
using Web.Aplication.Interface.Repositories.Student;
using Web.Aplication.Interface.Service.Student;

namespace Web.Aplication.Features.Course.Command.Handdle
{
    public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, bool>
    {
        private readonly IStudentService _studentService;

        public CreateStudentHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }
        public async Task<bool> Handle(CreateStudentCommand command, CancellationToken cancellationToken)
        {
            return await _studentService.AddStudent(command);
        }
    }
}
