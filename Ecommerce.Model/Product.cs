using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
    [Table("Products")]
    public class Product
    {

        public Product()
        {
            in_stock = true;
        }

        public int Id { get; set; }
        public string title { get; set; }
        public string product_description { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public bool? in_stock { get; set; }
        public int category_id { get; set; }
        public string? img { get; set; }
    }
}
