using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFComposer
{
    public class PdfComposer
    {
        public void ComposePages(string outputFile, List<PagesToCompose> pagesToCompose)
        {
            Dictionary<string, PdfDocument> docs = new Dictionary<string, PdfDocument>();
            using (PdfDocument outputDoc = new PdfDocument(outputFile))
            {
                foreach (PagesToCompose pageInfo in pagesToCompose)
                {
                    PdfDocument doc = null;
                    if (!docs.TryGetValue(pageInfo.FilePath, out doc))
                    {
                        doc = PdfReader.Open(pageInfo.FilePath, PdfDocumentOpenMode.Import);
                        docs.Add(pageInfo.FilePath, doc);
                    }
                    
                    for (int iZeroBased = pageInfo.FirstPage - 1; iZeroBased < pageInfo.LastPage; iZeroBased++)
                    {
                        outputDoc.AddPage(doc.Pages[iZeroBased]);
                    }
                }

                outputDoc.Close();
            }
        }
    }
}
