using System;
using ProductManagerConsole.Models;
using ProductManagerConsole.Services;

//-- APP DE CONSOLA - EJECUTE CON: dotnet run

namespace ProductManagerConsole
{
    class Program
    {
        static readonly ProductService _productService = new();

        static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Sistema de Gestión de Productos");
                Console.WriteLine("1. Agregar un Producto Nuevo");
                Console.WriteLine("2. Listar Productos");
                Console.WriteLine("3. Buscar Producto");
                Console.WriteLine("4. Actualizar Producto");
                Console.WriteLine("5. Eliminar Producto");
                Console.WriteLine("6. Nueva Venta Simulada");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");

                switch (Console.ReadLine())
                {
                    case "1": AddProductUI(); break;
                    case "2": ListProductsUI(); break;
                    case "3": FindProductUI(); break;
                    case "4": UpdateProductUI(); break;
                    case "5": DeleteProductUI(); break;
                    case "6": SellSimulationUI(); break;
                    case "0": return;
                    default: ShowError("Opción Inválida!"); break;
                }
                WaitForUser();
            }
        }
        static void AddProductUI()
        {
            Console.WriteLine("\nAGREGAR NUEVO PRODUCTO");
            try
            {
                var p = new Product(
                    id: 0,// ReadInt("ID de Producto - SKU: "),
                    category: ReadString("Categoría: "),
                    name: ReadString("Nombre del Producto: "),
                    brand: ReadString("Marca: "),
                    price: ReadInt("Precio: "),
                    stock: ReadInt("Stock: ")
                );
                _productService.AddProduct(p);
                ShowSuccess("Producto agregado con Éxito!");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        static void ListProductsUI()
        {
             Console.WriteLine("\nPRODUCT CATALOG");
            Console.WriteLine("SKU\tCategoria\tNombre\t\tMarca\t\tPrecio\t\tStock");
            Console.WriteLine("------------------------------------------------------------");

            foreach (Product p in _productService.GetAllProducts())
            {
                Console.WriteLine($"{p.ProductId}\t{p.Category}\t{p.ProductName}\t{p.Brand}\t{p.Price:$}\t{p.Stock}");
            }
        }

        

        static void FindProductUI()
        {
            Console.WriteLine("\nBUSCAR PRODUCTO");
            try
            {
                int id = ReadInt("Ingrese el ID - SKU del Producto: ");
                Product product = _productService.FindProduct(id);

                if (product != null)
                {
                    Console.WriteLine("\nDETALLES DEL PRODUCTO:");
                    Console.WriteLine($"SKU: {product.ProductId}");
                    Console.WriteLine($"Nombre: {product.ProductName}");
                    Console.WriteLine($"Categoría: {product.Category}");
                    Console.WriteLine($"Marca: {product.Brand}");
                    Console.WriteLine($"Precio: {product.Price}");
                    Console.WriteLine($"Stock: {product.Stock}");
                }
                else
                {
                    ShowError("Producto No Encontrado!");
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        static void UpdateProductUI()
        {
            Console.WriteLine("\nACTUALIZAR PRODUCTO");
            try
            {
                int id = ReadInt("Ingrese el SKU del Producto para actualizarlo: ");
                Product existing = _productService.FindProduct(id);

                if (existing == null)
                {
                    ShowError("No se encontró el Producto!");
                    return;
                }

                Console.WriteLine("\nValores Actuales (Déjelos en blanco para mantener el valor)");
                string category = ReadString($"Categoría [{existing.Category}]: ");
                string name = ReadString($"Nombre [{existing.ProductName}]: ");
                string brand = ReadString($"Marca [{existing.Brand}]: ");
                string priceInput = ReadString($"Precio [{existing.Price}]: ");
                string stockInput = ReadString($"Stock [{existing.Stock}]: ");

                Product updated = new Product(
                    id,
                    string.IsNullOrEmpty(category) ? existing.Category : category,
                    string.IsNullOrEmpty(name) ? existing.ProductName : name,
                    string.IsNullOrEmpty(brand) ? existing.Brand : brand,
                    string.IsNullOrEmpty(priceInput) ? existing.Price : int.Parse(priceInput),
                    string.IsNullOrEmpty(stockInput) ? existing.Stock : int.Parse(stockInput)
                );

                if (_productService.UpdateProduct(updated))
                {
                    ShowSuccess("Producto Actualizado Exitosamente!");
                }
                else
                {
                    ShowError("Fallo al intentar actualizar!");
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        static void DeleteProductUI()
        {
            Console.WriteLine("\nELIMINAR UN PRODUCTO");
            try
            {
                int id = ReadInt("Ingrese el ID o SKU del Producto a Eliminar: ");
                Console.Write($"Está seguro de eliminar el producto? (S/N): ");

                if (Console.ReadLine().ToUpper() == "S")
                {
                    if (_productService.DeleteProduct(id))
                    {
                        ShowSuccess("Producto Eliminado!");
                    }
                    else
                    {
                        ShowError("Producto no Encontrado!");
                    }
                }
                else
                {
                    ShowSuccess("Eliminación Cancelada.");
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }


        static void SellSimulationUI()
        {
            Console.WriteLine("\nVENTA SIMULADA");
            try
            {
                int id = ReadInt("Ingrese el SKU del Producto: ");
                Product product = _productService.FindProduct(id);

                if (product == null)
                {
                    ShowError("Producto no Encontrado!");
                    return;
                }

                Console.WriteLine($"\nProducto: {product.ProductName}");
                Console.WriteLine($"Stock Actual: {product.Stock}");
                Console.WriteLine($"Precio por Unidad: {product.Price:$}");

                int quantity = ReadInt("Ingrese la cantidad a vender: ");
                if (quantity <= 0)
                {
                    ShowError("Ingrese un número entero mayor a cero!");
                    return;
                }

                decimal discount = ReadDecimal("Ingrese un descuento % (0-100): ");
                if (discount < 0 || discount > 100)
                {
                    ShowError("El descuento debe estar entre 0-100!");
                    return;
                }

                var (success, message) = _productService.ProcessSale(id, quantity, discount);
                static string ReadString(string prompt)
                {
                    Console.Write(prompt);
                    return Console.ReadLine();
                }

                static int ReadInt(string prompt)
                {
                    Console.Write(prompt);
                    return int.Parse(Console.ReadLine());
                }

                static void ShowSuccess(string message) => Console.WriteLine($"\nPROCESO EXITOSO: {message}");
                static void ShowError(string message) => Console.WriteLine($"\nERROR: {message}");
                static void WaitForUser()
                {
                    Console.WriteLine("\nPresione Cualquier Tecla para continuar...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }
        //integer reader 
        static int ReadInt(string prompt)
        {
            Console.Write(prompt);
            return int.Parse(Console.ReadLine());
        }

        //decimal reader
        static decimal ReadDecimal(string prompt)
        {
            Console.Write(prompt);
            return decimal.Parse(Console.ReadLine());
        }

        //Método oK
        static void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nPROCESO EXITOSO: {message}");
            Console.ResetColor();
        }

        //método error
        static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nERROR: {message}");
            Console.ResetColor();
        }
        //método continuar
        static void WaitForUser()
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        //método leer strings
        static string ReadString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}