namespace RecipeSharingProject.Common.Model;

public class Recipe : BaseEntity
{
    public string Name { get; set; } = default!;
    public List<string> Ingredients {get;set;} =default!;
    public List<string> Steps { get; set; } = default!;
}
