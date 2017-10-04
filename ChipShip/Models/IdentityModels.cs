using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ChipShip.Models.ViewModels;
using static ChipShip.Models.ViewModels.OrderRequestViewModel;

namespace ChipShip.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public bool ApplyToDeliverer { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<ShoppingCartModel> ShopingCarts { get; set; }
        public DbSet<ShoppingCartJoinModel> ShoppingcartJoin { get; set; }
        public DbSet<OrderRequest> OrderRequest { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<UserAddress> AddressJoin { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<ZipCode> Zips { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<DelivererGeoLocationModel> DelivererGeoLocation { get; set; }
        public DbSet<StatusModel> OrderStatus { get; set; }
        public DbSet<Rating> Rating { get; set; }
    }
}