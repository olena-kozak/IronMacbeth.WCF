using IronMacbeth.Client.Annotations;
using IronMacbeth.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace IronMacbeth.Client.VVM.AdminOrderVVM
{
    public class EditOrderViewModel : INotifyPropertyChanged
    {
        public ICommand CancelCommand { get; set; }
        public ICommand ApplyChangesCommand { get; set; }

        public string[] AvailibleItemTypes => new[] { "Order is accepted", "Order is in proccessing", "Order completed", "Document is currently taking by the user", "Document returned" };

        public string Status { get; set; }
        public DateTime Min { get; }

        private DateTime _receiveDate;

        public DateTime ReceiveDate
        {
            get { return _receiveDate; }
            set
            {
                _receiveDate = value;
                OnPropertyChanged(nameof(ReceiveDate));
            }
        }


        public DateTime MinDateReturn { get; }


        private DateTime _dateTimeOfReturning;

        public DateTime DateOfReturning
        {
            get { return _dateTimeOfReturning; }
            set
            {
                _dateTimeOfReturning = value;
                OnPropertyChanged(nameof(DateOfReturning));
            }
        }

        public Order _order;


        public Order CurrentOrder;
        public EditOrderViewModel(Order order)
        {
            SpecifyOrderFields = new SpecifiedOrderFields();
            _order = order;
            Status = order.StatusOfOrder;
            DateTime dateTimeOfReceiving = DateTime.Now;
            Min = new DateTime(dateTimeOfReceiving.Ticks - (dateTimeOfReceiving.Ticks % TimeSpan.TicksPerSecond), dateTimeOfReceiving.Kind);
            _receiveDate = GetReceivedTime(order).ToLocalTime();
            DateTime dateTimeOfReturning = order.DateOfReturn;
            MinDateReturn = new DateTime(dateTimeOfReturning.Ticks - (dateTimeOfReturning.Ticks % TimeSpan.TicksPerSecond), dateTimeOfReturning.Kind);
            _dateTimeOfReturning = MinDateReturn.ToLocalTime();

            CancelCommand = new RelayCommand(CancelMethod);
            ApplyChangesCommand = new RelayCommand(ApplyChangesMethod) { CanExecuteFunc = CanExecuteMaintenanceMethods };
        }
        public SpecifiedOrderFields SpecifyOrderFields;

        public void ApplyChangesMethod(object parameter)
        {
            SpecifyOrderFields.DateOfReturning = DateOfReturning;
            SpecifyOrderFields.ReceiveDate = ReceiveDate;
            SpecifyOrderFields.Status = Status;
            CloseMethod(parameter);
        }

        public DateTime GetReceivedTime(Order order)
        {
            DateTime dateTimeNow = DateTime.Now;
            dateTimeNow = new DateTime(dateTimeNow.Ticks - (dateTimeNow.Ticks % TimeSpan.TicksPerSecond), dateTimeNow.Kind);
            if (dateTimeNow > order.ReceiveDate)
            {
                return dateTimeNow.AddMinutes(3);
            }
            else
            {
                return order.ReceiveDate;
            }
        }

        public bool CanExecuteMaintenanceMethods(object parameter)
        {
            return Status != null;
        }

        public void CloseMethod(object parameter)
        {

            (parameter as Window)?.Close();
        }

        public void CancelMethod(object parameter)
        {
            SpecifyOrderFields.DateOfReturning = _order.DateOfReturn;
            SpecifyOrderFields.ReceiveDate = _order.ReceiveDate;
            SpecifyOrderFields.Status = _order.StatusOfOrder;
            (parameter as Window)?.Close();
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
