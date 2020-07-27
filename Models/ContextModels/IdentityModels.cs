using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NabzeArz.Models;
using NabzeArz.Models.ContextModels;
using NabzeArz.Models.Nerkh;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentitySample.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
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
        public DbSet<UserModel> UsersBot { get; set; }
        public DbSet<ChannelModel> Chanel { get; set; }
        public DbSet<PointModel> Points { get; set; }
        public DbSet<TrackingModel> Usertracking { get; set; }
        public DbSet<possensionModel> possensions { get; set; }
        public DbSet<CurrencyModel> Currencies { get; set; }
        public DbSet<ticketModel> tickets { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }
        public DbSet<GoldRate> GoldRates { get; set; }
        public DbSet<CryptoRate> CryptoRates { get; set; }

        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}