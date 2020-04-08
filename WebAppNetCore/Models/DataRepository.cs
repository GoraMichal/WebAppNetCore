using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppNetCore.Models
{
    public class DataRepository : IRepository
    {
        //private List<Product> data = new List<Product>();

        private DataContext context;
        public DataRepository(DataContext tempContext) => context = tempContext;
        public IEnumerable<Product> Products => context.Products;

        public void AddProduct(Product product)
        {
            this.context.Products.Add(product);
            this.context.SaveChanges();
        }
    }
}
