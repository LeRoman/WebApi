using AutoMapper;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApiRadency.Models;
using WebApiRadency.Models.DTO;

namespace WebApiRadency.MappingProfile
{
    public class BookProfile : Profile
    {

        public BookProfile()
        {

            CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.Rating, source => source.MapFrom(src => src.DbContext.RatingItems.Where(x => x.BookId == src.Id).
                Select(y => y.Score).DefaultIfEmpty().Average()))
                .ForMember(dest => dest.Reviews, source => source.MapFrom(src => src.DbContext.ReviewItems.Where(x=>x.BookId==src.Id).Count()));

            CreateMap<Book, BookDetailsDTO>()
                .ForMember(dest => dest.Rating, source =>
                 source.MapFrom(src => src.DbContext.RatingItems.Where(x => x.BookId == src.Id).Select(y => y.Score).DefaultIfEmpty().Average()))

                .ForMember(dest => dest.ReviewsList, source => source.MapFrom(book => book.DbContext.ReviewItems.
                Where(x => x.BookId == book.Id)
                //));
                
                .Select(x=>new ReviewOutputDTO() { Id = x.Id, Message = x.Message, Reviewer = x.Reviewer }).ToList()));
                
               // .Select(x=>book.mapper.Map<ReviewOutputDTO>(x)).ToList()));

            CreateMap<RatingInputDTO, Rating>();
            CreateMap<ReviewInputDTO, Review>();
        }
    }
}
