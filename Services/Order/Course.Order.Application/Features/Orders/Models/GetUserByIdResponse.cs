using System.Text.Json.Serialization;

namespace Course.Order.Application.Features.Orders.Models;
public class GetUserByIdResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("userName")]
    public string UserName { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }
}
