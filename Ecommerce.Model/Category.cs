using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Model
{
    [Table("Category")]
    public class Category
    {
        public Category ()
        {
            
        }

        public int Id { get; set; }
        public string category_name { get; set; }
    }
}