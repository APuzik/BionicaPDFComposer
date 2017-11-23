using BionicaPDFComposer.Commands;
using Microsoft.Win32;
using PDFComposer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
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


        void UpdateNumbers(object sender, EventArgs e)
        {
            UpdateOutputPageNumbers();
        }

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
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = @"PDF files (*.pdf)|*.pdf";
                if (saveFileDialog.ShowDialog() == true)
                {
                    PdfComposer composer = new PdfComposer();
                    composer.ComposePages(saveFileDialog.FileName, PagesModel);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Невозможно извлечь страницы. Ошбика:\r\n{ex.Message}");
            }
        }

        public void AddFile(object parameter)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = @"PDF files (*.pdf)|*.pdf";
                if (openFileDialog.ShowDialog() == true)
                {
                    if (!docPages.ContainsKey(openFileDialog.FileName))
                    {
                        docPages.Add(openFileDialog.FileName, new PdfDoc { DocPath = openFileDialog.FileName });
                    }

                    int index = SelectedIndex;
                    if (index < 0)
                        index = PagesVM.Count;

                    PagesModel.Insert(index, new PagesToCompose { FilePath = openFileDialog.FileName, FirstPage = 1, LastPage = docPages[openFileDialog.FileName].PageCount });
                    PagesVM.Insert(index, new PagesToComposeVM(PagesModel[index]));

                    UpdateOutputPageNumbers();
                    SelectedIndex = -1;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Невозможно добавить файл. Скорее всего файл поврежден. Ошбика:\r\n{ex.Message}");
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

        Dictionary<string, PdfDoc> docPages = new Dictionary<string, PdfDoc>();
    }
}
