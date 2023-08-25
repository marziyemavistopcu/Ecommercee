using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model.Views
{
    [Table("V_Admins")]
    public class V_Admins
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
    }
}
