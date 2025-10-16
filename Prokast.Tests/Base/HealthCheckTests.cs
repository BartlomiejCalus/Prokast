using Prokast.Server.Entities;
using Prokast.Tests.Infrastructure;
using System.Net;

namespace Prokast.Tests.Base
{
    public class HealthCheckTests(TestAppFactory factory) : BaseTest(factory)
    {
        [Fact]
        public async Task HealthCheck_ShouldReturnOk_AfterAuthenticating()
        {
            // Arrange
            await Authenticate(Client);

            // Act
            var response = await Client.GetAsync(new Uri("_health", UriKind.Relative));

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("application/json", response.Content.Headers.ContentType?.ToString());
            Assert.False(string.IsNullOrEmpty(content));
        }

        [Fact]
        public async Task HealthCheck_ShouldReturnUnauthorized_WithoutAuthenticating()
        {
            // Act
            var response = await Client.GetAsync(new Uri("_health", UriKind.Relative));

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
