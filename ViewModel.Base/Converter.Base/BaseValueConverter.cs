using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace ViewModel.Base
{
    /// <summary>
    /// a base value converter that allows direct xaml usage
    /// </summary>
    /// <typeparam name="T">the type of this value converter</typeparam>
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        /// <summary>
        /// a single static instance of this value converter
        /// </summary>
        private static T Converter = null;

        #region markup extension methods


        /// <summary>
        /// provides a static instance of the value converter
        /// </summary>
        /// <param name="serviceProvider">the service provider</param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Converter ?? (Converter = new T());
        }
        #endregion

        #region value converter methods

        /// <summary>
        /// the method to convert one type to another
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        /// <summary>
        /// converter a value back to it's source type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
        #endregion
    }
}
