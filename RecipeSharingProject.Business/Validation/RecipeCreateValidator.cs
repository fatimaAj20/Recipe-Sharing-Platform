using FluentValidation;
using RecipeSharingProject.Common.Dtos.Recipe;

namespace RecipeSharingProject.Business.Validation;

public class RecipeCreateValidator : AbstractValidator<RecipeCreate>
{
    public RecipeCreateValidator()
    {
        RuleFor(recipeCreate => recipeCreate.Name).NotEmpty().MaximumLength(50);
        RuleFor(recipeCreate => recipeCreate.Email).NotEmpty().EmailAddress();

    }
}
