using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;
using MyMvcShopDb;

namespace MyMvcShopDb.Web.Tests
{
    public class AnonymousAuthTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<MyMvcShopDb.Program> _factory;

        public AnonymousAuthTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_SecurePage_RedirectsAnnonymousUser()
        {
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            // 2. Act
            var response = await client.GetAsync("/Products/Create");

            // 3. Assert
            Assert.Equal(System.Net.HttpStatusCode.Found, response.StatusCode);

            var location = response.Headers.Location;
            Assert.Contains("/Identity/Account/Login", location.OriginalString);
        }

        [Fact]
        public async Task Get_PublicPage_ReturnsOKForAnonymousUser()
        {
            // 1. Arrange
            var client = _factory.CreateClient();

            // 2. Act
            var response = await client.GetAsync("/Products/Index");

            // 3. Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_LoginPage_ShowsGoogleProvider()
        {
            // 1. Arrange
            var client = _factory.CreateClient();

            // 2. Act
            var response = await client.GetAsync("/Identity/Account/Login");
            var content = await response.Content.ReadAsStringAsync();

            // 3. Assert
            response.EnsureSuccessStatusCode();

            Assert.Contains("Google", content);
        }
    }
}