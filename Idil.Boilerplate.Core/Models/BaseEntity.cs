using System;
using System.Collections.Generic;
using System.Text;

namespace Idil.Boilerplate.Core.Models
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
