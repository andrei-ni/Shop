namespace DrPrint.Client.Services.SubCategoryService
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly HttpClient _http;

        public SubCategoryService(HttpClient http)
        {
            _http = http;
        }

        public List<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
        public List<SubCategory> AdminSubCategories { get; set; } = new List<SubCategory>();

        public event Action? SubCategoryChanged;
        public event Action? OnChange;

        public async Task AddSubCategory(SubCategory subCategory)
        {
            var response = await _http.PostAsJsonAsync("api/SubCategory/admin", subCategory);
            AdminSubCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<SubCategory>>>()).Data;
            await GetSubCategoriesAsync();
            OnChange.Invoke();
        }

        public SubCategory CreateNewSubCategory()
        {
            var newSubCategory = new SubCategory { IsNew = true, Editing = true };
            AdminSubCategories.Add(newSubCategory);
            OnChange.Invoke();
            return newSubCategory;
        }
        public async Task UpdateSubCategory(SubCategory subCategory)
        {
            var response = await _http.PutAsJsonAsync("api/SubCategory/admin", subCategory);
            AdminSubCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<SubCategory>>>()).Data;
            await GetSubCategoriesAsync();
            OnChange.Invoke();
        }

        public async Task DeleteSubCategory(int id)
        {
            var response = await _http.DeleteAsync($"api/SubCategory/admin/{id}");
            AdminSubCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<SubCategory>>>()).Data;
            await GetSubCategoriesAsync();
            OnChange.Invoke();
        }

        public async Task GetAdminSubCategoriesAsync()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<SubCategory>>>("api/SubCategory/admin");
            if (response != null && response.Data != null)
            {
                AdminSubCategories = response.Data;
            }
        }

        public async Task GetSubCategoriesAsync()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<SubCategory>>>("api/SubCategory");
            if (response != null && response.Data != null)
            {
                SubCategories = response.Data;
            }
        }

        public async Task<ServiceResponse<SubCategory>> GetSubCategory(int subCategoryId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<SubCategory>>($"api/SubCategory/{subCategoryId}");
            return result;
        }

        public async Task GetSubCategoryCategory(string? categoryUrl = null)
        {
            var result = categoryUrl == null ?
                await _http.GetFromJsonAsync<ServiceResponse<List<SubCategory>>>("api/SubCategory") :
                await _http.GetFromJsonAsync<ServiceResponse<List<SubCategory>>>($"api/SubCategory/category/{categoryUrl}");
            if (result != null && result.Data != null)
                SubCategories = result.Data;
            SubCategoryChanged.Invoke();
        }

        
    }
        
}
