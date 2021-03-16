using Application.Books;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.BookLists
{
    public class BookListDto : IMapFrom<BookList>
    {
        public int Id { get; set; }
        public IList<BookDto> Books { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BookList, BookListDto>();
        }
    }
}
