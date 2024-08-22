namespace RecipeSharingProject.Common.Dtos;

public class RecipeDetails
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Ingredients { get; set; }
    public string Steps { get; set; }
    public string? PhotoUrl { get; set; }
} 
