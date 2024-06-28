namespace RecipeSharingProject.Common.Dtos;

public record RecipeDetails(int Id, string Name,List<string> Ingredients, List<string> Steps);
