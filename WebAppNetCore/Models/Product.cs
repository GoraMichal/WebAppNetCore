using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppNetCore.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        //public string Category { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal RetailPrice { get; set; }

        //Klucz obcy, sledzenie relacji poprzez przypisanie wartosci klucza glownego identyfikujacego ob. Category.
        public long CategoryId { get; set; }
        //Zastepuje wlasciwosc Category. Populuje wlasciwosc obiektu Category i identyfikuje ja z kluczem obcym.
        public Category Category { get; set; }
    }
}
