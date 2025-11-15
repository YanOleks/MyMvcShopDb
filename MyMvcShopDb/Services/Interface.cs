using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyMvcShopDb.Services
{
    public interface IPhotoService
    {
        Task<string> AddPhotoAsync(IFormFile file);
    }
}