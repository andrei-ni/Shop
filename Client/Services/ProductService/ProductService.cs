using DrPrint.Shared;

namespace DrPrint.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Product> AdminProducts { get; set; } = new List<Product>();
        public List<Product> NewProducts { get; set; } = new List<Product>();
        public string Message { get; set; } = "Se incarca lista de produse...";
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public string LastSearchText { get; set; } = string.Empty;

        public event Action? ProductsChanged;

        public async Task<ServiceResponse<Product>> GetProduct(int productId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productId}");
            return result;
        }

        //public async Task GetProductsCategory(string? categoryUrl = null)
        //{
        //    var result = categoryUrl == null ?
        //        await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product") :
        //        await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{categoryUrl}");
        //    if (result != null && result.Data != null)
        //        Products = result.Data;

        //    ProductsChanged.Invoke();
        //}


        //public async Task GetProductsSubCategory(string? subCategoryUrl = null)
        //{
        //    var result = subCategoryUrl == null ?
        //        await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product/featured") :
        //        await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/subcategory/{subCategoryUrl}");
        //    if (result != null && result.Data != null)
        //    {
        //        Products = result.Data;
        //    }
        //    CurrentPage = 1;
        //    PageCount = 0;
        //    if (Products.Count == 0)
        //    {
        //        Message = "Nu sunt produse.";
        //    }

        //    ProductsChanged?.Invoke();
        //}

        public async Task SearchProducts(string searchText, int page)
        {
            LastSearchText = searchText;
            var result = await _http.GetFromJsonAsync<ServiceResponse<ProductSearchResultDTO>>($"api/product/search/{searchText}/{page}");

            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }
            if (Products.Count == 0)
            {
                Message = "Nu sunt produse.";
            }
            ProductsChanged?.Invoke();
        }

        public async Task<List<string>> GetProductSearchSuggestions(string searchText)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/searchsuggestions/{searchText}");

            return result.Data;
        }

        public async Task GetProductsSubCategory(string? subCategoryUrl = null, int page = 1)
        {
            var result = subCategoryUrl == null ?
                await _http.GetFromJsonAsync<ServiceResponse<ProductResponseDTO>>("api/product/featured") :
                await _http.GetFromJsonAsync<ServiceResponse<ProductResponseDTO>>($"api/product/subcategory/{subCategoryUrl}/{page}");
            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }

            if (Products.Count == 0)
            {
                Message = "Nu sunt produse.";
            }

            ProductsChanged?.Invoke();
        }

        public async Task GetAdminProducts(int page)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<ProductSearchResultDTO>>($"api/product/admin/{page}");
            if (result != null && result.Data != null)
            {
                AdminProducts = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }
            if (AdminProducts.Count == 0)
            {
                Message = "Nu sunt produse.";
            }
            ProductsChanged?.Invoke();
        }

        public async Task GetNewProducts()
        {
            var result = await _http.GetFromJsonAsync<List<Product>>($"api/product/newproducts");
            if(result != null)
            {
				NewProducts = result;
            }
            ProductsChanged?.Invoke();
        }

        public async Task<Product> CreateProduct(Product product)
        {
            var result = await _http.PostAsJsonAsync("api/product", product);
            var newProduct = (await result.Content.ReadFromJsonAsync<ServiceResponse<Product>>()).Data;
            return newProduct;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var result = await _http.PutAsJsonAsync($"api/product", product);
            return (await result.Content.ReadFromJsonAsync<ServiceResponse<Product>>()).Data;
        }

        public async Task DeleteProduct(Product product)
        {
            var result = await _http.DeleteAsync($"api/product/{product.Id}");
        }

        public async Task<Product> SetProductView(Product product)
        {
            var result = await _http.PutAsJsonAsync($"api/product/view", product);
            return (await result.Content.ReadFromJsonAsync<ServiceResponse<Product>>()).Data;
        }
    }
}
