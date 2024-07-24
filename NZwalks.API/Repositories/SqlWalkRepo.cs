using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZwalks.API.Data;
using NZwalks.API.Model.Domain;
using System.Globalization;

namespace NZwalks.API.Repositories
{
    public class SqlWalkRepo : IWalkRepo
    {
        private readonly NZWalksDBcontext dbContext;

        public SqlWalkRepo(NZWalksDBcontext dbContext)

        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> createAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();


            return walk;

        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
           var WalkDomainModel=await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (WalkDomainModel == null) {
                return null;
            }

             dbContext.Walks.Remove(WalkDomainModel);

            await dbContext.SaveChangesAsync();

            return WalkDomainModel;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuerry = null,string ? SortBy = null, bool IsAscending=true,int PageNumber=1,int PageSize=1000)
        {
          // return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();

            var walks= dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuerry) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase)) {

                    walks=walks.Where(x=>x.Name.Contains(filterQuerry));

                }
            }

            //sorting
            if (string.IsNullOrWhiteSpace(SortBy) == false) {

                if (SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {

                    walks = IsAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);


                }
                else if (SortBy.Equals("Length", StringComparison.OrdinalIgnoreCase)) { 
                
                   walks=IsAscending?walks.OrderBy(x => x.Lenghtinkm): walks.OrderByDescending(x=>x.Lenghtinkm);    

                
                }
            
            
            }

            //pagination
            var SkipResult=(PageNumber-1)*PageSize;

            return await walks.Skip(SkipResult).Take(PageSize).ToListAsync();
        }

        public async Task<Walk> GetByIdAsync(Guid id)
        {
            var WalkDomainModel=await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);

            if (WalkDomainModel == null)
            {
                return null;
            }

            return WalkDomainModel;

        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var WAlkDomainModel=await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id); 

            if(WAlkDomainModel ==null) {
                return null;
            }

            WAlkDomainModel.Name= walk.Name;
            WAlkDomainModel.Lenghtinkm=walk.Lenghtinkm;
            WAlkDomainModel.Description=walk.Description;
            WAlkDomainModel.WalkImageUrl=walk.WalkImageUrl;
            WAlkDomainModel.DifficultyId=walk.DifficultyId;
            WAlkDomainModel.RegionId=walk.RegionId; 

            await dbContext.SaveChangesAsync();

            return WAlkDomainModel;
        }
    }
}
