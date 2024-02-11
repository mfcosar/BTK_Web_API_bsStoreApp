using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApi.Utilities.AutoMapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDtoForUpdate, Book>(); //user'dan gelen update objesi Book'a dönüştürülür
        }
    }
}
