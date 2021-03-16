using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Authors.Queries.GetAuthorsWithBooks
{
    public class GetAuthorsWithBooksQuery : IRequest<IEnumerable<AuthorDto>>
    {
    }

    public class GetAuthorsWithBooksQueryHandler : IRequestHandler<GetAuthorsWithBooksQuery, IEnumerable<AuthorDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorsWithBooksQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorDto>> Handle(GetAuthorsWithBooksQuery request, CancellationToken cancellationToken)
        {
            var authors = await _context.Authors.Include(x => x.Books)
                .ProjectTo<AuthorDto>(_mapper.ConfigurationProvider)
                .OrderBy(a => a.LastName)
                .ToListAsync();

            return authors;

        }
    }
}
