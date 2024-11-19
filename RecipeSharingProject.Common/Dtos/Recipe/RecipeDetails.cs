namespace RecipeSharingProject.Common.Dtos;

public class RecipeDetails
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Ingredients { get; set; }
    public string Steps { get; set; }
    public string SharingKey { get; set; }
    public string Cuisine { get; set; }
    public int TimeNeeded { get; set; }
    public int Serving { get; set; }
    public int Calories { get; set; }
    public double Fat { get; set; }
    public double SaturatedFat { get; set; }
    public double TransFat { get; set; }
    public int Cholesterol { get; set; }
    public int Sodium { get; set; }
    public double Carbohydrates { get; set; }
    public double Fiber { get; set; }
    public double Sugars { get; set; }
    public double Protein { get; set; }
    public string ServingSize { get; set; }
    public string? PhotoUrl { get; set; }
} 
