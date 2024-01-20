using AutoMapper;
using Course.Catalog.Service.Api.Dtos.Category;
using Course.Catalog.Service.Api.Services.Generic;
using Course.Catalog.Service.Api.Settings;

namespace Course.Catalog.Service.Api.Services.Category;

public class CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
    : GenericService<Models.Category,CategoryDto>(mapper, databaseSettings), ICategoryService
{
    
}