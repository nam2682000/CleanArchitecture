using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Aplication.Modal;

namespace Web.Aplication.Features.Course.Command.Handdle
{
    public class GetStudentListQuery :  IRequest<List<StudentDetails>>
    {
    }
}
