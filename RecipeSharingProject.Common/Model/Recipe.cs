namespace RecipeSharingProject.Common.Model;

public class Recipe : BaseEntity
{
    
    public string Name { get; set; } = default!;
    public string Ingredients {get;set;} =default!;
    public string Steps { get; set; } = default!;
    public string SharingKey { get; set; } =default!;
    public string Email { get; set; } =default!;
    public string Cuisine { get; set; }=default!;
    public int TimeNeeded { get; set; } =default!;
    public int Serving {  get; set; } =default!;
    public int Calories { get; set; } = default!;
    public double Fat { get; set; } = default!;
    public double SaturatedFat { get; set; } = default!;
    public double TransFat { get; set; } = default!;
    public int Cholesterol { get; set; } = default!;
    public int Sodium { get; set; } = default!;
    public double Carbohydrates { get; set; } = default!;
    public double Fiber { get; set; } = default!;
    public double Sugars { get; set; } = default!;
    public double Protein { get; set; } = default!;
    public string ServingSize { get; set; }= default!;
    public string? RecipePhotoPath { get; set; } 
}
