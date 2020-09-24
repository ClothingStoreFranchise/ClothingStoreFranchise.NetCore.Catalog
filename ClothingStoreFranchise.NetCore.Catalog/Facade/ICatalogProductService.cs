using ClothingStoreFranchise.NetCore.Catalog.Dto;
using ClothingStoreFranchise.NetCore.Common.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Facade
{
    public interface ICatalogProductService
    {
        Task<CatalogProductDto> CreateAsync(CatalogProductDto customer);


        Task<ICollection<CatalogProductDto>> CreateAsync(ICollection<CatalogProductDto> customer);

        Task<CatalogProductDto> UpdateAsync(CatalogProductDto customer);

        Task DeleteAsync(ICollection<long> listAppId);

        Task<ICollection<CatalogProductDto>> LoadAllAsync();

        Task CreateProductAsync(CatalogProductDto catalogProductDto);
    }
}
