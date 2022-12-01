using DotNetGraphQL.Web.Entities;

namespace DotNetGraphQL.Web.Contracts;

public interface IBookService
{
    IQueryable<Book> GetAll();
    IQueryable<Book?> GetById(Guid id);
}