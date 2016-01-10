using System;
using System.Collections.Generic;
using System.Linq;

namespace BookTracker.DAL
{
    public class WishlistEntity
    {
        public int ID { get; set; }
        public string WishName { get; set; }
        public string WishFirstAuthor { get; set; }
        public string WishLastAuthor { get; set; }
        public decimal? Price { get; set; }
    }
}
