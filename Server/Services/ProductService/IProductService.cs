namespace DrPrint.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductAsync();
        Task<ServiceResponse<Product>> GetProductAsync(int productId);

        Task<ServiceResponse<List<Product>>> GetProductByCategoryAsync(string categoryUrl);

        Task<ServiceResponse<ProductResponseDTO>> GetProductBySubCategoryAsync(string subCategoryUrl, int page);

        Task<ServiceResponse<ProductSearchResultDTO>> SearchProducts(string searchText, int page);
        Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);

        Task<ServiceResponse<ProductResponseDTO>> GetFeaturedProducts();
        Task<ServiceResponse<ProductResponseDTO>> GetAdminProducts(int page);
        Task<List<Product>> GetNewProducts();

        Task<ServiceResponse<Product>> CreateProduct(Product product);
        Task<ServiceResponse<Product>> UpdateProduct(Product product);
        Task<ServiceResponse<bool>> DeleteProduct(int id);

        Task<ServiceResponse<Product>> SetProductView(Product product);
    }
}
