using System;
using System.Collections.Generic;
using System.Linq;

namespace BookTracker.Model.Common
{
    public interface IBook
    {
        int BookID { get; set; }
        int AuthorID { get; set; }
        string Name { get; set; }
        string FirstAuthor { get; set; }
        string LastAuthor { get; set; }
        DateTime? DateRead { get; set; }
        int? Rating { get; set; }

        IAuthor Author { get; set; }
    }
}
