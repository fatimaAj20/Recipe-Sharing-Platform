using Newtonsoft.Json;
using RecipeSharingProject.Common.Dtos;
using RecipeSharingProject.Common.Dtos.Recipe;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace RecipeSharingProject.Client.Clients
{
    public class RecipeClient : BaseClient
    {
        private string controllerName = "recipe";
        public RecipeClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

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
                if (!string.IsNullOrEmpty(filter.Email))
                {
                    queryParams.Add($"email={filter.Email}");
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

            using (var formContent = new MultipartFormDataContent())
            {
                // Add basic recipe data to the form
                formContent.Add(new StringContent(recipeCreate.Name), "Name");
                formContent.Add(new StringContent(recipeCreate.Email), "Email");
                formContent.Add(new StringContent(recipeCreate.Ingredients), "Ingredients");
                formContent.Add(new StringContent(recipeCreate.Steps), "Steps");

                // If an image file is provided, add it to the form content
                if (recipeCreate.Photo != null)
                {
                    var fileContent = new StreamContent(recipeCreate.Photo.OpenReadStream());
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(recipeCreate.Photo.ContentType);
                    formContent.Add(fileContent, "Photo", recipeCreate.Photo.FileName);
                }

                try
                {
                    // Post the form data to the API
                    HttpResponseMessage response = await this.httpClient.PostAsync(url, formContent);

                    // Handle response
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Recipe created successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Error creating recipe, error:{await response.Content.ReadAsStringAsync()}, {response.StatusCode}");
                    }
                }
                catch (HttpRequestException e)
                {
                    // Log exception
                    Console.WriteLine(e.Message);
                }
            }
        }

        public async Task UpdateRecipeAsync(RecipeUpdate recipeUpdate)
        {
            string url = $"{baseUrl}/{controllerName}/Update";
            using (var formContent = new MultipartFormDataContent())
            {
                // Add basic recipe data to the form
                formContent.Add(new StringContent(recipeUpdate.Id.ToString()), "Id");
                formContent.Add(new StringContent(recipeUpdate.Name), "Name");
                formContent.Add(new StringContent(recipeUpdate.Email), "Email");
                formContent.Add(new StringContent(recipeUpdate.Ingredients), "Ingredients");
                formContent.Add(new StringContent(recipeUpdate.Steps), "Steps");

                // If an image file is provided, add it to the form content
                if (recipeUpdate.Photo != null)
                {
                    var fileContent = new StreamContent(recipeUpdate.Photo.OpenReadStream());
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(recipeUpdate.Photo.ContentType);
                    formContent.Add(fileContent, "Photo", recipeUpdate.Photo.FileName);
                }

                try
                {
                    // Post the form data to the API
                    HttpResponseMessage response = await this.httpClient.PutAsync(url, formContent);

                    // Handle response
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Recipe created successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Error creating recipe, error:{await response.Content.ReadAsStringAsync()}, {response.StatusCode}");
                    }
                }
                catch (HttpRequestException e)
                {
                    // Log exception
                    Console.WriteLine(e.Message);
                }
            }
        }

        public async Task<RecipeDetails> GetRecipeByIdAsync(int id, string email)
        {
            string url = $"{baseUrl}/{controllerName}/get/{id}?email={email}";
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

            return null;
        }
        public async Task DeleteRecipeAsync(RecipeDelete recipeDelete)
        {
            string url = $"{baseUrl}/{controllerName}/Delete";

            try
            {
                var json = JsonConvert.SerializeObject(recipeDelete);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
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
    }
}
