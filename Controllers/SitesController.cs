using BusinessDirectory.DTOs;
using BusinessDirectory.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessDirectory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SitesController : ControllerBase
    {
        private readonly SitesService _sitesService;

        public SitesController(SitesService sitesService)
        {
            _sitesService = sitesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _sitesService.GetAllSites());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var site = await _sitesService.GetSiteById(id);
            if (site == null) return NotFound();
            return Ok(site);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSiteDTO siteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var site = await _sitesService.AddSite(siteDTO);
                return CreatedAtAction(nameof(Get), new { id = site.Id }, site);
            }
            catch (Exception ex)
            {
                return Conflict(new { ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSiteDTO updateSiteDTO)
        {
            var updatedSite = await _sitesService.UpdateSite(id, updateSiteDTO.SiteName, updateSiteDTO.SiteType);

            if (updatedSite == null) return NotFound();

            return Ok(updatedSite);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _sitesService.DeleteSite(id);
            if (!deleted) return NotFound();

            return NoContent();
        }

    }
}
