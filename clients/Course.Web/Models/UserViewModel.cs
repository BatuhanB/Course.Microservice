namespace Course.Web.Models;
public sealed record UserViewModel(string Id, string UserName, string Email, string City)
{
    public IEnumerable<string> GetUserProps()
    {
        yield return UserName;
        yield return Email;
        yield return City;
    }
}