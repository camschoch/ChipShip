using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipShip.Models
{
    public class DelivererGeoLocationModel
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public bool tracking { get; set; }
    }
}