using abc.Domain;
using abc.Domain.Interfaces;
using abc.infrastructure.Base;
using abc.infrastructure.Constants;
using abc.infrastructure.Events;
using abc.infrastructure.Extensions;
using abc.Infrastructure.Events;
using Prism.Commands;
using Prism.Logging;
using Prism.Unity;
using System.Collections.ObjectModel;
using System.Linq;

namespace abc.ModuleSection1.ViewModels
{
    internal class StockListViewModel : ViewModelBase
    {
        private readonly IStockService _stockService;

        #region Stock List Source
        private ObservableCollection<Stock> _stockListCollection;

        public ObservableCollection<Stock> StockListCollection
        {
            get { return _stockListCollection; }
            set { SetProperty(ref _stockListCollection, value); }
        }
        #endregion

        #region Constructor
        public StockListViewModel()
        {
            _stockService = (IStockService)Container.Resolve(typeof(IStockService), ServiceNames.StockService);
            StockListCollection = _stockService.GetAllStock().ToObservableCollection();
            Container.TryResolve<ILoggerFacade>().Log("StockListViewModel Created", Category.Info, Priority.None);

            EventAggregator.GetEvent<StockAddedEvent>().Subscribe(NewStockAdded);
            EventAggregator.GetEvent<StockUpdatedEvent>().Subscribe(StockUpdated);
        }
        #endregion

        #region EventHandlers
        private void StockUpdated(Stock updatedStock)
        {
            var stockToUpdate = StockListCollection.FirstOrDefault(stock => stock.Id.Equals(updatedStock.Id));
            stockToUpdate.CurrentPrice = updatedStock.CurrentPrice;
            stockToUpdate.IssuingCompany = updatedStock.IssuingCompany;
            stockToUpdate.LastRecordedPrice = updatedStock.LastRecordedPrice;
            stockToUpdate.StockCode = updatedStock.StockCode;
            stockToUpdate.StockName = updatedStock.StockName;
        }

        private void NewStockAdded(Stock newStock)
        {
            StockListCollection.Add(newStock);
        }
        #endregion

        #region DeleteStockCommand
        private DelegateCommand<string> _deleteStockCommand;

        public DelegateCommand<string> DeleteStockCommand
        {
            get
            {
                return _deleteStockCommand ?? (_deleteStockCommand = new DelegateCommand<string>((stockId) =>
                {

                    _stockService.DeleteStockById(stockId);
                    StockListCollection.Remove(StockListCollection.FirstOrDefault(stock => stock.Id.Equals(stockId)));

                }));
            }
        }
        #endregion

        #region SelectedStockChangedCommand
        private DelegateCommand<Stock> _selectedStockChangedCommand;

        public DelegateCommand<Stock> SelectedStockChangedCommand
        {
            get
            {
                return _selectedStockChangedCommand ?? (_selectedStockChangedCommand = new DelegateCommand<Stock>((stock) =>
                {
                    EventAggregator.GetEvent<SelectedStockChanged>().Publish(stock);
                }));
            }
        }

        #endregion
    }
}
