using DotNetGraphQL.Web.Contracts;
using DotNetGraphQL.Web.Data;
using DotNetGraphQL.Web.Entities;

namespace DotNetGraphQL.Web.Services;

public class BookService : IBookService
{
    private readonly ApplicationDbContext _context;

    public BookService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Book> GetAll()
    {
        return _context.Books;
    }

    public IQueryable<Book?> GetById(Guid id)
    {
        return _context.Books.Where(x => x.Id == id).AsQueryable();
    }
}