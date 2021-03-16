using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommand : IRequest<int>
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int BookListId { get; set; }
        public int PageCount { get; set; }
        public string Description { get; set; }
        public BookStatus Status { get; set; }
    }

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateBookCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            if (!_context.Authors.Any(a => a.Id == request.AuthorId))
                throw new NotFoundException(nameof(Author), request.AuthorId);

            if (!_context.BookLists.Any(b => b.Id == request.BookListId))
                throw new NotFoundException(nameof(BookList), request.BookListId);

            var entity = new Book
            {
                Title = request.Title,
                AuthorId = request.AuthorId,
                BookListId = request.BookListId,
                PageCount = request.PageCount,
                Description = request.Description,
                Status = request.Status
            };

            await _context.Books.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
