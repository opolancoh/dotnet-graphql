namespace DotNetGraphQL.Web.Entities;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime PublishedOn { get; set; }
}