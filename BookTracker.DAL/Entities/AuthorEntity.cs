using System;
using System.Collections.Generic;
using System.Linq;

namespace BookTracker.DAL
{
    public class AuthorEntity
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<BookEntity> Books { get; set; }
    }
}
