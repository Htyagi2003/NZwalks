using Microsoft.EntityFrameworkCore;
using NZwalks.API.Data;
using NZwalks.API.Model.Domain;
using NZwalks.API.Model.DTO;

namespace NZwalks.API.Repositories
{
    public class SqlRepo : IRegionRepo
    {
        private readonly NZWalksDBcontext dBcontext;

        public SqlRepo(NZWalksDBcontext dBcontext)
        {
            this.dBcontext = dBcontext;
        }

        public async Task<Region> createAsync(Region region)
        {
              
            await dBcontext.Regions.AddAsync(region);
            await dBcontext.SaveChangesAsync();
            return region;
            
        }

        public async Task<Region?> deleteAsync(Guid id)
        {
            var regionDomainModel = await dBcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel == null)
            {

                return null;

            }

            //we doesn't have await method for remove method
            dBcontext.Regions.Remove(regionDomainModel);
            await dBcontext.SaveChangesAsync();
            return regionDomainModel;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            
             return await dBcontext.Regions.ToListAsync();

         
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dBcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> updateAsync(Guid id, Region region)
        {
            var regiondomainnodel = await dBcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regiondomainnodel == null)
            {
                return null;
            }
            //change info or update
            regiondomainnodel.Name = region.Name;
            regiondomainnodel.Code = region.Code;
            regiondomainnodel.RegionImageurl = region.RegionImageurl;

            await dBcontext.SaveChangesAsync();

            return regiondomainnodel;
        }
    }
}
