using Web.Domain.Common.Interfaces;

namespace Web.Domain.Common
{
    public abstract class BaseEntityAuditable<T> : IAuditableEntity<T>
    {
        public T Id { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
