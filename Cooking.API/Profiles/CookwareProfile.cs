using AutoMapper;

namespace Cooking.API.Profiles
{
    public class CookwareProfile : Profile
    {
        public CookwareProfile()
        {
            CreateMap<Entities.Cookware, Models.CookwareDto>();
            CreateMap<Models.CookwareDto, Entities.Cookware>();
            CreateMap<Models.CookwareForCreationDto, Entities.Cookware>();
            CreateMap<Models.CookwareForUpdateDto, Entities.Cookware>();
            CreateMap<Entities.Cookware, Models.CookwareForUpdateDto>();
        }
    }
}
