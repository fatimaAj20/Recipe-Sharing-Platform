using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RecipeSharingProject.Client.Clients;
using RecipeSharingProject.Client.Utils;
using RecipeSharingProject.Common.Dtos;
using RecipeSharingProject.Common.Dtos.Recipe;

namespace RecipeSharingProject.Client.Pages.Recipe
{
    [Authorize]
    public class EditRecipeModel : PageModel
    {
        private readonly ILogger<EditRecipeModel> _logger;
        private RecipeClient client;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public RecipeDetails Recipe { get; set; }

        [BindProperty]
        public IFormFile? RecipePicture { get; set; }

        public EditRecipeModel(ILogger<EditRecipeModel> logger, RecipeClient client)
        {
            _logger = logger;
            this.client = client;
        }

        public async Task OnGetAsync()
        {
            this.Recipe = await client.GetRecipeByIdAsync(Id, AuthenticationUtils.GetEmail(User));
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var ingredients = Request.Form["Ingredients"];
            var steps = Request.Form["Steps"];

            RecipeUpdate recipeUpdate = new RecipeUpdate(Recipe.Id, Request.Form["Name"], ingredients, steps, AuthenticationUtils.GetEmail(User), Request.Form["Cuisine"], int.Parse(Request.Form["TimeNeeded"]), int.Parse(Request.Form["Serving"]), int.Parse(Request.Form["Calories"]), double.Parse(Request.Form["Fat"]), double.Parse(Request.Form["SturatedFat"]), double.Parse(Request.Form["TransFat"]), int.Parse(Request.Form["Cholesterol"]), int.Parse(Request.Form["Sodium"]), double.Parse(Request.Form["Carbohydrates"]), double.Parse(Request.Form["Fiber"]), double.Parse(Request.Form["Sugars"]), double.Parse(Request.Form["Protein"]),  Request.Form["ServingSize"], RecipePicture);
            await client.UpdateRecipeAsync(recipeUpdate);

            return RedirectToPage($"/Recipe/RecipeDetails", new { Id = Id });
        }
    }
}
