using System;
using System.Collections.Generic;
using System.Linq;
using BookTracker.Model.Common;

namespace BookTracker.Model
{
    public class Book : IBook
    {
        public int ID { get; set; }
        public int AuthorID { get; set; }
        public string Name { get; set; }
        public string FirstAuthor { get; set; }
        public string LastAuthor { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rating { get; set; }

        public virtual IAuthor Author { get; set; }
    }
}
