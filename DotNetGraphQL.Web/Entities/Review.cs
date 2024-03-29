namespace DotNetGraphQL.Web.Entities;

// Dependent (child)
public class Review
{
    public Guid Id { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }

    // One-to-many relationship (Book)
     public Guid BookId { get; set; } // Required foreign key property
    public Book Book { get; set; } = null!; // Required reference navigation to principal
}