using ClothingStoreFranchise.NetCore.Common.Extensible;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingStoreFranchise.NetCore.Catalog.Model
{
    public class CatalogProduct : ExtensibleEntity<long>
    {
        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public string PictureUrl { get; set; }

        public Offer CurrentOffer { get; set; }

        public ICollection<Offer> Offers { get; set; }

        [ForeignKey("Subategory")]
        public long SubcategoryId { get; set; }

        public Category Subcategory { get; set; }

        public override long GetAppId() => Id;
    }
}
