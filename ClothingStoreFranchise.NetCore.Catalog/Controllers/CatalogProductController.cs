using ClothingStoreFranchise.NetCore.Catalog.Dto;
using ClothingStoreFranchise.NetCore.Catalog.Facade;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Catalog.Controllers
{
    [Route("catalog_products")]
    public class CatalogProductController : ControllerBase
    {
        private readonly ICatalogProductService _catalogProductService;

        public CatalogProductController(ICatalogProductService catalogProductService)
        {
            _catalogProductService = catalogProductService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<CatalogProductDto>>> GetAll()
        {        
            return Ok(await _catalogProductService.LoadAllAsync());
        }

        [HttpGet("novelties")]
        public async Task<ActionResult<ICollection<CatalogProductDto>>> GetNovelties()
        {
            return Ok(await _catalogProductService.GetNovelties());
        }

        [HttpPost]
        public async Task<ActionResult<CatalogProductDto>> Post([FromBody] CatalogProductDto catalogProductDto)
        {
            return Created("catalog_products",await _catalogProductService.CreateAsync(catalogProductDto));
        }

        [HttpPut]
        public async Task<ActionResult<CatalogProductDto>> Update([FromBody] CatalogProductDto catalogProductDto)
        {
            return Ok(await _catalogProductService.UpdateAsync(catalogProductDto));
        }

        [HttpDelete("{productId}")]
        public async Task Delete(long productId){
 
            await _catalogProductService.DeleteAsync(productId);
        }
    }
}
