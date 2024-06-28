namespace RecipeSharingProject.Common.Dtos.Recipe;

public record RecipeCreate(string Name, List<string> Ingredients, List<string> Steps);