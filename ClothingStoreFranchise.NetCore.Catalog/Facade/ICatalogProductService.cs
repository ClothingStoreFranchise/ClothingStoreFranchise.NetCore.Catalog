using ClothingStoreFranchise.NetCore.Catalog.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Facade
{
    public interface ICatalogProductService
    {
        Task<CatalogProductDto> CreateAsync(CatalogProductDto catalogProductDto);

        Task<ICollection<CatalogProductDto>> CreateAsync(ICollection<CatalogProductDto> catalogProductDtos);

        Task<CatalogProductDto> UpdateAsync(CatalogProductDto catalogProductDto);

        Task DeleteAsync(long id);

        Task<ICollection<CatalogProductDto>> LoadAllAsync();

        Task<ICollection<CatalogProductDto>> GetNovelties();
    }
}
