using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Books.Queries.GetBooks
{
    public class GetBooksQuery : IRequest<IEnumerable<BookDto>>
    {

    }

    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<BookDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBooksQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> Handle(GetBooksQuery query, CancellationToken cancellationToken)
        {
            var list = await _context.Books.AsNoTracking()
                .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                .OrderBy(b => b.Title)
                .ToListAsync(cancellationToken);

            return list;
        }
    }
}
