using System.Net.Http.Json;
using Common.APICommon;
using Tests.Common.ApiTests;
using UserManagement.Application.Auth.AuthenticateUser;
using UserManagement.Domain.Enums;
using UserManagement.Integration.Auth.TestData;
using UserManagement.ORM;
using UserManagement.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace UserManagement.Integration.Auth;

public class AuthenticateUserFeatureTests(WebApplicationFactory<Program> factory) : BaseApiTests<Program, DefaultContext>(factory)
{
    [Fact]
    public async Task AuthenticateUser_ValidUser_ReturnSuccess()
    {
        // Given
        var dbContext = _webFactory.Services
            .CreateScope()
            .ServiceProvider.GetService<DefaultContext>()!;

        var existentUser = AuthenticateUserFeatureTestData.GenerateValidUser();
        var password = existentUser.Password;
        existentUser.Password = AuthenticateUserFeatureTestData.GeneratePasswordHash(password);

        dbContext.Users.Add(existentUser);
        await dbContext.SaveChangesAsync();

        var client = _webFactory.CreateClient();
        var request = new
        {
            Email = existentUser.Email,
            Password = password
        };

        // When
        var response = await client.PostAsJsonAsync("/api/auth", request);

        // Then
        response.EnsureSuccessStatusCode();
        var apiResponseData = await response.Content.ReadFromJsonAsync<ApiResponseWithData<AuthenticateUserResult>>();

        Assert.NotNull(apiResponseData);
        Assert.True(apiResponseData.Success);
        Assert.NotNull(apiResponseData.Data);
        Assert.Equal("User authenticated successfully", apiResponseData.Message);

        var authenticatedUser = apiResponseData.Data;
        Assert.NotEmpty(authenticatedUser.Token);
        Assert.Equal(existentUser.Email, authenticatedUser.Email);
        Assert.Equal(existentUser.Username, authenticatedUser.Username);
        Assert.Equal(existentUser.Role.ToString(), authenticatedUser.Role);
    }

    [Fact]
    public async Task AuthenticateUser_InvalidPassword_ReturnUnauthorized()
    {
        // Given
        var dbContext = _webFactory.Services
            .CreateScope()
            .ServiceProvider.GetService<DefaultContext>()!;

        var existentUser = AuthenticateUserFeatureTestData.GenerateValidUser();
        existentUser.Password = AuthenticateUserFeatureTestData.GeneratePasswordHash(existentUser.Password);

        dbContext.Users.Add(existentUser);
        await dbContext.SaveChangesAsync();

        var client = _webFactory.CreateClient();
        var request = new
        {
            Email = existentUser.Email,
            Password = AuthenticateUserFeatureTestData.GeneratePassword()
        };

        // When
        var response = await client.PostAsJsonAsync("/api/auth", request);

        // Then
        var apiResponseData = await response.Content.ReadFromJsonAsync<ApiResponse>();

        Assert.NotNull(apiResponseData);
        Assert.False(apiResponseData.Success);
        Assert.Equal("Authentication Failed", apiResponseData.Message);
        Assert.Single(apiResponseData.Errors);
        Assert.Collection(apiResponseData.Errors, errorDetail =>
        {
            Assert.Equal("Unauthorized", errorDetail.Error);
            Assert.Equal("Invalid credentials", errorDetail.Detail);
        });
    }

    [Theory]
    [InlineData(UserStatus.Inactive)]
    [InlineData(UserStatus.Suspended)]
    [InlineData(UserStatus.Unknown)]
    public async Task AuthenticateUser_UserNotActive_ReturnUnauthorized(UserStatus status)
    {
        // Given
        var dbContext = _webFactory.Services
            .CreateScope()
            .ServiceProvider.GetService<DefaultContext>()!;

        var existentUser = AuthenticateUserFeatureTestData.GenerateValidUser();
        var password = existentUser.Password;
        existentUser.Password = AuthenticateUserFeatureTestData.GeneratePasswordHash(password);
        existentUser.Status = status;

        dbContext.Users.Add(existentUser);
        await dbContext.SaveChangesAsync();

        var client = _webFactory.CreateClient();
        var request = new
        {
            Email = existentUser.Email,
            Password = password
        };

        // When
        var response = await client.PostAsJsonAsync("/api/auth", request);

        // Then
        var apiResponseData = await response.Content.ReadFromJsonAsync<ApiResponse>();

        Assert.NotNull(apiResponseData);
        Assert.False(apiResponseData.Success);
        Assert.Equal("Authentication Failed", apiResponseData.Message);
        Assert.Single(apiResponseData.Errors);
        Assert.Collection(apiResponseData.Errors,
        detail =>
        {
            Assert.Equal("Unauthorized", detail.Error);
            Assert.Equal("User is not active", detail.Detail);
        });
    }
}