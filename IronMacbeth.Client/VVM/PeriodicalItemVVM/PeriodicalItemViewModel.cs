using IronMacbeth.Client.VVM.BookVVM;
using System.Windows.Media.Imaging;

namespace IronMacbeth.Client.VVM.PeriodicalItemVVM
{
    public class PeriodicalItemViewModel : IDocumentViewModel
    {
        private Periodical _item;

        public PeriodicalItemViewModel(Periodical item)
        {
            _item = item;
        }

        public string Name => _item.Name;
        public BitmapImage BitmapImage => _item.Image?.BitmapImage;
        public string Responsible => _item.Responsible;
        public int Availiability => _item.Availiability;
        public int IssueNumber => _item.IssueNumber;
        public string TypeOfDocument => _item.TypeOfDocument;
        public string City => _item.City;
        public string RentPrice => _item.RentPrice;
        public string Location => _item.Location;

        public DocumentInfoViewModel MoreInfoVm => new DocumentInfoViewModel(_item);

        public object GetItem()
        {
            return _item;
        }
    }
}
