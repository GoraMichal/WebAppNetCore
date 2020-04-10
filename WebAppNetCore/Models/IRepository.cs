using System.Collections.Generic;

namespace WebAppNetCore.Models
{
    //interfejs repozytorium
    public interface IRepository
    {
        IEnumerable<Product> Products { get; }

        //Metoda zapewniajaca pojedynczy obiekt za pomoca klucza glownego
        Product GetProduct(long key);
        void AddProduct(Product product);
        
        //Metoda otrzymuje obiekt Product i nie zwrawca wyniku
        void UpdateProduct(Product product);

        //Aktualizacja zbiorcza (Bulk Updates)
        void UpdateAll(Product[] products);

        void Delete(Product product);
    }
}
