using ChipShip.Models;
using ChipShip.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ChipShip.Controllers
{
    public class RegisteredUserController : Controller
    {
        ApplicationDbContext context;
        public RegisteredUserController()
        {
            context = new ApplicationDbContext();
        }
        // GET: RegisteredUser
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ApplyToDeliverer()
        {
            var currentUser = context.Users.Where(a => a.UserName == User.Identity.Name).First();
            currentUser.ApplyToDeliverer = true;
            context.SaveChanges();            
            return View("Index");          
        }
        public ActionResult TestCallWalmart(string search)
        {
            WalmartApiViewModel model = new WalmartApiViewModel();
            model.Items = StaticClasses.StaticClasses.WalmartSearchApi(search);         
            return View("ShoppingPage", model);
        }
        public ActionResult AddToCart(int itemId, string name, float salePrice)
        {            
            List<List<ShoppingCartModel>> shoppingCarts = new List<List<ShoppingCartModel>>();
            var currentUser = context.Users.Where(b => b.UserName == User.Identity.Name).First();
            var orderAccepted = context.OrderRequest.Where(a => a.User.Id == currentUser.Id).First();
            if (orderAccepted.ActiveOrder == true || orderAccepted.OrderAccepted == true || orderAccepted.OrderPurchased == true)
            {
                return View("CannotAdd");
            }
            else
            {
            var myCartId = context.ShoppingcartJoin.Where(a => a.User.Id == currentUser.Id).ToList();
            foreach (var item in myCartId)
            {
                shoppingCarts.Add(context.ShopingCarts.Where(a => a.Id == item.Id).ToList());
            }
                foreach (var item in shoppingCarts)
                {
                    foreach (var thing in item)
                    {
                        if (thing.itemId == itemId)
                        {
                            thing.amount++;
                            context.SaveChanges();
                            return DisplayShoppingCart();
                        }
                        else if (thing.itemId == 0)
                        {
                            thing.amount = 1;
                            thing.name = name;
                            thing.salePrices = salePrice;
                            thing.itemId = itemId;
                            context.SaveChanges();
                            return DisplayShoppingCart();
                        }
                    }
                }          
            }
            ShoppingCartModel shoppingCart = new ShoppingCartModel();
            shoppingCart.itemId = itemId;
            shoppingCart.name = name;
            shoppingCart.salePrices = salePrice;
            shoppingCart.amount = 1;
            context.ShopingCarts.Add(shoppingCart);           
            context.SaveChanges();
            ShoppingCartJoinModel ShoppingCartJoin = new ShoppingCartJoinModel();
            ShoppingCartJoin.shoppingCart = shoppingCart;
            ShoppingCartJoin.User = context.Users.Where(a => a.Email == currentUser.Email).First();
            context.ShoppingcartJoin.Add(ShoppingCartJoin);
            context.SaveChanges();

            return DisplayShoppingCart();
        }
        public ActionResult RemoveFromCart(int itemId, string name, double salePrice)
        {          
            var currentUser = context.Users.Where(b => b.UserName == User.Identity.Name).First();
            var myCartId = context.ShoppingcartJoin.Where(a => a.User.Id == currentUser.Id);
            var removingFromCart = myCartId.Include("ShoppingCart").Where(a => a.shoppingCart.itemId == itemId).First();
            if (removingFromCart.shoppingCart.amount > 1)
            {
                removingFromCart.shoppingCart.amount--;
            }
            else
            {
                context.ShoppingcartJoin.Remove(removingFromCart);
            }           
            context.SaveChanges();
            return DisplayShoppingCart();
        }

        public ActionResult DisplayShoppingCart()
        {
            var currentUser = context.Users.Where(b => b.UserName == User.Identity.Name).First();
           // var currentOrder = context.Orders.Where(a => a.User.Id == currentUser.Id && a.completed == false).First();
            var orderAccepted = context.OrderRequest.Where(a => a.User.Id == currentUser.Id).First();                   
            List<List<ShoppingCartModel>> shoppingCarts = new List<List<ShoppingCartModel>>();          
            var myCartId = context.ShoppingcartJoin.Where(a => a.User.Id == currentUser.Id).ToList();
            foreach (var item in myCartId)
            {
                shoppingCarts.Add(context.ShopingCarts.Where(a => a.Id == item.Id).ToList());
            }
            ViewShoppingCart model = new ViewShoppingCart();
            model.shoppingCart = shoppingCarts;
            double roundedPrice = 0;
            double deliveryPrice = 0;
            double finalPrice = 0;
            foreach (var item in model.shoppingCart)
            {
                foreach (var thing in item)
                {
                    if (thing.amount > 1)
                    {
                        roundedPrice += thing.salePrices * thing.amount;
                        model.TotalPrice = Math.Round(roundedPrice, 2);
                    }
                    else
                    {
                        roundedPrice += thing.salePrices;
                        model.TotalPrice = Math.Round(roundedPrice, 2);
                    }
                }                             
            }
            try
            {
                var address = context.AddressJoin.Include("Address").Include("Address.Zip").Include("Address.City").Where(a => a.User.Id == currentUser.Id).First();
                var location = StaticClasses.StaticClasses.WalmartLocatorApi(address.Address.City.City, address.Address.Zip.zip.ToString());
                string distanceObject = StaticClasses.StaticClasses.GoogleDistanceApi();
                var hold = distanceObject.Split(' ');
                var distance = double.Parse(hold[0]);
                deliveryPrice += distance * 2.15;
                finalPrice = deliveryPrice + roundedPrice;
                model.deliveryPrice = Math.Round(deliveryPrice, 2);
                model.finalPrice = Math.Round(finalPrice, 2);
                if (orderAccepted.FinishOrder == true)
                {
                    FinisedOrderModel finishmodel = new FinisedOrderModel();
                    finishmodel.userId = currentUser.Id;                    
                    return View("FinishedOrder", finishmodel);
                }
                else if (orderAccepted.ActiveOrder == true && orderAccepted.OrderAccepted == true)
                {
                    var orderRequest = context.OrderRequest.Include("Deliverer").Where(a => a.User.Id == currentUser.Id).First();
                    var mapData = context.DelivererGeoLocation.Where(a => a.User.Id == orderRequest.Deliverer.Id).First();
                    var rating = context.Rating.Where(a => a.User.Id == orderRequest.Deliverer.Id).First();
                    if (rating.raitingCount > 1)
                    {
                        model.rating = rating.raiting / rating.raitingCount;
                    }
                    else
                    {
                        model.rating = 4;
                    }
                    model.lat = mapData.lat;
                    model.lng = mapData.lng;
                    var orderStatus = context.OrderStatus.Where(a => a.User.Id == currentUser.Id).First();
                    model.OrderStatus = orderStatus.status;
                    model.message = orderRequest.message;
                    return View("OrderInProgress", model);
                }
                else if (orderAccepted.ActiveOrder == true && orderAccepted.OrderAccepted == false)
                {
                    return View("SubmittedOrder", model);
                }
                else
                {
                    return View("ShoppingCart", model);
                }
            }
            catch
            {
                return View("NoAddress");
            }
        }
        public ActionResult calculatePayment()
        {
            var currentUser = context.Users.Where(b => b.UserName == User.Identity.Name).First();           
            var orderAccepted = context.OrderRequest.Where(a => a.User.Id == currentUser.Id).First();
            List<List<ShoppingCartModel>> shoppingCarts = new List<List<ShoppingCartModel>>();
            var myCartId = context.ShoppingcartJoin.Where(a => a.User.Id == currentUser.Id).ToList();
            foreach (var item in myCartId)
            {
                shoppingCarts.Add(context.ShopingCarts.Where(a => a.Id == item.Id).ToList());
            }
            ViewShoppingCart model = new ViewShoppingCart();
            model.shoppingCart = shoppingCarts;
            double roundedPrice = 0;
            foreach (var item in model.shoppingCart)
            {
                foreach (var thing in item)
                {
                    if (thing.amount > 1)
                    {
                        roundedPrice += thing.salePrices * thing.amount;
                        model.TotalPrice = Math.Round(roundedPrice, 2);
                    }
                    else
                    {
                        roundedPrice += thing.salePrices;
                        model.TotalPrice = Math.Round(roundedPrice, 2);
                    }
                }
            }
            var address = context.AddressJoin.Include("Address").Include("Address.Zip").Include("Address.City").Where(a => a.User.Id == currentUser.Id).First();
            var location = StaticClasses.StaticClasses.WalmartLocatorApi(address.Address.City.City, address.Address.Zip.zip.ToString());
            string distanceObject = StaticClasses.StaticClasses.GoogleDistanceApi();
            var hold = distanceObject.Split(' ');
            var distance = double.Parse(hold[0]);
            roundedPrice += distance * 2.15;
            
            return View("Index");
        }
        [HttpPost]
        public ActionResult ProcessPayment()
        {
            //StripeConfiguration.SetApiKey("sk_test_MrrHmTID56LoiGaWtnXyAJit");

            //// Token is created using Stripe.js or Checkout!
            //// Get the payment token submitted by the form:
            //var token = "tok_visa";

            //// Charge the user's card:
            //var charges = new StripeChargeService();
            //var charge = charges.Create(new StripeChargeCreateOptions
            //{
            //    Amount = 1000,
            //    Currency = "usd",
            //    Description = "Example charge",
            //    SourceTokenOrExistingSourceId = token
            //});
            return View("Test");
        }
        public ActionResult Payment()
        {
            return View();
        }
        public ActionResult submitRating(FinisedOrderModel model)
        {
            var resetUser = context.OrderRequest.Include("User").Include("Deliverer").Where(a => a.User.Id == model.userId).First();
            resetUser.Deliverer = null;
            resetUser.ActiveOrder = false;
            resetUser.OrderAccepted = false;
            resetUser.OrderPurchased = false;
            resetUser.FinishOrder = false;
            resetUser.ActiveOrder = false;
            var changeStatus = context.OrderStatus.Where(a => a.User.Id == model.userId).First();
            changeStatus.status = "No order in progress.";
            var usersCarts = context.ShoppingcartJoin.Where(a => a.User.Id == model.userId);
            foreach (var item in usersCarts)
            {
                context.ShoppingcartJoin.Remove(item);
            }
            var ratedUser = context.Rating.Where(a => a.User.Id == model.userId).First();
            ratedUser.raiting += model.rating;
            ratedUser.raitingCount++;
            context.SaveChanges();
            return View("Index");
        }
        public ActionResult OrderHistoryPeople()
        {
            try
            {
                OrderRequestViewModel model = new OrderRequestViewModel();
                List<ApplicationUser> userList = new List<ApplicationUser>();
                var currentUser = context.Users.Where(b => b.UserName == User.Identity.Name).First();
                var people = context.Orders.Include("Deliverer").Where(a => a.User.Id == currentUser.Id && a.completed == true);
                foreach (var item in people)
                {
                    userList.Add(item.Deliverer);
                }
                model.Users = userList;
                return View(model);
            }
            catch
            {
                //NO PREVIOUS OORDERS
                return View();
            }
        }
        public ActionResult OrderHistory(string userId)
        {
            OrderHistoryModel model = new OrderHistoryModel();
            var currentUser = context.Users.Where(b => b.UserName == User.Identity.Name).First();
            List<List<ShoppingCartModel>> listOfModel = new List<List<ShoppingCartModel>>();
            var orders = context.Orders.Include("Items").Where(a => a.User.Id == currentUser.Id && a.completed == true && a.Deliverer.Id == userId);
            foreach (var item in orders)
            {
                listOfModel.Add(item.Items);
            }
            model.ItemList = listOfModel;
            return View(model);
        }
        [HttpPost]
        public ActionResult SubmitOrder()
        {
            //NOT FINISHED

            var currentUser = context.Users.Where(b => b.UserName == User.Identity.Name).First();
            var address = context.AddressJoin.Include("Address").Where(a => a.User.Id == currentUser.Id).First();
            var myOrderId = context.OrderRequest.Where(a => a.User.Id == currentUser.Id).First();
            myOrderId.ActiveOrder = true;
            myOrderId.OrderAccepted = false;
            var myOrderStatus = context.OrderStatus.Where(a => a.User.Id == currentUser.Id).First();
            myOrderStatus.status = "Waiting to be approved by deliverer.";                        
            context.SaveChanges();
            return DisplayShoppingCart();
        }
        public ActionResult CreateAddress()
        {
            return View("Address");
        }

        [HttpPost]
        public ActionResult CreateAddress(Address model)
        {
            UserAddress junction = new UserAddress();
            ApplicationUser user = context.Users.Where(a => a.UserName == User.Identity.Name).First();
            junction.Address = GetAddress(model);
            junction.User = user;
            var existingJunction = (from data in context.AddressJoin where data.User.Id == junction.User.Id select data).ToList();
            if (existingJunction.Count > 0)
            {
                existingJunction[0].Address = junction.Address;
                context.SaveChanges();
            }
            else
            {
                context.AddressJoin.Add(junction);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        private Address GetAddress(Address model)
        {
            var currentUser = context.Users.Where(a => a.UserName == User.Identity.Name).First();
            model.City = GetCity(model);
            model.Zip = GetZip(model);
            model.Lattitude = StaticClasses.StaticClasses.GoogleGeoLocationApi(model.addressLine, model.Zip.zip, currentUser.Id)[0];
            model.Longitude = StaticClasses.StaticClasses.GoogleGeoLocationApi(model.addressLine, model.Zip.zip, currentUser.Id)[1];
            //model.Zone = StaticClasses.ApiCalls.CurrentZoneApi(model.Zip.zip.ToString());
            var addresses = (from data in context.Addresses where data.addressLine == model.addressLine && data.City.City == model.City.City && data.Zip.zip == model.Zip.zip select data).ToList();
            if (addresses.Count > 0)
            {
                return (from data in context.Addresses where data.addressLine == model.addressLine && data.City.City == model.City.City && data.Zip.zip == model.Zip.zip select data).First();
            }
            else
            {
                context.Addresses.Add(model);
                context.SaveChanges();
                return (from data in context.Addresses where data.addressLine == model.addressLine && data.City.City == model.City.City && data.Zip.zip == model.Zip.zip select data).First();

            }
        }
        private ZipCode GetZip(Address model)
        {
            var Zips = (from data in context.Zips where data.zip == model.Zip.zip select data).ToList();
            if (Zips.Count > 0)
            {
                return (from data in context.Zips where data.zip == model.Zip.zip select data).First();
            }
            else
            {
                context.Zips.Add(model.Zip);
                context.SaveChanges();
                return (from data in context.Zips where data.zip == model.Zip.zip select data).First();
            }
        }

        private Cities GetCity(Address model)
        {
            model.City.State = GetState(model);
            var cities = (from data in context.Cities where data.City == model.City.City && data.State.State == model.City.State.State select data).ToList();
            if (cities.Count > 0)
            {
                return (from data in context.Cities where data.City == model.City.City && data.State.State == model.City.State.State select data).First();
            }
            else
            {
                context.Cities.Add(model.City);
                context.SaveChanges();
                return (from data in context.Cities where data.City == model.City.City && data.State.State == model.City.State.State select data).First();
            }
        }

        private States GetState(Address model)
        {
            var States = (from data in context.States where data.State.ToLower() == model.City.State.State.ToLower() select data).ToList();
            if (States.Count > 0)
            {
                return (from data in context.States where data.State.ToLower() == model.City.State.State.ToLower() select data).First();
            }
            else
            {
                return model.City.State;
            }
        }
    }
}