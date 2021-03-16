using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BookLists.Queries.GetBookList
{
    public class GetBookListQuery : IRequest<BookListDto>
    {
        public int Id { get; set; }
    }

    public class GetBookListQueryHandler : IRequestHandler<GetBookListQuery, BookListDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetBookListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BookListDto> Handle(GetBookListQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.BookLists.Include(b => b.Books)
                .FirstOrDefaultAsync(b => b.Id == request.Id);

            if (list == null)
                throw new NotFoundException(nameof(BookList), request.Id);

            return _mapper.Map<BookListDto>(list);
        }
    }
}
