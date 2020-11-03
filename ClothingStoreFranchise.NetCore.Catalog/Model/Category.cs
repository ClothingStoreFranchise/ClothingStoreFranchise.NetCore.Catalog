using ClothingStoreFranchise.NetCore.Common.Constants;
using ClothingStoreFranchise.NetCore.Common.Extensible;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingStoreFranchise.NetCore.Catalog.Model
{
    public class Category : ExtensibleEntity<long>
    {
        public string Name { get; set; }

        [ForeignKey("CategoryBelonging")]
        public long? CategoryBelongingId { get; set; }

        public Category CategoryBelonging { get; set; }

        public ICollection<Category> Subcategories { get; set; }

        public ClothingSizeType? TypeClothingSize { get; set; }

        public Offer CurrentOffer { get; set; }

        public ICollection<Offer> Offers { get; set; }

        public ICollection<CatalogProduct> CatalogProducts { get; set; }

        public override long GetAppId() => Id;

        public Category()
        {
            Subcategories = new List<Category>();
            Offers = new List<Offer>();
            CatalogProducts = new List<CatalogProduct>();
        }
    }
}
