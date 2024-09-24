namespace DrPrint.Server.Services.SubCategoryService
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubCategoryService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<List<SubCategory>>> GetSubCategoriesAsync()
        {
            //var subCategories = await _context.SubCategories.Where(c => !c.Deleted && c.Visible).ToListAsync();
            //return new ServiceResponse<List<SubCategory>>
            //{
            //    Data = subCategories
            //};
            var subCategories = new List<SubCategory>();
            if (_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
            {
                subCategories = await _context.SubCategories.Include(c => c.Category).ToListAsync();

            }
            else
            {
                subCategories = await _context.SubCategories.Where(c => c.Visible).Include(c => c.Category).ToListAsync();
            }
            return new ServiceResponse<List<SubCategory>>
            {
                Data = subCategories
            };
        }

        public async Task<ServiceResponse<SubCategory>> GetSubCategoryAsync(int subCategoryId)
        {
            var response = new ServiceResponse<SubCategory>();
            var subCategory = await _context.SubCategories.Include(sc => sc.Category)
                .FirstOrDefaultAsync(sc => sc.Id == subCategoryId);
            if (subCategory == null)
            {
                response.Success = false;
                response.Message = "Subcategoria nu exista.";
            }
            else
            {
                response.Data = subCategory;
            }
            return response;
        }

        public async Task<ServiceResponse<List<SubCategory>>> GetSubCategoryByCategoryAsync(string categoryUrl)
        {
            var response = new ServiceResponse<List<SubCategory>>
            {
                Data = await _context.SubCategories.Where(p => p.Category.Url.ToLower()
                .Equals(categoryUrl.ToLower())).ToListAsync()
            };
            return response;
        }
        public async Task<ServiceResponse<List<SubCategory>>> AddSubCategory(SubCategory subCategory)
        {
            subCategory.Editing = subCategory.IsNew = false;
            _context.SubCategories.Add(subCategory);
            await _context.SaveChangesAsync();
            return await GetAdminSubCategoriesAsync();
        }
        public async Task<ServiceResponse<List<SubCategory>>> UpdateSubCategory(SubCategory subCategory)
        {
            var dbSubCategory = await GetSubCategoryById(subCategory.Id);
            if (dbSubCategory == null)
            {
                return new ServiceResponse<List<SubCategory>>
                {
                    Success = false,
                    Message = "Subcategoria nu a fost gasita."
                };
            }
            dbSubCategory.Name = subCategory.Name;
            dbSubCategory.Url = subCategory.Url;
            dbSubCategory.Description = subCategory.Description;
			dbSubCategory.CategoryId = subCategory.CategoryId;
			dbSubCategory.Visible = subCategory.Visible;
            await _context.SaveChangesAsync();
            return await GetAdminSubCategoriesAsync();
        }
        public async Task<ServiceResponse<List<SubCategory>>> DeleteSubCategory(int id)
        {
            SubCategory subCategory = await GetSubCategoryById(id);
            if (subCategory == null)
            {
                return new ServiceResponse<List<SubCategory>>
                {
                    Success = false,
                    Message = "Subcategoria nu a fost gasita."
                };
            }
            //subCategory.Deleted = true;
            _context.SubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();
            return await GetAdminSubCategoriesAsync();
        }

        private async Task<SubCategory> GetSubCategoryById(int id)
        {
            return await _context.SubCategories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ServiceResponse<List<SubCategory>>> GetAdminSubCategoriesAsync()
        {
            var subCategories = await _context.SubCategories.Where(sc => !sc.Deleted).ToListAsync();
            return new ServiceResponse<List<SubCategory>>
            {
                Data = subCategories
            };
        }
    }
}
