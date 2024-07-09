using Newtonsoft.Json;
using RecipeSharingProject.Common.Dtos;
using RecipeSharingProject.Common.Dtos.Recipe;
using System.Net.Http;
using System.Text;

namespace RecipeSharingProject.Client.Clients
{
    public class RecipeClient : BaseClient
    {
        private string controllerName = "recipe";
        public RecipeClient() { }

        public async Task<List<RecipeList>> GetRecipesAsync(RecipeFilter? filter)
        {
            string url = $"{baseUrl}/{controllerName}/get";

            if (filter != null)
            {
                List<string> queryParams = new List<string>();

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    queryParams.Add($"name={filter.Name}");
                }
                if (filter.Take != null && filter.Take > 0)
                {
                    queryParams.Add($"take={filter.Take}");
                }
                if (filter.Skip != null && filter.Skip > 0)
                {
                    queryParams.Add($"skip={filter.Skip}");
                }

                if (queryParams.Count > 0)
                {
                    url += "?" + string.Join("&", queryParams);
                }
            }

            Console.WriteLine($"{url}");
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
                    Console.WriteLine($"statusCode : {response.StatusCode}");
                    // Log status code
                }
            }
            catch (Exception e)
            {

                Console.WriteLine($"{e}");
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

        public async Task<RecipeDetails> GetRecipeByIdAsync(int id)
        {
            string url = $"{baseUrl}/{controllerName}/get/{id}";
            Console.WriteLine($"{url}");
            try
            {
                // Send the GET request
                HttpResponseMessage response = await this.httpClient.GetAsync(url);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RecipeDetails>(responseBody);
                }
                else
                {
                    Console.WriteLine($"statusCode : {response.StatusCode}");
                    // Log status code
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                // Log exception
            }

            return null ;
        }
        public async Task DeleteRecipeAsync(RecipeDelete recipeDelete)
        {   
            string url = $"{baseUrl}/{controllerName}/Delete";

            try
            {
                var json = JsonConvert.SerializeObject(recipeDelete);
                var content = new StringContent(json, Encoding.UTF8,"application/json");
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(url),
                    Content = content
                };

                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Recipe deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Error deleting recipe: {response.StatusCode}");
                }

            }
            catch (HttpRequestException e)
            {
                // Log exception
            }
        }
        public async Task UpdateRecipeAsync(RecipeUpdate recipeUpdate)
        {
            string url = $"{baseUrl}/{controllerName}/Update";

            try
            {
                // Serialize the RecipeCreate object to JSON
                var json = JsonConvert.SerializeObject(recipeUpdate);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send the POST request
                HttpResponseMessage response = await this.httpClient.PutAsync(url, content);

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
