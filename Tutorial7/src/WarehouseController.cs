using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace Tutorial7;

[ApiController]
[Route("api/warehouse")]
public class WarehouseController(IWarehouseService warehouseService) : ControllerBase
{
    [HttpPost]
    public IActionResult AddWarehouseData([FromBody] ProductDto productDto)
    {
        var dtoValidity = CheckProductDtoValidity();
        return dtoValidity;

        IActionResult CheckProductDtoValidity()
        {
            try
            {
                var productExists = warehouseService.ProductExists(productDto.IdProduct);

                if (!productExists)
                {
                    return NotFound($"Product with id {productDto.IdProduct} does not exist.");
                }

                var warehouseExists = warehouseService.WarehouseExists(productDto.IdWarehouse);

                if (!warehouseExists)
                {
                    return NotFound($"Warehouse with id {productDto.IdWarehouse} does not exist.");
                }

                return productDto.Amount <= 0
                    ? BadRequest("Value for amount should be greater than 0.")
                    : UpdateOrder();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        IActionResult UpdateOrder()
        {
            try
            {
                var orderId = warehouseService.CheckOrderValidityAndUpdate(productDto.IdProduct, productDto.Amount,
                    productDto.CreatedAt);
                if (orderId == null) return NotFound("Corresponding order does not exist or was already completed.");

                var newId = warehouseService.ProductWarehouseUpdate(productDto.IdWarehouse, productDto.IdProduct,
                    productDto.Amount, (int)orderId);

                return CreatedAtRoute("", new { }, new { Id = newId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }

    [HttpPost("procedure")]
    public IActionResult AddProductToWarehouse([FromBody] ProductDto productDto)
    {
        try
        {
            var newId = warehouseService.AddProductToWarehouse(productDto);
            return CreatedAtRoute("", new { }, new { Id = newId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
