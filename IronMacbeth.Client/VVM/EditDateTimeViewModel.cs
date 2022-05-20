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

namespace IronMacbeth.Client.VVM
{
    public class EditDateTimeViewModel : IPageViewModel, INotifyPropertyChanged
    {
        public string PageViewName => "DateTime";

        public void Update() { }
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
        public ICommand CloseCommand { get; set; }
        public ICommand ApplyChangesCommand { get; set; }

        public EditDateTimeViewModel()
        {
            DateTime dateTime = DateTime.Now.AddMinutes(179);
            Min = new DateTime(dateTime.Ticks - (dateTime.Ticks % TimeSpan.TicksPerSecond), dateTime.Kind);

            _receiveDate = DateTime.Now.AddHours(3);
            CloseCommand = new RelayCommand(CloseMethod);
            ApplyChangesCommand = new RelayCommand(ApplyChangesMethod) { CanExecuteFunc = CanExecuteMaintenanceMethods };
        }

        public void ApplyChangesMethod(object parameter)
        {
            CloseMethod(parameter);
        }



        public bool CanExecuteMaintenanceMethods(object parameter)
        {
            return ReceiveDate != null;
        }

        public void CloseMethod(object parameter)
        {
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
