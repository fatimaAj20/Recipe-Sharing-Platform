namespace RecipeSharingProject.Client.Clients
{
    public abstract class BaseClient
    {
        protected readonly HttpClient httpClient;
        protected string baseUrl = "https://localhost:7134";

        protected BaseClient()
        {
            this.httpClient = new HttpClient();
            // TODO: fill base URL from env variable
        }
    }
}
