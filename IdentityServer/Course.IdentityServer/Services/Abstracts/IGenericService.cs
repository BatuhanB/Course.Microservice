using Course.IdentityServer.Dtos;
using System.Threading.Tasks;

namespace Course.IdentityServer.Services.Abstracts
{
    public interface IGenericService<T>
    {
        Task<Response<bool>> CreateAsync(T entity);
        Task<Response<bool>> UpdateAsync(T entity);
        Task<Response<T>> GetByUserIdAsync(string userId);
        Task<Response<T>> GetByIdAsync(string id);
    }
}
