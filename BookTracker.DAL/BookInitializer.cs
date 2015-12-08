using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BookTracker.Model;

namespace BookTracker.DAL
{
    public class BookInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BookContext>
    {
        protected override void Seed(BookContext context)
        {
            var authors = new List<AuthorEntity>
            {
                new AuthorEntity{FirstName="J.K.", LastName="Rowling"},
                new AuthorEntity{FirstName="Dan", LastName="Brown"},
                new AuthorEntity{FirstName="J.R.R.", LastName="Tolkien"},
                new AuthorEntity{FirstName="Ian", LastName="Fleming"},
                new AuthorEntity{FirstName="William", LastName="Shakespeare"}
            };
            authors.ForEach(s => context.Authors.Add(s));
            context.SaveChanges();

            var books = new List<BookEntity>
            {
                new BookEntity{Name="Harry Potter and the Philosopher's Stone", FirstAuthor="J.K.", LastAuthor="Rowling", DateRead=DateTime.Parse("2010-09-04"), Rating=8, AuthorID=1 },
                new BookEntity{Name="Harry Potter and the Chamber of Secrets", FirstAuthor="J.K.", LastAuthor="Rowling", DateRead=DateTime.Parse("2010-11-12"), Rating=9, AuthorID=1 },
                new BookEntity{Name="The Da Vinci Code", FirstAuthor="Dan", LastAuthor="Brown", DateRead=DateTime.Parse("2008-05-01"), Rating=7, AuthorID=2 },
                new BookEntity{Name="Inferno", FirstAuthor="Dan", LastAuthor="Brown", DateRead=null, Rating=null, AuthorID=2 },
                new BookEntity{Name="The Hobbit", FirstAuthor="J.R.R.", LastAuthor="Tolkien", DateRead=DateTime.Parse("2006-06-22"), Rating=10, AuthorID=3 },
                new BookEntity{Name="Dr.No", FirstAuthor="Ian", LastAuthor="Fleming", DateRead=DateTime.Parse("2010-03-24"), Rating=6, AuthorID=4 },
                new BookEntity{Name="Casino Royale", FirstAuthor="Ian", LastAuthor="Fleming", DateRead=null, Rating=null, AuthorID=4 },
                new BookEntity{Name="Hamlet", FirstAuthor="William", LastAuthor="Shakespeare", DateRead=DateTime.Parse("2007-10-13"), Rating=8, AuthorID=5 }
            };
            books.ForEach(s => context.Books.Add(s));
            context.SaveChanges();

            var wishes = new List<WishlistEntity>
            {
                new WishlistEntity{ WishName="A Game of Thrones", WishFirstAuthor="George R.R.", WishLastAuthor="Martin", Price=59.99m},
                new WishlistEntity{ WishName="Harry Potter and the Prisoner of Azkaban", WishFirstAuthor="J.K.", WishLastAuthor="Rowling", Price=99.99m},
                new WishlistEntity{ WishName="Lord of the Rings: The Fellowship of the Ring", WishFirstAuthor="J.R.R.", WishLastAuthor="Tolkien", Price=120.00m}
            };
            wishes.ForEach(s => context.Wishlists.Add(s));
            context.SaveChanges();
        }
    }
}
