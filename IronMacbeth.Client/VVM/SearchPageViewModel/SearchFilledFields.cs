using IronMacbeth.Client.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IronMacbeth.Client
{
    public class SearchFilledFields : INotifyPropertyChanged
    {
        public string SearchName { get; set; }

        public string SearchAuthor { get; set; }

        public string Topic { get; set; }

        public int? SearchYearFrom { get; set; }

        public int? SearchYearTo { get; set; }

        private bool _isBookSelected = false;
        public bool IsBookSelected
        {
            get
            {

                return _isBookSelected;
            }
            set
            {
                _isBookSelected = value;
                OnPropertyChanged(nameof(IsBookSelected));
            }
        }

        private bool _isArticleSelected = false;
        public bool IsArticleSelected
        {
            get
            {
                return _isArticleSelected;
            }
            set
            {
                _isArticleSelected = value;
                OnPropertyChanged(nameof(IsArticleSelected));
            }
        }

        private bool _isPeriodicalSelected = false;
        public bool IsPeriodicalSelected
        {
            get { return _isPeriodicalSelected; }
            set
            {
                _isPeriodicalSelected = value;
                OnPropertyChanged(nameof(IsPeriodicalSelected));
            }
        }


        private bool _isNewspaperSelected = false;
        public bool IsNewspaperSelected
        {
            get { return _isNewspaperSelected; }
            set
            {
                _isNewspaperSelected = value;
                OnPropertyChanged(nameof(IsNewspaperSelected));
            }
        }

        private bool _isThesisSelected = false;
        public bool IsThesisSelected
        {
            get { return _isThesisSelected; }
            set
            {
                _isThesisSelected = value;
                OnPropertyChanged(nameof(IsThesisSelected));
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
