using EFCore5RC1.Models;
using System;
using System.Collections.Generic;

namespace Models
{
    public class Book : IModified
    {
        public Book()
        {
            Authors = new HashSet<Author>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Author> Authors { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}