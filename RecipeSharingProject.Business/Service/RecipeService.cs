using AutoMapper;
using FluentValidation;
using RecipeSharingProject.Business.Exceptions;
using RecipeSharingProject.Business.Validation;
using RecipeSharingProject.Common.Dtos;
using RecipeSharingProject.Common.Dtos.Recipe;
using RecipeSharingProject.Common.Interfaces;
using RecipeSharingProject.Common.Model;
using System.Linq.Expressions;

namespace RecipeSharingProject.Business.Exceptions;

public class RecipeService : IRecipeService
{
    private IMapper Mapper { get; }

    private IGenericRepository<Recipe> RecipeRepository { get; }
    private RecipeCreateValidator RecipeCreateValidator { get; }
    private RecipeUpdateValidator RecipeUpdateValidator { get; }

    public RecipeService(IMapper mapper, IGenericRepository<Recipe> recipeRepository ,RecipeCreateValidator recipeCreateValidator,
        RecipeUpdateValidator recipeUpdateValidator
        )
    {
        Mapper = mapper;
        RecipeRepository = recipeRepository;
        RecipeCreateValidator=recipeCreateValidator;
        RecipeUpdateValidator =recipeUpdateValidator;
    }
    public async Task<int> CreateRecipeAsync(RecipeCreate recipeCreate)
    {
        await RecipeCreateValidator.ValidateAndThrowAsync(recipeCreate);

        var entity = Mapper.Map<Recipe>(recipeCreate); 
        await RecipeRepository.InsertAsync(entity);
        await RecipeRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteRecipeAsync(RecipeDelete recipeDelete)
    {
        var entity = await RecipeRepository.GetByIdAsync(recipeDelete.Id);

        if (entity == null)
            throw new RecipeNotFoundException(recipeDelete.Id);

        RecipeRepository.Delete(entity);
        await RecipeRepository.SaveChangesAsync();
    }

    public async Task<RecipeDetails> GetRecipeAsync(int id)
    {
        var recipe = await RecipeRepository.GetByIdAsync(id);

        if (recipe == null)
            throw new RecipeNotFoundException(id);

        return Mapper.Map<RecipeDetails>(recipe);
    }

    public async Task<List<RecipeList>> GetRecipesAsync(RecipeFilter recipeFilter)
    {
        Expression<Func<Recipe, bool>> NameFilter = (recipe) => recipeFilter.Name == null ? true :
        recipe.Name.ToLower().StartsWith(recipeFilter.Name.ToLower());
      

        var entities = await RecipeRepository.GetFilterAsync(new Expression<Func<Recipe, bool>>[]
        {
            NameFilter
        }, recipeFilter.Skip, recipeFilter.Take
        );
        return Mapper.Map<List<RecipeList>>(entities);
    }

    public async Task UpdateRecipeAsync(RecipeUpdate recipeUpdate)
    {
        await RecipeUpdateValidator.ValidateAndThrowAsync(recipeUpdate);

        var existingentity = await RecipeRepository.GetByIdAsync(recipeUpdate.Id);

        if (existingentity == null)
            throw new RecipeNotFoundException(recipeUpdate.Id);

        var entity = Mapper.Map<Recipe>(recipeUpdate);
        RecipeRepository.Update(entity);
        await RecipeRepository.SaveChangesAsync();
    }
}
