using DotNetGraphQL.Web.Entities;

namespace DotNetGraphQL.Web.Data;

public static class DataHelper
{
    public static void Seed(WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        var bookId1 = new Guid("72d95bfd-1dac-4bc2-adc1-f28fd43777fd");
        var bookId2 = new Guid("c32cc263-a7af-4fbd-99a0-aceb57c91f6b");
        var bookId3 = new Guid("7b6bf2e3-5d91-4e75-b62f-7357079acc51");

        var books = new List<Book>
        {
            new()
            {
                Id = bookId1, Title = "Book 01", PublishedOn = DateTime.UtcNow,
                Reviews = new List<Review>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Comment = "Comment 01_01",
                        Rating = 5
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Comment = "Comment 01_02",
                        Rating = 3
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Comment = "Comment 01_03",
                        Rating = 1
                    }
                }
            },
            new()
            {
                Id = bookId2, Title = "Book 02", PublishedOn = DateTime.UtcNow,
                Reviews = new List<Review>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Comment = "Comment 02_01",
                        Rating = 4
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Comment = "Comment 02_02",
                        Rating = 2
                    }
                }
            },
            new()
            {
                Id = bookId3, Title = "Book 03", PublishedOn = DateTime.UtcNow,
                Reviews = new List<Review>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Comment = "Comment 03_01",
                        Rating = 3
                    }
                }
            }
        };

        context?.Database.EnsureCreated();
        context?.Books.AddRange(books);
        context?.SaveChanges();
    }
}