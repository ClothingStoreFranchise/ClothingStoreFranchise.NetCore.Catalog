using AutoMapper;
using ClothingStoreFranchise.NetCore.Catalog.Dao;
using ClothingStoreFranchise.NetCore.Catalog.Dto;
using ClothingStoreFranchise.NetCore.Catalog.Dto.Events;
using ClothingStoreFranchise.NetCore.Catalog.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Facade.Impl
{
    public class CatalogProductService : CatalogBaseService<CatalogProduct, long, CatalogProductDto, ICatalogProductDao>, ICatalogProductService
    {
        private readonly ICatalogIntegrationEventService _catalogIntegrationEventService;

        public CatalogProductService(ICatalogProductDao catalogProductDao, IMapper mapper, ICatalogIntegrationEventService catalogIntegrationEventService)
            : base(catalogProductDao, mapper)
        {
            _catalogIntegrationEventService = catalogIntegrationEventService;
        }

        public override async Task<CatalogProductDto> CreateAsync(CatalogProductDto catalogProductDto)
        {
            var productCreated = await base.CreateAsync(catalogProductDto);
            var productCreatedEvent = _mapper.Map<CreateProductEvent>(productCreated);
            await _catalogIntegrationEventService.SaveEventAndCatalogContextChangesAsync(productCreatedEvent);
            await _catalogIntegrationEventService.PublishThroughEventBusAsync(productCreatedEvent);
            return productCreated;
        }

        public override async Task<CatalogProductDto> UpdateAsync(CatalogProductDto catalogProductDto)
        {
            var productUpdated = await base.UpdateAsync(catalogProductDto);
            var productUpdatedEvent = _mapper.Map<UpdateProductEvent>(productUpdated);
            await _catalogIntegrationEventService.SaveEventAndCatalogContextChangesAsync(productUpdatedEvent);
            await _catalogIntegrationEventService.PublishThroughEventBusAsync(productUpdatedEvent);
            return productUpdated;
        }

        public override async Task DeleteAsync(long id)
        {
            await base.DeleteAsync(id);
            var deleteProductEvent = new DeleteProductEvent(id);
            await _catalogIntegrationEventService.SaveEventAndCatalogContextChangesAsync(deleteProductEvent);
            await _catalogIntegrationEventService.PublishThroughEventBusAsync(deleteProductEvent);
        }

        public async Task<ICollection<CatalogProductDto>> GetNovelties()
        {
            var novelties = await _entityDao.GetNovelties();
            return novelties.Select(l => _mapper.Map<CatalogProductDto>(l)).ToList();
        }

        protected override Expression<Func<CatalogProduct, bool>> EntityAlreadyExistsCondition(CatalogProductDto dto)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<CatalogProduct, bool>> EntityHasDependenciesToDeleteCondition(ICollection<long> listAppIds)
        {
            throw new NotImplementedException();
        }

        protected override bool IsValid(CatalogProductDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
