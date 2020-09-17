using System.Collections.Generic;

namespace Models
{
  public class Book
  {
    public Book()
    {
      Authors = new HashSet<Author>();
    }
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<Author> Authors { get; set; }
  }
}