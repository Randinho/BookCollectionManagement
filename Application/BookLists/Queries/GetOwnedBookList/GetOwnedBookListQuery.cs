using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BookLists.Queries.GetOwnedBookList
{
    public class GetOwnedBookListQuery : IRequest<BookListDto>
    {
        public int Id { get; set; }
    }

    public class GetOwnedBookListQueryHandler : IRequestHandler<GetOwnedBookListQuery, BookListDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetOwnedBookListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BookListDto> Handle(GetOwnedBookListQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.BookLists.Include(b => b.Books.Where(x => x.Status == BookStatus.Owned).OrderByDescending(x => x.Status))
                .FirstOrDefaultAsync();

            if (list == null)
                throw new NotFoundException(nameof(BookList), request.Id);

            return _mapper.Map<BookListDto>(list);
        }
    }
}
