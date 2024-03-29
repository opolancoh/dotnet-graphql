using DotNetGraphQL.Web.Contracts;
using DotNetGraphQL.Web.Entities;

namespace DotNetGraphQL.Web.GraphQL.Queries;

public class BookQuery
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Book?> GetBook([Service] IBookService service, Guid id) => service.GetById(id);
    
    [UsePaging]
    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Book> GetBooks([Service] IBookService service) => service.GetAll();
}