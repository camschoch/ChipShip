using ChipShip.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipShip.Models
{
    public class OrderHistoryModel
    {
        public List<List<ShoppingCartModel>> ItemList { get; set; }
    }
}