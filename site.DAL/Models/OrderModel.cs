using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace site.DAL.Models
{
    public class OrderModel
    {
        public string User { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public List<OrderProductModel> Products { get; set; }
    }
}
