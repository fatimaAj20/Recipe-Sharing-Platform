using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingProject.API.Controllers;
using RecipeSharingProject.Business.Exceptions;
using System.Text.Json;

namespace RecipeSharingProject.API;

public class ExceptionMiddleware
{
    private RequestDelegate Next { get; }
    private ILogger<ExceptionMiddleware> Logger { get; }
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> _logger)
    {
        Next = next;
        this.Logger = _logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await Next(context);
        }
        catch (RecipeNotFoundException ex)
        {
            Logger.LogError(ex, "Recipe not Found");
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = string.Empty,
                Instance = "",
                Title = $"Recipe for id {ex.Id} not found.",
                Type = "Error"
            };

            var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(problemDetailsJson);
        }
        catch (ValidationException ex)
        {
            Logger.LogError(ex, "Not valid  input");

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = JsonSerializer.Serialize(ex.Errors),
                Instance = "",
                Title = "Validation Error",
                Type = "Error"

            };

            var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(problemDetailsJson);

        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Internal server error");

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Detail = ex.Message,
                Instance = "",
                Title = "Internal Server Error - something went wrong.",
                Type = "Error"

            };

            var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(problemDetailsJson);

        }
    }
}