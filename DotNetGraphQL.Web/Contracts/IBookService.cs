using DotNetGraphQL.Web.DTOs;
using DotNetGraphQL.Web.Entities;

namespace DotNetGraphQL.Web.Contracts;

public interface IBookService
{
    IQueryable<Book> GetAll();
    IQueryable<Book?> GetById(Guid id);
    Task<BookDto> Create(BookForCreatingDto item);
    Task<BookDto> Update(BookForUpdatingDto item);
    Task Remove(Guid id);
}