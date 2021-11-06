using Library.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);

            // seeding
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, Name = "Alex Del Piero" },
                new Author { AuthorId = 2, Name = "Paolo Maldini" }
                );

            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, AuthorId = 1, Title = "Tutorial punizioni", PublishDate = new DateTime(2021,1,1), Publisher = "Publisher Red" },
                new Book { BookId = 2, AuthorId = 1, Title = "Calciare un rigore", PublishDate = new DateTime(2021, 10, 15), Publisher = "Publisher Red" },
                new Book { BookId = 3, AuthorId = 2, Title = "Manuale di difesa", PublishDate = new DateTime(2015, 2, 3), Publisher = "Publisher Green" }
                );
        }
    }
}
