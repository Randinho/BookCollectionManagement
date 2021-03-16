using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Book : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public BookList BookList { get; set; }
        public int BookListId { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public BookStatus Status { get; set; }
    }
}
