using Course.Web.Models;

namespace Course.Web.Services.Interfaces;
public interface IUserService
{
    Task<UserViewModel> GetUser();
}