using AutoMapper;
using Course.Catalog.Service.Api.Dtos;
using Course.Catalog.Service.Api.Dtos.Category;
using Course.Catalog.Service.Api.Dtos.Course;
using Course.Catalog.Service.Api.Dtos.Feature;
using Course.Catalog.Service.Api.Models;
using Course.Shared.Dtos;

namespace Course.Catalog.Service.Api.Mapping;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<Models.Course, CourseDto>()
            .ForMember("Feature",x=>x.Ignore())
            .ForMember("Category",x=>x.Ignore())
            .ReverseMap();
        
        CreateMap<Response<Category>, Response<CategoryDto>>().ReverseMap();
        CreateMap<CourseDto, Models.Course>().ReverseMap();
        CreateMap<CourseWithCategoryDto, Models.Course>().ReverseMap();
        CreateMap<CourseDto, CourseWithCategoryDto>().ReverseMap();
        
        CreateMap<Models.Feature, FeatureDto>().ReverseMap();
        
        CreateMap<Models.Category, CategoryDto>().ReverseMap();
        CreateMap<CategoryWithCoursesDto, CategoryDto>().ReverseMap();
        CreateMap<CategoryWithCoursesDto, Models.Category>().ReverseMap();
    }
}