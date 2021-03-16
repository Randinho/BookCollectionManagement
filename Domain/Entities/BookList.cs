using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class BookList : AuditableEntity
    {
        public int Id { get; set; }
        public IList<Book> Books { get; private set; } = new List<Book>();
    }
}
