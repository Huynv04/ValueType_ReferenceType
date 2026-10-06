using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RESTful_API_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static readonly List<ProductDto> _mockDb = new()
    {
        new ProductDto { Id = 1, Name = "Bàn phím cơ", Price = 90 }
    };
        // 1. GET: /api/products -> 200 OK
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetAll()
        {
            return Ok(_mockDb);
        }

        // 2. GET: /api/products/{id} -> 200 OK hoặc 404 Not Found
        [HttpGet("{id:int}")]
        public ActionResult<ProductDto> GetById(int id)
        {
            var product = _mockDb.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound(new { message = $"Không tìm thấy sản phẩm có Id = {id}" }); // 404

            return Ok(product); // 200
        }

        // 3. POST: /api/products -> 201 Created kèm header Location
        [HttpPost]
        public ActionResult<ProductDto> Create([FromBody] CreateProductRequest request)
        {
            var newProduct = new ProductDto
            {
                Id = _mockDb.Count + 1,
                Name = request.Name,
                Price = request.Price
            };
            _mockDb.Add(newProduct);

            // Trả về HTTP 201 Created kèm header Location: /api/products/{newProduct.Id}
            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }

        // 4. PUT: /api/products/{id} -> 204 No Content hoặc 404 Not Found
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] UpdateProductRequest request)
        {
            var product = _mockDb.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound(); // 404

            product.Name = request.Name;
            product.Price = request.Price;

            return NoContent(); // 204: Cập nhật thành công, không cần gửi lại body
        }

        // 5. DELETE: /api/products/{id} -> 204 No Content
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var product = _mockDb.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            _mockDb.Remove(product);
            return NoContent(); // 204
        }
    }
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateProductRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateProductRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}

