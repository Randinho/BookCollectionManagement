using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BookLists.Commands.DeleteBookList
{
    public class DeleteBookListCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteBookListCommandHandler : IRequestHandler<DeleteBookListCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeleteBookListCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteBookListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.BookLists.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(BookList), request.Id);

            _context.BookLists.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
