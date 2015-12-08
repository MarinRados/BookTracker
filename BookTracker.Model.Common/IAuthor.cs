using System;
using System.Collections.Generic;
using System.Linq;

namespace BookTracker.Model.Common
{
    public interface IAuthor
    {
        int ID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }

        ICollection<IBook> Books { get; set; }
    }
}
