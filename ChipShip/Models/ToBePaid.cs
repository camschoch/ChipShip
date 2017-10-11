using ChipShip.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipShip.Models
{
    public class ToBePaid
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public double amount { get; set; }
        public bool paid { get; set; }

    }
    public class ToBePaidData
    {
        public ApplicationUser Deliverer { get; set; }
        public double amount { get; set; }
    }
    public class PeopleToBePaid
    {
        public List<ApplicationUser> toBePaidUsers = new List<ApplicationUser>();
        public List<ToBePaid> toBePaid { get; set; }
    }
}