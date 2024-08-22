namespace RecipeSharingProject.Common.Model;

public class Recipe : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Ingredients {get;set;} =default!;
    public string Steps { get; set; } = default!;
    public string Email { get; set; } =default!;
    public string? RecipePhotoPath { get; set; } 
}
