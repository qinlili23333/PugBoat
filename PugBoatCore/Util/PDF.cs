using PdfSharp.Pdf;
using System;

namespace PugBoatCore.Util
{
    public static class PDF
    {
        /// <summary>
        /// Fetch and combine all PDF pages of an issue into a single PDF file.
        /// </summary>
        /// <param name="issue">BoltIssue object</param>
        /// <param name="Progress">Action<int,int> with current file and total files for progress</param>
        /// <param name="Preheat">Whether to preheat CDN connections for faster download, enabled by default, may result network bottleneck in rare condition</param>
        /// <returns>Combined PDF in MemoryStream</returns>
        public async static Task<MemoryStream> FetchAndCombine(BoltIssue issue, Action<int,int>? Progress = null, bool Preheat = true)
        {
            if (!issue.Fetched)
                throw new InvalidOperationException("Issue manifest not fetched. Call issue.Fetch() first.");
            if (!issue.PDF)
                throw new InvalidOperationException("Issue does not have PDF available.");
            PdfDocument combined = new();
            string[] url = issue.GetPDF();
            if (Preheat)
            {
                // No need to wait, preheat can do in background
                Network.Preheat(url);
            }
            for (int i = 0; i < url.Length; i++)
            {
                Progress?.Invoke(i + 1, url.Length);
                using var pageStream = await Network.GetHttpStream(url[i]);
                var pageDoc = PdfSharp.Pdf.IO.PdfReader.Open(pageStream, PdfSharp.Pdf.IO.PdfDocumentOpenMode.Import);
                foreach (PdfPage page in pageDoc.Pages)
                {
                    combined.AddPage(page);
                }
                pageDoc.Close();
            }
            MemoryStream outStream = new();
            await combined.SaveAsync(outStream);
            outStream.Position = 0;
            return outStream;
        }
    }
}
