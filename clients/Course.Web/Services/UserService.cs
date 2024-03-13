using Course.Web.Models;
using Course.Web.Services.Interfaces;

namespace Course.Web.Services;
public class UserService(HttpClient httpClient) : IUserService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<UserViewModel> GetUser()
    {
        return await _httpClient.GetFromJsonAsync<UserViewModel>("/api/user/get");
    }
}