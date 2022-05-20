using IronMacbeth.Client.VVM.BookVVM;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace IronMacbeth.Client.VVM.MyOrdersVVM.MyOrdersItemsVVM
{
    public class OrderBookItemViewModel : IDocumentViewModel
    {
        private Order _item;

        public OrderBookItemViewModel(Order item)
        {
            _item = item;
        }

        public string Price => GetPrice(_item.GetOrderedItem());

        public Visibility PriceVisibility => Price != null ? Visibility.Visible : Visibility.Collapsed;
        public string Name => GetName(_item.GetOrderedItem());

        public string Author => GetAuthor(_item.GetOrderedItem());

        public Visibility AuthorVisibility => Author != null ? Visibility.Visible : Visibility.Collapsed;

        public int Id => _item.Id;

        public string UserName => _item.UserName;
        public string UserSurname => _item.UserSurname;
        public int PhoneNumber => _item.PhoneNumber;

        public string StatusOfOrder => _item.StatusOfOrder;

        public string DateOfOrder => _item.DateOfOrder.ToLocalTime().ToString();

        public string DateOfReturn => _item.DateOfReturn.ToLocalTime().ToString();

        public string ReceiveDate => _item.ReceiveDate.ToLocalTime().ToString();

        public string UserLogin => _item.UserLogin;

        public string TypeOfDocument => _item.TypeOfOrder;




        public DocumentInfoViewModel MoreInfoVm => new DocumentInfoViewModel(_item.GetOrderedItem());

        public object GetItem()
        {
            return _item;
        }


        public string FormattedPhoneNumber => $"0{PhoneNumber.ToString()}";

        public string GetAuthor(object order)
        {
            if (order is Book)
            {
                Book book = (Book)order;
                return book.Author;
            }
            if (order is Article)
            {
                Article article = (Article)order;
                return article.Author;
            }
            if (order is Periodical)
            {
                Periodical periodical = (Periodical)order;
                return periodical.Responsible;
            }
            if (order is Thesis)
            {
                Thesis theses = (Thesis)order;
                return theses.Author;
            }
            if (order is Newspaper)
            {
                return null;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public string GetName(object order)
        {
            if (order is Book)
            {
                Book book = (Book)order;
                return book.Name;
            }
            if (order is Article)
            {
                Article article = (Article)order;
                return article.Name;
            }
            if (order is Periodical)
            {
                Periodical periodical = (Periodical)order;
                return periodical.Name;
            }
            if (order is Thesis)
            {
                Thesis theses = (Thesis)order;
                return theses.Name;
            }
            if (order is Newspaper)
            {

                Newspaper newspaper = (Newspaper)order;
                return newspaper.Name;
            }
            else
            {
                throw new InvalidCastException();
            }


        }
        public string GetPrice(object order)
        {
            if (order is Book)
            {
                Book book = (Book)order;
                return book.RentPrice;
            }
            if (order is Article)
            {
                return null;
            }
            if (order is Periodical)
            {
                Periodical periodical = (Periodical)order;
                return periodical.RentPrice;
            }
            if (order is Thesis)
            {
                return null;
            }
            if (order is Newspaper)
            {
                Newspaper newspaper = (Newspaper)order;
                return newspaper.RentPrice;
            }
            else
            {
                throw new InvalidCastException();
            }


        }
    }

}
