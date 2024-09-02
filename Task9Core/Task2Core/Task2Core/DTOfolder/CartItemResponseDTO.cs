namespace Task2Core.DTOfolder
{
    public class CartItemResponseDTO
    {
        public int CartItemId { get; set; }

        public int? CartId { get; set; }

        public int Quantity { get; set; }

        public ProductDTO Product { get; set; }
    }
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }

        public decimal? Price { get; set; }

        public string? Description { get; set; }

        public int? CategoryId { get; set; }
    }
}
