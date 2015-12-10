using System;
using System.Collections.Generic;
using System.Linq;

namespace BookTracker.DAL
{
    public class BookEntity
    {
        public int BookEntityID { get; set; }
        public int AuthorID { get; set; }
        public string Name { get; set; }
        public string FirstAuthor { get; set; }
        public string LastAuthor { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rating { get; set; }

        public virtual AuthorEntity Author { get; set; }
    }
}
