using IronMacbeth.Client.Annotations;
using IronMacbeth.Client.ViewModel;
using IronMacbeth.Client.VVM.ArticleItemVVM;
using IronMacbeth.Client.VVM.BookVVM;
using IronMacbeth.Client.VVM.Home;
using IronMacbeth.Client.VVM.NewspaperItemVVM;
using IronMacbeth.Client.VVM.PeriodicalItemVVM;
using IronMacbeth.Client.VVM.SearchResultsVVM;
using IronMacbeth.Client.VVM.ThesisItemVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace IronMacbeth.Client.VVM.SearchPageViewModel
{
    public class SearchViewModel : IPageViewModel, INotifyPropertyChanged
    {
        public string PageViewName => "Search";

        public SearchResultsViewModel SearchResultsViewModel
        {
            get
            {
                return new SearchResultsViewModel(SearchFilledFields);
            }
        }

        public SearchFilledFields SearchFilledFields { get; set; }
        public void Update()
        {
            DeleteAllMethod(null);
        }

        public ICommand SearchCommand { get; }
        public ICommand DeleteAllComand { get; }



        public SearchViewModel()
        {

            SearchFilledFields = new SearchFilledFields();
            DeleteAllComand = new RelayCommand(DeleteAllMethod);

        }
        public string SearchName { get => SearchFilledFields.SearchName; set => SearchFilledFields.SearchName = value; }

        public string SearchAuthor { get => SearchFilledFields.SearchAuthor; set => SearchFilledFields.SearchAuthor = value; }

        public string Topic { get => SearchFilledFields.Topic; set => SearchFilledFields.Topic = value; }

        public int? SearchYearFrom { get => SearchFilledFields.SearchYearFrom; set => SearchFilledFields.SearchYearFrom = value; }

        public int? SearchYearTo { get => SearchFilledFields.SearchYearTo; set => SearchFilledFields.SearchYearTo = value; }

        public void DeleteAllMethod(object parameter)
        {
            SearchFilledFields.SearchName = "";
            OnPropertyChanged(nameof(SearchFilledFields.SearchName));
            SearchFilledFields.Topic = "";
            OnPropertyChanged(nameof(SearchFilledFields.Topic));
            SearchFilledFields.SearchAuthor = "";
            OnPropertyChanged(nameof(SearchFilledFields.SearchAuthor));
            SearchFilledFields.SearchYearFrom = null;
            OnPropertyChanged(nameof(SearchFilledFields.SearchYearFrom));
            SearchFilledFields.SearchYearTo = null;
            OnPropertyChanged(nameof(SearchFilledFields.SearchYearTo));
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
