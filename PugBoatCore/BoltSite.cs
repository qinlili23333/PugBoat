using System.Text.Json;

namespace PugBoatCore
{
    /// <summary>
    /// Object representing a Bolt site.
    /// </summary>
    public class BoltSite
    {
        /// <summary>
        /// Timeline JSON document.
        /// </summary>
        private JsonDocument JsonDoc;
        /// <summary>
        /// Issues found on the site.
        /// </summary>
        public BoltIssue[] Issues;
        /// <summary>
        /// Private constructor, use static methods to create instances.
        /// </summary>
        private BoltSite(JsonDocument Json)
        {
            JsonDoc = Json;
            if (!IsValidSite())
            {
                throw new InvalidSiteException();
            }
            Issues = [.. JsonDoc.RootElement.GetProperty("timelines").EnumerateArray().Select(t => new BoltIssue(t))];
        }

        /// <summary>
        /// Verify whether it's a valid site, in case the JSON is not a valid timeline JSON.
        /// </summary>
        /// <returns>whether is valid site</returns>
        private bool IsValidSite()
        {
            return JsonDoc.RootElement.TryGetProperty("timelines", out var timelines) && timelines.GetArrayLength() > 0;
        }

        /// <summary>
        /// How many issues are on the site.
        /// </summary>
        public int IssueCount => Issues.Length;

        /// <summary>
        /// Get the original JSON string.
        /// </summary>
        /// <returns></returns>
        public string ExportJson()
        {
            return JsonDoc.RootElement.GetRawText();
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
