namespace RecipeSharingProject.Common.Dtos.Recipe;

public class SearchParameters
{
    public string? Query { get; set; }
    public string Email { get; set; }

    public SearchParameters()
    {

    }

    public SearchParameters(string? Query, string Email)
    {
        this.Query = Query;
        this.Email = Email;
    }
}
