using System.Collections.Generic;

namespace abc.Domain.Interfaces
{
    public interface IStockService
    {
        Stock GetById(string stockId);
        Stock CreateStock(decimal currentPrice, string issuingCompany, string stockCode, string stockName);
        Stock UpdateStock(Stock stock);
        Stock DeleteStockById(string stockId);
        List<Stock> GetAllStock();
    }
}
