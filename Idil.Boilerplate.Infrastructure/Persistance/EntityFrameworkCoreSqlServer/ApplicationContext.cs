using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Idil.Boilerplate.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Idil.Boilerplate.Infrastructure.Persistance.EntityFrameworkCoreSqlServer
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
