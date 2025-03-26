# Gestor de Productos - Consola

![C#](https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=white)

Aplicación de consola para gestión de productos desarrollada en C# con .NET.

## Características Principales

✅ **CRUD Completo**  
- Crear, leer, actualizar y eliminar productos  
- Persistencia en memoria mediante `ArrayList`  

📋 **Estructura de Productos**  
- ID único  
- Categoría  
- Nombre  
- Marca  
- Precio  
- Stock  

🔄 **Operaciones Avanzadas**  
- Simulación de ventas con control de inventario  
- Búsqueda por identificador  

## Estructura del Proyecto

ProductAppConsole/
├── Program.cs         # Lógica principal y menú
└── Product.cs         # Modelo de datos

##  Requisitos

.NET 8 SDK

Terminal/Consola

## Cómo Ejecutar

Clonar repositorio

Navegar al directorio del proyecto

Ejecutar: dotnet run

## Menú Principal

1. Añadir Producto
2. Listar Productos
3. Buscar Producto
4. Actualizar Producto
5. Eliminar Producto
6. Simular Venta
0. Salir

## Ejemplo de Uso

> Añadir Producto:
ID: Por defecto -> Tamaño del array + 1
Categoría: Electrónica
Nombre: Teclado Mecánico
Marca: Logitech
Precio: 89.99
Stock: 15

> Listar Productos:
101 | Teclado Mecánico | Logitech | $89.99 | Stock: 15

## Notas Técnicas

🔹 Implementado con ArrayList para almacenamiento
🔹 Validación básica de entradas
🔹 Formato de precio automático (ej: $89.99)

## Mejoras Futuras

◻ Persistencia en archivo JSON
◻ Validación avanzada de datos
◻ Soporte para múltiples categorías

⌨️ Desarrollado como ejercicio para la asignatura de C# Tec Mayor.