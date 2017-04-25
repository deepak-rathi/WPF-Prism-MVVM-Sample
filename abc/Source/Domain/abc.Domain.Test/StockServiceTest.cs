using abc.Domain.DTO;
using abc.Domain.Exceptions;
using abc.Domain.Interfaces;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace abc.Domain.Test
{
    [TestClass]
    public class StockServiceTest
    {
        private IStockRepository _stockRepository;
        private StockService _stockService;

        private const string ValidStockId = "1";
        private const string InvalidStockId = "INVALIDSTOCKID";

        [TestInitialize]
        public void Initialize()
        {
            //Initialize services as "composition" root
            AutoMapper.Mapper.CreateMap<StockDTO, Stock>();
            AutoMapper.Mapper.CreateMap<Stock, StockDTO>();

            _stockRepository = A.Fake<IStockRepository>();

            A.CallTo(() => _stockRepository.GetById(ValidStockId))
                .Returns(new DTO.StockDTO
                {
                    Id = ValidStockId
                });
            A.CallTo(() => _stockRepository.GetById(InvalidStockId)).Throws<StockNotFoundException>();
            A.CallTo(() => _stockRepository.DeleteById(InvalidStockId)).Throws<StockNotFoundException>();
            A.CallTo(() => _stockRepository.GetAllStock()).Returns(new List<Stock>() {
                new Stock(){ Id = "1", CurrentPrice = 64.95m, IssuingCompany="NASDAQ", LastRecordedPrice = 64.98m, StockCode = "MSFT", StockName = "Microsoft Corporation"},
                new Stock(){ Id = "2", CurrentPrice = 963.90m, IssuingCompany="NSE", LastRecordedPrice = 963.90m, StockCode = "INFY", StockName = "Infosys Ltd"}
            });

            _stockService = new StockService(_stockRepository);
        }

        [TestMethod]
        public void GetById_ValidStockId_ReturnsStock()
        {
            //Arrange
            //_stockService  = new StockService(_stockRepository);//using global variable
            //Act
            Stock stock = _stockService.GetById(ValidStockId);
            //Assert
            Assert.IsInstanceOfType(stock, typeof(Stock));
            Assert.AreEqual(ValidStockId, stock.Id);
        }

        [TestMethod, ExpectedException(typeof(StockNotFoundException))]
        public void GetById_InValidStockId_ThrowsException()
        {
            Stock stock = _stockService.GetById(InvalidStockId);
            Assert.IsInstanceOfType(stock, typeof(Stock));
            Assert.AreEqual(InvalidStockId, stock.Id);
        }

        [TestMethod]
        public void CreateStock_ValidStock_CreatesNewStock()
        {
            var stock = _stockService.CreateStock(123.00m, "NSE", "ABC", "ABC Inc");
            Assert.IsInstanceOfType(stock, typeof(Stock));
        }

        [TestMethod, ExpectedException(typeof(InValidStockException))]
        public void CreateStock_InvalidStock_CreatesNewStock()
        {
            var stock = _stockService.CreateStock(00.00m, "NSE", "ABC", "ABC Inc");
            Assert.IsInstanceOfType(stock, typeof(Stock));
        }

        [TestMethod]
        public void UpdateStock_ValidStock_ReturnStock()
        {
            Stock stock = _stockService.GetById(ValidStockId);
            stock.CurrentPrice = stock.CurrentPrice + 1;
            Stock updatedStock = _stockService.UpdateStock(stock);
            Assert.IsInstanceOfType(updatedStock, typeof(Stock));
        }

        [TestMethod]
        public void DeleteStock_ValidStockId_DeletesStock()
        {
            Stock updatedStock = _stockService.DeleteStockById(ValidStockId);
            Assert.IsInstanceOfType(updatedStock, typeof(Stock));
        }

        [TestMethod, ExpectedException(typeof(StockNotFoundException))]
        public void DeleteStock_InvalidStockId_DeletesStock()
        {
            Stock updatedStock = _stockService.DeleteStockById(InvalidStockId);
            Assert.IsInstanceOfType(updatedStock, typeof(Stock));
        }

        [TestMethod]
        public void GetStock_ReturnStockList()
        {
            var updatedStock = _stockService.GetAllStock();
            Assert.IsTrue(updatedStock.Count > 0);
        }
    }
}
