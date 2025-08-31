using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PugBoatCore
{
    /// <summary>
    /// Object representing a Bolt site.
    /// </summary>
    public class BoltSite
    {
        /// <summary>
        /// Private constructor, use static methods to create instances.
        /// </summary>
        private BoltSite(string Json)
        {
        }


        static public Task<BoltSite> CreateFromDomain(string domain)
        {
            return null;
        }

        static public Task<BoltSite> CreateFromJsonFile(string path)
        {
            return null;
        }

        /// <summary>
        /// Create a BoltSite object from a JSON string.
        /// </summary>
        /// <param name="json">JSON string</param>
        /// <returns>BoltSite object</returns>
        static public BoltSite CreateFromJsonString(string json)
        {
            return new BoltSite(json);
        }
    }
}
