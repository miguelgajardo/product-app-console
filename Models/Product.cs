namespace ProductManagerConsole.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }

        public Product(int id, string category, string name, string brand, int price, int stock)
        {
            ProductId = id;
            Category = category;
            ProductName = name;
            Brand = brand;
            Price = price;
            Stock = stock;
        }
    }
}