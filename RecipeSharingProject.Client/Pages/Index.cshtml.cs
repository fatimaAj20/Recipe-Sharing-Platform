using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RecipeSharingProject.Client.Clients;
using RecipeSharingProject.Common.Dtos.Recipe;
using System.Text.Json.Serialization;

namespace RecipeSharingProject.Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private RecipeClient client;
        public List<RecipeList> Recipes;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            client = new RecipeClient();
        }

        public async Task OnGetAsync()
        {
            this.Recipes = await client.GetRecipesAsync();
        }
    }
}
