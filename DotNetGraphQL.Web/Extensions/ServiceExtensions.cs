using DotNetGraphQL.Web.Contracts;
using Microsoft.EntityFrameworkCore;
using DotNetGraphQL.Web.Data;
using DotNetGraphQL.Web.GraphQL.Mutations;
using DotNetGraphQL.Web.GraphQL.Queries;
using DotNetGraphQL.Web.Services;
using HotChocolate.Types.Pagination;

namespace DotNetGraphQL.Web.Extensions;

public static class ServiceExtensions
{
    public static void ConfigurePersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
    }

    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opts =>
            opts
                .EnableSensitiveDataLogging()
                .UseNpgsql(configuration.GetConnectionString("PostgresConnection")));
    }


    public static void ConfigureGraphQl(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .SetPagingOptions(new PagingOptions
            {
                MaxPageSize = 100, IncludeTotalCount = true
            })
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddQueryType<BookQuery>()
            .AddMutationType<BookMutation>();
    }
}