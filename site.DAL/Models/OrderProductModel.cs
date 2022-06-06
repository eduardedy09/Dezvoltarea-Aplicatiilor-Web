using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace site.DAL.Models
{
    public class OrderProductModel
    {
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
    }
}
