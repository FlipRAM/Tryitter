using System.Net;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FluentAssertions;
using Tryitter.Controllers;
using Xunit;
using System.Text;

namespace SchoolLogin.Test
{
  public class TestLoginController : IClassFixture<WebApplicationFactory<Program>>
  {
    private readonly WebApplicationFactory<Program> _factory;

    public TestLoginController(WebApplicationFactory<Program> factory)
    {
      _factory = factory;
    }

    [Theory(DisplayName = "Logs in with correct user and password")]
    [InlineData("usario@gmail.com", "Teste@3452323!")]
    public async Task TestLoginSuccess(string email, string password)
    {
      var user = new LoginRequest
      {
        Email = email,
        Password = password
      };

      var json = JsonConvert.SerializeObject(user);
      var body = new StringContent(json, Encoding.UTF8, "application/json");
      var client = _factory.CreateClient();
      var httpResponse = await client.PostAsync("/login", body);

      httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
      var responseContent = await httpResponse.Content.ReadAsStringAsync();
      responseContent.Should().NotBeNullOrEmpty();
    }

    [Theory(DisplayName = "Fail to log in with not found user")]
    [InlineData("usuarionaocadastrado@gmail.com", "Teste@3452323!")]
    public async Task TestFailLoginNotFound(string email, string password)
    {
      var student = new LoginRequest
      {
        Email = email,
        Password = password
      };

      var json = JsonConvert.SerializeObject(student);
      var body = new StringContent(json, Encoding.UTF8, "application/json");
      var client = _factory.CreateClient();
      var httpResponse = await client.PostAsync("/login", body);

      httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Theory(DisplayName = "Fail to log in with Bad Request")]
    [InlineData("usuario@gmail.com", "")]
    public async Task TestFailLoginBadRequest(string email, string password)
    {
      var student = new LoginRequest
      {
        Email = email,
        Password = password
      };

      var json = JsonConvert.SerializeObject(student);
      var body = new StringContent(json, Encoding.UTF8, "application/json");
      var client = _factory.CreateClient();
      var httpResponse = await client.PostAsync("/login", body);

      httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
  }
}