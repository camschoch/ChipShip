using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipShip.Models
{
    public class StatusModel
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public string status { get; set; }
    }
}