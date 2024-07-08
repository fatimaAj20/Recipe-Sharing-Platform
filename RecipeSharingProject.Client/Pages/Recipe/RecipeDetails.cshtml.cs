using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RecipeSharingProject.Client.Clients;
using RecipeSharingProject.Common.Dtos;

namespace RecipeSharingProject.Client.Pages.Recipe
{
    public class RecipeDetailsModel : PageModel
    {
        private readonly ILogger<RecipeDetailsModel> _logger;
        private RecipeClient client;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public RecipeDetails Recipe { get; set; }
        public RecipeDetailsModel(ILogger<RecipeDetailsModel> logger)
        {
            _logger = logger;
            client = new RecipeClient();
        }
        public async Task OnGetAsync()
        {
            this.Recipe = await client.GetRecipeByIdAsync(Id);
        }
    }
}
