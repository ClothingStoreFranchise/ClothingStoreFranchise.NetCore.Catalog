using AutoMapper;
using ClothingStoreFranchise.NetCore.Catalog.Dao;
using ClothingStoreFranchise.NetCore.Catalog.Dto;
using ClothingStoreFranchise.NetCore.Catalog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Facade.Impl
{
    public class CategoryService : CatalogBaseService<Category, long, CategoryDto, ICategoryDao>, ICategoryService
    {
        private readonly ICategoryDao _categoryDao;

        public CategoryService(ICategoryDao categoryDao, IMapper mapper) : base(categoryDao, mapper)
        {
            _categoryDao = categoryDao;
        }

        public async Task<ICollection<CategoryDto>>  GetAllParentCategories()
        {
            ICollection<Category> categories = await _categoryDao.LoadAllAsync();
            return categories.Select(l => _mapper.Map<CategoryDto>(l))
                .Where(l => l.CategoryBelonging == null)
                .OrderBy(n => n.Name)
                .ToList();
        }

        public async Task<ICollection<CatalogProductDto>> GetSubcategoryProducts(long subcategoryId)
        {
            Category subcategory = await _categoryDao.FindByIdAsync(subcategoryId);
            return subcategory.CatalogProducts.Select(l => _mapper.Map<CatalogProductDto>(l)).ToList();
        }

        protected override Expression<Func<Category, bool>> EntityAlreadyExistsCondition(CategoryDto dto)
        {
            return c => c.CategoryBelongingId == dto.CategoryBelongingId
                && c.Name == dto.Name;
        }

        protected override Expression<Func<Category, bool>> EntityAlreadyExistsToUpdateCondition(CategoryDto dto)
        {
            return c => c.Id == dto.Id;
        }

        protected override Expression<Func<Category, bool>> EntityHasDependenciesToDeleteCondition(ICollection<long> listAppIds)
        {
            throw new NotImplementedException();
        }

        protected override bool IsValid(CategoryDto dto)
        {
            var isValid = false;
            if (NullValidations(dto))
            {
                isValid = true;

                if(dto.CategoryBelongingId != null && dto.ClothingSizeType == null)
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        private static bool NullValidations(CategoryDto dto)
        {
            return dto != null
                && !string.IsNullOrWhiteSpace(dto.Name);
        }
    }
}
