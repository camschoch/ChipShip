using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipShip.Models.ViewModels
{
    public class OrderRequestViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public List<ShoppingCartJoinModel> OrderRequestLists { get; set; }

        public class OrderRequestList
        {
            public string name { get; set; }
            public float salePrice { get; set; }
            public int amount { get; set; }
        }
        public class OrderRequest
        {
            public int Id { get; set; }
            public ApplicationUser User { get; set; }
            public bool ActiveOrder { get; set; }
        }
    }
}