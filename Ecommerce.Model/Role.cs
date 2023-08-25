using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
    [Table("Role")]
    public class Role
    {
        public int Id { get; set; }
        public string role { get; set; }
    }
}
