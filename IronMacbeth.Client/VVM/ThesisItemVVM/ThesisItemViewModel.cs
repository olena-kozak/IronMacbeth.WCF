using IronMacbeth.Client.VVM.BookVVM;


namespace IronMacbeth.Client.VVM.ThesisItemVVM
{
    public class ThesisItemViewModel : IDocumentViewModel
    {
        private Thesis _item;

        public ThesisItemViewModel(Thesis item)
        {
            _item = item;
        }

        public string Name => _item.Name;

        public string Author => _item.Author;

        public string TypeOfDocument => _item.TypeOfDocument;

        public int Year => _item.Year;

        public int Pages => _item.Pages;

        public string City => _item.City;

        public string Responsible => _item.Responsible;


        public DocumentInfoViewModel MoreInfoVm => new DocumentInfoViewModel(_item);

        public object GetItem()
        {
            return _item;
        }
    }
}
