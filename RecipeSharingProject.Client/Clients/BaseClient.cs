namespace RecipeSharingProject.Client.Clients
{
    public abstract class BaseClient
    {
        protected HttpClient httpClient;
        protected string baseUrl = Environment.GetEnvironmentVariable("BASE_URL");

        protected BaseClient()
        {
        }
    }
}
