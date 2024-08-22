using FluentValidation;
using RecipeSharingProject.Common.Dtos.Recipe;

namespace RecipeSharingProject.Business.Validation;

public class RecipeUpdateValidator : AbstractValidator<RecipeUpdate>
{
    public RecipeUpdateValidator()
    {
        RuleFor(recipeUpdate => recipeUpdate.Name).NotEmpty().MaximumLength(50);
        RuleFor(recipeCreate => recipeCreate.Email).NotEmpty().EmailAddress();
    }
}
