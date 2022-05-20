using IronMacbeth.Client.Annotations;
using IronMacbeth.Client.ViewModel;
using IronMacbeth.Client.VVM.EditBookVVM;
using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Image = IronMacbeth.Client.Model.Image;

namespace IronMacbeth.Client.VVM.BookVVM
{
    public class EditDocumentViewModel : IPageViewModel, INotifyPropertyChanged
    {
        public string PageViewName => "Book";

        public bool CollectionChanged { get; private set; }

        public string ImagePath
        {
            get => _imagePath ?? (FilledFieldsInfo.Image != null ? "<image>" : "");
            set => _imagePath = value;
        }

        public string PdfPath
        {
            get => _imagePath ?? (FilledFieldsInfo.ElectronicVersion != null ? "<file>" : "");
            set => _imagePath = value;
        }

        public FilledFieldsInfo FilledFieldsInfo { get; set; }

        public Visibility DocumentTypePickerVisibility { get; } = Visibility.Visible;

        public ICommand CloseCommand { get; set; }

        public ICommand SelectImageCommand { get; set; }

        public ICommand SelectPdfCommand { get; set; }

        public ICommand ApplyChangesCommand { get; set; }

        private Dispatch _dispatch;

        private object _objectForEdit;
        private string _imagePath;

        public string[] AvailibleItemTypes => new[] { "Book", "Article", "Periodical", "Thesis", "Newspaper" };
        public string[] AvailibleTopics => new[] { "Computer Science", "Engineering", "Physics", "Astronomy", "Mathematics", "Chemistry", "Neuroscience", "Medicine", "Economics","Literature", "Philologie"};

        public BitmapImage Image => FilledFieldsInfo.Image?.BitmapImage;

        public EditDocumentViewModel(object objectForEdit)
        {
            _dispatch = new Dispatch(new IHandler[] { new BookHandler(), new ArticleHandler(), new PeriodicalHandler(), new ThesisHandler(), new NewspaperHandler() });

            _objectForEdit = objectForEdit;

            if (_objectForEdit != null)
            {
                DocumentTypePickerVisibility = Visibility.Collapsed;
                FilledFieldsInfo = _dispatch.UnwrapObjectForEdit(objectForEdit);
            }
            else
            {
                FilledFieldsInfo = new FilledFieldsInfo();
            }

            CloseCommand = new RelayCommand(CloseMethod);
            SelectImageCommand = new RelayCommand(SelectImageMethod);
            SelectPdfCommand = new RelayCommand(SelectPdfMethod);
            ApplyChangesCommand = new RelayCommand(ApplyChangesMethod) { CanExecuteFunc = ApplyChangesCanExecute };
        }

        public void SelectPdfMethod(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Pdf Files (.pdf)|*.pdf",
                FilterIndex = 1,
                Multiselect = false
            };

            bool? userClickedOk = openFileDialog.ShowDialog();

            if (userClickedOk == true)
            {
                PdfPath = openFileDialog.FileName;

                // mark that file was updated
                FilledFieldsInfo.ElectronicVersionFileId = null;

                var documentElectronicVersion = new MemoryStream();

                using (var fileStream = File.OpenRead(PdfPath))
                {
                    fileStream.CopyTo(documentElectronicVersion);
                }

                documentElectronicVersion.Seek(0, SeekOrigin.Begin);

                FilledFieldsInfo.ElectronicVersion = documentElectronicVersion;

                OnPropertyChanged(nameof(PdfPath));
            }
        }

        public void ApplyChangesMethod(object parameter)
        {
            if (_objectForEdit == null)
            {
                _dispatch.DispatchCreation(FilledFieldsInfo);
            }
            else
            {
                _dispatch.DispatchUpdate(FilledFieldsInfo, _objectForEdit);
            }

            CollectionChanged = true;

            CloseMethod(parameter);

        }

        public void CloseMethod(object parameter)
        {
            (parameter as Window)?.Close();
        }

        public void SelectImageMethod(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (.png)|*.png",
                FilterIndex = 1,
                Multiselect = false
            };

            bool? userClickedOk = openFileDialog.ShowDialog();

            if (userClickedOk == true)
            {
                ImagePath = openFileDialog.FileName;

                // mark that image was updated
                FilledFieldsInfo.ImageFileId = null;

                var imageData = new MemoryStream();

                using (var fileStream = File.OpenRead(ImagePath))
                {
                    fileStream.CopyTo(imageData);
                }

                imageData.Seek(0, SeekOrigin.Begin);

                FilledFieldsInfo.Image = new Image(imageData);

                OnPropertyChanged(nameof(ImagePath));
                OnPropertyChanged(nameof(Image));
            }
        }

        public bool ApplyChangesCanExecute(object parameter)
        {
            return _dispatch.CanExecuteApplyChanges(FilledFieldsInfo);
        }

        public void Update() { }

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
