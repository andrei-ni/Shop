namespace DrPrint.Server.Services.CartService;
using System.Security.Claims;

public class CartService : ICartService
{
    public readonly DataContext _context;
    private readonly IAuthService _authService;
    public CartService(DataContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItem> cartItems)
    {
        var result = new ServiceResponse<List<CartProductResponse>>
        {
            Data = new List<CartProductResponse>()
        };
        foreach (var item in cartItems)
        {
            var product = await _context.Products.Where(p => p.Id == item.ProductId).Include(p => p.Images)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                continue;
            }

            var cartProduct = new CartProductResponse
            {
                ProductId = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Images = product.Images,
                Price = product.Price,
                Quantity = item.Quantity
            };
            result.Data.Add(cartProduct);
        }
        return result;
    }

    public async Task<ServiceResponse<List<CartProductResponse>>> StoreCartItems(List<CartItem> cartItems)
    {
        cartItems.ForEach(cartItem => cartItem.UserId = _authService.GetUserId());
        _context.CartItems.AddRange(cartItems);
        await _context.SaveChangesAsync();
        return await GetDbCartProducts();
    }

    public async Task<ServiceResponse<int>> GetCartItemsCount()
    {
        var count = (await _context.CartItems.Where(ci => ci.UserId == _authService.GetUserId()).ToListAsync()).Count;
        return new ServiceResponse<int> { Data = count };
    }
    public async Task<ServiceResponse<List<CartProductResponse>>> GetDbCartProducts()
    {
        return await GetCartProducts(await _context.CartItems.Where(ci => ci.UserId == _authService.GetUserId()).ToListAsync());
    }
    public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
    {
        cartItem.UserId = _authService.GetUserId();
        var sameItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId 
        && ci.UserId == cartItem.UserId);
        if (sameItem == null)
        {
            _context.CartItems.Add(cartItem);
        }
        else
        {
            sameItem.Quantity += cartItem.Quantity;
        }
        await _context.SaveChangesAsync();
        return new ServiceResponse<bool> { Data = true };
    }
    public async Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem)
    {
        var dbCartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId
        && ci.UserId == _authService.GetUserId());
        if (dbCartItem == null)
        {
            return new ServiceResponse<bool>
            {
                Data = false,
                Success = false,
                Message = "Produsul nu exista."
            };
        }
        dbCartItem.Quantity = cartItem.Quantity;
        await _context.SaveChangesAsync();
        return new ServiceResponse<bool> { Data = true };

    }
    public async Task<ServiceResponse<bool>> RemoveItemFromCart(int productId)
    {
        var dbCartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.ProductId == productId
        && ci.UserId == _authService.GetUserId());
        if (dbCartItem == null)
        {
            return new ServiceResponse<bool>
            {
                Data = false,
                Success = false,
                Message = "Produsul nu exista."
            };
        }
        _context.CartItems.Remove(dbCartItem);
        await _context.SaveChangesAsync();
        return new ServiceResponse<bool> { Data = true };
    }
}

