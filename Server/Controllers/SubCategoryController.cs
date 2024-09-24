using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrPrint.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<SubCategory>>>> GetSubCategoriesAsync()
        {
            var result = await _subCategoryService.GetSubCategoriesAsync();
            return Ok(result);
        }

        [HttpGet("{subCategoryId}")]
        // [Route("{id}")]
        public async Task<ActionResult<ServiceResponse<SubCategory>>> GetSubCategory(int subCategoryId)
        {
            var result = await _subCategoryService.GetSubCategoryAsync(subCategoryId);
            return Ok(result);
        }

        [HttpGet("category/{categoryUrl}")]
        public async Task<ActionResult<List<SubCategory>>> GetSubCategoryByCategoryAsync(string categoryUrl)
        {
            var result = await _subCategoryService.GetSubCategoryByCategoryAsync(categoryUrl);
            return Ok(result);
        }

        [HttpGet("admin")] //, Authorize(Roles = "Admin")
        public async Task<ActionResult<ServiceResponse<List<SubCategory>>>> GetAdminSubCategoriesAsync()
        {
            var result = await _subCategoryService.GetAdminSubCategoriesAsync();
            return Ok(result);
        }

        [HttpDelete("admin/{id}")]
        public async Task<ActionResult<ServiceResponse<List<SubCategory>>>> DeleteSubCategory(int id)
        {
            var result = await _subCategoryService.DeleteSubCategory(id);
            return Ok(result);
        }

        [HttpPost("admin")]
        public async Task<ActionResult<ServiceResponse<List<SubCategory>>>> AddSubCategory(SubCategory subCategory)
        {
            var result = await _subCategoryService.AddSubCategory(subCategory);
            return Ok(result);
        }

        [HttpPut("admin")]
        public async Task<ActionResult<ServiceResponse<List<SubCategory>>>> UpdateSubCategory(SubCategory subCategory)
        {
            var result = await _subCategoryService.UpdateSubCategory(subCategory);
            return Ok(result);
        }
    }
}

