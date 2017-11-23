using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace BionicaPDFComposer.ViewModels
{
    public class PageValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value as string;
            int result;
            if (int.TryParse(input, out result))
            {
                if (result < Wrapper.MinValue)
                {
                    return new ValidationResult(false, $"Значение должно быть не меньше {Wrapper.MinValue}.");
                }
                else if (result > Wrapper.MaxValue)
                {
                    return new ValidationResult(false, $"Значение должно быть не больше {Wrapper.MaxValue}.");
                }

                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Значение должно быть номером страницы.");
            }
        }

        public Wrapper Wrapper { get; set; }
    }

    public class Wrapper : DependencyObject
    {
        public static readonly DependencyProperty PageDataProperty =
             DependencyProperty.Register("PageData", typeof(PagesToComposeVM),
             typeof(Wrapper), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty MinValueProperty =
             DependencyProperty.Register("MinValue", typeof(int),
             typeof(Wrapper), new FrameworkPropertyMetadata(1));

        public static readonly DependencyProperty MaxValueProperty =
             DependencyProperty.Register("MaxValue", typeof(int),
             typeof(Wrapper), new FrameworkPropertyMetadata(int.MaxValue));

        public PagesToComposeVM PageData
        {
            get { return (PagesToComposeVM)GetValue(PageDataProperty); }
            set { SetValue(PageDataProperty, value); }
        }
        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }
        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }
    }

    public class BindingProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new PropertyMetadata(null));
    }
}
