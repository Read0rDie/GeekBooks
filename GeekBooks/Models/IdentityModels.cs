using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using GeekBooks.MySQL_Identity;
using System.Collections.Generic;

namespace GeekBooks.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        public virtual ICollection<Avatar> Avatar { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<CreditCard> CreditCards { get; set; }
        public virtual ICollection<BookRating> BookRatings { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
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
            
            Database.SetInitializer(new MyDbInitializer());
        }

        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<BookAuthor> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookRating> BookRatings { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ApplicationUser>().HasMany(x => x.Addresses).WithRequired().HasForeignKey(x => x.UID).WillCascadeOnDelete();
            modelBuilder.Entity<ApplicationUser>().HasMany(x => x.Avatar).WithRequired().HasForeignKey(x => x.UID).WillCascadeOnDelete();
            modelBuilder.Entity<ApplicationUser>().HasMany(x => x.CreditCards).WithRequired().HasForeignKey(x => x.UID).WillCascadeOnDelete();
            modelBuilder.Entity<ApplicationUser>().HasMany(x => x.BookRatings).WithOptional(y => y.User).WillCascadeOnDelete();
            modelBuilder.Entity<ApplicationUser>().HasMany(x => x.ShoppingCarts).WithOptional(y => y.User).WillCascadeOnDelete();

            //modelBuilder.Entity<Address>()
            //    .HasOptional(a => a.UID)
            //    .WithOptionalDependent()
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Avatar>()
            //    .HasOptional(a => a.UserAccount)
            //    .WithOptionalDependent()
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<CreditCard>()
            //    .HasOptional(a => a.UserAccount)
            //    .WithOptionalDependent()
            //    .WillCascadeOnDelete(true);
        }
    }

    public class MyDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {

            context.Authors.Add(new BookAuthor { AuthID = 1, AuthorName = "Robert Jackson Bennett", Blurb = "Robert Jackson Bennett is an American writer of speculative fiction." });
            context.Authors.Add(new BookAuthor { AuthID = 2, AuthorName = "Orson Scott Card", Blurb = "an American novelist, critic, public peaker, essayist, and columnist. he writesin several genresbut is known best for science fiction." });
            context.Authors.Add(new BookAuthor { AuthID = 3, AuthorName = "Timothy Zahn", Blurb = "American writer of science fiction and fantasy." });
            context.Authors.Add(new BookAuthor { AuthID = 4, AuthorName = "Mike Carey", Blurb = "British writer of comic books, novels, and films." });
            context.Authors.Add(new BookAuthor { AuthID = 5, AuthorName = "Gregory Benford", Blurb = "American science fiction author and astrophyicist at University of California, Irvine" });
            context.Authors.Add(new BookAuthor { AuthID = 6, AuthorName = "Dan Moren", Blurb = "Novelist, freelance writer, and prolific podcaster" });
            context.Authors.Add(new BookAuthor { AuthID = 7, AuthorName = "George Orwell", Blurb = "Eric Arthur Blair, better known by his pen name George Orwell, was an English novelist, essayist, journalist, and critic" });
            context.Authors.Add(new BookAuthor { AuthID = 8, AuthorName = "Ray Bradbury", Blurb = "Ray Douglas Bradbury was an American author and screenwriter. He worked in a variety of genres, including fantasy, science fiction, horror and mystery fiction" });
            context.Authors.Add(new BookAuthor { AuthID = 9, AuthorName = "J.R.R. Tolkien", Blurb = "John Ronald Reuel Tolkien, CBE, FRSL was an English writer, poet, philologist, and university professor " });
            context.Authors.Add(new BookAuthor { AuthID = 10, AuthorName = "Rachel Aaron", Blurb = "Rachel Aaron is an American author of fantasy and science fiction." });

            //seeding books
            context.Books.Add(new Book { BookID = 1, AuthorID = 1, CoverUrl = "http://prodimage.images-bn.com/pimages/9780553419740.jpg", Genre = "Fantasy", Price = (decimal)26.99, BookName = "City of Miracles", Publisher = "Crown/Archtype", Stock = 50, Synopsis = "Revenge. It’s something Sigrud je Harkvaldsson is very, very good at. Maybe the only thing. So when he learns that his oldest friend and ally former Prime Minister Shara Komayd, has been assassinated, he knows exactly what to do—and that no mortal force can stop him from meting out the suffering Shara’s killers deserve." });
            context.Books.Add(new Book { BookID = 2, AuthorID = 3, CoverUrl = "http://prodimage.images-bn.com/pimages/9780765329660.jpg", Genre = "Science Fiction", Price = (decimal)16.99, BookName = "Pawn", Publisher = "Tom Doherty Associates", Stock = 50, Synopsis = "Nicole Lee’s life is going nowhere. No family, no money, and stuck in a relationship with a thug named Bungie. But, after one of Bungie’s “deals” goes south, he and Nicole are whisked away by a mysterious moth-like humanoid to a strange ship called the Fyrantha." });
            context.Books.Add(new Book { BookID = 3, AuthorID = 4, CoverUrl = "http://prodimage.images-bn.com/pimages/9780316472203.jpg", Genre = "Fantasy", Price = (decimal)25.99, BookName = "The Boy on the Bridge", Publisher = "Orbit", Stock = 50, Synopsis = "Once upon a time, in a land blighted by terror, there was a very clever boy. The people thought the boy could save them, so they opened their gates and sent him out into the world. To where the monsters lived." });
            context.Books.Add(new Book { BookID = 4, AuthorID = 5, CoverUrl = "http://prodimage.images-bn.com/pimages/9781481487641.jpg", Genre = "Science Fiction", Price = (decimal)17.49, BookName = "The Berlin Project", Publisher = "Saga Press", Stock = 50, Synopsis = "Karl Cohen, a chemist and mathematician who is part of The Manhattan Project team, has discovered an alternate solution for creating the uranium isotope..." });
            context.Books.Add(new Book { BookID = 5, AuthorID = 6, CoverUrl = "http://prodimage.images-bn.com/pimages/9781940456843.jpg", Genre = "Science Fiction", Price = (decimal)15.99, BookName = "The Caledonian Gambit", Publisher = "Talos", Stock = 50, Synopsis = "Simon Kovalic is the Commonwealth’s greatest spy, the sort of man who engineers planet-wide events in order to shift the balance of power. He identifies an opportunity on the planet of Caledonia..." });

            context.Books.Add(new Book { BookID = 6, AuthorID = 7, CoverUrl = "https://prodimage.images-bn.com/pimages/9780451524935_p0_v2_s192x300.jpg", Genre = "Science Fiction", Price = (decimal)8.99, BookName = "1984", Publisher = "Penguin Publising Group", Stock = 0, Synopsis = "Winston Smith toes the Party line, rewriting history to satisfy the demands of the Ministry of Truth. With each lie he writes, Winston grows to hate the Party that seeks power for its own sake and persecutes those who dare to commit thoughtcrimes. But as he starts to think for himself, Winston can’t escape the fact that Big Brother is always watching..." });
            context.Books.Add(new Book { BookID = 7, AuthorID = 8, CoverUrl = "https://prodimage.images-bn.com/pimages/9781451673265_p0_v7_s192x300.jpg", Genre = "Science Fiction", Price = (decimal)16.15, BookName = "Fahrenheit 451", Publisher = "Simon & Schuster", Stock = 50, Synopsis = "Guy Montag is a fireman. In his world, where television rules and literature is on the brink of extinction, firemen start fires rather than put them out. His job is to destroy the most illegal of commodities, the printed book, along with the houses in which they are hidden." });
            context.Books.Add(new Book { BookID = 8, AuthorID = 2, CoverUrl = "https://prodimage.images-bn.com/pimages/9780765365408_p0_v3_s192x300.jpg", Genre = "Fantasy", Price = (decimal)25.99, BookName = "Gatefather", Publisher = "Tom Doherty Associates", Stock = 50, Synopsis = "Danny North is the first Gate Mage to be born on Earth in nearly 2000 years, or at least the first to survive to claim his power. Families of Westil in exile on Earth have had a treaty that required the death of any suspected Gate Mage. The wars between the Families had been terrible, until at last they realized it was their own survival in question." });
            context.Books.Add(new Book { BookID = 9, AuthorID = 9, CoverUrl = "https://prodimage.images-bn.com/pimages/9780547928227_p0_v2_s192x300.jpg", Genre = "Fantasy", Price = (decimal)8.99, BookName = "The Hobbit", Publisher = "Houghton Mifflin Harcourt", Stock = 50, Synopsis = "Bilbo Baggins is a hobbit who enjoys a comfortable, unambitious life, rarely traveling any farther than his pantry or cellar. But his contentment is disturbed when the wizard Gandalf and a company of dwarves arrive on his doorstep one day to whisk him away on an adventure." });
            context.Books.Add(new Book { BookID = 10, AuthorID = 10, CoverUrl = "https://prodimage.images-bn.com/pimages/9780316198387_p0_v1_s192x300.jpg", Genre = "Fantasy", Price = (decimal)25.99, BookName = "The Spirit war", Publisher = "Orbit", Stock = 50, Synopsis = "Eli Monpress is vain. He's cocky. And he's a thief. But he's a thief who has just seen his bounty topped and he's not happy about it.The bounty topper, as it turns out, is his best friend, bodyguard, and master swordsman, Josef.Who has been keeping secrets from Eli." });

            context.Books.Add(new Book { BookID = 11, AuthorID = 9, CoverUrl = "https://prodimage.images-bn.com/pimages/9780345339706_p0_v1_s192x300.jpg", Genre = "Fantasy", Price = (decimal)8.99, BookName = "The Fellowship of the Ring", Publisher = "Random House Publishing Group", Stock = 50, Synopsis = "The dark, fearsome Ringwraiths are searching for a Hobbit. Frodo Baggins knows that they are seeking him and the Ring he bears—the Ring of Power that will enable evil Sauron to destroy all that is good in Middle-earth. Now it is up to Frodo and his faithful servant, Sam, with a small band of companions, to carry the Ring to the one place it can be destroyed: Mount Doom, in the very center of Sauron’s realm." });
            context.Books.Add(new Book { BookID = 12, AuthorID = 9, CoverUrl = "https://prodimage.images-bn.com/pimages/9780345339713_p0_v1_s192x300.jpg", Genre = "Fantasy", Price = (decimal)18.99, BookName = "The Two Towers", Publisher = "Random House Publishing Group", Stock = 50, Synopsis = "The Fellowship is scattered. Some are bracing hopelessly for war against the ancient evil of Sauron. Some are contending with the treachery of the wizard Saruman. Only Frodo and Sam are left to take the accursed One Ring, ruler of all the Rings of Power, to be destroyed in Mordor, the dark realm where Sauron is supreme. Their guide is Gollum, deceitful and lust-filled, slave to the corruption of the Ring." });
            context.Books.Add(new Book { BookID = 13, AuthorID = 9, CoverUrl = "https://prodimage.images-bn.com/pimages/9780345339737_p0_v1_s192x300.jpg", Genre = "Fantasy", Price = (decimal)18.99, BookName = "The Return of the King", Publisher = "Random House Publishing Group", Stock = 50, Synopsis = "While the evil might of the Dark Lord Sauron swarms out to conquer all Middle-earth, Frodo and Sam struggle deep into Mordor, seat of Sauron’s power. To defeat the Dark Lord, the One Ring, ruler of all the accursed Rings of Power, must be destroyed in the fires of Mount Doom. But the way is impossibly hard, and Frodo is weakening. Weighed down by the compulsion of the Ring, he begins finally to despair." });
            context.Books.Add(new Book { BookID = 14, AuthorID = 2, CoverUrl = "https://prodimage.images-bn.com/pimages/9781429963947_p0_v2_s192x300.jpg", Genre = "Science Fiction", Price = (decimal)7.99, BookName = "Speaker for the Dead", Publisher = "Tom Doherty Associates", Stock = 50, Synopsis = "In the aftermath of his terrible war, Ender Wiggin disappeared, and a powerful voice arose: The Speaker for the Dead, who told the true story of the Bugger War. Now, long years later, a second alien race has been discovered, but again the aliens' ways are strange and frightening...again, humans die. And it is only the Speaker for the Dead, who is also Ender Wiggin the Xenocide, who has the courage to confront the mystery...and the truth." });
            context.Books.Add(new Book { BookID = 15, AuthorID = 2, CoverUrl = "https://prodimage.images-bn.com/pimages/9781429963961_p0_v4_s192x300.jpg", Genre = "Science Fiction", Price = (decimal)8.99, BookName = "Xenocide", Publisher = "Tom Doherty Associates", Stock = 50, Synopsis = "The war for survival of the planet Lusitania will be fought in the heart of a child named Gloriously Bright. On Lusitania, Ender found a world where humans and pequininos and the Hive Queen could all live together; where three very different intelligent species could find common ground at last. Or so he thought. Lusitania also harbors the descolada, a virus that kills all humans it infects, but which the pequininos require in order to become adults. The Starways Congress so fears the effects of the descolada, should it escape from Lusitania, that they have ordered the destruction of the entire planet, and all who live there.The Fleet is on its way, a second xenocide seems inevitable." });

            context.Books.Add(new Book { BookID = 16, AuthorID = 7, CoverUrl = "https://prodimage.images-bn.com/pimages/9780451526342_p0_v2_s192x300.jpg", Genre = "Fiction", Price = (decimal)8.99, BookName = "Animal Farm", Publisher = "Penguin Publising Group", Stock = 0, Synopsis = "A farm is taken over by its overworked, mistreated animals. With flaming idealism and stirring slogans, they set out to create a paradise of progress, justice, and equality. Thus the stage is set for one of the most telling satiric fables ever penned—a razor-edged fairy tale for grown-ups that records the evolution from revolution against tyranny to a totalitarianism just as terrible." });
            context.Books.Add(new Book { BookID = 17, AuthorID = 10, CoverUrl = "https://prodimage.images-bn.com/pimages/9780316198363_p0_v2_s192x300.jpg", Genre = "Fantasy", Price = (decimal)25.99, BookName = "Sprit's End", Publisher = "Orbit", Stock = 50, Synopsis = "First rule of thievery: don't be a hero. When Eli broke the rules and saved the Council Kingdoms, he thought he knew the price, but resuming his place as the Shepherdess's favorite isn't as simple as bowing his head. Now that she has her darling back, Benehime is setting in motion a plan that could destroy everything she was created to protect, and even Eli's charm might not be enough to stop her. " });
            context.Books.Add(new Book { BookID = 18, AuthorID = 10, CoverUrl = "https://prodimage.images-bn.com/pimages/9780316193573_p0_v1_s192x300.jpg", Genre = "Fantasy", Price = (decimal)25.99, BookName = "The Legend of Eli Monpress", Publisher = "Orbit", Stock = 50, Synopsis = "Eli Monpress is talented. He's charming. And he's a thief. But not just any thief.He's the greatest thief of the age - and he's also a wizard.And with the help of his partners - a swordsman with the most powerful magic sword in the world but no magical ability of his own, and a demonseed who can step through shadows and punch through walls - he's going to put his plan into effect. The first step is to increase the size of the bounty on his head, so he'll need to steal some big things. But he'll start small for now.He'll just steal something that no one will miss - at least for a while. Like a king." });



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