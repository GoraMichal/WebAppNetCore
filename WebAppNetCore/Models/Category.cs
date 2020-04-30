using System.Collections.Generic;

namespace WebAppNetCore.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
