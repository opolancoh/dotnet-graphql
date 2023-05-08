using DotNetGraphQL.Web.Contracts;
using DotNetGraphQL.Web.Data;
using DotNetGraphQL.Web.DTOs;
using DotNetGraphQL.Web.Entities;
using DotNetGraphQL.Web.Exceptions;
using Microsoft.EntityFrameworkCore;

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
    
    public async Task<BookDto> Create(BookForCreatingDto item)
    {
        var newItem = new Book()
        {
            Id = new Guid(),
            Title = item.Title!,
            PublishedOn = item.PublishedOn!.Value.ToUniversalTime(),
        };

        _context.Books.Add(newItem);
        await _context.SaveChangesAsync();

        var dto = TransformItemToBookDto(newItem);

        return dto;
    }

    public async Task<BookDto> Update(BookForUpdatingDto item)
    {
        var itemToUpdate = new Book
        {
            Id = item.Id!.Value,
            Title = item.Title!,
            PublishedOn = item.PublishedOn!.Value 
        };

        _context.Entry(itemToUpdate).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        
        var dto = TransformItemToBookDto(itemToUpdate);

        return dto;
    }

    public async Task Remove(Guid id)
    {
        var item = new Book { Id = id };

        _context.Books.Remove(item);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ItemExists(id))
            {
                throw new EntityNotFoundException(id);
            }
            else
            {
                throw;
            }
        }
    }
    
    private bool ItemExists(Guid id)
    {
        return _context.Books.Any(e => e.Id == id);
    }
    
    private static BookDto TransformItemToBookDto(Book item)
    {
        return new BookDto
        {
            Id = item.Id,
            Title = item.Title,
            PublishedOn = item.PublishedOn,
        };
    }
}