using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppNetCore.Models
{
    //interfejs repozytorium
    public interface IRepository
    {
        IEnumerable<Product> Products { get; }
        void AddProduct(Product product);
    }
}
