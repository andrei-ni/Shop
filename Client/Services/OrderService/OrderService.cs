using DrPrint.Client.Pages.Admin;
using Microsoft.AspNetCore.Components;

namespace DrPrint.Client.Services.OrderService
{
    public class OrderService : IOrderService
    {
        public List<Order> OrderList { get; set; } = new List<Order>();
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly NavigationManager _navigationManager;

        public OrderService(HttpClient http, AuthenticationStateProvider authStateProvider, NavigationManager nav)
        {
            _http = http;
            _authStateProvider = authStateProvider;
            _navigationManager = nav;
        }

        public async Task<OrderDetailsResponse> GetOrderDetails(int orderId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<OrderDetailsResponse>>($"api/order/{orderId}");
            return result.Data;
        }
        public async Task<OrderDetailsResponse> AdminGetOrderDetails(int orderId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<OrderDetailsResponse>>($"api/order/admin/{orderId}");
            return result.Data;
        }

        public async Task<List<OrderOverviewResponse>> GetOrders()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<OrderOverviewResponse>>>("api/order");
            return result.Data;
        }

        public async Task GetAllOrders()
        {
            var result = await _http.GetFromJsonAsync<List<Order>>("api/order/admin/orders");
            if (result != null)
            {
                OrderList = result;
            }
        }
        public async Task<List<OrderOverviewResponse>> AdminGetOrders()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<OrderOverviewResponse>>>("api/order/admin");
            return result.Data;
        }

        public async Task PlaceOrder(OrderDTO request)
        {
            if (await IsUserAuthenticated())
            {
                await _http.PostAsJsonAsync("api/order", request); //PostAsync("api/order", null);
            }
            else
            {
                _navigationManager.NavigateTo("login");
            }
        }

        private async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }


    }
}
