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

    private ILogger<RecipeController> Logger { get; }

    public RecipeController(IRecipeService recipeService, ILogger<RecipeController> logger)
    {
        RecipeService = recipeService;
        this.Logger = logger;
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
        Logger.LogInformation($"Get Recipes called with email: {recipeFilter.Email}");

        try
        {
            throw new Exception("This is a test exception");
            var recipes = await RecipeService.GetRecipesAsync(recipeFilter);
            return Ok(recipes);
        }
        catch(Exception ex)
        {
            Logger.LogError(ex, "Error happened when getting recipes");
            return Ok();
        }
    }
    [HttpGet]
    [Route("Share/{sharingKey}")]
    public async Task<IActionResult> ShareRecipe(string sharingKey)
    {
        var recipe = await RecipeService.GetSharedRecipeAsync(sharingKey);
        return Ok(recipe); 
    }
}
