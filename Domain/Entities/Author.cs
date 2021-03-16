using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Author : AuditableEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<Book> Books { get; private set; } = new List<Book>();

    }
}
