using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace RecipeSharingProject.Common.Dtos.Recipe;

public class RecipeDocument()
{
    [SimpleField(IsKey = true, IsFilterable = true)]
    public string Id { get; set; }

    [SearchableField(IsSortable = true)]
    public string Name { get; set; }

    [SimpleField(IsFilterable = true)]
    public string Email { get; set; }

    [SearchableField(IsFilterable = true, IsFacetable = true)]
    public string Cuisine { get; set; }

    [SearchableField(AnalyzerName = LexicalAnalyzerName.Values.EnLucene)]
    public string Ingredients { get; set; }
}
