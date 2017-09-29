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
    }
    public class ShoppingCartModel
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
        public int itemId { get; set; }
        public float salePrice { get; set; }
        public int amount { get; set; }
    }
    public class ShoppingCartJoinModel
    {
        public int Id { get; set; }
        public ShoppingCartModel shoppingCart { get; set; }
        public ApplicationUser User { get; set; }
    }
}