
using IronMacbeth.BFF.Contract;
using IronMacbeth.Client.Annotations;
using IronMacbeth.Client.ViewModel;
using IronMacbeth.Client.VVM.ArticleItemVVM;
using IronMacbeth.Client.VVM.BookVVM;
using IronMacbeth.Client.VVM.MyOrdersVVM.MyOrdersItemsVVM;
using IronMacbeth.Client.VVM.NewspaperItemVVM;
using IronMacbeth.Client.VVM.PeriodicalItemVVM;
using IronMacbeth.Client.VVM.ThesisItemVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace IronMacbeth.Client.VVM.SearchResultsVVM
{
    public class SearchResultsViewModel : IPageViewModel, INotifyPropertyChanged
    {
        public string PageViewName => "SearchResults";
        public void Update() { ShowCollection(); }

        private string _search = "";
        public string Search
        {
            get { return _search; }

            set
            {
                _search = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    UpdateCollection();
                }
                else
                {
                    ShowCollection();
                }
            }
        }

        public List<IDocumentViewModel> Items { get; private set; }

        public Visibility ButtonsVisibility => UserService.LoggedInUser.IsAdmin ? Visibility.Collapsed : Visibility.Visible;

        public ICommand AddtoMyOrdersCommand { get; }
        public ICommand OrderToReadingRoomCommand { get; }

        public IDocumentViewModel SelectedItem { get; set; }

        public IAvailiable Availibility { get; set; }

        public Order order;

        public SearchResultsDispatch SearchResultsDispatch;

        public void UpdateCollection()
        {
            Items = new List<IDocumentViewModel>();
            DocumentsSearchResults documentsSearchResults = ServerAdapter.Instance.SearchDocuments(_searchFilledFields);

            Items.AddRange(documentsSearchResults.Books.Where(item => item.Name.ToLower().Contains(Search.ToLower())).Select(x => new BookItemViewModel(x)));
            Items.AddRange(documentsSearchResults.Articles.Where(item => item.Name.ToLower().Contains(Search.ToLower())).Select(x => new ArticleItemViewModel(x)));
            Items.AddRange(documentsSearchResults.Periodicals.Where(item => item.Name.ToLower().Contains(Search.ToLower())).Select(x => new PeriodicalItemViewModel(x)));
            Items.AddRange(documentsSearchResults.Newspapers.Where(item => item.Name.ToLower().Contains(Search.ToLower())).Select(x => new NewspaperItemViewModel(x)));
            Items.AddRange(documentsSearchResults.Theses.Where(item => item.Name.ToLower().Contains(Search.ToLower())).Select(x => new ThesisItemViewModel(x)));
            OnPropertyChanged(nameof(Items));

        }
        public void AddToMyOrdersMethod(object parameter)
        {
            IsTypeIssueing = true;
            bool isAlreadyTheSameOrderExist = GetIdDispatch();
            if (!isAlreadyTheSameOrderExist)
            {
                SearchResultsDispatch = new SearchResultsDispatch(SelectedItem.GetItem(), IsTypeIssueing);
                ShowCollection();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show($"Such document already exist in your orders. Do you want to add one more?", "Document already exist",
     MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        SearchResultsDispatch = new SearchResultsDispatch(SelectedItem.GetItem(), IsTypeIssueing);
                        ShowCollection();
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
        }

        public bool IsTypeIssueing = false;

        public bool GetIdDispatch()
        {
            object selectedItem = SelectedItem.GetItem();
            if (selectedItem is Book)
            {
                Book book = (Book)selectedItem;
                return ServerAdapter.Instance.CheckOrder(book.Id, DocumentType.Book);

            }
            if (selectedItem is Article)
            {
                Article article = (Article)selectedItem;
                return ServerAdapter.Instance.CheckOrder(article.Id, DocumentType.Article);

            }
            if (selectedItem is Periodical)
            {
                Periodical periodical = (Periodical)selectedItem;
                return ServerAdapter.Instance.CheckOrder(periodical.Id, DocumentType.Periodical);

            }
            if (selectedItem is Newspaper)
            {
                Newspaper newspaper = (Newspaper)selectedItem;
                return ServerAdapter.Instance.CheckOrder(newspaper.Id, DocumentType.Newspaper);
            }
            if (selectedItem is Thesis)
            {
                Thesis thesis = (Thesis)selectedItem;
                return ServerAdapter.Instance.CheckOrder(thesis.Id, DocumentType.Thesis);
            }
            else
            {
                throw new InvalidCastException("Can not dispatch");
            }

        }

        public void OrderToReadingRoomMethod(object parameter)
        {
            IsTypeIssueing = false;
            bool isAlreadyTheSameOrderExist = GetIdDispatch();
            if (!isAlreadyTheSameOrderExist)
            {
                SearchResultsDispatch = new SearchResultsDispatch(SelectedItem.GetItem(), IsTypeIssueing);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show($"Such document already exist in your orders. Do you want to add one more?", "Document already exist",
     MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:

                        SearchResultsDispatch = new SearchResultsDispatch(SelectedItem.GetItem(), IsTypeIssueing);
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
        }


        private SearchFilledFields _searchFilledFields;
        public SearchResultsViewModel(SearchFilledFields searchFilledFields)
        {
            _searchFilledFields = searchFilledFields;

            AddtoMyOrdersCommand = new RelayCommand(AddToMyOrdersMethod) { CanExecuteFunc = CanExecuteMaintenanceMethods };
            OrderToReadingRoomCommand = new RelayCommand(OrderToReadingRoomMethod) { CanExecuteFunc = CanExecuteMaintenanceMethods };
        }



        public bool CanExecuteMaintenanceMethods(object parameter)
        {
            object selectedItem = SelectedItem?.GetItem();
            var availibility = GetAvailibility(selectedItem);

            bool isSelectedItemIsThesisOrArticle = selectedItem is Article || selectedItem is Thesis || availibility == null;
            bool IsAvailible = !isSelectedItemIsThesisOrArticle && availibility > 0;
            return SelectedItem != null && !isSelectedItemIsThesisOrArticle && IsAvailible;
        }

        public void ShowCollection()
        {
            Items = new List<IDocumentViewModel>();
            DocumentsSearchResults documentsSearchResults = ServerAdapter.Instance.SearchDocuments(_searchFilledFields);

            Items.AddRange(documentsSearchResults.Books.Select(x => new BookItemViewModel(x)));
            Items.AddRange(documentsSearchResults.Articles.Select(x => new ArticleItemViewModel(x)));
            Items.AddRange(documentsSearchResults.Periodicals.Select(x => new PeriodicalItemViewModel(x)));
            Items.AddRange(documentsSearchResults.Newspapers.Select(x => new NewspaperItemViewModel(x)));
            Items.AddRange(documentsSearchResults.Theses.Select(x => new ThesisItemViewModel(x)));
            OnPropertyChanged(nameof(Items));
        }

        public int? GetAvailibility(object selectedItem)
        {
            if (selectedItem is Book)
            {
                Book book = (Book)selectedItem;
                return book.Availiability;
            }
            else if (selectedItem is Periodical)
            {
                Periodical periodical = (Periodical)selectedItem;
                return periodical.Availiability;
            }
            else if (selectedItem is Newspaper)
            {
                Newspaper newspaper = (Newspaper)selectedItem;
                return newspaper.Availiability;
            }
            else
            {
                return null;
            }

        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
