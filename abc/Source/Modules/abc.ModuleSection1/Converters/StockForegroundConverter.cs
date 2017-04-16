using abc.Domain;
using abc.ModuleSection1.Utils;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace abc.ModuleSection1.Converters
{
    public class StockForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.GetType() == typeof(TextBlock))
            {
                var dataContext = ((TextBlock)value).DataContext;
                if (dataContext != null && dataContext.GetType() == typeof(Stock))
                    return StockUtil.GetStockForgroundColor((Stock)dataContext);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
