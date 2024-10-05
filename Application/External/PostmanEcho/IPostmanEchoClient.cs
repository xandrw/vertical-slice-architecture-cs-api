namespace Application.External.PostmanEcho;

public interface IPostmanEchoTimeClient
{
    Task<string?> Now();
}