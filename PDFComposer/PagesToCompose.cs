using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFComposer
{
    public class PagesToCompose
    {
        public string FilePath { get; set; }
        public int FirstPage { get; set; }
        public int LastPage { get; set; }
    }
}
