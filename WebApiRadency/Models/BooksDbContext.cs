using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;


namespace WebApiRadency.Models
{
    public class BooksDbContext : DbContext

    {


        public DbSet<Book> BooksItems { get; set; } = null!;
        public DbSet<Rating> RatingItems { get; set; } = null!;

        public DbSet<Review> ReviewItems { get; set; } = null!;

        public BooksDbContext(DbContextOptions<BooksDbContext> options)
            : base(options)
        {
           Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(new DataFaker().Faker(20));
        }


    }
}
