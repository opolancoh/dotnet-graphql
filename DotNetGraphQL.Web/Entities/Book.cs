namespace DotNetGraphQL.Web.Entities;

// Principal (parent)
public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime PublishedOn { get; set; }

    // One-to-many relationship (Review)
    // Collection navigation containing dependents
    public ICollection<Review> Reviews { get; init; } = new List<Review>();
}