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
    public class RecipeDetailsModel : PageModel
    {
        private readonly ILogger<RecipeDetailsModel> _logger;
        private RecipeClient _client;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public RecipeDetails Recipe { get; set; }
        public RecipeClient Client { get; }

        public RecipeDetailsModel(ILogger<RecipeDetailsModel> logger, RecipeClient client)
        {
            _logger = logger;
            this._client = client;
        }

        public async Task OnGetAsync()
        {
            Recipe = await _client.GetRecipeByIdAsync(Id, AuthenticationUtils.GetEmail(User)); 
        }

        public string GenerateSharingUrl() 
        { 
            return _client.GenerateSharingUrl(Recipe.SharingKey); 
        }

        public async Task<IActionResult> OnPostAsync()
        {
            RecipeDelete recipeDelete = new RecipeDelete(Id, AuthenticationUtils.GetEmail(User));
            await _client.DeleteRecipeAsync(recipeDelete);
            return RedirectToPage("/Index");
        }

    }
}
