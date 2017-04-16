using abc.Domain;
using System.Windows.Media;

namespace abc.ModuleSection1.Utils
{
    internal static class StockUtil
    {
        /// <summary>
        /// Checks if stock is making any profit as per section 1 rules.
        /// </summary>
        /// <param name="stock"></param>
        internal static SolidColorBrush GetStockForgroundColor(Stock stock)
        {
            if (stock.CurrentPrice > stock.LastRecordedPrice)
                return new SolidColorBrush(Colors.Green);
            else if (stock.CurrentPrice < stock.LastRecordedPrice)
                return new SolidColorBrush(Colors.Red);
            else
                return new SolidColorBrush(Colors.Black);
        }
        
    }
}
