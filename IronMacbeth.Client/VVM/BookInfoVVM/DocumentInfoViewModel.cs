using IronMacbeth.Client.ViewModel;
using IronMacbeth.Client.VVM.EditBookVVM;
using Microsoft.Win32;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace IronMacbeth.Client.VVM.BookVVM
{
    public  class DocumentInfoViewModel : IPageViewModel
    {
        private readonly object _objectForEdit;

        private readonly Dispatch _dispatch;

        public string PageViewName => "BookInfo";

        public FilledFieldsInfo FilledFieldsInfo { get; set; }

        public BitmapImage Image => FilledFieldsInfo.Image?.BitmapImage;

        public ICommand SaveElectronicVersionCommand { get; }

        public DocumentInfoViewModel(object item)
        {
            SaveElectronicVersionCommand = new RelayCommand(SaveElectronicVersionMethod) { CanExecuteFunc = (_) => FilledFieldsInfo.ElectronicVersion != null };

            _dispatch = new Dispatch(new IHandler[] { new BookHandler(), new ArticleHandler(), new PeriodicalHandler(), new ThesisHandler(), new NewspaperHandler() });
            _objectForEdit = item;
            if (_objectForEdit != null)
            {
                FilledFieldsInfo = _dispatch.UnwrapObjectForEdit(_objectForEdit);
            }
            else
            {
                FilledFieldsInfo = new FilledFieldsInfo();
            }
        }

        public void Update() { }

        private void SaveElectronicVersionMethod(object parameter)
        {
            var saveFileDialog = new SaveFileDialog { Filter = "Pdf Files (.pdf)|*.pdf" };

            if (saveFileDialog.ShowDialog() == true)
            {
                var fileInfo = new FileInfo(saveFileDialog.FileName);
                using (var stream = fileInfo.Open(FileMode.CreateNew, FileAccess.Write))
                {
                    FilledFieldsInfo.ElectronicVersion.CopyTo(stream);
                    stream.Close();
                }

                FilledFieldsInfo.ElectronicVersion.Seek(0, SeekOrigin.Begin);
            }
        }
    }
}
