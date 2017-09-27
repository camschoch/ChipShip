using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipShip.Models
{
    public class ShoppingCartJoinModel
    {
        public int ID { get; set; }
        public ShoppingCartModel shoppingCart { get; set; }
        public ApplicationUser User { get; set; }
    }
}