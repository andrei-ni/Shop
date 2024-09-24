namespace DrPrint.Server.Services.SubCategoryService
{
    public interface ISubCategoryService
    {
        Task<ServiceResponse<List<SubCategory>>> GetSubCategoriesAsync();
        Task<ServiceResponse<SubCategory>> GetSubCategoryAsync(int subCategoryId);

        Task<ServiceResponse<List<SubCategory>>> GetSubCategoryByCategoryAsync(string categoryUrl);

        Task<ServiceResponse<List<SubCategory>>> GetAdminSubCategoriesAsync();
        Task<ServiceResponse<List<SubCategory>>> AddSubCategory(SubCategory subCategory);
        Task<ServiceResponse<List<SubCategory>>> UpdateSubCategory(SubCategory subCategory);
        Task<ServiceResponse<List<SubCategory>>> DeleteSubCategory(int id);
    }
}
