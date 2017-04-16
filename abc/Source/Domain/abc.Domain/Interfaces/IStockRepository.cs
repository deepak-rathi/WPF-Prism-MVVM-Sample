using System.Collections.Generic;
using abc.Domain.DTO;

namespace abc.Domain.Interfaces
{
    public interface IStockRepository
    {
        StockDTO GetById(string stockId);
        StockDTO CreateStock(StockDTO stockDto);
        StockDTO UpdateStock(StockDTO updatedStockDto);
        StockDTO DeleteById(string stockId);
        List<Stock> GetAllStock();
    }
}
