using ClothingStoreFranchise.NetCore.Catalog.Dto;
using ClothingStoreFranchise.NetCore.Catalog.Facade;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Controllers
{
    [Route("category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<CategoryDto>>> GetAll()
        {
            return Ok(await _categoryService.GetAllParentCategories());
        }

        [HttpGet("subcategory/{subcategoryId}")]
        public async Task<ActionResult<ICollection<CatalogProductDto>>> GetSubcategoryProducts(long subcategoryId)
        {
            return Ok(await _categoryService.GetSubcategoryProducts(subcategoryId));
        }

        [HttpPost]
        public async Task<ActionResult<ICollection<CategoryDto>>> Post([FromBody] CategoryDto categoryDto)
        {
            await _categoryService.CreateAsync(categoryDto);
            return Ok(await _categoryService.GetAllParentCategories());
        }
    }
}
