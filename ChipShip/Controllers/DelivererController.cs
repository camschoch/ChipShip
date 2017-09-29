using ChipShip.Models;
using ChipShip.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChipShip.Controllers
{
    public class DelivererController : Controller
    {
        // GET: Deliverer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderRequests()
        {
            List<ApplicationUser> User = new List<ApplicationUser>();
            ApplicationDbContext context = new ApplicationDbContext();
            OrderRequestViewModel model = new OrderRequestViewModel();
            var AllReady = (context.OrderRequest.Include("User").Where(a => a.ActiveOrder == true));
            foreach (var item in AllReady)
            {
                User.Add(item.User);
            }
            model.Users = User;
            //foreach (var item in activeOrders)
            //{

            //    model.OrderRequestList = context.ShoppingcartJoin.Where(a => a.User.Id == item.Id.ToString()).ToList();                                         
            //}
            //foreach (var item in model.OrderRequestList)
            //{

            //}
            return View("OrderRequests", model);
        }
        public ActionResult SeeOrder(string userId)
        {         
            ApplicationDbContext context = new ApplicationDbContext();
            OrderRequestViewModel model = new OrderRequestViewModel();
            var currentUser = context.Users.Where(a => a.Id == userId).First();
            model.OrderRequestLists = context.ShoppingcartJoin.Include("ShoppingCart").Where(a => a.User.Id == currentUser.Id.ToString()).ToList();
            return View("ViewOrder", model);
        }
    }
}