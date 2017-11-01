using ChipShip.Models;
using ChipShip.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ChipShip.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext context;       
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ApproveDeliverer()
        {
            context = new ApplicationDbContext();
            ApproveViewModel model = new ApproveViewModel();            
            var toApprove = context.Users.Where(a => a.ApplyToDeliverer == true).ToList();
            model.toBeApproved = toApprove;
            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> ApproveDelivererPost(string Id)
        {

            context = new ApplicationDbContext();
            var applicant = context.Users.Where(a => a.Id == Id).First();
            var applicantRoleId = applicant.Roles.SingleOrDefault().RoleId;
            var applicantRoleName = context.Roles.SingleOrDefault(a => a.Id == applicantRoleId).Name;

            if (applicantRoleName != "Deliverer")
            {
                var store = new UserStore<ApplicationUser>(context);
                ApplicationUserManager Manager = new ApplicationUserManager(store);
                await Manager.RemoveFromRolesAsync(applicant.Id, applicantRoleName);
                await Manager.AddToRoleAsync(applicant.Id, "Deliverer");
                DelivererGeoLocationModel DelivererGeoLocation = new DelivererGeoLocationModel();
                DelivererGeoLocation.User = applicant;
                DelivererGeoLocation.tracking = false;
                context.DelivererGeoLocation.Add(DelivererGeoLocation);         
            }
            applicant.ApplyToDeliverer = false;
            context.SaveChanges();
            return View("Index");
        }
        public ActionResult ToBePaidPeople()
        {
            context = new ApplicationDbContext();
            PeopleToBePaid model = new PeopleToBePaid();
            List<ApplicationUser> userList = new List<ApplicationUser>();
            var allDeliverers = context.toBePaid.Include("User").Where(a => a.paid == false);
            var deliverers = allDeliverers.Distinct();
            foreach (var item in deliverers)
            {
                userList.Add(item.User);
            }
            model.toBePaidUsers = userList;
            return View(model);
        }
        public ActionResult ToBePaid(string userId)
        {
            context = new ApplicationDbContext();
            PeopleToBePaid model = new PeopleToBePaid();
            List<ToBePaid> userList = new List<ToBePaid>();
            var deliverers = context.toBePaid.Include("User").Where(a => a.paid != true && a.User.Id == userId);
            foreach (var item in deliverers)
            {
                userList.Add(item);
            }
            model.toBePaid = userList;
            return View(model);
        }
        public ActionResult PayDeliverer(int Id)
        {
            context = new ApplicationDbContext();
            var payedOrder = context.toBePaid.Include("User").Where(a => a.Id == Id).First(); ;
            payedOrder.paid = true;
            context.SaveChanges();
            return View("Index");
        }
    }
}