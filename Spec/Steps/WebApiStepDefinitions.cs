using System.Net;
using NUnit.Framework;

namespace Spec.Steps;

[Binding]
public class WebApiStepDefinitions
{
    private readonly HttpClient _client = new()
    {
        BaseAddress = new Uri("http://localhost:5255"),
        Timeout = TimeSpan.FromSeconds(30)
    };
    private HttpResponseMessage _response = new(HttpStatusCode.OK);

    [When(@"I make a GET request to ""(.*)""")]
    public async Task WhenIMakeAGetRequestTo(string endpoint)
    {
        _response = await _client.GetAsync(endpoint);
    }

    [Then(@"the response status code should be (.*)")]
    public void ThenTheResponseStatusCodeShouldBe(int statusCode)
    {
        Assert.That((int)_response.StatusCode, Is.EqualTo(statusCode));
    }

    [Then(@"the response content should contain ""(.*)""")]
    public async Task ThenTheResponseContentShouldContain(string expectedContent)
    {
        var content = await _response.Content.ReadAsStringAsync();
        Assert.That(content, Does.Contain(expectedContent));
    }
}