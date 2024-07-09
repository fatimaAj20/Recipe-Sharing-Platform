using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeSharingProject.Client.Clients;
using RecipeSharingProject.Common.Dtos;
using RecipeSharingProject.Common.Dtos.Recipe;

namespace RecipeSharingProject.Client.Pages.Recipe
{
    public class EditRecipeModel : PageModel
    {
        private readonly ILogger<EditRecipeModel> _logger;
        private RecipeClient client;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public RecipeDetails Recipe { get; set; }


        public EditRecipeModel(ILogger<EditRecipeModel> logger)
        {
            _logger = logger;
            client = new RecipeClient();
        }
        public async Task OnGetAsync()
        {
            this.Recipe = await client.GetRecipeByIdAsync(Id);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var ingredients = Request.Form["Ingredients"].ToString().Split(',', System.StringSplitOptions.RemoveEmptyEntries);
            var steps = Request.Form["Steps"].ToString().Split(',', System.StringSplitOptions.RemoveEmptyEntries);

            RecipeUpdate recipeUpdate = new RecipeUpdate(Recipe.Id, Request.Form["Name"], new List<string>(ingredients), new List<string>(steps));
            await client.UpdateRecipeAsync(recipeUpdate);

            return RedirectToPage($"/Recipe/RecipeDetails", new {Id =Id});
        }
    }
}
