using BionicaPDFComposer.ViewModels;
using PDFComposer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BionicaPDFComposer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<TextBox> errorBoxes = new List<TextBox>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //TextBox tb = sender as TextBox;
            //if(!Validation.GetHasError(tb))
            //{
            //    composerVM.UpdateOutputPageNumbers();
            //    errorBoxes.Remove(tb);
            //}
            //else
            //{
            //    errorBoxes.Add(tb);
            //}

            //button.IsEnabled = errorBoxes.Count == 0;            
        }
    }
}
