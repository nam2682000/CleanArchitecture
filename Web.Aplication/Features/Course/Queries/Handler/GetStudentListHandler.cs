using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Aplication.Features.Course.Command.Handdle;
using Web.Aplication.Interface.Service.Student;
using Web.Aplication.Modal;
using Web.Aplication.Services.Student;

namespace Web.Aplication.Features.Course.Command.Request
{
    public class GetStudentListHandler : IRequestHandler<GetStudentListQuery, List<Domain.Entities.Student>>
    {
        private readonly IStudentService _studentService;

        public GetStudentListHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<List<Domain.Entities.Student>> Handle(GetStudentListQuery query, CancellationToken cancellationToken)
        {
            return await _studentService.GetAllStudent();
        }
    }
}
