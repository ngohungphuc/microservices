using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlixOne.BookStore.ProductService.Persistence;
using FlixOne.BookStore.ProductService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlixOne.BookStore.ProductService.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var Productvm = _productRepository.GetAll().Select(Product
            => new ProductViewModel
            {
                CategoryId = Product.CategoryId,
                CategoryDescription = Product.Category.Description,
                CategoryName = Product.Category.Name,
                ProductDescription = Product.Description,
                ProductId = Product.Id,
                ProductImage = Product.Image,
                ProductName = Product.Name,
                ProductPrice = Product.Price
            }).ToList();
            return new OkObjectResult(Productvm);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
