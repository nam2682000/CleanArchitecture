using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Aplication.Interface.Repositories.Student;
using Web.Infrastructure.DbContext;
using Web.Infrastructure.Repositories.Common;

namespace Web.Infrastructure.Repositories.Student
{
    public class StudentRepository : BaseGenericRepository<Domain.Entities.Student, int>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
