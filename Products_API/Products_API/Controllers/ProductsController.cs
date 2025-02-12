using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products_API.Data;
using Products_API.Models;
using Products_API.Models.Entities;

namespace Products_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public ProductsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(dbContext.Products.ToList());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetProductById(Guid id)
        {
            var product = dbContext.Products.Find(id);
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);

        }

        [HttpPost]
        public IActionResult AddProduct(AddProductDetails addProductDetails)
        {
            var productEntity = new Product()
            {
                Name = addProductDetails.Name,
                Description = addProductDetails.Description,
                Price = addProductDetails.Price,
            };
            dbContext.Products.Add(productEntity);
            dbContext.SaveChanges();

            return Ok(productEntity);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateProduct(Guid id, UpdateProductDetails updateProductDetails)
        {
            var product = dbContext.Products.Find(id);
            if (product is null)
            {
                return NotFound();
            }

            product.Name = updateProductDetails.Name;
            product.Description = updateProductDetails.Description;
            product.Price = updateProductDetails.Price;

            dbContext.SaveChanges();
            return Ok(product);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var product = dbContext.Products.Find(id);
            if (product is null)
            {
                return NotFound();
            }

            dbContext.Products.Remove(product);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
