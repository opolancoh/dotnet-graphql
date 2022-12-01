using DotNetGraphQL.Web.Data;
using DotNetGraphQL.Web.Entities;

namespace DotNetGraphQL.Web.GraphQL.Queries;

public class BookQuery
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Book> GetAll([Service] ApplicationDbContext context) => context.Books;
}