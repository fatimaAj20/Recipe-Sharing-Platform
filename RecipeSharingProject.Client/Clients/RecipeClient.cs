using Newtonsoft.Json;
using RecipeSharingProject.Common.Dtos.Recipe;
using System.Text;

namespace RecipeSharingProject.Client.Clients
{
    public class RecipeClient : BaseClient
    {
        private string controllerName = "recipe";
        public RecipeClient() { }

        public async Task<List<RecipeList>> GetRecipesAsync()
        {
            string url = $"{baseUrl}/{controllerName}/get";
            List<RecipeList> recipes = new List<RecipeList>();
            try
            {
                // Send the GET request
                HttpResponseMessage response = await this.httpClient.GetAsync(url);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseBody = await response.Content.ReadAsStringAsync();
                    recipes = JsonConvert.DeserializeObject<List<RecipeList>>(responseBody);
                }
                else
                {

                    // Log status code
                }
            }
            catch (HttpRequestException e)
            {
                // Log exception
            }

            return recipes;
        }
        public async Task CreateRecipeAsync(RecipeCreate recipeCreate)
        {
            string url = $"{baseUrl}/{controllerName}/create";

            try
            {
                // Serialize the RecipeCreate object to JSON
                var json = JsonConvert.SerializeObject(recipeCreate);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send the POST request
                HttpResponseMessage response = await this.httpClient.PostAsync(url, content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Recipe created successfully.");
                }
                else
                {
                    Console.WriteLine($"Error creating recipe: {response.StatusCode}");
                }

            }
            catch (HttpRequestException e)
            {
                // Log exception
            }

        }
    }
}
