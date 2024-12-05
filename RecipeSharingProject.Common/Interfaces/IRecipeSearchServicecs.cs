using Azure.Search.Documents.Models;
using RecipeSharingProject.Common.Dtos.Recipe;

namespace RecipeSharingProject.Common.Interfaces;

public interface IRecipeSearchService
{
    void CreateIndex();
    void UploadRecipe(RecipeDocument recipe);
    void DeleteRecipe(RecipeDocument recipe);

    SearchResults<RecipeDocument> RunQuery(SearchParameters searchParameters);
}