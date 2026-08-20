using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using LibraryERD.Domain;

namespace LibraryERD.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Books>()
                .HasKey(b => b.BookId);

            modelBuilder.Entity<Authors>()
                .HasKey(a => a.AuthorId);

            modelBuilder.Entity<Categories>()
                .HasKey(c => c.CategoryId);

            modelBuilder.Entity<Publishers>()
                .HasKey(p => p.PublisherId);

            modelBuilder.Entity<Customers>()
                .HasKey(c => c.CustomerId);

            modelBuilder.Entity<Employees>()
                .HasKey(e => e.EmployeeId);

            modelBuilder.Entity<Orders>()
                .HasKey(o => o.OrderId);

            modelBuilder.Entity<BookAuthors>()
                .HasKey(ba => new
                {
                    ba.BookId,
                    ba.AuthorId
                });

            modelBuilder.Entity<BookCategories>()
                .HasKey(bc => new
                {
                    bc.BookId,
                    bc.CategoryId
                });

            modelBuilder.Entity<BookDetails>()
                .HasKey(bd => bd.BookId);

            modelBuilder.Entity<BookDetails>()
                .HasOne(bd => bd.Book)
                .WithOne()
                .HasForeignKey<BookDetails>(bd => bd.BookId);

            modelBuilder.Entity<ShippingAddresses>()
                .HasKey(sa => sa.CustomerId);

            modelBuilder.Entity<Reviews>()
                .HasKey(r => new
                {
                    r.CustomerId,
                    r.BookId
                });
            modelBuilder.Entity<OrderItems>()
                .HasKey(oi => new
                {
                    oi.OrderId,
                    oi.BookId
                });
            modelBuilder.Entity<OrderItems>()
                .HasOne(oi => oi.Book)
                .WithMany()
                .HasForeignKey(oi => oi.BookId);
            modelBuilder.Entity<OrderItems>()
                .HasOne(oi => oi.Order)
                .WithMany()
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<Books>()
                .HasOne<Publishers>()
                .WithMany()
                .HasForeignKey(b => b.PublisherId);

            modelBuilder.Entity<BookAuthors>()
                .HasOne<Books>()
                .WithMany()
                .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<BookAuthors>()
                .HasOne<Authors>()
                .WithMany()
                .HasForeignKey(ba => ba.AuthorId);

            modelBuilder.Entity<BookCategories>()
                .HasOne<Books>()
                .WithMany()
                .HasForeignKey(bc => bc.BookId);

            modelBuilder.Entity<BookCategories>()
                .HasOne<Categories>()
                .WithMany()
                .HasForeignKey(bc => bc.CategoryId);

            modelBuilder.Entity<BookDetails>()
                .HasOne<Books>()
                .WithOne()
                .HasForeignKey<BookDetails>(bd => bd.BookId);

            modelBuilder.Entity<ShippingAddresses>()
                .HasOne(sa => sa.customer)
                .WithOne()
                .HasForeignKey<ShippingAddresses>(sa => sa.CustomerId);

            modelBuilder.Entity<Orders>()
                .HasOne(o => o.customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Reviews>()
                .HasOne<Customers>()
                .WithMany()
                .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<Reviews>()
                .HasOne<Books>()
                .WithMany()
                .HasForeignKey(r => r.BookId);

            modelBuilder.Entity<OrderItems>()
                .HasOne<Orders>()
                .WithMany()
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItems>()
                .HasOne<Books>()
                .WithMany()
                .HasForeignKey(oi => oi.BookId);

            modelBuilder.Entity<Employees>()
                .HasOne<Employees>()
                .WithMany()
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Books> Book { get; set; }
        public DbSet<Authors> Author { get; set; }
        public DbSet<BookAuthors> BookAuthor { get; set; }
        public DbSet<BookDetails> BookDetail { get; set; }
        public DbSet<BookCategories> BookCategory { get; set; }
        public DbSet<Categories> Category { get; set; }
        public DbSet<Customers> Customer { get; set; }
        public DbSet<Employees> Employee { get; set; }
        public DbSet<OrderItems> OrderItem { get; set; }
        public DbSet<Orders> Order { get; set; }
        public DbSet<Publishers> Publisher { get; set; }
        public DbSet<Reviews> Review { get; set; }
        public DbSet<ShippingAddresses> ShippingAddress { get; set; }

       
    }       

}
