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
            return View("Test");          
        }
        public ActionResult TestCallWalmart(string search)
        {
            WalmartApiViewModel model = new WalmartApiViewModel();
            model.Items = StaticClasses.StaticClasses.WalmartSearchApi(search);         
            return View("ShoppingPage", model);
        }
        public ActionResult AddToCart(int itemId, string name, float salePrice)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            List<List<ShoppingCartModel>> shoppingCarts = new List<List<ShoppingCartModel>>();
            var currentUser = context.Users.Where(b => b.UserName == User.Identity.Name).First();
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
                        return View("Test");
                    }
                    else if (thing.itemId == 0)
                    {
                        thing.amount = 1;
                        thing.name = name;
                        thing.salePrices = salePrice;
                        thing.itemId = itemId;
                        context.SaveChanges();
                        return View("Test");
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

            return View("Test");
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
            return View("Test");
        }

        public ActionResult DisplayShoppingCart()
        {
            var currentUser = context.Users.Where(b => b.UserName == User.Identity.Name).First();
            var currentOrder = context.OrderRequest.Where(a => a.User.Id == currentUser.Id).First();                     
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
                    roundedPrice += thing.salePrices;
                    model.TotalPrice = Math.Round(roundedPrice, 2);             
                }                             
            }
            if (currentOrder.ActiveOrder == true)
            {
                var test = context.OrderRequest.Include("Deliverer").Where(a => a.User.Id == currentUser.Id).First();
                var mapData = context.DelivererGeoLocation.Where(a => a.User.Id == test.Deliverer.Id).First();
                model.lat = mapData.lat;
                model.lng = mapData.lng;     
                return View("OrderInProgress", model);
            }
            else
            {
                return View("ShoppingCart", model);
            }
        }
        [HttpPost]
        public ActionResult SubmitOrder()
        {
            //NOT FINISHED
            var currentUser = context.Users.Where(b => b.UserName == User.Identity.Name).First();
            var myOrderId = context.OrderRequest.Where(a => a.User.Id == currentUser.Id).First();
            myOrderId.ActiveOrder = true;
            var myOrderStatus = context.OrderStatus.Where(a => a.User.Id == currentUser.Id).First();
            myOrderStatus.status = "Waiting to be approved by deliverer.";
            context.SaveChanges();
            return View("Test");
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
            return RedirectToAction("Test");
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