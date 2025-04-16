using Microsoft.AspNetCore.Mvc;
using SalesTechnicalAssignment.Data;
using SalesTechnicalAssignment.DTO;
using SalesTechnicalAssignment.Interface;
using SalesTechnicalAssignment.Service;

namespace SalesTechnicalAssignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _service;

        public SalesController(ISalesService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] string? customer, [FromQuery] string? branch, [FromQuery] string? orderBy, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var result = _service.GetAll(customer, branch, orderBy, page, size);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var sale = _service.GetById(id);
            if (sale is null)
                return NotFound(new { type = "NotFound", error = "Sale not found", detail = $"No sale found with ID {id}" });

            return Ok(sale);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SaleRequest request)
        {
            try
            {
                var sale = _service.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = sale.Id }, sale);
            }
            catch (Exception ex)
            {
                return BadRequest(new { type = "ValidationError", error = ex.Message, detail = ex.ToString() });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] SaleRequest request)
        {
            try
            {
                var updated = _service.Update(id, request);
                if (updated is null)
                    return NotFound(new { type = "NotFound", error = "Sale not found", detail = $"No sale found with ID {id}" });

                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(new { type = "ValidationError", error = ex.Message, detail = ex.ToString() });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Cancel(Guid id)
        {
            var success = _service.Cancel(id);
            if (!success)
                return NotFound(new { type = "NotFound", error = "Sale not found or already cancelled", detail = $"Sale with ID {id} could not be cancelled" });

            return Ok(new { message = "Sale cancelled successfully" });
        }
    }
}
