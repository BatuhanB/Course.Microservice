using AutoMapper;
using Course.Catalog.Service.Api.Dtos;

namespace Course.Catalog.Service.Api.Mapping;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<Models.Course, CourseDto>().ReverseMap();
        CreateMap<Models.Course, CourseCreateDto>().ReverseMap();
        CreateMap<Models.Course, CourseUpdateDto>().ReverseMap();
        CreateMap<Models.Feature, FeatureDto>().ReverseMap();
        CreateMap<Models.Category, CategoryDto>().ReverseMap();
    }
}