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
                if (!string.IsNullOrEmpty(filter.Cuisine))
                {
                    queryParams.Add($"cuisine={filter.Cuisine}");
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

            List<RecipeList> recipes = new List<RecipeList>();
            try
            {
                HttpResponseMessage response = await this.httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    recipes = JsonConvert.DeserializeObject<List<RecipeList>>(responseBody);
                }
                else
                {
                    Console.WriteLine($"statusCode : {response.StatusCode}");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine($"{e}");
            }

            return recipes;
        }

        public async Task CreateRecipeAsync(RecipeCreate recipeCreate)
        {
            string url = $"{baseUrl}/{controllerName}/create";

            using (var formContent = new MultipartFormDataContent())
            {
                formContent.Add(new StringContent(recipeCreate.Name), "Name");
                formContent.Add(new StringContent(recipeCreate.Email), "Email");
                formContent.Add(new StringContent(recipeCreate.Ingredients), "Ingredients");
                formContent.Add(new StringContent(recipeCreate.Steps), "Steps");
                formContent.Add(new StringContent(recipeCreate.Cuisine), "Cuisine");
                formContent.Add(new StringContent(recipeCreate.TimeNeeded.ToString()), "TimeNeeded");
                formContent.Add(new StringContent(recipeCreate.Serving.ToString()), "Serving");
                formContent.Add(new StringContent(recipeCreate.Calories.ToString()), "Calories");
                formContent.Add(new StringContent(recipeCreate.Fat.ToString()), "Fat");
                formContent.Add(new StringContent(recipeCreate.SaturatedFat.ToString()), "SaturatedFat");
                formContent.Add(new StringContent(recipeCreate.TransFat.ToString()), "TransFat");
                formContent.Add(new StringContent(recipeCreate.Cholesterol.ToString()), "Cholesterol");
                formContent.Add(new StringContent(recipeCreate.Sodium.ToString()), "Sodium");
                formContent.Add(new StringContent(recipeCreate.Carbohydrates.ToString()), "Carbohydrates");
                formContent.Add(new StringContent(recipeCreate.Fiber.ToString()), "Fiber");
                formContent.Add(new StringContent(recipeCreate.Sugars.ToString()), "Sugars");
                formContent.Add(new StringContent(recipeCreate.Protein.ToString()), "Protein");
                formContent.Add(new StringContent(recipeCreate.ServingSize), "ServingSize");


                if (recipeCreate.Photo != null)
                {
                    var fileContent = new StreamContent(recipeCreate.Photo.OpenReadStream());
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(recipeCreate.Photo.ContentType);
                    formContent.Add(fileContent, "Photo", recipeCreate.Photo.FileName);
                }

                try
                {
                    HttpResponseMessage response = await this.httpClient.PostAsync(url, formContent);

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
                    Console.WriteLine(e.Message);
                }
            }
        }

        public async Task UpdateRecipeAsync(RecipeUpdate recipeUpdate)
        {
            string url = $"{baseUrl}/{controllerName}/Update";
            using (var formContent = new MultipartFormDataContent())
            {
                formContent.Add(new StringContent(recipeUpdate.Id.ToString()), "Id");
                formContent.Add(new StringContent(recipeUpdate.Name), "Name");
                formContent.Add(new StringContent(recipeUpdate.Email), "Email");
                formContent.Add(new StringContent(recipeUpdate.Ingredients), "Ingredients");
                formContent.Add(new StringContent(recipeUpdate.Steps), "Steps");
                formContent.Add(new StringContent(recipeUpdate.Cuisine), "Cuisine");
                formContent.Add(new StringContent(recipeUpdate.TimeNeeded.ToString()), "TimeNeeded");
                formContent.Add(new StringContent(recipeUpdate.Serving.ToString()), "Serving");
                formContent.Add(new StringContent(recipeUpdate.Calories.ToString()), "Calories");
                formContent.Add(new StringContent(recipeUpdate.Fat.ToString()), "Fat");
                formContent.Add(new StringContent(recipeUpdate.SaturatedFat.ToString()), "SaturatedFat");
                formContent.Add(new StringContent(recipeUpdate.TransFat.ToString()), "TransFat");
                formContent.Add(new StringContent(recipeUpdate.Cholesterol.ToString()), "Cholesterol");
                formContent.Add(new StringContent(recipeUpdate.Sodium.ToString()), "Sodium");
                formContent.Add(new StringContent(recipeUpdate.Carbohydrates.ToString()), "Carbohydrates");
                formContent.Add(new StringContent(recipeUpdate.Fiber.ToString()), "Fiber");
                formContent.Add(new StringContent(recipeUpdate.Sugars.ToString()), "Sugars");
                formContent.Add(new StringContent(recipeUpdate.Protein.ToString()), "Protein");
                formContent.Add(new StringContent(recipeUpdate.ServingSize), "ServingSize");

                if (recipeUpdate.Photo != null)
                {
                    var fileContent = new StreamContent(recipeUpdate.Photo.OpenReadStream());
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(recipeUpdate.Photo.ContentType);
                    formContent.Add(fileContent, "Photo", recipeUpdate.Photo.FileName);
                }

                try
                {
                    HttpResponseMessage response = await this.httpClient.PutAsync(url, formContent);

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
                    Console.WriteLine(e.Message);
                }
            }
        }

        public async Task<RecipeDetails> GetRecipeByIdAsync(int id, string email)
        {
            string url = $"{baseUrl}/{controllerName}/get/{id}?email={email}";
            try
            {
                HttpResponseMessage response = await this.httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RecipeDetails>(responseBody);
                }
                else
                {
                    Console.WriteLine($"statusCode : {response.StatusCode}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
            }

            return null;
        }

        public async Task<RecipeDetails> GetRecipeBySharingKey(string sharingkey)
        {
            string url = $"{baseUrl}/{controllerName}/share/{sharingkey}";
            try
            {
                HttpResponseMessage response = await this.httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RecipeDetails>(responseBody);
                }
                else
                {
                    Console.WriteLine($"statusCode : {response.StatusCode}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
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
            }
        }

        public string GenerateSharingUrl(string sharingKey)
        {
            return $"{clientBaseUrl}/Recipe/Shared/{sharingKey}";
        }

        public async Task<List<RecipeList>> SearchForRecipe(SearchParameters searchParameters)
        {
            string url = $"{baseUrl}/{controllerName}/Search";

            if (searchParameters != null)
            {
                List<string> queryParams = new List<string>();

                if (!string.IsNullOrEmpty(searchParameters.Query))
                {
                    queryParams.Add($"query={searchParameters.Query}");
                }

                if (!string.IsNullOrEmpty(searchParameters.Email))
                {
                    queryParams.Add($"email={searchParameters.Email}");
                }
                if (queryParams.Count > 0)
                {
                    url += "?" + string.Join("&", queryParams);
                }
            }
            List<RecipeList> recipes = new List<RecipeList>();
            try
            {
                HttpResponseMessage response = await this.httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    recipes = JsonConvert.DeserializeObject<List<RecipeList>>(responseBody);
                }
                else
                {
                    Console.WriteLine($"statusCode : {response.StatusCode}");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine($"{e}");
            }

            return recipes;
        }

    }
}
