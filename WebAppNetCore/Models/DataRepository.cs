using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppNetCore.Models
{
    public class DataRepository : IRepository
    {
        private List<Product> data = new List<Product>();

        public IEnumerable<Product> Products => data;

        public void AddProduct(Product product)
        {
            this.data.Add(product);
        }
    }
}
