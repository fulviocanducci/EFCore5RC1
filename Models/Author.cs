using System.Collections.Generic;

namespace Models
{
  public class Author
  {
    public Author()
    {
      Books = new HashSet<Book>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Book> Books { get; set; }
  }
}