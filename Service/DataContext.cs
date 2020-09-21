using EFCore5RC1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    public class MyInterceptor : SaveChangesInterceptor
    {
        private void SetDataCreatedAndUpdated(DbContextEventData eventData)
        {
            Func<EntityEntry, bool> where = where =>
                (where.State == EntityState.Added || where.State == EntityState.Modified) &&
                where.Entity.GetType().GetInterfaces().Contains(typeof(IModified));

            IOrderedEnumerable<EntityEntry> entityEntries = eventData
                .Context
                .ChangeTracker
                .Entries()
                .Where(where)
                .OrderBy(w => w.State);

            if (entityEntries.Count() > 0)
            {
                DateTime now = DateTime.Now;
                foreach (var entity in entityEntries)
                {
                    if (entity.State == EntityState.Added)
                    {
                        entity.Property("CreatedAt").CurrentValue = now;
                        entity.Property("UpdatedAt").CurrentValue = now;
                    }
                    else if (entity.State == EntityState.Modified)
                    {
                        entity.Property("UpdatedAt").CurrentValue = now;
                    }
                }
            }
        }
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            SetDataCreatedAndUpdated(eventData);
            return result;
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
           DbContextEventData eventData,
           InterceptionResult<int> result,
           CancellationToken cancellationToken = new CancellationToken())
        {
            SetDataCreatedAndUpdated(eventData);
            return new ValueTask<InterceptionResult<int>>(result);
        }
    }
    public sealed class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Animal> Animal { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<People> People { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<ViewAnimal> ViewAnimal { get; set; }
        public DbSet<Dictionary<string, object>> Items => Set<Dictionary<string, object>>("Items");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ViewAnimal>().ToSqlQuery("SELECT * FROM View_Animal");
            modelBuilder.Entity<People>(x =>
            {
                x.OwnsOne<Address>(c => c.Local);
                x.OwnsOne<Address>(c => c.Work);
            });
            modelBuilder.Entity<Author>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.Id).UseIdentityColumn();
                x.Property(x => x.Name).HasMaxLength(100);
                x.HasMany(x => x.Books)
            .WithMany(x => x.Authors)
            .UsingEntity<AuthorBook>(
              a => a.HasOne(e => e.Book).WithMany().HasForeignKey(a => a.BookId),
              b => b.HasOne(e => e.Author).WithMany().HasForeignKey(x => x.AuthorId),
              c => c.ToTable("AuthorBook").HasKey("AuthorId", "BookId")
              );
            });
            modelBuilder.Entity<Book>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.Id)
            .UseIdentityColumn();
                x.Property(x => x.Title)
              .HasMaxLength(100);
            });

            modelBuilder.SharedTypeEntity<Dictionary<string, object>>("Items", options =>
            {
                options.ToTable("Items");
                options.IndexerProperty<int>("Id").UseIdentityColumn();
                options.IndexerProperty<string>("Title").IsRequired().HasMaxLength(100);
                options.IndexerProperty<bool>("Status").IsRequired().HasDefaultValue(0);
            });
        }
    }
}