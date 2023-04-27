namespace IntelliTect.Multitool.Extensions;

/// <summary>
/// Various Uri Extensions
/// </summary>
public static class HttpExtensions
{
    /// <summary>
    /// Validates a Uri by attempting to make a GET request to it.
    /// </summary>
    /// <param name="client">The HttpClient to make the request</param>
    /// <param name="uri">The Uri to validate</param>
    /// <param name="headerValue">A string for what will be passed in the header of the validation.</param>
    /// <returns>The result of the call</returns>
    public static async Task<bool> ValidateUri(this HttpClient client, Uri uri, string headerValue = "IntelliTect.Multitool")
    {
        try
        {
            // Clear then add a user-agent header in case the API requires one (such as github.com or stackoverflow.com)
            // https://docs.github.com/rest/overview/resources-in-the-rest-api#user-agent-required
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue(new System.Net.Http.Headers.ProductHeaderValue(headerValue)));

            HttpResponseMessage result = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
}
