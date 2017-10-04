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
        public int twoStar { get; set; }
        public int threeStar { get; set; }
        public int fourStar { get; set; }
        public int fiveStar { get; set; }

    }
}