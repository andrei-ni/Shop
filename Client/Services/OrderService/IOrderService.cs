namespace DrPrint.Client.Services.OrderService
{
    public interface IOrderService
    {
       List<Order> OrderList { get; set; }
       Task PlaceOrder(OrderDTO request);
       Task<List<OrderOverviewResponse>> GetOrders();
       Task GetAllOrders();
       Task<List<OrderOverviewResponse>> AdminGetOrders();
       Task<OrderDetailsResponse> GetOrderDetails(int orderId);
       Task<OrderDetailsResponse> AdminGetOrderDetails(int orderId);
    }
}
