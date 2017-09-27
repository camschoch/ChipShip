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
            model.Items = StaticClasses.StaticClasses.WalmartApi(search);         
            return View("ShoppingPage", model);
        }
        //public ActionResult SearchingTags(string search)
        //{
        //    var hope = search;
        //    return View("ShoppingPage");
        //}
    }
}