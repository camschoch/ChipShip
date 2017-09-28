using ChipShip.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChipShip.Models
{
    public class ShoppingCartModel
    {
        [Key]
        public int Id { get; set; }
        public int itemId { get; set; }
        public int amount { get; set; }       
    }
}