using DrPrint.Client.Pages.Admin;
using DrPrint.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace DrPrint.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<List<Product>>> GetProductAsync()
        {
            var response = new ServiceResponse<List<Product>>()
            {
                Data = await _context.Products.Where(p => p.Visible && !p.Deleted).Include(p => p.SubCategory)
                .Include(p => p.Images)
                .ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        {
            var response = new ServiceResponse<Product>();
            Product product = null!;
            if (_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
            {
                product = await _context.Products.Where(p => !p.Deleted).Include(p => p.SubCategory).Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == productId && !p.Deleted);
            }
            else
            {
                product = await _context.Products.Where(p => p.Visible && !p.Deleted).Include(p => p.SubCategory)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == productId && !p.Deleted && p.Visible);
            }

            if (product == null)
            {
                response.Success = false;
                response.Message = "Produsul nu exista.";
            }
            else
            {
                response.Data = product;
            }
            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductByCategoryAsync(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products.Where(p => p.Category.Url.ToLower()
                .Equals(categoryUrl.ToLower()) && p.Visible && !p.Deleted).Include(p => p.Images).ToListAsync()
            };
            return response;

        }

        public async Task<ServiceResponse<ProductResponseDTO>> GetProductBySubCategoryAsync(string subCategoryUrl, int page)
        {
            var pageResult = 12f;
            var pageCount = Math.Ceiling(_context.Products.Where(p => p.SubCategory.Url.ToLower()
                .Equals(subCategoryUrl.ToLower()) && p.Visible && !p.Deleted).Count() / pageResult);

            var products = await _context.Products.Where(p => p.SubCategory.Url.ToLower()
                .Equals(subCategoryUrl.ToLower()) && p.Visible && !p.Deleted).Include(p => p.Images)
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult).ToListAsync();

            var response = new ServiceResponse<ProductResponseDTO>
            {
                Data = new ProductResponseDTO { Products = products, CurrentPage = page, Pages = (int)pageCount }
            };
            return response;
        }
        public async Task<ServiceResponse<ProductResponseDTO>> GetAdminProducts(int page)
        {
            var pageResults = 12f;
            var pageCount = Math.Ceiling(_context.Products.Count() / pageResults);

            var products = await _context.Products.Where(p => !p.Deleted).Include(p => p.SubCategory).Include(p => p.Images)
            .Skip((page - 1) * (int)pageResults).Take((int)pageResults)
            .ToListAsync();

            var response = new ServiceResponse<ProductResponseDTO>
            {
                Data = new ProductResponseDTO { Products = products, CurrentPage = page, Pages = (int)pageCount }
            };
            return response;
        }

        public async Task<List<Product>> GetNewProducts()
        {
            var allProducts = await _context.Products.Where(p => !p.Deleted).Include(p => p.SubCategory).Include(p => p.Images).ToListAsync();
            var newProducts = allProducts.TakeLast(12).Reverse().ToList();

			return newProducts;
        }

        private async Task<List<Product>> FindProductsBySearchText(string searchText)
        {
            return await _context.Products.Where(p =>
                            p.Name.ToLower().Contains(searchText.ToLower())
                            ||
                            p.Description.ToLower().Contains(searchText.ToLower()) && p.Visible && !p.Deleted) // .Include(p => p.Variants) if it has variants
                            .ToListAsync();
        }

        public async Task<ServiceResponse<ProductSearchResultDTO>> SearchProducts(string searchText, int page)
        {
            var pageResult = 12f;
            var pageCount = Math.Ceiling((await FindProductsBySearchText(searchText)).Count / pageResult);

            var products = await _context.Products.Where(p =>
            p.Name.ToLower().Contains(searchText.ToLower())
            ||
            p.Description.ToLower().Contains(searchText.ToLower()) && p.Visible && !p.Deleted)
            .Include(p => p.Images).Skip((page - 1) * (int)pageResult)
            .Take((int)pageResult).ToListAsync();


            var response = new ServiceResponse<ProductSearchResultDTO>
            {
                Data = new ProductSearchResultDTO { Products = products, CurrentPage = page, Pages = (int)pageCount }
            };
            return response;
        }

        public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText)
        {
            var products = await FindProductsBySearchText(searchText);

            List<string> result = new List<string>();

            foreach (var product in products)
            {
                if (product.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Name);
                }

                if (product.Description != null)
                {
                    var punctuation = product.Description.Where(char.IsPunctuation).Distinct().ToArray();
                    var words = product.Description.Split().Select(s => s.Trim(punctuation));
                    foreach (var word in words)
                    {
                        if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase) && !result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }
                }
            }

            return new ServiceResponse<List<string>> { Data = result };
        }


        public async Task<ServiceResponse<ProductResponseDTO>> GetFeaturedProducts()
        {
            var products = await _context.Products.Where(p => p.Featured && p.Visible && !p.Deleted).Include(p => p.Images)
                .ToListAsync();
            var page = 0;
            var pageCount = 0;
            var response = new ServiceResponse<ProductResponseDTO>
            {
                Data = new ProductResponseDTO { Products = products, CurrentPage = page, Pages = (int)pageCount }

            };
            return response;
        }

        public async Task<ServiceResponse<Product>> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Product> { Data = product };
        }

        public async Task<ServiceResponse<Product>> UpdateProduct(Product product)
        {
            var dbProduct = await _context.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == product.Id);

            if (dbProduct == null)
            {
                return new ServiceResponse<Product>
                {
                    Success = false,
                    Message = "Produsul nu a fost gasit."
                };
            }
            dbProduct.Name = product.Name;
            dbProduct.Description = product.Description;
            dbProduct.ImageUrl = product.ImageUrl;
            dbProduct.Price = product.Price;
            dbProduct.OriginalPrice = product.OriginalPrice;
            dbProduct.View = product.View;
            dbProduct.Stock = product.Stock;
            dbProduct.CategoryId = product.CategoryId;
            dbProduct.SubCategoryId = product.SubCategoryId;
            dbProduct.Featured = product.Featured;
            dbProduct.Visible = product.Visible;

            var productImages = dbProduct.Images;
            _context.Images.RemoveRange(productImages);

            dbProduct.Images = product.Images;

            await _context.SaveChangesAsync();
            return new ServiceResponse<Product> { Data = product };
        }

        public async Task<ServiceResponse<bool>> DeleteProduct(int id)
        {
            var dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Produsul nu a fost gasit."
                };
            }
            if (string.IsNullOrWhiteSpace(dbProduct.ImageUrl))
            {
                _context.Remove(dbProduct);
            }
            else
            {
                dbProduct.Deleted = true;
            }
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<Product>> SetProductView(Product product)
        {
            var dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
            if (dbProduct == null)
            {
                return new ServiceResponse<Product>
                {
                    Success = false,
                    Message = "Produsul nu a fost gasit."
                };
            }
            dbProduct.View++;
            await _context.SaveChangesAsync();
            return new ServiceResponse<Product> { Data = product };
        }
    }
}
