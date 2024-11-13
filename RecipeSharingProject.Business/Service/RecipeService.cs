using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
    private IUploadService UploadService { get; }
    private ImageFileValidator ImageFileValidator { get; }

    public RecipeService(IMapper mapper, IGenericRepository<Recipe> recipeRepository, RecipeCreateValidator recipeCreateValidator,
                         RecipeUpdateValidator recipeUpdateValidator, IUploadService uploadService, ImageFileValidator imageFileValidator
        )
    {
        Mapper = mapper;
        RecipeRepository = recipeRepository;
        RecipeCreateValidator = recipeCreateValidator;
        RecipeUpdateValidator = recipeUpdateValidator;
        UploadService = uploadService;
        ImageFileValidator = imageFileValidator;
    }

    public async Task<int> CreateRecipeAsync(RecipeCreate recipeCreate)
    {
        await RecipeCreateValidator.ValidateAndThrowAsync(recipeCreate);
        string filename = null;

        if (recipeCreate.Photo != null)
        {
            await ImageFileValidator.ValidateAndThrowAsync(recipeCreate.Photo);
            filename = await UploadService.UploadFileAsync(recipeCreate.Photo);
        }

        var entity = Mapper.Map<Recipe>(recipeCreate);
        entity.RecipePhotoPath = filename;
        entity.SharingKey = Guid.NewGuid().ToString();
        await RecipeRepository.InsertAsync(entity);
        await RecipeRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteRecipeAsync(RecipeDelete recipeDelete)
    {
        var entity = await RecipeRepository.GetByIdAsync(recipeDelete.Id);

        if (entity == null || !entity.Email.Equals(recipeDelete.Email))
            throw new RecipeNotFoundException(recipeDelete.Id);

        RecipeRepository.Delete(entity);
        await RecipeRepository.SaveChangesAsync();
    }

    public async Task<RecipeDetails> GetRecipeAsync(int id, string email)
    {
        var recipe = await RecipeRepository.GetByIdAsync(id);

        if (recipe == null || !recipe.Email.Equals(email))
            throw new RecipeNotFoundException(id);
        RecipeDetails recipeDetails = Mapper.Map<RecipeDetails>(recipe);

        // Populate the photoUrl using the path from the recipe.
        if (recipe.RecipePhotoPath != null)
        {
            recipeDetails.PhotoUrl = UploadService.GetFileUrl(recipe.RecipePhotoPath);
        }

        return recipeDetails;
    }

    public async Task<RecipeDetails> GetSharedRecipeAsync(string sharingKey)
    {
        Expression<Func<Recipe, bool>> sharingKeyFilter = (recipe) => recipe.SharingKey.StartsWith(sharingKey);

        var recipe = await RecipeRepository.GetFilterAsync(new Expression<Func<Recipe, bool>>[]
        {
            sharingKeyFilter
        }, 0, 1
        );

        if (recipe[0] == null)
            throw new RecipeNotFoundException();
        RecipeDetails recipeDetails = Mapper.Map<RecipeDetails>(recipe[0]);

        // Populate the photoUrl using the path from the recipe.
        if (recipe[0].RecipePhotoPath != null)
        {
            recipeDetails.PhotoUrl = UploadService.GetFileUrl(recipe[0].RecipePhotoPath);
        }

        return recipeDetails;
    }

    public async Task<List<RecipeList>> GetRecipesAsync(RecipeFilter recipeFilter)
    {
        Expression<Func<Recipe, bool>> NameFilter = (recipe) => recipeFilter.Name == null ? true :
        recipe.Name.ToLower().StartsWith(recipeFilter.Name.ToLower());

        Expression<Func<Recipe, bool>> EmailFilter = (recipe) => recipeFilter.Email == null ? false :
        recipeFilter.Email.Equals(recipe.Email);

        var entities = await RecipeRepository.GetFilterAsync(new Expression<Func<Recipe, bool>>[]
        {
            NameFilter, EmailFilter
        }, recipeFilter.Skip, recipeFilter.Take
        );

        return Mapper.Map<List<RecipeList>>(entities);
    }

    public async Task UpdateRecipeAsync(RecipeUpdate recipeUpdate)
    {
        await RecipeUpdateValidator.ValidateAndThrowAsync(recipeUpdate);

        var existingentity = await RecipeRepository.GetByIdAsync(recipeUpdate.Id);
        string filename = null;

        if (existingentity == null || !existingentity.Email.Equals(recipeUpdate.Email))
            throw new RecipeNotFoundException(recipeUpdate.Id);

        if (recipeUpdate.Photo != null)
        {
            await ImageFileValidator.ValidateAndThrowAsync(recipeUpdate.Photo);
            filename = await UploadService.UploadFileAsync(recipeUpdate.Photo);
            existingentity.RecipePhotoPath = filename;
        }

        Mapper.Map(recipeUpdate, existingentity);
        RecipeRepository.Update(existingentity);
        await RecipeRepository.SaveChangesAsync();
    }

}
