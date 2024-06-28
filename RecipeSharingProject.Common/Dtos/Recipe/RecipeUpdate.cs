namespace RecipeSharingProject.Common.Dtos.Recipe;

public record RecipeUpdate(int Id, string Name, List<string> Ingredients, List<string> Steps);