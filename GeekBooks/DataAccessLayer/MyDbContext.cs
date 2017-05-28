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

        public DbSet<Account> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Avatar> Avatars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }

    }

    public class MyDbInitializer : DropCreateDatabaseIfModelChanges<MyDbContext>
    {
        protected override void Seed(MyDbContext context)
        {
            context.Users.Add(new Account { UID = 1, FirstName = "Adam", LastName = "Levy", Email = "alevy030@fiu.edu", Username = "alevy030", PassWord = "alevy030", ConfirmPassword = "alevy030"});
            context.Users.Add(new Account { UID = 2, FirstName = "Juan", LastName = "Hernandez", Email = "juaneh5@gmail.com", Username = "juaneh5", PassWord = "juaneh5", ConfirmPassword = "juaneh5"});
            context.Users.Add(new Account { UID = 3, FirstName = "Brandon", LastName = "Cajigas", Email = "BrandonCaj@gmail.com", Username = "BrandonCaj", PassWord = "BrandonCaj", ConfirmPassword = "BrandonCaj"});
            context.Users.Add(new Account { UID = 4, FirstName = "Alex", LastName = "Dubuisson", Email = "adubu002@fiu.edu", Username = "adubu002", PassWord = "adubu002", ConfirmPassword = "adubu002"});
            base.Seed(context);
        }
    }
}