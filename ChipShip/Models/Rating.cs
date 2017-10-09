using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipShip.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public int raiting { get; set; }       
        public int raitingCount { get; set; }
    }
}