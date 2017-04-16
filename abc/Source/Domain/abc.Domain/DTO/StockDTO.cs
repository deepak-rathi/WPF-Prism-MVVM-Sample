namespace abc.Domain.DTO
{
    public class StockDTO
    {
        public string Id { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        public string IssuingCompany { get; set; }

        public decimal LastRecordedPrice { get; set; }

        public decimal CurrentPrice { get; set; }
    }
}
