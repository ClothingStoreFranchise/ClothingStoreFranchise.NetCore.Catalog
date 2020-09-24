using ClothingStoreFranchise.NetCore.Common.Events.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Dto.Events
{
    public class CreateProductEvent : IntegrationEvent
    {
        /*public CreateProductEvent(long id, int stock, decimal unitPrice, string pictureUrl)
        {
            Id = id;
            Stock = stock;
            UnitPrice = unitPrice;
            PictureUrl = pictureUrl;
        }*/

        public long Id { get; set; }

        public string Name { get; set; }

        public int Stock { get; set; }

        public decimal UnitPrice { get; set; }

        public string PictureUrl { get; set; }
    }
}
