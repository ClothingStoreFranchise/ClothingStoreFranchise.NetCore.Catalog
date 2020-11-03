using ClothingStoreFranchise.NetCore.Catalog.Model;
using ClothingStoreFranchise.NetCore.Common.Constants;
using ClothingStoreFranchise.NetCore.Common.Extensible;
using ClothingStoreFranchise.NetCore.Common.Types;
using System.Collections.Generic;

namespace ClothingStoreFranchise.NetCore.Catalog.Dto
{
    public class CategoryDto : BaseExtensibleEntityDto, IEntityDto<long>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long? CategoryBelongingId { get; set; }

        public CategoryDto CategoryBelonging { get; set; }

        public ICollection<CategoryDto> Subcategories { get; set; } = new List<CategoryDto>();

        public ClothingSizeType? ClothingSizeType { get; set; }

        public OfferDto CurrentOffer { get; set; }

        public ICollection<OfferDto> OffersRecord { get; set; } = new List<OfferDto>();

        public ICollection<CatalogProduct> CatalogProducts { get; set; }

        public override string ExtensibleEntityName => typeof(Category).Name;

        public long Key() => Id;
    }
}
