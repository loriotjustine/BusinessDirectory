using BusinessDirectory.DTOs;
using BusinessDirectory.Enums;
using BusinessDirectory.Models;
using BusinessDirectory.Repositories;

namespace BusinessDirectory.Services
{
    public class SitesService
    {
        private readonly ISitesRepository _sitesRepository;
        private readonly IUsersRepository _usersRepository;

        public SitesService(ISitesRepository sitesRepository, IUsersRepository usersRepository)
        {
            _sitesRepository = sitesRepository;
            _usersRepository = usersRepository;
        }

        public async Task<List<SiteDTO>> GetAllSites()
        {
            var sites = await _sitesRepository.ListAsync();

            return sites.Select(s => new SiteDTO
            {
                Id = s.Id,
                SiteName = s.SiteName,
                SiteType = (int)s.SiteType,
                SiteTypeName = s.SiteType.ToString()
            }).ToList();
        }

        public async Task<Site?> GetSiteById(int id)
        {
            return await _sitesRepository.FindAsync(id);
        }

        public async Task<Site> AddSite(CreateSiteDTO siteDTO)
        {
            if (await _sitesRepository.AnyAsync(s => s.SiteName == siteDTO.SiteName))
                throw new Exception("Le site existe déjà");

            var site = new Site
            {
                SiteName = siteDTO.SiteName,
                SiteType = siteDTO.SiteType
            };

            return await _sitesRepository.AddAsync(site);
        }

        public async Task<Site?> UpdateSite(int id, string siteName, SiteType siteType)
        {
            var existingSite = await _sitesRepository.FindAsync(id);
            if (existingSite == null) return null;

            existingSite.SiteName = siteName;
            existingSite.SiteType = siteType;
            await _sitesRepository.UpdateAsync(existingSite);
            return existingSite;
        }

        public async Task<bool> DeleteSite(int id)
        {
            var usersWithSite = await _usersRepository.ListAsync(u => u.SiteId == id);
            if (usersWithSite.Any())
            {
                throw new Exception("Suppression impossible : au moins un utilisateur est affecté à ce site.");
            }

            var site = await _sitesRepository.FindAsync(id);
            if (site == null) return false;

            await _sitesRepository.DeleteAsync(site);
            return true;
        }
    }
}
