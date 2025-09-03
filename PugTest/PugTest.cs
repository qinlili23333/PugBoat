using System.Threading.Tasks;

namespace PugTest
{
    internal class PugTest
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"PugTest {typeof(PugTest).Assembly.GetName().Version}");
            Console.WriteLine($"Now testing PugBoatCore {typeof(PugBoatCore.Core).Assembly.GetName().Version}...");
            TestPdfSite().GetAwaiter().GetResult();
        }

        static async Task TestPdfSite()
        {
            Console.WriteLine("Testing PDF site...");
            var site = await PugBoatCore.BoltSite.CreateFromDomain("app-houseandgarden-co-uk".Replace('-','.'));
            Console.WriteLine("Found " + site.IssueCount + " issues.");
            //Console.WriteLine("Original JSON:"+site.ExportJson());
            var first = site.Issues.First();
            Console.WriteLine("Latest release: " + first.Title);
            await first.Fetch();
            Console.WriteLine("Issue has " + first.PageCount + " pages.");
            Console.WriteLine("Cover URL: " + first.CoverUrl);
            Console.WriteLine("PDF available: " + first.PDF);
        }
    }
}
