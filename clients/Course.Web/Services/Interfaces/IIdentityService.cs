using Course.Shared.Dtos;
using Course.Web.Models;
using IdentityModel.Client;

namespace Course.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SigIn(SignIn model);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();
    }
}
