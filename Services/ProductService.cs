using System;
using System.Collections;
using ProductManagerConsole.Models;

namespace ProductManagerConsole.Services
{
    public class ProductService
    {
        //arreglo de productos (Catálogo)
        private readonly ArrayList _catalog = new();

        public ProductService()
        {
            //Data de ejemplo (Memoria)
            _catalog.Add(new Product(1, "Electro", "Notebook", "Sony", 399000, 10));
            _catalog.Add(new Product(2, "Vestir", "Polera", "Adidas", 12990, 50));
        }

        //Métodos de servicio - CRUD 
        public void AddProduct(Product product) 
        {
           int id = GetAllProducts().Count + 1;
           product.ProductId = id;
            _catalog.Add(product);
        }

        public ArrayList GetAllProducts() => _catalog;

        public Product FindProduct(int id)
        {
            foreach (Product p in _catalog)
                if (p.ProductId == id) return p;
            return null;
        }

        public bool UpdateProduct(Product updatedProduct)
        {
            for (int i = 0; i < _catalog.Count; i++)
            {
                Product p = (Product)_catalog[i];
                if (p.ProductId == updatedProduct.ProductId)
                {
                    _catalog[i] = updatedProduct;
                    return true;
                }
            }
            return false;
        }

        public bool DeleteProduct(int id)
        {
            for (int i = 0; i < _catalog.Count; i++)
            {
                Product p = (Product)_catalog[i];
                if (p.ProductId == id)
                {
                    _catalog.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public (bool success, string message) ProcessSale(int productId, int quantity, decimal discountPercent)
        {
            Product p = FindProduct(productId);
            if (p == null) return (false, "Producto no Encontrado");
            if (p.Stock < quantity) return (false, "Sin stock suficiente");

            p.Stock -= quantity;
            decimal total = p.Price * quantity * (1 - discountPercent/100);
            return (true, $"Venta Completada. Total: {total:$}");
        }
    }
}