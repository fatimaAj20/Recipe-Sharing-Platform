using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingProject.Common.Dtos.Recipe;
using RecipeSharingProject.Common.Interfaces;

namespace RecipeSharingProject.API.Controllers;


[ApiController]
[Route("[controller]")]
public class RecipeController: ControllerBase
{
    private IRecipeService RecipeService { get; }

    public RecipeController(IRecipeService recipeService)
    {
        RecipeService = recipeService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateRecipe(RecipeCreate recipeCreate)
    {
        var id = await RecipeService.CreateRecipeAsync(recipeCreate);
        return Ok(id);

    }
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateRecipe(RecipeUpdate recipeUpdate)
    {
        await RecipeService.UpdateRecipeAsync(recipeUpdate);
        return Ok();
    }

    

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteRecipe(RecipeDelete recipeDelete)
    {
        await RecipeService.DeleteRecipeAsync(recipeDelete);
        return Ok();
    }
    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetRecipe(int id)
    {
            var recipe = await RecipeService.GetRecipeAsync(id);
            return Ok(recipe);
       
    }
    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetRecipes([FromQuery] RecipeFilter recipeFilter)
    {
        var recipes = await RecipeService.GetRecipesAsync(recipeFilter);
        return Ok(recipes);
    }
}
