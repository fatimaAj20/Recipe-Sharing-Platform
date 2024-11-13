using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    public async Task<IActionResult> CreateRecipe([FromForm] RecipeCreate recipeCreate)
    {
        var id = await RecipeService.CreateRecipeAsync(recipeCreate);
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateRecipe([FromForm]RecipeUpdate recipeUpdate)
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
    public async Task<IActionResult> GetRecipe(int id, [FromQuery] string email)
    {
        var recipe = await RecipeService.GetRecipeAsync(id, email);
        return Ok(recipe);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetRecipes([FromQuery] RecipeFilter recipeFilter)
    {
        var recipes = await RecipeService.GetRecipesAsync(recipeFilter);
        return Ok(recipes);
    }

    [HttpGet]
    [Route("Share/{sharingKey}")]
    public async Task<IActionResult> ShareRecipe(string sharingKey)
    {
        var recipe = await RecipeService.GetSharedRecipeAsync(sharingKey);
        return Ok(recipe); 
    }
}
