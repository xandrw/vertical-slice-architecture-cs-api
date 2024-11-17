using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Application.Features.Auth.Login;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Spec.Helpers;

namespace Spec.Steps;

[Binding]
public sealed class HttpStepDefinitions
{
    private const string Host = "http://localhost";
    private const string Port = "5255";
    private const int TimeoutInSeconds = 10;
    
    private readonly HttpClient _client = new()
    {
        BaseAddress = new Uri($"{Host}:{Port}"),
        Timeout = TimeSpan.FromSeconds(TimeoutInSeconds)
    };
    
    private HttpResponseMessage _response = new(HttpStatusCode.OK);

    [Given(@"I authenticate with ""(.*)"" and ""(.*)""")]
    public async Task GivenIAuthenticateWith(string email, string password)
    {
        var request = new LoginRequest(email, password);
        var response = await _client.PostAsJsonAsync("/api/login", request);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadFromJsonAsync<LoginResponse>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseContent?.Token);
    }

    [When("I make a GET request to (.*)")]
    public async Task WhenIMakeAGetRequestTo(string endpoint)
    {
        _response = await _client.GetAsync(endpoint);
    }
    
    [When("I make a POST request to (.*) with the payload:")]
    public async Task WhenIMakeAPostRequestTo(string endpoint, string payload)
    {
        _response = await _client.PostAsync(endpoint, new StringContent(payload, Encoding.UTF8, "application/json"));
    }

    [Then("the response status code should be (.*)")]
    public void ThenTheResponseStatusCodeShouldBe(int statusCode)
    {
        Assert.That((int)_response.StatusCode, Is.EqualTo(statusCode));
    }

    [Then("the response should contain")]
    public async Task ThenTheResponseShouldContain(string expectedContent)
    {
        var responseContent = await _response.Content.ReadAsStringAsync();
        var expectedJson = JToken.Parse(expectedContent.Trim());
        var actualJson = JToken.Parse(responseContent.Trim());

        JsonHelper.CompareJson(expectedJson, actualJson);
    }
}