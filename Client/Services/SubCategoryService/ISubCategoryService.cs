namespace DrPrint.Client.Services.SubCategoryService
{
    public interface ISubCategoryService
    {
        event Action SubCategoryChanged;

        List<SubCategory> SubCategories { get; set; }
        Task GetSubCategoriesAsync();

        Task GetSubCategoryCategory(string? categoryUrl = null);

        Task<ServiceResponse<SubCategory>> GetSubCategory(int subCategoryId);

        event Action OnChange;
        List<SubCategory> AdminSubCategories { get; set; }
        Task GetAdminSubCategoriesAsync();
        Task AddSubCategory(SubCategory subCategory);
        Task UpdateSubCategory(SubCategory subCategory);
        Task DeleteSubCategory(int id);
        SubCategory CreateNewSubCategory();

    }
}
