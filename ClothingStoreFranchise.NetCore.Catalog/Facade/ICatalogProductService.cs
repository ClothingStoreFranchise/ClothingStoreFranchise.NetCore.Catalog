using ClothingStoreFranchise.NetCore.Catalog.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Facade
{
    public interface ICatalogProductService
    {
        Task<CatalogProductDto> CreateAsync(CatalogProductDto customer);


        Task<ICollection<CatalogProductDto>> CreateAsync(ICollection<CatalogProductDto> customer);

        Task<CatalogProductDto> UpdateAsync(CatalogProductDto customer);

        Task DeleteAsync(long id);

        Task<ICollection<CatalogProductDto>> LoadAllAsync();
    }
}
