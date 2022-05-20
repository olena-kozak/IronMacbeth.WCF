using IronMacbeth.Client.Annotations;
using IronMacbeth.Client.ViewModel;
using IronMacbeth.Client.VVM.MyOrdersVVM.MyOrdersItemsVVM;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using System.Windows.Input;

namespace IronMacbeth.Client.VVM.MyOrdersVVM
{
    class MyOrdersViewModel : IPageViewModel, INotifyPropertyChanged
    {

        public string PageViewName => "My orders";

        public List<IDocumentViewModel> Items { get; private set; }

        private string _search = "";
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
                    ShowCollection();
                }
            }
        }

        public void UpdateCollection(bool innerCall)
        {
            Items = new List<IDocumentViewModel>();
            List<Order> orders = ServerAdapter.Instance.GetAllOrders();

            Items.AddRange(orders.OrderByDescending(item => item.DateOfOrder).Where(item => item.Book != null && item.Book.Name.ToLower().Contains(Search.ToLower())).Select(x => new OrderBookItemViewModel(x)));
            Items.AddRange(orders.OrderByDescending(item => item.DateOfOrder).Where(item => item.Article != null && item.Article.Name.ToLower().Contains(Search.ToLower())).Select(x => new OrderBookItemViewModel(x)));
            Items.AddRange(orders.OrderByDescending(item => item.DateOfOrder).Where(item => item.Periodical != null && item.Periodical.Name.ToLower().Contains(Search.ToLower())).Select(x => new OrderBookItemViewModel(x)));
            Items.AddRange(orders.OrderByDescending(item => item.DateOfOrder).Where(item => item.Newspaper != null && item.Newspaper.Name.ToLower().Contains(Search.ToLower())).Select(x => new OrderBookItemViewModel(x)));
            Items.AddRange(orders.OrderByDescending(item => item.DateOfOrder).Where(item => item.Thesis != null && item.Thesis.Name.ToLower().Contains(Search.ToLower())).Select(x => new OrderBookItemViewModel(x)));
            OnPropertyChanged(nameof(Items));
        }

        public void Update() { ShowCollection(); }

        public ICommand CancelCommand { get; }

        public MyOrdersViewModel()
        {
            CancelCommand = new RelayCommand(CancelMethod) { CanExecuteFunc = CanExecuteMaintenanceMethods };
        }

        public IDocumentViewModel SelectedItem { get; set; }

        private object _selectedItem;

        public void CancelMethod(object parameter)
        {
            _selectedItem = SelectedItem.GetItem();

            Order orderToDelete = (Order)_selectedItem;
            ServerAdapter.Instance.DeleteOrder(orderToDelete.Id);
            Update();
        }

        public void ShowCollection()
        {
            Items = new List<IDocumentViewModel>();
            List<Order> orders = ServerAdapter.Instance.GetAllOrders();
            Items.AddRange(orders.OrderByDescending(item => item.DateOfOrder).Select(x => new OrderBookItemViewModel(x)));
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
