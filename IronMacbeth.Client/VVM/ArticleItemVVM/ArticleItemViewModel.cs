
using IronMacbeth.Client.VVM.BookVVM;
using System;

namespace IronMacbeth.Client.VVM.ArticleItemVVM
{
    class ArticleItemViewModel : IDocumentViewModel
    {
        private Article _item;

        public ArticleItemViewModel(Article item)
        {
            _item = item;
        }

        public string Name => _item.Name;
        public string Author => _item.Author;
        public string MainDocument => _item.MainDocumentId;
        public string TypeOfDocument => _item.TypeOfDocument;

        public DocumentInfoViewModel MoreInfoVm => new DocumentInfoViewModel(_item);

        public object GetItem()
        {
            return _item;
        }
    }
}
