using IronMacbeth.Client.Annotations;
using IronMacbeth.Client.ViewModel;
using IronMacbeth.Client.VVM.ArticleItemVVM;
using IronMacbeth.Client.VVM.EditBookVVM;
using IronMacbeth.Client.VVM.NewspaperItemVVM;
using IronMacbeth.Client.VVM.PeriodicalItemVVM;
using IronMacbeth.Client.VVM.ThesisItemVVM;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace IronMacbeth.Client.VVM.BookVVM
{
    class DocumentViewModel : IPageViewModel, INotifyPropertyChanged
    {
        public string PageViewName => "Books";

        private List<IDocumentViewModel> _items;

        public List<IDocumentViewModel> Items
        {
            get
            {
                UpdateCollection(true);
                return _items;
            }
            private set { _items = value; }
        }

        private string _search = "";     //TODO:??????
        public string Search
        {
            get { return _search; }

            set
            {
                _search = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    UpdateCollection(false);
                }
                else
                {
                    UpdateCollectionNoFilter();
                }
            }
        }



        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public DocumentViewModel()
        {
            _dispatch = new Dispatch(new IHandler[] { new BookHandler(), new ArticleHandler(), new PeriodicalHandler(), new ThesisHandler(), new NewspaperHandler() });
            AddCommand = new RelayCommand(AddMethod);
            EditCommand = new RelayCommand(EditMethod) { CanExecuteFunc = CanExecuteMaintenanceMethods };
            DeleteCommand = new RelayCommand(DeleteMethod) { CanExecuteFunc = CanExecuteMaintenanceMethods };
        }

        public void AddMethod(object parameter)
        {
            var editDocumentViewModel = new EditDocumentViewModel(null);
            new EditDocumentWindow { DataContext = editDocumentViewModel }.ShowDialog();
            if (editDocumentViewModel.CollectionChanged)
            {
                Update();
                UpdateCollection(false);
            }
        }

        public void Update()
        {

        }

        public IDocumentViewModel SelectedItem { get; set; }

        public void EditMethod(object parameter)
        {
            var editDocumentModel = new EditDocumentViewModel(SelectedItem.GetItem());
            new EditDocumentWindow { DataContext = editDocumentModel }.ShowDialog();
            if (editDocumentModel.CollectionChanged)
            {
                Update();
                UpdateCollection(false);
            }

        }
        private Dispatch _dispatch;

        public void DeleteMethod(object parameter)
        {
            _dispatch.DeleteDispatch(SelectedItem.GetItem());
            Update();
            UpdateCollection(false);
        }


        public void UpdateCollection(bool innerCall)
        {
            _items = new List<IDocumentViewModel>();

            _items.AddRange
            (
                ServerAdapter.Instance.GetAllBooks()
                    .OrderByDescending(item => item.Availiability)
                    .Where(item => item.Name.ToLower().Contains(Search.ToLower()))
                    .Select(x => new BookItemViewModel(x))
                    .ToList()
            );

            _items.AddRange
               (
                ServerAdapter.Instance.GetAllArticles()
                    .Where(item => item.Name.ToLower().Contains(Search.ToLower()))
                   .Select(x => new ArticleItemViewModel(x))
                    .ToList()
                  );

            _items.AddRange
                (
                 ServerAdapter.Instance.GetAllPeriodicals()
                    .OrderByDescending(item => item.Availiability)
                    .Where(item => item.Name.ToLower().Contains(Search.ToLower()))
                  .Select(x => new PeriodicalItemViewModel(x))
                   .ToList()
                 );
            _items.AddRange
                (
                 ServerAdapter.Instance.GetAllNewspapers()
                    .OrderByDescending(item => item.Availiability)
                    .Where(item => item.Name.ToLower().Contains(Search.ToLower()))
                    .Select(x => new NewspaperItemViewModel(x))
                   .ToList()
                 );
            _items.AddRange
                (
                 ServerAdapter.Instance.GetAllThesises().Where(item => item.Name.ToLower().Contains(Search.ToLower()))
                    .Select(x => new ThesisItemViewModel(x))
                   .ToList()
                 );

            if (!innerCall)
            {
                OnPropertyChanged(nameof(Items));
            }
        }

        public void UpdateCollectionNoFilter()    //TODO:///////////////////////////////
        {
            _items.AddRange
                (
                ServerAdapter.Instance.GetAllBooks()
                    .Select(x => new BookItemViewModel(x))
                    .ToList<IDocumentViewModel>()
                 );


            OnPropertyChanged(nameof(Items));
        }

        public bool CanExecuteMaintenanceMethods(object parameter)
        {
            return SelectedItem != null;
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

