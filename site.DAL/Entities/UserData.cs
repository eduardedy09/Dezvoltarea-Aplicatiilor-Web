using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace site.DAL.Entities
{
    public class UserData
    {
        public int Id { get; set; } // PK UserData Id
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
        public int UserId { get; set; } // FK UserId

        public virtual User User { get; set; }
    }
}
