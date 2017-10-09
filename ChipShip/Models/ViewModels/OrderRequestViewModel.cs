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
        public string addressLine { get; set; }
        public double rating { get; set; }
        public double TotalPrice { get; set; }
        public string userId { get; set; }

        public class OrderRequestList
        {
            public string name { get; set; }
            public double salePrice { get; set; }
            public int amount { get; set; }
        }
        public class OrderRequest
        {
            public int Id { get; set; }
            public ApplicationUser User { get; set; }
            public ApplicationUser Deliverer { get; set; }
            public bool ActiveOrder { get; set; }
            public bool OrderAccepted { get; set; }
            public bool OrderPurchased { get; set; }
            public bool FinishOrder { get; set; }
            public bool ShowOnDeliverer { get; set; }
            public string message { get; set; }
        }
    }
}