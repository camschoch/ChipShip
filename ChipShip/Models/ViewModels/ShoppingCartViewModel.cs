using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChipShip.Models.ViewModels
{
    public class ViewShoppingCart
    {
        public List<List<ShoppingCartModel>> shoppingCart { get; set; }
        public double TotalPrice { get; set; }
        public double deliveryPrice { get; set; }
        public double finalPrice { get; set; }
        public string UserName { get; set; }
        public string userId { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string OrderStatus { get; set; }
        public string address { get; set; }
        public double rating { get; set; }
        public string message { get; set; }
    }
    public class ShoppingCartModel
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
        public int itemId { get; set; }
        public float salePrices { get; set; }
        public int amount { get; set; }
    }
    public class ShoppingCartJoinModel
    {
        public int Id { get; set; }
        public ShoppingCartModel shoppingCart { get; set; }
        public ApplicationUser User { get; set; }
    }
}