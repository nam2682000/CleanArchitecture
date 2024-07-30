using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Common.Interfaces;

namespace Web.Domain.Common
{
    public abstract class BaseEntity<T> : IEntity<T>
    {
        public T Id { get; set; }
    }
}
