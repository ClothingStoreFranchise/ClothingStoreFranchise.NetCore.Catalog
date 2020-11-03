using ClothingStoreFranchise.NetCore.Catalog.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Facade
{
    public interface ICategoryService
    {
        Task<CategoryDto> CreateAsync(CategoryDto category);

        Task<ICollection<CategoryDto>> LoadAllAsync();

        Task<ICollection<CategoryDto>> GetAllParentCategories();

        Task<ICollection<CatalogProductDto>> GetSubcategoryProducts(long subcategoryId);
    }
}
