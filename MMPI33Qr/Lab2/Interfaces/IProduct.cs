using QrLib.Interfaces;

namespace Lab2.Interfaces
{
    /// <summary>
    /// Интерфейс продукта
    /// </summary>
    public interface IProduct
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public IQRCode Code { get; }
        public string Type { get; }
        public string AdditionalInformation { get; }
        public string Description { get; }
    }
}
