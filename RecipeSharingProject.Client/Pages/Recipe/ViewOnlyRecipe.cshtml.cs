using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeSharingProject.Client.Clients;
using RecipeSharingProject.Client.Utils;
using RecipeSharingProject.Common.Dtos.Recipe;
using RecipeSharingProject.Common.Dtos;

namespace RecipeSharingProject.Client.Pages.Recipe
{
    public class ViewOnlyRecipeModel : PageModel
    {
        private readonly ILogger<RecipeDetailsModel> _logger;
        private RecipeClient _client;

        [BindProperty(SupportsGet = true)]
        public string SharingKey { get; set; }

        public RecipeDetails Recipe { get; set; }
        public RecipeClient Client { get; }

        public ViewOnlyRecipeModel(ILogger<RecipeDetailsModel> logger, RecipeClient client)
        {
            _logger = logger;
            this._client = client;
        }
        public async Task OnGetAsync()
        {
            Recipe = await _client.GetRecipeBySharingKey(SharingKey);
        }

    }
}
