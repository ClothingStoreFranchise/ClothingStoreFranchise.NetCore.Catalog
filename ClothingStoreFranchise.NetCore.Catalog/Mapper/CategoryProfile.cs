using AutoMapper;
using ClothingStoreFranchise.NetCore.Catalog.Dao;
using ClothingStoreFranchise.NetCore.Catalog.Dto;
using ClothingStoreFranchise.NetCore.Catalog.Facade;
using ClothingStoreFranchise.NetCore.Catalog.Model;

namespace ClothingStoreFranchise.NetCore.Catalog.Mapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {

            CreateMap<Category, CategoryDto>();
            // Dto --> Entity
            CreateMap<CategoryDto, Category>();
        }
    }

    /*public class TrackCategoryAction : IMappingAction<CategoryDto, Category>
    {
        private readonly ICategoryDao _categoryDao;

        public TrackCategoryAction(ICategoryDao categoryDao)
        {
            _categoryDao = categoryDao;
        }


        public void Process(CategoryDto dto, Category entity, ResolutionContext context)
        {
        }
    }*/
}