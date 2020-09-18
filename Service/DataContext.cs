using Microsoft.EntityFrameworkCore;
using Models;

namespace Service
{
  public sealed class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Author> Author { get; set; }
    public DbSet<Book> Book { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Author>(x =>
      {
        x.HasKey(x => x.Id);
        x.Property(x => x.Id)
          .UseIdentityColumn();
        x.Property(x => x.Name)
        .HasMaxLength(100);
        x.HasMany(x => x.Books)
          .WithMany(x => x.Authors)
          .UsingEntity(x => x.ToTable("AuthorBook"));
      });
      modelBuilder.Entity<Book>(x =>
      {
        x.HasKey(x => x.Id);
        x.Property(x => x.Id)
          .UseIdentityColumn();
        x.Property(x => x.Title)
        .HasMaxLength(100);
        x.HasMany(x => x.Authors)
          .WithMany(x => x.Books)
          .UsingEntity(x => x.ToTable("AuthorBook"));
      });
    }
  }
}