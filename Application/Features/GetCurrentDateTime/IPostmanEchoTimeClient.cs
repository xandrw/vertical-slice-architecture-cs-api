namespace Application.Features.GetCurrentDateTime;

public interface IPostmanEchoTimeClient
{
    Task<string?> Now();
}