using System.Collections.Generic;

namespace WebAppNetCore.Models
{
    //interfejs repozytorium
    public interface IRepository
    {
        IEnumerable<Product> Products { get; }
        Product GetProduct(long key);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
    }
}
