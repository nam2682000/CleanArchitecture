using System.ComponentModel.DataAnnotations;
using Web.Domain.Common;

namespace Web.Domain.Entities
{
    public class Student : BaseEntityAuditable<int>
    {
        [Key]
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
