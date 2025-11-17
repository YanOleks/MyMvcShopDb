using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace MyMvcShopDb.Web.Tests
{
    public class AuthTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AuthTests(CustomWebApplicationFactory<MyMvcShopDb.Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_SecurePage_ReturnsOKForAuthenticatedUser()
        {
            // 1. Arrange

            // 2. Act
            var response = await _client.GetAsync("/Products/Create");

            // 3. Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}