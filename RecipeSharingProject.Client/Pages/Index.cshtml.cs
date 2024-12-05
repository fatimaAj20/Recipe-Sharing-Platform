using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeSharingProject.Client.Clients;
using RecipeSharingProject.Client.Utils;
using RecipeSharingProject.Common.Dtos.Recipe;

namespace RecipeSharingProject.Client.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private RecipeClient client;
        public List<RecipeList> Recipes;

        [BindProperty(SupportsGet = true)]
        public SearchParameters searchParameters { get; set; }

        public IndexModel(ILogger<IndexModel> logger, RecipeClient client)
        {
            this._logger = logger;
            this.client = client;
            this.searchParameters = new SearchParameters(null, null);
        }

        public async Task OnGetAsync()
        {
            searchParameters.Email = AuthenticationUtils.GetEmail(User);
            this.Recipes = await client.SearchForRecipe(searchParameters);
        }
    }
}
