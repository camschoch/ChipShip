using ChipShip.Models;
using ChipShip.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ChipShip.Models.ViewModels.OrderRequestViewModel;

namespace ChipShip.Controllers
{
    public class DelivererController : Controller
    {
        ApplicationDbContext context;
        public DelivererController()
        {
            context = new ApplicationDbContext();
        }
        public ActionResult Test()
        {
            //StaticClasses.StaticClasses.GoogleGeoLocationApi();
            DelivererGeoLocationModel model = new DelivererGeoLocationModel();            
            return View(model);
        }
        // GET: Deliverer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderRequests()
        {
            List<ApplicationUser> User = new List<ApplicationUser>();            
            OrderRequestViewModel model = new OrderRequestViewModel();
            var AllReady = (context.OrderRequest.Include("User").Where(a => a.ActiveOrder == true));
            foreach (var item in AllReady)
            {
                User.Add(item.User);
            }
            model.Users = User;
            return View("OrderRequests", model);
        }
        public ActionResult SeeOrder(string userId)
        {
            OrderRequestViewModel model = new OrderRequestViewModel();
            model.userId = userId;
            var currentUser = context.Users.Where(a => a.Id == userId).First();
            model.OrderRequestLists = context.ShoppingcartJoin.Include("ShoppingCart").Where(a => a.User.Id == currentUser.Id.ToString()).ToList();
            double roundedPrice = 0;
            foreach (var item in model.OrderRequestLists)
            {
                roundedPrice += item.shoppingCart.salePrices;
                model.TotalPrice = Math.Round(roundedPrice, 2);
                //Math.Round(model.TotalPrice += item.shoppingCart.salePrices, 2);
            }
            return View("ViewOrder", model);
        }
        public ActionResult AcceptOrder(string userId)
        {
            var currentUser = context.Users.Where(a => a.UserName == User.Identity.Name).First();
            var activeOrders = context.Orders.Where(a => a.Deliverer.Id == currentUser.Id && a.completed == false);
            if (activeOrders.Count() >= 1)
            {
                return View("Test");
            }
            else
            {
                var customer = context.Users.Where(a => a.Id == userId).First();
                //var orderRequestStatus = context.OrderRequest.Where(a => a.User.Id == customer.Id).First();
                //orderRequestStatus.ActiveOrder = false;
                var thing = context.OrderRequest.Where(a => a.User.Id == userId).First();
                thing.Deliverer = currentUser;                
                Orders newOrder = new Orders();
                newOrder.completed = false;
                newOrder.Deliverer = context.Users.Where(a => a.UserName == User.Identity.Name).First();
                newOrder.User = customer;
                context.Orders.Add(newOrder);
                var orderStatus = context.OrderStatus.Where(a => a.User.Id == customer.Id).First();
                orderStatus.status = "Order Accepted by " + currentUser.UserName;
                //var userAddress = context.AddressJoin.Include("Address").Include("Address.Zip").Include("Address.City").Where(a => a.User.Id == customer.Id).First();
                context.SaveChanges();
                return View("Test");
            }
        }
        [HttpPost]
        public ActionResult UpToDateGeoLocation(string lat, string lng)
        {
            var currentUser = context.Users.Where(a => a.UserName == User.Identity.Name).First();
            var delivererGeoLocation = context.DelivererGeoLocation.Where(a => a.User.Id == currentUser.Id).First();
            delivererGeoLocation.lat = lat;
            delivererGeoLocation.lng = lng;
            context.SaveChanges();
            return View("Test");
        }
        public ActionResult ActiveOrders()
        {
            List<List<ShoppingCartModel>> shoppingCarts = new List<List<ShoppingCartModel>>();
            var currentUser = context.Users.Where(b => b.UserName == User.Identity.Name).First();
            var activeOrders = context.Orders.Include("User").Where(a => a.Deliverer.Id == currentUser.Id && a.completed == false).First();            
            var myCartId = context.ShoppingcartJoin.Where(a => a.User.Id == activeOrders.User.Id).ToList();
            var customer = activeOrders.User;
            var userAddress = context.AddressJoin.Include("Address").Include("Address.Zip").Include("Address.City").Where(a => a.User.Id == customer.Id).First();
            foreach (var item in myCartId)
            {
                shoppingCarts.Add(context.ShopingCarts.Where(a => a.Id == item.Id).ToList());
            }
            ViewShoppingCart model = new ViewShoppingCart();
            model.shoppingCart = shoppingCarts;
            model.UserName = activeOrders.User.UserName;
            model.userId = activeOrders.User.Id;
            model.lat = userAddress.Address.Lattitude;
            model.lng = userAddress.Address.Longitude;
            double roundedPrice = 0;
            foreach (var item in model.shoppingCart)
            {
                foreach (var thing in item)
                {
                    roundedPrice += thing.salePrices;
                    model.TotalPrice = Math.Round(roundedPrice, 2);
                }
            }          
            return View("ActiveOrders", model);
        }
        [HttpPost]
        public ActionResult PurchasedOrder(string userId)
        {
            var customer = context.Users.Where(a => a.Id == userId).First();
            var customerOrderStatus = context.OrderStatus.Where(a => a.User.Id == userId).First();
            customerOrderStatus.status = "Order has been purchased and is out for dilivary.";
            return View("UserAddress");
        }
        public ActionResult UserAddress()
        {
            return View();
        }
    }
}