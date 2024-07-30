using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Aplication.Features.Course.Command.Request
{
    public class CreateStudentCommand : IRequest<bool>
    {
        public CreateStudentCommand(string lastName, string firstMidName)
        {
            LastName = lastName;
            FirstMidName = firstMidName;
        }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
    }
}
