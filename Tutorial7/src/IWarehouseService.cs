namespace Tutorial7;

public interface IWarehouseService
{
    bool ProductExists(int id);
    bool WarehouseExists(int id);
    int? CheckOrderValidityAndUpdate(int idProduct, int amount, DateTime createdAt);
    int ProductWarehouseUpdate(int idWarehouse, int idProduct, int amount, int idOrder);
    int AddProductToWarehouse(ProductDto productDto);
}