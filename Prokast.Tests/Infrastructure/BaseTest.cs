using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Prokast.Server;
using Prokast.Server.Entities;
using Prokast.Server.Models.AccountModels;
using Prokast.Server.Seeders;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Prokast.Tests.Infrastructure;

public abstract class BaseTest : IClassFixture<TestAppFactory>
{
    protected const string BaseUri = "https://localhost/";
    protected const string CurrentPassword = "marmar";
    protected const string CurrentLogin = "marmar123";

    protected readonly ProkastServerDbContext DbContext;
    protected readonly HttpClient Client;

    protected readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = false,
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    protected BaseTest(TestAppFactory factory)
    {
        var scope = factory.Services.CreateScope();

        DbContext = scope.ServiceProvider.GetRequiredService<ProkastServerDbContext>();

        DbContext.Database.Migrate();

        var seeder = scope.ServiceProvider.GetRequiredService<MainSeeder>();
        seeder.SeedDB();

        Client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri(BaseUri),
            HandleCookies = true
        });
    }

    protected static async Task<HttpResponseMessage> Authenticate(HttpClient client, string? login = null, string? password = null)
    {
        var loginCredentials = new LoginRequest
        {
            Login = string.IsNullOrWhiteSpace(login) ? CurrentLogin : login,
            Password = string.IsNullOrWhiteSpace(password) ? CurrentPassword : password
        };

        var res = await client.PostAsync(new Uri("api/login", UriKind.Relative), new StringContent(JsonSerializer.Serialize(loginCredentials), Encoding.UTF8, "application/json"));

        res.EnsureSuccessStatusCode();

        var json = await res.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);

        var token = doc.RootElement.GetProperty("token").GetString();

        if (string.IsNullOrEmpty(token))
            throw new InvalidOperationException("No token returned from login.");

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return res;
    }
}
