namespace Application.Interfaces.External.PostmanEcho;

public interface IPostmanEchoTimeClient
{
    Task<string?> Now();
}