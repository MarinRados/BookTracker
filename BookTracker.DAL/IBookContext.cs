using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using BookTracker.Model;

namespace BookTracker.DAL
{
    public interface IBookContext
    {
        DbSet<AuthorEntity> Authors { get; set; }
        DbSet<BookEntity> Books { get; set; }
        DbSet<WishlistEntity> Wishlists { get; set; }
    }
}

