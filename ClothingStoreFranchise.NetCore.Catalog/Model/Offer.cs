using ClothingStoreFranchise.NetCore.Common.Extensible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Model
{
    public class Offer : ExtensibleEntity<long>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Discount { get; set; }

        public DateTime StartDate { get; set;  }

        public DateTime EndDate { get; set; }

        public override long GetAppId()
        {
            throw new NotImplementedException();
        }
    }
}
