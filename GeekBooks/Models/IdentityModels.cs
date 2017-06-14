using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using GeekBooks.MySQL_Identity;

namespace GeekBooks.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

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
            : base("MyDbContextConnectionString", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MySqlInitializer());
            Database.SetInitializer<ApplicationDbContext>(new MyDbInitializer());
        }

        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<BookAuthor> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }

    public class MyDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //context.Users.Add(new Account { UID = 1, FirstName = "Adam", LastName = "Levy", Email = "alevy030@fiu.edu", Username = "alevy030", PassWord = "alevy030", ConfirmPassword = "alevy030" });
            //context.Users.Add(new Account { UID = 2, FirstName = "Juan", LastName = "Hernandez", Email = "juaneh5@gmail.com", Username = "juaneh5", PassWord = "juaneh5", ConfirmPassword = "juaneh5" });
            //context.Users.Add(new Account { UID = 3, FirstName = "Brandon", LastName = "Cajigas", Email = "BrandonCaj@gmail.com", Username = "BrandonCaj", PassWord = "BrandonCaj", ConfirmPassword = "BrandonCaj" });
            //context.Users.Add(new Account { UID = 4, FirstName = "Alex", LastName = "Dubuisson", Email = "adubu002@fiu.edu", Username = "adubu002", PassWord = "adubu002", ConfirmPassword = "adubu002" });

            //context.Avatars.Add(new Avatar { UID = 1, AVATARID = 1, ImageUrl = "~/Content/Images/avatar01.png" });

            //context.Addresses.Add(new Address { UID = 4, AID = 1, StreetAddress = "13401 NE 8th Street", AddressTwo = "", City = "Miami", Country = "United States", Postal = "33186", State_Province = "Florida" });
            //base.Seed(context);
        }
    }
}