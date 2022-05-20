using IronMacbeth.Client.Annotations;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using Image = IronMacbeth.Client.Model.Image;

namespace IronMacbeth.Client.VVM.EditBookVVM
{
    public class FilledFieldsInfo : INotifyPropertyChanged
    {
        public string Name { get; set; }

        public string Author { get; set; }

        public string Topic { get; set; }

        public string PublishingHouse { get; set; }

        public string MainDocumentId { get; set; }

        public string City { get; set; }

        public int? Year { get; set; }

        public int? Pages { get; set; }

        public string Responsible { get; set; }

        public int? Availiability { get; set; }

        public Guid? ElectronicVersionFileId { get; set; }

        public string Location { get; set; }

        public int? IssueNumber { get; set; }

        public string RentPrice { get; set; }

        public MemoryStream ElectronicVersion { get; set; }

        public string ImagePath { get; set; }
        public Guid? ImageFileId { get; set; }
        public Image Image { get; set; }

        private string _typeOfDocument;
        public string TypeOfDocument
        {
            get { return _typeOfDocument; }
            set
            {
                _typeOfDocument = value;
                OnSelectedItemTypeChanged(value);
            }
        }

        private bool _isBookSelected = false;
        private bool _isArticleSelected = false;
        private bool _isPeriodicalSelected = false;
        private bool _isThesisSelected = false;
        private bool _isNewspaperSelected = false;

        #region Visivility

        public Visibility GeneralVisibility => (_isPeriodicalSelected || _isBookSelected || _isThesisSelected || _isArticleSelected || _isNewspaperSelected).ToVisibility();

        public Visibility AuthorItemVisbility => (_isBookSelected || _isArticleSelected || _isThesisSelected).ToVisibility();

        public Visibility PublishingHouseVisibility => (_isBookSelected || _isPeriodicalSelected).ToVisibility();

        public Visibility LocationVisibility => (_isBookSelected || _isPeriodicalSelected || _isNewspaperSelected).ToVisibility();

        public Visibility RentPriceVisibility => (_isBookSelected || _isPeriodicalSelected || _isNewspaperSelected).ToVisibility();

        public Visibility MainDocumentVisibility => (_isArticleSelected).ToVisibility();

        public Visibility IssueNumberVisibility => (_isPeriodicalSelected || _isNewspaperSelected).ToVisibility();

        public Visibility ResponsibleVisibility => (_isPeriodicalSelected || _isThesisSelected).ToVisibility();

        public Visibility PagesVisibility => (_isPeriodicalSelected || _isBookSelected || _isThesisSelected || _isArticleSelected).ToVisibility();

        public Visibility ImageVisibility => (_isPeriodicalSelected || _isBookSelected).ToVisibility();

        public Visibility ToAllVisibility => (_isPeriodicalSelected || _isBookSelected || _isThesisSelected || _isArticleSelected || _isNewspaperSelected).ToVisibility();

        public Visibility CityVisibility => (_isPeriodicalSelected || _isBookSelected || _isThesisSelected || _isNewspaperSelected).ToVisibility();

        public Visibility AvailibilityVisibility => (_isBookSelected || _isPeriodicalSelected || _isNewspaperSelected).ToVisibility();

        public Visibility ElectronicVersionVisibility => (ElectronicVersion != null).ToVisibility();

        private void OnSelectedItemTypeChanged(string value)
        {
            if (value == "Book")
            {
                _isBookSelected = true;
                _isArticleSelected = false;
                _isPeriodicalSelected = false;
                _isNewspaperSelected = false;
                _isThesisSelected = false;

            }
            else if (value == "Article")
            {
                _isArticleSelected = true;
                _isBookSelected = false;
                _isPeriodicalSelected = false;
                _isNewspaperSelected = false;
                _isThesisSelected = false;
            }
            else if (value == "Periodical")
            {
                _isPeriodicalSelected = true;
                _isArticleSelected = false;
                _isBookSelected = false;
                _isNewspaperSelected = false;
                _isThesisSelected = false;
            }
            else if (value == "Thesis")
            {
                _isThesisSelected = true;
                _isPeriodicalSelected = false;
                _isArticleSelected = false;
                _isBookSelected = false;
                _isNewspaperSelected = false;
            }
            else if (value == "Newspaper")
            {
                _isNewspaperSelected = true;
                _isThesisSelected = false;
                _isPeriodicalSelected = false;
                _isArticleSelected = false;
                _isBookSelected = false;
            }
            else
            {
                throw new NotImplementedException("Selected item is not supported");
            }

            OnPropertyChanged(nameof(GeneralVisibility));
            OnPropertyChanged(nameof(AuthorItemVisbility));
            OnPropertyChanged(nameof(PublishingHouseVisibility));
            OnPropertyChanged(nameof(RentPriceVisibility));
            OnPropertyChanged(nameof(IssueNumberVisibility));
            OnPropertyChanged(nameof(ResponsibleVisibility));
            OnPropertyChanged(nameof(LocationVisibility));
            OnPropertyChanged(nameof(PagesVisibility));
            OnPropertyChanged(nameof(MainDocumentVisibility));
            OnPropertyChanged(nameof(ImageVisibility));
            OnPropertyChanged(nameof(CityVisibility));
            OnPropertyChanged(nameof(AvailibilityVisibility));
        }

        #endregion

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

