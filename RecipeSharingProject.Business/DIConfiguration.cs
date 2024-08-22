using Microsoft.Extensions.DependencyInjection;
using RecipeSharingProject.Business.Exceptions;
using RecipeSharingProject.Business.Validation;
using RecipeSharingProject.Common.Interfaces;

namespace RecipeSharingProject.Business;

public class DIConfiguration
{
    public static void RegisterService(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DtoEntityMapperProfile));
        services.AddScoped<IRecipeService, RecipeService>();

        services.AddScoped<RecipeCreateValidator>();
        services.AddScoped<RecipeUpdateValidator>();
        services.AddScoped<ImageFileValidator>();
    }
}