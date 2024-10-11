namespace Application.Common.Http.Responses;

public class UnprocessableEntityResponse
{
    public string Error { get; set; } = string.Empty;
    public int Status { get; set; }
    public Dictionary<string, string[]?> Errors { get; init; } = new();
}