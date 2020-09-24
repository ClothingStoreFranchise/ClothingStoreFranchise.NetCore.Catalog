using ClothingStoreFranchise.NetCore.Catalog.Dto;
using ClothingStoreFranchise.NetCore.Catalog.Facade;
using ClothingStoreFranchise.NetCore.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Controllers
{
    [Route("api/catalog")]
    public class CatalogProductController : ControllerBase
    {
        private readonly ICatalogProductService _catalogProductService;

        public CatalogProductController(ICatalogProductService catalogProductService)
        {
            _catalogProductService = catalogProductService;
        }

        [HttpGet("catalog_products")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<ICollection<CatalogProductDto>>> Get()
        {        //public IEnumerable<Customer> Get()
            return Ok(await _catalogProductService.LoadAllAsync());
        }

        [HttpPost]
        public async Task Post([FromBody] CatalogProductDto catalogProductDto)
        {
            await _catalogProductService.CreateProductAsync(catalogProductDto);
        }
    }
}
