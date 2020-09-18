using System;
using Microsoft.EntityFrameworkCore;
using Service;
using Models;
using System.Linq;

namespace EFCore5RC1
{
  class Program
  {
    static void Main(string[] args)
    {
      const string connectionString = @"Server=.\SqlExpress;Database=myServiceDatabase;User Id=sa;Password=123456;";
      DbContextOptionsBuilder<DataContext> builder = new DbContextOptionsBuilder<DataContext>();
      builder.UseSqlServer(connectionString);
      using (DataContext data = new DataContext(builder.Options))
      {
        //data.Database.EnsureCreated();
        // Author author = new Author();
        // author.Name = "Hugo";
        // Book book = new Book
        // {
        //   Title = "O Primeiro Leão"
        // };
        // author.Books.Add(book);
        // data.Author.Add(author);
        // data.SaveChanges();

        var items = data.Author.Include(i => i.Books).ToList();


      }
      builder = null;
      Console.WriteLine("Done.");
    }
  }
}
