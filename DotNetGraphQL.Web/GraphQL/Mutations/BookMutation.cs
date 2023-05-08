using DotNetGraphQL.Web.Contracts;
using DotNetGraphQL.Web.DTOs;

namespace DotNetGraphQL.Web.GraphQL.Mutations;

public class BookMutation
{
    public async Task<BookDto> CreateBook([Service] IBookService service, BookForCreatingDto item)
    {
        return await service.Create(item);
    }

    public async Task<BookDto> UpdateBook([Service] IBookService service, BookForUpdatingDto item)
    {
        return await service.Update(item);
    }

    public async Task<string> DeleteBook([Service] IBookService service, Guid id)
    {
        await service.Remove(id);
        return $"Item with id '${id}' was deleted!";
    }
}