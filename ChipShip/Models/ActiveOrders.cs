using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipShip.Models
{
    public class ActiveOrders
    {
        public string UserName { get; set; }
        public List<ItemDetail> ItemDetails { get; set; }
        public double totalPrice { get; set; }
    }
    public class ItemDetail
    {
        public string ItemName { get; set; }
        public int itemId { get; set; }
        public double salePrice { get; set; }
        public int amount { get; set; }
       
    }
}