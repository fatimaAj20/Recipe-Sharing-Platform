using Microsoft.AspNetCore.Http;

namespace RecipeSharingProject.Common.Dtos.Recipe;

public record RecipeUpdate(int Id, string Name, string Ingredients, string Steps,string Email, IFormFile? Photo);