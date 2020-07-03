using System;
using System.Collections.Generic;
using System.Text;

namespace Idil.Boilerplate.Core.Models
{
    public abstract class BaseEntity<T> 
    {
        public T Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
