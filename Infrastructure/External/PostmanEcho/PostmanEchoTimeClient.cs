using Application.External.PostmanEcho;

namespace Infrastructure.External.PostmanEcho;

public class PostmanEchoTimeClient(HttpClient httpClient) : IPostmanEchoTimeClient
{
    public async Task<string?> Now()
    {
        var response = await httpClient.GetAsync("/time/now");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        return null;
    }
}