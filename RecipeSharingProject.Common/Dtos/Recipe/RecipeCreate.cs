using Microsoft.AspNetCore.Http;

namespace RecipeSharingProject.Common.Dtos.Recipe;
public record RecipeCreate(string Name, string Ingredients, string Steps , string Email, IFormFile? Photo);