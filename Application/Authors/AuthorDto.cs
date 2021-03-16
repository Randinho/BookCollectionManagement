using Application.Books;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Authors
{
    public class AuthorDto : IMapFrom<Author>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<BookDto> Books { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Author, AuthorDto>();
        }
    }
}
