namespace RestApi.Example.Tests.IntegrationTests
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Xunit;

    public class ValuesControllerTests : BasicTests
    {
        public ValuesControllerTests(WebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task CanCallV1()
        {
            var response = await this.HttpClientInstance.GetAsync("/v1/api/values", CancellationToken.None);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CanCallV2()
        {
            var response = await this.HttpClientInstance.GetAsync("/v2.1/api/values", CancellationToken.None);

            response.EnsureSuccessStatusCode();
        }
    }
}