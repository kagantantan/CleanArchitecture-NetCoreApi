using System;
using System.Collections.Generic;
using System.Text;

namespace Idil.Boilerplate.Core.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
