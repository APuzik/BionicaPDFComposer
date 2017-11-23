using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFComposer
{
    public class PdfDoc
    {
        public string DocPath { get; set; }
        public int PageCount
        {
            get
            {
                if (pageCount == 0)
                {
                    using (PdfSharp.Pdf.PdfDocument doc = PdfReader.Open(DocPath, PdfDocumentOpenMode.InformationOnly))
                    {
                        pageCount = doc.PageCount;
                    }
                }
                return pageCount;
            }
        }

        internal int pageCount = 0;
    }
}
