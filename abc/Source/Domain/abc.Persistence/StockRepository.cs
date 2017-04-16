using abc.Domain;
using abc.Domain.DTO;
using abc.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace abc.Persistence
{
    public class StockRepository: IStockRepository
    {
        private readonly List<Stock> _dbStockList;

        public StockRepository()
        {
            _dbStockList = new List<Stock>() {
                new Stock(){ Id = "1", CurrentPrice = 64.95m, IssuingCompany="NASDAQ", LastRecordedPrice = 64.98m, StockCode = "MSFT", StockName = "Microsoft Corporation"},
                new Stock(){ Id = "2", CurrentPrice = 963.90m, IssuingCompany="NSE", LastRecordedPrice = 963.90m, StockCode = "INFY", StockName = "Infosys Ltd"},
                new Stock(){ Id = "3", CurrentPrice = 35.26m, IssuingCompany="NASDAQ", LastRecordedPrice = 35.25m, StockCode = "INTC", StockName = "Intel Corporation"},
                new Stock(){ Id = "4", CurrentPrice = 1281.54m, IssuingCompany="NSE", LastRecordedPrice = 1273.00m, StockCode = "M&M", StockName = "Mahindra & Mahindra Limited"},
                new Stock(){ Id = "5", CurrentPrice = 139.39m, IssuingCompany="NASDAQ", LastRecordedPrice = 139.62m, StockCode = "FB", StockName = "Facebook Inc"}
            };
        }

        public StockDTO CreateStock(StockDTO stockDto)
        {
            //ToDo://Have some logic to check if this StockDTO can be inserted
            //Do some DB operation like insert etc
            //For this application we are just inserting in our local list of stock
            var stock = AutoMapper.Mapper.Map<StockDTO, Stock>(stockDto);
            stock.Id = _dbStockList.LastOrDefault().Id + 1;
            stock.LastRecordedPrice = stock.CurrentPrice;
            _dbStockList.Add(stock);
            var insertedStockDto = AutoMapper.Mapper.Map<Stock, StockDTO>(stock);
            return insertedStockDto;
        }

        private Stock GetStockById(string stockId)
        {
            return _dbStockList.FirstOrDefault(dbStock => dbStock.Id.Equals(stockId));
        }

        public StockDTO DeleteById(string stockId)
        {
            var stock = GetStockById(stockId);
            if (stock == null)
                return null;
            else
            {
                _dbStockList.Remove(stock);
                return AutoMapper.Mapper.Map<Stock, StockDTO>(stock);
            }
        }

        public StockDTO GetById(string stockId)
        {
            var stock = GetStockById(stockId);
            if (stock == null)
                return null;
            else
                return AutoMapper.Mapper.Map<Stock, StockDTO>(stock);
        }

        public StockDTO UpdateStock(StockDTO updatedStockDto)
        {
            var stockToUpdate = _dbStockList.FirstOrDefault(stock => stock.Id.Equals(updatedStockDto.Id));
            if (stockToUpdate == null)
                return null;
            else
            {
                _dbStockList.Remove(stockToUpdate);
                var stock = AutoMapper.Mapper.Map<StockDTO, Stock>(updatedStockDto);
                _dbStockList.Add(stock);
                return updatedStockDto;
            }
        }

        public List<Stock> GetAllStock()
        {
            return _dbStockList;
        }
    }
}
