namespace DrPrint.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        event Action OnChange;
        List<Category> Categories { get; set; }
        List<Category> AdminCategories { get; set; }
        Task GetCategoriesAsync();
        Task GetAdminCategoriesAsync();
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int id);
        Category CreateNewCategory();

        Task<ServiceResponse<Category>> GetCategory(int categoryId);
    }
}
