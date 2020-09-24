using AutoMapper;
using ClothingStoreFranchise.NetCore.Catalog.Dto;
using ClothingStoreFranchise.NetCore.Catalog.Dto.Events;
using ClothingStoreFranchise.NetCore.Catalog.Model;

namespace ClothingStoreFranchise.NetCore.Catalog.Mapper
{
    public class CatalogProductProfile : Profile
    {
        public CatalogProductProfile()
        {
            CreateMap<CatalogProduct, CatalogProductDto>();

            CreateMap<CatalogProductDto, CatalogProduct>()
                .ForMember(entity => entity.CurrentOffer, p => p.Ignore())
                .ForMember(entiy => entiy.Offers, p => p.Ignore())
                .ForMember(entity => entity.CatalogProductId, p => p.Ignore());

            CreateMap<CatalogProductDto, CreateProductEvent>();
        }
    }
}
