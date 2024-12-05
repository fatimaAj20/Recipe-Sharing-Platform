using Azure.Search.Documents.Indexes;
using Azure.Search.Documents;
using Azure;
using Azure.Search.Documents.Models;
using RecipeSharingProject.Common.Dtos.Recipe;
using RecipeSharingProject.Common.Interfaces;

using Azure.Search.Documents.Indexes.Models;

namespace RecipeSharingProject.Infrastructure;

public class AzureRecipeSearchAIService : IRecipeSearchService
{
    private SearchClient searchclient { get; }
    private SearchIndexClient adminClient { get; }

    string indexName = "recipes-searchservice";

    public AzureRecipeSearchAIService()
    {
        string serviceName = Environment.GetEnvironmentVariable("SEARCH-SERVICE-NAME");
        string apiKey = Environment.GetEnvironmentVariable("SEARCH-SERVICE-ADMIN-API-KEY");


        Uri serviceEndpoint = new Uri($"https://{serviceName}.search.windows.net/");
        AzureKeyCredential credential = new AzureKeyCredential(apiKey);
        adminClient = new SearchIndexClient(serviceEndpoint, credential);

        searchclient = new SearchClient(serviceEndpoint, indexName, credential);
    }


    public void CreateIndex()
    {
        FieldBuilder fieldBuilder = new FieldBuilder();
        var searchFields = fieldBuilder.Build(typeof(RecipeDocument));

        var definition = new SearchIndex(indexName, searchFields);

        adminClient.CreateOrUpdateIndex(definition);

    }

    public SearchResults<RecipeDocument> RunQuery(SearchParameters searchParameters)
    {
        SearchOptions options;
        SearchResults<RecipeDocument> response;

        options = new SearchOptions()
        {
            Filter = $"Email eq '{searchParameters.Email}'",
        };

        options.Select.Add("Id");
        options.Select.Add("Name");
        options.Select.Add("Email");
        options.Select.Add("Cuisine");
        options.Select.Add("Ingredients");

        return response = searchclient.Search<RecipeDocument>(searchParameters.Query, options);
    }

    public void UploadRecipe(RecipeDocument recipe)
    {
        IndexDocumentsBatch<RecipeDocument> batch = IndexDocumentsBatch.Create(
        IndexDocumentsAction.Upload(recipe));
        try
        {
            IndexDocumentsResult result = searchclient.IndexDocuments(batch);
        }
        catch (Exception)
        {
            Console.WriteLine("Failed to index some of the documents: {0}");
        }
    }

    public void DeleteRecipe(RecipeDocument recipe)
    {
        IndexDocumentsBatch<RecipeDocument> batch = IndexDocumentsBatch.Create(
       IndexDocumentsAction.Delete(recipe));
        try
        {
            IndexDocumentsResult result = searchclient.IndexDocuments(batch);
        }
        catch (Exception)
        {
        }
    }
}
