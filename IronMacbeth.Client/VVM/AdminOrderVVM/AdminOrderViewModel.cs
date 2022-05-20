using IronMacbeth.Client.Annotations;
using IronMacbeth.Client.ViewModel;
using IronMacbeth.Client.VVM.MyOrdersVVM.MyOrdersItemsVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IronMacbeth.Client.VVM.AdminOrderVVM
{
    public class AdminOrderViewModel : IPageViewModel, INotifyPropertyChanged
    {

        public string PageViewName => "Orders";
        public void Update() { ShowCollection(); }

        public List<OrderBookItemViewModel> Items { get; private set; }

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public OrderBookItemViewModel SelectedItem { get; set; }

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
            Items = new List<OrderBookItemViewModel>();
            List<Order> orders = ServerAdapter.Instance.GetAllOrders();

            Items.AddRange(orders.OrderByDescending(item => item.DateOfOrder).Where(item => item.Book != null && item.Book.Name.ToLower().Contains(Search.ToLower())).Select(x => new OrderBookItemViewModel(x)));
            Items.AddRange(orders.OrderByDescending(item => item.DateOfOrder).Where(item => item.Article != null && item.Article.Name.ToLower().Contains(Search.ToLower())).Select(x => new OrderBookItemViewModel(x)));
            Items.AddRange(orders.OrderByDescending(item => item.DateOfOrder).Where(item => item.Periodical != null && item.Periodical.Name.ToLower().Contains(Search.ToLower())).Select(x => new OrderBookItemViewModel(x)));
            Items.AddRange(orders.OrderByDescending(item => item.DateOfOrder).Where(item => item.Newspaper != null && item.Newspaper.Name.ToLower().Contains(Search.ToLower())).Select(x => new OrderBookItemViewModel(x)));
            Items.AddRange(orders.OrderByDescending(item => item.DateOfOrder).Where(item => item.Thesis != null && item.Thesis.Name.ToLower().Contains(Search.ToLower())).Select(x => new OrderBookItemViewModel(x)));
            OnPropertyChanged(nameof(Items));
        }

        public AdminOrderViewModel()
        {
            EditCommand = new RelayCommand(EditOrderMethod) { CanExecuteFunc = CanExecuteMaintenanceMethods };
            DeleteCommand = new RelayCommand(CancelMethod) { CanExecuteFunc = CanExecuteMaintenanceMethods };
        }
        public void CancelMethod(object parameter)
        {
            Order orderToDelete = (Order)SelectedItem.GetItem();
            ServerAdapter.Instance.DeleteOrder(orderToDelete.Id);
            Update();
        }

        public void EditOrderMethod(object parameter)
        {
            Order order = (Order)SelectedItem.GetItem();
            SpecifiedOrderFields specifyOrderFields = new SpecifiedOrderFields();
            var editOrderViewModel = new EditOrderViewModel(order);
            new EditOrderWindow { DataContext = editOrderViewModel }.ShowDialog();

            DateTime receiveDateTime = editOrderViewModel.SpecifyOrderFields.ReceiveDate;
            string Status = editOrderViewModel.SpecifyOrderFields.Status;
            DateTime dateOfReturning = editOrderViewModel.SpecifyOrderFields.DateOfReturning;
            specifyOrderFields.ReceiveDate = receiveDateTime.ToUniversalTime();
            specifyOrderFields.Status = Status;
            specifyOrderFields.DateOfReturning = dateOfReturning.ToUniversalTime();
            ServerAdapter.Instance.UpdateOrder(order, specifyOrderFields);
            Update();
        }


        public void ShowCollection()
        {
            Items = new List<OrderBookItemViewModel>();
            List<Order> orders = ServerAdapter.Instance.GetAllOrders();
            Items.AddRange(orders.OrderBy(item => item.ReceiveDate).Select(x => new OrderBookItemViewModel(x)));
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
