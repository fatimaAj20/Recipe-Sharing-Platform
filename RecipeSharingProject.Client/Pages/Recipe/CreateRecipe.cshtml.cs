using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeSharingProject.Client.Clients;
using RecipeSharingProject.Client.Utils;
using RecipeSharingProject.Common.Dtos.Recipe;

namespace RecipeSharingProject.Client.Pages.Recipe
{
    [Authorize]
    public class RecipeModel : PageModel
    {
        private readonly ILogger<RecipeModel> _logger;
        private RecipeClient client;

        public RecipeModel(ILogger<RecipeModel> logger, RecipeClient client)
        {
            _logger = logger;
            this.client = client;
        }

        [BindProperty]
        public IFormFile? RecipePicture { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var ingredients = Request.Form["Ingredients"];
            var steps = Request.Form["Steps"];

            RecipeCreate recipeCreate = new RecipeCreate(Request.Form["Name"], ingredients, steps, AuthenticationUtils.GetEmail(User),
                Request.Form["Cuisine"], int.Parse(Request.Form["TimeNeeded"]),
                int.Parse(Request.Form["Serving"]),
                int.Parse(Request.Form["Calories"]),
                double.Parse(Request.Form["Fat"]),
                double.Parse(Request.Form["SaturatedFat"]),
                double.Parse(Request.Form["TransFat"]),
                int.Parse(Request.Form["Cholesterol"]),
                int.Parse(Request.Form["Sodium"]),
                double.Parse(Request.Form["Carbohydrates"]),
                double.Parse(Request.Form["Fiber"]),
                double.Parse(Request.Form["Sugars"]),
                double.Parse(Request.Form["Protein"]),
                Request.Form["ServingSize"], RecipePicture);

            await client.CreateRecipeAsync(recipeCreate);

            return RedirectToPage("/Index");
        }
    }
}
