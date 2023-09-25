using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Books.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }
        public DbSet<BookshelfBook> BookshelfBook { get; set; }
        public DbSet<BookSeries> BookSeries { get; set; }
        public DbSet<Bookshelf> Bookshelf { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<User> User { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Bookshelf>()
				.ToTable(u => u.HasTrigger("trg_SetIdBookshelfOnInsert"));

			modelBuilder.Entity<BookshelfBook>()
				.ToTable(u => u.HasTrigger("trg_SetIdBookshelfOnInsert1"));
		}
	}
}