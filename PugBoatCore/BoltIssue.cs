using System.Text.Json;

namespace PugBoatCore
{

    /// <summary>
    /// Class repersenting an issue of magazine on a Bolt site.
    /// </summary>
    public class BoltIssue
    {
        /// <summary>
        /// Json element in timeline JSON
        /// </summary>
        JsonElement TimelineJson;
        /// <summary>
        /// Manifest URL of the issue
        /// </summary>
        public string ManifestUrl;

        /// <summary>
        /// Whether issue manifest has been fetched
        /// </summary>
        public bool Fetched = false;

        /// <summary>
        /// Initialize with timeline JSON element
        /// </summary>
        /// <param name="timelineJson">Json element in timeline JSON</param>
        public BoltIssue(JsonElement timelineJson)
        {
            TimelineJson = timelineJson;
            ManifestUrl = timelineJson.GetProperty("feed").GetString();
        }

        /// <summary>
        /// Initialize with manifest URL
        /// </summary>
        /// <param name="manifestUrl">Issue manifest url</param>
        public BoltIssue(string manifestUrl)
        {
            ManifestUrl = manifestUrl;
        } 
    }
}
