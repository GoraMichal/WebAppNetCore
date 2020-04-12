using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAppNetCore.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> obj) : base(obj) { }
        
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
