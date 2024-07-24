using NZwalks.API.Model.Domain;

namespace NZwalks.API.Repositories
{
    public interface IRegionRepo
    {
        Task<List<Region>> GetAllAsync();

        Task<Region?> GetByIdAsync(Guid id);

       Task<Region> createAsync(Region region);


        Task<Region?> updateAsync(Guid id,Region region);

        Task<Region?> deleteAsync(Guid id);
    }
}
