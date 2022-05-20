using System.Collections.Generic;

namespace IronMacbeth.Client
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
