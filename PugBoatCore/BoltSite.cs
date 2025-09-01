using System.Text.Json;

namespace PugBoatCore
{
    /// <summary>
    /// Object representing a Bolt site.
    /// </summary>
    public class BoltSite
    {
        private JsonDocument JsonDoc;
        /// <summary>
        /// Private constructor, use static methods to create instances.
        /// </summary>
        private BoltSite(JsonDocument Json)
        {
            JsonDoc = Json;
        }

        public bool IsValidSite()
        {
            return JsonDoc.RootElement.TryGetProperty("timelines", out var timelines) && timelines.GetArrayLength() > 0;
        }


        static public async Task<BoltSite?> CreateFromDomain(string domain)
        {
            try
            {
                var Jsondoc = await Util.Network.GetJsonFromUrl($"https://{domain}/timelines.json");
                return new BoltSite(Jsondoc);
            }
            catch (Exception)
            {
                return null;
            }
        }

        static public async Task<BoltSite?> CreateFromJsonFile(string path)
        {
            try
            {
                using var stream = File.OpenRead(path);
                var Jsondoc = await JsonDocument.ParseAsync(stream);
                return new BoltSite(Jsondoc);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Create a BoltSite object from a JSON string.
        /// </summary>
        /// <param name="json">JSON string</param>
        /// <returns>BoltSite object</returns>
        static public BoltSite CreateFromJsonString(string json)
        {
            return new BoltSite(JsonDocument.Parse(json));
        }
    }
}
