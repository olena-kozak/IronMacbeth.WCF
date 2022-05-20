using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronMacbeth.BFF.Contract
{
    public class DocumentsSearchResults
    {
        public List<Book> Books { get; set; }
        public List<Article> Articles { get; set; }
        public List<Periodical> Periodicals { get; set; }
        public List<Newspaper> Newspapers { get; set; }
        public List<Thesis> Theses { get; set; }
    }
}
