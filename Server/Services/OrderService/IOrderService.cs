namespace DrPrint.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<bool>> PlaceOrder(OrderDTO request);
        Task<ServiceResponse<List<OrderOverviewResponse>>> GetOrders();
        Task<List<Order>> GetAllOrders();
        Task<ServiceResponse<List<OrderOverviewResponse>>> AdminGetOrders();
        Task<ServiceResponse<OrderDetailsResponse>> GetOrderDetails(int orderId);
        Task<ServiceResponse<OrderDetailsResponse>> AdminGetOrderDetails(int orderId);

        
    }
}
