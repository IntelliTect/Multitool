using System.Net.Http.Headers;

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
    /// <param name="headerValues">Header values to add to the clients user agent request headers.</param>
    /// <param name="completionOption">A completion option if the default completion option is not desired</param>
    /// <returns>The result of the call</returns>
    public static async Task<bool> ValidateUri(this HttpClient client, Uri uri, ProductInfoHeaderValue[]? headerValues = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseHeadersRead)
    {
        try
        {
            // Clear then add a user-agent header in case the API requires one (such as github.com or stackoverflow.com)
            // https://docs.github.com/rest/overview/resources-in-the-rest-api#user-agent-required
            client.DefaultRequestHeaders.Clear();
            if (headerValues is not null)
            {
                foreach (ProductInfoHeaderValue value in headerValues)
                {
                    client.DefaultRequestHeaders.UserAgent.Add(value);
                }
            }

            HttpResponseMessage result = await client.GetAsync(uri, completionOption);
            result.EnsureSuccessStatusCode();
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Validates a Uri by attempting to make a GET request to it.
    /// </summary>
    /// <param name="client">The HttpClient to make the request</param>
    /// <param name="uri">The Uri to validate</param>
    /// <param name="headerValue">Header value to add to the clients user agent request header.</param>
    /// <param name="completionOption">A completion option if the default completion option is not desired</param>
    /// <returns>The result of the call</returns>
    public static async Task<bool> ValidateUri(this HttpClient client, Uri uri, ProductInfoHeaderValue? headerValue, HttpCompletionOption completionOption = HttpCompletionOption.ResponseHeadersRead)
    {
        return await ValidateUri(client, uri, headerValue is null ? null : new ProductInfoHeaderValue[] { headerValue }, completionOption);
    }
}
