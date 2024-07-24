using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Model.Domain;

namespace NZwalks.API.Repositories
{
    public interface IWalkRepo
    {
         Task<Walk> createAsync(Walk walk);

        Task<List<Walk>> GetAllAsync( string? filterOn=null,string? filterQuerry=null,string? SortBy=null,bool IsAscending=true, int PageNumber = 1, int PageSize = 1000);

        Task<Walk?> GetByIdAsync(Guid id);

        Task<Walk?> UpdateAsync(Guid id , Walk walk);

       
        Task<Walk?> DeleteAsync(Guid id);

    }
}
