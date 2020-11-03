using AutoMapper;
using ClothingStoreFranchise.NetCore.Catalog.Dao;
using ClothingStoreFranchise.NetCore.Catalog.Dto;
using ClothingStoreFranchise.NetCore.Catalog.Dto.Events;
using ClothingStoreFranchise.NetCore.Catalog.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        
        public Task<ICollection<string>> LoadEditableEntitiesAsync()
        {
            throw new NotImplementedException();
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
