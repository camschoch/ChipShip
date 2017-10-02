using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipShip.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public ApplicationUser Deliverer { get; set; }
        public ApplicationUser User { get; set; }
        public bool completed { get; set; }
    }
}