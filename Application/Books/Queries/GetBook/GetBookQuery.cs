using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Books.Queries.GetBook
{
    public class GetBookQuery : IRequest<BookDto>
    {
        public int Id { get; set; }
    }

    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetBookQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            return _mapper.Map<BookDto>(book);
        }
    }
}
