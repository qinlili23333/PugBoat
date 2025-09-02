

using System.Text.Json;

namespace PugBoatCore.Util
{
    /// <summary>
    /// A static util class for all network related things
    /// </summary>
    internal static class Network
    {
        /// <summary>
        /// Get a JsonDocument from a URL. No try catch inside, so use with try catch.
        /// </summary>
        /// <param name="url">the JSON URL</param>
        /// <returns>JsonDocument object</returns>
        internal async static Task<JsonDocument> GetJsonFromUrl(string url)
        {
            using HttpClient client = new();
            {
                client.DefaultRequestHeaders.UserAgent.Clear();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36");
                var jsonStream = await client.GetStreamAsync(url);
                var jsonDoc = await JsonDocument.ParseAsync(jsonStream);
                jsonStream.Close();
                return jsonDoc;
            }
        }

        /// <summary>
        /// Compose an absolute URL from a base URL and a relative path.
        /// </summary>
        /// <param name="baseUrl">base url</param>
        /// <param name="relativePath">relative url</param>
        /// <returns>composed absolute url</returns>
        /// <exception cref="ArgumentException">url is invalid</exception>
        internal static string ComposeRelativeUrl(string baseUrl, string relativePath)
        {
            if (Uri.TryCreate(baseUrl, UriKind.Absolute, out var baseUri))
            {
                if (Uri.TryCreate(baseUri, relativePath, out var resultUri))
                {
                    return resultUri.ToString();
                }
                else
                {
                    throw new ArgumentException("Invalid relative path", nameof(relativePath));
                }
            }
            else
            {
                throw new ArgumentException("Invalid base URL", nameof(baseUrl));
            }
        }
    }
}
