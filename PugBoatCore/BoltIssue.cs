using System.Text.Json;

namespace PugBoatCore
{

    /// <summary>
    /// Class repersenting an issue of magazine on a Bolt site.
    /// </summary>
    public class BoltIssue
    {
        /// <summary>
        /// The site containing this issue
        /// </summary>
        BoltSite Site;
        /// <summary>
        /// Json element in timeline JSON
        /// </summary>
        JsonElement TimelineJson;
        /// <summary>
        /// Manifest URL of the issue
        /// </summary>
        public string ManifestUrl;
        /// <summary>
        /// Manifest JSON document
        /// </summary>
        JsonDocument ManifestDoc;
        /// <summary>
        /// Title of issue
        /// </summary>
        public string Title;
        /// <summary>
        /// Url of cover image
        /// </summary>
        public string CoverUrl;
        /// <summary>
        /// Publish time of issue
        /// </summary>
        public DateTimeOffset PublishTime;
        /// <summary>
        /// Number of pages (PDF) or articles (HTML)
        /// </summary>
        public int PageCount;
        /// <summary>
        /// Format of issue, HTML is always available, PDF may be available
        /// </summary>
        public bool PDF = false;
        


        /// <summary>
        /// Whether issue manifest has been fetched
        /// </summary>
        public bool Fetched = false;

        /// <summary>
        /// Initialize with timeline JSON element
        /// </summary>
        /// <param name="timelineJson">Json element in timeline JSON</param>
        public BoltIssue(BoltSite site, JsonElement timelineJson)
        {
            TimelineJson = timelineJson;
            Site = site;
            ManifestUrl = Util.Network.ComposeRelativeUrl($"https://{Site.Domain}/timelines.json", timelineJson.GetProperty("feed").GetString());
            Title = timelineJson.GetProperty("title").GetString();
            CoverUrl = Util.Network.ComposeRelativeUrl($"https://{Site.Domain}/timelines.json",timelineJson.GetProperty("image").GetString());
            PublishTime = DateTimeOffset.Parse(timelineJson.GetProperty("published").GetString());
        }
        /// <summary>
        /// Fetch manifest
        /// </summary>
        /// <returns></returns>
        public async Task Fetch()
        {
            ManifestDoc = await Util.Network.GetJsonFromUrl(ManifestUrl);
            Fetched = true;
            PageCount = ManifestDoc.RootElement.GetProperty("stories").GetArrayLength();
            CheckPDF();
        }

        /// <summary>
        /// If all pages have pdf_url, then PDF is available
        /// </summary>
        private void CheckPDF()
        {
            foreach (var page in ManifestDoc.RootElement.GetProperty("stories").EnumerateArray())
            {
                if (page.GetProperty("pdf_url").GetString()==null)
                {
                    return;
                }
            }
            PDF = true;
        }

        /// <summary>
        /// Get all PDF URLs, throws if manifest not fetched or PDF not available
        /// </summary>
        /// <returns>PDF URLs in array</returns>
        /// <exception cref="InvalidOperationException">manifest not fetched or PDF not available</exception>
        public string[] GetPDF()
        {
            if (!Fetched)
            {
                throw new InvalidOperationException("Manifest not fetched");
            }
            if (!PDF)
            {
                throw new InvalidOperationException("PDF not available");
            }
            return [.. ManifestDoc.RootElement.GetProperty("stories").EnumerateArray().Select(p => Util.Network.ComposeRelativeUrl(ManifestUrl,p.GetProperty("pdf_url").GetString()))];
        }
    }
}
