

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
                var jsonStream = await client.GetStreamAsync(url);
                var jsonDoc = await JsonDocument.ParseAsync(jsonStream);
                jsonStream.Close();
                return jsonDoc;
            }
        }
    }
}
