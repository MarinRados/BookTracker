using System;
using System.Collections.Generic;
using System.Linq;

namespace BookTracker.Model.Common
{
    public interface IWishlist
    {
        int ID { get; set; }
        string WishName { get; set; }
        string WishFirstAuthor { get; set; }
        string WishLastAuthor { get; set; }
        decimal? Price { get; set; }
    }
}
