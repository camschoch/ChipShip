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
        public float lat { get; set; }
        public float lng { get; set; }
        public bool tracking { get; set; }
    }
}