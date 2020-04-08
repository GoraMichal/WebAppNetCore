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

        public Product GetProduct(long key) => context.Products.Find(key);
        public void AddProduct(Product product)
        {
            this.context.Products.Add(product);
            this.context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            //Aktualizuje wszystkie dane
            //context.Products.Update(product);

            //Aktualizuje zmienione rekordy
            Product p = GetProduct(product.Id);
            p.Name = product.Name;
            p.Category = product.Category;
            p.PurchasePrice = product.PurchasePrice;
            p.RetailPrice = product.RetailPrice;

            context.SaveChanges();
        }
    }
}
