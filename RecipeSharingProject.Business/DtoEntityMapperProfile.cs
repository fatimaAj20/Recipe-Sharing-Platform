using AutoMapper;
using RecipeSharingProject.Common.Dtos;
using RecipeSharingProject.Common.Dtos.Recipe;
using RecipeSharingProject.Common.Model;

namespace RecipeSharingProject.Business;

public class DtoEntityMapperProfile : Profile
{
    public DtoEntityMapperProfile()
    {
        CreateMap<RecipeCreate, Recipe>()
        .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<RecipeUpdate, Recipe>();

        CreateMap<Recipe, RecipeDetails>();
        CreateMap<Recipe, RecipeList>();
    }
}
