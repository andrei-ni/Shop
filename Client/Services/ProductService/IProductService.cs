namespace DrPrint.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action ProductsChanged;

        List<Product> Products { get; set; }
        List<Product> AdminProducts { get; set; }
        List<Product> NewProducts { get; set; }

        string Message { get; set; } // gives user information about the search, ex: product not found
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        string LastSearchText { get; set; }
        

        //Task GetProductsCategory(string? categoryUrl = null);

        Task GetProductsSubCategory(string? subCategoryUrl = null, int page = 1);

        Task<ServiceResponse<Product>> GetProduct(int productId);
        Task GetAdminProducts(int page);
        Task GetNewProducts();

        Task SearchProducts(string searchText, int page);
        Task<List<string>> GetProductSearchSuggestions(string searchText);

        
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task DeleteProduct(Product product);

        Task<Product> SetProductView(Product product);
    }
}
