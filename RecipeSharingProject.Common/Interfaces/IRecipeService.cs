using RecipeSharingProject.Common.Dtos;
using RecipeSharingProject.Common.Dtos.Recipe;

namespace RecipeSharingProject.Common.Interfaces;

public interface IRecipeService
{
    Task<int> CreateRecipeAsync(RecipeCreate recipeCreate);
    Task UpdateRecipeAsync(RecipeUpdate recipeUpdate);
    public Task DeleteRecipeAsync(RecipeDelete recipeDelete);
    Task<RecipeDetails> GetRecipeAsync(int id, string email);
    public Task<RecipeDetails> GetSharedRecipeAsync(string sharingKey);
    Task<List<RecipeList>> GetRecipesAsync(RecipeFilter recipeFilter);
}
