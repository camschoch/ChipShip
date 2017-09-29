using ChipShip.Models;
using ChipShip.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ChipShip.Controllers
{
    public class RegisteredUserController : Controller
    {        
        // GET: RegisteredUser
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ApplyToDeliverer()
        {
            ApplicationDbContext context = new ApplicationDbContext();
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
                        thing.salePrice = salePrice;
                        thing.itemId = itemId;
                        context.SaveChanges();
                        return View("Test");
                    }
                }             
            }
            ShoppingCartModel shoppingCart = new ShoppingCartModel();
            shoppingCart.itemId = itemId;
            shoppingCart.name = name;
            shoppingCart.salePrice = salePrice;
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
        public ActionResult RemoveFromCart(int itemId, string name, float salePrice)
        {
            ApplicationDbContext context = new ApplicationDbContext();
          
            var currentUser = context.Users.Where(b => b.UserName == User.Identity.Name).First();
            var myCartId = context.ShoppingcartJoin.Where(a => a.User.Id == currentUser.Id);
            var removingFromCart = myCartId.Where(a => a.shoppingCart.itemId == itemId).First();
            context.ShoppingcartJoin.Remove(removingFromCart);
            context.SaveChanges();
            return View("Test");
        }

        public ActionResult DisplayShoppingCart()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            List<List<ShoppingCartModel>> shoppingCarts = new List<List<ShoppingCartModel>>();
            var currentUser = context.Users.Where(b => b.UserName == User.Identity.Name).First();
            var myCartId = context.ShoppingcartJoin.Where(a => a.User.Id == currentUser.Id).ToList();
            foreach (var item in myCartId)
            {
                shoppingCarts.Add(context.ShopingCarts.Where(a => a.Id == item.Id).ToList());
            }
            ViewShoppingCart model = new ViewShoppingCart();
            model.shoppingCart = shoppingCarts;
            return View("ShoppingCart", model);
        }
        [HttpPost]
        public ActionResult SubmitOrder()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var currentUser = context.Users.Where(b => b.UserName == User.Identity.Name).First();
            var myOrderId = context.OrderRequest.Where(a => a.User.Id == currentUser.Id).First();
            myOrderId.ActiveOrder = true;
            context.SaveChanges();
            return View("Test");
        }
    }
}