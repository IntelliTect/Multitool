namespace IntelliTect.Multitool.Extensions;

/// <summary>
/// Various Uri Extensions
/// </summary>
public static class UriExtensions
{
    private static HttpClient HttpClient { get; } = new();

    /// <summary>
    /// Validates a Uri by attempting to make a GET request to it.
    /// </summary>
    /// <param name="uri">The Uri you want to validate</param>
    /// <returns>The result of the call</returns>
    public static async Task<bool> ValidateUri(this Uri uri)
    {
        try
        {
            // Clear then add a user-agent header in case the API requires one (such as github.com or stackoverflow.com)
            // https://docs.github.com/rest/overview/resources-in-the-rest-api#user-agent-required
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue(new System.Net.Http.Headers.ProductHeaderValue("IntelliTect.Multitool")));

            HttpResponseMessage result = await HttpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
}
