using DotNetGraphQL.Web.Contracts;
using Microsoft.EntityFrameworkCore;
using DotNetGraphQL.Web.Data;
using DotNetGraphQL.Web.GraphQL.Queries;
using DotNetGraphQL.Web.Services;

namespace DotNetGraphQL.Web.Extensions;

public static class ServiceExtensions
{
    public static void ConfigurePersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
    }

    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<ApplicationDbContext>(options => { options.UseInMemoryDatabase("GraphQLDb"); }); 
    }


    public static void ConfigureGraphQl(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddQueryType<BookQuery>();
    }
}