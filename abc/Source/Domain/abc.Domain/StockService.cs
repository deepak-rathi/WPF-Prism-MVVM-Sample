using System;
using abc.Domain.DTO;
using abc.Domain.Exceptions;
using abc.Domain.Interfaces;
using System.Collections.Generic;

namespace abc.Domain
{
    public class StockService: IStockService
    {
        private readonly IStockRepository _stockRepository;
        private const string _stockNotFound = "Stock Not Found";
        private const string _invalidStock = "Invalid Stock";

        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
            
            AutoMapper.Mapper.CreateMap<StockDTO, Stock>();
            AutoMapper.Mapper.CreateMap<Stock, StockDTO>();
        }

        public Stock GetById(string stockId)
        {
            var stockDto = _stockRepository.GetById(stockId);
            if (stockDto == null)
                throw new StockNotFoundException(_stockNotFound);
            var stock = AutoMapper.Mapper.Map<StockDTO, Stock>(stockDto);
            
            return stock;
        }

        public Stock CreateStock(decimal currentPrice, string issuingCompany, string stockCode, string stockName)
        {
            if (currentPrice == 00.00m)
                throw new InValidStockException(_invalidStock);

            var newStockDto = _stockRepository.CreateStock(new StockDTO()
            {
                CurrentPrice = currentPrice,
                IssuingCompany = issuingCompany,
                StockCode = stockCode,
                StockName = stockName
            });
            var stock = AutoMapper.Mapper.Map<StockDTO, Stock>(newStockDto);
            return stock;
        }

        public Stock UpdateStock(Stock stock)
        {
            var updatedStockDto = AutoMapper.Mapper.Map<Stock, StockDTO>(stock);
            var returnedDto = _stockRepository.UpdateStock(updatedStockDto);
            var updatedStock = AutoMapper.Mapper.Map<StockDTO, Stock>(returnedDto);
            return updatedStock;
        }

        public Stock DeleteStockById(string stockId)
        {
            var stockDto = _stockRepository.DeleteById(stockId);
            if (stockDto == null)
                throw new StockNotFoundException(_stockNotFound);
            var stock = AutoMapper.Mapper.Map<StockDTO, Stock>(stockDto);
            
            return stock;
        }

        public List<Stock> GetAllStock()
        {
            return _stockRepository.GetAllStock();
        }
    }
}
 