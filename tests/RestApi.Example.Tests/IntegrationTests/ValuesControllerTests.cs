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
        public async Task WhenCallValues_ThenTheResultIsOk()
        {
            var response = await this.HttpClientInstance.GetAsync("/v2/values", CancellationToken.None);

            response.EnsureSuccessStatusCode();
        }
    }
}