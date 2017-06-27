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
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }

    }

    public class MyDbInitializer : DropCreateDatabaseIfModelChanges<MyDbContext>
    {
        protected override void Seed(MyDbContext context)
        {
            //seeding users
            context.Users.Add(new Account { UID = 1, FirstName = "Adam", LastName = "Levy", Email = "alevy030@fiu.edu", Username = "alevy030", PassWord = "alevy030", ConfirmPassword = "alevy030"});
            context.Users.Add(new Account { UID = 2, FirstName = "Juan", LastName = "Hernandez", Email = "juaneh5@gmail.com", Username = "juaneh5", PassWord = "juaneh5", ConfirmPassword = "juaneh5"});
            context.Users.Add(new Account { UID = 3, FirstName = "Brandon", LastName = "Cajigas", Email = "BrandonCaj@gmail.com", Username = "BrandonCaj", PassWord = "BrandonCaj", ConfirmPassword = "BrandonCaj"});
            context.Users.Add(new Account { UID = 4, FirstName = "Alex", LastName = "Dubuisson", Email = "adubu002@fiu.edu", Username = "adubu002", PassWord = "adubu002", ConfirmPassword = "adubu002"});

            //seeding authors
            context.Authors.Add(new Author { AuthID = 1, AuthorName = "Robert Jackson Bennett", Blurb = "Robert Jackson Bennett is an American writer of speculative fiction." });
            context.Authors.Add(new Author { AuthID = 2, AuthorName = "Orson Scott Card", Blurb = "an American novelist, critic, public peaker, essayist, and columnist. he writesin several genresbut is known best for science fiction." });
            context.Authors.Add(new Author { AuthID = 3, AuthorName = "Timothy Zahn", Blurb = "American writer of science fiction and fantasy." });
            context.Authors.Add(new Author { AuthID = 4, AuthorName = "Mike Carey", Blurb = "British writer of comic books, novels, and films." });
            context.Authors.Add(new Author { AuthID = 5, AuthorName = "Gregory Benford", Blurb = "American science fiction author and astrophyicist at University of California, Irvine" });
            context.Authors.Add(new Author { AuthID = 6, AuthorName = "Dan Moren", Blurb = "Novelist, freelance writer, and prolific podcaster" });
            context.Authors.Add(new Author { AuthID = 7, AuthorName = "George Orwell", Blurb = "Eric Arthur Blair, better known by his pen name George Orwell, was an English novelist, essayist, journalist, and critic" });
            context.Authors.Add(new Author { AuthID = 8, AuthorName = "Ray Bradbury", Blurb = "Ray Douglas Bradbury was an American author and screenwriter. He worked in a variety of genres, including fantasy, science fiction, horror and mystery fiction" });
            context.Authors.Add(new Author { AuthID = 9, AuthorName = "J.R.R. Tolkien", Blurb = "John Ronald Reuel Tolkien, CBE, FRSL was an English writer, poet, philologist, and university professor " });
            context.Authors.Add(new Author { AuthID = 10, AuthorName = "Rachel Aaron", Blurb = "Rachel Aaron is an American author of fantasy and science fiction." });

            //seeding books
            context.Products.Add(new Product { ProductID = 1, AuthID = 1, CoverUrl = "http://prodimage.images-bn.com/pimages/9780553419740.jpg", Genre = "Fantasy", Price = 26.99, ProductName = "City of Miracles", Publisher = "Crown/Archtype", Stock = 50, Synopsis = "Revenge. It’s something Sigrud je Harkvaldsson is very, very good at. Maybe the only thing. So when he learns that his oldest friend and ally former Prime Minister Shara Komayd, has been assassinated, he knows exactly what to do—and that no mortal force can stop him from meting out the suffering Shara’s killers deserve."});
            context.Products.Add(new Product { ProductID = 2, AuthID = 3, CoverUrl = "http://prodimage.images-bn.com/pimages/9780765329660.jpg", Genre = "Science Fiction", Price = 16.99, ProductName = "Pawn", Publisher = "Tom Doherty Associates", Stock = 50, Synopsis = "Nicole Lee’s life is going nowhere. No family, no money, and stuck in a relationship with a thug named Bungie. But, after one of Bungie’s “deals” goes south, he and Nicole are whisked away by a mysterious moth-like humanoid to a strange ship called the Fyrantha." });
            context.Products.Add(new Product { ProductID = 3, AuthID = 4, CoverUrl = "http://prodimage.images-bn.com/pimages/9780316472203.jpg", Genre = "Fantasy", Price = 25.99, ProductName = "The Boy on the Bridge", Publisher = "Orbit", Stock = 50, Synopsis = "Once upon a time, in a land blighted by terror, there was a very clever boy. The people thought the boy could save them, so they opened their gates and sent him out into the world. To where the monsters lived." });
            context.Products.Add(new Product { ProductID = 4, AuthID = 5, CoverUrl = "http://prodimage.images-bn.com/pimages/9781481487641.jpg", Genre = "Science Fiction", Price = 17.49, ProductName = "The Berlin Project", Publisher = "Saga Press", Stock = 50, Synopsis = "Karl Cohen, a chemist and mathematician who is part of The Manhattan Project team, has discovered an alternate solution for creating the uranium isotope..." });
            context.Products.Add(new Product { ProductID = 5, AuthID = 6, CoverUrl = "http://prodimage.images-bn.com/pimages/9781940456843.jpg", Genre = "Science Fiction", Price = 15.99, ProductName = "The Caledonian Gambit", Publisher = "Talos", Stock = 50, Synopsis = "Simon Kovalic is the Commonwealth’s greatest spy, the sort of man who engineers planet-wide events in order to shift the balance of power. He identifies an opportunity on the planet of Caledonia..." });

            context.Products.Add(new Product { ProductID = 6, AuthID = 7, CoverUrl = "https://prodimage.images-bn.com/pimages/9780451524935_p0_v2_s192x300.jpg", Genre = "Science Fiction", Price = 8.99, ProductName = "1984", Publisher = "Penguin Publising Group", Stock = 50, Synopsis = "Winston Smith toes the Party line, rewriting history to satisfy the demands of the Ministry of Truth. With each lie he writes, Winston grows to hate the Party that seeks power for its own sake and persecutes those who dare to commit thoughtcrimes. But as he starts to think for himself, Winston can’t escape the fact that Big Brother is always watching..." });
            context.Products.Add(new Product { ProductID = 7, AuthID = 8, CoverUrl = "https://prodimage.images-bn.com/pimages/9781451673265_p0_v7_s192x300.jpg", Genre = "Science Fiction", Price = 16.15, ProductName = "Fahrenheit 451", Publisher = "Simon & Schuster", Stock = 50, Synopsis = "Guy Montag is a fireman. In his world, where television rules and literature is on the brink of extinction, firemen start fires rather than put them out. His job is to destroy the most illegal of commodities, the printed book, along with the houses in which they are hidden." });
            context.Products.Add(new Product { ProductID = 8, AuthID = 2, CoverUrl = "https://prodimage.images-bn.com/pimages/9780765365408_p0_v3_s192x300.jpg", Genre = "Fantasy", Price = 25.99, ProductName = "Gatefather", Publisher = "Tom Doherty Associates", Stock = 50, Synopsis = "Danny North is the first Gate Mage to be born on Earth in nearly 2000 years, or at least the first to survive to claim his power. Families of Westil in exile on Earth have had a treaty that required the death of any suspected Gate Mage. The wars between the Families had been terrible, until at last they realized it was their own survival in question." });
            context.Products.Add(new Product { ProductID = 9, AuthID = 9, CoverUrl = "https://prodimage.images-bn.com/pimages/9780547928227_p0_v2_s192x300.jpg", Genre = "Fantasy", Price = 8.99, ProductName = "The Hobbit", Publisher = "Houghton Mifflin Harcourt", Stock = 50, Synopsis = "Bilbo Baggins is a hobbit who enjoys a comfortable, unambitious life, rarely traveling any farther than his pantry or cellar. But his contentment is disturbed when the wizard Gandalf and a company of dwarves arrive on his doorstep one day to whisk him away on an adventure." });
            context.Products.Add(new Product { ProductID = 10, AuthID = 10, CoverUrl = "https://prodimage.images-bn.com/pimages/9780316198387_p0_v1_s192x300.jpg", Genre = "Fantasy", Price = 25.99, ProductName = "The Spirit war", Publisher = "Orbit", Stock = 50, Synopsis = "Eli Monpress is vain. He's cocky. And he's a thief. But he's a thief who has just seen his bounty topped and he's not happy about it.The bounty topper, as it turns out, is his best friend, bodyguard, and master swordsman, Josef.Who has been keeping secrets from Eli." });
            
            base.Seed(context);
        }
    }
}