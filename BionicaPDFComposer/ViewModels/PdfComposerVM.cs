using BionicaPDFComposer.Commands;
using Microsoft.Win32;
using PDFComposer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BionicaPDFComposer.ViewModels
{
    public class PdfComposerVM
    {
        public PdfComposerVM()
        {
            ComposeCommand = new RelayCommand { ExecuteAction = ComposePages };
            AddFileCommand = new RelayCommand { ExecuteAction = AddFile };
            DeleteFileCommand = new RelayCommand { ExecuteAction = DeleteFile };
        }

        public ICommand ComposeCommand { get; set; }
        public ICommand AddFileCommand { get; set; }
        public ICommand DeleteFileCommand { get; set; }

        public ObservableCollection<PagesToComposeVM> PagesVM { get; set; } = new ObservableCollection<PagesToComposeVM>();
        public PagesToComposeVM SelectedItem { get; set; } = null;
        public int SelectedIndex { get; set; } = -1;

        public List<PagesToCompose> PagesModel { get; private set; } = new List<PagesToCompose>();

        public void UpdateOutputPageNumbers()
        {
            int currentPage = 1;
            for (int i = 0; i < PagesVM.Count; i++)
            {
                int pageCount = PagesVM[i].LastPage - PagesVM[i].FirstPage + 1;
                PagesVM[i].PageRange = $"{currentPage}-{currentPage + pageCount - 1}";
                currentPage += pageCount;
            }
        }

        public void ComposePages(object parameter)
        {
            PdfComposer composer = new PdfComposer();
            composer.ComposePages(@"D:\Projects\BionicaPDFComposer\Test\result.pdf", PagesModel);
        }

        public void AddFile(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = @"PDF files (*.pdf)|*.pdf";
            if (openFileDialog.ShowDialog() == true)
            {
                int index = SelectedIndex;
                if (index < 0)
                    index = PagesVM.Count;
                PagesVM.Insert(index, new PagesToComposeVM(new PagesToCompose { FilePath = openFileDialog.FileName, FirstPage = 1, LastPage = 1 }));
                UpdateOutputPageNumbers();
                SelectedIndex = -1;
            }

        }

        public void DeleteFile(object parameter)
        {
            if (SelectedIndex >= 0)
            {
                PagesVM.RemoveAt(SelectedIndex);
                UpdateOutputPageNumbers();
            }
        }
    }
}
