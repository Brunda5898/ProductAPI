namespace Products_API.Models
{
    public class UpdateProductDetails
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
