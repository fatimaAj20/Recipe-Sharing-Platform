namespace RecipeSharingProject.Client.Clients
{
    public abstract class BaseClient
    {
        protected readonly HttpClient httpClient;
        protected string baseUrl =Environment.GetEnvironmentVariable("BASE_URL");
        protected string subscriptionKey = Environment.GetEnvironmentVariable("SUBSCRIPTION_KEY");

        protected BaseClient()
        {
            this.httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", $"{subscriptionKey}");
            // TODO: fill base URL from env variable
        }
    }
}
