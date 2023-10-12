using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.DbOperations
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options):base(options) { }

        public DbSet<Book> Books { get; set; }

    }
}
