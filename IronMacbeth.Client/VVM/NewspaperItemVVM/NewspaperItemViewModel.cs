using IronMacbeth.Client.VVM.BookVVM;


namespace IronMacbeth.Client.VVM.NewspaperItemVVM
{
    public class NewspaperItemViewModel : IDocumentViewModel
    {
        private Newspaper _item;

        public NewspaperItemViewModel(Newspaper item)
        {
            _item = item;
        }

        public string Name => _item.Name;
        public int Availiability => _item.Availiability;
        public int IssueNumber => _item.IssueNumber;
        public int Year => _item.Year;
        public string City => _item.City;
        public string Location => _item.Location;
        public string TypeOfDocument => _item.TypeOfDocument;
        public string RentPrice => _item.RentPrice;

        public DocumentInfoViewModel MoreInfoVm => new DocumentInfoViewModel(_item);

        public object GetItem()
        {
            return _item;
        }
    }
}
