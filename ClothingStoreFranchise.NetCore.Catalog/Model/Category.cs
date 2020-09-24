using ClothingStoreFranchise.NetCore.Common.Extensible;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Model
{
    public class Category : ExtensibleEntity<long>
    {
        public string Nombre { get; set; }

        public long? CategoryBelonging { get; set; }

        [NotMapped]
        public ICollection<Category> Subcategories { get; set; }

        public Offer CurrentOffer { get; set; }

        public ICollection<Offer> Offers { get; set; }

        public override long GetAppId()
        {
            throw new NotImplementedException();
        }
    }
}
