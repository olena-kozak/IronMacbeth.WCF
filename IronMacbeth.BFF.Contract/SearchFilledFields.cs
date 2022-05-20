using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronMacbeth.BFF.Contract
{
    public class SearchFilledFields
    {
        public string SearchName { get; set; }

        public string SearchAuthor { get; set; }

        public string Topic { get; set; }

        public int? SearchYearFrom { get; set; }

        public int? SearchYearTo { get; set; }

        public bool IsBookSelected { get; set; }

        public bool IsArticleSelected { get; set; }

        public bool IsPeriodicalSelected { get; set; }

        public bool IsNewspaperSelected { get; set; }

        public bool IsThesisSelected { get; set; }

 
    }
}
