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
        private RecipeClient client;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public RecipeDetails Recipe { get; set; }
        public RecipeDetailsModel(ILogger<RecipeDetailsModel> logger, RecipeClient client)
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
            RecipeDelete recipeDelete = new RecipeDelete(Id, AuthenticationUtils.GetEmail(User));
            await client.DeleteRecipeAsync(recipeDelete);
            return RedirectToPage("/Index");
        }
    }
}
