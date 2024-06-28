﻿using RecipeSharingProject.Common.Dtos;

namespace RecipeSharingProject.Common.Interfaces;

public interface IRecipeService
{
    public Task<int> CreateRecipeAsync(RecipeCreate recipeCreate);
    public Task UpdateRecipeAsync(RecipeUpdate recipeUpdate);
    public Task DeleteRecipeAsync(RecipeDelete recipeDelete);
    Task<RecipeDetails> GetRecipeAsync(int id);
    Task<List<RecipeList>> GetRecipesAsync(RecipeFilter recipeFilter);
}