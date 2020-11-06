using AutoMapper;
using ClothingStoreFranchise.NetCore.Catalog.Dao;
using ClothingStoreFranchise.NetCore.Catalog.Dto;
using ClothingStoreFranchise.NetCore.Catalog.Dto.Events;
using ClothingStoreFranchise.NetCore.Catalog.Model;
using ClothingStoreFranchise.NetCore.Common.Constants;

namespace ClothingStoreFranchise.NetCore.Catalog.Mapper
{
    public class CatalogProductProfile : Profile
    {
        public CatalogProductProfile()
        {
            CreateMap<CatalogProduct, CatalogProductDto>();

            CreateMap<CatalogProductDto, CatalogProduct>()
                .ForMember(entity => entity.CurrentOffer, p => p.Ignore())
                .ForMember(entiy => entiy.Offers, p => p.Ignore());
                

            CreateMap<CatalogProductDto, CreateProductEvent>()
                .AfterMap<TrackCreateProductEventAction>();

            CreateMap<CatalogProductDto, UpdateProductEvent>()
                .AfterMap<TrackUpdateProductEventAction>();
        }
    }

    public class TrackCreateProductEventAction : IMappingAction<CatalogProductDto, CreateProductEvent>
    {
        private readonly ICategoryDao _categoryDao;

        public TrackCreateProductEventAction(ICategoryDao categoryDao) 
        {
            _categoryDao = categoryDao;
        }


        public void Process(CatalogProductDto source, CreateProductEvent destination, ResolutionContext context)
        {
            TrackCategory(source, destination);
        }

        public void TrackCategory(CatalogProductDto dto, CreateProductEvent @event)
        {
            Category b = _categoryDao.Load(dto.SubcategoryId);
            @event.ClothingSizeType = (ClothingSizeType)b.ClothingSizeType;
        }
    }

    public class TrackUpdateProductEventAction : IMappingAction<CatalogProductDto, UpdateProductEvent>
    {
        private readonly ICategoryDao _categoryDao;

        public TrackUpdateProductEventAction(ICategoryDao categoryDao)
        {
            _categoryDao = categoryDao;
        }


        public void Process(CatalogProductDto source, UpdateProductEvent destination, ResolutionContext context)
        {
            TrackCategory(source, destination);
        }

        public void TrackCategory(CatalogProductDto dto, UpdateProductEvent @event)
        {
            Category b = _categoryDao.Load(dto.SubcategoryId);
            @event.ClothingSizeType = (ClothingSizeType)b.ClothingSizeType;
        }
    }
}
