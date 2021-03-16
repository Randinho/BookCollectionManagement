using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BookLists.Commands.CreateBookList
{
    public class CreateBookListCommand : IRequest<int>
    {
    }

    public class CreateBookListCommandHandler : IRequestHandler<CreateBookListCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateBookListCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateBookListCommand request, CancellationToken cancellationToken)
        {
            var entity = new BookList();

            await _context.BookLists.AddAsync(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;

        }
    }
}
