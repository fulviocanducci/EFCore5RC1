using EFCore5RC1.Models;
using Microsoft.EntityFrameworkCore;
using Service;
using System;
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
            builder.AddInterceptors(new MyInterceptor());
            using (DataContext data = new DataContext(builder.Options))
            {
                data.Database.EnsureDeleted();
                data.Database.EnsureCreated();
                // Author author = new Author();
                // author.Name = "Hugo";
                // Book book = new Book
                // {
                //   Title = "O Primeiro Leão"
                // };
                // author.Books.Add(book);
                // data.Author.Add(author);
                // data.SaveChanges();

                //var items = data.Author.Include(i => i.Books).ToList();
                //Dictionary<string, object> item = new Dictionary<string, object>()
                //{
                //  ["Title"] = "First Item",
                //  ["Status"] = true
                //};
                //data.Items.Add(item);
                //data.SaveChanges();
                //object title = "First Item";
                // var items = data
                //   .Items
                //   .Where(c => ((string)c["Title"]).Contains("First Item"))
                //   .ToList();

                //Animal animal = new Animal()
                //{
                //    Name = "Gato"
                //};
                //data.Animal.Add(animal);
                //data.SaveChanges();

                //Animal cao = data.Animal.Find(1);
                //cao.Name = "Cão Grande";
                //data.SaveChanges();

                //var items = data.ViewAnimal.ToList();
                var people = new People
                {
                    Name = "Name 2",
                    //Local = new Address
                    //{
                    //    Number = "1",
                    //    Street = "Addres 1"
                    //},
                    //Work = new Address
                    //{
                    //    Number = "2",
                    //    Street = "Addres 2"
                    //},
                };

                data.People.Add(people);
                data.SaveChanges();

            }
            builder = null;
            Console.WriteLine("Done.");
        }
    }
}
