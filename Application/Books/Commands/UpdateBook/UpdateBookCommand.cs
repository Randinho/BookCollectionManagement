using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public int AuthorId { get; set; }
        public int BookListId { get; set; }

    }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateBookCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Books.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Book), request.Id);


            if (!_context.Authors.Any(a => a.Id == request.AuthorId))
                throw new NotFoundException(nameof(Author), request.AuthorId);
            if (!_context.BookLists.Any(b => b.Id == request.BookListId))
                throw new NotFoundException(nameof(BookList), request.BookListId);


            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.PageCount = request.PageCount;
            entity.AuthorId = request.AuthorId;
            entity.BookListId = request.BookListId;

            _context.Books.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
