using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RecipeSharingProject.Client.Clients;
using RecipeSharingProject.Client.Utils;
using RecipeSharingProject.Common.Dtos.Recipe;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace RecipeSharingProject.Client.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private RecipeClient client;
        public List<RecipeList> Recipes;

        [BindProperty(SupportsGet = true)]
        public RecipeFilter filter { get; set; }

        public IndexModel(ILogger<IndexModel> logger, RecipeClient client)
        {
            _logger = logger;
            this.client = client;
            filter = new RecipeFilter(null, null, null, null);
        }

        public async Task OnGetAsync()
        {
            filter.Email = AuthenticationUtils.GetEmail(User);
            this.Recipes = await client.GetRecipesAsync(filter);
        }
    }
}
