using abc.Common.Interfaces;
using Prism.Mvvm;

namespace abc.Common
{
    class Stock: BindableBase, IStock
    {
        #region Stock Code
        private string _stockCode;

        public string StockCode
        {
            get { return _stockCode; }
            set { SetProperty(ref _stockCode, value); }
        }
        #endregion

        #region Stock Name
        private string _stockName;

        public string StockName
        {
            get { return _stockName; }
            set { SetProperty(ref _stockName, value); }
        }
        #endregion

        #region Issuing Company
        private string _issuingCompany;

        public string IssuingCompany
        {
            get { return _issuingCompany; }
            set { SetProperty(ref _issuingCompany, value); }
        }
        #endregion

        #region Last Recorded Price
        private decimal _lastRecordedPrice;

        public decimal LastRecordedPrice
        {
            get { return _lastRecordedPrice; }
            set { SetProperty(ref _lastRecordedPrice, value); }
        }
        #endregion

        #region Current Price
        private decimal _currentPrice;

        public decimal CurrentPrice
        {
            get { return _currentPrice; }
            set { SetProperty(ref _currentPrice, value); }
        }
        #endregion
    }
}
