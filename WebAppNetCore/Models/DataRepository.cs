﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAppNetCore.Models.Pages;

namespace WebAppNetCore.Models
{
    public class DataRepository : IRepository
    {
        //private List<Product> data = new List<Product>();

        private readonly DataContext context;
        public DataRepository(DataContext tempContext) => context = tempContext;

        public IEnumerable<Product> Products => context.Products
            .Include(p => p.Category).ToArray();

        public Product GetProduct(long key) => context.Products
            .Include(p => p.Category).First(p => p.Id == key);

        //public IEnumerable<Product> Products => context.Products;
        //public Product GetProduct(long key) => context.Products.Find(key);

        public PagedList<Product> GetProducts(QueryOptions options, long category = 0)
        {
            //sortowanie po stronie DB
            IQueryable<Product> query = context.Products.Include(p => p.Category);
            //jeżeli jest różne
            if (category != 0)
            {
                query = query.Where(p => p.CategoryId == category);
            }
            return new PagedList<Product>(query, options);
        }

        public void AddProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        //Aktualizuje jeden obiekt
        public void UpdateProduct(Product product)
        {
            //Aktualizuje wszystkie rekordy
            //context.Products.Update(product)

            //Product p = GetProduct(product.Id);
            Product p = context.Products.Find(product.Id);
            p.Name = product.Name;
            p.CategoryId = product.CategoryId;
            p.PurchasePrice = product.PurchasePrice;
            p.RetailPrice = product.RetailPrice;
           
            context.SaveChanges();
        }

        //Aktualizuje wiele obiektow tzw. Bulk Updates
        public void UpdateAll(Product[] products)
        {
            //context.Products.UpdateRange(products);
            //context.SaveChanges();

            //Slownik dla obiektow Product z uzyciem kolekcji kluczy do zapytan odpowiednich obiektow w db
            Dictionary<long, Product> data = products.ToDictionary(p => p.Id);
            IEnumerable<Product> baseLine = context.Products.Where(p => data.Keys.Contains(p.Id));

            foreach (Product dbProduct in baseLine)
            {
                Product reqProduct = data[dbProduct.Id];
                dbProduct.Name = reqProduct.Name;
                dbProduct.CategoryId = reqProduct.CategoryId;
                dbProduct.PurchasePrice = reqProduct.PurchasePrice;
                dbProduct.RetailPrice = reqProduct.RetailPrice;
            }

            //foreach (Product dbProduct in products)
            //{
            //    Product reqProduct = context.Products.Find(dbProduct.Id);
            //    reqProduct.Name = dbProduct.Name;
            //    reqProduct.CategoryId = dbProduct.CategoryId;
            //    reqProduct.PurchasePrice = dbProduct.PurchasePrice;
            //    reqProduct.RetailPrice = dbProduct.RetailPrice;
            //}

            context.SaveChanges();
        }

        public void Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }
    }
}
