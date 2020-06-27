
using AutoMapper;
using RestFul_API.Infrastructure.Entities.Concrete;
using RestFul_API.Models.Dtos;

namespace RestFul_API.Infrastructure.Mapper
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
