using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipShip.Models
{

    public class UserAddress
    {
        public int ID { get; set; }
        public Address Address { get; set; }
        public ApplicationUser User { get; set; }
    }
    
}