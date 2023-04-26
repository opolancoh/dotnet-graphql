using DotNetGraphQL.Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetGraphQL.Web.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Review> Reviews { get; set; }
}