using Microsoft.AspNetCore.Http;

namespace RecipeSharingProject.Common.Dtos.Recipe;
public record RecipeCreate( string Name, string Ingredients, string Steps, string Email, string Cuisine, int TimeNeeded, int Serving, int Calories, double Fat, double SaturatedFat, double TransFat, int Cholesterol, int Sodium, double Carbohydrates, double Fiber, double Sugars, double Protein, string ServingSize, IFormFile? Photo);