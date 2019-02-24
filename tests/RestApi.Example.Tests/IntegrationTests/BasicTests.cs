namespace RestApi.Example.Tests.IntegrationTests
{
    using System;
    using System.Net.Http;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Xunit;

    public abstract class BasicTests : IClassFixture<WebApplicationFactory<Startup>>, IDisposable
    {
        private readonly WebApplicationFactory<Startup> factory;

        protected BasicTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "TEST");
            this.HttpClientInstance = factory.CreateClient();
        }

        protected HttpClient HttpClientInstance { get; private set; }

        public void Dispose()
        {
            this.HttpClientInstance.Dispose();
            this.factory.Dispose();
        }
    }
}
