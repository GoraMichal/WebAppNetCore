using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebAppNetCore.Models
{
    public class DataRepository : IRepository
    {
        //private List<Product> data = new List<Product>();

        private DataContext context;
        public DataRepository(DataContext tempContext) => context = tempContext;

        public IEnumerable<Product> Products => context.Products
            .Include(p => p.Category).ToArray();

        public Product GetProduct(long key) => context.Products
            .Include(p => p.Category).First(p => p.Id == key);

        //public IEnumerable<Product> Products => context.Products;
        //public Product GetProduct(long key) => context.Products.Find(key);

        public void AddProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        //Aktualizuje jeden obiekt (zmienione rekordy)
        public void UpdateProduct(Product product)
        {
            //Aktualizuje wszystkie rekordy
            //context.Products.Update(product)

            //Product p = GetProduct(product.Id);
            Product p = context.Products.Find(product.Id);

            p.Name = product.Name;
            //p.Category = product.Category;
            p.PurchasePrice = product.PurchasePrice;
            p.RetailPrice = product.RetailPrice;
            p.CategoryId = product.CategoryId;

            context.SaveChanges();
        }

        //Aktualizuje wiele obiektow tzw. Bulk Updates (zmienione rekordy)
        public void UpdateAll(Product[] products)
        {
            //context.Products.UpdateRange(products);
            //context.SaveChanges();

            //Slownik dla obiektow Product z uzyciem kolekcji kluczy do zapytan odpowiednich obiektow w db
            Dictionary<long, Product> data = products.ToDictionary(p => p.Id);
            IEnumerable<Product> dataKeys = context.Products.Where(p => data.Keys.Contains(p.Id));

            foreach (Product dbProduct in dataKeys)
            {
                Product reqProduct = data[dbProduct.Id];
                dbProduct.Name = reqProduct.Name;
                dbProduct.Category = reqProduct.Category;
                dbProduct.PurchasePrice = reqProduct.PurchasePrice;
                dbProduct.RetailPrice = reqProduct.RetailPrice;
            }
            context.SaveChanges();
        }

        public void Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }
    }
}
