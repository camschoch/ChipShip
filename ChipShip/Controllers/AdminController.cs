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
        // GET: Admin
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
            }
            applicant.ApplyToDeliverer = false;
            context.SaveChanges();
            return View();
        }
    }
}