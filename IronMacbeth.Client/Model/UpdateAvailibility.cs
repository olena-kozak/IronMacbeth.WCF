using IronMacbeth.Client.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IronMacbeth.Client.Model
{
    public class UpdateAvailibility
    {

        public static void UpdateBook(object selectedItem)
        {
            if (selectedItem is Book)
            {
                Book book = (Book)selectedItem;
                book.Availiability--;

                ServerAdapter.Instance.UpdateBook(book);
            }

        }

        public static void UpdatePeriodical(object selectedItem)
        {
            if (selectedItem is Periodical)
            {
                Periodical periodical = (Periodical)selectedItem;
                periodical.Availiability--;
                ServerAdapter.Instance.UpdatePeriodical(periodical);
            }
        }

        public static void UpdateNewspaper(object selectedItem)
        {
            if (selectedItem is Newspaper)
            {
                Newspaper newspaper = (Newspaper)selectedItem;
                newspaper.Availiability--;
                ServerAdapter.Instance.UpdateNewspaper(newspaper);
            }
        }
    }
}
