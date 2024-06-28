using System.Runtime.Serialization;

namespace RecipeSharingProject.Business.Exceptions;

[Serializable]
public class RecipeNotFoundException : Exception
{
    public int Id { get; }

    public RecipeNotFoundException()
    {
    }

    public RecipeNotFoundException(int id)
    {
        this.Id = id;
    }

    public RecipeNotFoundException(string? message) : base(message)
    {
    }

    public RecipeNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected RecipeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}