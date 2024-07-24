using NZwalks.API.Model.Domain;

namespace NZwalks.API.Repositories
{
    public interface IImageRepo
    {

        Task<Image> Upload(Image image);
    }
}
