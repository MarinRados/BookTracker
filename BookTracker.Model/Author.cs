using System;
using System.Collections.Generic;
using System.Linq;
using BookTracker.Model.Common;

namespace BookTracker.Model
{
    public class Author : IAuthor
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<IBook> Books { get; set; }
    }
}