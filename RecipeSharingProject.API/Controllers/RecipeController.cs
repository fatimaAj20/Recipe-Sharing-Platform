using Azure.Search.Documents.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Services;
using Newtonsoft.Json;
using RecipeSharingProject.Common.Dtos.Recipe;
using RecipeSharingProject.Common.Interfaces;

namespace RecipeSharingProject.API.Controllers;


[ApiController]
[Route("[controller]")]
public class RecipeController : ControllerBase
{
    private IRecipeService RecipeService { get; }
    private IRecipeSearchService RecipeSearchService { get; }

    public RecipeController(IRecipeService recipeService, IRecipeSearchService recipeSearchService)
    {
        RecipeService = recipeService;
        RecipeSearchService = recipeSearchService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateRecipe([FromForm] RecipeCreate recipeCreate)
    {
        var id = await RecipeService.CreateRecipeAsync(recipeCreate);
        RecipeDocument recipe = new RecipeDocument()
        {
            Id = id.ToString(),
            Email = recipeCreate.Email,
            Name = recipeCreate.Name,
            Cuisine = recipeCreate.Cuisine,
            Ingredients = recipeCreate.Ingredients,
        };

        RecipeSearchService.UploadRecipe(recipe);
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateRecipe([FromForm] RecipeUpdate recipeUpdate)
    {
        RecipeDocument recipe = new RecipeDocument()
        {
            Id = recipeUpdate.Id.ToString(),
            Email = recipeUpdate.Email,
            Name = recipeUpdate.Name,
            Cuisine = recipeUpdate.Cuisine,
            Ingredients = recipeUpdate.Ingredients,
        };
        await RecipeService.UpdateRecipeAsync(recipeUpdate);


        RecipeSearchService.UploadRecipe(recipe);
        return Ok();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteRecipe(RecipeDelete recipeDelete)
    {
        await RecipeService.DeleteRecipeAsync(recipeDelete);
        RecipeDocument recipe = new RecipeDocument()
        {
            Id = recipeDelete.Id.ToString(),
            Email = recipeDelete.Email,
        };

        RecipeSearchService.DeleteRecipe(recipe);
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
    [Route("Search")]
    public async Task<IActionResult> SearchRecipes([FromQuery] SearchParameters searchParameters)
    {
        List<RecipeList> recipeList = new List<RecipeList>();
        if (string.IsNullOrEmpty(searchParameters.Query))
        {
            searchParameters.Query = "*";
        }
        SearchResults<RecipeDocument> results = RecipeSearchService.RunQuery(searchParameters);
        foreach (SearchResult<RecipeDocument> result in results.GetResults())
        {
            if (result.Score > 0.2)
            {
                RecipeList recipe = new RecipeList(int.Parse(result.Document.Id), result.Document.Name);
                recipeList.Add(recipe);
            }
        }

        return Ok(recipeList);
    }

    [HttpGet]
    [Route("Share/{sharingKey}")]
    public async Task<IActionResult> ShareRecipe(string sharingKey)
    {
        var recipe = await RecipeService.GetSharedRecipeAsync(sharingKey);
        return Ok(recipe);
    }
}
