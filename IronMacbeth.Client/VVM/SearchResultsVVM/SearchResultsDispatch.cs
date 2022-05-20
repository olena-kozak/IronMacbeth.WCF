using IronMacbeth.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IronMacbeth.Client.VVM.SearchResultsVVM
{
    public class SearchResultsDispatch
    {
        public Order order;

        public SearchResultsDispatch(object selectedItem, bool IsTypeIssueing)
        {
            order = new Order();
            if (selectedItem is Book)
            {
                Book book = (Book)selectedItem;
                order.Book = book;

                if (IsTypeIssueing && order.Book.Location.ToLower().Contains("issueing")) { CreateOrder(order); }
                else { CreateReadingRoomOrder(order); }
                UpdateAvailibility.UpdateBook(book);
                MessageBox.Show($"Book \"{book.Name}\" added to your orders", "Book added", MessageBoxButton.OK,
                MessageBoxImage.Information);
            }
            else if (selectedItem is Article)
            {
                Article article = (Article)selectedItem;
                order.Article = article;
                CreateOrder(order);
                MessageBox.Show($"Article \"{article.Name}\" added to your orders", "Article added", MessageBoxButton.OK,
                   MessageBoxImage.Information);
            }
            else if (selectedItem is Periodical)
            {
                Periodical periodical = (Periodical)selectedItem;
                order.Periodical = periodical;
                if (IsTypeIssueing && order.Periodical.Location.ToLower().Contains("issueing")) { CreateOrder(order); }
                else { CreateReadingRoomOrder(order); }
                UpdateAvailibility.UpdatePeriodical(periodical);
                MessageBox.Show($"Periodical \"{periodical.Name}\" added to your orders", "Periodical added", MessageBoxButton.OK,
                 MessageBoxImage.Information);
            }
            else if (selectedItem is Newspaper)
            {
                Newspaper newspaper = (Newspaper)selectedItem;
                order.Newspaper = newspaper;
                if (IsTypeIssueing && order.Newspaper.Location.ToLower().Contains("issueing")) { CreateOrder(order); }
                else { CreateReadingRoomOrder(order); }
                UpdateAvailibility.UpdateNewspaper(newspaper);
                MessageBox.Show($"Newspaper \"{newspaper.Name}\" added to your orders", "Newspaper added", MessageBoxButton.OK,
               MessageBoxImage.Information);
            }
            else if (selectedItem is Thesis)
            {
                Thesis theses = (Thesis)selectedItem;
                order.Thesis = theses;
                CreateOrder(order);
                MessageBox.Show($"Theses \"{theses.Name}\" added to your orders", "Theses added", MessageBoxButton.OK,
               MessageBoxImage.Information);
            }

        }

        public void CreateReadingRoomOrder(Order readingRoomOrder)
        {
            var editDateTimeViewModel = new EditDateTimeViewModel();
            new EditDateTimeWindow { DataContext = editDateTimeViewModel }.ShowDialog();
            DateTime receiveDateTime = editDateTimeViewModel.ReceiveDate;
            order.ReceiveDate = receiveDateTime.ToUniversalTime();
            order.UserLogin = UserService.LoggedInUser.Login;
            order.PhoneNumber = UserService.LoggedInUser.PhoneNumber;
            order.TypeOfOrder = "Reading room order";
            order.StatusOfOrder = "Order is accepted";
            order.UserName = UserService.LoggedInUser.Name;
            order.UserSurname = UserService.LoggedInUser.Surname;
            order.UserLogin = UserService.LoggedInUser.Login;
            DateTime dateOfOrdering = DateTime.Now.ToUniversalTime();
            order.DateOfOrder = dateOfOrdering;

            ServerAdapter.Instance.CreateOrder(order);
        }


        public void CreateOrder(Order order)
        {
            var editDateTimeViewModel = new EditDateTimeViewModel();
            new EditDateTimeWindow { DataContext = editDateTimeViewModel }.ShowDialog();
            DateTime receiveDateTime = editDateTimeViewModel.ReceiveDate;
            order.ReceiveDate = receiveDateTime.ToUniversalTime();
            order.PhoneNumber = UserService.LoggedInUser.PhoneNumber;
            order.UserName = UserService.LoggedInUser.Name;
            order.UserSurname = UserService.LoggedInUser.Surname;
            order.UserLogin = UserService.LoggedInUser.Login;
            order.TypeOfOrder = "Issueing order";
            order.StatusOfOrder = "Order is accepted";
            DateTime dateOfOrdering = DateTime.Now.ToUniversalTime();
            order.DateOfOrder = dateOfOrdering;
            DateTime dateOfReturning = dateOfOrdering.AddMonths(1);
            order.DateOfReturn = dateOfReturning;
            ServerAdapter.Instance.CreateOrder(order);
        }
    }
}
