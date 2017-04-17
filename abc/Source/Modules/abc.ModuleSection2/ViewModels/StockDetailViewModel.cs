using abc.Domain;
using abc.Domain.Exceptions;
using abc.Domain.Interfaces;
using abc.infrastructure.Base;
using abc.infrastructure.Constants;
using abc.infrastructure.Events;
using abc.Infrastructure.Events;
using Prism.Commands;
using Prism.Logging;
using Prism.Unity;
using System;
using System.Windows;

namespace abc.ModuleSection2.ViewModels
{
    internal class StockDetailViewModel : ViewModelBase
    {
        private readonly IStockService _stockService;

        #region SelectedStock
        private Stock _selectedStock = new Stock();

        public Stock SelectedStock
        {
            get { return _selectedStock; }
            set
            {
                if (SetProperty(ref _selectedStock, value))
                {
                    UpdateStockCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #endregion

        #region Constructor
        public StockDetailViewModel()
        {
            _stockService = (IStockService)Container.Resolve(typeof(IStockService), ServiceNames.StockService);
            Container.TryResolve<ILoggerFacade>().Log("StockDetailViewModel Created", Category.Info, Priority.None);

            EventAggregator.GetEvent<SelectedStockChanged>().Subscribe(StockChanged);
        }
        #endregion
        
        #region EventHandler
        private void StockChanged(Stock stock)
        {
            if (stock == null) return;
            SelectedStock = new Stock
            {
                CurrentPrice = stock.CurrentPrice,
                Id = stock.Id,
                IssuingCompany = stock.IssuingCompany,
                LastRecordedPrice = stock.LastRecordedPrice,
                StockCode = stock.StockCode,
                StockName = stock.StockName
            };
        }
        #endregion

        #region AddStockCommand
        private DelegateCommand _addStockCommand;

        public DelegateCommand AddStockCommand
        {
            get
            {
                return _addStockCommand ?? (_addStockCommand = new DelegateCommand(() =>
                {
                    try
                    {
                        var stock = _stockService.CreateStock(SelectedStock.CurrentPrice, SelectedStock.IssuingCompany, SelectedStock.StockCode, SelectedStock.StockName);
                        EventAggregator.GetEvent<StockAddedEvent>().Publish(stock);
                        MessageBox.Show("Added successfuly");
                        SelectedStock = new Stock();
                    }
                    catch (InValidStockException exception)
                    {
                        Container.TryResolve<ILoggerFacade>().Log(exception.Message, Category.Exception, Priority.Medium);
                    }
                    catch (Exception exception)
                    {
                        Container.TryResolve<ILoggerFacade>().Log(exception.Message, Category.Exception, Priority.High);
                    }
                }));
            }
        }
        #endregion

        #region UpdateStockCommand
        private DelegateCommand _updateStockCommand;

        public DelegateCommand UpdateStockCommand
        {
            get
            {
                return _updateStockCommand ?? (_updateStockCommand = new DelegateCommand(() =>
                {
                    try
                    {
                        var stock = _stockService.UpdateStock(SelectedStock);
                        EventAggregator.GetEvent<StockUpdatedEvent>().Publish(stock);
                        MessageBox.Show("Updated successfuly");
                        SelectedStock = new Stock();
                    }
                    catch (StockNotFoundException exception)
                    {
                        Container.TryResolve<ILoggerFacade>().Log(exception.Message, Category.Exception, Priority.Medium);
                    }
                    catch (Exception exception)
                    {
                        Container.TryResolve<ILoggerFacade>().Log(exception.Message, Category.Exception, Priority.High);
                    }

                },()=>!string.IsNullOrEmpty(SelectedStock.Id)));
            }
        }
        #endregion
    }
}
