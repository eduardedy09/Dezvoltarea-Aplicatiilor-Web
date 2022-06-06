using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace site.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual IEnumerable<OrderProduct> OrderProducts { get; set; }
    }
}
