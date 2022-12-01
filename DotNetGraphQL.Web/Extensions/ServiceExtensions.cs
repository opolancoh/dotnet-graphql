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

    public static void ConfigureSqlContext(this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddDbContext<ApplicationDbContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));

    public static void ConfigureGraphQl(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType<BookQuery>()
            .AddProjections()
            .AddFiltering()
            .AddSorting();
    }
}