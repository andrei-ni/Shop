using DrPrint.Client.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using MudBlazor;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;


namespace DrPrint.Server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public OrderService(DataContext context, ICartService cartService, IAuthService authService, IEmailService emailService)
        {
            _context = context;
            _cartService = cartService;
            _authService = authService;
            _emailService = emailService;
        }

        public async Task<ServiceResponse<OrderDetailsResponse>> GetOrderDetails(int orderId)
        {
            var response = new ServiceResponse<OrderDetailsResponse>();
            var order = await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == _authService.GetUserId() && o.Id == orderId).OrderByDescending(o => o.OrderDate)
                .FirstOrDefaultAsync();
            if (order == null)
            {
                response.Success = false;
                response.Message = "Comanda nu a fost gasita.";
                return response;
            }
            var orderDetailsResponse = new OrderDetailsResponse
            {
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Products = new List<OrderDetailsProductResponse>()

            };
            order.OrderItems.ForEach(item => orderDetailsResponse.Products.Add(new OrderDetailsProductResponse
            {
                ProductId = item.ProductId,
                ImageUrl = item.Product.ImageUrl,
                Quantity = item.Quantity,
                Name = item.Product.Name,
                TotalPrice = item.TotalPrice,

            }));
            response.Data = orderDetailsResponse;
            return response;
        }

        public async Task<ServiceResponse<OrderDetailsResponse>> AdminGetOrderDetails(int orderId)
        {
            var response = new ServiceResponse<OrderDetailsResponse>();
            var user = await _context.Users.ToListAsync();
            var order = await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .Where(o => o.Id == orderId).OrderByDescending(o => o.OrderDate)
                .FirstOrDefaultAsync();
            if (order == null)
            {
                response.Success = false;
                response.Message = "Comanda nu a fost gasita.";
                return response;
            }
            var orderDetailsResponse = new OrderDetailsResponse
            {
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                UserId= order.UserId,
                Image = order.Image,
                Message = order.Message,
                Products = new List<OrderDetailsProductResponse>()

            };
            order.OrderItems.ForEach(item => orderDetailsResponse.Products.Add(new OrderDetailsProductResponse
            {
                ProductId = item.ProductId,
                ImageUrl = item.Product.ImageUrl,
                Quantity = item.Quantity,
                Name = item.Product.Name,
                TotalPrice = item.TotalPrice,

            }));
            response.Data = orderDetailsResponse;
            return response;
        }

        public async Task<ServiceResponse<List<OrderOverviewResponse>>> GetOrders()
        {
            var response = new ServiceResponse<List<OrderOverviewResponse>>();
            var orders = await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == _authService.GetUserId()).OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            var orderResponse = new List<OrderOverviewResponse>();
            orders.ForEach(o => orderResponse.Add(new OrderOverviewResponse
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice,
                Product = o.OrderItems.Count > 1 ? $"{o.OrderItems.First().Product.Name} si "
                + $" alte {o.OrderItems.Count - 1}" : o.OrderItems.First().Product.Name,
                ProductImgUrl = o.OrderItems.First().Product.ImageUrl,
            }));
            response.Data = orderResponse;
            return response;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return orders;
        }

        public async Task<ServiceResponse<List<OrderOverviewResponse>>> AdminGetOrders()
        {
            var response = new ServiceResponse<List<OrderOverviewResponse>>();
            var orders = await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            var orderResponse = new List<OrderOverviewResponse>();

            orders.ForEach(o => orderResponse.Add(new OrderOverviewResponse
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice,
                Product = o.OrderItems.Count > 1 ? $"{o.OrderItems.First().Product.Name} si "
                + $" alte {o.OrderItems.Count - 1}" : o.OrderItems.First().Product.Name,
                ProductImgUrl = o.OrderItems.First().Product.ImageUrl,
            }));
            response.Data = orderResponse;
            return response;
        }

        public async Task<ServiceResponse<bool>> PlaceOrder(OrderDTO request)
        {
            var products = (await _cartService.GetDbCartProducts()).Data;
            double totalPrice = 0;

            var email = new EmailDTO();

            products.ForEach(p => totalPrice += p.Price * p.Quantity);

            var orderItems = new List<OrderItem>();
            products.ForEach(p => orderItems.Add(new OrderItem
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity,
                TotalPrice = p.Price * p.Quantity,

            }));
            var order = new Order
            {
                UserId = _authService.GetUserId(),
                OrderDate = DateTime.Now,
                TotalPrice = totalPrice,
                OrderItems = orderItems,
                Message = request.Message,
                Image = request.Image

            };

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == order.UserId);
            double finalPrice = order.TotalPrice + 20.00;

            email.To = user.Email;

            email.Cc = "contact@drprint.ro";
            email.From = "noreply@drprint.ro";
            email.Subject = "[DrPrint] Confirmare inregistrare comanda";

            email.Body = "<div><a href=\" https://www.drprint.ro/ \" style=\"text-decoration:none\" target=\"_blank\"><h2 style=\"color:#3A83DF\">DRPRINT</h2></a><hr/>";
            email.Body += "Salut, " + request.Address.Name + "<br/>";
            email.Body += "<p style=\"font-size:18px;\">Iti multumim pentru comanda.<br/>Produsele vor ajunge la tine prin curier.</p> <br/>";
            email.Body += "<a href=\"https://drprint.ro/orders\" target=\"_blank\" style=\"padding:10px 5px 10px 5px; color:#fff; text-decoration: none; background-color:#3A83DF; border: none; border-radius:5px;\">Detalii comanda</a> <br/>";
            email.Body += "<br/> Data: " + order.OrderDate + "<br/> <br/> <b>Produse:</b> <br/>";
            foreach (var p in products)
            {
                email.Body += p.Name + " &emsp;" + p.Quantity + " buc.&emsp; x" + p.Price.ToString("0.00") + " Lei" + "<br/>";
            }
            email.Body += "<br/> <b>Cost livrare:</b> 20.00 Lei <br/> <b>Total:</b> " + finalPrice.ToString("0.00") + " Lei";
            email.Body += "<hr/><br/> <b>Detalii comanda:</b> <br/> <br/> Nume: " + request.Address.Name + "<br/> Telefon: " + request.Address.PhoneNumber + "<br/> Adresa livrare: " + request.Address.Street + ", " + request.Address.City + ", " + request.Address.PostalCode + "<br/>Companie: " + request.Address.CompanyName + ", " + request.Address.CompanyVat + "<br/>";
            if (order.Message != string.Empty)
            {
                email.Body += "<br/>Mesaj: <br/> " + order.Message + " <br/><br/>";
            }
            if (order.Image != string.Empty)
            {
                email.Body += "Imagine: <br/> <a href=\"" + order.Image + "\" target=\"_blank\"><img src=\"" + order.Image + "\" style=\"width: 100px\"></img></a><br/>";
            }
            email.Body += "<br/>ADCO BIROTIC ART SRL <br/> 0723 896 370 <br/> www.drprint.ro </div>";

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(_context.CartItems.Where(ci => ci.UserId == _authService.GetUserId()));
            await _emailService.SendOrderDetails(email);
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }


    }
}