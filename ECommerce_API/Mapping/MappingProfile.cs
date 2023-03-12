using AutoMapper;
using Entities;
using Entities.DTO;

namespace ECommerce_API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<Category,CategoryDTO>().ReverseMap();
        }
    }
}
