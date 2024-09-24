﻿namespace DrPrint.Server.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<List<Category>>> AddCategory(Category category)
        {
            category.Editing = category.IsNew = false;
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return await GetAdminCategoriesAsync();
        }
        public async Task<ServiceResponse<List<Category>>> UpdateCategory(Category category)
        {
            var dbCategory = await GetCategoryById(category.Id);
            if(dbCategory == null)
            {
                return new ServiceResponse<List<Category>>
                {
                    Success = false,
                    Message = "Categoria nu a fost gasita."
                };
            }
            dbCategory.Name = category.Name;
            dbCategory.Url = category.Url;
            dbCategory.Visible = category.Visible;
            await _context.SaveChangesAsync();
            return await GetAdminCategoriesAsync();
        }
        public async Task<ServiceResponse<List<Category>>> DeleteCategory(int id)
        {
            Category category = await GetCategoryById(id);
            if(category == null)
            {
                return new ServiceResponse<List<Category>>
                {
                    Success = false,
                    Message = "Categoria nu a fost gasita."
                };
            }
            //category.Deleted = true;
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return await GetAdminCategoriesAsync();
        }

        private async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ServiceResponse<List<Category>>> GetAdminCategoriesAsync()
        {
            var categories = await _context.Categories.Where(c => !c.Deleted).ToListAsync();
            return new ServiceResponse<List<Category>>
            {
                Data = categories
            };
        }

        public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
        {
            //var categories = await _context.Categories.Where(c => !c.Deleted && c.Visible).ToListAsync();
            //return new ServiceResponse<List<Category>>
            //{
            //    Data = categories
            //};
            var categories = new List<Category>();

            if (_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
            {
                categories = await _context.Categories.ToListAsync();
            }
            else
            {
                categories = await _context.Categories.Where(c => c.Visible).ToListAsync();
            }
            return new ServiceResponse<List<Category>>
            {
                Data = categories
            };
        }

        
    }
}
