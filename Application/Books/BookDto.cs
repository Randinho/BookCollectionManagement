using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Books
{
    public class BookDto : IMapFrom<Book>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public int AuthorId { get; set; }
        public string Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Book, BookDto>()
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));
        }
    }
}
