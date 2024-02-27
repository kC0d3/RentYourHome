using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using RentYourHome.Contracts;
using RentYourHome.Models.Addresses;
using RentYourHome.Models.Ads;
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
        var userReg = new RegistrationRequest("user1@user1.com", "user1", "firstname", "lastname", "123456");
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

    [Fact]
    public async Task CreateAd()
    {
        //Arrange
        var application = new RentYourHomeWebApplicationFactory();
        var userReg = new RegistrationRequest("user1@user1.com", "user1", "firstname", "lastname", "123456");
        var user = new AuthRequest("user1", "123456");
        var ad = new AdReqDto
        {
            Address = new AddressDto
            {
                City = "City",
                HouseNumber = "HouseNumber",
                Street = "Street",
                ZipCode = "ZipCode"
            },
            Description = "Description",
            Images = new[] { "1.jpg" },
            Price = 1,
            Rooms = 1,
            Size = 1,
            UserId = 1
        };

        var client = application.CreateClient();
        await client.PostAsJsonAsync("/api/auth/register", userReg);
        var logResp = await client.PostAsJsonAsync("/api/auth/login", user);
        var cookies = logResp.Headers.GetValues("Set-Cookie");
        var token = cookies
            .SelectMany(cookie => cookie.Split(';'))
            .FirstOrDefault(c => c.Contains("token="))?
            .Split('=')[1];
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


        //Act
        var response = await client.PostAsJsonAsync("/api/ads", ad);

        //Asserrt
        response.EnsureSuccessStatusCode();
        var adResponse = await response.Content.ReadFromJsonAsync<AdReqDto>();
        adResponse?.UserId.Should().Be(1);
    }

    [Fact]
    public async Task ApproveAd()
    {
        //Arrange
        var application = new RentYourHomeWebApplicationFactory();
        var userReg = new RegistrationRequest("user1@user1.com", "user1", "firstname", "lastname", "123456");
        var admin = new AuthRequest("admin", "admin123");
        var user = new AuthRequest("user1", "123456");
        var ad = new AdReqDto
        {
            Address = new AddressDto
            {
                City = "City",
                HouseNumber = "HouseNumber",
                Street = "Street",
                ZipCode = "ZipCode"
            },
            Description = "Description",
            Images = new[] { "1.jpg" },
            Price = 1,
            Rooms = 1,
            Size = 1,
            UserId = 1
        };

        var client = application.CreateClient();
        //User reg, login & logout
        await client.PostAsJsonAsync("/api/auth/register", userReg);
        var userLogResp = await client.PostAsJsonAsync("/api/auth/login", user);
        var userCookies = userLogResp.Headers.GetValues("Set-Cookie");
        var userToken = userCookies
            .SelectMany(cookie => cookie.Split(';'))
            .FirstOrDefault(c => c.Contains("token="))?
            .Split('=')[1];
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
        await client.PostAsJsonAsync("/api/ads", ad);
        await client.PostAsync("/api/auth/logout", null);

        //Admin login
        var adminLogResp = await client.PostAsJsonAsync("/api/auth/login", admin);
        var adminCookies = adminLogResp.Headers.GetValues("Set-Cookie");
        var adminToken = adminCookies
            .SelectMany(cookie => cookie.Split(';'))
            .FirstOrDefault(c => c.Contains("token="))?
            .Split('=')[1];
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);

        //Act
        await client.PutAsync("/api/ads/approve/1", null);
        var response = await client.GetAsync("/api/ads/1");

        //Asserrt
        response.EnsureSuccessStatusCode();
        var adResponse = await response.Content.ReadFromJsonAsync<AdDto>();
        adResponse?.Id.Should().Be(1);
        adResponse?.Approved.Should().Be(true);
    }
}