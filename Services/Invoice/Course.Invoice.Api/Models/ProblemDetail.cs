namespace Course.Invoice.Api.Models;

public sealed class ProblemDetails
{
    public int? StatusCode { get; set; }
    public string? Type { get; set; }
    public string? Title { get; set; }
    public string? Detail { get; set; }
    public string? Instance { get; set; }
}