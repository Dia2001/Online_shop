using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Schemas;

namespace OnlineShop.Application.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            List<ProductSchema> products = GetProducts();
            return View(products);
        }

        public List<ProductSchema> GetProducts()
        {
            var products = new List<ProductSchema> {
                new ProductSchema{
                    Name = "Furry hooded parka",
                    DefaultImage = "img/shop/shop-1.jpg",
                    Price = 15.55f
                },
                new ProductSchema{
                    Name = "Furry hooded parka",
                    DefaultImage = "img/shop/shop-1.jpg",
                    Price = 15.55f
                },
            };
            return products;
        }
    }
}
