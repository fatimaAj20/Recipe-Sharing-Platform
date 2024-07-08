using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeSharingProject.Client.Clients;
using RecipeSharingProject.Common.Dtos.Recipe;

namespace RecipeSharingProject.Client.Pages.Recipe
{
    public class RecipeModel : PageModel
    {
        private readonly ILogger<RecipeModel> _logger;
        private RecipeClient client;
        [BindProperty]
        public RecipeCreate Recipe { get; set; }
        

        public RecipeModel(ILogger<RecipeModel> logger)
        {
            _logger = logger;
            client = new RecipeClient();
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
          

            await client.CreateRecipeAsync(Recipe);

            return RedirectToPage("/Index");
        }
    }
}
