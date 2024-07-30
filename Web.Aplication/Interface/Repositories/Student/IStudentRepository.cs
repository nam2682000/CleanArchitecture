using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Aplication.Interface.Repositories.Common;

namespace Web.Aplication.Interface.Repositories.Student
{
    public interface IStudentRepository:IGenericRepository<Domain.Entities.Student,int> 
    {
    }
}
