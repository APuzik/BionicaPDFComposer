using PDFComposer;
using System.ComponentModel;
using System.Windows;

namespace BionicaPDFComposer.ViewModels
{
    public class PagesToComposeVM : INotifyPropertyChanged
    {
        public PagesToComposeVM(PagesToCompose pageToComposeModel)
        {
            PageToComposeModel = pageToComposeModel;
        }

        public string FilePath
        {
            get { return PageToComposeModel.FilePath; }
            set { PageToComposeModel.FilePath = value; OnPropertyChanged("FilePath"); }
        }
        public int FirstPage
        {
            get { return PageToComposeModel.FirstPage; }
            set { PageToComposeModel.FirstPage = value; OnPropertyChanged("FirstPage"); }
        }
        public int LastPage
        {
            get { return PageToComposeModel.LastPage; }
            set { PageToComposeModel.LastPage = value; OnPropertyChanged("LastPage"); }
        }

        public string PageRange
        {
            get { return pageRange; }
            set { pageRange = value; OnPropertyChanged("PageRange"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public PagesToCompose PageToComposeModel { get; private set; }
        private string pageRange;
    }
}
