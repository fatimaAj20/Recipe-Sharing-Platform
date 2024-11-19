namespace RecipeSharingProject.Common.Dtos.Recipe;

public class RecipeFilter
{
    public string? Name { get; set; }
    public string? Cuisine { get; set; }
    public int? Skip { get; set; }
    public int? Take { get; set; }
    public string Email { get; set; }

    public RecipeFilter()
    {
        
    }

    public RecipeFilter(string? Name, string? Cuisine, int? Skip, int? Take, string Email)
    {
        this.Name = Name;
        this.Cuisine = Cuisine;
        this.Skip = Skip;
        this.Take = Take;
        this.Email = Email;
    }
}
