using GeekBooks.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBooks.DataAccessLayer
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("MyDbContextConnectionString")
        {
            Database.SetInitializer<MyDbContext>(new MyDbInitializer());
        }

        public DbSet<UserAccount> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }
    }

    public class MyDbInitializer : DropCreateDatabaseIfModelChanges<MyDbContext>
    {
        protected override void Seed(MyDbContext context)
        {
            context.Users.Add(new UserAccount { UID = 1, FirstName = "Adam", LastName = "Levy", Email = "alevy030@fiu.edu", NickName = "alevy030", PassWord = "alevy030", ConfirmPassword = "alevy030"});
            context.Users.Add(new UserAccount { UID = 2, FirstName = "Juan", LastName = "Hernandez", Email = "juaneh5@gmail.com", NickName = "juaneh5", PassWord = "juaneh5", ConfirmPassword = "juaneh5"});
            context.Users.Add(new UserAccount { UID = 3, FirstName = "Brandon", LastName = "Cajigas", Email = "BrandonCaj@gmail.com", NickName = "BrandonCaj", PassWord = "BrandonCaj", ConfirmPassword = "BrandonCaj"});
            context.Users.Add(new UserAccount { UID = 4, FirstName = "Alex", LastName = "Dubuisson", Email = "adubu002@fiu.edu", NickName = "adubu002", PassWord = "adubu002", ConfirmPassword = "adubu002"});
            base.Seed(context);
        }
    }
}