using ClothingStoreFranchise.NetCore.Catalog.Model;
using ClothingStoreFranchise.NetCore.Common.Extensible;
using ClothingStoreFranchise.NetCore.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Dto
{
    public class CategoryDto : BaseExtensibleEntityDto, IEntityDto<long>
    {
        public long Id { get; set; }

        public string Nombre { get; set; }

        public long CategoryBelonging { get; set; }

        public ICollection<CategoryDto> Subcategories { get; set; }

        public OfferDto CurrentOffer { get; set; }

        public ICollection<OfferDto> OffersRecord { get; set; }

        public override string ExtensibleEntityName => typeof(Category).Name;

        public long Key() => Id;
    }
}
