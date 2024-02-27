using System.Diagnostics;
using System.Net.Http.Json;
using FluentAssertions;
using RentYourHome.Contracts;
using RentYourHome.Services.Authentication;

namespace RentYourHomeIntegrationTests;

public class RentYourHomeIntegrationTests
{
    [Fact]
    public async Task Login()
    {
        //Arrange
        var application = new RentYourHomeWebApplicationFactory();
        var user = new AuthRequest("admin", "admin123");
        var client = application.CreateClient();

        //Act
        var response = await client.PostAsJsonAsync("/api/auth/login", user);

        //Assert
        response.EnsureSuccessStatusCode();
        var userResponse = await response.Content.ReadAsStringAsync();
        var cookies = response.Headers.GetValues("Set-Cookie");
        var token = cookies
            .SelectMany(cookie => cookie.Split(';'))
            .FirstOrDefault(c => c.Contains("token="))?
            .Split('=')[1];

        userResponse?.Should().Be("Admin");
        token.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Register()
    {
        //Arrange
        var application = new RentYourHomeWebApplicationFactory();
        var userReg = new RegistrationRequest("user1@user1.com", "user1", "123456");
        var user = new AuthRequest("user1", "123456");
        var client = application.CreateClient();

        //Act
        await client.PostAsJsonAsync("/api/auth/register", userReg);
        var response = await client.PostAsJsonAsync("/api/auth/login", user);

        //Assert
        response.EnsureSuccessStatusCode();
        var userResponse = await response.Content.ReadAsStringAsync();
        var cookies = response.Headers.GetValues("Set-Cookie");
        var token = cookies
            .SelectMany(cookie => cookie.Split(';'))
            .FirstOrDefault(c => c.Contains("token="))?
            .Split('=')[1];

        userResponse?.Should().Be("User");
        token.Should().NotBeNullOrEmpty();
    }
}